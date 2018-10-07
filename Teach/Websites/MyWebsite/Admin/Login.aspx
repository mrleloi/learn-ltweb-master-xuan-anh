<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="LoginPage"  Title="Thanh toán trực tuyến" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>Đăng nhập hệ thống</title>
<link href="css/login.css" rel="stylesheet" type="text/css"/>
</head>
<body>
  <form id="formLogin" runat="server">
	<div id="all">
    	<div id="center">
        	<table border="0" bordercolor="#000000" cellspacing="0" cellpadding="0" width="520px">
        	    <tr  >
        	      <td  style="height:100px"></td>
        	    </tr>
            	<tr>
                	<td >
                        <div align="center">
                            <asp:Login ID="m_login" runat="server" class="forumline" BorderColor="Transparent" 
                                BorderPadding="4" BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana"
                                Font-Size="14px" LoginButtonText=" Đăng nhập " TitleText="" TitleTextStyle-Font-Size="14px"
                                TitleTextStyle-Font-Names="Arial" TitleTextStyle-Font-Bold="true" InstructionText=""
                                InstructionTextStyle-ForeColor="red" OnAuthenticate="m_login_Authenticate" UserNameLabelText="Tên đăng nhập"
                                PasswordLabelText="Mật khẩu" RememberMeText="Nhớ mật khẩu" DisplayRememberMe="true"
                                FailureText="Tên hoặc mật khẩu chưa đúng!" Width="461" Height="220" 
                                VisibleWhenLoggedIn="False" RememberMeSet="false">
                                <LoginButtonStyle BackColor="#FFFBFF" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px"
                                    Font-Names="Verdana" Font-Size="0.8em" ForeColor="#284775" />
                                <TextBoxStyle Font-Size="0.8em" />
                                <TitleTextStyle BackColor="Transparent" Font-Bold="True" Font-Size="0.9em" ForeColor="Black" Font-Names="Arial" />
                                <InstructionTextStyle Font-Italic="True" ForeColor="Black" />
                            </asp:Login>
                        </div>
                    </td>
                </tr>
            </table>
        </div><!--Center-->		
    </div><!--all-->
    </form>
</body>
</html>
