using System.Data;
using System.Data.SqlClient;
using System.Reflection.Metadata;

namespace MVC_Task.Models
{
    public class DataLayer
    {
        public static string dbcon;
        public DataTable GetCourse()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conn = new SqlConnection(dbcon);

                conn.Open();
                SqlCommand dCmd = new SqlCommand("SP_GetCourse", conn);
                dCmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter(dCmd);
                da.Fill(dt);
                conn.Close();

                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetSectionList()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conn = new SqlConnection(dbcon);

                conn.Open();
                SqlCommand dCmd = new SqlCommand("SP_GetSection", conn);
                dCmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter(dCmd);
                da.Fill(dt);
                conn.Close();

                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public string InsertData(Section objSec)
        {
            SqlConnection con = null;

            string result = "";
            try
            {
                con = new SqlConnection(dbcon);
                SqlCommand cmd = new SqlCommand("SP_InsertUpdateSection", con);
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@CustomerID", 0);    
                cmd.Parameters.AddWithValue("@Name", objSec.Name);
                cmd.Parameters.AddWithValue("@Order", objSec.Order);
                cmd.Parameters.AddWithValue("@CourseId", objSec.CourseId);
                cmd.Parameters.AddWithValue("@Id", objSec.Id);
                cmd.Parameters.AddWithValue("@Msg", "");
                cmd.Parameters["@Msg"].Direction = ParameterDirection.InputOutput;
                cmd.Parameters["@Msg"].Size = 0X100;
                con.Open();
                result = cmd.ExecuteNonQuery().ToString();
                result =Convert.ToString(cmd.Parameters["@Msg"].Value);
                return result;
            }
            catch(Exception ex)
            {
                return result = "";
            }

        }
    }
}
