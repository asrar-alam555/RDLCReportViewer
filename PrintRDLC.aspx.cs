using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ReportViewerMVC
{
    public partial class PrintRDLC : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LocalReport rep = rptReportViewer.LocalReport;
                rep.ReportPath = "EmployeeList.rdlc";
                ReportDataSource rds = new ReportDataSource();

                DataTable dt = new DataTable();
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter();
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeDBConnectionString"].ConnectionString);
                
                cmd = new SqlCommand("SP_SelectEMP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                da.SelectCommand = cmd;

                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    rds.Name = "DS_PrintRDLC";
                    rds.Value = dt;
                    rep.DataSources.Add(rds);
                }


            }
        }
    }
}