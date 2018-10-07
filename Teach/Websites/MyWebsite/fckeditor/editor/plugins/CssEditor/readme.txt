Name:
	CssEditor plug-in for FCKEditor.
Version:	
	0.1.0

About:
	Author - Dmitriy Barbasura (delta3@pisem.net)

	Copyright (C) 2004 by Dmitriy Barbasura
	Portions of this code Copyright (C) 2003-2004 Frederico Caldeira Knabben (Color Selector, Browser Info, Script Loader).

	Licensed under the terms of the GNU Lesser General Public License: 
		http://www.opensource.org/licenses/lgpl-license.php


	CssEditor original idea - Macromedia DreamWeaver (with +/- same functionality).
	Tested with FCKEditor v.2.0.RC1 on IE6/Firefox 1.

History:
	0.0.1 - Start 2004/12/03.
	0.0.13 - Development versions.
	0.1.0 - First public version 2004/12/10.


Installation:
	1. Copy CssEditor folder to /fckeditor/editor/plugins.
	2. Edit /fckeditor/fckconfig.js:
	3. Add to ToolbarSets new button - ['CssEditor'].
		FCKConfig.ToolbarSets["Default"] = [
			['CssEditor']
		] ;
	4. At the end of fckconfig.js:
		FCKConfig.Plugins.Add( 'CssEditor', 'en' );

