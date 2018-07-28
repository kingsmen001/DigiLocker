<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateCourseOfficers.aspx.cs" Inherits="DigiLocker3.CreateCourseOfficers" %>

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
    <link href="../css/tooltip.css" rel="stylesheet">

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

    <script type="text/javascript">
        function showModal() {
            $("#myModal").modal('show');
        }

        $(function () {
            $("#btnShow").click(function () {
                showModal();
            });
        });
    </script>

</head>

<body>

    <div id="wrapper">
        <form id="form1" runat="server">
            <!-- Navigation -->
            <nav class="navbar navbar-default-top navbar-static-top" role="navigation" style="margin-bottom: 0">
                <div class="navbar-header">
                    <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
                        <span class="sr-only">Toggle navigation</span>
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                    </button>
                    <a class="navbar-brand" href="Home.aspx" style="color: white">Result Section</a>
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
                    <li><asp:Button runat="server" CssClass="btn btn-primary" ID="Button2" Text="Logout" OnClick="LogoutButton_Click"/>
                    </li>
                    <%--</ul>--%>
                    <!-- /.dropdown-user -->
                    <%--</li>--%>
                    <!-- /.dropdown -->
                </ul>
                <!-- /.navbar-top-links -->

                <div class="navbar-default sidebar" role="navigation">
                    <div class="sidebar-nav navbar-collapse">
                        <ul class="nav" id="side-menu">
                            <li class="sidebar-search">
                                <div style="display: table-cell; vertical-align: middle; text-align: center">
                                    <img class="" src="Valsura.png" alt="alternate text" style="width: 170px; height: 170px; align-items: center; margin-left: 25px" />
                                    <%--<input type="text" class="form-control" placeholder="Search...">
                                <span class="input-group-btn">
                                    <button class="btn btn-default" type="button">
                                        <i class="fa fa-search"></i>
                                    </button>
                                </span>--%>
                                </div>
                                <!-- /input-group -->
                            </li>
                            <li>
                                <a href="Home.aspx"><i class="fa fa-edit fa-fw"></i>DashBoard</a>
                            </li>
                           <li>
                                <a href="#"><i class="fa fa-wrench fa-fw"></i>Officers<span class="fa arrow"></span></a>
                                <ul class="nav nav-second-level">

                                    <li id="opnCreateCourseOfficer" runat="server">
                                        <a href="#"><i class="fa fa-edit fa-fw"></i>Create Course <span class="fa arrow"></span></a>
                                        <ul class="nav nav-third-level">
                                            <li>
                                                <a href="CreateCourseOfficers.aspx"><i class="fa fa-edit fa-fw"></i>Add Name and Entry Details</a>
                                            </li>
                                            <%--<li>
                                            <a href="SeniorityDetails.aspx"><i class="fa fa-edit fa-fw"></i>Add Seniority</a>
                                        </li>--%>
                                            <li>
                                                <a href="AddSubjectsOfficers.aspx"><i class="fa fa-edit fa-fw"></i>Add Subjects</a>
                                            </li>
                                        </ul>
                                    </li>
                                    <li id="opnAddCourseOfficer" runat="server">
                                        <a href="NewCourseOfficers.aspx"><i class="fa fa-edit fa-fw"></i>Add Course</a>
                                    </li>
                                    <li id="opnAddTraineesOfficer" runat="server">
                                        <a href="UploadNominalRollOfficers.aspx"><i class="fa fa-edit fa-fw"></i>Add Trainees</a>
                                    </li>
                                    <li id="opnViewTraineesOfficer" runat="server">
                                        <a href="ViewTraineesOfficers.aspx"><i class="fa fa-edit fa-fw"></i>View Trainees</a>
                                    </li>
                                    <li id="opnUploadMarksOfficer" runat="server">
                                        <a href="UploadMarksOfficers.aspx"><i class="fa fa-edit fa-fw"></i>Upload Marks</a>
                                    </li>
                                    <li id="opnUpdateMarksOfficer" runat="server">
                                        <a href="UpdateMarksOfficers.aspx"><i class="fa fa-edit fa-fw"></i>Update Marks and Seniority</a>
                                    </li>
                                    <li id="opnViewResultOfficer" runat="server">
                                        <a href="ViewResultOfficers.aspx"><i class="fa fa-edit fa-fw"></i>View Result</a>
                                    </li>


                                </ul>
                                <!-- /.nav-second-level -->
                            </li>
                            <li>
                                <a href="#"><i class="fa fa-wrench fa-fw"></i>Sailors<span class="fa arrow"></span></a>
                                <ul class="nav nav-second-level">

                                    <li id="opnCreateCourse" runat="server">
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
                                    <li id="opnAddCourse" runat="server">
                                        <a href="NewCourseSailors.aspx"><i class="fa fa-edit fa-fw"></i>Add Course</a>
                                    </li>
                                    <li id="opnAddTrainees" runat="server">
                                        <a href="UploadNominalRollSailors.aspx"><i class="fa fa-edit fa-fw"></i>Add Trainees</a>
                                    </li>
                                    <li id="opnViewTrainees" runat="server">
                                        <a href="ViewTraineesSailors.aspx"><i class="fa fa-edit fa-fw"></i>View Trainees</a>
                                    </li>
                                    <li id="opnUploadMarks" runat="server">
                                        <a href="UploadMarksSailors.aspx"><i class="fa fa-edit fa-fw"></i>Upload Marks</a>
                                    </li>
                                    <li id="opnUpdateMarks" runat="server">
                                        <a href="UpdateMarksSailors.aspx"><i class="fa fa-edit fa-fw"></i>Update Marks and Seniority</a>
                                    </li>
                                    <li id="opnViewResult" runat="server">
                                        <a href="ViewResultSailors.aspx"><i class="fa fa-edit fa-fw"></i>View Result</a>
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
                        <h1 class="page-header">Create Course</h1>
                    </div>
                    <!-- /.col-lg-12 -->
                </div>
                <!-- /.row -->
                <div class="row">
                    <div class="col-lg-12">
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                Enter details here
                            </div>
                            <div class="panel-body">
                                <div class="row" style="width: 100%">


                                    <div class="col-lg-6" style="width: 100%">

                                        <div class="modal fade" id="myModal" role="dialog">
                                            <div class="modal-dialog modal-lg">
                                                <div class="modal-content">
                                                    <div class="modal-header">
                                                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                                                        <h4 class="modal-title">Modal Header</h4>
                                                    </div>
                                                    <div class="modal-body">
                                                        <asp:Label ID="lblMessage" runat="server"></asp:Label>
                                                    </div>
                                                    <div class="modal-footer">
                                                        <%--<asp:Button ID="Button2" runat="server" Text="Click Me" OnClick="Button2_Click" />--%>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="form-row">
                                                <div class="col-md-4">
                                                    <label>Enter Course Name</label>
                                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtCourseName" type="text" aria-describedby="nameHelp" placeholder="Course Name" AutoPostBack="true" OnTextChanged="txtCourseName_TextChanged" />
                                                </div>
                                                <div class=" help-tip" style="position: relative; margin-left: 7px; margin-top: 10px; z-index: 500">
                                                    <p>Course Number Should consist only of Alphabets, Numbers, Space and Underscore.</p>
                                                </div>
                                                <div class="col-md-4">
                                                    <%--<label>Existing Courses</label>--%>
                                                    <asp:DropDownList runat="server" CssClass="form-control" ID="ddlCourseType" Visible="false" EnableViewState="false">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="form-group">
                                            <div class="form-row">
                                                <div class="col-md-4">
                                                    <%--<label>Entry Type Excel File</label>--%>
                                                    <%--<asp:FileUpload style="width:auto" ID="FileUpload1" class="form-control" runat="server"  />--%>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-4">

                                            <asp:CheckBox ID="CheckBox1" runat="server" />
                                            <label>Seniority Applicable</label>
                                        </div>
                                        <br />



                                        <br />
                                        <br />

                                        <div class="form-row">
                                            <div class="form-group col-md-4" style="height: auto; max-height: 500px; width: 97%; overflow: auto;">
                                                <asp:GridView CssClass="table table-striped table-bordered table-hover columnscss" ID="excelgrd" runat="server" AutoGenerateColumns="false" ShowFooter="true" PageSize="50">
                                                    <Columns>

                                                        <asp:TemplateField HeaderText="Entry Name">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtMaxMarks" runat="server" OnTextChanged="OntextChanged" placeholder="No Special Characters."></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Term Label">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtMinMarks" runat="server" OnTextChanged="OntextChanged" placeholder="A1,A2,B1,B2"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Duration(in Weeks)">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtSeniority" runat="server" OnTextChanged="OntextChanged" placeholder="52"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="Right" />
                                                            <FooterTemplate>
                                                                <asp:Button class="btn btn-default" ID="ButtonAdd" runat="server" Text="Add New Row" OnClick="ButtonAdd_Click" />
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                            <div class=" help-tip" style="position: relative; margin-left: 7px; margin-top: 10px; z-index: 500">
                                                <p>Entry Name not to contain any special characters other than underscore. Mention name of terms separated by commas with no space in between such as A1,A2,B1 or A,B,C or Ph1,Ph2</p>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <asp:Button runat="server" ID="SubmitButton" class="btn btn-default" Text="Submit" OnClick="SubmitButton_Click" AutoPostback="false" />

                                            <asp:Button runat="server" type="reset" class="btn btn-default" Text="Reset" OnClick="ResetButton_Click" />
                                        </div>
                                        <asp:Button runat="server" ID="ConfirmButton" class="btn btn-default" Text="Confirm" OnClick="ConfirmButton_Click" Visible="False" EnableViewState="false" />


                                    </div>
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
      </form>
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
