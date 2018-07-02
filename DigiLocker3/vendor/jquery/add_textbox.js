$(document).ready(function () {

        //$('#GridView1').append('<tr><td> <asp:TextBox ID="TextBox1" runat="server" ></asp:TextBox> </td><tr>');

        // ADD ROWS TO THE TABLE.
        //$('table tr:last')
        //    .after('<td><asp:TextBox ID="TextBox1" runat="server" ></asp:TextBox></td>');
		
		$('#GridView1 tr:first ').append("<td class='TableHeading'>Marks_Text</td>");    

		$('#GridView1 tr:not(:first)').each(function(){
		$(this).append($('<input type="text" id="marks" name="hidden_field" value="your value" />'));

 });  
    });