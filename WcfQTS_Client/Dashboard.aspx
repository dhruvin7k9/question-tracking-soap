<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="WcfQTS_Client.Dashboard" %>

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

        span {
            text-align: center;
            position: absolute;
            left: 40%;
            font-size:larger;
        }

        .container {
            margin: 20px auto;
            width: 80%;
        }

        table {
            width: 100%;
            border-collapse: collapse;
        }

        th, td {
            padding: 10px;
            border: 1px solid #ccc;
            text-align: left;
        }

        th {
            background-color: #333;
            color: #fff;
        }

        .actions {
            text-align: center;
        }

        .actions a {
            display: inline-block;
            margin: 5px;
            padding: 5px 10px;
            background-color: #333;
            color: #fff;
            text-decoration: none;
            border-radius: 3px;
        }

        .actions a:hover {
            background-color: #555;
        }

        #hlAdd {
            display: inline-block;
            position: absolute;
            right: 10%;
            padding: 10px 10px;
            background-color: green;
            color: #fff;
            text-decoration: none;
            border-radius: 3px;
        }

        #hlAdd:hover {
            background-color: forestgreen;
            padding: 10px 10px;
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

        .delete-button {
            padding: 6px 10px;
            background-color: red;
            color: #fff;
            border: none;
            border-radius: 3px;
            font-weight: bold;
        }

        .delete-button:hover {
            background-color: orangered;
        }

        .search-container {
            margin-bottom: 20px;
        }

        .search-container #inputTextBox {
            padding: 10px;
            margin-right: 10px;
            border: 1px solid #ccc;
            border-radius: 4px;
            width: 40%;
        }

        .search-container #btnSearch {
            padding: 10px 20px;
            background-color: #333;
            color: #fff;
            border: none;
            border-radius: 4px;
            cursor: pointer;
        }

        .search-container #btnSearch:hover {
            background-color: #555;
        }

        .no-results {
            font-style: italic;
            color: #999;
        }

        .topic-column {
            width: 20%;
        }

        .filter-container {
            margin-bottom: 20px;
        }

        .filter-container select {
            padding: 10px;
            margin-right: 10px;
            border: 1px solid #ccc;
            border-radius: 4px;
        }

        .filter-container #btnFilter {
            padding: 10px 20px;
            background-color: #333;
            color: #fff;
            border: none;
            border-radius: 4px;
            cursor: pointer;
        }

        .filter-container #btnFilter:hover {
            background-color: #555;
        }

        .refresh {
            position: absolute;
            top: 5px;
            left: 5px;
            padding: 10px 20px;
            text-decoration: none;
            background-color: white;
            color: black;
        }
        .refresh:hover {
            background-color: papayawhip;
        }

        .plt {
            text-decoration: none;
            background-color: antiquewhite;
            color: black;
            padding: 5px;
            border-radius: 8px;
            font-weight: 600;
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
        <a class="refresh" href="Dashboard.aspx">Refresh</a>
        <span>Welcome, <%= ((WcfQTS_Client.ServiceReferenceUser.User)Session["CurrentUser"]).username %></span>
        <asp:Button ID="btnLogout" runat="server" Text="Log Out" OnClick="BtnLogout_Click" />
    </div>

    <div class="container">
        <div class="search-container">
            <asp:TextBox ID="inputTextBox" runat="server" placeholder="Search question by title ..." AutoPostBack="false"></asp:TextBox>
            <asp:Button ID="btnSearch" runat="server" onclick="BtnSearch_Click" Text="Search"/>

            <asp:HyperLink ID="hlAdd" runat="server" Text="Add Question" NavigateUrl="CreateQuestion.aspx" />
        </div>

        <div class="filter-container">
            <asp:DropDownList ID="ddlPlatformFilter" runat="server" AppendDataBoundItems="true" AutoPostBack="false">
                <asp:ListItem Text="All Platforms" Value=""></asp:ListItem>
            </asp:DropDownList>

            <asp:DropDownList ID="ddlDifficultyFilter" runat="server" AppendDataBoundItems="true" AutoPostBack="false">
                <asp:ListItem Text="All Difficulties" Value=""></asp:ListItem>
                <asp:ListItem Text="Easy" Value="Easy"></asp:ListItem>
                <asp:ListItem Text="Medium" Value="Medium"></asp:ListItem>
                <asp:ListItem Text="Hard" Value="Hard"></asp:ListItem>
            </asp:DropDownList>

            <asp:DropDownList ID="ddlRemarkFilter" runat="server" AppendDataBoundItems="true" AutoPostBack="false">
                <asp:ListItem Text="All Remarks" Value=""></asp:ListItem>
                <asp:ListItem Text="Unattempted" Value="Unattempted"></asp:ListItem>
                <asp:ListItem Text="Attempted" Value="Attempted"></asp:ListItem>
                <asp:ListItem Text="Solved" Value="Solved"></asp:ListItem>
                <asp:ListItem Text="Revise" Value="Revise"></asp:ListItem>
            </asp:DropDownList>

            <asp:Button ID="btnFilter" onclick="BtnFilter_Click" runat="server" Text="Apply Filter"/>
        </div>
        
        <h2>Questions</h2> <hr />

        <table>
            <tr>
                <th>Platform</th>
                <th>Title</th>
                <th>Difficulty</th>
                <th>Remark</th>
                <th>Topics</th>
                <th>Actions</th>
            </tr>
            <asp:Repeater ID="rptQuestions" runat="server">

                <ItemTemplate>
                    <tr>
                        <td><a class="plt" href='<%# Eval("Platform.url") %>' target="_blank"><%# Eval("Platform.name") %></a></td>
                        <td><a href='<%# Eval("Question.url") %>' target="_blank"><%# Eval("Question.title") %></a></td>
                        <td class="<%# Eval("Question.difficulty") %>"><%# Eval("Question.difficulty") %></td>
                        <td><%# Eval("Question.remark") %></td>
                        <td class="topic-column">
                            <asp:Repeater runat="server" DataSource='<%# Eval("Topics") %>'>
                                <ItemTemplate>
                                    # <%# Container.DataItem %><br />
                                </ItemTemplate>
                            </asp:Repeater>
                        </td>
                        <td class="actions">
                            <asp:HyperLink ID="hlView" runat="server" Text="View" NavigateUrl='<%# "ViewQuestion.aspx?Id=" + Eval("Question.Id") %>' />
                            <asp:HyperLink ID="hlEdit" runat="server" Text="Edit" NavigateUrl='<%# "EditQuestion.aspx?Id=" + Eval("Question.Id") %>' />
                            <asp:Button ID="hlDelete" CssClass="delete-button" runat="server" Text="Delete" OnClick="BtnDeleteQue_Click" CommandArgument='<%# Eval("Question.Id") %>'/>
                        </td>
                    </tr>
                </ItemTemplate>

            </asp:Repeater>
        </table>
        <asp:Label ID="lblNoResults" runat="server" CssClass="no-results" Visible="false">No results found.</asp:Label>
    </div>

    </form>

</body>
</html>