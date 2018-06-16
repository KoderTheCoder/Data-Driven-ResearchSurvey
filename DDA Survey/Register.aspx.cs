using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//added
using System.Data.SqlClient;
using System.Configuration;

namespace DDA_Survey
{
    public partial class Register : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session["surveyFinished"] != null)
            {
                Response.Redirect("FinishedSurvey.aspx");
            }
        }

        protected void Skip_Click(object sender, EventArgs e)
        {
            String ipAddress = HttpContext.Current.Session["ip_address"].ToString();
            String date = HttpContext.Current.Session["date"].ToString();
            int R_ID = 0;

            SqlConnection connection;
            string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
            connection = new SqlConnection();
            connection.ConnectionString = connectionString;

            SqlCommand command = new SqlCommand("INSERT INTO Respondant(first_name, ip_address, date) VALUES ('anonymous','" + ipAddress + "', '" + date + "' )", connection);

            connection.Open();
            command.ExecuteNonQuery();

            command = new SqlCommand("SELECT ID FROM Respondant WHERE ip_address = '"+ipAddress+"' AND date='"+date+ "' ORDER BY ID DESC", connection);
            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                R_ID = (int)reader["ID"];
            }
            
            List<Answer> answers = (List<Answer>)HttpContext.Current.Session["answers"];

            foreach (Answer ans in answers)
            {
                command = new SqlCommand("INSERT INTO Answers(value, Q_ID, O_ID, R_ID) VALUES ('" + ans.getText() + "', " + ans.getQ_id() + ", " + ans.getO_id() +", "+ R_ID +" )", connection);
                command.ExecuteNonQuery();
            }

            Response.Redirect("FinishedSurvey.aspx");
            connection.Close();

        }

        protected void Next_Click(object sender, EventArgs e)
        {
            bool error = false;
            SqlConnection connection;
            string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;

            connection = new SqlConnection();
            connection.ConnectionString = connectionString;

            

            String firstName = FirstNameTextBox.Text;
            String lastName = LastNameTextBox.Text;
            String phone = PhoneNumberTextBox.Text;
            String dob = DOBTextBox.Text;

            if(firstName == String.Empty || lastName == String.Empty || phone == String.Empty || dob == String.Empty)
            {
                error = true;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('You need to fill in all fields to register')", true);
            }

            String ipAddress = System.Web.HttpContext.Current.Request.UserHostAddress;
            String date = DateTime.Now.Date.ToString();
            


            if (!error)
            {
                SqlCommand command = new SqlCommand("INSERT INTO Respondant(first_name, last_name, phone, dob, ip_address, date) VALUES ('" + firstName + "', '" + lastName + "', '" + phone + "','" + dob + "', '" + ipAddress + "', '" + date + "' )", connection);
                try
                {
                    //command = new SqlCommand("INSERT INTO Respondant(first_name, last_name, phone, dob, ip_address, date) VALUES ('Koder', 'Musty', '0402546215','20/10/1542', '"+ipAddress+"', '"+date+"' )", connection);
                    //command = new SqlCommand("DROP * FROM Respondants", connection);
                    connection.Open();//open the sql connection using the connection string info
                    command.ExecuteNonQuery();
                    Response.Redirect("FinishedSurvey.aspx");
                    connection.Close();
                }
                catch
                {
                    connection.Close();
                }
            }
            
        }
    }
}