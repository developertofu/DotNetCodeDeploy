using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RDSTestHarness
{
    class Program
    {
        static void Main(string[] args)
        {

            string user = "awsuser";
            string password = "";
            string connString = string.Format("Data Source=testdbinstance.cx0dtiz2oqwo.us-east-1.rds.amazonaws.com;User ID={0};Password={1};Initial Catalog=soccerteam-stats", user, password);
            int downtime = 0;

            bool runLoop = true;
            while (runLoop) {
                SqlConnection Conn = null;
                SqlCommand DBCmd;
                try
                {
                    Conn = new SqlConnection(connString);
                    Conn.Open();
                    string strSQL = "SELECT Count(*) from Players";
                    DBCmd = new SqlCommand(strSQL, Conn);
                    Console.WriteLine("Player Count is " + DBCmd.ExecuteScalar());
                    Thread.Sleep(5000);
                }
                catch (Exception e)
                {
                    downtime += 5;
                    Console.WriteLine(string.Format("SQL Error Occurred! Down for {0} seconds. Error: {1}", downtime, e.Message));
                    Thread.Sleep(5000);
                }
                finally
                {
                    Conn.Dispose();
                }
            }
        }
    }
}
