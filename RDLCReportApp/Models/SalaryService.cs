using System.Data;
using System.Data.SqlClient;

namespace RDLCReportApp.Models
{
    public class SalaryService
    {
        //string constr = @"Data Source=DESKTOP-T64S8Q3;Initial Catalog=Movies;User ID=MooSalah;Password=12346";
        //string constr = @"Data Source=DESKTOP-T64S8Q3;Integrated Security=true;";
        string constr = @"Data Source=DESKTOP-T64S8Q3;Database=Movies;Trusted_Connection=True;";

        public DataTable GetActorInfo()
        {
            var dt = new DataTable();
            using (SqlConnection con = new(constr))
            {
                var cmd = new SqlCommand("SPGetActorInfo", con)
                {
                    CommandType = CommandType.StoredProcedure
                };

                con.Open();

                var dataAdapter = new SqlDataAdapter(cmd);
                dataAdapter.Fill(dt);

                con.Close();

            }
            return dt;
        }


    }
}
