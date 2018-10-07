window.name="myWindow";
window.target = "myWindow";
var oEditor ;
try
{
  oEditor= window.parent.InnerDialogLoaded() ;
}
catch(ex)
{
  oEditor= window.parent.InnerDialogLoaded().FCK;
}
window.onload = function()
{
  document.body.style.padding = '0px' ;
  if (oEditor==null)
  {
    alert("Null oEditor ");
  }
  else
  {
    oEditor.FCKLanguageManager.TranslatePage(document) ;
  }
  window.parent.SetOkButton( false ) ;
  window.parent.SetAutoSize( true ) ;	
}

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