using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using CyloAPI.Model;
using System.Collections.Generic;
using System;

namespace CyloAPI.Respository
{
    public class AdminRepository
    {
        private readonly IConfiguration _configuration;
        public AdminRepository(IConfiguration config)
        {
            _configuration = config;
        }
        public string addAdmin(AdminDetails admin)
        {
            string query = "insert into dbo.AdminDetails values(@name,@email,@username,@password)";
            string sqlDataSource = _configuration.GetConnectionString("BicycleAppCon");
            SqlDataReader myReader;
            try
            {
                using (SqlConnection myConn = new SqlConnection(sqlDataSource))
                {
                    myConn.Open();
                    using (SqlCommand myCmd = new SqlCommand(query, myConn))
                    {
                        myCmd.Parameters.AddWithValue("@name", admin.name);
                        myCmd.Parameters.AddWithValue("@email", admin.email);
                        myCmd.Parameters.AddWithValue("@username", admin.username);
                        myCmd.Parameters.AddWithValue("@password", admin.password);
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
        public List<AdminDetails> getAdmin()
        {

            List<AdminDetails> admins = new List<AdminDetails>();
            string query = @"select * from dbo.AdminDetails";
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
                            AdminDetails admin = new AdminDetails();
                            admin.id = Convert.ToInt32(myReader["id"]);
                            admin.name = myReader["name"].ToString();
                            admin.email = myReader["email"].ToString();
                            admin.password = myReader["password"].ToString();
                            admin.username = myReader["username"].ToString();
                            admins.Add(admin);

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
            return admins;
        }
       
    }
}
