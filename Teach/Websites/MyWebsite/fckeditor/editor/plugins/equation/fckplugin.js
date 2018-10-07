/* 
 Copyright CodeCogs 2006-2011
 Written by Will Bateman.
 
 Version 1: FCK Editor Plugin that insert LaTeX into HTML
*/
var language=FCKConfig.DefaultLanguage;   // specify your language if not English (en_en)
// Latex equation editor

var InsertEquationCommand=function(){};

InsertEquationCommand.prototype.Execute=function(){ }

InsertEquationCommand.GetState=function() {
        return FCK_TRISTATE_OFF; //we dont want the button to be toggled
}

var popupEqnwin = null;
InsertEquationCommand.Execute=function(latex) 
{
  //open a popup window when the button is clicked
	if (popupEqnwin==null || popupEqnwin.closed || !popupEqnwin.location) 
	{
  	var url='http://latex.codecogs.com/editor_json4.php?type=url&editor=FCKEditor';
    if(language!='') url+='&lang='+language;
	  if(latex!==undefined) 
		{	
		  latex=unescape(latex);
			latex=latex.replace(/\+/g,'&plus;');
			url+='&latex='+escape(latex);
		}
	
		popupEqnwin=window.open('','LaTexEditor','width=700,height=450,status=1,scrollbars=yes,resizable=1');
		if (!popupEqnwin.opener) popupEqnwin.opener = self;
		popupEqnwin.document.open();
		popupEqnwin.document.write('<!DOCTYPE html><html><head><script src="'+url+'" type="text/javascript"></script></head><body></body></html>');
		popupEqnwin.document.close();
  }
	else if (window.focus) 
	{ 
	  popupEqnwin.focus()
		if(latex!==undefined)
		{
		  latex=unescape(latex);
	
			try
			{
				popupEqnwin.EqEditor.load(latex);
			}
			catch(err)
			{
				alert(err.message);
			}
    }
		popupEqnwin.document.getElementById("latex_formula").focus();
		popupEqnwin.document.getElementById("latex_formula").select();
	}
}

// Register the related command.
FCKCommands.RegisterCommand( 'Equation', InsertEquationCommand ) ;

// Create the "Placeholder" toolbar button.
var oEquationItem = new FCKToolbarButton( 'Equation', FCKLang.EquationBtn ) ;

//oEquationItem.IconPath = FCKPlugins.Items['equation_html'].Path + 'equation.gif' ;
//Anhnx
//oEquationItem.IconPath = FCKPlugins.Items['equation'].Path + 'equation.gif' ;
oEquationItem.IconPath = FCKConfig.PluginsPath  + 'equation/equation.gif' ;
FCKToolbarItems.RegisterItem( 'Equation', oEquationItem ) ;


// The object used for all Placeholder operations.
var FCKEquation= new Object() ;

// Add a new placeholder at the actual selection.
// 
FCKEquation.Add = function( name )
{
	var oImage = FCK.InsertElement('img');
	
	if(FCKConfig.TidyEqns) name=name.replace(/\\/g,'%5C');     // encode backslashes
	oImage.src = name;
	oImage.align ='absmiddle';
	
	// Let figure our whats being added. 
	// match   <img src="latex.codecogs.com/gif.latex?1+sin(x)" /> 
	var sName = name.match( /(gif|svg)\.latex\?(.*)/ );
	var latex= unescape(sName[2]);
	latex = latex.replace(/@plus;/g,'+');
	latex = latex.replace(/&plus;/g,'+');
	latex = latex.replace(/&space;/g,' ');
	latex = latex.replace(/&hash;/g,'#');
		
  oImage.alt = latex;
	
	if ( FCKBrowserInfo.IsGecko )
		oimage.style.cursor = 'default' ;
	
//	image._fckequation = sName[2];
//	oImage.contentEditable = false;
	
	oImage.onresizestart = function()
	{
		FCK.EditorWindow.event.returnValue = false ;
		return false ;
	}
	
// Then select the new placeholder
	FCKSelection.SelectNode(oImage);
}

// Open the Placeholder dialog on double click.
FCKEquation.OnDoubleClick = function( image )
{
	if ( image.tagName == 'IMG')
	{
		var sName = image.src.match( /http:\/\/(latex.codecogs.com)\/(gif|svg)\.latex\?(.*)/ );
				
		if(sName[1]=='latex.codecogs.com')
		  FCKCommands.GetCommand( 'Equation' ).Execute(sName[3]) ;
	}
}

FCK.RegisterDoubleClickHandler( FCKEquation.OnDoubleClick, 'IMG' ) ;


