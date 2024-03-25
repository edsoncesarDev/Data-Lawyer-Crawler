
namespace DataLawyer.Domain.Validation;

public  class DomainValidationExceptions : Exception
{
    public DomainValidationExceptions(string error) : base(error) { }

    public static void When(bool hasError, string error)
    {
        if (hasError)
            throw new DomainValidationExceptions(error);
    }
}
