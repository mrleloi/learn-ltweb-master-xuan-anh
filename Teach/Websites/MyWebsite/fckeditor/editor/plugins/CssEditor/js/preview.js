/*
 * CssEditor plugin for FCKEditor.
 * Copyright (C) 2004 by Dmitriy Barbasura (delta3@pisem.net)
 * 
 * Licensed under the terms of the GNU Lesser General Public License:
 * 		http://www.opensource.org/licenses/lgpl-license.php
 * 
 */

var Preview=new Object();
Preview.sId="Preview";
Preview.content=document.getElementById(Preview.sId);
Preview.sValue="";
Preview.sTarget="";
Preview.sEndLine=";\n";
Preview.sFloat="";


Preview.change = function ()
{
	window.status="";
	Preview.content=document.getElementById(Preview.sId);

	this.setFontFamily();
	this.setFontSize();
	this.setLineHeight();
        this.setTextDecoration();
	this.setBackgroundImage();
	this.setBackgroundPosition();
	this.setWordSpacing();
	this.setLetterSpacing();
	this.setVerticalAlign();
	this.setTextIndent();
	this.setDisplay();
	this.setWidth();
	this.setHeight();
	this.setPadding();
	this.setMargin();
	this.setBorderStyle();
	this.setBorderWidth();
	this.setBorderColor();
	this.setListStyleImage();
	this.setZIndex();
	this.setTop();
	this.setRight();
	this.setBottom();
	this.setLeft();
	this.setClip();
	this.setPageBreakBefore();
	this.setPageBreakAfter();
	this.setCursor();
	this.setFilter();
	this.setFloat();
	this.setFontStyle();
	this.setFontWeight();
	this.setFontVariant();
	this.setTextTransform();
	this.setColor();
	this.setBackgroundColor();
	this.setBackgroundRepeat();
	this.setBackgroundAttachment();
	this.setTextAlign();
	this.setWhiteSpace();
	this.setClear();
	this.setListStyleType();
	this.setListStylePosition();
	this.setPosition();
	this.setVisibility();
	this.setOverflow();

	this.showCode();
	document.getElementById('btnInsert').disabled = ( document.getElementById('Code').value.length == 0 ) ;
}

Preview.prepareCode = function ()
{
	if (this.content.style.fontFamily.length>0)
		this.sValue+="font-family: "+this.content.style.fontFamily+this.sEndLine;
	if (this.content.style.fontSize.length>0)
		this.sValue+="font-size: "+this.content.style.fontSize+this.sEndLine;
	if (this.content.style.color.length>0)
		this.sValue+="color: "+this.content.style.color+this.sEndLine;
	if (this.content.style.fontStyle.length>0)
		this.sValue+="font-style: "+this.content.style.fontStyle+this.sEndLine;
	if (this.content.style.fontWeight.length>0)
		this.sValue+="font-weight: "+this.content.style.fontWeight+this.sEndLine;
	if (this.content.style.fontVariant.length>0)
		this.sValue+="font-variant: "+this.content.style.fontVariant+this.sEndLine;
	if (this.content.style.textTransform.length>0)
		this.sValue+="text-transform: "+this.content.style.textTransform+this.sEndLine;
	if (this.content.style.textDecoration.length>0)
		this.sValue+="text-decoration: "+this.content.style.textDecoration+this.sEndLine;
	if (this.content.style.lineHeight.length>0)
		this.sValue+="line-height: "+this.content.style.lineHeight+this.sEndLine;
	if (this.content.style.backgroundColor.length>0)
		this.sValue+="background-color: "+this.content.style.backgroundColor+this.sEndLine;
	if (this.content.style.backgroundImage.length>0)
		this.sValue+="background-image: "+this.content.style.backgroundImage+this.sEndLine;
	if (this.content.style.backgroundRepeat.length>0)
		this.sValue+="background-repeat: "+this.content.style.backgroundRepeat+this.sEndLine;
	if (this.content.style.backgroundAttachment.length>0)
		this.sValue+="background-attachment: "+this.content.style.backgroundAttachment+this.sEndLine;
	if (this.content.style.backgroundPosition.length>0 && this.content.style.backgroundPosition!=" 50%")
		this.sValue+="background-position: "+this.content.style.backgroundPosition+this.sEndLine;
	if (this.content.style.wordSpacing.length>0)
		this.sValue+="word-spacing: "+this.content.style.wordSpacing+this.sEndLine;
	if (this.content.style.letterSpacing.length>0)
		this.sValue+="letter-spacing: "+this.content.style.letterSpacing+this.sEndLine;
	if (this.content.style.verticalAlign.length>0)
		this.sValue+="vertical-align: "+this.content.style.verticalAlign+this.sEndLine;
	if (this.content.style.textAlign.length>0)
		this.sValue+="text-align: "+this.content.style.textAlign+this.sEndLine;
	if (this.content.style.textIndent.length>0)
		this.sValue+="text-indent: "+this.content.style.textIndent+this.sEndLine;
	if (this.content.style.whiteSpace.length>0)
		this.sValue+="white-space: "+this.content.style.whiteSpace+this.sEndLine;
	if (this.content.style.display.length>0)
		this.sValue+="display: "+this.content.style.display+this.sEndLine;
	if (this.content.style.width.length>0)
		this.sValue+="width: "+this.content.style.width+this.sEndLine;
	if (this.content.style.height.length>0)
		this.sValue+="height: "+this.content.style.height+this.sEndLine;
	if (BrowserInfo.IsIE==false)
	{
		if (this.content.style["float"].length>0)
			this.sValue+="float: "+this.content.style["float"]+this.sEndLine;
	}
	else
	{
		if (this.sFloat.length>0)
			this.sValue+="float: "+this.sFloat+this.sEndLine;
	}
	if (this.content.style.clear.length>0)
		this.sValue+="clear: "+this.content.style.clear+this.sEndLine;
	if (document.getElementById("padding_all").checked==true)
	{
		if (this.content.style.padding.length>0)
			this.sValue+="padding: "+this.content.style.padding+this.sEndLine;
	}
	else
	{
		if (this.content.style.paddingTop.length>0)
			this.sValue+="padding-top: "+this.content.style.paddingTop+this.sEndLine;
		if (this.content.style.paddingRight.length>0)
			this.sValue+="padding-right: "+this.content.style.paddingRight+this.sEndLine;
		if (this.content.style.paddingBottom.length>0)
			this.sValue+="padding-bottom: "+this.content.style.paddingBottom+this.sEndLine;
		if (this.content.style.paddingLeft.length>0)
			this.sValue+="padding-left: "+this.content.style.paddingLeft+this.sEndLine;
	}
	if (document.getElementById("margin_all").checked==true)
	{
		if (this.content.style.margin.length>0)
			this.sValue+="margin: "+this.content.style.margin+this.sEndLine;
	}
	else
	{
		if (this.content.style.marginTop.length>0)
			this.sValue+="margin-top: "+this.content.style.marginTop+this.sEndLine;
		if (this.content.style.marginRight.length>0)
			this.sValue+="margin-right: "+this.content.style.marginRight+this.sEndLine;
		if (this.content.style.marginBottom.length>0)
			this.sValue+="margin-bottom: "+this.content.style.marginBottom+this.sEndLine;
		if (this.content.style.marginLeft.length>0)
			this.sValue+="margin-left: "+this.content.style.marginLeft+this.sEndLine;
	}
	if (document.getElementById("borderStyle_all").checked==true)
	{
		if (this.content.style.borderStyle.length>0)
			this.sValue+="border-style: "+this.content.style.borderStyle+this.sEndLine;
	}
	else
	{
		if (this.content.style.borderTopStyle.length>0)
			this.sValue+="border-top-style: "+this.content.style.borderTopStyle+this.sEndLine;
		if (this.content.style.borderRightStyle.length>0)
			this.sValue+="border-right-style: "+this.content.style.borderRightStyle+this.sEndLine;
		if (this.content.style.borderBottomStyle.length>0)
			this.sValue+="border-bottom-style: "+this.content.style.borderBottomStyle+this.sEndLine;
		if (this.content.style.borderLeftStyle.length>0)
			this.sValue+="border-left-style: "+this.content.style.borderLeftStyle+this.sEndLine;
	}
	if (document.getElementById("borderWidth_all").checked==true)
	{
		if (this.content.style.borderWidth.length>0)
			this.sValue+="border-width: "+this.content.style.borderWidth+this.sEndLine;
	}
	else
	{
		if (this.content.style.borderTopWidth.length>0)
			this.sValue+="border-top-width: "+this.content.style.borderTopWidth+this.sEndLine;
		if (this.content.style.borderRightWidth.length>0)
			this.sValue+="border-right-width: "+this.content.style.borderRightWidth+this.sEndLine;
		if (this.content.style.borderBottomWidth.length>0)
			this.sValue+="border-bottom-width: "+this.content.style.borderBottomWidth+this.sEndLine;
		if (this.content.style.borderLeftWidth.length>0)
			this.sValue+="border-left-width: "+this.content.style.borderLeftWidth+this.sEndLine;
	}
	if (document.getElementById("borderColor_all").checked==true)
	{
		if (this.content.style.borderColor.length>0)
			this.sValue+="border-color: "+this.content.style.borderColor+this.sEndLine;
	}
	else
	{
		if (this.content.style.borderTopColor.length>0)
			this.sValue+="border-top-color: "+this.content.style.borderTopColor+this.sEndLine;
		if (this.content.style.borderRightColor.length>0)
			this.sValue+="border-right-color: "+this.content.style.borderRightColor+this.sEndLine;
		if (this.content.style.borderBottomColor.length>0)
			this.sValue+="border-bottom-color: "+this.content.style.borderBottomColor+this.sEndLine;
		if (this.content.style.borderLeftColor.length>0)
			this.sValue+="border-left-color: "+this.content.style.borderLeftColor+this.sEndLine;
	}
	if (this.content.style.listStyleType.length>0)
		this.sValue+="list-style-type: "+this.content.style.listStyleType+this.sEndLine;
	if (this.content.style.listStyleImage.length>0)
		this.sValue+="list-style-image: "+this.content.style.listStyleImage+this.sEndLine;
	if (this.content.style.listStylePosition.length>0)
		this.sValue+="list-style-position: "+this.content.style.listStylePosition+this.sEndLine;
	if (this.content.style.position.length>0)
		this.sValue+="position: "+this.content.style.position+this.sEndLine;
	if (this.content.style.visibility.length>0)
		this.sValue+="visibility: "+this.content.style.visibility+this.sEndLine;
	if (this.content.style.zIndex>0)
		this.sValue+="z-index: "+this.content.style.zIndex+this.sEndLine;
	if (this.content.style.overflow.length>0)
		this.sValue+="overflow: "+this.content.style.overflow+this.sEndLine;
	if (this.content.style.top.length>0)
		this.sValue+="top: "+this.content.style.top+this.sEndLine;
	if (this.content.style.right.length>0)
		this.sValue+="right: "+this.content.style.right+this.sEndLine;
	if (this.content.style.bottom.length>0)
		this.sValue+="bottom: "+this.content.style.bottom+this.sEndLine;
	if (this.content.style.left.length>0)
		this.sValue+="left: "+this.content.style.left+this.sEndLine;
	if (this.content.style.clip.toString().match("rect\\(auto auto auto auto\\)")==null && this.content.style.clip.toString().match("rect\\(auto, auto, auto, auto\\)")==null)
		this.sValue+="clip: "+this.content.style.clip+this.sEndLine;
	if (this.content.style.cursor.length>0)
		this.sValue+="cursor: "+this.content.style.cursor+this.sEndLine;
	if (BrowserInfo.IsIE)
	{
		if (this.content.style.pageBreakBefore.length>0)
			this.sValue+="page-break-before: "+this.content.style.pageBreakBefore+this.sEndLine;
		if (this.content.style.pageBreakAfter.length>0)
			this.sValue+="page-break-after: "+this.content.style.pageBreakAfter+this.sEndLine;
		if (this.content.style.filter.length>0)
			this.sValue+="filter: "+this.content.style.filter+this.sEndLine;
	}
}

Preview.showCode = function ()
{
	this.sTarget="";
	this.sValue="";
	
	if (this.isTarget()==true)
	{
		this.sValue="\t";
		this.sEndLine+="\t";
	}

	this.prepareCode();
	

	if (this.isTarget()==true)
	{
		if (document.getElementById("targetTag_other").checked==true)
	        	this.sTarget=document.getElementById("targetTag_value").value;			
		else
	        	this.sTarget=document.getElementById("targetTag").value;

		if (document.getElementById("targetClass").value.length>0)
	        	this.sTarget+="."+document.getElementById("targetClass").value;

		if (document.getElementById("targetId").value.length>0)
	        	this.sTarget+="#"+document.getElementById("targetId").value;

		if (document.getElementById("targetElement").value.length>0)
	        	this.sTarget+=":"+document.getElementById("targetId").value;

		this.sTarget+=" {\n";
		this.sValue+="}";
	}


	document.getElementById("Code").value=this.sTarget+this.sValue;
}

Preview.setFontFamily = function ()
{
	if (document.getElementById("fontFamily_other").checked==true)
	{
		this.content.style.fontFamily=document.getElementById("fontFamily_value").value;
	}
	else
	{
		this.content.style.fontFamily=document.getElementById("fontFamily").value;
	}
}

Preview.setFontSize = function ()
{
	if (document.getElementById("fontSize_other").checked==true)
	{
		if (Validator.number(document.getElementById("fontSize_value").value))
			this.content.style.fontSize=""+document.getElementById("fontSize_value").value+document.getElementById("fontSize_unit").value;
		else
			this.content.style.fontSize="";
	}
	else
	{
		this.content.style.fontSize=document.getElementById("fontSize").value;
	}
}

Preview.setLineHeight = function ()
{
	if (document.getElementById("lineHeight_other").checked==true)
	{
		if (Validator.number(document.getElementById("lineHeight_value").value))
			this.content.style.lineHeight=""+document.getElementById("lineHeight_value").value+document.getElementById("lineHeight_unit").value;
		else
			this.content.style.lineHeight="";
	}
	else
	{
		if (document.getElementById("lineHeight_normal").checked==true)
			this.content.style.lineHeight=document.getElementById("lineHeight_normal").value;
		else
			this.content.style.lineHeight="";
	}
}

Preview.setTextDecoration = function ()
{
	var sValue="";
	if (document.getElementById("textDecoration_underline").checked==true)
		sValue+=document.getElementById("textDecoration_underline").value+" ";

	if (document.getElementById("textDecoration_overline").checked==true)
		sValue+=document.getElementById("textDecoration_overline").value+" ";

	if (document.getElementById("textDecoration_line_through").checked==true)
		sValue+=document.getElementById("textDecoration_line_through").value+" ";

	if (document.getElementById("textDecoration_blink").checked==true)
		sValue+=document.getElementById("textDecoration_blink").value+" ";

	if (document.getElementById("textDecoration_none").checked==true)
		sValue+=document.getElementById("textDecoration_none").value+" ";

	this.content.style.textDecoration=sValue;
}

Preview.setBackgroundImage = function ()
{
	if (document.getElementById("backgroundImage_other").checked==true)
	{
		this.content.style.backgroundImage="url("+document.getElementById("backgroundImage_value").value+")";
	}
	else
	{
		if (document.getElementById("backgroundImage_none").checked==true)
			this.content.style.backgroundImage=document.getElementById("backgroundImage_none").value;
		else
			this.content.style.backgroundImage="";
	}
}

Preview.setBackgroundPosition = function ()
{
	var sValue="";

	if (document.getElementById("horizontalPosition_other").checked==true)
	{
		if (Validator.number(document.getElementById("horizontalPosition_value").value))
			sValue=""+document.getElementById("horizontalPosition_value").value+document.getElementById("horizontalPosition_unit").value;
	}
	else
	{
		sValue=document.getElementById("horizontalPosition").value;
	}

	if (document.getElementById("verticalPosition_other").checked==true)
	{
		if (Validator.number(document.getElementById("verticalPosition_value").value))
			sValue+=" "+document.getElementById("verticalPosition_value").value+document.getElementById("verticalPosition_unit").value;
	}
	else
	{
		sValue+=" "+document.getElementById("verticalPosition").value;
	}

	this.content.style.backgroundPosition=sValue;
}

Preview.setWordSpacing = function ()
{
	if (Validator.number(document.getElementById("wordSpacing").value))
	{
		this.content.style.wordSpacing=""+document.getElementById("wordSpacing").value+document.getElementById("wordSpacing_unit").value;
	}
	else
	{
		this.content.style.wordSpacing="";
	}
}

Preview.setLetterSpacing = function ()
{
	if (Validator.number(document.getElementById("letterSpacing").value))
	{
		this.content.style.letterSpacing=""+document.getElementById("letterSpacing").value+document.getElementById("letterSpacing_unit").value;
	}
	else
	{
		this.content.style.letterSpacing="";
	}
}

Preview.setVerticalAlign = function ()
{
	if (document.getElementById("verticalAlign_other").checked==true)
	{
		if (Validator.number(document.getElementById("verticalAlign_value").value))
			this.content.style.verticalAlign=""+document.getElementById("verticalAlign_value").value+"%";
		else
			this.content.style.verticalAlign="";
	}
	else
	{
		this.content.style.verticalAlign=document.getElementById("verticalAlign").value;
	}
}

Preview.setTextIndent = function ()
{
	if (Validator.number(document.getElementById("textIndent").value))
	{
		this.content.style.textIndent=""+document.getElementById("textIndent").value+document.getElementById("textIndent_unit").value;
	}
	else
	{
		this.content.style.textIndent="";
	}
}

Preview.setDisplay = function ()
{
	try
	{
		this.content.style.display=document.getElementById("display").value;
	}
	catch (e)
	{
		window.status=""+e;
	}
}

Preview.setWidth = function ()
{
	if (document.getElementById("width_other").checked==true)
	{
		if (Validator.number(document.getElementById("width_value").value))
			this.content.style.width=""+document.getElementById("width_value").value+document.getElementById("width_unit").value;
		else
			this.content.style.width="";
	}
	else
	{
		if (document.getElementById("width_auto").checked==true)
			this.content.style.width=document.getElementById("width_auto").value;
		else
			this.content.style.width="";
	}
}

Preview.setHeight = function ()
{
	if (document.getElementById("height_other").checked==true)
	{
		if (Validator.number(document.getElementById("height_value").value))
			this.content.style.height=document.getElementById("height_value").value+document.getElementById("height_unit").value;
		else
			this.content.style.height="";
	}
	else
	{
		if (document.getElementById("height_auto").checked==true)
			this.content.style.height=document.getElementById("height_auto").value;
		else
			this.content.style.height="";
	}
}

Preview.setPadding = function ()
{
	if (document.getElementById("padding_all").checked==true)
	{
		if (Validator.number(document.getElementById("paddingTop").value))
			this.content.style.padding=""+document.getElementById("paddingTop").value+document.getElementById("paddingTop_unit").value;
		else
			this.content.style.padding="";
	}
	else
	{
		if (Validator.number(document.getElementById("paddingTop").value))
			this.content.style.paddingTop=""+document.getElementById("paddingTop").value+document.getElementById("paddingTop_unit").value;
		else
			this.content.style.paddingTop="";

		if (Validator.number(document.getElementById("paddingRight").value))
			this.content.style.paddingRight=""+document.getElementById("paddingRight").value+document.getElementById("paddingRight_unit").value;
		else
			this.content.style.paddingRight="";

		if (Validator.number(document.getElementById("paddingBottom").value))
			this.content.style.paddingBottom=""+document.getElementById("paddingBottom").value+document.getElementById("paddingBottom_unit").value;
		else
			this.content.style.paddingBottom="";

		if (Validator.number(document.getElementById("paddingLeft").value))
			this.content.style.paddingLeft=""+document.getElementById("paddingLeft").value+document.getElementById("paddingLeft_unit").value;
		else
			this.content.style.paddingLeft="";
	}
}

Preview.setMargin = function ()
{
	if (document.getElementById("margin_all").checked==true)
	{
		if (Validator.number(document.getElementById("marginTop").value))
			this.content.style.margin=""+document.getElementById("marginTop").value+document.getElementById("marginTop_unit").value;
		else
			this.content.style.margin="";
	}
	else
	{
		if (Validator.number(document.getElementById("marginTop").value))
			this.content.style.marginTop=""+document.getElementById("marginTop").value+document.getElementById("marginTop_unit").value;
		else
			this.content.style.marginTop="";

		if (Validator.number(document.getElementById("marginRight").value))
			this.content.style.marginRight=""+document.getElementById("marginRight").value+document.getElementById("marginRight_unit").value;
		else
			this.content.style.marginRight="";

		if (Validator.number(document.getElementById("marginBottom").value))
			this.content.style.marginBottom=""+document.getElementById("marginBottom").value+document.getElementById("marginBottom_unit").value;
		else
			this.content.style.marginBottom="";

		if (Validator.number(document.getElementById("marginLeft").value))
			this.content.style.marginLeft=""+document.getElementById("marginLeft").value+document.getElementById("marginLeft_unit").value;
		else
			this.content.style.marginLeft="";
	}
}

Preview.setBorderStyle = function ()
{
	if (document.getElementById("borderStyle_all").checked==true)
	{
		this.content.style.borderStyle=document.getElementById("borderTopStyle").value;
	}
	else
	{
		this.content.style.borderTopStyle=document.getElementById("borderTopStyle").value;
		this.content.style.borderRightStyle=document.getElementById("borderRightStyle").value;
		this.content.style.borderBottomStyle=document.getElementById("borderBottomStyle").value;
		this.content.style.borderLeftStyle=document.getElementById("borderLeftStyle").value;
	}
}

Preview.setBorderWidth = function ()
{
	if (document.getElementById("borderWidth_all").checked==true)
	{
		if (Validator.number(document.getElementById("borderTopWidth").value))
			this.content.style.borderWidth=""+document.getElementById("borderTopWidth").value+document.getElementById("borderTopWidth_unit").value;
		else
			this.content.style.borderWidth="";
	}
	else
	{
		if (Validator.number(document.getElementById("borderTopWidth").value))
			this.content.style.borderTopWidth=""+document.getElementById("borderTopWidth").value+document.getElementById("borderTopWidth_unit").value;
		else
			this.content.style.borderTopWidth="";

		if (Validator.number(document.getElementById("borderRightWidth").value))
			this.content.style.borderRightWidth=""+document.getElementById("borderRightWidth").value+document.getElementById("borderRightWidth_unit").value;
		else
			this.content.style.borderRightWidth="";

		if (Validator.number(document.getElementById("borderBottomWidth").value))
			this.content.style.borderBottomWidth=""+document.getElementById("borderBottomWidth").value+document.getElementById("borderBottomWidth_unit").value;
		else
			this.content.style.borderBottomWidth="";

		if (Validator.number(document.getElementById("borderLeftWidth").value))
			this.content.style.borderLeftWidth=""+document.getElementById("borderLeftWidth").value+document.getElementById("borderLeftWidth_unit").value;
		else
			this.content.style.borderLeftWidth="";
	}
}

Preview.setBorderColor = function ()
{
	if (document.getElementById("borderColor_all").checked==true)
	{
		if (Validator.color(document.getElementById("borderTopColor").value))
			this.content.style.borderColor=document.getElementById("borderTopColor").value;
		else
			this.content.style.borderColor="";
	}
	else
	{
		if (Validator.color(document.getElementById("borderTopColor").value))
			this.content.style.borderTopColor=document.getElementById("borderTopColor").value;
		else
			this.content.style.borderTopColor="";
	
		if (Validator.color(document.getElementById("borderRightColor").value))
			this.content.style.borderRightColor=document.getElementById("borderRightColor").value;
		else
			this.content.style.borderRightColor="";

		if (Validator.color(document.getElementById("borderBottomColor").value))
			this.content.style.borderBottomColor=document.getElementById("borderBottomColor").value;
		else
			this.content.style.borderBottomColor="";

		if (Validator.color(document.getElementById("borderLeftColor").value))
			this.content.style.borderLeftColor=document.getElementById("borderLeftColor").value;
		else
			this.content.style.borderLeftColor="";
	}
}

Preview.setListStyleImage = function ()
{
	if (document.getElementById("listStyleImage").value.length>0)
	{
		this.content.style.listStyleImage="url("+document.getElementById("listStyleImage").value+")";
	}
	else
	{
		this.content.style.listStyleImage="";
	}
}

Preview.setZIndex = function ()
{
	if (document.getElementById("zIndex_other").checked==true)
	{
		if (Validator.number(document.getElementById("zIndex_value").value))
			this.content.style.zIndex=""+document.getElementById("zIndex_value").value;
	}
	else
	{
		this.content.style.zIndex="";
	}
}

Preview.setTop = function ()
{
	if (Validator.number(document.getElementById("top").value))
	{
		this.content.style.top=""+document.getElementById("top").value+document.getElementById("top_unit").value;
	}
	else
	{
		this.content.style.top="";
	}
}

Preview.setRight = function ()
{
	if (Validator.number(document.getElementById("right").value))
	{
		this.content.style.right=""+document.getElementById("right").value+document.getElementById("right_unit").value;
	}
	else
	{
		this.content.style.right="";
	}
}

Preview.setBottom = function ()
{
	if (Validator.number(document.getElementById("bottom").value))
	{
		this.content.style.bottom=""+document.getElementById("bottom").value+document.getElementById("right_unit").value;
	}
	else
	{
		this.content.style.bottom="";
	}
}

Preview.setLeft = function ()
{
	if (Validator.number(document.getElementById("left").value))
	{
		this.content.style.left=""+document.getElementById("left").value+document.getElementById("left_unit").value;
	}
	else
	{
		this.content.style.left="";
	}
}

Preview.setClip = function ()
{
	var sValue="rect(";

	if (document.getElementById("clipTop_other").checked==true && Validator.number(document.getElementById("clipTop_value").value))
	{
		sValue+=""+document.getElementById("clipTop_value").value+document.getElementById("clipTop_unit").value+" ";
	}
	else
	{
		sValue+=document.getElementById("clipTop_auto").value+" ";
	}
	if (document.getElementById("clipRight_other").checked==true && Validator.number(document.getElementById("clipRight_value").value))
	{
		sValue+=""+document.getElementById("clipRight_value").value+document.getElementById("clipRight_unit").value+" ";
	}
	else
	{
		sValue+=document.getElementById("clipRight_auto").value+" ";
	}
	if (document.getElementById("clipBottom_other").checked==true && Validator.number(document.getElementById("clipBottom_value").value))
	{
		sValue+=""+document.getElementById("clipBottom_value").value+document.getElementById("clipBottom_unit").value+" ";
	}
	else
	{
		sValue+=document.getElementById("clipBottom_auto").value+" ";
	}
	if (document.getElementById("clipLeft_other").checked==true && Validator.number(document.getElementById("clipLeft_value").value))
	{
		sValue+=""+document.getElementById("clipLeft_value").value+document.getElementById("clipLeft_unit").value+" ";
	}
	else
	{
		sValue+=document.getElementById("clipLeft_auto").value+" ";
	}

	sValue+=")";
	this.content.style.clip=sValue;
}


Preview.setPageBreakBefore = function ()
{
	if (BrowserInfo.IsIE==false)
	{
        	return;
	}
	this.content.style.pageBreakBefore=document.getElementById("pageBreakBefore").value;
}

Preview.setPageBreakAfter = function ()
{
	if (BrowserInfo.IsIE==false)
	{
        	return;
	}
	this.content.style.pageBreakAfter=document.getElementById("pageBreakAfter").value;
}

Preview.setCursor = function ()
{
	if (document.getElementById("clipLeft_other").checked==true)
	{
		this.content.style.cursor="url("+document.getElementById("cursor_value").value+")";
	}
	else
	{
		this.content.style.cursor=document.getElementById("cursor").value;
	}
}

Preview.setFilter = function ()
{
	if (BrowserInfo.IsIE==false)
	{
        	return;
	}
	this.content.style.filter=document.getElementById("filter").value;
}

Preview.setFloat = function ()
{
	if (BrowserInfo.IsIE)
	{
		this.sFloat=document.getElementById("float").value;
        	return;
	}
	this.content.style["float"]=document.getElementById("float").value;
}

Preview.setFontStyle = function ()
{
	this.content.style.fontStyle=document.getElementById("fontStyle").value;
}

Preview.setFontWeight = function ()
{
	this.content.style.fontWeight=document.getElementById("fontWeight").value;
}

Preview.setFontVariant = function ()
{
	this.content.style.fontVariant=document.getElementById("fontVariant").value;
}

Preview.setTextTransform = function ()
{
	this.content.style.textTransform=document.getElementById("textTransform").value;
}

Preview.setColor = function ()
{
	if (Validator.color(document.getElementById("color").value))
		this.content.style.color=document.getElementById("color").value;
	else
		this.content.style.color="";
}

Preview.setBackgroundColor = function ()
{
	if (Validator.color(document.getElementById("backgroundColor").value))
		this.content.style.backgroundColor=document.getElementById("backgroundColor").value;
	else
		this.content.style.backgroundColor="";
}

Preview.setBackgroundRepeat = function ()
{
	this.content.style.backgroundRepeat=document.getElementById("backgroundRepeat").value;
}

Preview.setBackgroundAttachment = function ()
{
	this.content.style.backgroundAttachment=document.getElementById("backgroundAttachment").value;
}

Preview.setTextAlign = function ()
{
	this.content.style.textAlign=document.getElementById("textAlign").value;
}

Preview.setWhiteSpace = function ()
{
	this.content.style.whiteSpace=document.getElementById("whiteSpace").value;
}

Preview.setClear = function ()
{
	this.content.style.clear=document.getElementById("clear").value;
}

Preview.setListStyleType = function ()
{
	this.content.style.listStyleType=document.getElementById("listStyleType").value;
}

Preview.setListStylePosition = function ()
{
	this.content.style.listStylePosition=document.getElementById("listStylePosition").value;
}

Preview.setPosition = function ()
{
	this.content.style.position=document.getElementById("position").value;
}

Preview.setVisibility = function ()
{
	this.content.style.visibility=document.getElementById("visibility").value;
}

Preview.setOverflow = function ()
{
	this.content.style.overflow=document.getElementById("overflow").value;
}

Preview.isTarget = function()
{
	if (document.getElementById("targetTag").value.length>0)
		return true;
	if (document.getElementById("targetTag_other").checked==true && document.getElementById("targetTag_value").value.length>0)
		return true;
	if (document.getElementById("targetClass").value.length>0)
		return true;
	if (document.getElementById("targetId").value.length>0)
		return true;
	return false;
}

Preview.clear = function()
{
	document.getElementById("frmMain").reset();
	this.sFloat="";
	this.change();
	var object=new Object();
	object.value="";
	Example.change(object);
}

