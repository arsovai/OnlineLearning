<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/MasterPage.Master" CodeFile="ViewSubjects.aspx.cs" Inherits="ViewSubjects" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="margin-top:90px; margin-left: 35px;"> <asp:Label ID="poraka" runat="server" ></asp:Label> </div>

    <asp:GridView ID="gvSubjects" runat="server" AutoGenerateColumns="False" 
            OnRowCommand="subjects_RowCommand" CellPadding="4" EnableModelValidation="True" 
            ForeColor="#333333" GridLines="None">  
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Label Visible="false" runat="server" ID="SubjectID" Value='<%# Eval("SubjectID") %>' Text='<%# Eval("SubjectID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField> 
                <asp:BoundField DataField="SubjectName" SortExpression="Name" HeaderText="Предмет" />
                <asp:BoundField DataField="Semestar" SortExpression="Semestar" HeaderText="Семестар" />
                <asp:BoundField DataField="Credits" SortExpression="Credits" HeaderText="Кредити" />
                <asp:ButtonField CommandName="createOnlineLearning" Text="Креирај онлајн учење" Visible="false" />
                <asp:ButtonField CommandName="selectOblast" Text="Одбери област" />
            </Columns>
        <EditRowStyle BackColor="#999999" />
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
    </asp:GridView> 

    <asp:Panel ID="panelCreateOL" runat="server" Visible="false">
        <table>
            <tr>
                <td> <asp:Label ID="Label1" runat="server" Text="Област: "></asp:Label> </td>
                <td>  <asp:DropDownList ID="ddlOblast" AutoPostBack="true" runat="server" AppendDataBoundItems="true">
                </asp:DropDownList></td>
            </tr>
            <tr>
                <td> <asp:Label ID="Label2" runat="server" Text="Стратегија: "></asp:Label> </td>
                <td>  <asp:DropDownList ID="ddlStrategy" runat="server" AutoPostBack="true" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlStrategy_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
    </asp:Panel>

    <asp:Panel ID="scValue" runat="server" Visible="false">
        <table>
        <tr>
        <td> <asp:Label ID="Label3" runat="server" Text="Број на последователно точни одговори: "></asp:Label> </td>
        <td> <asp:TextBox ID="tbSubsequentlyCorrect" runat="server"></asp:TextBox> </td>
        </tr>
        </table>
    </asp:Panel>

    <asp:Panel ID="tcValue" runat="server" Visible="false">
        <table>
        <tr>
        <td> <asp:Label ID="Label4" runat="server" Text="Број на вкупно точни одговори: "></asp:Label> </td>
        <td> <asp:TextBox ID="tbTotalCorrect" runat="server"></asp:TextBox> </td>
        </tr>
        </table>
    </asp:Panel>

    <asp:Button ID="createOnlineLearning" runat="server" Text="Create" Visible="false" OnClick="createOnlineLearning_Click" />

    <div style="float: left;">
        <asp:GridView ID="gvoblasti" runat="server" AutoGenerateColumns="False" CellPadding="4" 
                    EnableModelValidation="True" ForeColor="#333333" GridLines="None" OnRowCommand="oblasti_RowCommand">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Label Visible="false" runat="server" ID="OblastID" Value='<%# Eval("OblastID") %>' Text='<%# Eval("OblastID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField> 
                        <asp:BoundField DataField="Name" HeaderText="Област" SortExpression="Name" />                                     
                        <asp:ButtonField CommandName="startOnlineLearning" Text="Почни онлајн учење" /> 
             </Columns>
            <EditRowStyle BackColor="#999999" />
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
        </asp:GridView>
    </div>
</asp:Content>
