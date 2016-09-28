<%@ Page Language="C#" MasterPageFile="~/Site.master" CodeBehind="Default.aspx.cs" Inherits="BackOfficeSystem._Default" %>

<asp:Content ID="headContent" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server" />

    <h2 class="DDSubHeader">POS Items Managment</h2>
    <asp:GridView ID="PosItemsManagmentGridView" runat="server" AutoGenerateColumns="false"
        CssClass="DDGridView" RowStyle-CssClass="td" HeaderStyle-CssClass="th" CellPadding="6">
        <Columns>
            <asp:HyperLinkField HeaderText="Operation" DataTextField="DisplayText" DataNavigateUrlFields="LinkPath"></asp:HyperLinkField>
        </Columns>
        <Columns>
            <asp:HyperLinkField HeaderText="Description" DataTextField="DetailDescription" DataNavigateUrlFields="LinkPath"></asp:HyperLinkField>
        </Columns>
    </asp:GridView>
    <br />

    <h2 class="DDSubHeader">POS Staff Managment</h2>
    <asp:GridView ID="PosStaffManagmentGridView" runat="server" AutoGenerateColumns="false"
        CssClass="DDGridView" RowStyle-CssClass="td" HeaderStyle-CssClass="th" CellPadding="6">
        <Columns>
            <asp:HyperLinkField HeaderText="Operation" DataTextField="DisplayText" DataNavigateUrlFields="LinkPath"></asp:HyperLinkField>
        </Columns>
        <Columns>
            <asp:HyperLinkField HeaderText="Description" DataTextField="DetailDescription" DataNavigateUrlFields="LinkPath"></asp:HyperLinkField>
        </Columns>
    </asp:GridView>

    <h2 class="DDSubHeader">POS Function Managment</h2>
    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="false"
        CssClass="DDGridView" RowStyle-CssClass="td" HeaderStyle-CssClass="th" CellPadding="6">
        <Columns>
            <asp:TemplateField HeaderText="Table Name" SortExpression="TableName">
                <ItemTemplate>
                    <asp:DynamicHyperLink ID="HyperLink1" runat="server"><%# Eval("DisplayName") %></asp:DynamicHyperLink>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>


