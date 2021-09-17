using System;

using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.DynamicData;

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
            if (Session["Account"] == "Admin")
                Page.Master.FindControl("AdminFunc").Visible = true;

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
                int count = 0;
                string id = "";
                while (reader.Read())
                {
                    if (id != reader["p_id"].ToString())
                    {
                        id = reader["p_id"].ToString();
                        QuickFunction(DataCollect(), count++);
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
            int count = 0;
            list.FindAll(x => x.p_name.ToLower().Contains(SearchInput.Text.ToLower()))
                .ForEach(p => QuickFunction(p, count++));
        }
        protected void PriceRange_Click(object sender, EventArgs e)
        {
            stressTable.Rows.Clear();
            int count = 0;
            if (DropDownCategory.SelectedItem != null && DropDownCategory.SelectedIndex != 0)
                list.FindAll(t => (t.u_price >= float.Parse(MinPrice.Text) && t.u_price <= float.Parse(MaxPrice.Text)))
                    .FindAll(x => x.category.Equals(DropDownCategory.SelectedItem.Text))
                    .ForEach(p => QuickFunction(p, count++));
            else
                list.FindAll(x => (x.u_price >= float.Parse(MinPrice.Text) && x.u_price <= float.Parse(MaxPrice.Text)))
                    .ForEach(p => QuickFunction(p, count++));
        }
        
        protected void SortBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SortBy.SelectedItem.Text != "")
            {
                stressTable.Rows.Clear();
                switch (SortBy.SelectedIndex)
                {
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
                if (DropDownCategory.SelectedItem.Text!="All")
                    PriceCategory();
                else
                {
                    int count = 0;
                    if (MinPrice.Text != "" || MaxPrice.Text != "")
                    {
                        if (MinPrice.Text != "" && MaxPrice.Text != "")
                            list.FindAll(t => (t.u_price >= float.Parse(MinPrice.Text) &&
                                t.u_price <= float.Parse(MaxPrice.Text)))
                                .ForEach(t => QuickFunction(t, count++));
                        else
                        {
                            if (MinPrice.Text != "")
                                list.FindAll(t => (t.u_price >= float.Parse(MinPrice.Text)))
                                    .ForEach(t => QuickFunction(t, count++));
                            else
                                list.FindAll(t => (t.u_price <= float.Parse(MaxPrice.Text)))
                                    .ForEach(t => QuickFunction(t, count++));
                        }
                    }
                    else
                        list.ForEach(x => QuickFunction(x, count++));
                }
            }
        }

        void PriceCategory()
        {
            int count = 0;
            if (MinPrice.Text == "" && MaxPrice.Text == "")
                list.FindAll(t => t.category.Equals(DropDownCategory.SelectedItem.Text))
                    .ForEach(t => QuickFunction(t, count++));
            else
            {
                if (MinPrice.Text != "" && MaxPrice.Text != "")
                    list.FindAll(t => (t.u_price >= float.Parse(MinPrice.Text) && t.u_price <= float.Parse(MaxPrice.Text)))
                        .FindAll(t => t.category.Equals(DropDownCategory.SelectedItem.Text))
                        .ForEach(t => QuickFunction(t, count++));
                else
                {
                    if (MinPrice.Text != "")
                        list.FindAll(t => (t.u_price >= float.Parse(MinPrice.Text)))
                            .FindAll(t => t.category.Equals(DropDownCategory.SelectedItem.Text))
                            .ForEach(t => QuickFunction(t, count++));
                    else
                        list.FindAll(t => (t.u_price <= float.Parse(MaxPrice.Text)))
                            .FindAll(t => t.category.Equals(DropDownCategory.SelectedItem.Text))
                            .ForEach(t => QuickFunction(t, count++));
                }
            }
        }
        protected void Categorize(object sender, EventArgs e)
        {
            if(DropDownCategory.SelectedIndex!=0)
            sqlCommand = new SqlCommand("select * from product inner join proimage on product.p_id=proimage.p_id where product.category= '" + DropDownCategory.SelectedItem.Text + "'", conn);
            else
                sqlCommand = new SqlCommand("select * from product inner join proimage on product.p_id=proimage.p_id", conn);
            conn.Open();
            stressTable.Rows.Clear();
            try
            {
                reader = sqlCommand.ExecuteReader();
                int count = 0;
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
                products.ForEach(x => QuickFunction(x, count++));
            }
            catch (SqlException)
            {

            }
        }
        Product DataCollect()
        {
            Product product = new Product();
            product.p_id = reader["p_id"].ToString();
            product.p_url = reader["p_url"].ToString();
            product.p_details = reader["p_details"].ToString();
            product.p_name = reader["p_name"].ToString();
            product.u_price = float.Parse(reader["u_price"].ToString());
            product.quantity = reader["quantity"].ToString();
            product.p_image = (byte[])(reader["p_image"]);
            product.category = reader["category"].ToString();
            list.Add(product);

            return product;
        }
        void QuickFunction(Product p, int count)
        {
            int width = (Request.Browser.ScreenPixelsWidth)*2-100;
            int height = (Request.Browser.ScreenPixelsHeight)*2-100;
            if (width <= 700)
            {
                if(!ProductTable.CssClass.Contains("-fluid"))
                    ProductTable.CssClass += "-fluid";
            }
            /*    else
                {
                    if (ProductTable.CssClass.Contains("container"))
                        ProductTable.CssClass.Remove(ProductTable.CssClass.IndexOf("container"), "container".Length - 1);
                }*/

            TableRow tableRow = new TableRow();
            TableCell tableCell = new TableCell();
            Image image = new Image();
            image.ImageUrl = "data:image/jpeg;base64," + Convert.ToBase64String(p.p_image);
            image.CssClass = "img-fluid";
            image.Width = Unit.Percentage(100);
            tableRow.HorizontalAlign = HorizontalAlign.Justify;
            tableRow.BorderStyle = BorderStyle.Solid;
            tableRow.BorderWidth = Unit.Pixel(3);
            image.Visible = true;
            DynamicHyperLink hyperLink = new DynamicHyperLink();
            hyperLink.NavigateUrl = p.p_url;
            hyperLink.Visible = true;
            hyperLink.CssClass = "nav-link active";
            hyperLink.ForeColor = System.Drawing.Color.WhiteSmoke;
            hyperLink.Controls.Add(image);
            if (width <= 700)
            {
                hyperLink.Controls.Add(new LiteralControl("<br /> Name: " + p.p_name + "<br />"));
                hyperLink.Controls.Add(new LiteralControl("ID: " + p.p_id + "<br />"));
                hyperLink.Controls.Add(new LiteralControl("Category: " + p.category + "<br />"));
                hyperLink.Controls.Add(new LiteralControl("Unit Price: " + p.u_price + "<br />"));
                hyperLink.Controls.Add(new LiteralControl("Details: " + p.p_details));
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
                hyperLink2.Controls.Add(new LiteralControl("Unit Price: " + p.u_price + "<br />"));
                hyperLink2.Controls.Add(new LiteralControl("Details: " + p.p_details));
                tableCell2.Controls.Add(hyperLink2);
                tableCell2.RowSpan = 1;
                tableCell2.HorizontalAlign = HorizontalAlign.Left;
                tableCell2.Visible = true;
                stressTable.Rows[count].Cells.Add(tableCell2);
                ProductFilter.RowSpan++;
            }
        }
    }
}
