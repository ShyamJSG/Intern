using System.Data;
using System.Data.SqlClient;

namespace WebAPIApplication.Models
{
    public class DAL //Data Access Layer
    {
        public Response GetAllEmployees(SqlConnection connection)
        {
            Response response = new Response();
            SqlDataAdapter Adapter = new SqlDataAdapter("Select * from Crud",connection);
            DataTable dt = new DataTable();
            Adapter.Fill(dt);
            List<Crud> listcruds = new List<Crud>();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Crud cruds = new Crud();
                    cruds.Id = Convert.ToInt32(dt.Rows[i]["Id"]);
                    cruds.Name = Convert.ToString(dt.Rows[i]["Name"]);
                    cruds.Email = Convert.ToString(dt.Rows[i]["Email"]);
                    cruds.IsActive = Convert.ToInt32(dt.Rows[i]["IsActive"]);
                    listcruds.Add(cruds);
                }
            }
            if (listcruds.Count > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Data Found ";
                response.cruds = listcruds;
            }
            else
            {
                response.StatusCode=100;
                response.StatusMessage = "No Data Found";
                response.cruds = listcruds;
            }
            return response;
        }
        public Response GetEmployeeByID(SqlConnection connection,int id)
        {
            Response response = new Response();
            SqlDataAdapter Adapter = new SqlDataAdapter("Select * from Crud where Id = '"+id+"' and IsActive = 1", connection);
            DataTable dt = new DataTable();
            Adapter.Fill(dt);
            Crud listcruds = new Crud();
            if (dt.Rows.Count > 0)
            {
                Crud cruds = new Crud();
                cruds.Id = Convert.ToInt32(dt.Rows[0]["Id"]);
                cruds.Name = Convert.ToString(dt.Rows[0]["Name"]);
                cruds.Email = Convert.ToString(dt.Rows[0]["Email"]);
                cruds.IsActive = Convert.ToInt32(dt.Rows[0]["IsActive"]);
                response.StatusCode = 200;
                response.StatusMessage = "Data Found ";
                response.crud = cruds;
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "No Data Found";
            }
            return response;
        }
        public Response AddEmployee(SqlConnection connection, Crud crud)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("Insert into Crud (Name,Email,IsActive,CreatedOn) values ('"+crud.Name+"','"+crud.Email+"','"+crud.IsActive+"',getdate())", connection);
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();
            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Data Inserted";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "No Data Inserted";
            }
            return response;
        }
        public Response UpdateEmployee(SqlConnection connection, Crud crud)
        {
            Response response = new Response();
            List<string> updates = new List<string>();
            if (!string.IsNullOrEmpty(crud.Name))
            {
                updates.Add("Name = @Name");
            }
            if (!string.IsNullOrEmpty(crud.Email))
            {
                updates.Add("Email = @Email");
            }
            if (crud.IsActive.HasValue)
            {
                updates.Add("IsActive = @IsActive");
            }
            string updateClause = string.Join(", ", updates);
            string query = $"UPDATE Crud SET {updateClause} WHERE Id = @Id";

            SqlCommand cmd = new SqlCommand(query, connection);
            if (!string.IsNullOrEmpty(crud.Name))
            {
                cmd.Parameters.AddWithValue("@Name", crud.Name);
            }
            if (!string.IsNullOrEmpty(crud.Email))
            {
                cmd.Parameters.AddWithValue("@Email", crud.Email);
            }
            if (crud.IsActive.HasValue)
            {
                cmd.Parameters.AddWithValue("@IsActive", crud.IsActive);
            }
            cmd.Parameters.AddWithValue("@Id", crud.Id);

            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();

            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Data Updated";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Update Failed";
            }
            return response;

            //Response response = new Response();
            //SqlCommand cmd = new SqlCommand("Update Crud set Name ='"+crud.Name+"',Email ='" + crud.Email + "',IsActive = '"+crud.IsActive+"' where Id = '"+crud.Id+"'", connection);
            //connection.Open();
            //int i = cmd.ExecuteNonQuery();
            //connection.Close();
            //if (i > 0)
            //{
            //    response.StatusCode = 200;
            //    response.StatusMessage = "Data Updated";
            //}
            //else
            //{
            //    response.StatusCode = 100;
            //    response.StatusMessage = "Update Failed";
            //}
            //return response;
        }
        public Response DeleteEmployee(SqlConnection connection, int id)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("Update Crud set IsActive = 0 where Id = '" + id + "'", connection);
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();
            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Data Deleted";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Data Not Deleted";
            }
            return response;
        }
    }
}
