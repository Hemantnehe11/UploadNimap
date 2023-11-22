using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace Nimap_Product_Test.Models
{
    public class ProductRepository
    {
        public List<ProductDM> GetProductList(int num)
        {
            SqlConnection con = null;
            DataSet ds = null;
            List<ProductDM> Productlist = null;
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["NimapConnection"].ToString());
                SqlCommand cmd = new SqlCommand("PaginationQuery", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PageSize", 10 );
                cmd.Parameters.AddWithValue("@PageNum", num);
                cmd.Parameters.AddWithValue("@TotalRows", null);

                con.Open();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                ds = new DataSet();
                da.Fill(ds);
                Productlist = new List<ProductDM>();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    ProductDM pro = new ProductDM();
                    pro.CategoryId = Convert.ToInt32(ds.Tables[0].Rows[i]["CategoryId"].ToString());
                    pro.ProductId = Convert.ToInt32(ds.Tables[0].Rows[i]["ProductId"].ToString());
                    pro.ProductName = ds.Tables[0].Rows[i]["ProductName"].ToString();
                    pro.CategoryName = ds.Tables[0].Rows[i]["CategoryName"].ToString();
                    pro.TotalRows = Convert.ToInt32(ds.Tables[0].Rows[i]["TotalRows"].ToString());
                    pro.PageNumber = Convert.ToInt32(ds.Tables[0].Rows[i]["PageNumber"].ToString());


                    Productlist.Add(pro);
                }
                return Productlist;
            }
            catch
            {
                return Productlist;
            }
            finally
            {
                con.Close();
            }
        }

        string cs = ConfigurationManager.ConnectionStrings["NimapConnection"].ConnectionString;
        public int InsertProduct(ProductDM pro)
        {
            int i;
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand com = new SqlCommand("getInserUpdateDeleteProduct", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@CategoryId", pro.CategoryId);
                com.Parameters.AddWithValue("@ProductId", 0);
                com.Parameters.AddWithValue("@ProductName", pro.ProductName);
                com.Parameters.AddWithValue("@Action", "Insert");
                i = com.ExecuteNonQuery();
            }
            return i;
        }

        public int UpdateProduct(ProductDM pro)
        {
            int i;
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand com = new SqlCommand("getInserUpdateDeleteProduct", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@ProductId", pro.ProductId);
                com.Parameters.AddWithValue("@CategoryId", pro.CategoryId);
                com.Parameters.AddWithValue("@ProductName", pro.ProductName);
                com.Parameters.AddWithValue("@Action", "Update");
                i = com.ExecuteNonQuery();
            }
            return i;
        }

        public int DeleteProduct(int id)
        {
            int i;
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand com = new SqlCommand("getInserUpdateDeleteProduct", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@ProductId", id);
                com.Parameters.AddWithValue("@CategoryId", 0);
                com.Parameters.AddWithValue("@ProductName", "");
                com.Parameters.AddWithValue("@Action", "Delete");
                i = com.ExecuteNonQuery();
            }
            return i;
        }


        public ProductDM BindProductByID(int id)
        {
            SqlConnection con = null;
            DataSet ds = null;
            ProductDM pro = new ProductDM();
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["NimapConnection"].ToString());
                SqlCommand cmd = new SqlCommand("getInserUpdateDeleteProduct", con);
                cmd.Parameters.AddWithValue("@CategoryId", 0);
                cmd.Parameters.AddWithValue("@ProductId", id);
                cmd.Parameters.AddWithValue("@ProductName", "");
                cmd.Parameters.AddWithValue("@Action", "GetById");
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                ds = new DataSet();
                da.Fill(ds);

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {

                    pro.CategoryId = Convert.ToInt32(ds.Tables[0].Rows[i]["CategoryId"].ToString());
                    pro.ProductId = Convert.ToInt32(ds.Tables[0].Rows[i]["ProductId"].ToString());
                    pro.ProductName = ds.Tables[0].Rows[i]["ProductName"].ToString();

                }
                return pro;
            }
            catch
            {
                return pro;
            }
            finally
            {
                con.Close();
            }
        }


        public int GetRowCount(int page)
        {
            int count = 0;
            SqlConnection con = null;
            DataSet ds = null;
            ProductDM pro = new ProductDM();
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["NimapConnection"].ToString());
                SqlCommand cmd = new SqlCommand("PaginationQuery", con);
                cmd.Parameters.AddWithValue("@PageSize", 10);
                cmd.Parameters.AddWithValue("@PageNum", page);
                cmd.Parameters.AddWithValue("@TotalRows", null);
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                ds = new DataSet();
                da.Fill(ds);

                for (int i = 0; i < 1; i++)
                {

                    count = Convert.ToInt32(ds.Tables[0].Rows[i]["TotalRows"].ToString());
                 
                }
                return count;
            }
            catch
            {
                return count;
            }
            finally
            {
                con.Close();
            }
        }


    }
}