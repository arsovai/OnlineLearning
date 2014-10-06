using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Data;

public partial class ViewSubjects : System.Web.UI.Page
{
	localhost.OnlineLearning dbws = new localhost.OnlineLearning();
	static int SubjectID;
	static int OblastID;
	string strategyValue;
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!IsPostBack)
		{
			if (Page.User.Identity.IsAuthenticated)
			{
				Guid userID = (Guid)Membership.GetUser().ProviderUserKey;
				DataSet ds = dbws.selectSubjectByUser();
				gvSubjects.DataSource = ds;
				gvSubjects.DataBind();

                if (Page.User.IsInRole("student"))
                {
                    Master.FindControl("Li4").Visible = false;
                }

			}
		}
	}

	protected void subjects_RowCommand(object sender, GridViewCommandEventArgs e)
	{
		if (e.CommandName == "createOnlineLearning")
		{
			GridViewRow selectedRow = gvSubjects.Rows[Convert.ToInt32(e.CommandArgument)];
			Label l = (Label)selectedRow.Cells[0].FindControl("SubjectID");
			SubjectID = int.Parse(l.Text);
			Session["SubjectID"] = SubjectID.ToString();
			DataSet dsOblast = dbws.selectOblast(SubjectID);
			DataTable dt = new DataTable();
			dt = dsOblast.Tables[0];
			DataSet dsStrategy = dbws.selectStrategy();
			DataTable dtStrategy = new DataTable();
			dtStrategy = dsStrategy.Tables[0];
			
			gvSubjects.Visible = false;
			panelCreateOL.Visible = true;

			ddlOblast.DataSource = dt;
			ddlOblast.DataTextField = "Name";
			ddlOblast.DataValueField = "OblastID";
			ddlOblast.Items.Insert(0, new ListItem("--------Одбери--------", "0"));
			ddlOblast.SelectedIndex = 0;
			ddlOblast.DataBind();

			ddlStrategy.DataSource = dtStrategy;
			ddlStrategy.DataTextField = "Name";
			ddlStrategy.DataValueField = "StrategyID";
			ddlStrategy.Items.Insert(0, new ListItem("--------Одбери--------", "0"));
			ddlStrategy.SelectedIndex = 0;
			ddlStrategy.DataBind();

		}

		if (e.CommandName == "selectOblast")
		{
			GridViewRow selectedRow = gvSubjects.Rows[Convert.ToInt32(e.CommandArgument)];
			Label l = (Label)selectedRow.Cells[0].FindControl("SubjectID");
			SubjectID = int.Parse(l.Text);
			Session["SubjectID"] = SubjectID.ToString();
			DataSet dsOblast;
			dsOblast = dbws.selectOblast(SubjectID);
			DataTable dt = new DataTable();
			dt = dsOblast.Tables[0];
			gvoblasti.DataSource = dsOblast;
			gvoblasti.DataBind();
			gvSubjects.Visible = false;            

		}
	}

	protected void ddlStrategy_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (ddlStrategy.SelectedValue == "1")
		{
			scValue.Visible = true;
			tcValue.Visible = false;
			createOnlineLearning.Visible = true;
			strategyValue = tbSubsequentlyCorrect.Text;
		}
		else if (ddlStrategy.SelectedValue == "2")
		{
			scValue.Visible = false;
			tcValue.Visible = true;
			strategyValue = tbTotalCorrect.Text;
			createOnlineLearning.Visible = true;
		}
	}

	protected void createOnlineLearning_Click(object sender, EventArgs e)
	{ 

		if (ddlStrategy.SelectedValue == "1")
		{
			strategyValue = tbSubsequentlyCorrect.Text;
		}
		else if (ddlStrategy.SelectedValue == "2")
		{
			strategyValue = tbTotalCorrect.Text;
		}

		int oblastID = Int32.Parse(ddlOblast.SelectedValue);
		int strategyID = Int32.Parse(ddlStrategy.SelectedValue);
		poraka.Text = dbws.addOnlineLearning(Int32.Parse(Session["SubjectID"].ToString()),oblastID,strategyID,Int32.Parse(strategyValue));

	}
   
	protected void oblasti_RowCommand(object sender, GridViewCommandEventArgs e)
	{
		if (e.CommandName == "startOnlineLearning")
		{
			GridViewRow selectedRow = gvoblasti.Rows[Convert.ToInt32(e.CommandArgument)];
			Label l = (Label)selectedRow.Cells[0].FindControl("OblastID");
			OblastID = int.Parse(l.Text);
			Session["SubjectID"] = SubjectID.ToString();
			Session["OblastID"] = OblastID.ToString();

            if (checkForEmptyField(SubjectID, OblastID))
            {
                System.Windows.Forms.MessageBox.Show("Грешка! Областа не може да се изминува!");
                return;
            }

			if(Page.User.IsInRole("profesor"))
			{
				Response.Redirect("OnlineLearning.aspx");
			}
			else
			{
				Guid StudentID = (Guid)Membership.GetUser().ProviderUserKey;
				int hasCompleted = dbws.selectCompletedOblastByStudent(OblastID, StudentID);
				if (hasCompleted == 1)
				{
						System.Windows.Forms.MessageBox.Show("Избраната област е веќе измината!");
				}
				else
				{
					Response.Redirect("OnlineLearning.aspx");
				}
			}
		}
	}

    protected bool checkForEmptyField(int SubjectID, int OblastID)
    {
        DataTable dtQuestions = dbws.selectAllQuestionsBySubjectAndField(SubjectID, OblastID).Tables[0];

        if (dtQuestions.Rows.Count < 1)
        {
            return true;
        }

        else return false;
       
    }
}