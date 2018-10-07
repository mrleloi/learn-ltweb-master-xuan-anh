<%@ Page Language="C#" ValidateRequest="false" AutoEventWireup="true" CodeFile="InsertImageToMediaItem.aspx.cs" Inherits="admin_pages_admin_articles_InsertImageToMediaItem" %>
<%@ OutputCache Location="None" %>
<%@ Import Namespace="Lib.Web" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
  <head id="Head1" runat="server">
    <meta name="description" content="ALT Jsc."/>
    <meta http-equiv="Content-Language" content="en-us"/>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Thêm ảnh vào nội dung</title>
    <script src="incleditor.js" type="text/javascript"></script>
  </head>  
  <body>
    <form id="uploadImage" runat="server" name="fmUploadImage" action="InsertImageToMediaItem.aspx" method="POST" enctype="MULTIPART/FORM-DATA">
      <table border="0" id="table1" align="center" width="90%">
        <tr>
          <td colspan="2">
            <font size="4"><b>Thêm ảnh vào nội dung<br /> </b></font>
          </td>
        </tr>
        <tr>
          <td height="150" align="center"> </td>
          <td><img alt="" name="imgPreviewImage" src="" /></td>
        </tr>
        <tr valign="middle">
          <td style="height: 26px; " valign="middle">Chọn ảnh</td>
          <td style="height: 26px; "> 
            <asp:FileUpload ID="uploadImageFile" onchange="getImage()" runat="server" Width="400" />
          </td>
        </tr>
        <tr valign="middle">
          <td style="height: 26px" valign="middle">Căn lề</td>
          <td style="height: 26px">
            <asp:DropDownList ID="cboAligns" DataTextField="AlignDesc" DataValueField="AlignName" runat="server"></asp:DropDownList>
          </td>
        </tr>
        <tr valign="middle">
          <td style="height: 26px" valign="middle">Loại ảnh</td>
          <td style="height: 26px">
            <asp:DropDownList ID="cboImageTypes" runat="server" DataTextField="ImageTypeDesc" DataValueField="ImageTypeName"></asp:DropDownList>
          </td>
        </tr>
	      <tr valign="middle">
	 	  <td style="height: 26px; " valign="middle">
            Mô tả
          </td>
	      <td style="height: 26px; ">
            <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" Rows="4" Width="360"></asp:TextBox></td>
	      </tr>
	      <tr valign="middle">
	        <td style="height: 26px; " valign="middle"></td>
	        <td style="height: 26px; ">
              <asp:Button ID="btnInsertImage" runat="server" Text="Thêm ảnh vào nội dung" OnClick="btnInsertImage_Click" />&nbsp;
              <input type="button" name="btnClose" value="Thoát" onclick="javascript: window.close();" />                        
              <asp:HiddenField ID="chkImage" runat="server" />
            </td>
	      </tr>
        </table>
      </form>
    </body>
</html>
<script type="text/javascript">
  function getImage()
  {
    var ImageItem = document.all('imgPreviewImage');
    InItem=document.all('uploadImageFile');
    ImageItem.src=InItem.value;
    var file=InItem.value;
    var dir = file.substring(0,file.lastIndexOf('\\')+1);
    var _filename = file.substring(dir.length,file.length+1);
  }
  function validString(strString)
  {
	  var txt=strString;
	  if (txt =="") return "";
	  var wrd = "'";
	  var wrdFix = "&#39;";
    while ( txt.indexOf(wrd) > -1)
    {
		  pos = txt.indexOf(wrd);
		  txt = txt.substring(0,pos) + wrdFix + txt.substring((pos+1), txt.length);
    }
	  return txt;
  }
    
  function doupload()
  {
    if (validimage())
    {
      document.all("chkImage").value = "OK";
    }
    else
    {
      document.all("chkImage").value = "";
    }     
  }
	
  function validimage()
  {
    var file=document.all('uploadImageFile').value;
    if (file == '') 
    {
        showAlert("Hãy chọn ảnh để đưa vào nội dung!");
        return false;
    }
    var dir = file.substring(0,file.lastIndexOf('\\')+1);
    var url = file.substring(dir.length,file.length+1);
    if(!validFileName(url))
    {
        showAlert("Tên tập tin không hợp lệ!");
        return false;
    }
    var ext=url.substring(url.lastIndexOf(".") + 1, url.length);
    ext=ext.toLowerCase();
    if(!validFileFormat(ext))
    {
        showAlert("Định dạng ảnh không hợp lệ!");
        return false;
    }
    return true;
 }

 function validFileName(url)
 {

	if ( url.indexOf(' ') > -1)
	{
		return false;
	}
	if ( url.indexOf('%') > -1)
	{
		return false;
	}
	if ( url.indexOf(';') > -1)
	{
		return false;
	}
	if ( url.indexOf('&') > -1)
	{
		return false;
	}

	if ( url.indexOf('"') > -1)
	{
		return false;
	}
    return true;
  }

  function validFileFormat(ext)
  {
    var txt="jpg gif jpeg swf flv";
	if ( txt.indexOf(ext) > -1)
	{
		return true;
	}
    return false;
  }

  function validDescLength(field, maxlimit )
  {
    if ( field.value.length > maxlimit )
    {
      field.value = field.value.substring( 0, maxlimit );
      showAlert('Phần chú thích vượt quá số ký tự cho phép.' );
      field.focus();
      return false;
    }
    else
    {
      return true;
    }
  }

  function validDesc()
  {
    var _desText = document.UploadImage.des;
    _desText.value = trim(_desText.value);
    if ( _desText.value.length > 255 )
    {
        showAlert( 'Phần chú thích vượt quá số ký tự cho phép.' );
        _desText.focus();
        return false;
    }
    return validateRequiredTextArea(_desText,"Chưa nhập phần chú thích cho ảnh.")
 }

//Check required textarea input field in a form
  function validateRequiredTextArea(textarea, promptMessage)
  {
    if ((textarea.value == null) || (textarea.value.length == 0))
    {
      showAlert(promptMessage);
      textarea.focus();
      return false;
    }
    return true;
  }

// display a alert
  function showAlert(strMessage)
  {
	  var strHeader = "Alt Jsc. - Elearn\n\n";
    var	msg = strHeader + strMessage + "\n\n";
    alert(msg);
  }

  function trim( s )
  {
	var i, sRetVal = "";
	if (s.length==0) return "";
	i = s.length-1;
	while ( i>=0 && s.charAt(i) == ' ' ) i--;
	s = s.substring( 0, i+1 );
	i = 0;
	while ( i< s.length && s.charAt(i) == ' ') i++;
	s = s.substring( i );
	s2 = "";
    for (i=0; i<(s.length); i++)
    {
      if ((s.charAt(i)==' ')&& (s.charAt(i+1)==' '))
      {
	  }
	  else
	  {
	  	s2 = s2 + s.charAt(i);
	  }
    }
    return s2;
  }	
</script>
