<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Majors.aspx.cs" Inherits="Majors" MasterPageFile="~/HomeLib.master" %>
<%@ Register TagPrefix="MajorsControl" Src="~/Book/MajorsList.ascx" TagName="TagMajors" %>
   

<asp:Content ID="majors" ContentPlaceHolderID="ContentPlaceHolderHomeLib" runat="server">
    <link href="../Content/Majors.css" rel="stylesheet" />
   <script src="https://code.jquery.com/jquery-1.11.2.min.js"></script>
    
    <script type="text/javascript">
        $(document).ready(function () {
            $('a[data-toggle="tab"]').on('show.bs.tab', function (e) {
                localStorage.setItem('activeTab', $(e.target).attr('href'));
            });
            var activeTab = localStorage.getItem('activeTab');
            if (activeTab) {
                $('#my-tab a[href="' + activeTab + '"]').tab('show');
            }
        });
    </script>   

    <div class="container-fluid" id="tab-s">       
        <div class="row">
            <div class="col-md-12 ">
                <div>
                    <h4 class="title text-left p-2">TÀI LIỆU THEO NGÀNH</h4>
                </div>
                <nav>
                    <div class="nav nav-tab-s nav-fill" id="my-tab" role="tablist">
                        <a class="nav-item nav-link active" id="nav-dh-tab" data-toggle="tab" href="#nav-dh" role="tab" aria-controls="nav-dh" aria-selected="true">Đại học</a>
                        <a class="nav-item nav-link " id="nav-sdh-tab" data-toggle="tab" href="#nav-sdh" role="tab" aria-controls="nav-sdh" aria-selected="false">Sau đại học</a>
                    </div>
                </nav>
                <div class="col-md-12 col-sm-12 ">
                    <div class="tab-content py-3  px-sm-0" id="nav-tabContent">
                        <div class="tab-pane fade show active" id="nav-dh" role="tabpanel" aria-labelledby="nav-dh-tab">
                            <div class="d-none d-md-block">
                                <asp:DataList ID="DataList_DH" runat="server" RepeatColumns="3" HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <div class="col-md-12 pl-3 pr-3" id="divhover">
                                            <a href='<%# String.Format("nganh-hoc-mon-hoc?id={0}&title={1}&number={2}",Eval("OlogyID"),Eval("OlogyName") ,Eval("sl")) %>#mh' class="text-white js-scroll-trigger " onclick="BoldText(this);"><i class="fa fa-caret-right"></i>&nbsp;<%# Eval("OlogyName") %></a></div>
                                    </ItemTemplate>
                                </asp:DataList>
                            </div>
                            <div class="d-block d-md-none ">
                                <div>
                                    <strong>Ngành học</strong>
                                    <asp:DropDownList ID="DropDown_DH" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="DropDown_DH_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                                <div class="mt-2" id="MH_DH" runat="server">
                                    <strong>
                                        <asp:Label ID="Lab_monhoc_DH" runat="server"></asp:Label></strong>                                   
                                        <asp:DropDownList ID="DropDown_DH_OlogyCurriculums" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="DropDown_DH_OlogyCurriculums_SelectedIndexChanged"></asp:DropDownList>
                                     
                                </div>
                            </div>
                        </div>
                        <div class="tab-pane fade " id="nav-sdh" role="tabpanel" aria-labelledby="nav-sdh-tab">
                            <div class="d-none d-md-block">
                                <asp:DataList ID="DataList_SDH" runat="server" RepeatColumns="3" HorizontalAlign="Center" Font-Names="Arial">
                                    <ItemTemplate>
                                        <div class="col-md-12 pl-3 pr-3 " id="divhover">
                                            <a href='<%# String.Format("nganh-hoc-mon-hoc?id={0}&title={1}&number={2}",Eval("OlogyID"),Eval("OlogyName").ToString() ,Eval("sl")) %>#mh' class="text-white js-scroll-trigger" onclick="BoldText(this);"><i class="fa fa-caret-right"></i>&nbsp;<%# Eval("OlogyName").ToString() %></a></div>
                                    </ItemTemplate>
                                </asp:DataList>
                            </div>
                            <div class="d-block d-md-none ">
                                <div>
                                    <strong>Ngành học</strong>
                                    <asp:DropDownList ID="DropDown_SDH" runat="server" CssClass="form-control" OnSelectedIndexChanged="DropDown_SDH_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                </div>
                                <div class="mt-2" id="MH_SDH" runat="server">
                                    <strong>
                                        <asp:Label ID="Lab_monhoc_SDH" runat="server"></asp:Label></strong>
                                    <asp:DropDownList ID="DropDown_SDH_OlogyCurriculums" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="DropDown_SDH_OlogyCurriculums_SelectedIndexChanged"></asp:DropDownList>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="container-fluid">
        <section id="mh" ">
            <div class="row ">
                <div class="p-3 mb-2 text-center text-white col-md-12 d-none d-md-block" style="background-color: #4A1878">
                    <h4>
                        <asp:Label ID="Lab_SDH" runat="server"></asp:Label></h4>
                </div>
            </div>
        </section>        
        <div class="row">           
            <div class="col-md-3 d-none d-md-block"> 
                <asp:DataList ID="DataList_Curriculums" runat="server"  CellPadding="4" OnItemDataBound="DataList_Curriculums_ItemDataBound" Width="100%" Font-Names="Arial" ForeColor="#333333">
                    <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                    <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <ItemStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <ItemTemplate>
                        <div class="col-md-12">
                            <ul class="list-group">
                                <li>
                                    <asp:HyperLink ID="HyperCurriculumID" CssClass=" text-black js-scroll-trigger" onclick="BoldText(this);" Text='<%# ToUpperFirstLetter(Eval("CurriculumName").ToString().ToLower()) %>' runat="server" ToolTip='<%# Eval("CurriculumName")%>'>HyperLink</asp:HyperLink>
                                </li>
                            </ul>
                        </div>
                    </ItemTemplate>
                    <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True"  ForeColor="#333333" />
                </asp:DataList>
            </div>
            <div class="col-md-9 col-sm-12">                
                <MajorsControl:TagMajors ID="MJ" runat="server"  />
            </div>
        </div>
    </div>
</asp:Content>
