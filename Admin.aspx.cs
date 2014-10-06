using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.OleDb;

public partial class Admin : System.Web.UI.Page
{
	localhost.OnlineLearning dbws = new localhost.OnlineLearning();
	static int SubjectID;
	static int ChapterID;

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!IsPostBack)
		{
			if (Page.User.Identity.IsAuthenticated)
			{
				DataSet ds = dbws.selectQuestions();
				// gvChoices.DataSource = ds;
				// gvChoices.DataBind();

				DataSet dsSubjects = dbws.selectAllSubjects();
				SubjectDropDownList.DataSource = dsSubjects;
				SubjectDropDownList.DataTextField = "SubjectName";
				SubjectDropDownList.DataValueField = "SubjectID";
				SubjectDropDownList.DataBind();

				DataSet dsQuestions = dbws.selectAllQuestions();
				allQuestionsDropDownList.DataSource = dsQuestions;
				allQuestionsDropDownList.DataTextField = "QuestionText";
				allQuestionsDropDownList.DataValueField = "QuestionID";
				allQuestionsDropDownList.DataBind();
			}
			else
			{
				Response.Redirect("LoginPage.aspx");
			}
		}
	}


	protected void InsertButton_Click(object sender, EventArgs eventArgs)
	{
		try
		{

            if (ChapterDropDownList.SelectedValue != "0")
            {
                ChapterID = Int32.Parse(ChapterDropDownList.SelectedValue);
            }
			string QuestionText = QuestionTextBox.Text;
            int numberQuestionByChapter = dbws.selectNumberQuestionByChapter(ChapterID);
            int QuestionID = Int32.Parse(dbws.addQuestion(numberQuestionByChapter+1, QuestionText));

            if (SubjectDropDownList.SelectedValue != "0")
            {
                SubjectID = Int32.Parse(SubjectDropDownList.SelectedValue);
            }            
			if (QuestionID != 0)
			{
				QuestionOutputLabel.Text = dbws.addSubjectQuestionData(QuestionID, SubjectID, ChapterID);
                allQuestionsDropDownList.DataBind();
			}
		}
		catch (Exception e)
		{
			QuestionOutputLabel.Text = "Question is not added!";
		}		
	}

	protected void AddAnswerButton_Click(object sender, EventArgs eventArgs)
	{		
		try
		{
			int questionId = Int32.Parse(allQuestionsDropDownList.SelectedValue);
			string choiceLetter = choiceLetterTextBox.Text;
			string choiceText = choiceTextBox.Text;
            int rightChoice = Int32.Parse(rightChoiceTextBox.Text);
			answerOutputLabel.Text = dbws.addAnswer(questionId, choiceLetter, choiceText, rightChoice);
		}
		catch(Exception e)
		{
			answerOutputLabel.Text = "You've done something wrong!";
		}
	}

	protected void SubjectDropDownList_SelectedIndexChanged(object sender, EventArgs e)
	{
		SubjectID = Int32.Parse(SubjectDropDownList.SelectedValue);

        if (SubjectID != 0)
        {
            //TODO: append the default value first in the list and selected
            DataSet dsChapters = dbws.selectAllOblastBySubject(Int32.Parse(SubjectDropDownList.SelectedValue));
            ChapterDropDownList.DataSource = dsChapters;
            ChapterDropDownList.DataTextField = "Name";
            ChapterDropDownList.DataValueField = "OblastID";
            ListItem defaultItem = new ListItem("Области", "0", true);
            ChapterDropDownList.Items.Insert(1, defaultItem);
        }
        else
        {
            ChapterDropDownList.Items.Clear();
            ChapterDropDownList.DataBind();
            ListItem defaultItem = new ListItem("Области", "0", true);
            ChapterDropDownList.Items.Insert(0,defaultItem);
            ChapterDropDownList.SelectedValue = "0";
        }
		ChapterDropDownList.DataBind();
	}

	protected void ChapterDropDownList_SelectedIndexChanged(object sender, EventArgs e)
	{
		ChapterID = Int32.Parse(ChapterDropDownList.SelectedValue);
	}
}
