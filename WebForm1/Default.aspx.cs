using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm1
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            gvbind(1, 0, 0, null);
        }
        public DataSet gvbind(int action, int id, int orderid, string description)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            HttpClient req = new HttpClient();
            HttpResponseMessage http_res = new HttpResponseMessage();
            DataSet ds = new DataSet();
            try
            {
                //action = 1;
                //id = 0;
                //orderid = 0;
                //description = "";
                string body = "{"
                                  + Environment.NewLine + "\"action\":" + action + ","
                                     + Environment.NewLine + "\"id\":" + id + ","
                                     + Environment.NewLine + "\"orderid\":" + orderid + ","
                                          + Environment.NewLine + "\"description\":\"" + description + "\""
                 + Environment.NewLine + "}";

                //lblbody.Text = body;
                req.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                StringContent sc = new StringContent(body, Encoding.UTF8, "application/json");
                http_res = req.PostAsync("https://localhost:44347/CrudTest", sc).Result;
                string json = http_res.Content.ReadAsStringAsync().Result;

                dynamic jsonDict = JsonConvert.DeserializeObject(json);

                var dt = JsonConvert.DeserializeObject<RootObject>(jsonDict).Table;
                //var ds2 = JsonConvert.DeserializeObject<RootObject>(jsonDict).Table1;
                GridView1.DataSource = dt;
                GridView1.DataBind();




            }
            catch (Exception ex)
            {
                lblbody.Text = ex.Message;
            }
            return ds;
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "EditData")
                {
                    var clickedButton = e.CommandSource as Button;
                    var clickedRow = clickedButton.NamingContainer as GridViewRow;
                    lblID.Text = clickedRow.Cells[0].Text;
                    txtOrderID.Text = clickedRow.Cells[1].Text;
                    txtDescription.Text = clickedRow.Cells[2].Text;
                    btnsubmit.Text = "Update";
                }
                if (e.CommandName == "DeleteData")
                {
                    var clickedButton = e.CommandSource as Button;
                    var clickedRow = clickedButton.NamingContainer as GridViewRow;
                    lblID.Text = clickedRow.Cells[0].Text;
                    gvbind(4, Convert.ToInt32(lblID.Text), Convert.ToInt32(txtOrderID.Text == "" ? "0" : txtOrderID.Text), txtDescription.Text);
                }
                gvbind(1, 0, 0, null);
            }
            catch (Exception ex)
            {

            }
        }
        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int index = Convert.ToInt32(e.RowIndex);
                 
            }
            catch (Exception ex)
            {
                
            }
        }
        void resetcontrols()
        {
            lblID.Text = string.Empty;
            txtOrderID.Text = string.Empty;
            txtDescription.Text = string.Empty;
        }

        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            if (btnsubmit.Text == "Submit")
            {
                gvbind(2, Convert.ToInt32(lblID.Text == "" ? "0" : lblID.Text), Convert.ToInt32(txtOrderID.Text), txtDescription.Text);
                resetcontrols();
            }
            else
            {
                gvbind(3, Convert.ToInt32(lblID.Text), Convert.ToInt32(txtOrderID.Text), txtDescription.Text);
                resetcontrols();
            }
            btnsubmit.Text = "Submit";
            gvbind(1, 0, 0, null);
        }
    }
}
class RootObject
{
    public DataTable Table;
    //public DataTable Table1;
}