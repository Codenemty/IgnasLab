<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Pagrindinis.aspx.cs" Inherits="IgnasLab.Pagrindinis" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SoccerStats</title>
</head>
<body>
    <form id="InputForm" runat="server">
        <asp:Label ID="TitleName" runat="server" Text="SoccerStats.io "></asp:Label>
        <br />
        <input id="PositionInput" runat="server" type="text" />
        <asp:Button ID="ExecButton" runat="server" Text="Button" OnClick="ExecButton_Click" />
    </form>
        <asp:Panel ID="ResultPanel" runat="server">
        </asp:Panel>
</body>
</html>
