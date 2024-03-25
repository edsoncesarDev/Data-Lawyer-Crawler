
using DataLawyer.Domain.Shared;
using System.Security.Cryptography;
using System.Text;

namespace DataLawyer.Domain.Entities;

public class User : EntityBase
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;

    public User() { }
    
    public void PasswordEncryption(string password)
    {
        byte[] dataBytes = Encoding.ASCII.GetBytes(password);
        using (SHA512 sha512 = new SHA512Managed())
        {
            byte[] hashBytes = sha512.ComputeHash(dataBytes);
            string hash = Convert.ToBase64String(hashBytes);
            Password =  hash;
        }
    }
}
