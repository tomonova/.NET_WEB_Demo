﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RWA_Admin
{
    public partial class Home : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Page_Error(object sender, EventArgs e)
        {
            Response.Redirect("~/Errors.aspx");
        }
    }
}