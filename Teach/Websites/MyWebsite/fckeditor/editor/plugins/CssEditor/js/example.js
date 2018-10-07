/*
 * CssEditor plugin for FCKEditor.
 * Copyright (C) 2004 by Dmitriy Barbasura (delta3@pisem.net)
 * 
 * Licensed under the terms of the GNU Lesser General Public License:
 * 		http://www.opensource.org/licenses/lgpl-license.php
 * 
 */

var Example=new Object();

Example.change = function (object)
{
	var sValue="";
	var sId="Preview";
	var preview=document.getElementById(sId);

	if (object.value=="longText")
	{
		sValue="Long text ";
		for (i=0; i<3; i++)
			sValue+=sValue;
		Preview.sId="Preview";
	}
	else if (object.value=="simpleTable")
	{
		sId="table";
		sValue="<table id='"+sId+"' border='1'><tr><td>Simple</td><td>Table</td></tr></table>";
		Preview.sId=sId;
	}
	else if (object.value=="unorderedList")
	{
		sId="list";
		sValue="<ul id='"+sId+"'><li>One</li><li>Two</li><li>Three</li></ul>";
		Preview.sId=sId;
	}
	else if (object.value=="image")
	{
		sId="image";
		sValue="<img id='"+sId+"' src='test.gif' width='100' height='100' alt='Test Image'>";
		Preview.sId=sId;
	}
	else
	{
		sValue="Just sample<br>text";
		Preview.sId="Preview";
	}
	preview.innerHTML=sValue;
}