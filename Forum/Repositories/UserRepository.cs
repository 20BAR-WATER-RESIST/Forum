﻿using Dapper;
using Forum.Context;
using Forum.Contracts;
using Forum.Models;
using System.Reflection.Metadata.Ecma335;

namespace Forum.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DefaultDbContext _context;

        public UserRepository(DefaultDbContext context)
        {
            _context = context;
        }

        public async Task<User> LoadUserProfileHeader(string profileName)
        {
            using(var connection = _context.CreateConnection())
            {
                var query = @"select u.UserID, u.UserName, u.UserRegisteredDate, u.UserTypeID, ut.UserTypeName from users u
                              left join userstypes ut on ut.UserTypeID = u.UserTypeID
                              where u.UserName = @Name;";
                var result = await connection.QueryAsync<User, UserType, User>(
                    sql: query,
                    map: (user, userType) =>
                    {
                        user.UserTypes = userType;
                        return user;
                    },
                    param: new { Name = profileName },
                    splitOn: "UserTypeID"
                    );
                return result.FirstOrDefault();
            }
        }

    }
}
