<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShoppingCart.aspx.cs" Inherits="HomeLib_Shopping_Cart" MasterPageFile="~/HomeLib.master" %>

<asp:Content ID="CartContent" ContentPlaceHolderID="ContentPlaceHolderHomeLib" runat="server">
   
    <link href="../Content/Cart.css" rel="stylesheet" />
    <div style="margin-top: 80px">
        <section id="sectionCart" class="pt-5 pb-5" runat="server">
            <div class="container">
                <div class="row">
                    <div class="col-lg-12 col-md-12 col-12">
                        <h3 class="display-5 mb-2 text-center">Giỏ tài liệu</h3>     
                        <asp:Repeater ID="Repeater1" runat="server" OnItemCommand="Repeater1_ItemCommand" OnItemDataBound="Repeater1_ItemDataBound">
                            <HeaderTemplate>
                                <table id="shoppingCart" class="table table-condensed table-responsive ">
                                    <thead>
                                        <tr>
                                            <th style="width: 2%"></th>
                                            <th style="width: 80%"></th>                                            
                                            <th style="width: 16%"></th>
                                        </tr>
                                    </thead>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tbody>
                                    <tr>
                                        <td data-th="" class="align-middle"><%# Repeater1.Items.Count +1 %></td>
                                        <td data-th="Nhan đề">
                                            <div class="row">
                                                <div class="col-md-2 text-left">
                                                    <asp:Image ID="ImageBook" runat="server" Height="90" CssClass="img-fluid d-none d-md-block rounded mb-2 shadow" />
                                                </div>
                                                <div class="col-md-10 text-left mt-sm-2 ">
                                                    <h5><a class="text-black link" href='<%# string.Format("/chi-tiet?id={0}", Eval("RecordID"))%>' /><%# Eval("title") %></a></h5>
                                                    <p class="font-weight-light"></p>
                                                </div>
                                            </div>
                                        </td>                                       
                                        <td class="actions" data-th="">
                                            <div class="text-right">
                                                <asp:LinkButton ID="LinkTrash" runat="server" CommandName="DeleteProducts" CommandArgument='<%# Eval("RecordID") %>' CssClass="btn btn-white border-secondary bg-white btn-md mb-2" ToolTip="Xóa cuốn này"><i class="fa fa-trash"></i></asp:LinkButton>

                                            </div>
                                        </td>
                                    </tr>
                                </tbody>
                            </ItemTemplate>
                            <FooterTemplate>
                                </table>
                            </FooterTemplate>
                        </asp:Repeater>
                       
                    </div>
                </div>
                <div class="row mt-4 ml-lg-5 d-flex align-items-center ">
                    <div class="col-sm-6 mb-3 mb-m-1 order-md-1 text-md-left">
                        <button id="prev" runat="server" type="submit" onclick="javascript:history.back(); return false;" class="btn btn-default" aria-hidden="True"><i class="fa fa-arrow-left mr-2"></i>Chọn thêm sách</button>
                    </div>
                    <div class="col-sm-6 mb-3 mb-m-1 order-md-1 text-md-right">
                        <button id="ExportExcel" runat="server" type="submit" onserverclick="ExportToExcel" class="btn btn-default" aria-hidden="True" visible="false"><i class="fas fa-arrow-left mr-2"></i>Xuất Excel</button>
                       
                    </div>
                </div>
            </div>
        </section>       
 </div>
    <div class="container" style="margin-top: 150px" id="cartno" runat="server">
        <div class="row mt-4 d-flex align-items-center">
            <div class="col-lg-12 col-md-12 col-12">
                <div class="text-center align-middle">
                    <span><i class="fa fa-shopping-cart fa-5x" aria-hidden="true">404</i></span>
                </div>
                <h5 class="display-5 mb-2 mt-lg-5 text-center">Không có cuốn nào trong giỏ tài liệu</h5>
                <div class="mb-3 mb-m-1 order-md-1 text-center">
                    <a id="Button1" href="/fts" class="btn btn-default text-black" aria-hidden="True"><i class="fa fa-arrow-left mr-2"></i>Tiếp tục tìm sách</a>
                </div>
            </div>
        </div>
    </div>
</asp:Content>


