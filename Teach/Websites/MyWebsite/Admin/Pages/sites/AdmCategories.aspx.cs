using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Reflection;
using Lib.Web;
using Lib.Utilities;
public partial class admin_pages_admin_sites_AdmCategories : System.Web.UI.Page
{
	protected string WEB_CONSTR = MyConstants.WEB_CONSTR;
	protected string LogFilePath = MyConstants.LogFilePath;
	protected string LogFileName = MyConstants.LogFileName;
	protected List<Categories> cboParentCategories = new List<Categories>();
	protected List<CategoryStatus> cboCategoryStatus = new List<CategoryStatus>();
	protected Categories m_Categories;
	protected CategoryStatus m_CategoryStatus;
	private int ActUserId = 0;
	private int EditIndex;
	protected string IpAddress = "";
	private string SysMessageDesc = "";
	protected void Page_Load(object sender, EventArgs e)
	{
		string redirect = "";
		try
		{
			m_Categories = new Categories(WEB_CONSTR);
			m_CategoryStatus = new CategoryStatus(WEB_CONSTR);
			IpAddress = Request.UserHostAddress;
			ActUserId = MyConstants.ActUserId;
			if (ActUserId > 0)
			{
				if (!IsPostBack)
				{
					bindData(-1);
				}
			}
			else
			{
				redirect = MyConstants.PRJ_ROOT + "/Login.aspx";
			}
		}
		catch (Exception ex)
		{
			LogFiles.WriteLog(ex.Message, LogFilePath + "\\Exception", LogFileName + "." + this.GetType().Name + "." + MethodBase.GetCurrentMethod().Name);
		}
		if (!string.IsNullOrEmpty(redirect))
		{
			Response.Redirect(redirect);
		}
	}
	//-------------------------------------------------------------------------------------------------
	private void bindData(int index)
	{
		try
		{
			List<Categories> l_Categories = m_Categories.GetList(LogFilePath, LogFileName, ActUserId);
			cboParentCategories = m_Categories.CopyNA(l_Categories);
			cboCategoryStatus = m_CategoryStatus.GetList(LogFilePath, LogFileName);
			m_grid.EditIndex = index;
			bool NoRecord = (l_Categories.Count <= 0);
			if (NoRecord)
			{
				l_Categories.Add(new Categories(WEB_CONSTR));
			}
			m_grid.DataSource = l_Categories;
			m_grid.DataBind();
			if (m_grid.Rows.Count > 0)
			{
				if (NoRecord)
				{
				  m_grid.Rows[0].Enabled = false;
				}
				else
				{
					GridViewRow row;
					LinkButton lbutton;
					string confirm = "return confirm('Bạn thực sự muốn xóa chuyên mục này?')";
					for (int i = 0; i < m_grid.Rows.Count; i++)
					{
						row = m_grid.Rows[i];
						lbutton = (LinkButton)row.FindControl("cmdDelete");
						if (lbutton != null)
						{
							lbutton.Attributes.Add("onclick", confirm);
						}
					}
				}
			}
		}
		catch (Exception ex)
		{
			LogFiles.WriteLog(ex.Message, LogFilePath + "\\Exception", LogFileName + "." + this.GetType().Name + "." + MethodBase.GetCurrentMethod().Name);
		}
	}

	protected void m_grid_RowEditing(object sender, GridViewEditEventArgs e)
	{
		EditIndex = e.NewEditIndex;
		bindData(EditIndex);
	}

	protected void m_grid_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
	{
		bindData(-1);
	}

	protected void m_grid_RowUpdating(object sender, GridViewUpdateEventArgs e)
	{
		try
		{
			short DisplayOrder = 0;
			short ParentCategoryId;
			int id = e.RowIndex;
			m_grid.EditIndex = id;
			GridViewRow row = m_grid.Rows[id];
			short updateId = 0;
			Int16.TryParse(m_grid.DataKeys[id].Value.ToString(), out updateId);
			Int16.TryParse(((DropDownList)row.FindControl("cboParentCategories")).SelectedValue, out ParentCategoryId);
			m_Categories = m_Categories.Get(LogFilePath, LogFileName, updateId);
			if (m_Categories.CategoryId > 0)
			{
				Int16.TryParse(((TextBox)row.FindControl("txtDisplayOrder")).Text.Trim(), out DisplayOrder);
				if (DisplayOrder > 0)
				{
					m_Categories.DisplayOrder = DisplayOrder;
				}
				m_Categories.CategoryName = ((TextBox)row.FindControl("txtCategoryName")).Text;
				m_Categories.CategoryDesc = ((TextBox)row.FindControl("txtCategoryDesc")).Text;
				m_Categories.ParentCategoryId = ParentCategoryId;
				m_Categories.Url = ((TextBox)row.FindControl("txtUrl")).Text.Trim();
				m_Categories.CategoryStatusId = Convert.ToByte(((DropDownList)row.FindControl("ddlCategoryStatus")).Text);
				m_Categories.ImageIcon = ((TextBox)row.FindControl("txtImageIcon")).Text;
				m_Categories.CrUserId = ActUserId;
				m_Categories.CrDateTime = System.DateTime.Now;
				if (m_Categories.UpdateNoInform(LogFilePath, LogFileName, MyConstants.DISTRIBUTED_PROCESS, IpAddress, ActUserId))
				{
					SysMessageDesc = "Đã cập nhật chuyên mục";
				}
				else
				{
					SysMessageDesc = "Lỗi cập nhật chuyên mục";
				}
			}
			else
			{
				SysMessageDesc = "Không tìm thấy chuyên mục";
			}
			JSAlert.Alert(SysMessageDesc, this);
			bindData(-1);
		}
		catch (Exception ex)
		{
			LogFiles.WriteLog(ex.Message, LogFilePath + "\\Exception", LogFileName + "." + this.GetType().Name + "." + MethodBase.GetCurrentMethod().Name);
		}

	}
	//-----------------------------------------------------------------------------------------------------
	protected void m_grid_RowCommand(object sender, GridViewCommandEventArgs e)
	{
		try
		{
			string commandName = e.CommandName;
			GridViewRow row = m_grid.FooterRow;
			if (commandName == "Insert")
			{
				short DisplayOrder = 0;
				Int16.TryParse(((TextBox)row.FindControl("txtInsertDisplayOrder")).Text.Trim(), out DisplayOrder);
				m_Categories.CategoryName = ((TextBox)row.FindControl("txtInsertCategoryName")).Text;
				m_Categories.CategoryDesc = ((TextBox)row.FindControl("txtInsertCategoryDesc")).Text;
				m_Categories.ParentCategoryId = Int16.Parse(((DropDownList)row.FindControl("cboInsertParentCategories")).SelectedValue);
				m_Categories.Url = ((TextBox)row.FindControl("txtInsertUrl")).Text;
				m_Categories.DisplayOrder = DisplayOrder;
				m_Categories.CategoryStatusId = Convert.ToByte(((DropDownList)row.FindControl("ddlInsertCategoryStatus")).Text);
				m_Categories.ImageIcon = ((TextBox)row.FindControl("txtInsertImageIcon")).Text;
				m_Categories.CrUserId = ActUserId;
				m_Categories.CrDateTime = System.DateTime.Now;
				if (m_Categories.InsertNoInform(LogFilePath, LogFileName, MyConstants.DISTRIBUTED_PROCESS, IpAddress, ActUserId))
				{
					SysMessageDesc = "Đã thêm chuyên mục";
				}
				else
				{
					SysMessageDesc = "Lỗi thêm chuyên mục";
				}
				JSAlert.Alert(SysMessageDesc, this);
				bindData(-1);
			}
		}
		catch (Exception ex)
		{
			LogFiles.WriteLog(ex.Message, LogFilePath + "\\Exception", LogFileName + "." + this.GetType().Name + "." + MethodBase.GetCurrentMethod().Name);
		}
	}
	//--------------------------------------------------------------------------------------------
	protected void m_grid_RowDeleting(object sender, GridViewDeleteEventArgs e)
	{
		try
		{
			short delId = (Int16)m_grid.DataKeys[e.RowIndex].Value;
			if (delId > 0)
			{
					m_Categories.CategoryId = delId;
					m_Categories.CrUserId = ActUserId;
					m_Categories.CrDateTime = System.DateTime.Now;
					if (m_Categories.DeleteNoInform(LogFilePath, LogFileName, MyConstants.DISTRIBUTED_PROCESS, IpAddress, ActUserId))
					{
						SysMessageDesc = "Đã xoá chuyên mục";
					}
					else
					{
						SysMessageDesc = "Lỗi xoá chuyên mục";
					}
				JSAlert.Alert(SysMessageDesc, this);
				bindData(-1);
			}
		}
		catch (Exception ex)
		{
			LogFiles.WriteLog(ex.Message, LogFilePath + "\\Exception", LogFileName + "." + this.GetType().Name + "." + MethodBase.GetCurrentMethod().Name);
		}
	}
	//---------------------------------------------------------------------------------------
	protected void btnSearch_Click(object sender, EventArgs e)
	{
		bindData(-1);
	}
	//-----------------------------------------------------------------------------------------
	protected void m_grid_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		m_grid.PageIndex = e.NewPageIndex;
		bindData(-1);
	}
}
