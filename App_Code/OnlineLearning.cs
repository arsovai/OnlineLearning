using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for OnlineLearning
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class OnlineLearning : System.Web.Services.WebService
{

	[WebMethod]
	public string addQuestion(int questionNumber, string questionText)
	{
		try
		{
			string cnnstring = ConfigurationManager.ConnectionStrings["ApplicationServices"].ToString();
			SqlConnection con = new SqlConnection(cnnstring); 
			con.Open();
			string sql = "Insert into Questions(QuestionNumber,QuestionText) VALUES (@QuestionNumber, @QuestionText); SELECT SCOPE_IDENTITY() as ID";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();            
			cmd.Parameters.AddWithValue("@QuestionNumber", questionNumber);
			cmd.Parameters.AddWithValue("@QuestionText", questionText);
			//cmd.ExecuteNonQuery();
            da.Fill(ds);
			
			string id = ds.Tables[0].Rows[0].ItemArray[0].ToString();
			con.Close();

			return id;
			//return "Question is successfully added!";
		}
		catch (Exception e1)
		{
			//return 0;
			return "Question is not added! " + e1.Message;
		}
	}

	[WebMethod]
	public String editQuestion(int questionID, int questionNumber, string questionText)
	{
		try
		{
			string cnnstring = ConfigurationManager.ConnectionStrings["ApplicationServices"].ToString();
			SqlConnection con = new SqlConnection(cnnstring);
			con.Open();
			string sql = "Update Questions set QuestionNumber='" + questionNumber + "', QuestionText='" + questionText + "' where QuestionID like '" + questionID + "'";
			SqlCommand cmd = new SqlCommand(sql, con);
			cmd.ExecuteNonQuery();
			con.Close();
			return "Question is successfully changed!";
		}
		catch (Exception e1)
		{
			return "Question is not successfully changed! " + e1.Message;
		}
	}

	[WebMethod]
	public String deleteQuestion(int questionID)
	{
		try
		{
			string cnnstring = ConfigurationManager.ConnectionStrings["ApplicationServices"].ToString();
			SqlConnection con = new SqlConnection(cnnstring);
			con.Open();
			string sql = "Delete from Questions where QuestionID like '" + questionID + "'";
			SqlCommand cmd = new SqlCommand(sql, con);
			cmd.ExecuteNonQuery();
			con.Close();

			return "Question is successfully deleted!";
		}
		catch (Exception e1)
		{
			return "Question is not successfully deleted! " + e1.Message;
		}
	}

	[WebMethod]
	public string addAnswer(int questionID, string choiceLetter, string choiceText, int rightChoice)
	{
		try
		{
			string cnnstring = ConfigurationManager.ConnectionStrings["ApplicationServices"].ToString();
			SqlConnection con = new SqlConnection(cnnstring);
			con.Open();
            string sql = "Insert into Choices(QuestionID, ChoiceLetter, ChoiceText, RightChoice) VALUES (@QuestionID, @ChoiceLetter, @ChoiceText, @RightChoice)";
			SqlCommand cmd = new SqlCommand(sql, con);
			cmd.Parameters.AddWithValue("@QuestionID", questionID);
			cmd.Parameters.AddWithValue("@ChoiceLetter", choiceLetter);
			cmd.Parameters.AddWithValue("@ChoiceText", choiceText);
            cmd.Parameters.AddWithValue("@RightChoice", rightChoice);
			cmd.ExecuteNonQuery();
			con.Close();
			return "Answer is successfully added!";
		}
		catch (Exception e1)
		{
			return "Answer is not added! " + e1.Message;
		}
	}

	[WebMethod]
	public String editAnswer(int questionID, string choiceLetter, string choiceText)
	{
		try
		{
			string cnnstring = ConfigurationManager.ConnectionStrings["ApplicationServices"].ToString();
			SqlConnection con = new SqlConnection(cnnstring);
			con.Open();
			string sql = "Update Choices set ChoiceLetter='" + choiceLetter + "', ChoiceText='" + choiceText + "' where QuestionID like '" + questionID + "'";
			SqlCommand cmd = new SqlCommand(sql, con);
			cmd.ExecuteNonQuery();
			con.Close();
			return "Answer is successfully changed!";
		}
		catch (Exception e1)
		{
			return "Answer is not successfully changed! " + e1.Message;
		}
	}

	[WebMethod]
	public String deleteAnswer(int questionID)
	{
		try
		{
			string cnnstring = ConfigurationManager.ConnectionStrings["ApplicationServices"].ToString();
			SqlConnection con = new SqlConnection(cnnstring);
			con.Open();
			string sql = "Delete from Choices where QuestionID like '" + questionID + "'";
			SqlCommand cmd = new SqlCommand(sql, con);
			cmd.ExecuteNonQuery();
			con.Close();

			return "Answer is successfully deleted!";
		}
		catch (Exception e1)
		{
			return "Answer is not successfully deleted! " + e1.Message;
		}
	}

	[WebMethod]
	public DataSet selectChoicesByQuestionID(int QuestionID)
	{
		string cnnstring = ConfigurationManager.ConnectionStrings["ApplicationServices"].ToString();
		SqlConnection con = new SqlConnection(cnnstring);
		con.Open();
		SqlCommand cmd = new SqlCommand("SELECT ChoiceID, ChoiceText FROM Choices WHERE QuestionID like '" + QuestionID + "'", con);
		cmd.ExecuteNonQuery();
		SqlDataAdapter da = new SqlDataAdapter(cmd);
		DataSet ds = new DataSet();
		da.Fill(ds);
		con.Close();
		return ds;
	}

	[WebMethod]
    public string addCompletedChapterByStudent(Guid studentId, int chapterId, int hasCompleted)
	{
		try
		{
			string cnnstring = ConfigurationManager.ConnectionStrings["ApplicationServices"].ToString();
			SqlConnection con = new SqlConnection(cnnstring);
			con.Open();
			string sql = "Insert into StudentOblast (StudentID, OblastID, HasCompleted) VALUES (@StudentID, @OblastID, @HasCompleted)";
			SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@StudentID", studentId);
            cmd.Parameters.AddWithValue("@OblastID", chapterId);
			cmd.Parameters.AddWithValue("@HasCompleted", hasCompleted);
			cmd.ExecuteNonQuery();
			con.Close();
			return "Chapter completed added to database successfully!";
		}
		catch (Exception e1)
		{
			return "Chapter completed add to database failed!" + e1.Message;
		}
	}

	[WebMethod]
	public DataSet checkRightChoice(int ChoiceID)
	{
		string cnnstring = ConfigurationManager.ConnectionStrings["ApplicationServices"].ToString();
		SqlConnection con = new SqlConnection(cnnstring);
		con.Open();
		SqlCommand cmd = new SqlCommand("SELECT RightChoice FROM Choices WHERE ChoiceID like '" + ChoiceID + "'", con);
		cmd.ExecuteNonQuery();
		SqlDataAdapter da = new SqlDataAdapter(cmd);
		DataSet ds = new DataSet();
		da.Fill(ds);
		con.Close();
		return ds;
	}

	[WebMethod]
	public string addSubjectQuestionData(int questionID, int subjectID, int oblastID)
	{
		try
		{
			string cnnstring = ConfigurationManager.ConnectionStrings["ApplicationServices"].ToString();
			SqlConnection con = new SqlConnection(cnnstring);
			con.Open();
			string sql = "Insert into SubjectQuestion(QuestionID, SubjectID, OblastID) VALUES (@QuestionID, @SubjectID, @OblastID)";
			SqlCommand cmd = new SqlCommand(sql, con);
			cmd.Parameters.AddWithValue("@QuestionID", questionID);
			cmd.Parameters.AddWithValue("@SubjectID", subjectID);
			cmd.Parameters.AddWithValue("@OblastID", oblastID);
			cmd.ExecuteNonQuery();
			con.Close();
			return "Question is successfully added!";
		}
		catch (Exception e1)
		{
			return "Question is not added! " + e1.Message;
		}
	}

    [WebMethod]
    public int selectNumberQuestionByChapter(int chapterID)
    {
        string cnnstring = ConfigurationManager.ConnectionStrings["ApplicationServices"].ToString();
        SqlConnection con = new SqlConnection(cnnstring);
        con.Open();
        SqlCommand cmd = new SqlCommand("SELECT COUNT(QuestionID) AS Expr1 FROM SubjectQuestion WHERE (OblastID = '" + chapterID + "')", con);
        cmd.ExecuteNonQuery();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        con.Close();

        int result = Int32.Parse(ds.Tables[0].Rows[0].ItemArray[0].ToString());
        return result;
    }    

	[WebMethod]
	public DataSet selectAllSubjects()
	{
		string cnnstring = ConfigurationManager.ConnectionStrings["ApplicationServices"].ToString();
		SqlConnection con = new SqlConnection(cnnstring);
		con.Open();
		SqlCommand cmd = new SqlCommand("SELECT Subject.SubjectID, Subject.SubjectName, Subject.Semestar, Subject.Credits FROM Subject ", con);
		cmd.ExecuteNonQuery();
		SqlDataAdapter da = new SqlDataAdapter(cmd);
		DataSet ds = new DataSet();
		da.Fill(ds);
		con.Close();
		return ds;
	}

	[WebMethod]
	public DataSet selectAllOblastBySubject(int subjectID)
	{
		string cnnstring = ConfigurationManager.ConnectionStrings["ApplicationServices"].ToString();
		SqlConnection con = new SqlConnection(cnnstring);
		con.Open();
		SqlCommand cmd = new SqlCommand("SELECT OblastID, SubjectID, Name FROM Oblast where SubjectID like '" + subjectID + "'", con);
		cmd.ExecuteNonQuery();
		SqlDataAdapter da = new SqlDataAdapter(cmd);
		DataSet ds = new DataSet();
		da.Fill(ds);
		con.Close();
		return ds;
	}    

	[WebMethod]
	public string addVisitedQuestion(int QuestionID, int numOfQuestion)
	{
		try
		{
			string cnnstring = ConfigurationManager.ConnectionStrings["ApplicationServices"].ToString();
			SqlConnection con = new SqlConnection(cnnstring);
			con.Open();
			string sql = "Insert into VisitQuestion (QuestionID, Visited, VisitedNumber, Answered, AnsweredTrue) VALUES (@QuestionID, @Visited, @VisitedNumber, @Answered, @AnsweredTrue)";
			SqlCommand cmd = new SqlCommand(sql, con);
			cmd.Parameters.AddWithValue("@QuestionID", QuestionID);
			cmd.Parameters.AddWithValue("@Visited", 1);
			cmd.Parameters.AddWithValue("@VisitedNumber", numOfQuestion);
			cmd.Parameters.AddWithValue("@Answered", 0);
			cmd.Parameters.AddWithValue("@AnsweredTrue", 0);
			cmd.ExecuteNonQuery();
			con.Close();
			return "Prashanjeto e uspeshno dodadeno!";
		}
		catch (Exception e1)
		{
			return "Prashanjeto ne e dodadeno! " + e1.Message;
		}
	}

	[WebMethod]
	public int selectVisitedByQuestionID(int questionID)
	{
		string cnnstring = ConfigurationManager.ConnectionStrings["ApplicationServices"].ToString();
		SqlConnection con = new SqlConnection(cnnstring);
		con.Open();
		SqlCommand cmd = new SqlCommand("Select Visited from VisitQuestion where QuestionID like '" + questionID + "'", con);
		SqlDataAdapter da = new SqlDataAdapter(cmd);
		DataSet ds = new DataSet();
		DataTable dt = new DataTable();
		da.Fill(ds);
		dt = ds.Tables[0];
		con.Close();
		string visited = dt.Rows[0]["Visited"].ToString();
		return Int32.Parse(visited);
	}

	[WebMethod]
	public int selectVisitedNumberByQuestionID(int questionID)
	{
		string cnnstring = ConfigurationManager.ConnectionStrings["ApplicationServices"].ToString();
		SqlConnection con = new SqlConnection(cnnstring);
		con.Open();
		SqlCommand cmd = new SqlCommand("Select VisitedNumber from VisitQuestion where QuestionID like '" + questionID + "'", con);
		SqlDataAdapter da = new SqlDataAdapter(cmd);
		DataSet ds = new DataSet();
		DataTable dt = new DataTable();
		da.Fill(ds);
		dt = ds.Tables[0];
		con.Close();
		string visited = dt.Rows[0]["VisitedNumber"].ToString();
		return Int32.Parse(visited);
	}

	[WebMethod]
	public int selectAnsweredTrueByQuestionID(int questionID)
	{
		string cnnstring = ConfigurationManager.ConnectionStrings["ApplicationServices"].ToString();
		SqlConnection con = new SqlConnection(cnnstring);
		con.Open();
		SqlCommand cmd = new SqlCommand("Select AnsweredTrue from VisitQuestion where QuestionID like '" + questionID + "'", con);
		SqlDataAdapter da = new SqlDataAdapter(cmd);
		DataSet ds = new DataSet();
		DataTable dt = new DataTable();
		da.Fill(ds);
		dt = ds.Tables[0];
		con.Close();
		string visited = dt.Rows[0]["AnsweredTrue"].ToString();
		return Int32.Parse(visited);
	}

	[WebMethod]
	public int selectNumberLastAnsweredQuestions(int StrategyValue)
	{
		string cnnstring = ConfigurationManager.ConnectionStrings["ApplicationServices"].ToString();
		SqlConnection con = new SqlConnection(cnnstring);
		con.Open();
		SqlCommand cmd = new SqlCommand("SELECT COUNT(*) AS NumberLastAnswered " +
						"FROM (SELECT TOP (" + StrategyValue + ") * " +
						"FROM VisitQuestion ORDER BY DateAnswered DESC) AS table1 " +
						"WHERE (AnsweredTrue = '1') GROUP BY AnsweredTrue", con);
		SqlDataAdapter da = new SqlDataAdapter(cmd);
		DataSet ds = new DataSet();
		DataTable dt = new DataTable();
		da.Fill(ds);
		dt = ds.Tables[0];
		con.Close();
		string numLastAnsweredQuestions = dt.Rows[0]["NumberLastAnswered"].ToString();
		return Int32.Parse(numLastAnsweredQuestions);
	}

	[WebMethod]
	public string setAnsweredTrueQuestion(int QuestionID, int answeredTrue)
	{
		try
		{
			string cnnstring = ConfigurationManager.ConnectionStrings["ApplicationServices"].ToString();
			SqlConnection con = new SqlConnection(cnnstring);
			con.Open();
			string sql = "";
			SqlCommand cmd = new SqlCommand();
			cmd.Connection = con;
			cmd.CommandType = System.Data.CommandType.Text;

			if (answeredTrue == 0)
			{
				sql = "Update VisitQuestion set [Answered]=@Answered, [DateAnswered]=@DateAnswered where QuestionID=@QuestionID";
			}
			else if (answeredTrue == 1)
			{
				sql = "Update VisitQuestion set [Answered]=@Answered, [AnsweredTrue]=@AnsweredTrue, [DateAnswered]=@DateAnswered where QuestionID=@QuestionID";
				cmd.Parameters.AddWithValue("@AnsweredTrue", '1');
			}
			cmd.Parameters.AddWithValue("@QuestionID", QuestionID);
			cmd.Parameters.AddWithValue("@Answered", 1);
			cmd.Parameters.AddWithValue("@DateAnswered", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));

			cmd.CommandText = sql;
			cmd.ExecuteNonQuery();
			con.Close();
			return "Prashanjeto e uspeshno promeneto!";
		}
		catch (Exception e1)
		{
			return "Prashanjeto ne e promeneto! " + e1.Message;
		}
	}

	[WebMethod]
	public int selektirajQuestionID(int questionID)
	{
		string cnnstring = ConfigurationManager.ConnectionStrings["ApplicationServices"].ToString();
		SqlConnection con = new SqlConnection(cnnstring);
		con.Open();
		SqlCommand cmd = new SqlCommand("Select QuestionID from VisitQuestion where QuestionID like '" + questionID + "'", con);
		SqlDataAdapter da = new SqlDataAdapter(cmd);
		DataSet ds = new DataSet();
		DataTable dt = new DataTable();
		da.Fill(ds);
		dt = ds.Tables[0];
		con.Close();
		if (dt.Rows.Count != 0)
		{
			return 1;
		}
		else
		{
			return 0;
		}

	}

	[WebMethod]
	public void removeDataFromVisitQuestion()
	{
		try
		{
			string cnnstring = ConfigurationManager.ConnectionStrings["ApplicationServices"].ToString();
			SqlConnection con = new SqlConnection(cnnstring);
			con.Open();
			string sql = "Delete from VisitQuestion";
			SqlCommand cmd = new SqlCommand(sql, con);
			cmd.ExecuteNonQuery();
			con.Close();
		}
		catch (Exception e1)
		{
			Console.WriteLine(e1.Message);
		}
	}

	[WebMethod]
	public DataSet selectSubjectByUser()
	{
		string cnnstring = ConfigurationManager.ConnectionStrings["ApplicationServices"].ToString();
		SqlConnection con = new SqlConnection(cnnstring);
		con.Open();
		SqlCommand cmd = new SqlCommand("SELECT Subject.SubjectID, Subject.SubjectName, Subject.Semestar, Subject.Credits FROM Subject", con);
		cmd.ExecuteNonQuery();
		SqlDataAdapter da = new SqlDataAdapter(cmd);
		DataSet ds = new DataSet();
		da.Fill(ds);
		con.Close();
		return ds;
	}

	[WebMethod]
	public DataSet selectOblast(int subjectID)
	{
		string cnnstring = ConfigurationManager.ConnectionStrings["ApplicationServices"].ToString();
		SqlConnection con = new SqlConnection(cnnstring);
		con.Open();
		SqlCommand cmd = new SqlCommand("SELECT Oblast.Name, Oblast.OblastID FROM Oblast WHERE Oblast.SubjectID like '" + subjectID + "'", con);
		cmd.ExecuteNonQuery();
		SqlDataAdapter da = new SqlDataAdapter(cmd);
		DataSet ds = new DataSet();
		da.Fill(ds);
		con.Close();
		return ds;
	}

	[WebMethod]
	public DataSet selectOblastByStudent(int subjectID, Guid studentID)
	{
		string cnnstring = ConfigurationManager.ConnectionStrings["ApplicationServices"].ToString();
		SqlConnection con = new SqlConnection(cnnstring);
		con.Open();
		SqlCommand cmd = new SqlCommand("SELECT Oblast.Name, Oblast.OblastID, StudentOblast.HasCompleted " +
			"FROM Oblast INNER JOIN StudentOblast ON Oblast.OblastID = StudentOblast.OblastID " +
			"WHERE Oblast.SubjectID like '" + subjectID + "' " +
			"AND StudentOblast.StudentID like '" + studentID + "'", con);
		cmd.ExecuteNonQuery();
		SqlDataAdapter da = new SqlDataAdapter(cmd);
		DataSet ds = new DataSet();
		da.Fill(ds);
		con.Close();
		return ds;
	}

	[WebMethod]
	public int selectCompletedOblastByStudent(int oblastID, Guid studentID)
	{
		string cnnstring = ConfigurationManager.ConnectionStrings["ApplicationServices"].ToString();
		SqlConnection con = new SqlConnection(cnnstring);
		con.Open();
		SqlCommand cmd = new SqlCommand("SELECT HasCompleted FROM StudentOblast WHERE OblastID like '" + oblastID + "' AND StudentID like '" + studentID + "'", con);
		cmd.ExecuteNonQuery();
		SqlDataAdapter da = new SqlDataAdapter(cmd);
		DataSet ds = new DataSet();
		da.Fill(ds);
		con.Close();
		int HasCompleted = 0;
		if (ds.Tables.Count != 0)
		{
			DataTable dt = new DataTable();
			dt = ds.Tables[0];
			if (dt.Rows.Count != 0)
			{
				HasCompleted = Int32.Parse(dt.Rows[0][0].ToString());
			}			
			con.Close();
		}		
		return HasCompleted;
	}

	[WebMethod]
	public DataSet selectStrategy()
	{
		string cnnstring = ConfigurationManager.ConnectionStrings["ApplicationServices"].ToString();
		SqlConnection con = new SqlConnection(cnnstring);
		con.Open();
		SqlCommand cmd = new SqlCommand("SELECT  Strategy.StrategyID, Strategy.Name FROM Strategy", con);
		cmd.ExecuteNonQuery();
		SqlDataAdapter da = new SqlDataAdapter(cmd);
		DataSet ds = new DataSet();
		da.Fill(ds);
		con.Close();
		return ds;
	}

	[WebMethod]
	public DataSet selectStrategyBySubjectAndOblast(int subjectID, int oblastID)
	{
		string cnnstring = ConfigurationManager.ConnectionStrings["ApplicationServices"].ToString();
		SqlConnection con = new SqlConnection(cnnstring);
		con.Open();
		SqlCommand cmd = new SqlCommand("SELECT * from OnlineLearning where SubjectID like '" + subjectID + "' and OblastID like '" + oblastID + "'", con);
		cmd.ExecuteNonQuery();
		SqlDataAdapter da = new SqlDataAdapter(cmd);
		DataSet ds = new DataSet();
		da.Fill(ds);
		con.Close();
		return ds;
	}    

	[WebMethod]
	public int selectStrategyValue(int subjectID, int oblastID, int strategyID)
	{
		string cnnstring = ConfigurationManager.ConnectionStrings["ApplicationServices"].ToString();
		SqlConnection con = new SqlConnection(cnnstring);
		con.Open();
		SqlCommand cmd = new SqlCommand("Select StrategyValue from OnlineLearning where SubjectID like '" + subjectID + "' and OblastID like '" + oblastID + "' and StrategyID like '" + strategyID + "'", con);
		SqlDataAdapter da = new SqlDataAdapter(cmd);
		DataSet ds = new DataSet();
		DataTable dt = new DataTable();
		da.Fill(ds);
		dt = ds.Tables[0];
		int strategyValue = Int32.Parse(dt.Rows[0]["StrategyValue"].ToString());
		con.Close();
		return strategyValue;

	}

	[WebMethod]
	public DataSet selectQuestions()
	{
		string cnnstring = ConfigurationManager.ConnectionStrings["ApplicationServices"].ToString();
		SqlConnection con = new SqlConnection(cnnstring);
		con.Open();
		SqlCommand cmd = new SqlCommand("SELECT TOP 1 * FROM Questions order by newid()", con);
		cmd.ExecuteNonQuery();
		SqlDataAdapter da = new SqlDataAdapter(cmd);
		DataSet ds = new DataSet();
		da.Fill(ds);
		con.Close();
		return ds;
	}

	[WebMethod]
	public DataSet selectAllQuestions()
	{
		string cnnstring = ConfigurationManager.ConnectionStrings["ApplicationServices"].ToString();
		SqlConnection con = new SqlConnection(cnnstring);
		con.Open();
		SqlCommand cmd = new SqlCommand("SELECT * FROM Questions", con);
		cmd.ExecuteNonQuery();
		SqlDataAdapter da = new SqlDataAdapter(cmd);
		DataSet ds = new DataSet();
		da.Fill(ds);
		con.Close();
		return ds;
	}

	[WebMethod]
	public DataSet selectOneQuestionBySubjectAndField(int SubjectID, int OblastID)
	{
		DataSet ds = new DataSet();
		try
		{
			string cnnstring = ConfigurationManager.ConnectionStrings["ApplicationServices"].ToString();
			SqlConnection con = new SqlConnection(cnnstring);
			con.Open();
			SqlCommand cmd = new SqlCommand("SELECT TOP 1 Q.QuestionID, Q.QuestionNumber, Q.QuestionText FROM Questions AS Q INNER JOIN SubjectQuestion AS SQ " +
			"ON Q.QuestionID = SQ.QuestionID where (SQ.SubjectID like '" + SubjectID + "') AND (SQ.OblastID like '" + OblastID + "') order by newid()", con);
			cmd.ExecuteNonQuery();
			SqlDataAdapter da = new SqlDataAdapter(cmd);
			
			da.Fill(ds);
			con.Close();
		   
		}
		catch (Exception e1)
		{
			Console.Write(e1.Message);
		}
		return ds;
	}

	[WebMethod]
	public DataSet selectAllQuestionsBySubjectAndField(int SubjectID, int OblastID)
	{
		string cnnstring = ConfigurationManager.ConnectionStrings["ApplicationServices"].ToString();
		SqlConnection con = new SqlConnection(cnnstring);
		con.Open();
		SqlCommand cmd = new SqlCommand("SELECT Q.QuestionID, Q.QuestionNumber, Q.QuestionText FROM Questions AS Q INNER JOIN SubjectQuestion AS SQ " +
		"ON Q.QuestionID = SQ.QuestionID where (SQ.SubjectID like '" + SubjectID + "') AND (SQ.OblastID like '" + OblastID + "') order by newid()", con);
		cmd.ExecuteNonQuery();
		SqlDataAdapter da = new SqlDataAdapter(cmd);
		DataSet ds = new DataSet();
		da.Fill(ds);
		con.Close();
		return ds;
	}

	[WebMethod]
	public DataSet selectQuestionsBySubjectFieldAndValue(int SubjectID, int OblastID, int Value)
	{
		string cnnstring = ConfigurationManager.ConnectionStrings["ApplicationServices"].ToString();
		SqlConnection con = new SqlConnection(cnnstring);
		con.Open();
		SqlCommand cmd = new SqlCommand("SELECT TOP '" + Value + "' Q.QuestionID, Q.QuestionNumber, Q.QuestionText FROM Questions AS Q INNER JOIN SubjectQuestion AS SQ " +
		"ON Q.QuestionID = SQ.QuestionID where (SQ.SubjectID like '" + SubjectID + "') AND (SQ.OblastID like '" + OblastID + "') order by newid()", con);
		cmd.ExecuteNonQuery();
		SqlDataAdapter da = new SqlDataAdapter(cmd);
		DataSet ds = new DataSet();
		da.Fill(ds);
		con.Close();
		return ds;
	}

	[WebMethod]
	public string addOnlineLearning(int subjectID, int oblastID, int strategyID, int strategyValue)
	{
		try
		{
			string cnnstring = ConfigurationManager.ConnectionStrings["ApplicationServices"].ToString();
			SqlConnection con = new SqlConnection(cnnstring);
			con.Open();
			string sql = "Insert into OnlineLearning (SubjectID, OblastID, StrategyID, StrategyValue) VALUES (@SubjectID, @OblastID, @StrategyID, @StrategyValue)";
			SqlCommand cmd = new SqlCommand(sql, con);
			cmd.Parameters.AddWithValue("@SubjectID", subjectID);
			cmd.Parameters.AddWithValue("@OblastID", oblastID);
			cmd.Parameters.AddWithValue("@StrategyID", strategyID);
			cmd.Parameters.AddWithValue("@StrategyValue", strategyValue);
			cmd.ExecuteNonQuery();
			con.Close();
			return "Online learning e uspeshno dodadeno!";
		}
		catch (Exception e1)
		{
			return "Online learning ne e dodadeno! " + e1.Message;
		}
	}
}
