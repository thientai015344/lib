<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Tailieulinhvuc.aspx.cs" Inherits="HomeLib_Tailieuchude" MasterPageFile="~/HomeLib.master" %>


<asp:Content ID="CartContent" ContentPlaceHolderID="ContentPlaceHolderHomeLib" runat="server">
    <style>
        a:hover {
            text-decoration: underline;
        }
    </style>
    <div class="container" style="margin-top: 150px">
        <div class="row">
            <h4 class="ml-5 mb-3">TÀI LIỆU THEO LĨNH VỰC </h4>
            <div class="col-md-12 col-sm-12 col-lg-12 mb-5">
            <asp:DataList ID="DataList1" runat="server" Width="100%" HorizontalAlign="Center" CellPadding="4" ForeColor="#333333">
                <AlternatingItemStyle BackColor="White" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <ItemStyle BackColor="#EFF3FB" />
                <ItemTemplate>                    
                    <table>
                        <tr>
                            <td rowspan="2">
                                <div class="col-2 text-left ">
                                    <label style="font-weight:600"><%# Eval("Dewey")%> </label>
                                </div>
                            </td>
                            <td>
                                <div>
                                    <a href='<%# String.Format("/fts?q=ddc:{0}*&Field=&start=0&ck=false",Eval("Link")) %>' style="color: #05687a; font-weight: 600; text-decoration: underline"><%# Eval("Sub_TiengViet")%> </a>
                                </div>
                            </td>
                        </tr>
                        <tr>                            
                            <td>
                                <label style="font-style: italic"><%# Eval("Sub_TiengAnh")%> </label>
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
                <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            </asp:DataList>                
            </div>
        </div>
    </div>
</asp:Content>
