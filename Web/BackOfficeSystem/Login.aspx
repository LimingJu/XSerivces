<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="BackOfficeSystem.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login to BackOfficeSystem</title>
</head>
<link rel="stylesheet" type="text/css" href="Style/base.css">
<link rel="stylesheet" type="text/css" href="Style/Style.css">
<link rel="stylesheet" type="text/css" href="Style/Spiritnew/Style.css">
<body>

    <div class="logo">
        <center>
            <img src="/Images/logo_login.gif" width="200" height="80">
        </center>
    </div>
    <div class="center_div">
        <form id="form1" runat="server">
            <table cellspacing="0" cellpadding="5" border="0">
                <tr>
                    <td align="center">
                        <table class="container" style="max-width: 350px; height: 28px; width: 350px">

                            <tr>
                                <td class="container_title" colspan="2">Login</td>
                            </tr>

                            <tr>
                                <td class="container_input_title" style="width: 70px">Username</td>
                                <td class="container_top" style="width: 280px; padding-bottom: 6px; padding-top: 6px; padding-left: 6px; padding-right: 6px">
                                    <asp:TextBox runat="server" ID="UserName" Style="height: 30px; width: 98%" MaxLength="100" value="" />
                            </tr>

                            <tr>
                                <td class="container_input_title">Password</td>
                                <td id="container_body" style="border-bottom: #abb6cc 1px solid; padding-bottom: 6px; padding-top: 6px; padding-left: 6px; padding-right: 6px">
                                    <asp:TextBox runat="server" ID="Password" TextMode="Password" Style="height: 30px; width: 98%" MaxLength="100" type="password" value="" name="password" />
                            </tr>

                            <tr>
                                <td class="container_input_title" style="background: #ffffff; text-align: center;padding-top: 8px" colspan="2">
                                    <asp:Button runat="server" ID="LoginButton" Text="Log in" OnClick="LoginButton_Click" />
                                    <asp:PlaceHolder runat="server" ID="LogoutButton" Visible="false">
                                        <div>
                                            <div>
                                                <asp:Button ID="LogoutBtn" runat="server" OnClick="LogoutButton_Click" Text="Log out" />
                                            </div>
                                        </div>
                                    </asp:PlaceHolder>
                                </td>
                            </tr>

                            <tr>
                                <td class="container_input_title" style="padding-bottom: 8px; text-align: right; padding-top: 5px; padding-left: 5px; padding-right: 30px" colspan="2"><a href="../Configuration/SSFLicense.php?ret_link=%2F&type=notLogged" id="LoginLink1">License Information >></a></td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:PlaceHolder runat="server" ID="LoginStatus" Visible="False" EnableTheming="True">
                                        <p style="color: red;font-size: 18px">
                                            <asp:Literal runat="server" ID="StatusText" />
                                        </p>
                                    </asp:PlaceHolder>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </form>
    </div>
</body>
</html>
