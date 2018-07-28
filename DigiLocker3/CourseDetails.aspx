<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CourseDetails.aspx.cs" Inherits="DigiLocker3.CourseDetails" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>

    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta name="description" content="">
    <meta name="author" content="">

    <title>Result Generation System</title>

    <!-- Bootstrap Core CSS -->
    <link href="../vendor/bootstrap/css/bootstrap.min.css" rel="stylesheet">

    <!-- MetisMenu CSS -->
    <link href="../vendor/metisMenu/metisMenu.min.css" rel="stylesheet">

    <!-- Custom CSS -->
    <link href="../dist/css/sb-admin-2.css" rel="stylesheet">
    <link href="../dist/css/style.css" rel="stylesheet">

    <!-- Morris Charts CSS -->
    <link href="../vendor/morrisjs/morris.css" rel="stylesheet">

    <!-- Custom Fonts -->
    <link href="../vendor/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css">

    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
        <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
        <script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>
    <![endif]-->

    <style>
        .button {
            background-color: #ebebeb; /* Green */
            border: none;
            width: 100%;
            color: black;
            padding: 10px;
            text-align: left;
            text-decoration: none;
            display: inline-block;
            font-size: 16px;
            margin: 4px 2px;
            cursor: pointer;
        }

        .button1 {
            border-radius: 2px;
        }

        .button2 {
            border-radius: 4px;
        }

        .button3 {
            border-radius: 8px;
        }

        .button4 {
            border-radius: 12px;
        }

        .button5 {
            border-radius: 50%;
        }
    </style>

</head>

<body>

    <div id="wrapper">

        <!-- Navigation -->
        <nav class="navbar navbar-default-top navbar-static-top" role="navigation" style="margin-bottom: 0">
            <div class="navbar-header">
                <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
                    <span class="sr-only">Toggle navigation</span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                </button>
                <a class="navbar-brand" href="Home.aspx" style="color:white;">Result Section</a>
            </div>
            <!-- /.navbar-header -->

            <ul class="nav navbar-top-links navbar-right">

                <!-- /.dropdown -->
                <%--<li class="dropdown">
                    <a class="dropdown-toggle" data-toggle="dropdown" href="#">
                        <i class="fa fa-user fa-fw"></i><i class="fa fa-caret-down"></i>
                    </a>
                    <ul class="dropdown-menu dropdown-user">
                        <li><a href="#"><i class="fa fa-user fa-fw"></i>User Profile</a>
                        </li>
                        <li><a href="#"><i class="fa fa-gear fa-fw"></i>Settings</a>
                        </li>
                        <li class="divider"></li>--%>
                <%--<li ><a href="login.aspx "><i class="fa fa-sign-out fa-fw"></i>Logout</a>
                </li>--%>
                <%--</ul>--%>
                <!-- /.dropdown-user -->
                <%--</li>--%>
                <!-- /.dropdown -->
            </ul>
            <!-- /.navbar-top-links -->
            <form id="form1" runat="server">

                <div class="navbar-default sidebar" role="navigation">
                    <div class="sidebar-nav navbar-collapse">
                        <ul class="nav" id="side-menu">
                            <li class="sidebar-search">
                                <div style="display: table-cell; vertical-align: middle; text-align: center; margin-left: -10px">
                                    <div class="col-md-12">
                                        <label style="float: left">Personal Number</label>
                                        <asp:TextBox ID="Course_Number_TextBox" CssClass="form-control" runat="server" AutoPostBack="true" placeholder="Personal Number"></asp:TextBox>
                                    </div>

                                    <div class="col-md-12" style="margin-top: 10px">
                                        <label style="float: left">Password</label>
                                        <asp:TextBox ID="TextBox1" CssClass="form-control" runat="server" AutoPostBack="true" placeholder="Password" type="password"></asp:TextBox>
                                    </div>
                                    <div class="col-md-12" style="margin-top: 10px">
                                        <asp:Button runat="server" ID="SubmitButton" class="btn btn-default" Text="Login" Style="float: left; margin-top: 10px" />
                                    </div>
                                </div>
                                <!-- /input-group -->
                            </li>
                            <li>
                                <a href="Home.aspx"><i class="fa fa-edit fa-fw"></i>DashBoard</a>
                            </li>
                            <li>
                                <a href="#"><i class="fa fa-wrench fa-fw"></i>Officers<span class="fa arrow"></span></a>
                                <ul class="nav nav-second-level">
                                    <li>
                                        <a href="#"><i class="fa fa-edit fa-fw"></i>Create Course</a>
                                    </li>
                                    <li>
                                        <a href="#"><i class="fa fa-edit fa-fw"></i>Add Subjects</a>
                                    </li>
                                    <li>
                                        <a href="#"><i class="fa fa-edit fa-fw"></i>Add Course</a>
                                    </li>
                                    <li>
                                        <a href="#"><i class="fa fa-edit fa-fw"></i>Add Trainees</a>
                                    </li>
                                    <li>
                                        <a href="#"><i class="fa fa-edit fa-fw"></i>Upload Marks</a>
                                    </li>
                                    <li>
                                        <a href="#"><i class="fa fa-edit fa-fw"></i>View Result</a>
                                    </li>
                                    <li>
                                        <a href="#"><i class="fa fa-edit fa-fw"></i>View Individual</a>
                                    </li>

                                </ul>
                                <!-- /.nav-second-level -->
                            </li>
                            <li>
                                <a href="#"><i class="fa fa-wrench fa-fw"></i>Sailors<span class="fa arrow"></span></a>
                                <ul class="nav nav-second-level">

                                    <li>
                                        <a href="#"><i class="fa fa-edit fa-fw"></i>Create Course <span class="fa arrow"></span></a>
                                        <ul class="nav nav-third-level">
                                            <li>
                                                <a href="CreateCourseSailors.aspx"><i class="fa fa-edit fa-fw"></i>Add Name and Entry Details</a>
                                            </li>
                                            <%--<li>
                                            <a href="SeniorityDetails.aspx"><i class="fa fa-edit fa-fw"></i>Add Seniority</a>
                                        </li>--%>
                                            <li>
                                                <a href="AddSubjectsSailors.aspx"><i class="fa fa-edit fa-fw"></i>Add Subjects</a>
                                            </li>
                                        </ul>
                                    </li>
                                    <li>
                                        <a href="NewCourseSailors.aspx"><i class="fa fa-edit fa-fw"></i>Add Course</a>
                                    </li>
                                    <li>
                                        <a href="UploadNominalRollSailors.aspx"><i class="fa fa-edit fa-fw"></i>Add Trainees</a>
                                    </li>
                                    <li>
                                        <a href="ViewTrainees1.aspx"><i class="fa fa-edit fa-fw"></i>View Trainees</a>
                                    </li>
                                    <li>
                                        <a href="UploadMarksSailors.aspx"><i class="fa fa-edit fa-fw"></i>Upload Marks</a>
                                    </li>
                                    <li>
                                        <a href="UpdateMarks.aspx"><i class="fa fa-edit fa-fw"></i>Update Marks</a>
                                    </li>
                                    <li>
                                        <a href="ViewResult1.aspx"><i class="fa fa-edit fa-fw"></i>View Result</a>
                                    </li>


                                </ul>
                                <!-- /.nav-second-level -->
                            </li>
                        </ul>
                    </div>
                    <!-- /.sidebar-collapse -->
                </div>
            </form>
            <!-- /.navbar-static-side -->
        </nav>
        <div id="page-wrapper">
            <div class="row">
                <div class="col-lg-12 page-header" style="width: 100%; margin-top:0">
                    <h1 class="" id="heading" runat="server" style="width: 50%; float:left; vertical-align:baseline;"></h1>
                    <img class="" src="logo39.jpg" alt="alternate text" style="width: 100px; height: 100px; float: right" />
                </div>
                <!-- /.col-lg-12 -->
            </div>
            <!-- /.row -->
            <div class="row">
                <div class="col-lg-12">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            Course details 
                        </div>
                        <div class="panel-body">
                            <div class="row" style="width: 100%">

                                <div class="col-lg-6" style="width: 100%">



                                    <%--<label>Entry Details</label>--%>
                                    <div class="form-group" style="height: auto; max-height: 500px; width: auto; overflow: auto;">

                                        <asp:GridView CssClass="table table-striped table-bordered table-hover columnscss" ID="GridView1" runat="server" ScrollBars="Both" AllowPaging="False" Visible="false" EnableViewState="false">
                                        </asp:GridView>
                                    </div>
                                    <%= MyMethodCall() %>

                                    <a href="NewCourseSailors.aspx?coursename=<%=Server.UrlDecode(Request.QueryString["coursename"]) %>">
                                        <label class="button button3  btn btn-primary" style="width: auto; background-color: #047bff; color: white">Enrol Course</label></a>
                                </div>

                                <!-- /.col-lg-6 (nested) -->
                            </div>
                            <!-- /.row (nested) -->
                        </div>
                        <!-- /.panel-body -->
                    </div>
                    <!-- /.panel -->
                </div>
                <!-- /.col-lg-12 -->
            </div>
            <!-- /.row -->
        </div>
        <!-- /#page-wrapper -->

    </div>
    <!-- /#wrapper -->

    <!-- jQuery -->
    <script src="../vendor/jquery/jquery.min.js"></script>

    <!-- Bootstrap Core JavaScript -->
    <script src="../vendor/bootstrap/js/bootstrap.min.js"></script>

    <!-- Metis Menu Plugin JavaScript -->
    <script src="../vendor/metisMenu/metisMenu.min.js"></script>

    <!-- Custom Theme JavaScript -->
    <script src="../dist/js/sb-admin-2.js"></script>

</body>

</html>
