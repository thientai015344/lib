<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NewBookInfo.aspx.cs" Inherits="test" MasterPageFile="~/Site.master" %>


<asp:Content ID="CartContent" ContentPlaceHolderID="MainContent" runat="server">
	<link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css" />
	<script src="https://code.jquery.com/jquery-1.12.0.min.js"></script>
	<script src='https://www.google.com/recaptcha/api.js'></script>
	<link href="../Content/bootstrap.css" rel="stylesheet" />
	<link href="../Content/bootstrap-datetimepicker.css" rel="stylesheet" />
	<link href="../Content/daterangepicker.css" rel="stylesheet" />
	<link rel="stylesheet" href="../Content/bootstrap.min.css" />
	<link rel="stylesheet" href="../Content/bootstrapValidator.min.css" />
	<link href="../Content/PageNumber.css" rel="stylesheet" />

	<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>

	<style type="text/css">
		.table_td_thumbnail {
			width: 10%;
		}

		#table_result tr > td {
			border: 0;
			font-family: Arial;
			font-size: 13px;
			line-height: 25px;
		}

		strong {
			color: #727070;
		}

		a.link {
			font-family: Merriweather;
			color: #0066C0;
			font-size: 1.1em;
			font-weight: 600
		}
	</style>

	<div class="container" style="margin-top: 80px;">
		<div class="row">
			<div class="form-inline">
				<h4>
					<img src="Logo/new book.jpg" height="30" />
					SÁCH MỚI HÀNG NGÀY</h4>
			</div>
			<br />
			<div class='col-xs-12 col-sm-10 col-md-10 col-lg-10'>
				<div class="form-inline">
					<div>
						<label>Thời gian</label>
						<asp:DropDownList ID="DropDown_time" CssClass="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDown_time_SelectedIndexChanged">
							<asp:ListItem Value="0">Hôm nay</asp:ListItem>
							<asp:ListItem Value="1">Tuần qua</asp:ListItem>
							<asp:ListItem Value="2">Tháng qua</asp:ListItem>
							<asp:ListItem Value="3">Ba tháng qua</asp:ListItem>
							<asp:ListItem Value="4">Năm qua</asp:ListItem>
						</asp:DropDownList>
						<div class="form-group">
							<label>Lĩnh vực:</label>
							<asp:DropDownList ID="DRLV" CssClass="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DRLV_SelectedIndexChanged" AppendDataBoundItems="True"></asp:DropDownList>
						</div>
					</div>
					<div class="text-right col-sm-12 col-md-7">
						<div class="checkbox-inline">
							<asp:CheckBox ID="Check_haveFile" CssClass="form-check-input" runat="server" ForeColor="#666666" AutoPostBack="True" OnCheckedChanged="Check_haveFile_CheckedChanged" Text="Sách có file" />
						</div>
					</div>
				</div>
			</div>
		</div>
		<div>
			<asp:Label ID="Label_thongtin" runat="server"></asp:Label>
		</div>
		<div class="row">
			<div class="col-md-12">
				<asp:Repeater ID="RData" runat="server" OnItemDataBound="RData_ItemDataBound">
					<ItemTemplate>
						<div style="border-top: 1px dotted #c1650f">
							<table id="table_result">
								<asp:HyperLink ID="hplBookName" CssClass="link" Font-Size="1em" ForeColor="#333300" EnableTheming="True" NavigateUrl='<%#"/chi-tiet?id="+ Eval("RecordID")%>' Text='<%# LimitLength(Eval("Nhan đề").ToString(),100,"") %>' ToolTip='<%# DataBinder.Eval(Container.DataItem, "Nhan đề")%>' runat="server" Target="_blank">
								</asp:HyperLink>
								</div>
                                <tr>
									<td colspan="2">
										<div>
									</td>
								</tr>
								<tr>
									<td rowspan="2" class="table_td_thumbnail">
										<div style="margin-right: 7px">
											<asp:HyperLink ID="hplImage" runat="server" NavigateUrl='<%#"/chi-tiet?id="+ Eval("RecordID")%>' Target="_blank">
												<asp:Image ID="ImageBook" runat="server" ImageAlign="Middle" class="thumbnail " Width="95" ToolTip='<%# DataBinder.Eval(Container.DataItem, "Nhan đề")%>' />
											</asp:HyperLink>
										</div>
									</td>
									<td>
										<strong>Tác giả:</strong>
										<asp:Repeater ID="repLinks" runat="server" ViewStateMode="Enabled">
											<ItemTemplate>
												<asp:HyperLink runat="server" NavigateUrl='<%# string.Format("/fts?q=au:{0}{1}", HttpUtility.UrlEncode(Container.DataItem.ToString().Replace("(GVHD)","")), "&ck=false") %>' Text='<%# Container.DataItem.ToString() + ", " %>' ID="HL" ForeColor="#0066CC" />
											</ItemTemplate>
										</asp:Repeater>
										<br />
										<strong>Thông tin xuất bản: </strong>
										<asp:Label ID="lbNamXB" runat="server" Text='<%# Eval("Nơi XB").ToString() + Eval("Nhà XB").ToString() +" "+ LimitLength(Eval("Năm XB").ToString(),50,"") %>' />
										<br />
										<strong>Mô tả vật lý:</strong>
										<asp:Label ID="MTvatly" runat="server" Text='<%#Eval("Mô tả vật lý").ToString() %>'></asp:Label>
										<br />
										<strong>Ký hiệu phân loại:</strong>
										<asp:HyperLink ID="HyperLink3" runat="server" Text='<%#Eval("Phân loại").ToString() %>' NavigateUrl='<%# Eval("[Phân loại]","/fts?q=ddc:{0}&ck=false") %>' Font-Bold="True"></asp:HyperLink>
										<br />
										<strong><span class="glyphicon glyphicon-book"></span>Bộ sưu tập:</strong>
										<asp:HyperLink ForeColor="#336699" Font-Bold="true" ID="HyperGroupName" NavigateUrl='<%# string.Format("fts?q=grid:{0}{1}", Eval("Nhomtailieu"), "&ck=false") %>' runat="server"><%#Eval("[Loại tài liệu]") %></asp:HyperLink>
									</td>
								</tr>
								<tr>
									<td></td>
								</tr>
							</table>

						</div>
					</ItemTemplate>
				</asp:Repeater>
			</div>
		</div>
		<div class="row">
			<div class=" text-center">
				<asp:Repeater ID="rptPager" runat="server">
					<ItemTemplate>
						<asp:LinkButton ID="lnkPage" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>'
							CssClass='<%# Convert.ToBoolean(Eval("Enabled")) ? "page_enabled" : "page_disabled" %>'
							OnClick="Page_Changed" OnClientClick='<%# !Convert.ToBoolean(Eval("Enabled")) ? "return false;" : "" %>'></asp:LinkButton>
					</ItemTemplate>
				</asp:Repeater>
			</div>
		</div>
	</div>
</asp:Content>
