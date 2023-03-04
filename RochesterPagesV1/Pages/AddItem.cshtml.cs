using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Data.SqlClient;
using static RochesterPagesV1.Pages.DirectoryModel;

namespace RochesterPagesV1.Pages
{    
    public class AddItemModel : PageModel
    {
        public Directories directory = new Directories();
        public String errorMessage = "";
        public String successMessage = "";

        public void OnGet()
        {
        }

        public void OnPostDirectorySubmit()
        {
            errorMessage = "";
            successMessage = "";
            directory.name = Request.Form["name"];
            directory.address = Request.Form["address"];
            directory.number = Request.Form["number"];
            errorMessage = validateData(directory);

            if (errorMessage.Length==0)
            {
                try
                {
                    string myDbConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=RochesterPages;Integrated Security=True";

                    using (SqlConnection connection = new SqlConnection(myDbConnectionString))
                    {
                        connection.Open();
                        String sqlQuery = "insertDirectory";
                        using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                        {
                            command.CommandType = System.Data.CommandType.StoredProcedure;
                            SqlParameter param;

                            param = command.Parameters.Add("@name", SqlDbType.VarChar, 50);
                            param.Value = directory.name;
                            param = command.Parameters.Add("@address", SqlDbType.VarChar, int.MaxValue);
                            param.Value = directory.address;
                            param = command.Parameters.Add("@number", SqlDbType.VarChar, 10);
                            param.Value = directory.number;

                            int rowsAffected = command.ExecuteNonQuery();
                            if (rowsAffected> 0)
                            {
                                successMessage = "Data saved successfully";
                                directory.name = "";
                                directory.number = "";
                                directory.address = "";
                            }
                            else
                            {
                                errorMessage = "Place already exists with that name & address";
                            }
                            connection.Close();
                        }
                    }
                }
                catch(Exception ex)
                {
                    errorMessage = ex.Message;
                    return;
                }
                
            }
        }

        private String validateData(Directories data)
        {
            String validationError = "";
            if(data.address.Length == 0 || data.name.Length == 0 || data.number.Length == 0)
            {
                validationError = "All fields need to be set.";
            }
            else if (data.name.Length > 50)
            {
                validationError = "Length of name be less than 50.";
            }
            else if (data.number.Length !=10)
            {
                validationError = "Number must be 10 digits.";
            }
            return validationError;
        }
    }
}
