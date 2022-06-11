using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using CyloAPI.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System;

namespace CyloAPI.Respository
{
    public class UserRepository
    {
        private readonly IConfiguration _configuration;
        public UserRepository(IConfiguration config)
        {
            _configuration = config;

        }
        public string addUser(UserDetails user)
        {

            string query = "insert into dbo.UserDetails values(@name,@email,@contact,@password)";
            string sqlDataSource = _configuration.GetConnectionString("BicycleAppCon");
            SqlDataReader myReader;
            try
            {
                using (SqlConnection myConn = new SqlConnection(sqlDataSource))
                {
                    myConn.Open();
                    using (SqlCommand myCmd = new SqlCommand(query, myConn))
                    {
                        myCmd.Parameters.AddWithValue("@name", user.name);
                        myCmd.Parameters.AddWithValue("@email", user.email);
                        myCmd.Parameters.AddWithValue("@contact", user.contact);
                        myCmd.Parameters.AddWithValue("@password", user.password);
                        myReader = myCmd.ExecuteReader();
                        myReader.Close();
                        myConn.Close();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error Occurred!");
            }
            return "Added Successfully";
        }

        public List<UserDetails> getUser()
        {
            List<UserDetails> users = new List<UserDetails>();  
            string query = @"select * from dbo.UserDetails";
            string sqlDataSource = _configuration.GetConnectionString("BicycleAppCon");
            SqlDataReader myReader;
            try {
                using (SqlConnection myConn = new SqlConnection(sqlDataSource))
                {
                    myConn.Open();
                    using (SqlCommand myCmd = new SqlCommand(query, myConn))
                    {
                        myReader = myCmd.ExecuteReader();
                        while (myReader.Read())
                        {
                            UserDetails user = new UserDetails();
                            user.id = Convert.ToInt32(myReader["id"]);
                            user.name = myReader["name"].ToString();
                            user.email = myReader["email"].ToString();
                            user.password = myReader["password"].ToString();
                            user.contact = Convert.ToInt64(myReader["contact"]);
                            users.Add(user);

                        }
                        myReader.Close();
                        myConn.Close();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error Occurred!");
            }
                   
  
            return users;
        }
        public string updateUser(UserDetails user)
        {
            string query = @"update dbo.UserDetails set password=@password where id=@id";

            string sqlDataSource = _configuration.GetConnectionString("BicycleAppCon");
            SqlDataReader myReader;
            try
            {
                using (SqlConnection myConn = new SqlConnection(sqlDataSource))
                {
                    myConn.Open();
                    using (SqlCommand myCmd = new SqlCommand(query, myConn))
                    {
                        myCmd.Parameters.AddWithValue("@id", user.id);
                        myCmd.Parameters.AddWithValue("@password", user.password);
                        myReader = myCmd.ExecuteReader();
                        myReader.Close();
                        myConn.Close();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error Occurred!");
            }
            return "Updated Successfully";
        }
    }
}
