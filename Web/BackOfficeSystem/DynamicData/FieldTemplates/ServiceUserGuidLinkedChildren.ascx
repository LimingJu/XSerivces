<%@ Control Language="C#" CodeBehind="ServiceUserGuidLinkedChildren.ascx.cs" Inherits="BackOfficeSystem.DynamicData.FieldTemplates.ServiceUserGuidLinkedChildren" %>
<asp:Panel ID="Wrapper" runat="server">
    <asp:Repeater ID="Repeater1" runat="server">
        <ItemTemplate>
            <asp:DynamicHyperLink runat="server"
                OnDataBinding="DynamicHyperLink_DataBinding"></asp:DynamicHyperLink>
        </ItemTemplate>
    </asp:Repeater>
    <asp:HyperLink ID="HyperLink1" runat="server" Text="None" Visible="False"
        Font-Italic="True"
        NavigateUrl="<%# GetChildrenPath() %>" />
</asp:Panel>
