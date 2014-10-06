<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.Master" CodeFile="Admin.aspx.cs" Inherits="Admin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>

        <asp:SqlDataSource ID="SQLSubjectsDataSouce" runat="server" ConnectionString="<%$ ConnectionStrings:ApplicationServices %>" 
        SelectCommand="SELECT * FROM Subject">
        </asp:SqlDataSource>

        <asp:SqlDataSource ID="SQLQuestionsDataSouce" runat="server" ConnectionString="<%$ ConnectionStrings:ApplicationServices %>"
            DeleteCommand="DELETE FROM Questions WHERE QuestionID = @QuestionID" 
            SelectCommand="SELECT * FROM Questions" 
            UpdateCommand="UPDATE Questions SET QuestionNumber = &#13;&#13;&#10;@QuestionNumber, QuestionText = @QuestionText WHERE QuestionID = @QuestionID">
            <DeleteParameters>
                <asp:Parameter Name="QuestionID" />
            </DeleteParameters>
            <UpdateParameters>
                <asp:Parameter Name="QuestionNumber" />
                <asp:Parameter Name="QuestionText" />
                <asp:Parameter Name="QuestionID" />
            </UpdateParameters>
        </asp:SqlDataSource>

        <asp:SqlDataSource ID="SQLChoicesDataSouce" runat="server" ConnectionString="<%$ ConnectionStrings:ApplicationServices %>"
            DeleteCommand="DELETE FROM Choices WHERE ChoiceID = @ChoiceID" 
            InsertCommand="INSERT INTO Choices VALUES (@QuestionID, @ChoiceLetter, @ChoiceText)"
            SelectCommand="SELECT * FROM Choices" 
            UpdateCommand="UPDATE Choices SET QuestionID = @QuestionID, ChoiceLetter = @ChoiceLetter, ChoiceText = &#13;&#13;&#10;@ChoiceText WHERE  ChoiceID = @ChoiceID">
            <DeleteParameters>
                <asp:Parameter Name="ChoiceID" />
            </DeleteParameters>
            <UpdateParameters>
                <asp:Parameter Name="QuestionID" />
                <asp:Parameter Name="ChoiceLetter" />
                <asp:Parameter Name="ChoiceText" />
                <asp:Parameter Name="ChoiceID" />
            </UpdateParameters>
            <InsertParameters>
                <asp:Parameter Name="QuestionID" />
                <asp:Parameter Name="ChoiceLetter" />
                <asp:Parameter Name="ChoiceText" />
            </InsertParameters>
        </asp:SqlDataSource>        
        &nbsp;
    </div>
    <div>
        <table id="parentTable" runat="server" style="height: 280px; float:left; display:block;" cellspacing="0" border="0" cellpadding="10">
            <tr>              
                <td valign="top" style="width: 800px; text-align: left;">
                    <h3 style="text-align: left">Додади прашање</h3>
                    <table id="insertTable" border="0" style="height:200px">
                        <tr>
                            <td>
                                <asp:Label ID="SubjectLabel" runat="server" Text="Предмет:"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="SubjectDropDownList" runat="server" 
                                    onselectedindexchanged="SubjectDropDownList_SelectedIndexChanged" AutoPostBack="True" AppendDataBoundItems="true">
                                    <asp:ListItem Text="Предмети" Value="0" Selected="True"> </asp:ListItem>    
                                </asp:DropDownList>
                            </td>
                        </tr>
                         <tr>
                            <td>
                                <asp:Label ID="FieldLabel" runat="server" Text="Област:"></asp:Label>
                            </td>                            
                            <td>
                                <asp:DropDownList ID="ChapterDropDownList" runat="server"
                                    onselectedindexchanged="ChapterDropDownList_SelectedIndexChanged" AutoPostBack="True" AppendDataBoundItems="false">
                                 <asp:ListItem Text="Области" Value="0" Selected="True" Enabled="true"></asp:ListItem>    
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="QuestionLabel" runat="server" Text="Текст на прашањето:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="QuestionTextBox" runat="server" Width="250px" height="20px"></asp:TextBox>
                            </td>
                        </tr>               
                        <tr>
                            <td>
                                <asp:Button ID="InsertButton" runat="server" Text="Додади" onclick="InsertButton_Click" />
                            </td>
                        </tr>
                    </table>
                    <asp:GridView ID="questionsGridView" runat="server" AllowPaging="True" 
                        AllowSorting="True" AutoGenerateEditButton="True" 
                        DataSourceID="SQLQuestionsDataSouce" Height="600px" Width="600px" CellPadding="4" 
                        ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" 
                        DataKeyNames="QuestionID" EnableModelValidation="True" >
                        
                        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                        <RowStyle BackColor="#E3EAEB" />
                        <EditRowStyle BackColor="#7C6F57" />
                        <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="left" />
                        <HeaderStyle BackColor="#000066" Font-Bold="True" ForeColor="White" />
                        <AlternatingRowStyle BackColor="White" />
                        
                        <Columns>
                            <asp:BoundField DataField="QuestionID" HeaderText="QuestionID" InsertVisible="False" ReadOnly="True" SortExpression="QuestionID" />
                            <asp:BoundField DataField="QuestionNumber" HeaderText="QuestionNumber" SortExpression="QuestionNumber" />
                            <asp:BoundField DataField="QuestionText" HeaderText="QuestionText" SortExpression="QuestionText" />
                        </Columns>
                    </asp:GridView>
                    <br /><asp:Label ID="QuestionOutputLabel" runat="server" Text="" Visible="true"></asp:Label>
                </td>                
                <td valign="top" style="text-align: left; width: 500px;">
                    <h3 style="text-align: left">Додади одговор</h3>      
                    <table id="answerTable" border="0" style="height:200px">
                        <tr>
                            <td>
                                <asp:Label ID="choiceQuestionLabel" runat="server" Text="Прашање:"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="allQuestionsDropDownList" runat="server"  Width="300px"
                                 AutoPostBack="True" AppendDataBoundItems="true">
                                    <asp:ListItem Text="Прашања" Value="0" Selected="True"> </asp:ListItem>    
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="choiceLetterLabel" runat="server" Text="Буква за одговорот:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="choiceLetterTextBox" runat="server" Width="250px" height="20px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="choiceTextLabel" runat="server" Text="Текст на одговорот:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="choiceTextBox" runat="server" Width="250px" height="20px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="rightChoiceLabel" runat="server" Text="Дали е точен одговор? (0/1)"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="rightChoiceTextBox" runat="server" Width="250px" height="20px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="choiceOutputLabel" runat="server" Text="" Visible="false"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="AddAnswerButton" runat="server" Text="Додади" onclick="AddAnswerButton_Click" />
                            </td>
                        </tr>
                    </table>                  
                    <asp:GridView ID="answersGridView" runat="server" 
                        DataSourceID="SQLChoicesDataSouce" AllowSorting="True" AllowPaging="True" 
                        AutoGenerateEditButton="True" Height="600px" Width="600px" CellPadding="4" 
                        ForeColor="#333333" GridLines="None" EnableModelValidation="True">

                        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                        <RowStyle BackColor="#E3EAEB" />
                        <EditRowStyle BackColor="#7C6F57" />
                        <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                        <HeaderStyle BackColor="#000066" Font-Bold="True" ForeColor="White" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
                    <br /><asp:Label ID="answerOutputLabel" runat="server" Text="" Visible="true"></asp:Label>
                </td>
            </tr>
        </table>
     </div>
</asp:Content>