<%@ Page Language="C#" MasterPageFile="~/admin/MasterPageAdmin.master" AutoEventWireup="true" CodeFile="AdmCategories.aspx.cs" Inherits="admin_pages_admin_sites_AdmCategories"  Title="Danh sách các chuyên mục" %>
<%@ Import Namespace="Lib.Web" %>
<%@ Import Namespace="Lib.Utilities" %>
<asp:Content ID="Content1" ContentPlaceHolderID="m_contentBody" runat="Server">
  <font class="titleForm"><%=this.Title.ToUpper()%></font>
  <asp:GridView ID="m_grid" DataKeyNames="CategoryId" runat="server" CellPadding="4"
      ForeColor="#333333" ShowHeader="true" AutoGenerateColumns="False" Width="100%"
      OnRowDeleting="m_grid_RowDeleting" OnRowEditing="m_grid_RowEditing" OnRowCancelingEdit="m_grid_RowCancelingEdit"
      OnRowUpdating="m_grid_RowUpdating" ShowFooter="true" OnRowCommand="m_grid_RowCommand">
      <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
      <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
      <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
      <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
      <EditRowStyle BackColor="#999999" />
      <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
    <Columns>
      <asp:TemplateField HeaderText="STT" ItemStyle-HorizontalAlign="Center">
        <ItemTemplate> <%#Eval("DisplayOrder")%>  </ItemTemplate>
        <EditItemTemplate>
          <asp:TextBox ID="txtDisplayOrder" runat="server" Columns="3" MaxLength="5" Text='<%# Bind("DisplayOrder") %>' />
        </EditItemTemplate>
        <FooterTemplate>
          <asp:TextBox ID="txtInsertDisplayOrder" runat="server" Columns="3" MaxLength="5" Text="0"></asp:TextBox><br />
        </FooterTemplate>
        <ItemStyle HorizontalAlign="Left" />
      </asp:TemplateField>
      
      <asp:TemplateField HeaderText="Tên chuyên mục">
        <ItemTemplate> <%#Eval("CategoryName")%>  </ItemTemplate>
        <EditItemTemplate>
          <asp:TextBox ID="txtCategoryName" runat="server" Columns="20" MaxLength="255" Text='<%# Bind("CategoryName") %>' />
          <asp:RequiredFieldValidator ID="RequiredCategoryName" runat="server" ControlToValidate="txtCategoryName" Display="Dynamic" ErrorMessage="Nhập tên chuyên mục" SetFocusOnError="True"></asp:RequiredFieldValidator>
        </EditItemTemplate>
        <FooterTemplate>
          <asp:TextBox ID="txtInsertCategoryName" runat="server" Columns="20" MaxLength="255"></asp:TextBox><br />
          <asp:RequiredFieldValidator ID="RequiredInsertCategoryName" runat="server" ControlToValidate="txtInsertCategoryName" Display="Dynamic" ErrorMessage="Nhập tên chuyên mục" SetFocusOnError="True"></asp:RequiredFieldValidator>
        </FooterTemplate>
        <ItemStyle HorizontalAlign="Left" />
      </asp:TemplateField>
      
      <asp:TemplateField HeaderText="Mô tả">
        <ItemTemplate> <%#Eval("CategoryDesc")%> </ItemTemplate>
        <EditItemTemplate>
          <asp:TextBox ID="txtCategoryDesc" runat="server" Columns="20" MaxLength="255" Text='<%# Bind("CategoryDesc") %>' />
          <asp:RequiredFieldValidator ID="RequiredCategoryDesc" runat="server" ControlToValidate="txtCategoryDesc"  Display="Dynamic" ErrorMessage="Nhập tên chuyên mục" SetFocusOnError="True"></asp:RequiredFieldValidator>
        </EditItemTemplate>
        <FooterTemplate>
          <asp:TextBox ID="txtInsertCategoryDesc" runat="server" Columns="20" MaxLength="255"></asp:TextBox><br />
          <asp:RequiredFieldValidator ID="RequiredInsertCategoryDesc" runat="server" ControlToValidate="txtInsertCategoryDesc"  Display="Dynamic" ErrorMessage="Nhập mô tả chuyên mục" SetFocusOnError="True"></asp:RequiredFieldValidator>
        </FooterTemplate>
        <ItemStyle HorizontalAlign="Left" />
      </asp:TemplateField>
      
      <asp:TemplateField HeaderText="Mức trên">
        <ItemTemplate>
            <%# m_Categories.Get(cboParentCategories, Convert.ToInt16(Eval("ParentCategoryId"))).CategoryDesc%>
        </ItemTemplate>
        <EditItemTemplate>
            <asp:DropDownList ID="cboParentCategories" runat="server" DataSource='<%#cboParentCategories%>' DataTextField="CategoryDesc" DataValueField="CategoryId" SelectedValue='<%#Bind("ParentCategoryId") %>' />
        </EditItemTemplate>
        <FooterTemplate>
            <asp:DropDownList ID="cboInsertParentCategories" runat="server" DataSource='<%#cboParentCategories %>' DataTextField="CategoryDesc" DataValueField="CategoryId" SelectedValue='<%#Bind("ParentCategoryId") %>' />
        </FooterTemplate>
        <ItemStyle HorizontalAlign="Left" />
      </asp:TemplateField>
      
      <asp:TemplateField HeaderText="Liên kết">
        <ItemTemplate>
          <%#Eval("Url")%>
        </ItemTemplate>
        <EditItemTemplate>
          <asp:TextBox ID="txtUrl" runat="server" Columns="20" MaxLength="255" Text='<%# Bind("Url") %>' />
          <asp:RequiredFieldValidator ID="RequiredUrl" runat="server" ControlToValidate="txtUrl" Display="Dynamic" ErrorMessage="Nhập nơi lưu chuyên mục" SetFocusOnError="True"></asp:RequiredFieldValidator>
        </EditItemTemplate>
        <FooterTemplate>
          <asp:TextBox ID="txtInsertUrl" runat="server" Columns="20" MaxLength="255"></asp:TextBox><br />
          <asp:RequiredFieldValidator ID="RequiredInsertUrl" runat="server" ControlToValidate="txtInsertUrl" Display="Dynamic" ErrorMessage="Nhập nơi lưu chuyên mục" SetFocusOnError="True"></asp:RequiredFieldValidator>
        </FooterTemplate>
        <ItemStyle HorizontalAlign="Left" />
      </asp:TemplateField>
      
      
      <asp:TemplateField HeaderText="Trạng thái">
        <ItemTemplate>
          <%# m_CategoryStatus.Get(cboCategoryStatus, Convert.ToByte(Eval("CategoryStatusId"))).CategoryStatusDesc%>
        </ItemTemplate>
        <EditItemTemplate>
          <asp:DropDownList ID="ddlCategoryStatus" runat="server" DataSource='<%#cboCategoryStatus %>' DataTextField="CategoryStatusDesc" DataValueField="CategoryStatusId" SelectedValue='<%#Bind("CategoryStatusId") %>' />
        </EditItemTemplate>
        <FooterTemplate>
          <asp:DropDownList ID="ddlInsertCategoryStatus" runat="server" DataSource='<%#cboCategoryStatus %>' DataTextField="CategoryStatusDesc" DataValueField="CategoryStatusId" SelectedValue='<%#Bind("CategoryStatusId") %>' />
        </FooterTemplate>
        <ItemStyle HorizontalAlign="Left" />
      </asp:TemplateField>
      
      <asp:TemplateField HeaderText="Biểu tượng">
        <ItemTemplate> <%#Eval("ImageIcon")%> </ItemTemplate>
        <EditItemTemplate>
          <asp:TextBox ID="txtImageIcon" runat="server" Columns="20" MaxLength="100" Text='<%# Bind("ImageIcon") %>' />
        </EditItemTemplate>
        <FooterTemplate>
          <asp:TextBox ID="txtInsertImageIcon" runat="server" Columns="20" MaxLength="100"></asp:TextBox><br />
        </FooterTemplate>
        <ItemStyle HorizontalAlign="Left" />
      </asp:TemplateField>     
      <asp:TemplateField HeaderText="Người cập nhật">
        <ItemTemplate>
          <%#Eval("CrUserId")%>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Right" />
      </asp:TemplateField>
      <asp:TemplateField HeaderText="Ngày cập nhật">
        <ItemTemplate> <%#Eval("CrDateTime")%></ItemTemplate>
      </asp:TemplateField> 
      <asp:TemplateField>
        <HeaderStyle Width="110px"></HeaderStyle>
        <ItemTemplate>
          <asp:LinkButton ID="cmdEdit" runat="server" CommandName="Edit" CausesValidation="false">Sửa</asp:LinkButton>&nbsp;
          <asp:LinkButton ID="cmdDelete" runat="server" CommandName="Delete" CausesValidation="false">Xo&#225;</asp:LinkButton>
        </ItemTemplate>
        <FooterTemplate>
          <asp:LinkButton ID="cmdInsert" runat="server" CommandName="Insert">Thêm mới</asp:LinkButton>
        </FooterTemplate>
        <EditItemTemplate>
          <asp:LinkButton ID="cmdUpdate" runat="server" CommandName="Update" CausesValidation="false">Cập nhật</asp:LinkButton>&nbsp;
          <asp:LinkButton ID="cmdCancel" runat="server" CommandName="Cancel" CausesValidation="false">Tho&#225;t</asp:LinkButton>
        </EditItemTemplate>
      </asp:TemplateField>
    </Columns>
  </asp:GridView>
  Số lượng chuyên mục: <%=m_grid.Rows.Count %>  
</asp:Content>
