using DataLawyer.Domain.Entities;

namespace DataLawyer.Domain.Interfaces;

public interface IProcessCrawler
{
    Task<Process> CrawlerProcessAsync(string process);
}
