var InsertVariableCommand=function(){
//create our own command, we dont want to use the FCKDialogCommand because it uses the default fck layout and not our own
};

InsertVariableCommand.GetState=function() {
return FCK_TRISTATE_OFF; //we dont want the button to be toggled
}

InsertVariableCommand.Execute=function() {
//open a popup window when the button is clicked
//window.open('/Modules/News/InsertMedia.aspx', 'insertVariable', 'width=500,height=400,scrollbars=no,scrolling=no,location=no,toolbar=no');
window.open('/Modules/News/InsertImageToMediaItem.aspx', 'insertVariable', 'width=500,height=400,scrollbars=no,scrolling=no,location=no,toolbar=no');
}

FCKCommands.RegisterCommand('Insert_Variables', InsertVariableCommand ); //otherwise our command will not be found
var oInsertVariables = new FCKToolbarButton('Insert_Variables', 'insert variable');

oInsertVariables.IconPath = FCKConfig.PluginsPath + 'cmscontent/cmscontent.gif'; 

FCKToolbarItems.RegisterItem( 'CMSContent', oInsertVariables );
