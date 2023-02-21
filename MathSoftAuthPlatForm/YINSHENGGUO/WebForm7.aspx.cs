using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business;
using DTOModel;
using MathSoftCommonLib;
using MathSoftModelLib;

namespace MAZIKONG
{
    public partial class WebForm7 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitControls();
            }

        }

        public void InitControls() {
            BLL_View_AnalySISS bLL_View_AnalySISS = new BLL_View_AnalySISS();
            List<SSISDetail> list = bLL_View_AnalySISS.GetDetail(Request.QueryString["Name"]);
            for (int i = 0; i < list.Count; i++)
            {
                list[i].Index = i + 1;
            }

            this.GridView1.DataSource = list;
            this.GridView1.DataBind();


        }

    }
}