<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Pagrindinis.aspx.cs" Inherits="IgnasLab.Pagrindinis" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../Content/Site.css" rel="stylesheet" type="text/css" />
    <title>SoccerStats</title>
</head>
<body>
    <form id="InputForm" runat="server">
        <asp:Label ID="TitleName" runat="server" Text="SoccerStats.io "></asp:Label>
        <br /><br />
        <asp:Label ID="PlayerUploadLabel" runat="server" Text="Upload your player data (optional): "></asp:Label>
        <asp:FileUpload ID="PlayerDataUpload" runat="server" /> <br />
        <asp:Label ID="TeamUploadLabel" runat="server" Text="Upload your team data (optional): "></asp:Label>
        <asp:FileUpload ID="TeamDataUpload" runat="server" /> <br />
        <asp:Label ID="InputLabel" runat="server" Text="Search for a team"></asp:Label>
        <input id="PositionInput" runat="server" type="text" placeholder="CSKA, KamandaX"/>
        <asp:Button ID="ExecButton" runat="server" Text="Search" OnClick="ExecButton_Click" />
        <br /><br />

    </form>
        <asp:Panel ID="ResultPanel" runat="server">
        </asp:Panel>
</body>
</html>
