using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IgnasLab
{
    public partial class Pagrindinis : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ExecButton_Click(object sender, EventArgs e)
        {
            SoccerExec.Run(ResultPanel, PositionInput.Value);
        }
    }
}