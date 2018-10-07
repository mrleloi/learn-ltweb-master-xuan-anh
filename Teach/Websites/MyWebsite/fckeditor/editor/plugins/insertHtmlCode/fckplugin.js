/*
 * File Name: fckplugin.js
 * Plugin to launch the Insert Code dialog in FCKeditor
 */

// Register the related command.
FCKCommands.RegisterCommand( 'insertHtmlCode', new FCKDialogCommand( 'InsertHtmlCode', 'Chèn mã HTML', FCKPlugins.Items['insertHtmlCode'].Path + 'fck_insertHtmlCode.html', 415, 300 ) ) ;

// Create the "insertHtmlCode" toolbar button.
var oinsertHtmlCodeItem = new FCKToolbarButton( 'insertHtmlCode',  'Chèn mã HTML', 'Chèn mã HTML', null, null, false, true) ;
oinsertHtmlCodeItem.IconPath = FCKPlugins.Items['insertHtmlCode'].Path + 'insertHtmlCode.gif' ;

FCKToolbarItems.RegisterItem( 'insertHtmlCode', oinsertHtmlCodeItem ) ;

