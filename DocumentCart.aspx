<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DocumentCart.aspx.cs" Inherits="ViewCart" MasterPageFile="~/Site.master" %>

<%@ Register TagPrefix="recaptcha" Namespace="Recaptcha" Assembly="Recaptcha" %>

<asp:Content ID="CartContent" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function goBack() {
            var ref = document.referrer;
            window.location.href = ref;
        }
        function goBackcookie() {
            document = get;
        }
    </script>
    <br />
    <br />
    <br />
    <div class="container-fluid" style="font-family: Arial">
        <div class="row">
            <div id="divCart" runat="server" class="col-lg-12 col-lg-offset-2">
                <h2>Giỏ tài liệu</h2>
            </div>
            <div id="div_checkCart" runat="server" class=" text-center" visible="false">
                <h5>Không có cuốn nào trong giỏ tài liệu</h5>
                <asp:LinkButton ID="LinkButton1" class="btn btn-warning" PostBackUrl="~/" runat="server">Tiếp tục tìm sách</asp:LinkButton>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-12 " style="margin-bottom: 10px">
                <asp:GridView ID="GridViewCart" runat="server" Font-Names="Arial,sans-serif" Width="100%" AutoGenerateColumns="False" OnRowCancelingEdit="GridViewCart_RowCancelingEdit" OnRowEditing="GridViewCart_RowEditing" OnRowUpdating="GridViewCart_RowUpdating" OnRowDeleting="GridViewCart_RowDeleting" GridLines="None" CellPadding="8" CssClass="tab-content" ForeColor="#333333" CellSpacing="10">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                        <asp:BoundField DataField="RecordID" HeaderText="ID" HeaderStyle-CssClass="col-sm-1 col-md-1" ReadOnly="True">
                            <HeaderStyle></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:HyperLinkField DataNavigateUrlFields="RecordID" DataNavigateUrlFormatString="chi-tiet?id={0}" DataTextField="Title" ItemStyle-CssClass="col-lg-8">
                            <ItemStyle VerticalAlign="Top" />
                        </asp:HyperLinkField>
                        <asp:BoundField DataField="Quantity" HeaderText="Số lượng" HeaderStyle-CssClass="text-center">
                            <HeaderStyle CssClass="text-center" Font-Bold="True" Font-Names="Arial" VerticalAlign="Top"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:CommandField CancelText="Hủy" EditText="Sửa" ShowEditButton="True" UpdateText="Cập nhật ">
                            <ItemStyle Font-Underline="False" HorizontalAlign="Center" Font-Bold="False" ForeColor="#FF5050" VerticalAlign="Middle" />
                        </asp:CommandField>
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkDelete" runat="server" CausesValidation="False" CommandName="Delete" Text="Xóa" OnClientClick="return confirm(&quot;Xóa khỏi giỏ tài liệu của bạn?&quot;)"><span class="glyphicon glyphicon-trash" aria-hidden="true"></span></asp:LinkButton>

                            </ItemTemplate>
                            <ItemStyle Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                    </Columns>
                    <EditRowStyle BackColor="White" />
                    <EmptyDataTemplate>
                    </EmptyDataTemplate>
                    <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <PagerTemplate>
                    </PagerTemplate>
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" Height="35px" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                </asp:GridView>
            </div>
            <div class="col-lg-4 ">
            </div>
        </div>
        <div class="row">
            <div class="col-md-2 col-lg-offset-2">
                <button id="prev" runat="server" type="submit" onclick="javascript:history.back(); return false;" class="btn  btn-default" style="margin-top: 10px; margin-bottom: 15px" aria-hidden="True">&lt;&lt; Chọn thêm sách</button>
            </div>
        </div>
        
        <div class="col-md-2">
        </div>
        <div id="div_select" class="row" runat="server" visible="false">
            <br />


            <div class="col-md-8 col-lg-offset-2">
                <p>
                    <asp:RadioButton ID="Radio_hutech" runat="server" GroupName="hutech" Text="Bạn là sinh viên HUTECH?" OnCheckedChanged="Radio_hutech_CheckedChanged" AutoPostBack="True" Visible="False" />
                </p>
                <asp:RadioButton ID="Radio_nohutech" runat="server" GroupName="hutech" Text="Bạn là khách vãn lai?" AutoPostBack="True" OnCheckedChanged="Radio_nohutech_CheckedChanged" Visible="False" />
            </div>
        </div>
        <!-- Thong tin dang ky cho khách vãn lai-->
        <div id="DivReInfo_Nohutech" runat="server" class="col-lg-8 col-lg-offset-2" visible="false">
            <br />
            <div class="messages"></div>
            <div class="controls">
                <div class="row">
                    <div class="col-md-6">
                    </div>
                    <div class="col-md-4">
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <div class="form-group">
                            <label for="form_lastname">Họ *</label>
                            <input id="form_lastname" runat="server" type="text" name="name" class="form-control" required="required" data-error="Vui lòng nhập Họ." />

                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label for="form_name">Tên *</label>
                            <input id="form_name" runat="server" type="text" name="surname" class="form-control" placeholder="" required="required" data-error="Vui lòng nhập Tên." />
                            <div class="help-block with-errors"></div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <div class="form-group">
                            <label for="form_email">Email *</label>
                            <input id="form_email" runat="server" type="email" name="email" class="form-control" placeholder="" required="required" data-error="Email không được để trống." />
                            <div class="help-block with-errors"></div>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label for="form_phone">Điện thoại *</label>
                            <input id="form_phone" runat="server" type="tel" name="phone" class="form-control" required="required" data-error="Vui lòng nhập SĐT." />
                            <div class="help-block with-errors"></div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <div class="form-group">
                            <label for="form_Address">Địa chỉ liên hệ *</label>
                            <input id="form_Address" runat="server" type="text" name="address" class="form-control" required="required" data-error="Vui lòng nhập địa chỉ." />
                            <div class="help-block with-errors"></div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <div class="form-group">
                            <label for="form_message">Ghi chú thêm</label>
                            <textarea id="form_message" runat="server" name="message" class="form-control" placeholder="" rows="4"></textarea>
                            <div class="help-block with-errors"></div>
                        </div>
                    </div>

                    <div class="col-md-12">
                        <div class="form-group">
                            <div class="form-group">
                                <!-- Replace data-sitekey with your own one, generated at https://www.google.com/recaptcha/admin -->
                                <!--<div class="g-recaptcha" data-sitekey="6LfrmwwTAAAAAASMjPZlf412kX2NUNIcQqRTOyNX"></div>-->
                                <div class="help-block with-errors"></div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-12">
                        <asp:Button ID="Bt_muon" runat="server" Text="Đặt mượn" OnClick="Bt_muon_Click" class="btn btn-danger" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <p class="text-muted"><strong>(*)</strong>Các trường này bắt buộc</p>
                    </div>
                </div>
            </div>

        </div>


        <!-- Thong tin dang ky cho sinh vien hutech-->
        <div id="Div_hutech" runat="server" class="col-lg-8 col-lg-offset-2" visible="false">
            <br />
            <div class="messages"></div>
            <div class="controls">
                <div class="row">
                    <div class="col-md-6">
                        <div class="form-group">
                            <label for="form_select_sv">Lựa chọn hình thức mượn *</label>
                            <asp:DropDownList ID="DropDown_sv" runat="server" class="form-control">
                                <asp:ListItem Value="0">Mượn từ Thư viện ĐBP đến Thư viện Q9</asp:ListItem>
                                <asp:ListItem Value="0">Mượn từ Thư viện Q9 đến Thư viện ĐBP</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <div class="form-group">
                            <label for="form_mssv">Mã số sinh viên*</label>
                            <input id="form_mssv" runat="server" type="text" name="name" class="form-control" required="required" data-error="Vui lòng nhập MSSV." />
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6" style="left: 2px; top: -1px">
                        <div class="form-group">
                            <label for="form_lastname">Họ *</label>
                            <input id="form_lastname_sv" runat="server" type="text" name="name" class="form-control" required="required" data-error="Vui lòng nhập Họ." />

                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label for="form_name">Tên *</label>
                            <input id="form_name_sv" runat="server" type="text" name="surname" class="form-control" placeholder="" required="required" data-error="Vui lòng nhập Tên." />
                            <div class="help-block with-errors"></div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <div class="form-group">
                            <label for="form_email">Email *</label>
                            <input id="form_email_sv" runat="server" type="email" name="email" class="form-control" placeholder="" required="required" data-error="Email không được để trống." />
                            <div class="help-block with-errors"></div>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label for="form_phone">Điện thoại *</label>
                            <input id="form_phone_sv" runat="server" type="tel" name="phone" class="form-control" required="required" placeholder="" data-error="Vui lòng nhập SĐT." />
                            <div class="help-block with-errors"></div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <div class="form-group">
                            <label for="form_Address">Địa chỉ liên hệ *</label>
                            <input id="form_Address_sv" runat="server" type="text" name="address" class="form-control" required="required" placeholder="" data-error="Vui lòng nhập địa chỉ." />
                            <div class="help-block with-errors"></div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <div class="form-group">
                            <label for="form_message">Ghi chú thêm</label>
                            <textarea id="form_message_sv" runat="server" name="message" class="form-control" placeholder="" rows="4"></textarea>
                            <div class="help-block with-errors"></div>
                        </div>
                    </div>

                    <div class="col-md-12">
                        <div class="form-group">
                            <div class="form-group">
                                <!-- Replace data-sitekey with your own one, generated at https://www.google.com/recaptcha/admin -->
                                <!--<div class="g-recaptcha" data-sitekey="6LfrmwwTAAAAAASMjPZlf412kX2NUNIcQqRTOyNX"></div>-->
                                <div class="help-block with-errors"></div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-12">
                        <asp:Button ID="Button1" runat="server" Text="Đặt mượn" OnClick="Bt_muon_Click" class="btn btn-danger" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <p class="text-muted"><strong>(*)</strong>Các trường này bắt buộc</p>
                    </div>
                </div>
            </div>

        </div>
    </div>

</asp:Content>
