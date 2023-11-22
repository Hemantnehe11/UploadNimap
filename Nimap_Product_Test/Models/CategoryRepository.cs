using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Reflection;

namespace Nimap_Product_Test.Models
{
    public class CategoryRepository
    {
      
          //  string constr = ConfigurationManager.ConnectionStrings["NimapConnection"].ToString();
          
        //public DataTable GetCategory()
        //{
        //    DataTable dt = new DataTable();
        //    string strConString = constr;
        //    using (SqlConnection con = new SqlConnection(strConString))
        //    {
        //        con.Open();
        //        SqlCommand cmd = new SqlCommand("Select * from tblStudent", con);
        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        da.Fill(dt);
        //    }
        //    return dt;
        //}

        public List<CategoryDM> GetCategoryList()
        {
            SqlConnection con = null;
            DataSet ds = null;
            List<CategoryDM> Categorylist = null;
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["NimapConnection"].ToString());
                SqlCommand cmd = new SqlCommand("getCategory", con);
                cmd.CommandType = CommandType.StoredProcedure;
               
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                ds = new DataSet();
                da.Fill(ds);
                Categorylist = new List<CategoryDM>();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    CategoryDM cat = new CategoryDM();
                    cat.CategoryId = Convert.ToInt32(ds.Tables[0].Rows[i]["CategoryId"].ToString());
                    cat.CategoryName = ds.Tables[0].Rows[i]["CategoryName"].ToString();


                    Categorylist.Add(cat);
                }
                return Categorylist;
            }
            catch
            {
                return Categorylist;
            }
            finally
            {
                con.Close();
            }
        }



        string cs = ConfigurationManager.ConnectionStrings["NimapConnection"].ConnectionString;

        public int InsertCategory(CategoryDM cat)
        {
            int i;
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand com = new SqlCommand("InsertUpdateAdGetCategory", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@CategoryId", 0);
                com.Parameters.AddWithValue("@CategoryName", cat.CategoryName);
                com.Parameters.AddWithValue("@Action", "Insert");
                i = com.ExecuteNonQuery();
            }
            return i;
        }

        public int UpdateCategory(CategoryDM cat)
        {
            int i;
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand com = new SqlCommand("InsertUpdateAdGetCategory", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@CategoryId", cat.CategoryId);
                com.Parameters.AddWithValue("@CategoryName", cat.CategoryName);
                com.Parameters.AddWithValue("@Action", "Update");
                i = com.ExecuteNonQuery();
            }
            return i;
        }

        public int DeleteCategory(int id)
        {
            int i;
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand com = new SqlCommand("InsertUpdateAdGetCategory", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@CategoryId", id);
                com.Parameters.AddWithValue("@CategoryName", "");
                com.Parameters.AddWithValue("@Action", "Delete");
                i = com.ExecuteNonQuery();
            }
            return i;
        }


        public CategoryDM BindCategoryByID(int id)
        {
            SqlConnection con = null;
            DataSet ds = null;
            CategoryDM cat = new CategoryDM();
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["NimapConnection"].ToString());
                SqlCommand cmd = new SqlCommand("InsertUpdateAdGetCategory", con);
                cmd.Parameters.AddWithValue("@CategoryId", id);
                cmd.Parameters.AddWithValue("@CategoryName", "");
                cmd.Parameters.AddWithValue("@Action", "Get");
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                ds = new DataSet();
                da.Fill(ds);
             
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                   
                    cat.CategoryId = Convert.ToInt32(ds.Tables[0].Rows[i]["CategoryId"].ToString());
                    cat.CategoryName = ds.Tables[0].Rows[i]["CategoryName"].ToString();
                   
                }
                return cat;
            }
            catch
            {
                return cat;
            }
            finally
            {
                con.Close();
            }
        }
    }
}