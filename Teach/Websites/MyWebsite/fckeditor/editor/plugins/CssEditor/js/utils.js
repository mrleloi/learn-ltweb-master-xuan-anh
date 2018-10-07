/*
 * CssEditor plugin for FCKEditor.
 * Copyright (C) 2004 by Dmitriy Barbasura (delta3@pisem.net)
 * 
 * Licensed under the terms of the GNU Lesser General Public License:
 * 		http://www.opensource.org/licenses/lgpl-license.php
 * 
 */

function showMenu(branch){
    var objBranch = document.getElementById(branch).style;
    if(objBranch.display=="block")
        objBranch.display="none";
    else
        objBranch.display="block";
}

