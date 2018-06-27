<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Test.aspx.cs" Inherits="DigiLocker3.Test" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:FileUpload class="form-control" ID="FileUpload1" runat="server"/>
    <asp:Button class="form-control" ID="btnUpload" runat="server" Text="Upload" OnClick="btnUpload_Click"/>
    <br />
    <asp:Label  class="form-control" ID="Label1" runat="server" Text="Has Header ?"/>
    <asp:RadioButtonList class="form-control" ID="rbHDR" runat="server">
    <asp:ListItem class="form-control" Text="Yes" Value="Yes" Selected="True"></asp:ListItem>
    <asp:ListItem class="form-control" Text="No" Value="No"></asp:ListItem>
    </asp:RadioButtonList>
    <asp:GridView class="form-control" ID="GridView1" runat="server" OnPageIndexChanging="PageIndexChanging"
        AllowPaging="true">
    </asp:GridView>



    </div>
    </form>
</body>
</html>
