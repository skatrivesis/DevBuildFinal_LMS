﻿using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using DevBuildFinal_LMS.Models;
using System.Data.SqlClient;

namespace DevBuildFinal_LMS.Services
{
    public class UserDataService : IUserDataService
    {
        private readonly string connString;

        public UserDataService(IConfiguration config)
        {
            connString = config.GetConnectionString("default");
        }

        public IEnumerable<User> GetUsers()
        {
            SqlConnection conn = new SqlConnection(connString);

            string command = "SELECT * FROM LMS.dbo.UserTable";

            IEnumerable<User> result = conn.Query<User>(command);

            conn.Close();

            return result;
        }

        public IEnumerable<User> GetTeachers()
        {
            SqlConnection conn = new SqlConnection(connString);

            string command = "SELECT * FROM LMS.dbo.UserTable where userTypeId = 2";

            IEnumerable<User> result = conn.Query<User>(command);

            conn.Close();

            return result;
        }

        public IEnumerable<User> GetRegularUsers()
        {
            SqlConnection conn = new SqlConnection(connString);

            string command = "SELECT * FROM LMS.dbo.UserTable where userTypeId != 1";

            IEnumerable<User> result = conn.Query<User>(command);

            conn.Close();

            return result;
        }

        public IEnumerable<string> GetUsersNames()
        {
            SqlConnection conn = new SqlConnection(connString);

            string command = "SELECT userName FROM LMS.dbo.UserTable";

            IEnumerable<string> result = conn.Query<string>(command);

            conn.Close();

            return result;
        }

        public User GetUserByName(string userName)
        {
            SqlConnection conn = new SqlConnection(connString);

            string command = "SELECT * FROM LMS.dbo.UserTable WHERE userName = @val";

            User result = conn.QueryFirst<User>(command, new { val = userName });

            conn.Close();

            return result;
        }

        public int AddUser(User user)
        {
            SqlConnection conn = new SqlConnection(connString);

            string command = "insert into LMS.dbo.UserTable (userTypeId, userName) ";
            command += "values (@userTypeId, @userName)";

            int result = conn.Execute(command, user);

            conn.Close();

            return result;
        }

        public int ChangeAdminStatus(User user)
        {
            SqlConnection conn = new SqlConnection(connString);

            string command = "update LMS.dbo.UserTable set userTypeId = 1 where userId = @userId";

            int result = conn.Execute(command, new { userId = user.userId });

            conn.Close();

            return result;
        }

    }
}
