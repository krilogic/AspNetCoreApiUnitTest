using AspNetCoreApi.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace AspNetCoreApi.Repository
{
    public class UserRepository : IUserRepository
    {
        IConfiguration _configuration;
        public string constring;

        public UserRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            constring = _configuration["ConnectionStrings:DefaultConnection"];
        }
        public int Add(User user)
        {
            int count = 0;
            string strSql = $"Insert into Users(FirstName, LastName, Email, Password) values('{user.firstName}','{user.lastName}','{user.email}','{user.password}')";
            using (SqlConnection con = new SqlConnection(constring))
            {
                using (SqlCommand cmd = new SqlCommand(strSql, con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.Text;
                    count = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return count;
        }

        public int DeleteUser(int id)
        {
            string strSql = $"Delete from User where Id = {id}";
            int count = 0;
            using (SqlConnection con = new SqlConnection(constring))
            {
                using (SqlCommand cmd = new SqlCommand(strSql, con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.Text;
                    count = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return count;
        }

        public int EditUser(User user)
        {
            string strSql = $"update Users set FirstName = '{user.firstName}', LastName = '{user.lastName}', Email = '{user.email}', Password ='{user.password}'   where Id = '{user.id}'";
            int count = 0;
            using (SqlConnection con = new SqlConnection(constring))
            {
                using (SqlCommand cmd = new SqlCommand(strSql, con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.Text;
                    count = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return count;
        }

        public List<User> GetList()
        {
            List<User> userList = new List<User>();
            string strSql = "Select * From Users";
            DataTable dtuserInfo = null;
            using (SqlConnection con = new SqlConnection(constring))
            {
                con.Open();
                string sql = strSql;
                SqlCommand cmd = new SqlCommand(strSql, con);
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (!(dt.Rows.Count > 0))
                    dt = null;
                dtuserInfo = dt;
            }
            if (dtuserInfo != null)
            {
                userList = new List<User>();
                foreach (DataRow dr in dtuserInfo.Rows)
                    userList.Add(GetUserInfoByRow(dr));
            }
            return userList;
        }

        public User GetUser(int id)
        {
            User userInfo = null;
            string strSql = $"select * from Users where Id= {id}";
            DataTable dtUserInfo = null;
            using (SqlConnection con = new SqlConnection(constring))
            {
                con.Open();
                string sql = strSql;
                SqlCommand cmd = new SqlCommand(strSql, con);
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (!(dt.Rows.Count > 0))
                    dt = null;
                dtUserInfo = dt;
            }
            if (dtUserInfo != null)
            {
                DataRow dr = dtUserInfo.Rows[0];
                userInfo = GetUserInfoByRow(dr);
            }
            return userInfo;
        }

        public User GetUserInfoByRow(DataRow dr)
        {
            User user = new User();
            user.id = Convert.ToInt32(dr["Id"]);
            user.firstName = Convert.ToString(dr["FirstName"]);
            user.lastName = Convert.ToString(dr["LastName"]);
            user.email = Convert.ToString(dr["Email"]);
            user.password = Convert.ToString(dr["Password"]);
            return user;
        }
    }
}
