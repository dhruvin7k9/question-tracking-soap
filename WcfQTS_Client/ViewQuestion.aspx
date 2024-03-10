<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewQuestion.aspx.cs" Inherits="WcfQTS_Client.ViewQuestion" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>QTS</title>
     <style>
        body {
            font-family: Arial, sans-serif;
            background-color: #f2f2f2;
            margin: 0;
            padding: 0;
        }

        .header {
            background-color: #333;
            color: #fff;
            padding: 10px;
            text-align: right;
        }

        .welcome {
            text-align: center;
            position: absolute;
            left: 40%;
            font-size:larger;
        }

        #btnLogout {
            background-color: red;
            padding: 6px 10px;
            border-radius: 5px;
            color: white;
            font-weight: bold;
        }

        #btnLogout:hover {
            background-color: orangered;
            padding: 6px 10px;
            border-radius: 5px;
        }

        .container {
            margin: 50px auto;
            background-color: #fff;
            border-radius: 10px;
            padding: 20px;
            width: 80%;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
        }

        h2 {
            text-align: center;
            color: #333;
        }

        label {
            display: block;
            font-weight: bold;
            margin-bottom: 5px;
        }

        .details {
            margin-bottom: 20px;
        }

        .details div {
            margin-bottom: 10px;
        }

        .actions {
            text-align: center;
        }

        .actions button {
            margin-right: 10px;
            padding: 10px 20px;
            background-color: #007bff;
            color: #fff;
            border: none;
            border-radius: 4px;
            cursor: pointer;
            transition: background-color 0.3s ease;
        }

        .actions button:hover {
            background-color: #0056b3;
        }

        .actions button:last-child {
            margin-right: 0;
        }

        .btn {
            margin-right: 20px;
            padding: 8px 10px;
            border-radius: 5px;
            color: white;
        }

        #lblNoteValue {
            width: 90%;
            margin-left: 4%;
            background-color: antiquewhite;
            border: thin;
            padding: 20px;
            color: #232023;
        }

        #btnEdit {
            background-color: green;
        }
        #btnEdit:hover {
            background-color: forestgreen;
        }

        #btnDelete {
            background-color: red;
        }
        #btnDelete:hover {
            background-color: orangered;
        }

        #btnGoBack {
            background-color: blue;
        }
        #btnGoBack:hover {
            background-color: dodgerblue;
        }

        .Easy {
            color: green;
        }
        .Medium {
            color: blue;
        }
        .Hard {
            color: red;
        }
    </style>
</head>
<body>

    <form id="form1" runat="server">

        <div class="header">
            <span class="welcome">Welcome, <%= ((WcfQTS_Client.ServiceReferenceUser.User)Session["CurrentUser"]).username %></span>
            <asp:Button ID="btnLogout" runat="server" Text="Log Out" OnClick="BtnLogout_Click" />
        </div>

        <div class="container">
            <h2>View Question</h2> <br />
            <div class="details">
                <div>
                    <asp:Label ID="lblTitle" runat="server" Text="Title:"></asp:Label>
                    <asp:Label ID="lblTitleValue" runat="server"></asp:Label>
                </div>
                <div>
                    <asp:Label ID="lblUrl" runat="server" Text="URL:"></asp:Label>
                    <asp:HyperLink ID="hlUrlValue" runat="server" Target="_blank"></asp:HyperLink>
                </div>
                <div>
                    <asp:Label ID="lblDifficulty" runat="server" Text="Difficulty:"></asp:Label>
                    <asp:Label ID="lblDifficultyValue" runat="server"></asp:Label>
                </div>
                <div>
                    <asp:Label ID="lblRemark" runat="server" Text="Remark:"></asp:Label>
                    <asp:Label ID="lblRemarkValue" runat="server"></asp:Label>
                </div>
                <div>
                    <asp:Label ID="lblNote" runat="server" Text="Note:"></asp:Label><br />
                    <asp:Label ID="lblNoteValue" runat="server" TextMode="MultiLine" BorderColor="#999999" BorderStyle="Solid" Height="20%" Width="90%"></asp:Label>
                </div>
                <div>
                    <asp:Label ID="lblPlatform" runat="server" Text="Platform:"></asp:Label>
                    <asp:HyperLink ID="hlPlatformValue" runat="server" Target="_blank"></asp:HyperLink>
                </div>
                <div>
                    <asp:Label ID="lblTopics" runat="server" Text="Topics:"></asp:Label>
                    <asp:Label ID="lblTopicsValue" runat="server"></asp:Label>
                </div>
            </div>
            <div class="actions">
                <asp:Button CssClass="btn" ID="btnEdit" runat="server" Text="Edit" OnClick="BtnEdit_Click" />
                <asp:Button CssClass="btn" ID="btnDelete" runat="server" Text="Delete" OnClick="BtnDelete_Click" />
                <asp:Button CssClass="btn" ID="btnGoBack" runat="server" Text="Go Back" OnClick="BtnGoBack_Click" />
            </div>
        </div>
    </form>
</body>
</html>
