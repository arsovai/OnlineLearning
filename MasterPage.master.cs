using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.User.Identity.IsAuthenticated)
        {
            menubar.Visible = true;
            if (Page.User.IsInRole("profesor"))
            {
                Li4.Visible = true;
            }
        }
        else
        {
            menubar.Visible = false;
            Li4.Visible = false;
        }
        
    }
}
