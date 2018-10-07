var BrowserInfo = new Object() ;

var sAgent = navigator.userAgent.toLowerCase() ;

BrowserInfo.IsIE		= sAgent.indexOf("msie") != -1 ;
BrowserInfo.IsGecko		= !BrowserInfo.IsIE ;
BrowserInfo.IsNetscape	= sAgent.indexOf("netscape") != -1 ;

if ( BrowserInfo.IsIE )
{
	BrowserInfo.MajorVer = navigator.appVersion.match(/MSIE (.)/)[1] ;
	BrowserInfo.MinorVer = navigator.appVersion.match(/MSIE .\.(.)/)[1] ;
}
else
{
	// TODO: Other browsers
	BrowserInfo.MajorVer = 0 ;
	BrowserInfo.MinorVer = 0 ;
}

BrowserInfo.IsIE55OrMore = BrowserInfo.IsIE && ( BrowserInfo.MajorVer > 5 || BrowserInfo.MinorVer >= 5 ) ;



// Set the dialog tabs.
window.parent.AddTab( 'Type', 'Type' ) ;
window.parent.AddTab( 'Background', 'Background' ) ;
window.parent.AddTab( 'Block', 'Block' ) ;
window.parent.AddTab( 'Box', 'Box' ) ;
window.parent.AddTab( 'Border', 'Border' ) ;
window.parent.AddTab( 'List', 'List' ) ;
window.parent.AddTab( 'Positioning', 'Positioning' ) ;
window.parent.AddTab( 'Target', 'Target' ) ;

// Function called when a dialog tag is selected.
function OnDialogTabChange( tabCode )
{
	ShowE('divType'		, ( tabCode == 'Type' ) ) ;
	ShowE('divBackground'	, ( tabCode == 'Background' ) ) ;
	ShowE('divBlock'	, ( tabCode == 'Block' ) ) ;
	ShowE('divBox'	, ( tabCode == 'Box' ) ) ;
	ShowE('divBorder'	, ( tabCode == 'Border' ) ) ;
	ShowE('divList'	, ( tabCode == 'List' ) ) ;
	ShowE('divPositioning'	, ( tabCode == 'Positioning' ) ) ;
	ShowE('divExtensions'	, ( tabCode == 'Extensions' ) ) ;
	ShowE('divTarget'	, ( tabCode == 'Target' ) ) ;
}


function GetE( elementId )
{
	return document.getElementById( elementId )  ;
}

function ShowE( element, isVisible )
{
	if ( typeof( element ) == 'string' )
		element = GetE( element ) ;
	element.style.display = isVisible ? '' : 'none' ;
}

function SetAttribute( element, attName, attValue )
{
	if ( attValue == null || attValue.length == 0 )
		element.removeAttribute( attName, 0 ) ;			// 0 : Case Insensitive
	else
		element.setAttribute( attName, attValue, 0 ) ;	// 0 : Case Insensitive

}

function GetAttribute( element, attName, valueIfNull )
{
	var oAtt = element.attributes[attName] ;
	
	if ( oAtt == null || !oAtt.specified )
		return valueIfNull ;
		
	var oValue = element.getAttribute( attName, 2 ) ;
	
	return ( oValue == null ? valueIfNull : oValue ) ;
}

String.prototype.startsWith = function( value )
{
	return ( this.substr( 0, value.length ) == value ) ;
}

String.prototype.remove = function( start, length )
{
	var s = '' ;
	
	if ( start > 0 )
		s = this.substring( 0, start ) ;
	
	if ( start + length < this.length )
		s += this.substring( start + length , this.length ) ;
		
	return s ;
}
