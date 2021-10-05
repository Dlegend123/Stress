using LabAssignment.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace LabAssignment
{
    public partial class ManageUsers : System.Web.UI.Page
    {
        private Table ManageTable;
        private Table AddTable;
        TextBox UserName;
        TextBox UserID;
        TextBox RoleID;
        TextBox RoleName;
        TableCell HouseTable;
        protected IdentityUser customer;
        Table SearchTable;
        UserStore<IdentityUser> userStore;
        Entity entity;
        protected UserManager<IdentityUser> Customers;
        private Button UserUpdate;
        private TextBox SearchBox;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Request.IsSecureConnection)
            {
                string url = ConfigurationManager.AppSettings["SecurePath"] + "ManageUsers.aspx";
                Response.Redirect(url);
            }
            if (Session["Account"] != null)
            {
                (Page.Master.FindControl("SignInLink") as HtmlAnchor).InnerText = "Sign Out";
                if ((Session["Account"] as ApplicationUser).Roles.Any(x => x.RoleId == "Admin"))
                    Page.Master.FindControl("AdminFunc").Visible = true;
                if ((Session["Account"] as ApplicationUser).Roles.Any(x => x.RoleId == "Cust"))
                    Page.Master.FindControl("CartLink").Visible = true;
            }

            entity = new Entity();
            entity.Database.Connection.ConnectionString = ConfigurationManager
                .ConnectionStrings["LIConnectionString"].ConnectionString;

            userStore = new UserStore<IdentityUser>(entity);
            Customers = new UserManager<IdentityUser>(userStore);
            AdminHouse.Controls.Add(new LiteralControl("<br/> <br/>"));
            LargeTable();
            EditTable();
            SearchTable.Visible = true;
            XAddTable();
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
                ToolTip = "User ID",
                TextMode = TextBoxMode.Number
            };
            SearchBox.Attributes.Add("placeholder", "Enter UserID");
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
                Text = "User",
                BorderStyle = BorderStyle.None,
                BorderWidth = Unit.Empty,
            };
            Heading.Font.Bold = true;
            Heading.Font.Size = FontUnit.Larger;
            headerCell.Controls.Add(Heading);
            row.Cells.Add(headerCell);
            ManageTable.Rows.Add(row);
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
                Text = "User ID",
                BorderStyle = BorderStyle.None,
                Visible = true
            };
            idLabel.Font.Bold = true;
            idLabel.Font.Size = FontUnit.Medium;

            cell.Controls.Add(idLabel);
            cell.Controls.Add(new LiteralControl("<br/>"));
            UserID = new TextBox
            {
                ID = "UserID",
                Visible = true,
            };
            cell.Controls.Add(UserID);
            TableCell tableCell = new TableCell
            {
                Visible = true,
                HorizontalAlign = HorizontalAlign.Left
            };
            Label NameLabel = new Label
            {
                Text = "UserName",
                BorderStyle = BorderStyle.None,
                Visible = true,
            };
            NameLabel.Font.Bold = true;
            NameLabel.Font.Size = FontUnit.Medium;
            tableCell.Controls.Add(NameLabel);
            UserName = new TextBox
            {
                ID = "UserName",
                Visible = true,
            };
            UserName.Font.Size = FontUnit.Medium;
            tableCell.Controls.Add(new LiteralControl("<br/>"));
            tableCell.Controls.Add(UserName);
            row.Cells.Add(tableCell);
            row.Cells.Add(cell);
            AddTable.Rows.Add(row);
            TableRow row1 = new TableRow();
            TableCell tableCell1 = new TableCell
            {
                HorizontalAlign = HorizontalAlign.Left
            };
            Label RoleLabel = new Label
            {
                Text = "Role Name",
                BorderStyle = BorderStyle.None,
                Visible = true
            };
            RoleLabel.Font.Bold = true;
            RoleLabel.Font.Size = FontUnit.Medium;
            tableCell1.Controls.Add(RoleLabel);
            tableCell1.Visible = true;
            RoleID = new TextBox
            {
                ID = "Role ID",
                Visible = true
            };
            tableCell1.Controls.Add(new LiteralControl("<br/>"));
            tableCell1.Controls.Add(RoleID);
            row1.Cells.Add(tableCell1);
            row1.Visible = true;
            TableCell tableCell2 = new TableCell
            {
                Visible = true,
                HorizontalAlign = HorizontalAlign.Left
            };
            Label PriceLabel = new Label
            {
                Text = "Role ID",
                BorderStyle = BorderStyle.None,
                Visible = true
            };
            PriceLabel.Font.Bold = true;
            PriceLabel.Font.Size = FontUnit.Medium;
            tableCell2.Controls.Add(PriceLabel);
            RoleName = new TextBox
            {
                ID = "RoleName",
                Visible = true
            };
            tableCell2.Controls.Add(new LiteralControl("<br/>"));
            tableCell2.Controls.Add(RoleName);
            row1.Cells.Add(tableCell2);
            AddTable.Rows.Add(row1);
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
            UserUpdate = new Button
            {
                CssClass = "btn btn-warning",
                Text = "Add",
                ID = "UserUpdate",
                Visible = true
            };
            UserUpdate.Click += new EventHandler(Update_Click);
            tableCell9.Controls.Add(UserUpdate);
            row7.Cells.Add(tableCell9);
            AddTable.Rows.Add(row7);
            AddTable.Visible = false;
            HouseTable.Controls.Add(AddTable);
        }
        protected void Search(object sender, EventArgs e)
        {
            try
            {
                customer = Customers.FindById(SearchBox.Text);
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

                if (customer == null)
                {
                    if (AddTable.Visible)
                        AddTable.Visible = false;
                    tableCell.Controls.Add(new LiteralControl("User not found"));
                    tableRow.Cells.Add(tableCell);
                    table.Rows.Add(tableRow);
                    HouseTable.Controls.Add(table);
                }
                else
                {
                    UserName.Text = customer.UserName;
                    UserID.Text = customer.Id;
                    RoleID.Text = customer.Roles.First().RoleId;
                    RoleName.Text = customer.Roles.First().UserId;
                    if (!AddTable.Visible)
                        AddTable.Visible = true;
                    Button Removal = new Button
                    {
                        CssClass = "btn btn-warning",
                        Text = "Remove User",
                        ID = "Removal",
                        Visible = true
                    };
                    tableCell.Controls.Add(Removal);
                    tableRow.Cells.Add(tableCell);
                    table.Rows.Add(tableRow);
                    SearchTable.Rows.Add(tableRow);
                    Removal.Click += new EventHandler(RemoveUser_Click);
                    UserUpdate.Text = "Update";
                }
            }
            catch (Exception x)
            {
                Session["LastError"] = x;
            }
            SearchTable.Visible = true;
        }
        private void RemoveUser_Click(object sender, EventArgs e)
        {
            Customers.DeleteAsync(customer);
        }
        private void Update_Click(object sender, EventArgs e)
        {
            IdentityUser temp = customer;
            temp.Id = UserID.Text;
            temp.UserName=UserName.Text;
            temp.Roles.First().RoleId= RoleID.Text;
            temp.Roles.First().UserId= UserID.Text;
            Customers.FindByIdAsync(UserID.Text).Result.UserName = temp.UserName;
            Customers.FindByIdAsync(UserID.Text).Result.Id = temp.Id;
            Customers.FindByIdAsync(UserID.Text).Result.Roles.First().RoleId = temp.Roles.First().RoleId;
            Customers.FindByIdAsync(UserID.Text).Result.Roles.First().UserId = temp.Roles.First().UserId;
            var result=Customers.UpdateAsync(customer).Result;
            if (result.Succeeded)
                SearchBox.Text = "succeeded";
        }
    }
}