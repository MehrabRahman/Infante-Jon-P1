using System.Security.Cryptography;

namespace DL;

public static class PasswordHash
{

    public static string GenerateHashedPassword(string userPass)
    {
        //create salt value
        byte[] salt;
        RandomNumberGenerator rng = RandomNumberGenerator.Create();
        rng.GetBytes(salt = new byte[16]);

        //get hash value
        var pbkdf2 = new Rfc2898DeriveBytes(userPass, salt, 100000);
        byte[] hash = pbkdf2.GetBytes(20);

        //Combine salt and password bytes
        byte[] hashBytes = new byte[36];
        Array.Copy(salt, 0, hashBytes, 0, 16);
        Array.Copy(hash, 0, hashBytes, 16, 20);

        //Convery byte array to string
        string savedPasswordHash = Convert.ToBase64String(hashBytes);

        return savedPasswordHash;
    }

    public static bool VeryifyHashedPassword(string userPass, string hashedPassword)
    {
        //Extracting bytes
        byte[] hashBytes = Convert.FromBase64String(hashedPassword);

        //Get new salt
        byte[] salt = new byte[16];
        //copying the hashed bytes into the salt array, 0 to 16
        Array.Copy(hashBytes, 0, salt, 0, 16);

        //Computing the hash on the user's input password
        var pbkdf2 = new Rfc2898DeriveBytes(userPass, salt, 100000);
        byte[] hash = pbkdf2.GetBytes(20);

        //Comparing the results of each array and returns a boolean
        return hash.SequenceEqual(hashBytes.Skip(16));
    }

}