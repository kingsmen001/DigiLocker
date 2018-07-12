﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SeniorityDetails.aspx.cs" Inherits="DigiLocker3.SeniorityDetails" %>

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
                <a class="navbar-brand" href="index.aspx">Result Section</a>
            </div>
            <!-- /.navbar-header -->

            <ul class="nav navbar-top-links navbar-right">

                <!-- /.dropdown -->
                <li class="dropdown">
                    <a class="dropdown-toggle" data-toggle="dropdown" href="#">
                        <i class="fa fa-user fa-fw"></i><i class="fa fa-caret-down"></i>
                    </a>
                    <ul class="dropdown-menu dropdown-user">
                        <li><a href="#"><i class="fa fa-user fa-fw"></i>User Profile</a>
                        </li>
                        <li><a href="#"><i class="fa fa-gear fa-fw"></i>Settings</a>
                        </li>
                        <li class="divider"></li>
                        <li><a href="login.html"><i class="fa fa-sign-out fa-fw"></i>Logout</a>
                        </li>
                    </ul>
                    <!-- /.dropdown-user -->
                </li>
                <!-- /.dropdown -->
            </ul>
            <!-- /.navbar-top-links -->

            <div class="navbar-default sidebar" role="navigation">
                <div class="sidebar-nav navbar-collapse">
                    <ul class="nav" id="side-menu">
                        <li class="sidebar-search">
                            <div class="input-group custom-search-form">
                                <input type="text" class="form-control" placeholder="Search...">
                                <span class="input-group-btn">
                                    <button class="btn btn-default" type="button">
                                        <i class="fa fa-search"></i>
                                    </button>
                                </span>
                            </div>
                            <!-- /input-group -->
                        </li>
                        
                        <li>
                            <a href="Home.aspx?id=<%=Server.UrlDecode(Request.QueryString["id"]) %>"><i class="fa fa-edit fa-fw"></i>View Courses</a>
                        </li>
                        <li>
                            <a href="#"><i class="fa fa-wrench fa-fw"></i>Officers<span class="fa arrow"></span></a>
                            <ul class="nav nav-second-level">
                                <li>
                                    <a href="CreateCourseSailors.aspx?id=<%=Server.UrlDecode(Request.QueryString["id"]) %>"><i class="fa fa-edit fa-fw"></i>Create Course</a>
                                </li>
                                <li>
                                    <a href="AddSubjectsSailors.aspx?id=<%=Server.UrlDecode(Request.QueryString["id"]) %>"><i class="fa fa-edit fa-fw"></i>Add Subjects</a>
                                </li>
                                <li>
                                    <a href="NewCourseSailors.aspx?id=<%=Server.UrlDecode(Request.QueryString["id"]) %>"><i class="fa fa-edit fa-fw"></i>Add Course</a>
                                </li>
                                <li>
                                    <a href="UploadNominalRollSailors.aspx?id=<%=Server.UrlDecode(Request.QueryString["id"]) %>"><i class="fa fa-edit fa-fw"></i>Add Trainees</a>
                                </li>
                                <li>
                                    <a href="UploadMarksSailors.aspx?id=<%=Server.UrlDecode(Request.QueryString["id"]) %>"><i class="fa fa-edit fa-fw"></i>Upload Marks</a>
                                </li>
                                <li>
                                    <a href="ViewResultSailors.aspx?id=<%=Server.UrlDecode(Request.QueryString["id"]) %>"><i class="fa fa-edit fa-fw"></i>View Result</a>
                                </li>
                                <li>
                                    <a href="ViewIndividualSailors.aspx?id=<%=Server.UrlDecode(Request.QueryString["id"]) %>"><i class="fa fa-edit fa-fw"></i>View Individual</a>
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
                                            <a href="CreateCourseSailors.aspx?id=<%=Server.UrlDecode(Request.QueryString["id"]) %>"><i class="fa fa-edit fa-fw"></i>Add Name and Entry Details</a>
                                        </li>
                                        <li>
                                            <a href="SeniorityDetails.aspx?id=<%=Server.UrlDecode(Request.QueryString["id"]) %>"><i class="fa fa-edit fa-fw"></i>Add Seniority</a>
                                        </li>
                                        <li>
                                            <a href="AddSubjectsSailors.aspx?id=<%=Server.UrlDecode(Request.QueryString["id"]) %>"><i class="fa fa-edit fa-fw"></i>Add Subjects</a>
                                        </li>
                                    </ul>
                                </li>
                                <li>
                                    <a href="NewCourseSailors.aspx?id=<%=Server.UrlDecode(Request.QueryString["id"]) %>"><i class="fa fa-edit fa-fw"></i>Add Course</a>
                                </li>
                                <li>
                                    <a href="UploadNominalRollSailors.aspx?id=<%=Server.UrlDecode(Request.QueryString["id"]) %>"><i class="fa fa-edit fa-fw"></i>Add Trainees</a>
                                </li>
                                <li>
                                    <a href="UploadMarksSailors.aspx?id=<%=Server.UrlDecode(Request.QueryString["id"]) %>"><i class="fa fa-edit fa-fw"></i>Upload Marks</a>
                                </li>
                                <li>
                                    <a href="ViewResult1.aspx?id=<%=Server.UrlDecode(Request.QueryString["id"]) %>"><i class="fa fa-edit fa-fw"></i>View Result</a>
                                </li>


                            </ul>
                            <!-- /.nav-second-level -->
                        </li>
                    </ul>
                </div>
                <!-- /.sidebar-collapse -->
            </div>
            <!-- /.navbar-static-side -->
        </nav>
        <div id="page-wrapper">
            <div class="row">
                <div class="col-lg-12">
                    <h1 class="page-header">Add Seniority</h1>
                </div>
                <!-- /.col-lg-12 -->
            </div>
            <!-- /.row -->
            <div class="row">
                <div class="col-lg-12">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            Enter Seniority Criteria details here
                        </div>
                        <div class="panel-body">
                            <div class="row" style="width: 100%">


                                <div class="col-lg-6" style="width: 100%">
                                    <form id="form1" runat="server">

                                        <div class="form-group">
                                            <label>Select Entry Type</label>
                                            <asp:DropDownList class="form-control" Style="width: auto" ID="ddlEntryType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlEntryTypeIndexChanged">
                                            </asp:DropDownList>
                                        </div>


                                        <div class="form-group">
                                            <label>Select Term</label>
                                            <asp:ListBox class="form-control" Style="width: auto; height: auto" ID="ddlTerm" runat="server" SelectionMode="Multiple" AutoPostBack="True" OnSelectedIndexChanged="ddlTermIndexChanged"></asp:ListBox>
                                            <%--<asp:DropDownList class="form-control" style = "width:auto" ID="ddlTerm" runat="server" AutoPostBack = "true" OnSelectedIndexChanged = "ddlTermIndexChanged">
                                                
                                            </asp:DropDownList>--%>
                                        </div>
                                        <div id="div1" runat="server">
                                            <div class="form-group">
                                                <%--<label>Seniority Details Excel File</label>
                                            <asp:FileUpload style="width:auto" ID="FileUpload1" class="form-control" runat="server" />--%>
                                                <asp:GridView CssClass="table table-striped table-bordered table-hover columnscss" ID="excelgrd" runat="server" AutoGenerateColumns="false" ShowFooter="true" PageSize="50">
                                                    <Columns>

                                                        <asp:TemplateField HeaderText="Max Marks(in %)">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtMaxMarks" runat="server" OnTextChanged="OntextChanged"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Min Marks(in %)">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtMinMarks" runat="server" OnTextChanged="OntextChanged"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Seniority(in Months)">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtSeniority" runat="server" OnTextChanged="OntextChanged"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="Right" />
                                                            <FooterTemplate>
                                                                <asp:Button class="btn btn-default" ID="ButtonAdd" runat="server" Text="Add New Row" OnClick="ButtonAdd_Click" />
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>

                                            <asp:Button runat="server" ID="SubmitButton" class="btn btn-default" Text="Submit" OnClick="SubmitButton_Click" />

                                            <asp:Button runat="server" type="reset" class="btn btn-default" Text="Reset" OnClick="ResetButton_Click" />
                                        </div>
                                        <br />
                                        <br />

                                        <div id="div2" runat="server">
                                            <div class="form-group" style="height: auto; max-height: 500px; width: 100%; overflow: auto;">
                                                <asp:GridView CssClass="table table-striped table-bordered table-hover columnscss" ID="GridView1" runat="server" ScrollBars="Both" AllowPaging="False">
                                                </asp:GridView>
                                            </div>
                                        </div>
                                        <asp:Button runat="server" ID="ConfirmButton" class="btn btn-default" Text="Add Subjects" OnClick="ConfirmButton_Click" Visible="False" EnableViewState="false" />
                                    </form>
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
