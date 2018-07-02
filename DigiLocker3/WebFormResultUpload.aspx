<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebFormResultUpload.aspx.cs" Inherits="DigiLocker3.WebFormResultUpload" %>

<!DOCTYPE html>
<html>
<head>

    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta name="description" content="">
    <meta name="author" content="">

    <title>DigiLocker: A digtal locker for Indian Navy</title>

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
                <a class="navbar-brand" href="index.aspx">DigiLocker</a>
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
                            <a href="index.aspx"><i class="fa fa-dashboard fa-fw"></i> Dashboard</a>
                        </li>
                        
                        <li>
                            <a href="download.aspx"><i class="fa fa-table fa-fw"></i> Download</a>
                        </li>
                        <li>
                            <a href="upload.aspx"><i class="fa fa-edit fa-fw"></i> Upload</a>
                        </li>
                        <li>
                            <a href="#"><i class="fa fa-wrench fa-fw"></i> Sailors<span class="fa arrow"></span></a>
                            <ul class="nav nav-second-level">
                                <li>
                                    <a href="NewCourseSailors.aspx?id=<%=Server.UrlDecode(Request.QueryString["id"]) %>"><i class="fa fa-edit fa-fw"></i> Add Course</a>
                                </li>
                                <li>
                                    <a href="AddSubjectsSailors.aspx?id=<%=Server.UrlDecode(Request.QueryString["id"]) %>"><i class="fa fa-edit fa-fw"></i> Add Subjects</a>
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
                        
                    </ul>
                </div>
                <!-- /.sidebar-collapse -->
            </div>
            <!-- /.navbar-static-side -->
        </nav>
        <div id="page-wrapper">
            <div class="row">
                <div class="col-lg-12">
                    <h1 class="page-header">Upload</h1>
                </div>
                <!-- /.col-lg-12 -->
            </div>
            <!-- /.row -->
            <div class="row">
                <div class="col-lg-12">
                    <div class="panel panel-default" >
                        <div class="panel-heading">
                            Enter details here
                        </div>
                        <div class="panel-body">
                            <div class="row" style = "width:100%">
                                
			
                                <div class="col-lg-6" style = "width:100%">
                                    <form id="form1" runat="server" >
                                        
                                        <div class="form-group">
                                            <label>Select Course Type</label>
                                            <asp:DropDownList class="form-control" style = "width:auto" ID="ddlCourseType" runat="server" AutoPostBack = "true" OnSelectedIndexChanged = "ddlCourseTypeIndexChanged">
                                                
                                                
                                            </asp:DropDownList>
                                            </div>

                                        <div class="form-group">
                                            <label>Select Course Number</label>
                                            <asp:DropDownList class="form-control" style = "width:auto" ID="ddlCourseNo" runat="server" AutoPostBack = "true" OnSelectedIndexChanged = "ddlCourseNoIndexChanged">
                                                
                                                
                                            </asp:DropDownList>
                                            </div>
                                        <div class="form-group">
                                            <label>Select Term</label>
                                            <asp:DropDownList class="form-control" style = "width:auto" ID="ddlTerm" runat="server" AutoPostBack = "true" OnSelectedIndexChanged = "ddlTermIndexChanged">
                                                <asp:ListItem>A1</asp:ListItem>
                                                <asp:ListItem>A2</asp:ListItem>
                                                <asp:ListItem>B1</asp:ListItem>
                                                <asp:ListItem>B2</asp:ListItem>
                                                <asp:ListItem>C</asp:ListItem>
                                                <asp:ListItem>D1</asp:ListItem>
                                                <asp:ListItem>D2</asp:ListItem>
                                                <asp:ListItem>D3</asp:ListItem>
                                                <asp:ListItem>E</asp:ListItem>
                                            </asp:DropDownList>
                                            </div>

                                        <div class="form-group">
                                            <label>Select Entry Type</label>
                                            <asp:DropDownList class="form-control" style = "width:auto" ID="ddlEntryType" runat="server" AutoPostBack = "true" OnSelectedIndexChanged = "ddlEntryTypeIndexChanged">
                                                
                                                
                                            </asp:DropDownList>
                                            </div>
                                        <div class="form-group">
                                            <label>Select Subject</label>
                                            <asp:DropDownList class="form-control" style = "width:auto" ID="ddlSubject" runat="server" AutoPostBack = "true">
                                                
                                                
                                            </asp:DropDownList>
                                            </div>
                                                                             
                                        
                                        
                                        <div class="form-group">
                                            <label>Marks Excel File</label>
                                            <asp:FileUpload style="width:auto" ID="FileUpload1" class="form-control" runat="server" />
                                        </div>
                                        
                                        <asp:Button runat="server" id="SubmitButton" class="btn btn-default" text="Submit" onclick="SubmitButton_Click" />
                                        
                                        <asp:Button runat="server" type="reset" class="btn btn-default" text="Reset" onclick="ResetButton_Click"/>
                                        <br /><br />
                                            
                                    
                                    <div class="form-group" style="height:auto; max-height:500px; width:100%; overflow:auto;">
                                        <asp:GridView CssClass="table table-striped table-bordered table-hover columnscss" ID="GridView1" runat="server" ScrollBars="Both" AllowPaging="False" OnRowDataBound = "OnRowDataBound" AutoGenerateColumns="False">
                                          <columns>

                                                <asp:BoundField HeaderText="Entry" DataField="Entry" />
                                                <asp:BoundField HeaderText="Personal_No." DataField="Personal_No" />
                                                <asp:BoundField HeaderText="Name" DataField="Name" />
                                              <asp:BoundField HeaderText="Rank" DataField="Rank" />
                                              <asp:TemplateField HeaderText="Marks">  
                            <ItemTemplate>  
                                <asp:TextBox ID="TextBox1" runat="server" Text=' '></asp:TextBox>  
                                </ItemTemplate>
                                                  </asp:TemplateField>
                                          </columns>  
                                        </asp:GridView>
                                    </div>
                                        <asp:Button runat="server" id="ConfirmButton" class="btn btn-default" text="Confirm" onclick="ConfirmButton_Click" visible="False"  EnableViewState="false" />
                                        </form>
                                </div>
                               
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

