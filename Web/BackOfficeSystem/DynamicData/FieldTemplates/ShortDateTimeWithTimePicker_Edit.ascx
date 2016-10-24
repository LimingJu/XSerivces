<%@ Control Language="C#" CodeBehind="ShortDateTimeWithTimePicker_Edit.ascx.cs" Inherits="BackOfficeSystem.DynamicData.FieldTemplates.ShortDateTimeWithTimePicker_EditField" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="TimePicker" Namespace="MKB.TimePicker" TagPrefix="cc1" %>

<asp:TextBox ID="TextBox1" runat="server"  Columns="20"></asp:TextBox>
<cc1:TimeSelector ID="TimeSelector1" runat="server" DisplaySeconds="false" Visible="True">
</cc1:TimeSelector>
<%--<input type="text" name="TextBox2" id="TextBox2" value="" />--%>
<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" CssClass="DDControl DDValidator" ControlToValidate="TextBox1" Display="Static" Enabled="false" />
<asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator1" CssClass="DDControl DDValidator" ControlToValidate="TextBox1" Display="Static" Enabled="false" />
<asp:DynamicValidator runat="server" ID="DynamicValidator1" CssClass="DDControl DDValidator" ControlToValidate="TextBox1" Display="Static" />
<asp:CustomValidator runat="server" ID="DateValidator" CssClass="DDControl DDValidator" ControlToValidate="TextBox1" Display="Static" EnableClientScript="false" Enabled="false" OnServerValidate="DateValidator_ServerValidate" />
<ajaxToolkit:CalendarExtender
    ID="Calendar"
    TargetControlID="TextBox1" 
    Format="yyyy-MM-dd"
    runat="server" />
<%--<link href="../../Scripts/jquery-ui-timepicker-addon.css" rel="stylesheet" type="text/css" />

<script type="text/javascript" src="../../Scripts/jquery-ui-timepicker-addon.js"></script>
<script type="text/javascript">
    $('#TextBox2').datetimepicker();
</script>--%>
