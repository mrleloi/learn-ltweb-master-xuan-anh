/*
 * CssEditor plugin for FCKEditor.
 * Copyright (C) 2004 by Dmitriy Barbasura (delta3@pisem.net)
 * 
 * Licensed under the terms of the GNU Lesser General Public License:
 * 		http://www.opensource.org/licenses/lgpl-license.php
 * 
 */

var Validator=new Object();

Validator.number = function (value)
{
	if (value.match(/^[0-9]+$/))
		return true;
	return false;
}

Validator.color = function (value)
{
	if (value.match(/^[a-zA-Z0-9#]+$/))
		return true;
	return false;
}
