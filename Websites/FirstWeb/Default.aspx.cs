using System;
using System.IO;
using System.Diagnostics;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Reflection;
//using System.Threading;
using Lib.Utilities;

public class Person
{
    public string name;

    public Person(string name)
    {
        this.name = name;
    }

    public virtual string render()
    {
        return "Hello "+ name +" !!";
    }
}

public class Student : Person
{
    public int age;

    public Student(string name, int age) : base(name)
    {
        this.age = age;
    }

    public override string render()
    {
        return "Hello " + name + " - "+ age +" !!";
    }
}

public partial class _Default : System.Web.UI.Page 
{
    protected string res;
    public static readonly string pathServer = Server.MapPath("~");
    private string pathLogs = pathServer + "\\Logs";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string name = "Loi";
            int age = 18;
            Person p = new Student(name, age);
            res = p.render();
            int z = 0;
            int a = 1 / z;
        }
        catch (Exception ex)
        {
            string fileName = this.GetType().Name + MethodBase.GetCurrentMethod().Name;
            LogFiles.WriteLog(ex.Message, _Default.pathServer, fileName);
        }

        Debug.WriteLine("--------" + pathServer);
    }
}
