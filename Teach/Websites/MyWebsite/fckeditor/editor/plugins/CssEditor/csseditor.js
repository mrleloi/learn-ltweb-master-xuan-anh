/*
 * CssEditor plugin for FCKEditor.
 * Copyright (C) 2004 by Dmitriy Barbasura (delta3@pisem.net)
 * 
 * Licensed under the terms of the GNU Lesser General Public License:
 * 		http://www.opensource.org/licenses/lgpl-license.php
 * 
 */

function loadCssEditor()
{
	document.getElementById("frmMain").reset();
	ScriptLoader.AddScript("js/other.js");
	ScriptLoader.AddScript("js/update.js");
	ScriptLoader.AddScript("js/preview.js");
	ScriptLoader.AddScript("js/color_dialog.js");
	ScriptLoader.AddScript("js/utils.js");
	ScriptLoader.AddScript("js/browser_info.js");
	ScriptLoader.AddScript("js/validator.js");
	ScriptLoader.AddScript("js/example.js");
}


var ScriptLoader = new Object() ;
ScriptLoader.IsLoading = false ;
ScriptLoader.Queue = new Array() ;

// Adds a script or css to the queue.
ScriptLoader.AddScript = function( scriptPath )
{
	ScriptLoader.Queue[ ScriptLoader.Queue.length ] = scriptPath ;
	
	if ( !this.IsLoading )
		this.CheckQueue() ;
}

// Checks the queue to see if there is something to load.
// This function should not be called by code. It's a internal function
// that's called recursively.
ScriptLoader.CheckQueue = function() 
{
	// Check if the queue is not empty.
	if ( this.Queue.length > 0 )
	{
		this.IsLoading = true ;
		
		// Get the first item in the queue
		var sScriptPath = this.Queue[0] ;
		
		// Removes the first item from the queue
		var oTempArray = new Array() ;
		for ( i = 1 ; i < this.Queue.length ; i++ )
			oTempArray[ i - 1 ] = this.Queue[ i ] ;
		this.Queue = oTempArray ;
		
//		window.status = ( 'Loading ' + sScriptPath + '...' ) ;

		// Dynamically load the file (it can be a CSS or a JS)
		var e ;
		
		// If is a CSS
		if ( sScriptPath.lastIndexOf( '.css' ) > 0 )
		{
			e = document.createElement( 'LINK' ) ;
			e.rel	= 'stylesheet' ;
			e.type	= 'text/css' ;
		}
		// It is a JS
		else
		{
			e = document.createElement( "script" ) ;
			e.type	= "text/javascript" ;
		}
		
		// Add the new object to the HEAD.
		document.getElementsByTagName("head")[0].appendChild( e ) ; 

		var oEvent = function()
		{
			// Gecko doesn't have a "readyState" property
			if ( this.tagName == 'LINK' || !this.readyState || this.readyState == 'loaded' )
				// Load the next script available in the queue
				ScriptLoader.CheckQueue() ;
		}
		
		// Start downloading it.
		if ( e.tagName == 'LINK' )
		{
			// IE must wait for the file to be downloaded.
			if ( BrowserInfo.IsIE )
				e.onload = oEvent ;
			// Gecko doens't fire any event when the CSS is loaded, so we 
			// can't wait for it.
			else
				ScriptLoader.CheckQueue() ;
				
			e.href = sScriptPath ;
		}
		else
		{
			// Gecko fires the "onload" event and IE fires "onreadystatechange"
			e.onload = e.onreadystatechange = oEvent ;
			e.src = sScriptPath ;
		}
	}
	else
	{
		this.IsLoading = false ;
		
		// Call the "OnEmpty" event.
		if ( this.OnEmpty ) 
			this.OnEmpty() ;
	}
}

