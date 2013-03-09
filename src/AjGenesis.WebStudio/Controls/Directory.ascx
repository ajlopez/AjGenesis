<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Directory.ascx.vb" Inherits="Controls_Directory" %>
<asp:GridView ID="grdDirectory" runat="server" AutoGenerateColumns="False" CssClass="DataTable" Width="400" DataKeyNames="Name">
    <Columns>
        <asp:BoundField DataField="Name" HeaderText="Name" />
        <asp:TemplateField HeaderText="Commands">
            <ItemTemplate>
                <asp:LinkButton ID="lnkView" runat="server" CausesValidation="False" CommandName="View"
                    Text="View" CommandArgument='<%# Eval("Name") %>'></asp:LinkButton>
                    
                <asp:LinkButton ID="lnkDownload" runat="server" CausesValidation="False"
                        CommandName="Download" Text="Download" CommandArgument='<%# Eval("Name") %>' Visible='<%# Not Eval("IsFile") %>'></asp:LinkButton>
                    
                <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="False"
                        CommandName="Edit" Text="Edit" CommandArgument='<%# Eval("Name") %>' Visible='<%# Eval("IsFile") %>'></asp:LinkButton>
                    
                <asp:LinkButton ID="lnkDelete" runat="server" CausesValidation="False"
                        CommandName="DeleteFile" Text="Delete" CommandArgument='<%# Eval("Name") %>' Visible='<%# Eval("IsFile") %>'></asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
