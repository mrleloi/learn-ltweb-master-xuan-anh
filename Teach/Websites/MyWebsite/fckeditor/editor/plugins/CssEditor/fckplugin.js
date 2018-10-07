// Register the related commands.
FCKCommands.RegisterCommand( 'CssEditor', new FCKDialogCommand( FCKLang['DlgCssEditorTitle'], FCKLang['DlgCssEditorBtn'], FCKConfig.PluginsPath + 'CssEditor/csseditor.html', 700, 600 ) ) ;


// Create the "CssEditor" toolbar button.
var oCssEditorItem	= new FCKToolbarButton( 'CssEditor', FCKLang['DlgCssEditorTitle'] ) ;
oCssEditorItem.IconPath	= FCKConfig.PluginsPath + 'CssEditor/css.gif' ;

FCKToolbarItems.RegisterItem( 'CssEditor', oCssEditorItem ) ;
