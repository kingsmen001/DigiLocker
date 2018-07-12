<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="DigiLocker3.Home" %>

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
    background-color: #007bff; /* Green */
    border: none;
    color: white;
    padding: 10px;
    text-align: center;
    text-decoration: none;
    display: inline-block;
    font-size: 16px;
    margin: 4px 2px;
    cursor: pointer;
}

.button1 {border-radius: 2px;}
.button2 {border-radius: 4px;}
.button3 {border-radius: 8px;}
.button4 {border-radius: 10px;}
.button5 {border-radius: 50%;}
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
                <a class="navbar-brand" href="index.aspx">Result Section</a>
            </div>
            <!-- /.navbar-header -->

            <ul class="nav navbar-top-links navbar-right">
                
                <!-- /.dropdown -->
                <li class="dropdown">
                    <a class="dropdown-toggle" data-toggle="dropdown" href="#">
                        <i class="fa fa-user fa-fw"></i> <i class="fa fa-caret-down"></i>
                    </a>
                    <ul class="dropdown-menu dropdown-user">
                        <li><a href="#"><i class="fa fa-user fa-fw"></i> User Profile</a>
                        </li>
                        <li><a href="#"><i class="fa fa-gear fa-fw"></i> Settings</a>
                        </li>
                        <li class="divider"></li>
                        <li><a href="login.html"><i class="fa fa-sign-out fa-fw"></i> Logout</a>
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
                                    <a href="Home.aspx?id=<%=Server.UrlDecode(Request.QueryString["id"]) %>"><i class="fa fa-edit fa-fw"></i> View Courses</a>
                        </li>
                        <li>
                            <a href="#"><i class="fa fa-wrench fa-fw"></i> Officers<span class="fa arrow"></span></a>
                            <ul class="nav nav-second-level">
                                <li>
                                    <a href="CreateCourseSailors.aspx?id=<%=Server.UrlDecode(Request.QueryString["id"]) %>"><i class="fa fa-edit fa-fw"></i> Create Course</a>
                                </li>
                                <li>
                                    <a href="AddSubjectsSailors.aspx?id=<%=Server.UrlDecode(Request.QueryString["id"]) %>"><i class="fa fa-edit fa-fw"></i> Add Subjects</a>
                                </li>
                                <li>
                                    <a href="NewCourseSailors.aspx?id=<%=Server.UrlDecode(Request.QueryString["id"]) %>"><i class="fa fa-edit fa-fw"></i> Add Course</a>
                                </li>
                                <li>
                                    <a href="UploadNominalRollSailors.aspx?id=<%=Server.UrlDecode(Request.QueryString["id"]) %>"><i class="fa fa-edit fa-fw"></i> Add Trainees</a>
                                </li>
                                <li>
                                    <a href="UploadMarksSailors.aspx?id=<%=Server.UrlDecode(Request.QueryString["id"]) %>"><i class="fa fa-edit fa-fw"></i> Upload Marks</a>
                                </li>
                                <li>
                                    <a href="ViewResultSailors.aspx?id=<%=Server.UrlDecode(Request.QueryString["id"]) %>"><i class="fa fa-edit fa-fw"></i> View Result</a>
                                </li>
                                <li>
                                    <a href="ViewIndividualSailors.aspx?id=<%=Server.UrlDecode(Request.QueryString["id"]) %>"><i class="fa fa-edit fa-fw"></i> View Individual</a>
                                </li>
                                
                            </ul>
                            <!-- /.nav-second-level -->
                        </li>
                        <li>
                            <a href="#"><i class="fa fa-wrench fa-fw"></i> Sailors<span class="fa arrow"></span></a>
                            <ul class="nav nav-second-level">
                                
                                <li>
                                    <a href="#"><i class="fa fa-edit fa-fw"></i> Create Course <span class="fa arrow"></span></a>
                                    <ul class="nav nav-third-level">
                                    <li>
                                    <a href="CreateCourseSailors.aspx?id=<%=Server.UrlDecode(Request.QueryString["id"]) %>"><i class="fa fa-edit fa-fw"></i> Add Name and Entry Details</a>
                                    </li>
                                    <li>
                                    <a href="SeniorityDetails.aspx?id=<%=Server.UrlDecode(Request.QueryString["id"]) %>"><i class="fa fa-edit fa-fw"></i> Add Seniority</a>
                                    </li>
                                    <li>
                                    <a href="AddSubjectsSailors.aspx?id=<%=Server.UrlDecode(Request.QueryString["id"]) %>"><i class="fa fa-edit fa-fw"></i> Add Subjects</a>
                                    </li>
                                        </ul>
                                </li>
                                <li>
                                    <a href="NewCourseSailors.aspx?id=<%=Server.UrlDecode(Request.QueryString["id"]) %>"><i class="fa fa-edit fa-fw"></i> Add Course</a>
                                </li>
                                <li>
                                    <a href="UploadNominalRollSailors.aspx?id=<%=Server.UrlDecode(Request.QueryString["id"]) %>"><i class="fa fa-edit fa-fw"></i> Add Trainees</a>
                                </li>
                                <li>
                                    <a href="UploadMarksSailors.aspx?id=<%=Server.UrlDecode(Request.QueryString["id"]) %>"><i class="fa fa-edit fa-fw"></i> Upload Marks</a>
                                </li>
                                <li>
                                    <a href="ViewResult1.aspx?id=<%=Server.UrlDecode(Request.QueryString["id"]) %>"><i class="fa fa-edit fa-fw"></i> View Result</a>
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
                    <h1 class="page-header">Course Management System </h1>
                </div>
                <!-- /.col-lg-12 -->
            </div>
            <!-- /.row -->
            <div class="row">
                <div class="col-lg-12">
                    <div class="panel panel-default" >
                        <div class="panel-heading">
                            List of Available Courses
                        </div>
                        <div class="panel-body">
                            <div class="row" style = "width:100%">
                                
			                    <div class="col-lg-8" style = "width:100%">
                                    <form id="form1" runat="server" >
                                        <div class="col-lg-6">
                                            <label>Sailor Courses</label>
                                            <%--<ul style="list-style-type:none">--%>
                                         <table style ="width:200px" class="table table-striped table-bordered table-hover" id="dataTables-example">
                                <thead>
                                    <tr>
                                        <%--<th>Sr.No.</th>
                                        <th>Course Name</th>--%>
                                        
                                     </tr>
                                </thead>
                                <tbody>
                                      <%=getCourseDetails()%>
                                  <%--</ul>--%>
                                </tbody>
                                
                            </table>
                                        </div>
                                        <div class="col-lg-6">
                                            <%--<label>Courses Enrolled</label>
                                         <table style ="width:400px" class="table table-striped table-bordered table-hover" id="dataTables-example">
                                <thead>
                                    <tr>
                                        <th>Sr.No.</th>
                                        <th>Course Name</th>
                                        
                                     </tr>
                                </thead>
                                <tbody>
                                      <%=getEnrolledCourseDetails()%>
                                  
                                </tbody>
                                
                            </table>--%>
                                        </div>
                                        </form>
                                   
                                    
                                </div>
                                
                                <!-- /.col-lg-6 (nested) -->
                            </div>
                            <!-- /.row (nested) -->
                            <div style="align-self:center">
                            <a href ="CreateCourseSailors.aspx"><label class="button button3 btn btn-primary">Create Course</label></a>
                                    <a href ="NewCourseSailors.aspx"><label class="button button3  btn btn-primary">Enrol Course</label></a>
                                </div>
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