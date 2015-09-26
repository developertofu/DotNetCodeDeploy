using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace SoccerTeamWeb
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
         
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            SqlConnection Conn = null;
            try
            {
                string user = Request["txtUser"].ToString();
                string password = Request["txtPassword"].ToString();
                string connString = string.Format("Data Source=testdbinstance.cx0dtiz2oqwo.us-east-1.rds.amazonaws.com;User ID={0};Password={1};Initial Catalog=soccerteam-stats", user, password);
                Conn = new SqlConnection(connString);
                Conn.Open();
                WriteContent(Conn);
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            finally
            {
                Conn.Close();
                Conn.Dispose();
            }
        }

        private void WriteContent(SqlConnection Conn)
        {
            try
            {
                string strSQL = "SELECT * from Players";
                SqlCommand DBCmd = new SqlCommand(strSQL, Conn);
                SqlDataReader myDataReader;
                myDataReader = DBCmd.ExecuteReader();

                Response.Write("<table cellpadding='5'><tr><td align='center'><u>All Star</u></td><td align='center'><u>Name</u></td><td align='center'><u>Age</u></td><td align='center'><u>Goals</u></td><td align='center'><u>Position</u></td><td align='center'><u>Princess</u></td>");
                while (myDataReader.Read())
                {
                    Response.Write("<tr>");
                    Response.Write("<td align='center'>");
                    if ((int)myDataReader["Goals"] >= 1)
                    {
                        Response.Write("<img height='25' width='25' src='http://pngimg.com/upload/star_PNG1580.png' />");
                    }
                    Response.Write("</td>");
                    Response.Write("<td align='center'>" + myDataReader["Name"].ToString() + "</td>");
                    Response.Write("<td align='center'>" + myDataReader["Age"].ToString() + "</td>");
                    Response.Write("<td align='center'>" + myDataReader["Goals"].ToString() + "</td>");
                    Response.Write("<td align='center'>" + myDataReader["Position"].ToString() + "</td>");
                    Response.Write("<td align='center'><img height='40' width='40' src='");
                    switch (myDataReader["Princess"].ToString())
                    {
                        case "Ariel":
                            Response.Write("http://img4.wikia.nocookie.net/__cb20130929203816/disney/images/d/d1/Disney-ariel-temporary-tattoo.jpg");
                            break;
                        case "Mulan":
                            Response.Write("http://www.polyvore.com/cgi/img-thing?.out=jpg&size=l&tid=1927341");
                            break;
                        case "Belle":
                            Response.Write("http://img2.wikia.nocookie.net/__cb20120610130947/disney/images/4/45/Belle2.jpg");
                            break;
                        case "Anna":
                            Response.Write("http://vignette3.wikia.nocookie.net/disneyfanon/images/2/2f/Disney_frozen_anna_by_rodrigoyborra-d5u29wz.jpg/revision/latest?cb=20130507125351");
                            break;
                        case "Elsa":
                            Response.Write("http://images6.fanpop.com/image/photos/35800000/Elsa-Anna-disney-frozen-35828419-1280-1153.jpg");
                            break;
                        case "Snow White":
                            Response.Write("http://vignette3.wikia.nocookie.net/p__/images/d/d2/599936-snow_white1_large.jpg/revision/latest?cb=20120617190120&path-prefix=protagonist");
                            break;
                        case "Cinderella":
                            Response.Write("http://www.tripletsandus.com/disney/Cinderella/jpg/Cinderella-Blue-Dress-3.jpg");
                            break;
                        case "Tiana":
                            Response.Write("http://images2.fanpop.com/image/photos/10200000/Princess-Tiana-disney-princess-10215815-420-645.jpg");
                            break;
                        default:
                            Response.Write("https://browshot.com/static/images/not-found.png");
                            break;
                    }
                    Response.Write("'/></td></tr>");

                }
                Response.Write("</table><br/><br/>");
                myDataReader.Close();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
    }
}