using Blog.DataLibrary.DataAccess;
using Blog.DataLibrary.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Blog.DataLibrary.BusinessLogic
{
    public class AccountProcessor : IAccountProcessor
    {
        private ISqlDataAccess _sqlDataAccess;

        public AccountProcessor(ISqlDataAccess sqlDataAccess)
        {
            _sqlDataAccess = sqlDataAccess;
        }

        public async Task<int> Create(
            string userName, 
            string email, 
            string salt, 
            string passwordHash)
            => await _sqlDataAccess.SaveData(
                "spUser_Insert @UserName, @Email, @Salt, @PasswordHash", 
                new User
                {
                    UserName = userName,
                    Email = email,
                    Salt = salt,
                    PasswordHash = passwordHash
                });

        public async Task<User> LoadByEmail(string email)
            => (await _sqlDataAccess
                .LoadData<User>("spUser_LoadByEmail @Email", new 
                {
                    Email = email 
                }))
                .FirstOrDefault();

        /*public Task<IEnumerable<User>> Load()
        {

        }

        public Task<User> Load(int id)
        {

        }

        public Task<User> Load(string title)
        {

        }

        public Task<int> Update(int id, string title, string body)
        {
            
        }

        public Task<int> Delete(int id)
        {

        }*/

        public byte[] Hash(string password, byte[] salt)
        {
            using (var rngCsp = new RNGCryptoServiceProvider())
            {
                rngCsp.GetNonZeroBytes(salt);
            }

            return KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8);
        }
    }
}
