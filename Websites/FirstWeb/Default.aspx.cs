using System;
using System.Diagnostics;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

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

    protected void Page_Load(object sender, EventArgs e)
    {
        string name = "Loi";
        int age = 18;
        Person p = new Student(name, age);
        res = p.render();

        Debug.WriteLine(res);
    }
}
