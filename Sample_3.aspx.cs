using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Day5_8
{
    public partial class Sample_3 : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }
        }
        protected void BindGrid()
        {
            string cstring = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
            MySqlConnection con = new MySqlConnection(cstring);
            MySqlCommand cmd = new MySqlCommand("select CustomerID,Companyname,City,Country from customers", con);
            MySqlDataAdapter sda = new MySqlDataAdapter();
            sda.SelectCommand = cmd;
            DataSet ds = new DataSet();
            sda.Fill(ds);
            GridView1.DataSource = ds;
            GridView1.DataBind();
        }
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            BindGrid();
        }
        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string cstring = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
            MySqlConnection con = new MySqlConnection(cstring);
            GridViewRow row = GridView1.Rows[e.RowIndex];
            con.Open();
            MySqlCommand cmd1 = new MySqlCommand("SET FOREIGN_KEY_CHECKS = 0;" + "delete FROM customers where CustomerID= '" + (GridView1.DataKeys[e.RowIndex].Value.ToString()) + "'", con);
            cmd1.ExecuteNonQuery();
            con.Close();
            BindGrid();
        }
        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string cstring = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
            MySqlConnection con = new MySqlConnection(cstring);
            string CusID = (GridView1.DataKeys[e.RowIndex].Value.ToString());
            GridViewRow row = GridView1.Rows[e.RowIndex];
            TextBox textCompanyName = (TextBox)row.Cells[1].Controls[0];
            TextBox textCity = (TextBox)row.Cells[2].Controls[0];
            TextBox textCountry = (TextBox)row.Cells[3].Controls[0];

            GridView1.EditIndex = -1;
            con.Open();
            MySqlCommand cmd = new MySqlCommand("update customers set CompanyName='" + textCompanyName.Text + "',City='" + textCity.Text + "',Country='" + textCountry.Text + "' where CustomerID='" + CusID + "'", con);
            cmd.ExecuteNonQuery();
            con.Close();
            BindGrid();
        }
        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            BindGrid();
        }
        protected void Insert(object sender, EventArgs e)
        {
            string CustomerID = textCustID.Text;
            string CompanyName = textCompanyName.Text;
            string City = textCity.Text;
            string Country = textCountry.Text;
            textCustID.Text = "";
            textCompanyName.Text = "";
            textCity.Text = "";
            textCountry.Text = "";
            //MySqlCommand VerifyCustomerID;
            //object idExists;
            //try
            //{
                string cstring1 = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;                
                MySqlConnection con1 = new MySqlConnection(cstring1);
                //MySqlCommand cmd1 = new MySqlCommand("select CustomerID from customers where CustomerID ='" + CustomerID + "'", con1);
                //VerifyCustomerID = new MySqlCommand(cmd1.ToString(), con1);
                //idExists = VerifyCustomerID.ExecuteReader();
                //if (idExists == null)
                //{
                    MySqlCommand cmd2 = new MySqlCommand("INSERT INTO customers(CustomerID, CompanyName, City, Country ) VALUES ( " + "'" + CustomerID + "'" + "," + "'" + CompanyName + "'" + "," + "'" + City + "'" + "," + "'" + Country + "'" + ")", con1);
                    con1.Open();
                    cmd2.ExecuteNonQuery();
                    con1.Close();
                    BindGrid();
            //    }
            //    else
            //    {
            //        ClientScript.RegisterStartupScript(this.GetType(), "msgbox", "alert('Customer ID already exists');", true);

            //    }
            //}
            //catch (Exception ex)
            //{
            //    Response.Write(ex.Message);
            //}
            //finally
            //{
            //     idExists= null;
            //}
        }
    }
}