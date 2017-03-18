<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserReqistration.aspx.cs" Inherits="simpleTest.UserReqistration" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
    <head>
        <title>User Registration</title>

     <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js" type="text/javascript"></script>
    <script type="text/JavaScript">
        function SendMail() {
            var emailaddress = $("#<%= txtemailaddress.ClientID  %>").val(); //what is this #<% %>
            $.ajax({
                type: "post",
                url: "UserReqistration.aspx/SendMail",
                data: '{emailaddress: "' + emailaddress + '" }',
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
    <form id="form1" runat="server">
    <div>
         <table style="width:100%">
            <tr>
                <td style="width:50%" Align="right">
                    <asp:Label ID="lblname" Text="Enter University of Rochester email address" runat="server"></asp:Label>
                </td>
                <td style="width:50%">
                   <asp:TextBox ID="txtemailaddress" runat="server"></asp:TextBox>  <!--what is this asp tag-->
                </td>
            </tr>
             

          
            <tr >
                <td colspan="2" Align="Center">
                    <input type="button" onclick="SendMail()" value="Register Me.." />
                
                    <%--<asp:Button runat="server" Text="Register Me.." ID="btnSubmit" OnClick="SendMail" />--%>
                </td>

            </tr>
              <tr >
                <td colspan="2" Align="Center">
                    <asp:Label ID="lblmessage" runat="server" ></asp:Label>
                </td>

            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
