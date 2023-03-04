using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using static RochesterPagesV1.Pages.DirectoryModel;
using System.Data.Common;

namespace RochesterPagesV1.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public List<Directories> directories = new List<Directories>();

        public String errorMessage="";
        public String lookup = "";

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }

        public void OnPost()
        {
            errorMessage = "";
            directories = new List<Directories>();
            lookup = Request.Form["lookup"];
            if (lookup.Length>0)
            {
                try
                {
                    string myDbConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=RochesterPages;Integrated Security=True";

                    using (SqlConnection connection = new SqlConnection(myDbConnectionString))
                    {
                        connection.Open();
                        String sqlQuery = "SearchDirectory";
                        using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                        {
                            command.CommandType = System.Data.CommandType.StoredProcedure;
                            SqlParameter param;

                            param = command.Parameters.Add("@lookupName", SqlDbType.VarChar, 50);
                            param.Value = lookup;

                            SqlDataReader reader = command.ExecuteReader();

                            while (reader.Read())
                            {
                                Directories directory = new Directories();
                                directory.id = reader.GetString(0);
                                directory.name = reader.GetString(1);
                                directory.address = reader.GetString(2);
                                directory.number = reader.GetString(3);
                                directories.Add(directory);
                            }
                            connection.Close();
                            if(directories.Count==0)
                            {
                                errorMessage = "No data found with that keyword.";
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return;
                }

            }
        }
    }
}