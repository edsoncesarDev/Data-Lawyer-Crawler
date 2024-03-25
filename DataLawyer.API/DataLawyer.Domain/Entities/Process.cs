
using DataLawyer.Domain.Shared;

namespace DataLawyer.Domain.Entities;

public class Process : EntityBase
{
    public string ProcessNumber { get; private set; } = null!;
    public string Situation { get; private set; } = null!;
    public string Grade { get; private set; } = null!;
    public Area Area { get; private set; } = null!;
    public string Topic { get; private set; } = null!;
    public string From { get; private set; } = null!;
    public string Distribution { get; private set; } = null!;
    public string Rapporteur { get; private set; } = null!;
    public List<Movement> Movements { get; private set; } = new List<Movement>();

    public Process() { }

    public Process(string processNumber, string situation, string grade, Area area, string topic, string from, string distribution, string rapporteur)
    {
        SetProcess(processNumber, situation, grade, area, topic, from, distribution, rapporteur);
    }

    public void SetProcess(string processNumber, string situation, string grade, Area area, string topic, string from, string distribution, string rapporteur)
    {
        ProcessNumber = processNumber;
        Situation = situation;
        Grade = grade;
        Area = area;
        Topic = topic;
        From = from;
        Distribution = distribution;
        Rapporteur = rapporteur;
    }

    public void AddMovement(Movement value)
    {
        if (value != null)
            Movements.Add(value!);
    }

    public void RemoveMovement(Movement value)
    {
        if (value != null)
            Movements.Remove(value!);
    }
}
