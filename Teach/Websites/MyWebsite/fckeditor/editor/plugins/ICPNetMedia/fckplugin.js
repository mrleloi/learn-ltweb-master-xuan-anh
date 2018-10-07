function PageQuery(q) {
if(q.length > 1) this.q = q.substring(1, q.length);
else this.q = null;
this.keyValuePairs = new Array();
if(q) {
for(var i=0; i < this.q.split("&").length; i++) {
this.keyValuePairs[i] = this.q.split("&")[i];
}
}
this.getKeyValuePairs = function() { return this.keyValuePairs; }
this.getValue = function(s) {
for(var j=0; j < this.keyValuePairs.length; j++) {
if(this.keyValuePairs[j].split("=")[0] == s)
return this.keyValuePairs[j].split("=")[1];
}
return false;
}
this.getParameters = function() {
var a = new Array(this.getLength());
for(var j=0; j < this.keyValuePairs.length; j++) {
a[j] = this.keyValuePairs[j].split("=")[0];
}
return a;
}
this.getLength = function() { return this.keyValuePairs.length; } 
}
//**********************
function queryString(key){
var page = new PageQuery(window.parent.parent.location.search); 
return unescape(page.getValue(key)); 
}

//InsertMedia.aspx
FCKCommands.RegisterCommand( 'ICP_InsertImageDB', 
					new FCKDialogCommand(
 					'ICP_InsertImageDB',
					'Quản lý Media',
					 '../../fckplugins/InsertImageToMediaItem.aspx', 600, 380 
					)
 ) ;

var oICP_InsertImageDB = new FCKToolbarButton( 'ICP_InsertImageDB',  'xxx', 'Chèn ảnh', null, null, false, true) ;
oICP_InsertImageDB.IconPath = FCKPlugins.Items['ICPNetMedia'].Path +  'InsertImage.gif'; 
FCKToolbarItems.RegisterItem( 'ICP_InsertImageDB', oICP_InsertImageDB);

//----------------------------------------------------------------------------------
FCKCommands.RegisterCommand( 'ICP_InsertVote', 
					new FCKDialogCommand(
 					'ICP_InsertVote',
					'Quản lý Votes',
                    '../../fckplugins/PoolQuestionsPlugin.aspx', 600, 600 
					)
 ) ;

var oICP_InsertVote = new FCKToolbarButton( 'ICP_InsertVote',  'xxx', 'Chèn thăm dò dư luận', null, null, false, true) ;
oICP_InsertVote.IconPath = FCKPlugins.Items['ICPNetMedia'].Path +  'InsertVote.gif';
FCKToolbarItems.RegisterItem('ICP_InsertVote', oICP_InsertVote);

//----------------------------------------------------------------------------------
FCKCommands.RegisterCommand('ICP_Insert3GPFile',
					new FCKDialogCommand(
 					'ICP_Insert3GPFile',
					'Chèn video clip cho mobile',
                    '../../fckplugins/Insert3GPFile.aspx', 600, 600
					)
 );

var oICP_Insert3GPFile = new FCKToolbarButton('ICP_Insert3GPFile', 'xxx', 'Chèn video clip cho mobile', null, null, false, true);
oICP_Insert3GPFile.IconPath = FCKPlugins.Items['ICPNetMedia'].Path + 'Insert3GPFile.gif';
FCKToolbarItems.RegisterItem('ICP_Insert3GPFile', oICP_Insert3GPFile);
//----------------------------------------------------------------------------------
FCKCommands.RegisterCommand('ICP_InsertMP4File',
					new FCKDialogCommand(
 					'ICP_InsertMP4File',
					'Chèn video clip mp4',
                    '../../fckplugins/InsertMP4File.aspx', 600, 600
					)
 );

var oICP_ICP_InsertMP4File = new FCKToolbarButton('ICP_InsertMP4File', 'xxx', 'Chèn video clip MP4', null, null, false, true);
oICP_ICP_InsertMP4File.IconPath = FCKPlugins.Items['ICPNetMedia'].Path + 'InsertVideo.gif';
FCKToolbarItems.RegisterItem('ICP_InsertMP4File', oICP_ICP_InsertMP4File);
//----------------------------------------------------------------------------------
FCKCommands.RegisterCommand('ICP_InsertArticle',
					new FCKDialogCommand(
 					'ICP_InsertArticle',
					'Chèn bài viết',
                    '../../fckplugins/InsertArticle.aspx', 800, 600
					)
 );

var oICP_ICP_InsertArticle = new FCKToolbarButton('ICP_InsertArticle', 'xxx', 'Chèn Bài viết', null, null, false, true);
oICP_ICP_InsertArticle.IconPath = FCKPlugins.Items['ICPNetMedia'].Path + 'newrelate.png';
FCKToolbarItems.RegisterItem('ICP_InsertArticle', oICP_ICP_InsertArticle);
//----------------------------------------------------------------------------------
FCKCommands.RegisterCommand('ICP_InsertInternalLink',
					new FCKDialogCommand(
 					'ICP_InsertInternalLink',
					'Chèn Internal Link',
                    '../../fckplugins/InsertInternalLink.aspx', 800, 600
					)
 );

var oICP_ICP_InsertArticle = new FCKToolbarButton('ICP_InsertInternalLink', 'xxx', 'Cấu hình Internal Link', null, null, false, true);
oICP_ICP_InsertArticle.IconPath = FCKPlugins.Items['ICPNetMedia'].Path + 'internallink.png';
FCKToolbarItems.RegisterItem('ICP_InsertInternalLink', oICP_ICP_InsertArticle);
//----------------------------------------------------------------------------------
FCKCommands.RegisterCommand('ICP_InsertComment',
					new FCKDialogCommand(
 					'ICP_InsertComment',
					'Chèn Trích dẫn',
                    '../../fckplugins/InsertComment.aspx', 800, 400
					)
 );

var oICP_ICP_InsertArticle = new FCKToolbarButton('ICP_InsertComment', 'xxx', 'Chèn Trích dẫn', null, null, false, true);
oICP_ICP_InsertArticle.IconPath = FCKPlugins.Items['ICPNetMedia'].Path + 'comment.png';
FCKToolbarItems.RegisterItem('ICP_InsertComment', oICP_ICP_InsertArticle);
//----------------------------------------------------------------------------------
FCKCommands.RegisterCommand('ICP_InsertTemplate',
					new FCKDialogCommand(
 					'ICP_InsertTemplate',
					'Chèn Nội dung mẫu',
                    '../../fckplugins/InsertTemplate.aspx', 800, 400
					)
 );

var oICP_ICP_InsertArticle = new FCKToolbarButton('ICP_InsertTemplate', 'xxx', 'Chèn Nội dung mẫu', null, null, false, true);
oICP_ICP_InsertArticle.IconPath = FCKPlugins.Items['ICPNetMedia'].Path + 'chenmau.png';
FCKToolbarItems.RegisterItem('ICP_InsertTemplate', oICP_ICP_InsertArticle);