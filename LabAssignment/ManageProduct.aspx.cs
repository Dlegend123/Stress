using LabAssignment.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;

using System.Configuration;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace LabAssignment
{
    public partial class ManageProduct : System.Web.UI.Page
    {
        Table ManageTable;
        Table AddTable;
        Button ProductAdd;
        Table SearchTable;
        TableCell HouseTable;
        private SqlCommand sqlCommand;
        private SqlConnection conn;
        private SqlDataReader reader;
        private BinaryReader b;
        private byte[] binData;
        string key;
        Product temp;
        FileUpload ProductVid;
        TextBox ProductID;
        TextBox ProductName;
        TextBox ProductDetail;
        TextBox ProductCat;
        TextBox ProductPrice;
        FileUpload ProductImage1;
        FileUpload ProductImage2;
        FileUpload ProductImage3;
        TextBox ProductDesk;
        TextBox ProductMob;
        TextBox ProductQuantity;

        TextBox SearchBox;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Request.IsSecureConnection)
            {
                string url = ConfigurationManager.AppSettings["SecurePath"] + "ManageProduct.aspx";
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

            AdminHouse.Controls.Add(new LiteralControl("<br/> <br/>"));
            LargeTable();
            EditTable();
            SearchTable.Visible = false;
            XAddTable();
        }
        void LargeTable()
        {
            ManageTable = new Table
            {
                CssClass = "table table-dark table-striped table-bordered container-fluid",
                BorderWidth = Unit.Pixel(3),
                BorderStyle = BorderStyle.Solid,
                BorderColor = System.Drawing.Color.WhiteSmoke
            };
            ManageTable.Style.Add("max-width", "fit-content");
            ManageTable.Style.Add("max-height", "fit-content");
            TableRow row = new TableRow
            {
                HorizontalAlign = HorizontalAlign.Center
            };
            TableHeaderCell headerCell = new TableHeaderCell
            {
                ColumnSpan = 2,
                HorizontalAlign = HorizontalAlign.Center
            };
            Label Heading = new Label
            {
                Text = "Product",
                BorderStyle = BorderStyle.None,
                BorderWidth = Unit.Empty,
            };
            Heading.Font.Bold = true;
            Heading.Font.Size = FontUnit.Larger;
            headerCell.Controls.Add(Heading);
            row.Cells.Add(headerCell);
            ManageTable.Rows.Add(row);

            DropDownList dropDown = new DropDownList
            {
                CssClass = "btn btn-warning dropdown-toggle"
            };
            dropDown.SelectedIndexChanged += new EventHandler(DropDownIndex_Changed);
            while (dropDown.Items.Count < 2)
                dropDown.Items.Add(new ListItem());
            dropDown.Items[0].Selected = true;
            dropDown.Items[0].Text = "Add";
            dropDown.Items[1].Text = "Edit";

            dropDown.Visible = true;
            dropDown.AutoPostBack = true;
            TableCell tableCell = new TableCell
            {
                HorizontalAlign = HorizontalAlign.Right,
                ColumnSpan = 2
            };
            tableCell.Controls.Add(dropDown);
            TableRow row1 = new TableRow();
            row1.Cells.Add(tableCell);
            row1.Visible = true;
            ManageTable.Rows.Add(row1);
            HouseTable = new TableCell
            {
                Visible = true,
                ColumnSpan = 2
            };
            TableRow row2 = new TableRow();
            row2.Cells.Add(HouseTable);
            ManageTable.Rows.Add(row2);
            AdminHouse.Controls.Add(ManageTable);
            AdminHouse.Controls.Add(new LiteralControl("<br/>"));

        }
        void XAddTable()
        {
            AddTable = new Table
            {
                ID = "AddTable",
                CssClass = "table table-dark table-striped table-bordered container-fluid"
            };
            TableRow row = new TableHeaderRow
            {
                Visible = true
            };
            TableCell cell = new TableCell
            {
                Visible = true,
                HorizontalAlign = HorizontalAlign.Left
            };
            Label idLabel = new Label
            {
                Text = "ID",
                BorderStyle = BorderStyle.None,
                Visible = true
            };
            idLabel.Font.Bold = true;
            idLabel.Font.Size = FontUnit.Medium;

            cell.Controls.Add(idLabel);
            cell.Controls.Add(new LiteralControl("<br/>"));
            ProductID = new TextBox
            {
                ID = "ProductID",
                Visible = true,
            };
            cell.Controls.Add(ProductID);
            TableCell tableCell = new TableCell
            {
                Visible = true,
                HorizontalAlign = HorizontalAlign.Left
            };
            Label NameLabel = new Label
            {
                Text = "Name",
                BorderStyle = BorderStyle.None,
                Visible = true,
            };
            NameLabel.Font.Bold = true;
            NameLabel.Font.Size = FontUnit.Medium;
            tableCell.Controls.Add(NameLabel);
            ProductName = new TextBox
            {
                ID = "ProductName",
                Visible = true,
            };
            tableCell.Controls.Add(new LiteralControl("<br/>"));
            tableCell.Controls.Add(ProductName);
            row.Cells.Add(tableCell);
            row.Cells.Add(cell);
            AddTable.Rows.Add(row);
            TableRow row1 = new TableRow();
            TableCell tableCell1 = new TableCell
            {
                HorizontalAlign = HorizontalAlign.Left
            };
            Label CatLabel = new Label
            {
                Text = "Category",
                BorderStyle = BorderStyle.None,
                Visible = true
            };
            CatLabel.Font.Bold = true;
            CatLabel.Font.Size = FontUnit.Medium;
            tableCell1.Controls.Add(CatLabel);
            tableCell1.Visible = true;
            ProductCat = new TextBox
            {
                ID = "Category",
                Visible = true
            };
            tableCell1.Controls.Add(new LiteralControl("<br/>"));
            tableCell1.Controls.Add(ProductCat);
            row1.Cells.Add(tableCell1);
            row1.Visible = true;
            TableCell tableCell2 = new TableCell
            {
                Visible = true,
                HorizontalAlign = HorizontalAlign.Left
            };
            Label PriceLabel = new Label
            {
                Text = "Price",
                BorderStyle = BorderStyle.None,
                Visible = true
            };
            PriceLabel.Font.Bold = true;
            PriceLabel.Font.Size = FontUnit.Medium;
            tableCell2.Controls.Add(PriceLabel);
            ProductPrice = new TextBox
            {
                ID = "Price",
                Visible = true
            };
            tableCell2.Controls.Add(new LiteralControl("<br/>"));
            tableCell2.Controls.Add(ProductPrice);
            row1.Cells.Add(tableCell2);
            AddTable.Rows.Add(row1);
            TableRow row2 = new TableRow
            {
                Visible = true
            };
            TableCell tableCell3 = new TableCell
            {
                ColumnSpan = 2,
                HorizontalAlign = HorizontalAlign.Left,
                Visible = true
            };
            Label label = new Label
            {
                Text = "Details"
            };
            label.Font.Bold = true;
            label.Font.Size = FontUnit.Medium;
            ProductDetail = new TextBox
            {
                ID = "Details",
                Visible = true,
                CssClass = "container-fluid"
            };

            tableCell3.Controls.Add(label);
            tableCell3.Controls.Add(new LiteralControl("<br/>"));
            tableCell3.Controls.Add(ProductDetail);
            row2.Cells.Add(tableCell3);
            AddTable.Rows.Add(row2);
            TableRow row3 = new TableRow
            {
                Visible = true
            };
            TableCell tableCell4 = new TableCell
            {
                ColumnSpan = 2,
                HorizontalAlign = HorizontalAlign.Left,
                Visible = true
            };
            TableCell tableCell5 = new TableCell
            {
                ColumnSpan = 2,
                HorizontalAlign = HorizontalAlign.Left,
                Visible = true
            };
            Label label1 = new Label()
            {
                Text = "Video"
            };
            label1.Font.Bold = true;
            label1.Font.Size = FontUnit.Medium;
            ProductVid = new FileUpload
            {
                ID = "Video",
                Visible = true
            };
            tableCell4.Controls.Add(label1);
            tableCell4.Controls.Add(new LiteralControl("<br/>"));
            tableCell4.Controls.Add(ProductVid);
            row3.Cells.Add(tableCell4);
            AddTable.Rows.Add(row3);
            TableRow row4 = new TableRow
            {
                Visible = true
            };
            Label QLabel = new Label
            {
                Text = "Quantity",
                Visible = true
            };
            QLabel.Font.Bold = true;
            QLabel.Font.Size = FontUnit.Medium;
            tableCell5.Controls.Add(QLabel);
            tableCell5.Controls.Add(new LiteralControl("<br/>"));
            ProductQuantity = new TextBox
            {
                ID = "Quantity"
            };
            tableCell5.Controls.Add(ProductQuantity);
            row4.Cells.Add(tableCell5);
            AddTable.Rows.Add(row4);

            TableRow row5 = new TableRow
            {
                Visible = true
            };
            TableCell tableCell6 = new TableCell
            {
                Visible = true,
                ColumnSpan = 2,
                HorizontalAlign = HorizontalAlign.Left
            };
            Label ImgLabel = new Label
            {
                Text = "Images",
                Visible = true
            };
            ImgLabel.Font.Bold = true;
            ImgLabel.Font.Size = FontUnit.Medium;
            tableCell6.Controls.Add(ImgLabel);
            tableCell6.Controls.Add(new LiteralControl("<br/>"));
            ProductImage1 = new FileUpload
            {
                ID = "ProductImage1",
                Visible = true
            };
            ProductImage2 = new FileUpload
            {
                ID = "ProductImage2",
                Visible = true
            };
            ProductImage3 = new FileUpload
            {
                ID = "ProductImage3",
                Visible = true
            };
            tableCell6.Controls.Add(ProductImage1);
            tableCell6.Controls.Add(new LiteralControl("<br/>"));
            tableCell6.Controls.Add(ProductImage2);
            tableCell6.Controls.Add(new LiteralControl("<br/>"));
            tableCell6.Controls.Add(ProductImage3);
            row5.Cells.Add(tableCell6);
            AddTable.Rows.Add(row5);
            TableRow row6 = new TableRow
            {
                Visible = true
            };
            TableCell tableCell7 = new TableCell
            {
                Visible = true,
                HorizontalAlign = HorizontalAlign.Left
            };
            Label PageD = new Label
            {
                Visible = true,
                Text = "Page(Desktop)"
            };
            tableCell7.Controls.Add(PageD);
            tableCell7.Controls.Add(new LiteralControl("<br/>"));
            ProductDesk = new TextBox
            {
                Visible = true,
                ID = "ProductDesk"
            };

            tableCell7.Controls.Add(ProductDesk);
            row6.Cells.Add(tableCell7);
            TableCell tableCell8 = new TableCell
            {
                Visible = true,
                HorizontalAlign = HorizontalAlign.Left
            };
            Label PageM = new Label
            {
                Text = "Page(Mobile)",
                Visible = true
            };
            tableCell8.Controls.Add(PageM);
            tableCell8.Controls.Add(new LiteralControl("<br/>"));
            ProductMob = new TextBox
            {
                ID = "ProductMob"
            };
            tableCell8.Controls.Add(ProductMob);
            row6.Cells.Add(tableCell8);
            AddTable.Rows.Add(row6);
            TableFooterRow row7 = new TableFooterRow
            {
                Visible = true
            };
            TableCell tableCell9 = new TableCell
            {
                Visible = true,
                ColumnSpan = 2,
                HorizontalAlign = HorizontalAlign.Left
            };
            ProductAdd = new Button
            {
                CssClass = "btn btn-warning",
                Text = "Add",
                ID = "ProductAdd",
                Visible = true
            };
            ProductAdd.Click += new EventHandler(ProductAdd_Click);
            tableCell9.Controls.Add(ProductAdd);
            row7.Cells.Add(tableCell9);
            AddTable.Rows.Add(row7);
            AddTable.Visible = true;
            HouseTable.Controls.Add(AddTable);
        }
        protected void DropDownIndex_Changed(object sender, EventArgs e)
        {
            DropDownList dropDown = sender as DropDownList;

            switch (dropDown.SelectedIndex)
            {
                case 0:
                    {
                        try
                        {
                            if (SearchTable.Visible)
                                SearchTable.Visible = false;
                            if (!AddTable.Visible)
                                AddTable.Visible = true;
                            ProductAdd.Text = "Add";
                                ProductAdd.Click -= ProductUpdate_Click;
                            ProductAdd.Click += new EventHandler(ProductAdd_Click);
                            if (!ProductVid.Parent.Parent.Visible)
                                ProductVid.Parent.Parent.Visible = true;
                            if (!ProductImage1.Parent.Parent.Visible)
                                ProductImage1.Parent.Parent.Visible = true;
                        }
                        catch (Exception)
                        {

                        }
                    }
                    break;
                case 1:
                    {
                        try
                        {
                            if (AddTable.Visible)
                                AddTable.Visible = false;
                            if (!SearchTable.Visible)
                                SearchTable.Visible = true;
                            ProductAdd.Text = "Update";
                            ProductAdd.Click -= ProductAdd_Click;
                            ProductAdd.Click += new EventHandler(ProductUpdate_Click);
                            if (ProductVid.Parent.Parent.Visible)
                                ProductVid.Parent.Parent.Visible = false;
                            if (ProductImage1.Parent.Parent.Visible)
                                ProductImage1.Parent.Parent.Visible = false;
                        }
                        catch (Exception)
                        {

                        }
                    }
                    break;
            }
        }
        protected void Search(object sender, EventArgs e)
        {
            sqlCommand = new SqlCommand("select * from product where p_id = '" + SearchBox.Text + "'", conn);
            try
            {
                conn.Open();
                reader = sqlCommand.ExecuteReader();
                TableRow tableRow = new TableRow();
                TableCell tableCell = new TableCell();
                tableRow.HorizontalAlign = HorizontalAlign.Center;
                tableRow.BorderStyle = BorderStyle.Solid;
                Table table = new Table
                {
                    ID = "temp",
                    CssClass = "table table-dark table-striped table-bordered container-fluid"
                };
                tableRow.BorderWidth = Unit.Pixel(3);
                if (!reader.HasRows)
                {
                    if (AddTable.Visible)
                        AddTable.Visible = false;
                    tableCell.Controls.Add(new LiteralControl("Product not found"));
                    tableRow.Cells.Add(tableCell);
                    table.Rows.Add(tableRow);
                    HouseTable.Controls.Add(table);
                }
                else
                {
                    reader.Read();
                    DataCollect();
                    if (!AddTable.Visible)
                        AddTable.Visible = true;
                    Button Removal = new Button
                    {
                        CssClass = "btn btn-warning",
                        Text = "Remove Product",
                        ID = "Removal",
                        Visible = true
                    };
                    tableCell.Controls.Add(Removal);
                    tableRow.Cells.Add(tableCell);
                    table.Rows.Add(tableRow);
                    SearchTable.Rows.Add(tableRow);
                    Removal.Click += new EventHandler(RemoveProduct_Click);
                }
                conn.Close();
            }
            catch (Exception x)
            {
                Session["LastError"] = x;
            }
            SearchTable.Visible = true;
        }

        private void RemoveProduct_Click(object sender, EventArgs e)
        {
            key = SearchBox.Text;
            sqlCommand = new SqlCommand("Delete  from proimage where p_id = " + key , conn);
            try
            {
                conn.Open();
                sqlCommand.ExecuteNonQuery();
                sqlCommand = new SqlCommand("Delete  from product where p_id = " + key, conn);
                sqlCommand.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception x)
            {
                Session["LastError"] = x;
            }
        }

        void EditTable()
        {
            TableHeaderRow tableHeader = new TableHeaderRow();
            Button button = new Button
            {
                CssClass = "btn btn-outline-warning",
                Text = "Search",
            };
            button.Click += new EventHandler(Search);
            TableHeaderCell tableCell = new TableHeaderCell
            {
                ColumnSpan = 2,
                HorizontalAlign = HorizontalAlign.Center
            };
            SearchBox = new TextBox
            {
                ID = "SearchBox",
                ToolTip = "Product ID",
                TextMode = TextBoxMode.Number
            };
            SearchBox.Attributes.Add("placeholder", "Enter Product ID");
            tableCell.Controls.Add(SearchBox);
            tableCell.Controls.Add(new LiteralControl("&nbsp;"));
            tableCell.Controls.Add(button);
            tableHeader.Cells.Add(tableCell);

            SearchTable = new Table
            {
                ID = "SearchTable",
                CssClass = "table table-dark table-striped table-bordered container-fluid",
                Visible = true
            };
            SearchTable.Controls.Add(tableHeader);
            HouseTable.Controls.Add(SearchTable);
        }
        protected void ProductAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (ProductVid.HasFile)
                {
                    sqlCommand = new SqlCommand("INSERT INTO Product(p_id,p_name,p_details,category,u_price,quantity,p_video,p_url,p_urlM) Values (@p_id,@p_name,@p_details,@category,@u_price,@quantity,@p_video,@p_url,@p_urlM)", conn);
                    b = new BinaryReader(ProductVid.PostedFile.InputStream);
                    binData = b.ReadBytes(ProductVid.PostedFile.ContentLength);
                    sqlCommand.Parameters.AddWithValue("@p_video", binData);
                }
                else
                {
                    sqlCommand = new SqlCommand("INSERT INTO Product(p_id,p_name,p_details,category,u_price,quantity,p_url,p_urlM) Values (@p_id,@p_name,@p_details,@category,@u_price,@quantity,@p_url,@p_urlM)", conn);
                }
                sqlCommand.Parameters.AddWithValue("@p_id", Convert.ToInt32(ProductID.Text));
                sqlCommand.Parameters.AddWithValue("@p_name", ProductName.Text);
                sqlCommand.Parameters.AddWithValue("@p_details", ProductDetail.Text);
                sqlCommand.Parameters.AddWithValue("@category", ProductCat.Text);
                sqlCommand.Parameters.AddWithValue("@u_price", SqlMoney.Parse( ProductPrice.Text));

                sqlCommand.Parameters.AddWithValue("@quantity", Convert.ToInt32(ProductQuantity.Text));
                sqlCommand.Parameters.AddWithValue("@p_url", "~/" + ProductDesk.Text);
                sqlCommand.Parameters.AddWithValue("@p_urlM", "~/" + ProductMob.Text);
                conn.Open();
                sqlCommand.ExecuteNonQuery();
                sqlCommand = new SqlCommand("INSERT INTO ProImage(p_id,p_image) Values (@p_id,@p_image)", conn);
                sqlCommand.Parameters.AddWithValue("@p_id", Convert.ToInt32(ProductID.Text));
                b = new BinaryReader(ProductImage1.PostedFile.InputStream);
                binData = b.ReadBytes(ProductImage1.PostedFile.ContentLength);
                //sqlCommand.Parameters.AddWithValue("@imageType", ProductImage1.PostedFile.ContentType);
                sqlCommand.Parameters.AddWithValue("@p_image", binData);
                sqlCommand.ExecuteNonQuery();
                sqlCommand.Parameters.Clear();
                b = new BinaryReader(ProductImage2.PostedFile.InputStream);
                binData = b.ReadBytes(ProductImage2.PostedFile.ContentLength);
                sqlCommand.Parameters.AddWithValue("@p_id", Convert.ToInt32(ProductID.Text));
                // sqlCommand.Parameters.AddWithValue("@imageType", ProductImage2.PostedFile.ContentType);
                sqlCommand.Parameters.AddWithValue("@p_image", binData);
                sqlCommand.ExecuteNonQuery();

                if (ProductImage3.HasFile)
                {
                    sqlCommand.Parameters.Clear();
                    b = new BinaryReader(ProductImage3.PostedFile.InputStream);
                    binData = b.ReadBytes(ProductImage3.PostedFile.ContentLength);
                    sqlCommand.Parameters.AddWithValue("@p_id", Convert.ToInt32(ProductID.Text));
                    // sqlCommand.Parameters.AddWithValue("@imageType", ProductImage3.PostedFile.ContentType);
                    sqlCommand.Parameters.AddWithValue("@p_image", binData);
                    sqlCommand.ExecuteNonQuery();
                }
                conn.Close();
            }
            catch (Exception x)
            {
                Session["LastError"] = x;
            }
        }
        protected void ProductUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (ProductVid.HasFile)
                {
                    sqlCommand = new SqlCommand("Update Product SET p_id = @p_id, p_name = @p_name, p_details = @p_details, category = @category, u_price = @u_price, quantity = @quantity, p_video = @p_video, p_url = @p_url, p_urlM = @p_urlM where p_id = '" + temp.p_id + "'", conn);
                    b = new BinaryReader(ProductVid.PostedFile.InputStream);
                    binData = b.ReadBytes(ProductVid.PostedFile.ContentLength);
                    sqlCommand.Parameters.AddWithValue("@p_video", binData);
                }
                else
                {
                    sqlCommand = new SqlCommand("Update Product SET p_id = @p_id, p_name = @p_name, p_details = @p_details, category = @category, u_price = @u_price, quantity = @quantity, p_url = @p_url, p_urlM = @p_urlM where p_id = '" + temp.p_id + "'", conn);
                }
                sqlCommand.Parameters.AddWithValue("@p_id", Convert.ToInt32(ProductID.Text));
                sqlCommand.Parameters.AddWithValue("@p_name", ProductName.Text);
                sqlCommand.Parameters.AddWithValue("@p_details", ProductDetail.Text);
                sqlCommand.Parameters.AddWithValue("@category", ProductCat.Text);
                sqlCommand.Parameters.AddWithValue("@u_price",SqlMoney.Parse( ProductPrice.Text));

                sqlCommand.Parameters.AddWithValue("@quantity", Convert.ToInt32(ProductQuantity.Text));
                sqlCommand.Parameters.AddWithValue("@p_url", "~/" + ProductDesk.Text);
                sqlCommand.Parameters.AddWithValue("@p_urlM", "~/" + ProductMob.Text);
                conn.Open();
                sqlCommand.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception x)
            {
                Session["LastError"] = x;
            }
        }
        void DataCollect()
        {
            temp = new Product
            {
                p_id = reader["p_id"].ToString(),
                p_url = reader["p_url"].ToString(),
                p_details = reader["p_details"].ToString(),
                p_name = reader["p_name"].ToString(),
                u_price = float.Parse(reader["u_price"].ToString()),
                quantity = int.Parse(reader["quantity"].ToString()),
                //    p_image = (byte[])(reader["p_video"]),
                category = reader["category"].ToString(),
                p_urlM = reader["p_urlM"].ToString()
            };
            key = ProductID.Text = reader["p_id"].ToString();
            ProductDesk.Text = reader["p_url"].ToString();
            ProductDetail.Text = reader["p_details"].ToString();
            ProductName.Text = reader["p_name"].ToString();
            ProductPrice.Text = float.Parse(reader["u_price"].ToString()).ToString();
            ProductQuantity.Text = reader["quantity"].ToString();
            ProductCat.Text = reader["category"].ToString();
            ProductMob.Text = reader["p_urlM"].ToString();
        }
    }
}