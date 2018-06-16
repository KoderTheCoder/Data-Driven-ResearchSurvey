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
    public class Answer
    {
        int o_id;
        int q_id;
        string text;
        public Answer(int oid, int qid, string text)
        {
            o_id = oid;
            q_id = qid;
            this.text = text;
        }
        public int getO_id()
        {
            return o_id;
        }
        public int getQ_id()
        {
            return q_id;
        }
        public string getText()
        {
            return text;
        }
    }
    public partial class Question : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(HttpContext.Current.Session["ip_address"] == null)
            {
                HttpContext.Current.Session["ip_address"] = HttpContext.Current.Request.UserHostAddress;
                HttpContext.Current.Session["date"] = DateTime.Now.Date.ToString();
            }
            int currentQuestion = 1;

            if (HttpContext.Current.Session["questionNumber"] == null)
            {
                HttpContext.Current.Session["questionNumber"] = 1;
            }
            else
            {
                currentQuestion = (int)HttpContext.Current.Session["questionNumber"];
            }

            //setup DB connection
            SqlConnection connection;
            SqlCommand command;
            string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;

            connection = new SqlConnection();
            connection.ConnectionString = connectionString;

            connection.Open();//open the sql connection using the connection string info
            
            List<Int32> followUpQuestions = (List<Int32>)HttpContext.Current.Session["followUps"];
            if (followUpQuestions == null || followUpQuestions.Count() == 0)
            {
                if(HttpContext.Current.Session["currentFollowUp"] != null)
                {
                    int sq = (int)HttpContext.Current.Session["currentFollowUp"];
                    command = new SqlCommand("SELECT * FROM Question, Type WHERE Question.type_ID = type.ID AND Question.ID = " + sq, connection);
                    HttpContext.Current.Session["currentFollowUp"] = null;
                }
                else
                {
                    command = new SqlCommand("SELECT * FROM Question, Type WHERE Question.type_ID = type.ID AND Question.order_position = " + currentQuestion, connection);

                    SkipButton.Visible = false;
                }
            }
            else
            {
                    command = new SqlCommand("SELECT * FROM Question, Type WHERE Question.type_ID = type.ID AND Question.ID = " + followUpQuestions[0], connection);
                    HttpContext.Current.Session["currentFollowUp"] = followUpQuestions[0];
                if (HttpContext.Current.Session["secondLoad"] != null)
                {
                    followUpQuestions.RemoveAt(0);
                    HttpContext.Current.Session["followUps"] = followUpQuestions;
                    HttpContext.Current.Session["secondLoad"] = null;
                }
            }
            

            SqlDataReader reader = command.ExecuteReader(); //execute above query
            if (!reader.HasRows)
            {
                Response.Redirect("Register.aspx");
            }
            while (reader.Read())
            {
                HttpContext.Current.Session["questionID"] = reader["ID"];
                string questionText = reader["text"].ToString();
                string questionType = reader["type_name"].ToString().ToLower(); //just incase, so we dont have to check for textBox vs TextBox vs textbox
                if (questionType.Equals("text"))
                {
                    //TODO load up textbox controls
                    TextBoxControl textControl = (TextBoxControl)LoadControl("~/TextBoxControl.ascx");
                    textControl.ID = "textBoxControl" + HttpContext.Current.Session["questionID"].ToString();
                    QuestionLabel.Text = questionText;

                    //add it to the ui
                    PlaceHolder1.Controls.Add(textControl);
                }
                else if (questionType.Equals("check"))
                {
                    QuestionLabel.Text = questionText;
                    //TODO load up checkbox controls
                    CheckBoxControl checkBoxControl = (CheckBoxControl)LoadControl("~/CheckBoxControl.ascx");
                    checkBoxControl.ID = "checkBoxControl" + HttpContext.Current.Session["questionID"].ToString();
                    QuestionLabel.Text = questionText;

                    //TODO load up checkbox options/choices to add to checkbox control
                    SqlCommand optionCommand = new SqlCommand("SELECT * FROM [DB_9AB8B7_B18DDA6042].[dbo].[Option] WHERE Q_ID = " + (int)HttpContext.Current.Session["questionID"], connection);

                    SqlDataReader optionReader = optionCommand.ExecuteReader();
                    //cycle through all options
                    while (optionReader.Read())
                    {
                        //                           text you see,                      value its worth
                        ListItem item = new ListItem(optionReader["value"].ToString(), optionReader["ID"].ToString());
                        checkBoxControl.CheckBoxList1.Items.Add(item); //add item to option list
                    }

                    //done, add it to placeholder
                    PlaceHolder1.Controls.Add(checkBoxControl);
                }
                else if (questionType.Equals("radio"))
                {
                    QuestionLabel.Text = questionText;
                    //TODO load up checkbox controls
                    RadioOptions radioControl = (RadioOptions)LoadControl("~/RadioOptions.ascx");
                    radioControl.ID = "radioControl" + HttpContext.Current.Session["questionID"].ToString();
                    QuestionLabel.Text = questionText;

                    //TODO load up checkbox options/choices to add to checkbox control
                    SqlCommand optionCommand = new SqlCommand("SELECT * FROM [DB_9AB8B7_B18DDA6042].[dbo].[Option] WHERE Q_ID = " + (int)HttpContext.Current.Session["questionID"], connection);
                    SqlDataReader optionReader = optionCommand.ExecuteReader();
                    //cycle through all options
                    while (optionReader.Read())
                    {
                        ListItem item = new ListItem(optionReader["value"].ToString(), optionReader["ID"].ToString());
                        radioControl.RadioControl1.Items.Add(item); //add item to option list
                    }

                    //done, add it to placeholder
                    PlaceHolder1.Controls.Add(radioControl);
                }
            }
            connection.Close();
        }

        protected void Skip_Click(object sender, EventArgs e)
        {
            HttpContext.Current.Session["questionNumber"] = (int)HttpContext.Current.Session["questionNumber"] + 1;
            Response.Redirect("Question.aspx");
        }

        protected void Next_Click(object sender, EventArgs e)
        {
            if(HttpContext.Current.Session["secondLoad"] == null)
            {
                HttpContext.Current.Session["secondLoad"] = true;
            }

            CheckBoxControl cb = (CheckBoxControl)PlaceHolder1.FindControl("checkBoxControl"+ HttpContext.Current.Session["questionID"].ToString());
            RadioOptions rb = (RadioOptions)PlaceHolder1.FindControl("radioControl" + HttpContext.Current.Session["questionID"].ToString());
            TextBoxControl tb = (TextBoxControl)PlaceHolder1.FindControl("textBoxControl" + HttpContext.Current.Session["questionID"].ToString());
            bool followUp = false;

            //DB Connection
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString; ;

            connection.Open();//open the sql connection using the connection string info
            if (cb != null)
            {

                foreach (ListItem item in cb.CheckBoxList1.Items)
                {
                    if (item.Selected)
                    {
                        //TODO store in session

                        //Check if selected items ID in the DB leads to followUp questions
                        SqlCommand optionCommand = new SqlCommand("SELECT * FROM [DB_9AB8B7_B18DDA6042].[dbo].[Option] WHERE ID = " + item.Value, connection);

                        SqlDataReader optionReader = optionCommand.ExecuteReader();

                        List<Int32> followUpQuestions = new List<int>();

                        while (optionReader.Read())
                        {
                            addAnswersToSession(item, connection);

                            //check for followup questions
                            if (optionReader["NQ"] != System.DBNull.Value)
                            {
                                if (HttpContext.Current.Session["followUps"] != null)
                                {
                                    followUpQuestions = (List<Int32>)HttpContext.Current.Session["followUps"];
                                    if (!hasDuplicates(followUpQuestions, (int)optionReader["NQ"])) //do not add follow up question to the list if it it already in there
                                    {
                                        followUpQuestions.Add((int)optionReader["NQ"]);
                                    }
                                    HttpContext.Current.Session["followUps"] = followUpQuestions;
                                    followUp = true;
                                }
                                else
                                {
                                    followUp = true;
                                    if (!hasDuplicates(followUpQuestions, (int)optionReader["NQ"]))
                                    {
                                        followUpQuestions.Add((int)optionReader["NQ"]);
                                    }
                                    HttpContext.Current.Session["followUps"] = followUpQuestions;
                                }
                                HttpContext.Current.Session["currentFollowUp"] = null;
                            }
                        }



                        //if so, add to Session["followUps"]

                        //selectedAnswerBulletedList.Items.Add(item);
                    }
                }
            }
            else if (rb != null)
            {
                foreach (ListItem item in rb.RadioControl1.Items)
                {
                    if (item.Selected)
                    {
                        addAnswersToSession(item, connection);
                    }
                }
            }
            else if(tb != null)
            {
                addAnswersToSession(tb.AnswerTextBox.Text, connection);
            }

            if (!followUp) //if next question is not a follow up, then go to the next question
            {
                HttpContext.Current.Session["questionNumber"] = (int)HttpContext.Current.Session["questionNumber"] + 1;
                connection.Close();
                Response.Redirect("Question.aspx");
            }
            //string value = radioControl1.SelectedItem.Value.ToString();
            Response.Redirect("Question.aspx");
            connection.Close();
        }

        void addAnswersToSession(ListItem item, SqlConnection connection)
        {
            SqlCommand optionCommand = new SqlCommand("SELECT * FROM [DB_9AB8B7_B18DDA6042].[dbo].[Option] WHERE ID = " + item.Value, connection);

            SqlDataReader optionReader = optionCommand.ExecuteReader();

            List<Int32> followUpQuestions = new List<int>();

            while (optionReader.Read())
            {
                //this stores the answer in to a list that is stored in our session
                Answer answer = new Answer(Int32.Parse(item.Value), (int)optionReader["Q_ID"], item.Text);
                if (HttpContext.Current.Session["answers"] != null)
                {
                    List<Answer> answers = (List<Answer>)HttpContext.Current.Session["answers"];
                    answers.Add(answer);
                    HttpContext.Current.Session["answers"] = answers;
                }
                else
                {
                    List<Answer> answers = new List<Answer>();
                    answers.Add(answer);
                    HttpContext.Current.Session["answers"] = answers;
                }
            }
        }
        void addAnswersToSession(String text, SqlConnection connection)
        {
            //this stores the answer in to a list that is stored in our session
            Answer answer = new Answer(0, (int)HttpContext.Current.Session["questionId"], text);
            if (HttpContext.Current.Session["answers"] != null)
            {
                List<Answer> answers = (List<Answer>)HttpContext.Current.Session["answers"];
                answers.Add(answer);
                HttpContext.Current.Session["answers"] = answers;
            }
            else
            {
                List<Answer> answers = new List<Answer>();
                answers.Add(answer);
                HttpContext.Current.Session["answers"] = answers;
            }
        }

        bool hasDuplicates(List<Int32> array, int value)
        {
            foreach (int item in array)
            {
                if(item == value)
                {
                    return true;
                }
            }
            return false;
        }
    }
}