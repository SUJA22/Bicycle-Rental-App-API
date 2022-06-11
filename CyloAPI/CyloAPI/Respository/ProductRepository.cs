using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using CyloAPI.Model;
using System.Collections.Generic;
using System;

namespace CyloAPI.Respository
{
    public class ProductRepository
    {
        private readonly IConfiguration _configuration;

        public ProductRepository(IConfiguration config)
        {
            _configuration = config;
        }

        public string addProduct(ProductDetails product ) {
            string query = @"insert into dbo.ProductDetails values(@name,@type,@perHourCost,@imagePath)";
            string sqlDataSource = _configuration.GetConnectionString("BicycleAppCon");
            SqlDataReader myReader;
            try
            {
                using (SqlConnection myConn = new SqlConnection(sqlDataSource))
                {
                    myConn.Open();
                    using (SqlCommand myCmd = new SqlCommand(query, myConn))
                    {
                        myCmd.Parameters.AddWithValue("@name", product.name);
                        myCmd.Parameters.AddWithValue("@type", product.type);
                        myCmd.Parameters.AddWithValue("@perHourCost", product.perHourCost);
                        myCmd.Parameters.AddWithValue("@imagePath", product.imagePath);
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
        public List<ProductDetails> getProduct() {
            string query = @"select * from dbo.ProductDetails";
            List<ProductDetails> products = new List<ProductDetails>();
            string sqlDataSource = _configuration.GetConnectionString("BicycleAppCon");
            SqlDataReader myReader;
            try
            {
                using (SqlConnection myConn = new SqlConnection(sqlDataSource))
                {
                    myConn.Open();
                    using (SqlCommand myCmd = new SqlCommand(query, myConn))
                    {
                        myReader = myCmd.ExecuteReader();
                        while (myReader.Read())
                        {
                            ProductDetails product = new ProductDetails();
                            product.id = Convert.ToInt32(myReader["id"]);
                            product.name = myReader["name"].ToString();
                            product.type = myReader["type"].ToString();
                            product.imagePath = myReader["imagePath"].ToString();
                            product.perHourCost = Convert.ToInt64(myReader["perHourCost"]);
                            products.Add(product);

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
            return products;
        }
        public string updateProduct(ProductDetails product) {
            string query = @"update dbo.ProductDetails set name=@name,type=@type,perHourCost=@perHourCost,imagePath=@imagePath where id=@id";
               
            string sqlDataSource = _configuration.GetConnectionString("BicycleAppCon");
            SqlDataReader myReader;
            try
            {
                using (SqlConnection myConn = new SqlConnection(sqlDataSource))
                {
                    myConn.Open();
                    using (SqlCommand myCmd = new SqlCommand(query, myConn))
                    {
                        myCmd.Parameters.AddWithValue("@id", product.id);
                        myCmd.Parameters.AddWithValue("@name", product.name);
                        myCmd.Parameters.AddWithValue("@type", product.type);
                        myCmd.Parameters.AddWithValue("@perHourCost", product.perHourCost);
                        myCmd.Parameters.AddWithValue("@imagePath", product.imagePath);
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
        public string deleteProduct(int id) {

            string query = @"delete from dbo.ProductDetails where id=@id";
           
            string sqlDataSource = _configuration.GetConnectionString("BicycleAppCon");
            SqlDataReader myReader;
            try
            {
                using (SqlConnection myConn = new SqlConnection(sqlDataSource))
                {
                    myConn.Open();
                    using (SqlCommand myCmd = new SqlCommand(query, myConn))
                    {
                        myCmd.Parameters.AddWithValue("@id", id);
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
            return "Deleted Successfully";
        }
    }
}
