using System;
using System.Collections.Generic;
using System.IO;
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
            const string playersPath = "./App_Data/players1.txt";
            const string teamsPath = "./App_Data/teams1.txt";
            Stream playerStream = (PlayerDataUpload.HasFile && PlayerDataUpload.FileName.EndsWith(".txt")) ? PlayerDataUpload.FileContent : null;
            Stream teamStream = (TeamDataUpload.HasFile && TeamDataUpload.FileName.EndsWith(".txt")) ? TeamDataUpload.FileContent : null;

            SoccerExec.Run(ResultPanel, PositionInput.Value, playersPath, teamsPath, playerStream, teamStream);
        }
    }
}