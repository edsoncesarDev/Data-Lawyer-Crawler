using DataLawyer.Domain.Entities;
using DataLawyer.Domain.Interfaces;
using DataLawyer.Domain.Validation;
using HtmlAgilityPack;
using System.Globalization;
using System.Text;
using System.Web;
using static System.Net.Mime.MediaTypeNames;


namespace DataLawyer.Infrastructure.CrawlerFunction;

public class ProcessCrawler : IProcessCrawler
{
    private readonly HtmlWeb _htmlWeb;

    public ProcessCrawler()
    {
        _htmlWeb = new HtmlWeb();
        _htmlWeb.OverrideEncoding = Encoding.UTF8;
    }

    public async Task<Process> CrawlerProcessAsync(string process)
    {
        var responseUrl = string.Format($"http://esaj.tjba.jus.br/cpo/sg/search.do?paginaConsulta=1&cbPesquisa=NUMPROC&tipoNuProcesso=SAJ&numeroDigitoAnoUnificado=&foroNumeroUnificado=&dePesquisaNuUnificado=&dePesquisa={process}&pbEnviar=Pesquisar");
        var document = await _htmlWeb.LoadFromWebAsync(responseUrl);
        return (AddProcess(document));
    }

    private Process AddProcess(HtmlDocument document)
    {
        string processNumber = string.Empty;

        if (document.DocumentNode.SelectSingleNode("/html/body/table[4]//tr/td/table[2]//tr[1]/td[2]/table//tr/td/span[1]") == null)
            DomainValidationExceptions.When(true, "Invalid process number.");

        processNumber = FormatDocument(document.DocumentNode.SelectSingleNode("/html/body/table[4]//tr/td/table[2]//tr[1]/td[2]/table//tr/td/span[1]").InnerText);
        var situation = FormatDocument(document.DocumentNode.SelectSingleNode("/html/body/table[4]//tr/td/table[2]//tr[1]/td[2]/table//tr/td/span[3]").InnerText);
        var grade = FormatDocument(document.DocumentNode.SelectSingleNode("html/body/table[4]//tr/td/table[2]//tr[2]/td[2]/table//tr/td/span/span").InnerText);
        var area = FormatDocument(document.DocumentNode.SelectSingleNode("//span[@class='labelClass']/following-sibling::text()[1]").InnerText);
        var topic = FormatDocument(document.DocumentNode.SelectSingleNode("/html/body/table[4]//tr/td/table[2]//tr[4]/td[2]/span").InnerText);
        var from = FormatDocument(document.DocumentNode.SelectSingleNode("/html/body/table[4]//tr/td/table[2]//tr[5]/td[2]/span/text()").InnerText);
        var distribution = FormatDocument(document.DocumentNode.SelectSingleNode("/html/body/table[4]//tr/td/table[2]//tr[7]/td[2]/span").InnerText);
        var rapporteur = FormatDocument(document.DocumentNode.SelectSingleNode("/html/body/table[4]//tr/td/table[2]//tr[8]/td[2]/span").InnerText);
        var moviments = document.DocumentNode.SelectSingleNode("/html/body/table[4]//tr/td/table[10]");

        var process = new Process(processNumber, FormatText(situation), FormatText(grade), new Area(FormatText(area)), FormatText(topic), FormatText(from), FormatText(distribution), FormatText(rapporteur));

        return (AddMoviment(process, moviments));

    }

    private Process AddMoviment(Process process, HtmlNode moviments)
    {
        if (moviments.ChildNodes.Count() > 0)
        {
            var collection = moviments.ChildNodes;
            
            foreach (var item in collection)
            {
                if (item.Name == "tr")
                {
                    var format = FormatDocument(item.InnerText);

                    string[] separateText = format.Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries);
                    var date = DateTime.ParseExact(separateText[0], "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    string description = string.Empty;
                    for (int i = 1; i < separateText.Length; i++)
                    {
                        description += $"{separateText[i]} ";
                    }

                    process.AddMovement(new Movement(FormatText(FormatDocument(description.Trim())), date));

                }
            }
        }

        return process;
    }

    private string FormatDocument(string value)
    {
        var formattedText = value.Replace("\n", "").Replace("\t", "").Replace("\r", "").Trim();
        return formattedText;
    }

    private string FormatText(string text)
    {
        string[] separateText = text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

        string description = string.Empty;

        for (int i = 0; i < separateText.Length; i++)
        {
            separateText[i] = (separateText[i].Contains("Apela��o")) ? separateText[i] = "Apelação" : separateText[i];
            separateText[i] = (separateText[i].Contains("C�vel")) ? separateText[i] = "Cível" : separateText[i];
            separateText[i] = (separateText[i].Contains("V�cios")) ? separateText[i] = "Vícios" : separateText[i];
            separateText[i] = (separateText[i].Contains("Constru��o")) ? separateText[i] = "Construção" : separateText[i];
            separateText[i] = (separateText[i].Contains("1�")) ? separateText[i] = "1ª" : separateText[i];
            separateText[i] = (separateText[i].Contains("C�mara")) ? separateText[i] = "Câmara" : separateText[i];
            separateText[i] = (separateText[i].Contains("Expedi��o")) ? separateText[i] = "Expedição" : separateText[i];
            separateText[i] = (separateText[i].Contains("Certid�o")) ? separateText[i] = "Certidão" : separateText[i];
            separateText[i] = (separateText[i].Contains("N�cleo")) ? separateText[i] = "Núcleo" : separateText[i];
            separateText[i] = (separateText[i].Contains("Digitaliza��o")) ? separateText[i] = "Digitalização" : separateText[i];
            separateText[i] = (separateText[i].Contains("&Agrave")) ? separateText[i] = "À" : separateText[i];
            separateText[i] = (separateText[i].Contains("&agrave")) ? separateText[i] = "à" : separateText[i];
            separateText[i] = (separateText[i].Contains("Judici&aacute;rio")) ? separateText[i] = "Judiciário" : separateText[i];
            separateText[i] = (separateText[i].Contains("n&ordm;")) ? separateText[i] = "nº" : separateText[i];
            separateText[i] = (separateText[i].Contains("edi&ccedil;&atilde;o")) ? separateText[i] = "edição" : separateText[i];
            separateText[i] = (separateText[i].Contains("h&aacute;")) ? separateText[i] = "há" : separateText[i];
            separateText[i] = (separateText[i].Contains("in&iacute;cio")) ? separateText[i] = "início" : separateText[i];
            separateText[i] = (separateText[i].Contains("digitaliza&ccedil;&atilde;o")) ? separateText[i] = "digitalização" : separateText[i];
            separateText[i] = (separateText[i].Contains("f&iacute;sicos")) ? separateText[i] = "físicos" : separateText[i];
            separateText[i] = (separateText[i].Contains("1&ordf;")) ? separateText[i] = "1ª" : separateText[i];
            separateText[i] = (separateText[i].Contains("C&acirc;mara")) ? separateText[i] = "Câmara" : separateText[i];
            separateText[i] = (separateText[i].Contains("C&iacute;vil")) ? separateText[i] = "Cívil" : separateText[i];
            separateText[i] = (separateText[i].Contains("Justi&ccedil;a")) ? separateText[i] = "Justiça" : separateText[i];
            separateText[i] = (separateText[i].Contains("Eletr&ocirc;nico-")) ? separateText[i] = "Eletrônico-" : separateText[i];
            separateText[i] = (separateText[i].Contains("refer&ecirc;ncia")) ? separateText[i] = "referência" : separateText[i];
            separateText[i] = (separateText[i].Contains("migra&ccedil;&atilde;o.")) ? separateText[i] = "Migração." : separateText[i];

            description += $"{separateText[i]} ";

            if (separateText.Length == 1)
                description = description.Trim();

            if(i == separateText.Length - 1)
                description = description.Trim();
        }

        return description;
    }
   
}
