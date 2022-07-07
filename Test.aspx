<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Test.aspx.cs" Inherits="Test" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
	<meta charset="utf-8" />
	
</head>
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@4.0.0/dist/css/bootstrap.min.css" integrity="sha384-Gn5384xqQ1aoWXA+058RXPxPg6fy4IWvTNh0E263XmFcJlSAwiGgFAW/dAiS6JXm" crossorigin="anonymous">
    <script src="https://code.jquery.com/jquery-3.2.1.slim.min.js" integrity="sha384-KJ3o2DKtIkvYIK3UENzmM7KCkRr/rE9/Qpg6aAZGJwFDMVNA/GpGFF93hXpG5KkN" crossorigin="anonymous"></script>
<script src="https://cdn.jsdelivr.net/npm/popper.js@1.12.9/dist/umd/popper.min.js" integrity="sha384-ApNbgh9B+Y1QKtv3Rn7W3mgPxhU9K/ScQsAP7hUibX39j7fakFPskvXusvfa0b4Q" crossorigin="anonymous"></script>
<script src="https://cdn.jsdelivr.net/npm/bootstrap@4.0.0/dist/js/bootstrap.min.js" integrity="sha384-JZR6Spejh4U02d8jOt6vLEHfe/JQGiRRSQQxSfFWpi1MquVdAyjUar5+76PVCmYl" crossorigin="anonymous"></script>
<body>
    <form id="form1" runat="server">
		<div class="container">
			<div class="row ">
				<asp:ScriptManager ID="ScriptManager1" runat="server">
				</asp:ScriptManager>

				<div class="col-10" >
					<asp:TextBox ID="TextBox1" CssClass="col-10"  runat="server" Height="400px" TextMode="MultiLine"> </asp:TextBox>
					
						<cc1:HtmlEditorExtender ID="HtmlEditorExtender1" runat="server" DisplaySourceTab="true" TargetControlID="TextBox1" EnableSanitization="false">
						<Toolbar>
							<cc1:Undo />
							<cc1:Redo />
							<cc1:Bold />
							<cc1:Italic />
							<cc1:Underline />
							<cc1:StrikeThrough />
							<cc1:Subscript />
							<cc1:Superscript />
							<cc1:JustifyLeft />
							<cc1:JustifyCenter />
							<cc1:JustifyRight />
							<cc1:JustifyFull />
							<cc1:InsertOrderedList />
							<cc1:InsertUnorderedList />
							<cc1:CreateLink />
							<cc1:UnLink />
							<cc1:RemoveFormat />
							<cc1:SelectAll />
							<cc1:UnSelect />
							<cc1:Delete />
							<cc1:Cut />
							<cc1:Copy />
							<cc1:Paste />
							<cc1:BackgroundColorSelector />
							<cc1:ForeColorSelector />
							<cc1:FontNameSelector />
							<cc1:FontSizeSelector />
							<cc1:Indent />
							<cc1:Outdent />
							<cc1:InsertHorizontalRule />
							<cc1:HorizontalSeparator />
							<cc1:InsertImage />
						</Toolbar>
					</cc1:HtmlEditorExtender>					
					
				</div>
				<br />
			</div>
			<div class="row">
				<asp:Button ID="Button1" runat="server" Text="saveFile" />
			</div>

		</div>
    </form>
</body>
</html>
