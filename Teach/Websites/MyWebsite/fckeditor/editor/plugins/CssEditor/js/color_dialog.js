var CSSDialog = new Object();
CSSDialog.OpenDialog = function( dialogName, dialogTitle, dialogPage, width, height, customValue )
{
	// Setup the dialog info.
	var oDialogInfo = new Object() ;
	oDialogInfo.Title = dialogTitle ;
	oDialogInfo.Page = dialogPage ;
	oDialogInfo.Editor = window ;
	oDialogInfo.CustomValue = customValue;
	
	this.Show( oDialogInfo, dialogName, dialogPage, width, height ) ;
}

CSSDialog.Show = function( dialogInfo, dialogName, pageUrl, dialogWidth, dialogHeight )
{
	if (BrowserInfo.IsIE)	
	{
		window.showModalDialog( pageUrl, dialogInfo, "dialogWidth:" + dialogWidth + "px;dialogHeight:" + dialogHeight + "px;help:no;scroll:no;status:no" ) ;
		return;
	}

	var iTop  = (screen.height - dialogHeight) / 2 ;
	var iLeft = (screen.width  - dialogWidth)  / 2 ;

	var sOption  = "location=no,menubar=no,resizable=no,toolbar=no,dependent=yes,dialog=yes,minimizable=no,modal=yes,alwaysRaised=yes" +
		",width="  + dialogWidth +
		",height=" + dialogHeight +
		",top="  + iTop +
		",left=" + iLeft ;

	var oWindow = window.open( '', 'ColorDialog', sOption, true ) ;
	oWindow.moveTo( iLeft, iTop ) ;
	oWindow.resizeTo( dialogWidth, dialogHeight ) ;
	oWindow.focus() ;
	oWindow.location.href = pageUrl ;
	
	oWindow.dialogArguments = dialogInfo ;
	
	this.Window = oWindow ;

}
