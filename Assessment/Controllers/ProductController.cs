using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Assessment;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.SqlServer.Server;
using System.Data;


namespace Assessment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Product> GetProductDetails()
        {
            SqlConnection con = new SqlConnection("Server =192.168.4.41; Database = DBTEST;TrustServerCertificate = true; user id=sa; password=sql#2017  ");
            SqlDataAdapter da = new SqlDataAdapter("Select * from Product_RutujaJahdav", con);
            DataSet ds = new DataSet();
            con.Open();
            da.Fill(ds);
            int ctr = 0;
            List<Product> lst = new List<Product>();
            while (ctr<ds.Tables[0].Rows.Count)
               {
                Product obj = new Product();
                obj.ProductId = System.Convert.ToInt32(ds.Tables[0].Rows[ctr]["ProductId"]);
                obj.Name = ds.Tables[0].Rows[ctr]["Name"].ToString();
                obj.description = ds.Tables[0].Rows[ctr]["description"].ToString();
                obj.Price = System.Convert.ToInt32(ds.Tables[0].Rows[ctr]["Price"]);
                obj.category = ds.Tables[0].Rows[ctr]["category"].ToString();
                lst.Add(obj);
                ctr++;
               }
            return lst;
        }


        [HttpGet("GetById")]
        public IActionResult GetById(int ProductId)
        {
            SqlConnection con = new SqlConnection("Server =192.168.4.41; Database = DBTEST;TrustServerCertificate = true; user id=sa; password=sql#2017  ");
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand();
            da.SelectCommand.Connection = con;
            da.SelectCommand.CommandText = "Select ProductId from Product_RutujaJahdav where ProductId=@ProductId"; 
            da.SelectCommand.Parameters.Add(new SqlParameter("ID", SqlDbType.Int));
            da.SelectCommand.Parameters["ID"].Value = ProductId;
            da.SelectCommand.Connection.Open();
            SqlDataReader dr = da.SelectCommand.ExecuteReader();
            Product obj = null;
            if (dr.Read())
            {
                obj = new Product();
                obj.ProductId = System.Convert.ToInt32(dr[0]);
                obj.Name = dr[1].ToString();
                obj.description = dr[2].ToString();
                obj.Price = System.Convert.ToInt32(dr[3]);
                obj.category = dr[4].ToString();
            }
            return Ok(obj);

        }
        
        [HttpPost]

        public IActionResult CreateRecord(Product obj)
        {
            SqlConnection con = new SqlConnection("Server =192.168.4.41; Database = DBTEST;TrustServerCertificate = true; user id=sa; password=sql#2017  ");
            SqlDataAdapter da=new SqlDataAdapter();
            da.InsertCommand= new SqlCommand();
            da.InsertCommand.Connection = con;
            da.InsertCommand.CommandText = "insert into Product_RutujaJahdav  values (01, 'sceancare', 'its related to sceencare', 250, 'sceancare')";
            da.InsertCommand.Parameters.Add(new SqlParameter("ProductId", SqlDbType.Int));
            da.InsertCommand.Parameters.Add(new SqlParameter("Name", SqlDbType.VarChar));
            da.InsertCommand.Parameters.Add(new SqlParameter("description", SqlDbType.VarChar));
            da.InsertCommand.Parameters.Add(new SqlParameter("Price", SqlDbType.Int));
            da.InsertCommand.Parameters.Add(new SqlParameter("category", SqlDbType.VarChar));
            da.InsertCommand.Parameters["ProductId"].Value = obj.ProductId;
            da.InsertCommand.Parameters["Name"].Value = obj.Name;
            da.InsertCommand.Parameters["description"].Value = obj.description;
            da.InsertCommand.Parameters["Price"].Value = obj.Price;
            da.InsertCommand.Parameters["category"].Value = obj.category;
            da.InsertCommand.Connection.Open();
            int rowsAffected = da.InsertCommand.ExecuteNonQuery();
            da.InsertCommand.Connection.Close();

            if (rowsAffected > 0)
                return Ok(rowsAffected.ToString() + "record inserted");
            else
                return BadRequest("record not found");

        }
      
       [HttpDelete("DeleteRecord")]
        public IActionResult DeleteRecord(int Pid)
        {
            SqlConnection con = new SqlConnection("Server =192.168.4.41; Database = DBTEST;TrustServerCertificate = true; user id=sa; password=sql#2017  ");
            SqlDataAdapter da = new SqlDataAdapter();
            da.DeleteCommand = new SqlCommand();
            da.DeleteCommand.Connection = con;
            da.DeleteCommand.CommandText = "delete from Product_RutujaJahdav where ProductId=@Pid";
            da.DeleteCommand.Parameters.Add(new SqlParameter("ID", SqlDbType.Int));
            da.DeleteCommand.Parameters["ID"].Value = Pid;
            da.DeleteCommand.Connection.Open();
            int rowsAffected = da.InsertCommand.ExecuteNonQuery();
            da.DeleteCommand.Connection.Close();

            if (rowsAffected > 0)
                return Ok(rowsAffected.ToString() + "record deleted");
            else
                return BadRequest("record not found");

        }

        [HttpDelete("UpadateRecoed")]
        public IActionResult UpdateRecord(int Pid)
        {
            SqlConnection con = new SqlConnection("Server =192.168.4.41; Database = DBTEST;TrustServerCertificate = true; user id=sa; password=sql#2017  ");
            SqlDataAdapter da = new SqlDataAdapter();
            da.UpdateCommand = new SqlCommand();
            da.UpdateCommand.Connection = con;
            da.UpdateCommand.CommandText = "update table Product_RutujaJahdav where ProductId=@Pid values (, 'sceancare', 'its related to sceencare', 250, 'sceancare')";
            da.UpdateCommand.Parameters.Add(new SqlParameter("ID", SqlDbType.Int));
            da.UpdateCommand.Parameters["ID"].Value = Pid;
            da.UpdateCommand.Connection.Open();
            int rowsAffected = da.InsertCommand.ExecuteNonQuery();
            da.UpdateCommand.Connection.Close();

            if (rowsAffected > 0)
                return Ok(rowsAffected.ToString() + "Updated Record");
            else
                return BadRequest("record not found");

        }

    }

}






