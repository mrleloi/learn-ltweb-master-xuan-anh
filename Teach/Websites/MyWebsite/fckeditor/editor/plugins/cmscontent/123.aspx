<script type="text/javascript">

var oEditor = window.parent.InnerDialogLoaded() ;

window.onload = function()
{
	document.body.style.padding = '0px' ;

	// First of all, translate the dialog box texts
	oEditor.FCKLanguageManager.TranslatePage(document) ;

	window.parent.SetOkButton( true ) ;
	window.parent.SetAutoSize( true ) ;	
}

function Ok()
{
	var oArea = document.getElementById( 'insCode_area' ) ;

	if ( oArea.value.length > 0 )
		oEditor.FCK.InsertHtml( oArea.value ) ;

	return true ;
}

</script>
<body style="OVERFLOW: hidden" scroll="no">
		<textarea id="insCode_area" cols="40" rows="15" style="width:100%;height:180px;"></textarea>
dfsdfdsfdsfds
		<script type="text/javascript" src="dialogue.js"></script>
	</body>
