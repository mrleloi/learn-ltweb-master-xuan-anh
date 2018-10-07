/*
 * CssEditor plugin for FCKEditor.
 * Copyright (C) 2004 by Dmitriy Barbasura (delta3@pisem.net)
 * 
 * Licensed under the terms of the GNU Lesser General Public License:
 * 		http://www.opensource.org/licenses/lgpl-license.php
 * 
 */

var Update=new Object();

Update.textDecorationNone = function ()
{
	if (document.getElementById("textDecoration_none").checked==true)
	{
		document.getElementById("textDecoration_underline").checked=false;
		document.getElementById("textDecoration_overline").checked=false;
		document.getElementById("textDecoration_line_through").checked=false;
		document.getElementById("textDecoration_blink").checked=false;
	}
}

Update.textDecorationChange = function (object)
{
	if (object.checked==true)
		document.getElementById("textDecoration_none").checked=false;
}

Update.filterTypeChange = function (object)
{
	document.getElementById("filter").value=document.getElementById("filterType").value;
}

Update.colorPreview = function (object)
{
	if (Validator.color(object.value))
		document.getElementById("colorPreview").style.backgroundColor=object.value;
	else
		document.getElementById("colorPreview").style.backgroundColor="";
}

Update.backgroundColorPreview = function (object)
{
	if (Validator.color(object.value))
		document.getElementById("backgroundColorPreview").style.backgroundColor=object.value;
	else
		document.getElementById("backgroundColorPreview").style.backgroundColor="";
}

Update.borderTopColorPreview = function (object)
{
	if (Validator.color(object.value))
		document.getElementById("borderTopColorPreview").style.backgroundColor=object.value;
	else
		document.getElementById("borderTopColorPreview").style.backgroundColor="";
}

Update.borderRightColorPreview = function (object)
{
	if (Validator.color(object.value))
		document.getElementById("borderRightColorPreview").style.backgroundColor=object.value;
	else
		document.getElementById("borderRightColorPreview").style.backgroundColor="";
}

Update.borderBottomColorPreview = function (object)
{
	if (Validator.color(object.value))
		document.getElementById("borderBottomColorPreview").style.backgroundColor=object.value;
	else
		document.getElementById("borderBottomColorPreview").style.backgroundColor="";
}

Update.borderLeftColorPreview = function (object)
{
	if (Validator.color(object.value))
		document.getElementById("borderLeftColorPreview").style.backgroundColor=object.value;
	else
		document.getElementById("borderLeftColorPreview").style.backgroundColor="";
}

Update.setColorPreview = function (value)
{
	if (Validator.color(value))
		document.getElementById("color").value=value;
	else
		document.getElementById("color").value="";

	var object=new Object();
	object.value=value;
	Update.colorPreview(object);
} 

Update.setBackgroundColorPreview = function (value)
{
	if (Validator.color(value))
		document.getElementById("backgroundColor").value=value;
	else
		document.getElementById("backgroundColor").value="";

	var object=new Object();
	object.value=value;
	Update.backgroundColorPreview(object);
} 

Update.setBorderTopColorPreview = function (value)
{
	if (Validator.color(value))
		document.getElementById("borderTopColorPreview").value=value;
	else
		document.getElementById("borderTopColorPreview").value="";

	var object=new Object();
	object.value=value;
	Update.borderTopColorPreview(object);
}

Update.setBorderRightColorPreview = function (value)
{
	if (Validator.color(value))
		document.getElementById("borderRightColorPreview").value=value;
	else
		document.getElementById("borderRightColorPreview").value="";

	var object=new Object();
	object.value=value;
	Update.borderRightColorPreview(object);
}

Update.setBorderBottomColorPreview = function (value)
{
	if (Validator.color(value))
		document.getElementById("borderBottomColorPreview").value=value;
	else
		document.getElementById("borderBottomColorPreview").value="";
		
	var object=new Object();
	object.value=value;
	Update.borderBottomColorPreview(object);
}

Update.setBorderLeftColorPreview = function (value)
{
	if (Validator.color(value))
		document.getElementById("borderLeftColorPreview").value=value;
	else
		document.getElementById("borderLeftColorPreview").value="";

	var object=new Object();
	object.value=value;
	Update.borderLeftColorPreview(object);
}

Update.colorPreviewDialog = function (object)
{
	CSSDialog.OpenDialog("Dialog_Color","Select Color","dialogs/colorselector.html",400,330, Update.setColorPreview);
}

Update.backgroundColorPreviewDialog = function (object)
{
	CSSDialog.OpenDialog("Dialog_Color","Select Color","dialogs/colorselector.html",400,330, Update.setBackgroundColorPreview);
}

Update.borderTopColorPreviewDialog = function (object)
{
	CSSDialog.OpenDialog("Dialog_Color","Select Color","dialogs/colorselector.html",400,330, Update.setBorderTopColorPreview);
}

Update.borderRightColorPreviewDialog = function (object)
{
	CSSDialog.OpenDialog("Dialog_Color","Select Color","dialogs/colorselector.html",400,330, Update.setBorderRightColorPreview);
}

Update.borderBottomColorPreviewDialog = function (object)
{
	CSSDialog.OpenDialog("Dialog_Color","Select Color","dialogs/colorselector.html",400,330, Update.setBorderBottomColorPreview);
}

Update.borderLeftColorPreviewDialog = function (object)
{
	CSSDialog.OpenDialog("Dialog_Color","Select Color","dialogs/colorselector.html",400,330, Update.setBorderLeftColorPreview);
}