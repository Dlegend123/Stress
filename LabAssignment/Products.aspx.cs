using System;

using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.DynamicData;
using System.Linq;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Web.UI.HtmlControls;
using LabAssignment.Models;

namespace LabAssignment
{
    public partial class Products : System.Web.UI.Page
    {
        SqlCommand sqlCommand;
        SqlConnection conn;
        SqlDataReader reader;
        List<Product> list;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Request.IsSecureConnection)
            {
                string url = ConfigurationManager.AppSettings["SecurePath"] + "Products.aspx";
                Response.Redirect(url);
            }
            if (Session["Account"] != null)
            {
                if ((Session["Account"] as ApplicationUser).Roles.Any(x => x.RoleId == "Admin"))
                {
                    if (!Page.Master.FindControl("AdminFunc").Visible)
                        Page.Master.FindControl("AdminFunc").Visible = true;
                    if ((Page.Master.FindControl("SignInLink") as HtmlAnchor).InnerText != "Sign Out")
                        (Page.Master.FindControl("SignInLink") as HtmlAnchor).InnerText = "Sign Out";
                }
                if ((Session["Account"] as ApplicationUser).Roles.Any(x => x.RoleId == "Cust"))
                {
                    if (!Page.Master.FindControl("CustFunc").Visible)
                        Page.Master.FindControl("CustFunc").Visible = true;
                    if ((Page.Master.FindControl("SignInLink") as HtmlAnchor).InnerText != "Sign Out")
                        (Page.Master.FindControl("SignInLink") as HtmlAnchor).InnerText = "Sign Out";
                    (Page.Master.FindControl("CustList") as HtmlAnchor).InnerText = (Session["Account"] as ApplicationUser).UserName;
                }
            }

            conn = new SqlConnection
            {
                ConnectionString = ConfigurationManager.ConnectionStrings["LIConnectionString"].ConnectionString
            };
            sqlCommand = new SqlCommand("select * from product inner join proimage on product.p_id=proimage.p_id", conn);
            conn.Open();
            list = new List<Product>();
            try
            {
                reader = sqlCommand.ExecuteReader();
                string id = "";
                while (reader.Read())
                {
                    if (id != reader["p_id"].ToString())
                    {
                        id = reader["p_id"].ToString();
                        QuickFunction(DataCollect());
                    }
                }
            }
            catch (SqlException)
            {

            }
            conn.Close();
        }
        protected void SearchSubmit_Click(object sender, EventArgs e)
        {
            stressTable.Rows.Clear();
            list.FindAll(x => x.p_name.ToLower().Contains(SearchInput.Text.ToLower()))
                .ForEach(p => QuickFunction(p));
        }
        protected void PriceRange_Click(object sender, EventArgs e)
        {
            stressTable.Rows.Clear();
            if (DropDownCategory.SelectedItem.Text != "All")
                PriceCategory();
            else
            {
                list.FindAll(x => (x.u_price >= float.Parse(MinPrice.Text) && x.u_price <= float.Parse(MaxPrice.Text)))
                    .ForEach(p => QuickFunction(p));
            }
        }

        protected void SortBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            stressTable.Rows.Clear();
            switch (SortBy.SelectedIndex)
            {
                case 0:
                    break;
                case 1:
                    list.Sort((x, y) => x.p_name.CompareTo(y.p_name));
                    break;
                case 2:
                    list.Sort((x, y) => y.p_id.CompareTo(x.p_id));
                    break;
                case 3:
                    list.Sort((x, y) => x.u_price.CompareTo(y.u_price));
                    break;
            }
            if (DropDownCategory.SelectedItem.Text != "All")
                PriceCategory();
            else
            {
                if (MinPrice.Text != "" || MaxPrice.Text != "")
                {
                    if (MinPrice.Text != "" && MaxPrice.Text != "")
                        list.FindAll(t => (t.u_price >= float.Parse(MinPrice.Text) &&
                            t.u_price <= float.Parse(MaxPrice.Text)))
                            .ForEach(t => QuickFunction(t));
                    else
                    {
                        if (MinPrice.Text != "")
                            list.FindAll(t => (t.u_price >= float.Parse(MinPrice.Text)))
                                .ForEach(t => QuickFunction(t));
                        else
                            list.FindAll(t => (t.u_price <= float.Parse(MaxPrice.Text)))
                                .ForEach(t => QuickFunction(t));
                    }
                }
                else
                    list.ForEach(x => QuickFunction(x));
            }
        }

        void PriceCategory()
        {
            if (MinPrice.Text == "" && MaxPrice.Text == "")
                list.FindAll(t => t.category.Equals(DropDownCategory.SelectedItem.Text))
                    .ForEach(t => QuickFunction(t));
            else
            {
                if (MinPrice.Text != "" && MaxPrice.Text != "")
                    list.FindAll(t => (t.u_price >= float.Parse(MinPrice.Text) && t.u_price <= float.Parse(MaxPrice.Text)))
                        .FindAll(t => t.category.Equals(DropDownCategory.SelectedItem.Text))
                        .ForEach(t => QuickFunction(t));
                else
                {
                    if (MinPrice.Text != "")
                        list.FindAll(t => (t.u_price >= float.Parse(MinPrice.Text)))
                            .FindAll(t => t.category.Equals(DropDownCategory.SelectedItem.Text))
                            .ForEach(t => QuickFunction(t));
                    else
                        list.FindAll(t => (t.u_price <= float.Parse(MaxPrice.Text)))
                            .FindAll(t => t.category.Equals(DropDownCategory.SelectedItem.Text))
                            .ForEach(t => QuickFunction(t));
                }
            }
        }
        protected void Categorize(object sender, EventArgs e)
        {
            if (DropDownCategory.SelectedIndex != 0)
                sqlCommand = new SqlCommand("select * from product inner join proimage on product.p_id=proimage.p_id where product.category= '" + DropDownCategory.SelectedItem.Text + "'", conn);
            else
                sqlCommand = new SqlCommand("select * from product inner join proimage on product.p_id=proimage.p_id", conn);
            conn.Open();
            stressTable.Rows.Clear();
            try
            {
                reader = sqlCommand.ExecuteReader();
                string id = "";
                List<Product> products = new List<Product>();
                while (reader.Read())
                {
                    if (id != reader["p_id"].ToString())
                    {
                        products.Add(DataCollect());
                        id = reader["p_id"].ToString();
                    }
                }
                conn.Close();
                switch (SortBy.SelectedIndex)
                {
                    case 1:
                        products.Sort((x, y) => x.p_name.CompareTo(y.p_name));
                        break;
                    case 2:
                        products.Sort((x, y) => y.p_id.CompareTo(x.p_id));
                        break;
                    case 3:
                        products.Sort((x, y) => x.u_price.CompareTo(y.u_price));
                        break;
                }
                products.ForEach(x => QuickFunction(x));
            }
            catch (Exception x)
            {
                Session["LastError"] = x;
            }
        }
        Product DataCollect()
        {
            Product product = new Product
            {
                p_id = reader["p_id"].ToString(),
                p_url = reader["p_url"].ToString(),
                p_details = reader["p_details"].ToString(),
                p_name = reader["p_name"].ToString(),
                u_price = float.Parse(reader["u_price"].ToString()),
                quantity = int.Parse(reader["quantity"].ToString()),
                p_image = (byte[])(reader["p_image"]),
                category = reader["category"].ToString()
            };
            list.Add(product);

            return product;
        }
        void QuickFunction(Product p)
        {
            int width = (Request.Browser.ScreenPixelsWidth) * 2 - 100;
            
            if (width <= 700)
            {
                if (!ProductTable.CssClass.Contains("-fluid"))
                    ProductTable.CssClass += "-fluid";
            }
            /*    else
                {
                    if (ProductTable.CssClass.Contains("container"))
                        ProductTable.CssClass.Remove(ProductTable.CssClass.IndexOf("container"), "container".Length - 1);
                }*/

            TableRow tableRow = new TableRow();
            TableCell tableCell = new TableCell();
            Image image = new Image
            {
                ImageUrl = "data:image/jpeg;base64," + Convert.ToBase64String(p.p_image),
                CssClass = "img-fluid",
            };
            image.Style.Add("max-height", "40vh");
            image.Style.Add("max-width", "40vw");
            tableRow.HorizontalAlign = HorizontalAlign.Justify;
            tableRow.BorderStyle = BorderStyle.Solid;
            tableRow.BorderWidth = Unit.Pixel(3);
            image.Visible = true;
            DynamicHyperLink hyperLink = new DynamicHyperLink
            {
                NavigateUrl = p.p_url,
                Visible = true,
                CssClass = "nav-link active",
                ForeColor = System.Drawing.Color.WhiteSmoke
            };
            hyperLink.Controls.Add(image);
            if (width <= 700)
            {
                hyperLink.Controls.Add(new LiteralControl("<br /> Name: " + p.p_name + "<br />"));
                hyperLink.Controls.Add(new LiteralControl("ID: " + p.p_id + "<br />"));
                hyperLink.Controls.Add(new LiteralControl("Category: " + p.category + "<br />"));
                hyperLink.Controls.Add(new LiteralControl("Unit Price: $" + p.u_price + "<br />"));
                hyperLink.Controls.Add(new LiteralControl("Details: " + ((p.p_details.Length > 40) ? p.p_details.Substring(0, 40) + "..." : p.p_details)));
                tableCell.Controls.Add(hyperLink);
                tableCell.VerticalAlign = VerticalAlign.Middle;
                tableCell.HorizontalAlign = HorizontalAlign.Center;
                tableRow.Visible = true;
                tableRow.Cells.Add(tableCell);
                stressTable.Rows.Add(tableRow);
                ProductFilter.RowSpan++;
            }
            else
            {
                tableCell.Controls.Add(hyperLink);
                tableRow.Visible = true;
                tableRow.Cells.Add(tableCell);
                stressTable.Rows.Add(tableRow);
                DynamicHyperLink hyperLink2 = new DynamicHyperLink();
                TableCell tableCell2 = new TableCell();
                tableCell.Visible = true;
                hyperLink2.Visible = true;
                hyperLink2.NavigateUrl = p.p_url;
                hyperLink2.ForeColor = System.Drawing.Color.WhiteSmoke;
                hyperLink2.CssClass = "nav-link active";
                ProductFilter.RowSpan++;
                hyperLink2.Controls.Add(new LiteralControl("Name: " + p.p_name + "<br />"));
                hyperLink2.Controls.Add(new LiteralControl("ID: " + p.p_id + "<br />"));
                hyperLink2.Controls.Add(new LiteralControl("Category: " + p.category + "<br />"));
                hyperLink2.Controls.Add(new LiteralControl("Unit Price: $" + p.u_price + "<br />"));
                hyperLink2.Controls.Add(new LiteralControl("Details: " + ((p.p_details.Length>40)?p.p_details.Substring(0, 40) + "...": p.p_details)));
                tableCell2.Controls.Add(hyperLink2);
                tableCell2.RowSpan = 1;
                tableCell2.HorizontalAlign = HorizontalAlign.Left;
                tableCell2.Visible = true;
                stressTable.Rows[stressTable.Rows.Count - 1].Cells.Add(tableCell2);
                ProductFilter.RowSpan++;
            }
        }
    }
}