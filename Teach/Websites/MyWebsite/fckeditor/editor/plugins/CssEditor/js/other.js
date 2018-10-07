/*
 * CssEditor plugin for FCKEditor.
 * Copyright (C) 2004 by Dmitriy Barbasura (delta3@pisem.net)
 * 
 * Licensed under the terms of the GNU Lesser General Public License:
 * 		http://www.opensource.org/licenses/lgpl-license.php
 * 
 */

var Other = new Object();

Other.fontFamily = function (object)
{
	if (object.checked==true)
	{
		document.getElementById("fontFamily_value").disabled=false;
		document.getElementById("fontFamily_value").focus();
		document.getElementById("fontFamily").disabled=true;
	}
	else
	{
		document.getElementById("fontFamily").disabled=false;
		document.getElementById("fontFamily").focus();
		document.getElementById("fontFamily_value").disabled=true;
	}
}

Other.fontSize = function (object)
{
	if (object.checked==true)
	{
		document.getElementById("fontSize_value").disabled=false;
		document.getElementById("fontSize_unit").disabled=false;
		document.getElementById("fontSize_value").focus();
		document.getElementById("fontSize").disabled=true;
	}
	else
	{
		document.getElementById("fontSize").disabled=false;
		document.getElementById("fontSize").focus();
		document.getElementById("fontSize_value").disabled=true;
		document.getElementById("fontSize_unit").disabled=true;
	}
}

Other.lineHeight = function (object)
{
	if (object.checked==true)
	{
		document.getElementById("lineHeight_value").disabled=false;
		document.getElementById("lineHeight_unit").disabled=false;
		document.getElementById("lineHeight_value").focus();
		document.getElementById("lineHeight_normal").disabled=true;
	}
	else
	{
		document.getElementById("lineHeight_normal").disabled=false;
		document.getElementById("lineHeight_normal").focus();
		document.getElementById("lineHeight_value").disabled=true;
		document.getElementById("lineHeight_unit").disabled=true;
	}
}

Other.backgroundImage = function (object)
{
	if (object.checked==true)
	{
		document.getElementById("backgroundImage_value").disabled=false;
		document.getElementById("backgroundImage_value").focus();
		document.getElementById("backgroundImage_none").disabled=true;
	}
	else
	{
		document.getElementById("backgroundImage_none").disabled=false;
		document.getElementById("backgroundImage_none").focus();
		document.getElementById("backgroundImage_value").disabled=true;
	}
}

Other.horizontalPosition = function (object)
{
	if (object.checked==true)
	{
		document.getElementById("horizontalPosition_value").disabled=false;
		document.getElementById("horizontalPosition_unit").disabled=false;
		document.getElementById("horizontalPosition_value").focus();
		document.getElementById("horizontalPosition").disabled=true;
	}
	else
	{
		document.getElementById("horizontalPosition").disabled=false;
		document.getElementById("horizontalPosition").focus();
		document.getElementById("horizontalPosition_value").disabled=true;
		document.getElementById("horizontalPosition_unit").disabled=true;
	}
}

Other.verticalPosition = function (object)
{
	if (object.checked==true)
	{
		document.getElementById("verticalPosition_value").disabled=false;
		document.getElementById("verticalPosition_unit").disabled=false;
		document.getElementById("verticalPosition_value").focus();
		document.getElementById("verticalPosition").disabled=true;
	}
	else
	{
		document.getElementById("verticalPosition").disabled=false;
		document.getElementById("verticalPosition").focus();
		document.getElementById("verticalPosition_value").disabled=true;
		document.getElementById("verticalPosition_unit").disabled=true;
	}
}


Other.verticalAlign = function (object)
{
	if (object.checked==true)
	{
		document.getElementById("verticalAlign_value").disabled=false;
		document.getElementById("verticalAlign_value").focus();
		document.getElementById("verticalAlign").disabled=true;
	}
	else
	{
		document.getElementById("verticalAlign").disabled=false;
		document.getElementById("verticalAlign").focus();
		document.getElementById("verticalAlign_value").disabled=true;
	}
}

Other.width = function (object)
{
	if (object.checked==true)
	{
		document.getElementById("width_value").disabled=false;
		document.getElementById("width_unit").disabled=false;
		document.getElementById("width_value").focus();
		document.getElementById("width_auto").disabled=true;
	}
	else
	{
		document.getElementById("width_auto").disabled=false;
		document.getElementById("width_auto").focus();
		document.getElementById("width_value").disabled=true;
		document.getElementById("width_unit").disabled=true;
	}
}

Other.height = function (object)
{
	if (object.checked==true)
	{
		document.getElementById("height_value").disabled=false;
		document.getElementById("height_unit").disabled=false;
		document.getElementById("height_value").focus();
		document.getElementById("height_auto").disabled=true;
	}
	else
	{
		document.getElementById("height_auto").disabled=false;
		document.getElementById("height_auto").focus();
		document.getElementById("height_value").disabled=true;
		document.getElementById("height_unit").disabled=true;
	}
}

Other.paddingAll = function (object)
{
	if (object.checked==true)
	{
		document.getElementById("paddingRight").disabled=true;
		document.getElementById("paddingBottom").disabled=true;
		document.getElementById("paddingLeft").disabled=true;
		document.getElementById("paddingRight_unit").disabled=true;
		document.getElementById("paddingBottom_unit").disabled=true;
		document.getElementById("paddingLeft_unit").disabled=true;
	}
	else
	{
		document.getElementById("paddingRight").disabled=false;
		document.getElementById("paddingBottom").disabled=false;
		document.getElementById("paddingLeft").disabled=false;
		document.getElementById("paddingRight_unit").disabled=false;
		document.getElementById("paddingBottom_unit").disabled=false;
		document.getElementById("paddingLeft_unit").disabled=false;
	}
}

Other.marginAll = function (object)
{
	if (object.checked==true)
	{
		document.getElementById("marginRight").disabled=true;
		document.getElementById("marginBottom").disabled=true;
		document.getElementById("marginLeft").disabled=true;
		document.getElementById("marginRight_unit").disabled=true;
		document.getElementById("marginBottom_unit").disabled=true;
		document.getElementById("marginLeft_unit").disabled=true;
	}
	else
	{
		document.getElementById("marginRight").disabled=false;
		document.getElementById("marginBottom").disabled=false;
		document.getElementById("marginLeft").disabled=false;
		document.getElementById("marginRight_unit").disabled=false;
		document.getElementById("marginBottom_unit").disabled=false;
		document.getElementById("marginLeft_unit").disabled=false;
	}
}

Other.borderStyleAll = function (object)
{
	if (object.checked==true)
	{
		document.getElementById("borderRightStyle").disabled=true;
		document.getElementById("borderBottomStyle").disabled=true;
		document.getElementById("borderLeftStyle").disabled=true;
	}
	else
	{
		document.getElementById("borderRightStyle").disabled=false;
		document.getElementById("borderBottomStyle").disabled=false;
		document.getElementById("borderLeftStyle").disabled=false;
	}
}

Other.borderWidthAll = function (object)
{
	if (object.checked==true)
	{
		document.getElementById("borderRightWidth").disabled=true;
		document.getElementById("borderBottomWidth").disabled=true;
		document.getElementById("borderLeftWidth").disabled=true;
		document.getElementById("borderRightWidth_unit").disabled=true;
		document.getElementById("borderBottomWidth_unit").disabled=true;
		document.getElementById("borderLeftWidth_unit").disabled=true;
	}
	else
	{
		document.getElementById("borderRightWidth").disabled=false;
		document.getElementById("borderBottomWidth").disabled=false;
		document.getElementById("borderLeftWidth").disabled=false;
		document.getElementById("borderRightWidth_unit").disabled=false;
		document.getElementById("borderBottomWidth_unit").disabled=false;
		document.getElementById("borderLeftWidth_unit").disabled=false;
	}
}

Other.borderColorAll = function (object)
{
	if (object.checked==true)
	{
		document.getElementById("borderRightColor").disabled=true;
		document.getElementById("borderBottomColor").disabled=true;
		document.getElementById("borderLeftColor").disabled=true;
	}
	else
	{
		document.getElementById("borderRightColor").disabled=false;
		document.getElementById("borderBottomColor").disabled=false;
		document.getElementById("borderLeftColor").disabled=false;
	}
}

Other.ZIndex = function (object)
{
	if (object.checked==true)
	{
		document.getElementById("zIndex_value").disabled=false;
		document.getElementById("zIndex_value").focus();
		document.getElementById("zIndex_auto").disabled=true;
	}
	else
	{
		document.getElementById("zIndex_auto").disabled=false;
		document.getElementById("zIndex_auto").focus();
		document.getElementById("zIndex_value").disabled=true;
	}
}

Other.clipTop = function (object)
{
	if (object.checked==true)
	{
		document.getElementById("clipTop_value").disabled=false;
		document.getElementById("clipTop_unit").disabled=false;
		document.getElementById("clipTop_value").focus();
		document.getElementById("clipTop_auto").disabled=true;
	}
	else
	{
		document.getElementById("clipTop_auto").disabled=false;
		document.getElementById("clipTop_auto").focus();
		document.getElementById("clipTop_value").disabled=true;
		document.getElementById("clipTop_unit").disabled=true;
	}
}

Other.clipRight = function (object)
{
	if (object.checked==true)
	{
		document.getElementById("clipRight_value").disabled=false;
		document.getElementById("clipRight_unit").disabled=false;
		document.getElementById("clipRight_value").focus();
		document.getElementById("clipRight_auto").disabled=true;
	}
	else
	{
		document.getElementById("clipRight_auto").disabled=false;
		document.getElementById("clipRight_auto").focus();
		document.getElementById("clipRight_value").disabled=true;
		document.getElementById("clipRight_unit").disabled=true;
	}
}

Other.clipBottom = function (object)
{
	if (object.checked==true)
	{
		document.getElementById("clipBottom_value").disabled=false;
		document.getElementById("clipBottom_unit").disabled=false;
		document.getElementById("clipBottom_value").focus();
		document.getElementById("clipBottom_auto").disabled=true;
	}
	else
	{
		document.getElementById("clipBottom_auto").disabled=false;
		document.getElementById("clipBottom_auto").focus();
		document.getElementById("clipBottom_value").disabled=true;
		document.getElementById("clipBottom_unit").disabled=true;
	}
}

Other.clipLeft = function (object)
{
	if (object.checked==true)
	{
		document.getElementById("clipLeft_value").disabled=false;
		document.getElementById("clipLeft_unit").disabled=false;
		document.getElementById("clipLeft_value").focus();
		document.getElementById("clipLeft_auto").disabled=true;
	}
	else
	{
		document.getElementById("clipLeft_auto").disabled=false;
		document.getElementById("clipLeft_auto").focus();
		document.getElementById("clipLeft_value").disabled=true;
		document.getElementById("clipLeft_unit").disabled=true;
	}
}

Other.cursor = function (object)
{
	if (object.checked==true)
	{
		document.getElementById("cursor_value").disabled=false;
		document.getElementById("cursor_value").focus();
		document.getElementById("cursor").disabled=true;
	}
	else
	{
		document.getElementById("cursor").disabled=false;
		document.getElementById("cursor").focus();
		document.getElementById("cursor_value").disabled=true;
	}
}

Other.targetTag = function (object)
{
	if (object.checked==true)
	{
		document.getElementById("targetTag_value").disabled=false;
		document.getElementById("targetTag_value").focus();
		document.getElementById("targetTag").disabled=true;
	}
	else
	{
		document.getElementById("targetTag").disabled=false;
		document.getElementById("targetTag").focus();
		document.getElementById("targetTag_value").disabled=true;
	}
}
