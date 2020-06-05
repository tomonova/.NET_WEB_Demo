using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;

public class BasePage : System.Web.UI.Page
{
    public int DDLIndexLanguage
    {
        get
        {
            if (Request.Cookies["RWALanguage"] != null)
            {
                if (Request.Cookies["RWALanguage"]["index"] != null)
                    return int.Parse(Request.Cookies["RWALanguage"]["index"]);
            }
            return 0;
        }
        set
        {
            Request.Cookies["RWALanguage"]["index"] = value.ToString();
        }
    }
    protected override void InitializeCulture()
    {
        if (Request.Cookies["RWALanguage"] != null)
        {
            if (Request.Cookies["RWALanguage"]["language"] != null)
            {
                string culture = Request.Cookies["RWALanguage"]["language"];
                Thread.CurrentThread.CurrentCulture = new CultureInfo(culture);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(culture);
            }
        }
        base.InitializeCulture();
    }
    protected override void OnLoad(EventArgs e)
    {
        BaseLoad();
        base.OnLoad(e); 
    }

    private void BaseLoad() 
    {
        if (Session["username"] == null 
            & !HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("Login"))
        {
            Response.Redirect("Login.aspx");
        }
    }

    public BasePage()
    {

    }
}
