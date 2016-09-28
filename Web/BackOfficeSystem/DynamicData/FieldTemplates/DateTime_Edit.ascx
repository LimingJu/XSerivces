<%@ Control Language="C#" CodeBehind="DateTime_Edit.ascx.cs" Inherits="BackOfficeSystem.DateTime_EditField" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:TextBox ID="TextBox1" runat="server" Text='<%# FieldValueEditString %>' Columns="20"></asp:TextBox>
<%--<input type="text" name="TextBox2" id="TextBox2" value="" />--%>
<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" CssClass="DDControl DDValidator" ControlToValidate="TextBox1" Display="Static" Enabled="false" />
<asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator1" CssClass="DDControl DDValidator" ControlToValidate="TextBox1" Display="Static" Enabled="false" />
<asp:DynamicValidator runat="server" ID="DynamicValidator1" CssClass="DDControl DDValidator" ControlToValidate="TextBox1" Display="Static" />
<asp:CustomValidator runat="server" ID="DateValidator" CssClass="DDControl DDValidator" ControlToValidate="TextBox1" Display="Static" EnableClientScript="false" Enabled="false" OnServerValidate="DateValidator_ServerValidate" />
<ajaxtoolkit:calendarextender
    ID="Calendar"
    TargetControlID="TextBox1"
    Format="yyyy-MM-dd HH:mm:ss"
    runat="server" />
<%--<link href="../../Scripts/jquery-ui-timepicker-addon.css" rel="stylesheet" type="text/css" />

<script type="text/javascript" src="../../Scripts/jquery-ui-timepicker-addon.js"></script>
<script type="text/javascript">
    $('#TextBox2').datetimepicker();
</script>--%>
