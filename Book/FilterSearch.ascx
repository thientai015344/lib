<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FilterSearch.ascx.cs" Inherits="Book_MenuSearchContain"  %>

<script>
    var currElem = null; //will hold the element that is bold now

    function BoldText(elem) {

        if (elem != currElem) { //do thing if you're clicking on a bold link

            if (currElem) //if a link bold right now unbold it

                currElem.style.fontWeight = 'normal';

            currElem = elem; //assign this element as the current one

            elem.style.fontWeight = 'bold';  //make the element clicked bold

        }

    }
</script>

<div class="container-fluid" style="font-size:0.9em">
    <div class="row">
        <div class="col-12">
            <h6><b>Tác giả</b></h6>
            <asp:DataList ID="DataList_Author" runat="server" Width="100%" OnItemDataBound="DataList_Author_ItemDataBound">
                <ItemTemplate>
                    <div class="ml-2">
                        <asp:HyperLink ID="Link_au" ForeColor="#0f1111" runat="server"><%# Eval("Author_gr") %></asp:HyperLink>
                        <label id="au_count" class="form-check-label text-black-80">(<%# Eval("Author_count")%>)</label>
                    </div>
                </ItemTemplate>
            </asp:DataList>
        </div>
    </div>
    <div class="row">
        <div class="col-12 ">
            <h6><b>Nhà xuất bản</b></h6>
            <asp:DataList ID="DataList_Publisher" runat="server" Width="100%" OnItemDataBound="DataList_Publisher_ItemDataBound">
                <ItemTemplate>
                    <div class="ml-2">
                        <asp:HyperLink ID="Link_Publisher" ForeColor="#0f1111" runat="server"><%# Eval("Publisher_gr") %></asp:HyperLink>
                        <label class="form-check-label text-black-80">(<%# Eval("Publisher_count")%>)</label>
                    </div>
                </ItemTemplate>
            </asp:DataList>
        </div>
    </div>
    <div class="row">
        <div class="col-12">
            <h6><b>Bộ sưu tập</b></h6>
            <asp:DataList ID="DataList_RecordGroupName" runat="server" Width="100%" OnItemDataBound="DataList_RecordGroupName_ItemDataBound">
                <ItemTemplate>
                    <div class="ml-2">
                        <asp:HyperLink ID="Link_GroupName" ForeColor="#0f1111" runat="server"><%# Eval("RecordGroupName_gr") %></asp:HyperLink>
                        <label class="form-check-label text-black-80">(<%# Eval("RecordGroupName_count")%>)</label>
                    </div>
                </ItemTemplate>
            </asp:DataList>
        </div>
    </div>
    <div class="row">
        <div class="col-12">
            <h6><b>Định dạng</b></h6>
            <asp:DataList ID="DataList_file" runat="server" Width="100%" OnItemDataBound="DataList_file_ItemDataBound">

                <ItemTemplate>
                    <div class="ml-2">
                        <asp:HyperLink ID="Link_File" ForeColor="#0f1111" runat="server"><%# Eval("Filename_gr") %></asp:HyperLink>
                        <label class="form-check-label text-black-80">(<%# Eval("Filename_count")%>)</label>
                    </div>
                </ItemTemplate>
            </asp:DataList>
        </div>
    </div>
</div>
