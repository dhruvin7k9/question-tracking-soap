<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WcfQTS_Client.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>QTS</title>
    <style>
        body {
            font-family: Arial;
        }

        h2 {
            text-align: center;
        }

        h3 {
            text-align: center;
            color: green;
        }

        .input {
            width: 100%;
            padding: 12px 20px;
            margin: 8px 0;
            display: block;
            border: 1px solid #ccc;
            border-radius: 4px;
            box-sizing: border-box;
        }

        #Button1 {
            width: 100%;
            background-color: #04AA6D;
            color: white;
            padding: 14px 20px;
            margin: 8px 0;
            border: none;
            border-radius: 4px;
            cursor: pointer;
            font-weight: bold;
        }

        #Button1:hover {
            background-color: #45a049;
        }
        #Button2 {
            width: 40%;
            background-color: #d73b3b;
            color: white;
            padding: 14px 20px;
            margin: 8px 0;
            border: none;
            border-radius: 4px;
            cursor: pointer;
            font-weight: 900;
        }

        #Button2:hover {
            background-color: #f97979;
        }

        div.container {
            border-radius: 5px;
            background-color: #f2f2f2;
            padding: 20px;
            width: 50%;
            position: absolute;
            left: 25%;
        }

        .errors {
            position: absolute;
            top: 50%;
            width: 25%;
            color: red;
        }
    </style>
</head>
<body>
    <h2>Welcome to QTS</h2>
    <h3>Login to Continue</h3>
    <form id="form1" runat="server">

        <div class="container">
            <asp:Label ID="Label1" runat="server" Text="UserName :"></asp:Label><br />
            <asp:TextBox ID="TextBox1" Class="input" runat="server"></asp:TextBox><br />
            <br />

            <asp:Label ID="Label2" runat="server" Text="Password :"></asp:Label><br />
            <asp:TextBox ID="TextBox2" Class="input" runat="server" TextMode="Password"></asp:TextBox><br />
            <br />


            <asp:Button ID="Button1" runat="server" Text="Login" OnClick="Button1_Click" ValidationGroup="rqrfldv"/>
            <asp:Button ID="Button2" runat="server" Text="Register here !" OnClick="Button2_Click" />
        </div>

        <div class="errors">
            <asp:Label ID="Label3" runat="server" Text=""></asp:Label>
            <br />
            <asp:RequiredFieldValidator ValidationGroup="rqrfldv" ID="RequiredFieldValidator1" runat="server" ErrorMessage="username required" ControlToValidate="TextBox1"></asp:RequiredFieldValidator>
            <br />
            <asp:RequiredFieldValidator ValidationGroup="rqrfldv" ID="RequiredFieldValidator2" runat="server" ErrorMessage="password required" ControlToValidate="TextBox2"></asp:RequiredFieldValidator>
        </div>

    </form>

</body>
</html>
