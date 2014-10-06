<%@ Page Language="C#" MasterPageFile="~/MasterPage.Master" CodeFile="OnlineLearning.aspx.cs" Inherits="OnlineLearning" Debug="true"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div>
		
		<asp:SqlDataSource ID="SQLQuizDataSouce" runat="server" ConnectionString="<%$ ConnectionStrings:ApplicationServices %>"
				SelectCommand="SELECT * FROM Choices"></asp:SqlDataSource>
		<br /><br />         
	   
		<asp:Button ID="startButton" runat="server" Text="Започни учење"  OnClick="startButton_click" Visible="false" Width="120px" />
		<br /><br />                 
		

		<asp:Table ID="Table1" runat="server" Height="39px" Width="579px">    
		</asp:Table>
		<br />
	
		<asp:RadioButtonList ID="choicesRadioButtonList" runat="server"         
			onselectedindexchanged="choicesRadioButtonList_SelectedIndexChanged" >  
		</asp:RadioButtonList>
		<br /> 

		<div class="newLine">
			<br />
			<asp:Button ID="nextButton" runat="server" Text="Следно прашање" OnClick="nextButton_click" 
					Width="120px" />
			&nbsp;&nbsp;&nbsp;
			<asp:Button ID="finishButton" runat="server" Text="Заврши учење" OnClick="finish_click" 
					Width="120px" />
		</div>
				  
	
		<div style="margin-left: 35px;">
			<asp:Panel runat="server" ID="panel" />
			<asp:Label ID="resultsLabel" runat="server"/>  
		</div>
</div>
</asp:Content>