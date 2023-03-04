using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace RochesterPagesV1.Pages
{
    public class DirectoryModel : PageModel
    {
        public List<Directories> directories = new List<Directories>();
        public void OnGet()
        {
            try
            {
                string myDbConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=RochesterPages;Integrated Security=True";

                using(SqlConnection connection = new SqlConnection(myDbConnectionString))
                {
                    connection.Open();
                    String sqlQuery = "select Directory.D_ID, D_Name, D_Address, D_Number \r\nFrom Directory order by 2";
                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Directories directory = new Directories();
                                directory.id = reader.GetString(0);
                                directory.name = reader.GetString(1);
                                directory.address = reader.GetString(2);
                                directory.number = reader.GetString(3);
                                directories.Add(directory);
                            }
                        }
                    }
                    connection.Close();
                }

            }
            catch(Exception ex)
            {
                Console.WriteLine("A databas error occured!" + ex.Message);
            }
        }

        public class Directories
        {
            public String id;
            public String name;
            public String address;
            public String number;
        }
    }
}
