using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Collections.Generic;

namespace MyQuiz
{
    public partial class QuizPage : System.Web.UI.Page
    {
        localhost.OnlineLearning dbws = new localhost.OnlineLearning();
        protected System.Web.UI.WebControls.Button SubmitButton;

        static int SubjectID;
        static int OblastID;
        static int StrategyID;
        static int StrategyValue;
        static string OblastName;

        static int numOfQuestions;
        static int numOfVisitedQuestions;
        static int numOfAnsweredQuestions;
        static int trueAnswers;
        static int answer = 0;
        static int visit = 0;
        static int QuestionID;

        static DataSet dsQuestions;
        static DataTable dtQuestions;
        static DataSet dsChoices;
        static DataTable dtChoices;


        protected void Page_Load(object sender, System.EventArgs e)
        {

            if (!IsPostBack)
            {
                startButton.Visible = true;
                finishButton.Visible = false;
                nextButton.Visible = false;

                SubjectID = Int32.Parse(Session["SubjectID"].ToString());
                OblastID = Int32.Parse(Session["OblastID"].ToString());
                DataTable oblastTable = dbws.selectAllOblastBySubject(SubjectID).Tables[0];
                OblastName = oblastTable.Rows[0]["Name"].ToString();

                DataTable dtStrategy = dbws.selectStrategyBySubjectAndOblast(SubjectID, OblastID).Tables[0];
                StrategyID = Int32.Parse(dtStrategy.Rows[0]["StrategyID"].ToString());
                StrategyValue = Int32.Parse(dtStrategy.Rows[0]["StrategyValue"].ToString());
                
                StrategyID = 1;
                StrategyValue = 3;                
            }
            else
            {
                startButton.Visible = false;
                finishButton.Visible = true;
                nextButton.Visible = true;            
            }
        }

        protected void choicesRadioButtonList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selValue = choicesRadioButtonList.SelectedValue ;

            if (choicesRadioButtonList.SelectedValue != "")
            {
                //check if is the right choice clicked
                int answer = checkRightChoice(selValue);
                if (answer == 1)
                {
                    trueAnswers++;
                    resultsLabel.Style["color"] = "lightgreen";
                    resultsLabel.Text = "The answer is correct!" + "<br />";
                    dbws.setAnsweredTrueQuestion(QuestionID,1);
                    //check strategy
                    checkStrategy();

                }
                else
                {
                    resultsLabel.Style["color"] = "red";
                    resultsLabel.Text = "The answer is not correct!" + "<br />";
                    dbws.setAnsweredTrueQuestion(QuestionID, 0);
                }

            }
        }
             
        protected int checkRightChoice(string selValue)
        {
            DataSet dsChoices = dbws.checkRightChoice(selValue);
            DataTable dtChoices = dsChoices.Tables[0];
            answer = 0;

            if(dtChoices.Rows.Count > 0)
                if(dtChoices.Rows[0] != null)
                    answer = Int32.Parse(dtChoices.Rows[0]["RightChoice"].ToString());
            
            return answer;
        }

        protected void startButton_click(object sender, EventArgs e)
        {
            trueAnswers = 0;
            QuestionID = 0;
            numOfQuestions = 0;

            //remova data from VisitQuestion database table
            dbws.removeDataFromVisitQuestion();

            nextButton_click(sender, e);
        }

        protected void nextButton_click(object sender, EventArgs e)
        {      
            // create a row for the question and read it from the database
            TableRow tr = new TableRow();
            tr.ID = "questionRowID";
            Table1.Rows.Add(tr);
            TableCell aCell = new TableCell();

            dsQuestions = dbws.selectOneQuestionBySubjectAndField(SubjectID, OblastID);
            dtQuestions = dsQuestions.Tables[0];
            string questionID = dtQuestions.Rows[0]["QuestionID"].ToString();
            QuestionID = Int32.Parse(questionID);

            int num = 0;
            if (checkedIfVisitedQuestion(QuestionID) == 0)
            {
                num = numOfQuestions;
            }
            else
            {
                checkedIfAnsweredTrueQuestion(QuestionID, sender, e);
                num = dbws.selectVisitedNumberByQuestionID(QuestionID);
            }

            // get the text for the question and stick it in the cell
            aCell.Text = "<b>" + num + ".</b> " + dtQuestions.Rows[0]["QuestionText"].ToString();
            tr.Cells.Add(aCell);

            //get the choices
            dsChoices = dbws.selectChoicesByQuestionID(QuestionID);
            dtChoices = dsChoices.Tables[0];
            List<String> choicesList = new List<string>();
            for (int j = 0; j < dtChoices.Rows.Count; j++)
            {
                choicesList.Add(dtChoices.Rows[j]["ChoiceText"].ToString());      
            }

            //choicesRadioButtonList.DataSource = dsChoices;
            choicesRadioButtonList.DataSource = choicesList;
            choicesRadioButtonList.DataBind();
         
        }

        private int checkedIfVisitedQuestion(int QuestionID)
        {
            visit = dbws.selektirajQuestionID(QuestionID);

            if (visit == 0)
            {
                dbws.addVisitedQuestion(QuestionID, ++numOfQuestions);
            }
            else
            { 
                
            }
            return visit;
        }

        private void checkedIfAnsweredTrueQuestion(int QuestionID, object sender, EventArgs e)
        {
            int answeredTrue = dbws.selectAnsweredTrueByQuestionID(QuestionID);
            if (answeredTrue == 1)
            {
                nextButton_click(sender, e);
            }
        }            

        protected void finish_click(object sender, EventArgs e)
        {
            dbws.removeDataFromVisitQuestion();
            Response.Redirect("Results.aspx");
        }

        private void checkStrategy()
        {
            //posledovatelno tocni odgovori
            if (StrategyID == 1)
            {
                int numLastTrueAnswers = dbws.selectNumberLastAnsweredQuestions(StrategyValue);
                if (numLastTrueAnswers >= StrategyValue){
                    resultsLabel.Text += "Одговоривте точно на последните " + StrategyValue + " прашања. Областа " + OblastName + " е успешно измината!" + "<br />";
                    Response.AddHeader("REFRESH", "5;URL=ViewSubjects.aspx");
                }
            }

            //vkupno tocni odgovori
            if (StrategyID == 2)
            {
                if (trueAnswers >= StrategyValue)
                {
                    resultsLabel.Text += "Одговоривте точно на вкупно " + StrategyValue + " прашања. Областа " + OblastName + " е успешно измината!" + "<br />";
                    Response.AddHeader("REFRESH", "5;URL=ViewSubjects.aspx");
                    //Response.Redirect("ViewSubjects.aspx");
                }
            }
        }
}
}
