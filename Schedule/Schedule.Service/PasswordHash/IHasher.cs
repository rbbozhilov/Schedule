using System.Security.Cryptography;
using System;

namespace Schedule.Service.PasswordHash
{
    public interface IHasher
    {
        void AddUser(string username, string password);

        string GetUserPass(string password);

         string CreateHash(string password);

         bool ValidatePassword(string password, string correctHash);

    }
}
