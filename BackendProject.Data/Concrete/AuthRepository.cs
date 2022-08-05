﻿using BackendProject.Data.Abstract;
using BackendProject.Entity.Context;
using BackendProject.Entity.DomainModels;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BackendProject.Data.Concrete
{
    public class AuthRepository : IAuthRepository
    {
        private readonly PersonAdminContext context;

        public AuthRepository(PersonAdminContext context)
        {
            this.context = context;
        }
        public async Task<User> Login(string userName, string password)
        {
            var user = await context.User.FirstOrDefaultAsync(x => x.UserName == userName);
            if(user==null)
            {
                return null;
            }

            if(!VerifyPasswordHash(password,user.PasswordHash,user.PasswordSalt))
            {
                return null;
            }
            return user;
        }

        private bool VerifyPasswordHash(string password, byte[] userPasswordHash, byte[] userPasswordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(userPasswordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for(int i=0;i<computedHash.Length;i++)
                {
                    if(computedHash[i]!=userPasswordHash[i] )
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        public async Task<User> Register(User user, string password)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await context.User.AddAsync(user);
            await context.SaveChangesAsync();
            return user;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac=new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public async Task<bool> UserExists(string userName)
        {
            if(await context.User.AnyAsync(x=>x.UserName==userName))
            {
                return true;
            }
            return false;
        }
    }
}
