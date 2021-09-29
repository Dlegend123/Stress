using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.DynamicData;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LabAssignment
{
    public partial class Checkout : System.Web.UI.Page
    {
        ShoppingCart shoppingCart;
        List<float> subTotals;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Request.IsSecureConnection)
            {
                string url = ConfigurationManager.AppSettings["SecurePath"] + "Checkout.aspx";
                Response.Redirect(url);
            }
            if (Session["shoppingCart"] != null)
            {
                shoppingCart = Session["shoppingCart"] as ShoppingCart;
                subTotals = Session["subTotals"] as List<float>;
                int count = 0;
                shoppingCart.products.ForEach(x => QuickFunction(x, count++));
            }
            else
            {
                
                Exception x = new Exception
                {
                    Source = "Invalid Navigation"
                };
                x.Source="Invalid Navigation";
                x.Message.Concat("Should have been navigated from cart");
                Session["LastError"] = x;
                Response.Redirect("~/CustomErrors/ErrorPage1.aspx",false);
            }
        }
        void QuickFunction(Product p, int count)
        {
            TableRow tableRow = new TableRow();
            TableCell tableCell = new TableCell();
            Image image = new Image
            {
                ImageUrl = "data:image/jpeg;base64," + Convert.ToBase64String(p.p_image),
                CssClass = "img-fluid",
            };
            image.Style.Add("max-height", "70vh");
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
            hyperLink2.Controls.Add(new LiteralControl("<br /> ID: " + p.p_id + "<br />"));
            hyperLink2.Controls.Add(new LiteralControl("Quantity: $" + p.quantity + "<br />"));
            hyperLink2.Controls.Add(new LiteralControl("Subtotal: $" + subTotals[count]));
            tableCell2.Controls.Add(hyperLink2);
            tableCell2.RowSpan = 1;
            tableCell2.HorizontalAlign = HorizontalAlign.Left;
            tableCell2.Visible = true;
            stressTable.Rows[stressTable.Rows.Count - 1].Cells.Add(tableCell2);
        }
        protected void ProcessOrder_Click(object sender, EventArgs e)
        {

        }
    }
}