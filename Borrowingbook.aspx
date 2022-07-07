<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Borrowingbook.aspx.cs" Inherits="borrowingbook" MasterPageFile="~/Site.master" %>


<asp:Content ID="CartContent" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        $('.has-clear input[type="text"]').on('input propertychange', function () {
            var $this = $(this);
            var visible = Boolean($this.val());
            $this.siblings('.form-control-clear').toggleClass('hidden', !visible);
        }).trigger('propertychange');

        $('.form-control-clear').click(function () {
            $(this).siblings('input[type="text"]').val('')
              .trigger('propertychange').focus();
        });
    </script>

    <style type="text/css">
        ::-ms-clear {
            display: none;
        }

        .form-control-clear {
            z-index: 10;
            pointer-events: auto;
            cursor: pointer;
        }
    </style>
    <div class="container-fluid" style="font-size: 13px; font-family: Arial">
        <div class="row">
            <div class=" col-lg-8" style="margin-top: 80px; line-height: 25px">
                <div class="col-sm form-inline">
                    <img src="Logo/kisspng.png" height="50" />
                    <h4 style="color: dimgray">KIỂM TRA SÁCH ĐANG MƯỢN</h4>

                </div>
                <div class="btn-group" style="margin-bottom: 20px">
                    <div class="input-group  col-sm-6 col-lg-6">
                        <div class="form-group has-feedback has-clear  ">
                            <asp:TextBox ID="TxtMS" runat="server" class="form-control " placeholder="Nhập MSSV hoặc Email"></asp:TextBox>
                            <span class="form-control-clear glyphicon glyphicon-remove form-control-feedback hidden"></span>
                        </div>
                        <span class="input-group-btn">
                            <asp:Button ID="Bu_check" runat="server" Text="Kiểm tra" CssClass="btn btn-primary" OnClick="Bu_check_Click" />
                        </span>
                    </div>
                     <div>
                    <asp:Label ID="Label_info" runat="server" ForeColor="#FF3300"></asp:Label>
                </div>
                </div>               
                <asp:DetailsView ID="DetailsView_info" runat="server" CssClass="col-xs-12 col-lg-8" CellPadding="5" GridLines="Horizontal" AutoGenerateRows="False" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px">
                    <AlternatingRowStyle BackColor="#F7F7F7" />
                    <EditRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                    <Fields>

                        <asp:BoundField DataField="CardNumber" HeaderText="Mã độc giả:" />
                        <asp:BoundField DataField="ReaderName" HeaderText="Họ và tên:">
                            <ItemStyle Font-Bold="False" />
                        </asp:BoundField>
                        <asp:BoundField DataField="GroupName" HeaderText="Địa chỉ:" />
                        <asp:BoundField DataField="DateOfBirth" HeaderText="Ngày sinh:" />
                        <asp:BoundField DataField="Loan" HeaderText="Tình trạng nợ:">
                            <ItemStyle ForeColor="#6600FF" />
                        </asp:BoundField>
                    </Fields>
                    <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                    <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                    <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                    <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" />
                </asp:DetailsView>
            </div>

            <br />

        </div>
        <br />
        <div class=" row">
            <div class="col-lg-3 col-sm-4 col-xs-12">
                <asp:DropDownList ID="DropDownValue" runat="server" CssClass="form-control" AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="DropDownValue_SelectedIndexChanged" Visible="False">
                    <asp:ListItem Value="0">Sách còn nợ</asp:ListItem>
                    <asp:ListItem Value="1">Sách nợ và mượn trả trong ngày</asp:ListItem>
                    <asp:ListItem Value="2">Lịch sử mượn trả</asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-lg-10" style="line-height: 28px">
                <div id="dvCustomers">
                    <asp:DataList ID="DataListBook" runat="server" OnItemDataBound="DataListBook_ItemDataBound">
                        <ItemTemplate>
                            <div class="row">
                                <div class="col-sm-10 col-lg-10">
                                    <span class="label label-danger ">
                                        <asp:Label ID="Label1" runat="server" Text='<%#Eval("RowNumber")%>'></asp:Label></span>
                                    &nbsp;&nbsp;<strong style="color: dimgrey">Mã sách: </strong>
                                    <asp:Label ID="La_CopyID" runat="server" Text='<%#Eval("CopyID")%>' ForeColor="#009999" Font-Bold="true"></asp:Label>

                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm">
                                    <asp:HyperLink ID="Hyper_title" runat="server" NavigateUrl='<%#"/chi-tiet?id="+ Eval("RecordID")%>' Text='<%#Eval("Title")%>' Font-Bold="true">
                                        
                                    </asp:HyperLink>
                                    <div class="col-sm">
                                        Ngày mượn:
                          <asp:Label ID="La_StartDate" runat="server" Text='<%#  Eval("StartDate0")%>'></asp:Label>
                                    </div>
                                    <div class="col-sm">
                                        Hạn trả:
                           &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="La_Enddate" runat="server" Text='<%#Eval("EndDate")%>'></asp:Label>
                                    </div>
                                    <div class="col-sm">
                                        <span class="glyphicon glyphicon-log-out"></span>&nbsp;&nbsp;Ghi chú:                           
                                &nbsp;&nbsp;<span class="glyphicon glyphicon-time"></span>
                                        <asp:Label ID="La_Note" runat="server" Text='<%#Eval("Note")%>'></asp:Label>
                                    </div>
                                </div>
                                <div style="border-bottom: dashed 1px #ff6a00">
                                </div>
                        </ItemTemplate>
                    </asp:DataList>
                </div>
            </div>
        </div>
    </div>

</asp:Content>


