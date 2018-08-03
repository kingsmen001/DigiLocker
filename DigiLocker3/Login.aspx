<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="DigiLocker3.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>

    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta name="description" content="">
    <meta name="author" content="">

    <title>Result Management System</title>

    <!-- Bootstrap Core CSS -->
    <link href="./vendor/bootstrap/css/bootstrap.min.css" rel="stylesheet">

    <!-- MetisMenu CSS -->
    <link href="./vendor/metisMenu/metisMenu.min.css" rel="stylesheet">

    <!-- Custom CSS -->
    <link href="./dist/css/sb-admin-2.css" rel="stylesheet">
    <link href="../dist/css/style.css" rel="stylesheet">

    <!-- Custom Fonts -->
    <link href="./vendor/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css">

    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
        <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
        <script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>
    <![endif]-->

</head>

<body style="background: url(login-bg3.jpg) no-repeat center center fixed; background-size: cover;">
    <%--<div class="col-lg-12" style="display:table-cell; vertical-align:middle; text-align:center; margin-top:50px;"><img src="valsura.png" alt="alternate text" style="width:auto; height:auto" /></div>--%>
    <div class="col-lg-12" style="text-align:center">
        <%--<h1 class="page-header" id="heading" runat="server" style="text-align:center;">Course Management System</h1>--%>
    </div>
    
    <div class="container" style="margin:0 auto; width:40%; opacity:0.85; margin-top:5%">
        <div class="row">
            <div >
                <div class="login-panel panel panel-default">
                    <%--<div class="panel-heading">
                        <h3 class="panel-title"></h3>
                    </div>--%>

                    <%--<div class="panel-body">--%>
                    <div class="row" style="width: auto; margin: 0 auto">
                        <h1 class="page-header" id="h1" runat="server" style="text-align:center;">Result Management System</h1>
                        <div class="signin-form profile" >
                            <div class="login-form" style="width:50%;  margin: 0 auto">
                                <form id="form2" runat="server">
                                    <%--<input type="email" name="email" placeholder="E-mail" required="">--%>
                                    <asp:TextBox ID="PNo_TextBox" runat="server" placeholder="Userid"></asp:TextBox>
                                    <%--<input type="password" name="password" placeholder="Password" required="">--%>
                                    <asp:TextBox ID="Password_TextBox" type="password" runat="server" placeholder="Password"></asp:TextBox>
                                    <asp:Button runat="server" ID="LoginButton" Text="Login" OnClick="LoginButton_Click" />
                                    <div style="margin-top:10px">
                                    <a href="Home.aspx" >Visit as Guest</a>
                                        </div>
                                </form>
                            </div>
                        </div>
                    </div>
                    <div>
                        <ol>
                            <li>
                                To see the details of Courses and Trainees, Just login as guest.
                            </li>
                            <li>
                                To make any changes in course structure, Admin Priviliges are required.
                            </li>
                            <li>
                                Login using your credentials to access the system.
                            </li>
                        </ol>
                    </div>

                    <%--</div>--%>
                </div>
            </div>
        </div>
    </div>

    <!-- jQuery -->
    <script src="./vendor/jquery/jquery.min.js"></script>

    <!-- Bootstrap Core JavaScript -->
    <script src="./vendor/bootstrap/js/bootstrap.min.js"></script>

    <!-- Metis Menu Plugin JavaScript -->
    <script src="./vendor/metisMenu/metisMenu.min.js"></script>

    <!-- Custom Theme JavaScript -->
    <script src="./dist/js/sb-admin-2.js"></script>

</body>

</html>
