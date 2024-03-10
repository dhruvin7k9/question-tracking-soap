<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateQuestion.aspx.cs" Inherits="WcfQTS_Client.CreateQuestion" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>QTS</title>
    <style>
        body {
            font-family: Arial, sans-serif;
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
            margin: 20px auto;
            border-radius: 5px;
            background-color: #f2f2f2;
            padding: 20px;
            width: 80%;
            position: absolute;
            left: 10%;
        }

        h2 {
            text-align: center;
        }

        form div {
            margin-bottom: 10px;
        }

        label {
            display: block;
            font-weight: bold;
        }

        input[type="text"], input[type="url"],
        textarea  {
            width: 100%;
            padding: 8px;
            border: 1px solid #ccc;
            border-radius: 4px;
            box-sizing: border-box;
        }

        #txtNote {
            max-width: 100%;
        }

        select {
            width: 100%;
            padding: 8px;
            border: 1px solid #ccc;
            border-radius: 4px;
            box-sizing: border-box;
        }

        button {
            padding: 10px 20px;
            background-color: #007bff;
            color: #fff;
            border: none;
            border-radius: 4px;
            cursor: pointer;
        }

        button:hover {
            background-color: #0056b3;
        }

        .add {
            background-color: green;
            padding: 10px 20px;
            border-radius: 5px;
            margin-right: 20px;
            color: white;
            font-weight: bold;
        }

        .add:hover {
            background-color: forestgreen;
        }

        .cancel {
            background-color: red;
            padding: 10px 20px;
            border-radius: 5px;
            color: white;
            font-weight: bold;
        }

        .cancel:hover {
            background-color: orangered;
        }

        .queExists {
            position: absolute;
            left: 47%;
            top: 10%;
        }

        .checkboxlist-container {
            width: 100%;
        }

        .checkboxlist-container br {
            display: none;
        }

        .checkboxlist-container input[type=checkbox] {
            transform: scale(1.5);
        }

        .cbl-li{
            display: inline-block;
            width: 50px;
            margin-right: 140px;
            margin-top: 5px;
            margin-bottom: 5px;
            white-space: nowrap;
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
            <h2>Create Question</h2>
            <br />
            <br />
            <asp:Label CssClass="queExists" ForeColor="Red" ID="lblQueExists" runat="server" Text=""></asp:Label>
            <div class="add-container">
                <asp:Label ID="lblTitle" runat="server" Text="Title :"></asp:Label>
                <asp:TextBox ID="txtTitle" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator CssClass="error" ID="rfvTitle" runat="server" ForeColor="Red" ControlToValidate="txtTitle" ErrorMessage="Title is required." ValidationGroup="questionValidation"></asp:RequiredFieldValidator>
            </div>
            <br />

            <div>
                <asp:Label ID="lblUrl" runat="server" Text="URL :"></asp:Label>
                <asp:TextBox ID="txtUrl" runat="server" TextMode="Url"></asp:TextBox>
                <asp:RequiredFieldValidator CssClass="error" ID="rfvUrl" runat="server" ForeColor="Red" ControlToValidate="txtUrl" ErrorMessage="URL is required." ValidationGroup="questionValidation"></asp:RequiredFieldValidator>
            </div>
            <br />

            <div>
                <asp:Label ID="lblDifficulty" runat="server" Text="Difficulty :"></asp:Label>
                <asp:DropDownList ID="ddlDifficulty" runat="server">
                    <asp:ListItem Text="Easy" Value="Easy"></asp:ListItem>
                    <asp:ListItem Text="Medium" Value="Medium"></asp:ListItem>
                    <asp:ListItem Text="Hard" Value="Hard"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <br />

            <div>
                <asp:Label ID="lblRemark" runat="server" Text="Remark :"></asp:Label>
                <asp:DropDownList ID="ddlRemark" runat="server">
                    <asp:ListItem Text="Unattempted" Value="Unattempted"></asp:ListItem>
                    <asp:ListItem Text="Attempted" Value="Attempted"></asp:ListItem>
                    <asp:ListItem Text="Solved" Value="Solved"></asp:ListItem>
                    <asp:ListItem Text="Revise" Value="Revise"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <br />

            <div>
                <asp:Label ID="lblNote" runat="server" Text="Note :"></asp:Label>
                <asp:TextBox ID="txtNote" runat="server" TextMode="MultiLine"></asp:TextBox>
            </div>
            <br />

            <div>
                <asp:Label ID="lblPlatform" runat="server" Text="Platform :"></asp:Label>
                <asp:DropDownList ID="ddlPlatform" runat="server"></asp:DropDownList>
            </div>
            <br />

            <asp:Label ID="lblTopics" runat="server" Text="Topics :"></asp:Label>
            <br />
            <div class="checkboxlist-container">
                <asp:CheckBoxList ID="cblTopics" runat="server" RepeatLayout="Flow"></asp:CheckBoxList>
            </div>
            <br />

            <asp:Button CssClass="add" ID="btnSubmit" runat="server" Text="Save" OnClick="BtnSubmit_Click" ValidationGroup="questionValidation" />
            <asp:Button CssClass="cancel" ID="btnCancel" runat="server" Text="Cancel" OnClick="BtnCancel_Click" />

        </div>
    </form>

</body>
</html>