<%@ Control Language="C#" CodeBehind="Default.ascx.cs" Inherits="BackOfficeSystem.DefaultEntityTemplate" %>

<asp:EntityTemplate runat="server" ID="EntityTemplate1">
    <ItemTemplate>
        <tr class="td">
            <td class="DDLightHeader">
                <asp:Label runat="server" OnInit="Label_Init" />
            </td>
            <td>
                <asp:DynamicControl runat="server" OnInit="DynamicControl_Init" />
            </td>
        </tr>
        <tr>
            <td></td>
        </tr>
    </ItemTemplate>
</asp:EntityTemplate>

<div id="treeView" runat="server">
    <h3>Root</h3>
    <ul>
        <li>Folder 1
                            <ul>
                                <li>Folder 2
                                    <ul>
                                        <li>Folder 3
                                            <ul>
                                                <li>file1.xml</li>
                                                <li>file2.xml</li>
                                                <li>file3.xml</li>
                                            </ul>
                                        </li>
                                    </ul>
                                </li>
                                <li>file.html</li>
                            </ul>
        </li>
        <li>file.psd</li>
        <li>file.cpp</li>
    </ul>
</div>
