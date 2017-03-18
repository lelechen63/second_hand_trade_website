<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateAccount.aspx.cs" Inherits="simpleTest.CreateAccount" %>


<!DOCTYPE html>
<html>
<head runat="server">
    <title>Create Account</title>
         <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js" ></script>

   <script>
       function upload()
       {
            var obj = {};
           obj.fn = $("#<%= firstname.ClientID  %>").val(); 
           obj.ln = $("#<%= lastname.ClientID  %>").val();
           obj.un = $("#<%= username.ClientID  %>").val();
           obj.pn = $("#<%= phone.ClientID  %>").val();
           obj.pw = $("#<%= password.ClientID  %>").val();
           obj.ea = $("#<%= email.ClientID  %>").val();
           

           $.ajax({
               type: "post",
               url: "CreateAccount.aspx/SaveInformation",
               data: JSON.stringify(obj),
               contentType: "application/json; charset=utf-8",
               dataType: "json",
               success: function (result) {
                   OnSuccess(result.d);
               },
               error: function (xhr, status, error) {
                   OnFailure(error);
               }
           });
       }

        function OnSuccess(dateTime) {
            if (dateTime) {
                $('#lblmessage').text(dateTime)
            }
        }
        function OnFailure(error) {
            alert(error);
        }
       
    </script>
</head>
<body>
    <h1> Please Complete Your Registration</h1>
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td>
                    <asp:Label Text="First Name" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="firstname"   runat="server" ValidationGroup="val"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ID="reqName" controltovalidate="firstname" errormessage="!!!!!!!" />
                    <!--Validation not working!!!-->
                </td>

            </tr>
            <tr>
                <td>
                    <asp:Label Text="Last Name" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="lastname" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label Text=" User Name" runat="server"></asp:Label><!--need to eliminate duplication-->
                </td>
                <td>
                    <asp:TextBox ID="username" runat="server"></asp:TextBox>
                </td>
                <td colspan="2" Align="Center">
                    <asp:Label ID="lblmessage" runat="server" ></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label Text="Password" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="password" TextMode="Password" runat="server" ></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label Text="Phone Number(optional)" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="phone" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label Text="Email" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="email" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2" Align="Center">
                     <input type="button" onclick="upload()" value="Submit"  /> 
                </td>
            </tr>
        </table>
    </div>
</form>
</body>
</html>

