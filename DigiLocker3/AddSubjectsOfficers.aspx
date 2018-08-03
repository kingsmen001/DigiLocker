<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddSubjectsOfficers.aspx.cs" Inherits="DigiLocker3.AddSubjectOfficers" %>

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

            <ul class="nav navbar-top-links navbar-right" style="margin-top: 7px; margin-right: 15px">

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
                </div>
                <!-- /.sidebar-collapse -->
            </div>
            <!-- /.navbar-static-side -->
        </nav>
        <div id="page-wrapper">
            <div class="row">
                <div class="col-lg-12">
                    <h1 class="page-header" id="heading" runat="server">Add Subjects-Officers</h1>
                </div>
                <!-- /.col-lg-12 -->
            </div>
            <!-- /.row -->
            <div class="row">
                <div class="col-lg-12">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            Enter Subject details here
                        </div>
                        
                            <div class="panel-body">

                                <div class="row" style="width: 100%">
                                    <div class="col-lg-6" style="width: 100%">
                                        <div class="form-row" style="width: 100%">
                                            <div class="form-group col-lg-4" id="divType" runat="server">
                                                <label runat="server" id="lblType">Select Course Type</label>
                                                <asp:DropDownList class="form-control" Style="width: auto" ID="ddlCourseType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCourseTypeIndexChanged">
                                                </asp:DropDownList>
                                            </div>
                                            <div id="div3" runat="server" class="form-group col-lg-4">
                                                <label>Select Entry Type</label>
                                                <asp:DropDownList class="form-control" Style="width: auto" ID="lbEntryType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="lblEntryTypeIndexChanged"></asp:DropDownList>
                                            </div>
                                            <div id="div2" runat="server" class="form-group col-lg-4">
                                                <label>Select Term</label>
                                                <asp:DropDownList class="form-control" Style="width: auto" ID="ddlTerm" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlTermIndexChanged">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div id="div1" runat="server">
                                            <div class="form-group" runat="server" id="exlfile">
                                                <label>Subject Detail Excel File</label>
                                                <asp:FileUpload Style="width: auto" ID="FileUpload1" class="form-control" runat="server" />
                                            </div>
                                            <div class="form-group" id="divgrid" runat="server">
                                                <label>Existing Subjects</label>
                                                <div class="form-group" style="height: auto; max-height: 350px; width: 100%; overflow: auto;">
                                                    <asp:GridView ID="GridView3" CssClass="table table-striped table-bordered table-hover columnscss" runat="server" DataKeyNames="Max_Marks" AutoGenerateColumns="False" CellPadding="6" OnRowCancelingEdit="GridView1_RowCancelingEdit"
                                                        OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" OnRowDeleting="GridView1_RowDeleting" ShowHeaderWhenEmpty="True" EmptyDataText="No records Found" OnSelectedIndexChanged="GridView3_SelectedIndexChanged">
                                                        <Columns>

                                                            <asp:TemplateField HeaderText="ID" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_ID" runat="server" Text='<%#Eval("ID") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Subject Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_Name" runat="server" Text='<%#Eval("Subject_Name") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="txt_Name" runat="server" Text='<%#Eval("Subject_Name") %>'></asp:TextBox>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Theory">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_Theory" runat="server" Text='<%#Eval("Theory") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="txt_Theory" runat="server" Text='<%#Eval("Theory") %>'></asp:TextBox>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="IA">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_IA" runat="server" Text='<%#Eval("IA") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="txt_IA" runat="server" Text='<%#Eval("IA") %>'></asp:TextBox>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Practical">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_Pra" runat="server" Text='<%#Eval("Practical") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="txt_Pra" runat="server" Text='<%#Eval("Practical") %>'></asp:TextBox>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Total">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_City" runat="server" Text='<%#Eval("Max_Marks") %>'></asp:Label>
                                                                </ItemTemplate>

                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:Button ID="btn_Edit" runat="server" Text="Edit" CommandName="Edit" />
                                                                    <asp:Button ID="btn_Delete" runat="server" Text="Delete" CommandName="Delete" />
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:Button ID="btn_Update" runat="server" Text="Update" CommandName="Update" />
                                                                    <asp:Button ID="btn_Cancel" runat="server" Text="Cancel" CommandName="Cancel" />
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                    <%--<asp:GridView CssClass="table table-striped table-bordered table-hover columnscss" ID="GridView2" runat="server" ScrollBars="Both" AllowPaging="False" >
                                            </asp:GridView>--%>
                                                </div>
                                            </div>
                                            <div class="form-group" id="divgridspl" runat="server">
                                                <label>Existing Subjects</label>
                                                <div class="form-group" style="height: auto; max-height: 350px; width: 100%; overflow: auto;">
                                                    <asp:GridView ID="GridView2" CssClass="table table-striped table-bordered table-hover columnscss" runat="server" DataKeyNames="Max_Marks" AutoGenerateColumns="False" CellPadding="6" OnRowCancelingEdit="GridView2_RowCancelingEdit"
                                                        OnRowEditing="GridView2_RowEditing" OnRowUpdating="GridView1_RowUpdating" OnRowDeleting="GridView2_RowDeleting" ShowHeaderWhenEmpty="True" EmptyDataText="No records Found" OnSelectedIndexChanged="GridView2_SelectedIndexChanged">
                                                        <Columns>

                                                            <asp:TemplateField HeaderText="ID" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_ID" runat="server" Text='<%#Eval("ID") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Subject Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_Name" runat="server" Text='<%#Eval("Subject_Name") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="txt_Name" runat="server" Text='<%#Eval("Subject_Name") %>'></asp:TextBox>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Theory">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_Theory" runat="server" Text='<%#Eval("Theory") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="txt_Theory" runat="server" Text='<%#Eval("Theory") %>'></asp:TextBox>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="IA">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_IA" runat="server" Text='<%#Eval("IA") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="txt_IA" runat="server" Text='<%#Eval("IA") %>'></asp:TextBox>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Practical">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_Pra" runat="server" Text='<%#Eval("Practical") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="txt_Pra" runat="server" Text='<%#Eval("Practical") %>'></asp:TextBox>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Total">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_City" runat="server" Text='<%#Eval("Max_Marks") %>'></asp:Label>
                                                                </ItemTemplate>

                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Specialisation">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_spc" runat="server" Text='<%#Eval("Class") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="txt_spc" runat="server" Text='<%#Eval("Class") %>'></asp:TextBox>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:Button ID="btn_Edit" runat="server" Text="Edit" CommandName="Edit" />
                                                                    <asp:Button ID="btn_Delete" runat="server" Text="Delete" CommandName="Delete" />
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:Button ID="btn_Update" runat="server" Text="Update" CommandName="Update" />
                                                                    <asp:Button ID="btn_Cancel" runat="server" Text="Cancel" CommandName="Cancel" />
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                    <%--<asp:GridView CssClass="table table-striped table-bordered table-hover columnscss" ID="GridView2" runat="server" ScrollBars="Both" AllowPaging="False" >
                                            </asp:GridView>--%>
                                                </div>
                                            </div>
                                            <div class="form-group" runat="server" id="single">

                                                <div class="form-row">

                                                    <div class="form-group col-md-2">
                                                        <%--<label for="lbl_equip_code">Subject Name</label>--%>
                                                        <asp:TextBox runat="server" type="text" CssClass="form-control" ID="txtSubject" placeholder="Subject Name" AutoPostBack="true" OnTextChanged="txtCourseName_TextChanged" />
                                                    </div>
                                                    <div class="form-group col-md-2">
                                                        <%--<label for="lbl_equip_desc">Maximum Marks</label>--%>
                                                        <asp:TextBox runat="server" type="text" CssClass="form-control" ID="txtTheory" placeholder="Theory Marks" />
                                                    </div>
                                                    <div class="form-group col-md-2">
                                                        <%--<label for="lbl_equip_desc">Maximum Marks</label>--%>
                                                        <asp:TextBox runat="server" type="text" CssClass="form-control" ID="txtIA" placeholder="IA Marks" />
                                                    </div>
                                                    <div class="form-group col-md-2">
                                                        <%--<label for="lbl_equip_desc">Maximum Marks</label>--%>
                                                        <asp:TextBox runat="server" type="text" CssClass="form-control" ID="txtPractical" placeholder="Practical Marks" />
                                                    </div>
                                                    <div class="form-group col-md-2">
                                                        <%--<label for="lbl_equip_desc">Maximum Marks</label>--%>
                                                        <asp:TextBox runat="server" type="text" CssClass="form-control" ID="txtMarks" placeholder="Maximum Marks" />
                                                    </div>
                                                    <div class="form-group col-md-2" id ="divspl" runat="server">
                                                        <%--<label for="lbl_equip_desc">Maximum Marks</label>--%>
                                                        <asp:TextBox runat="server" type="text" CssClass="form-control" ID="txtSpc" placeholder="Specialization" />
                                                    </div>
                                                    <div class="form-group col-md-2">
                                                        <asp:Button runat="server" ID="SubmitButton" CssClass="form-control btn btn-default" Text="Submit" OnClick="SubmitButton_Click" />
                                                    </div>
                                                </div>

                                            </div>



                                            <asp:Button runat="server" type="reset" class="btn btn-default" Text="Reset" OnClick="ResetButton_Click" Visible="false" EnableViewState="false" />
                                            <br />
                                            <br />
                                        </div>

                                    </div>
                                </div>
                                <!-- /.col-lg-6 (nested) -->
                                <div class="col-lg-6">
                                </div>
                                <!-- /.col-lg-6 (nested) -->
                            </div>
                            <!-- /.row (nested) -->

                            <div class="panel-body">

                                <div class="row" style="width: 100%">
                                    <div class="col-lg-6" style="width: 100%">
                                        <div class="form-group" style="height: auto; max-height: 500px; width: 100%; overflow: auto;">

                                            <asp:GridView CssClass="table table-striped table-bordered table-hover columnscss" ID="GridView1" runat="server" ScrollBars="Both" AllowPaging="False">
                                            </asp:GridView>
                                        </div>
                                        <asp:Button runat="server" ID="ConfirmButton" class="btn btn-default" Text="Confirm" OnClick="ConfirmButton_Click" Visible="False" EnableViewState="false" />
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
    </form>
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
