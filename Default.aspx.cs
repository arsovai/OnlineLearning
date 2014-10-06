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

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            textLabel.Text = "Добредојдовте на Learning environment порталот на ФИНКИ. Преку овој портал можете да пристапите до содржините на курсевите кои се дел од студиите на Факултетот за информатички науки и компјутерско инжинерство.";
             
            if (!Page.User.Identity.IsAuthenticated)
            {
                textLabel.Text += "<br/>" + "За да се најавите на системот кликнете на линкот \"Најави се\". За корисничко име користете го својот индекс."; 
                //Response.Redirect("LoginPage.aspx");
            }
            else if (Page.User.Identity.IsAuthenticated)
            {
                if (Page.User.IsInRole("student"))
                {
                    Master.FindControl("Li4").Visible = false;
                }
            }
        }
    }
}
