<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Doan.aspx.cs" Inherits="Majors" MasterPageFile="~/HomeLib.master" %>


 <asp:Content ID="majors" ContentPlaceHolderID="ContentPlaceHolderHomeLib" runat="server">
    <style>
       a {
          text-decoration: none;          
          font-size: 1em;        
          color:#0a2bac
        }
       a:hover
       {
           text-decoration:underline
       }
    </style>

    <div id="tab-s" style="margin-top: 120px;font-weight:600">
        <div class="container">
            <div class="row align-content-center">
                <div class="col-md-12 ">
                    <div>
                        <h4 class="title h4 text-left p-2">ĐỒ ÁN TỐT NGHIỆP CÁC NGÀNH</h4>
                    </div>
                    <table class="mb-4 table table-striped table-hover ">
                        <tbody>                         
                            <tr >
                                <td>
                                     <a href="/fts?q=grid:2%20mj:%22Cơ%20Tin%20học%20Kỹ%20thuật%22&amp;Field=&amp;start=0&amp;ck=false&amp;file=" target="_blank">Cơ Tin học Kỹ thuật</a>
                                </td>
                            </tr>                           
                            <tr>
                                <td>
                                     <a href="/fts?q=grid:2%20mj:%22C&ocirc;ng%20nghệ%20sinh%20học%22&amp;Field=&amp;start=0&amp;ck=false&amp;file=" target="_blank">C&ocirc;ng nghệ sinh học</a>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                     <a href="/fts?q=grid:2%20mj:%22C&ocirc;ng%20nghệ%20th&ocirc;ng%20tin%22&amp;Field=&amp;start=0&amp;ck=false&amp;file=" target="_blank">C&ocirc;ng nghệ th&ocirc;ng tin</a>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                     <a href="/fts?q=grid:2%20mj:%22C&ocirc;ng%20nghệ%20thực%20phẩm%22&amp;Field=&amp;start=0&amp;ck=false&amp;file=" target="_blank">C&ocirc;ng nghệ thực phẩm</a>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                     <a href="/fts?q=grid:2%20mj:%22Điện%20c&ocirc;ng%20nghiệp%22&amp;Field=&amp;start=0&amp;ck=false&amp;file=" target="_blank">Điện c&ocirc;ng nghiệp</a>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                     <a href="/fts?q=grid:2%20mj:%22Điện%20tử%20viễn%20th&ocirc;ng%22&amp;Field=&amp;start=0&amp;ck=false&amp;file=" target="_blank">Điện tử viễn th&ocirc;ng</a>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                     <a href="/fts?q=grid:2%20mj:%22Kế%20to&aacute;n%22&amp;Field=&amp;start=0&amp;ck=false&amp;file=" target="_blank">Kế to&aacute;n</a>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                     <a href="/fts?q=grid:2%20mj:%22Kế%20to&aacute;n%20-%20Kiểm%20to&aacute;n%22&amp;Field=&amp;start=0&amp;ck=false&amp;file=" target="_blank">Kế to&aacute;n - Kiểm to&aacute;n</a>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                     <a href="/fts?q=grid:2%20mj:%22Kinh%20tế%20x&acirc;y%20dựng%22&amp;Field=&amp;start=0&amp;ck=false&amp;file=" target="_blank">Kinh tế x&acirc;y dựng</a>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                     <a href="/fts?q=grid:2%20mj:%22Kỹ%20thuật%20cơ%20điện%20tử%22&amp;Field=&amp;start=0&amp;ck=false&amp;file=" target="_blank">Kỹ thuật cơ điện tử</a>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                     <a href="/fts?q=grid:2%20mj:%22Kỹ%20thuật%20cơ%20kh&iacute;%22&amp;Field=&amp;start=0&amp;ck=false&amp;file=" target="_blank">Kỹ thuật cơ kh&iacute;</a>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                     <a href="/fts?q=grid:2%20mj:%22Kỹ%20thuật%20c&ocirc;ng%20tr&igrave;nh%20x&acirc;y%20dựng%22&amp;Field=&amp;start=0&amp;ck=false&amp;file=" target="_blank">Kỹ thuật c&ocirc;ng tr&igrave;nh x&acirc;y dựng</a>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                     <a href="/fts?q=grid:2%20mj:%22Kỹ%20thuật%20điện%22&amp;Field=&amp;start=0&amp;ck=false&amp;file=" target="_blank">Kỹ thuật điện</a>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                     <a href="/fts?q=grid:2%20mj:%22Kỹ%20thuật%20điện%20tử%20truyền%20th&ocirc;ng%22&amp;Field=&amp;start=0&amp;ck=false&amp;file=" target="_blank">Kỹ thuật điện tử truyền th&ocirc;ng</a>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                     <a href="/fts?q=grid:2%20mj:%22Kỹ%20thuật%20điện,%20điện%20tử%22&amp;Field=&amp;start=0&amp;ck=false&amp;file=" target="_blank">Kỹ thuật điện, điện tử</a>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                     <a href="/fts?q=grid:2%20mj:%22Kỹ%20thuật%20điều%20khiển%20v&agrave;%20tự%20động%20h&oacute;a%22&amp;Field=&amp;start=0&amp;ck=false&amp;file=" target="_blank">Kỹ thuật điều khiển v&agrave; tự động h&oacute;a</a>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                     <a href="/fts?q=grid:2%20mj:%22Kỹ%20thuật%20m&ocirc;i%20trường%22&amp;Field=&amp;start=0&amp;ck=false&amp;file=" target="_blank">Kỹ thuật m&ocirc;i trường</a>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                     <a href="/fts?q=grid:2%20mj:%22Kỹ%20thuật%20x&acirc;y%20dựng%22&amp;Field=&amp;start=0&amp;ck=false&amp;file=" target="_blank">Kỹ thuật x&acirc;y dựng</a>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                     <a href="/fts?q=grid:2%20mj:%22Kỹ%20thuật%20x&acirc;y%20dựng%20c&ocirc;ng%20tr&igrave;nh%20giao%20th&ocirc;ng%22&amp;Field=&amp;start=0&amp;ck=false&amp;file=" target="_blank">Kỹ thuật x&acirc;y dựng c&ocirc;ng tr&igrave;nh giao th&ocirc;ng</a>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                     <a href="/fts?q=grid:2%20mj:%22Kỹ%20thuật%20x&acirc;y%20dựng&nbsp;c&ocirc;ng%20tr&igrave;nh%20d&acirc;n%20dụng%20v&agrave;%20c&ocirc;ng%20nghiệp%22&amp;Field=&amp;start=0&amp;ck=false&amp;file=" target="_blank">Kỹ thuật x&acirc;y dựng&nbsp;c&ocirc;ng tr&igrave;nh d&acirc;n dụng v&agrave; c&ocirc;ng nghiệp</a>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                     <a href="/fts?q=grid:2%20mj:%22Luật%20kinh%20tế%22&amp;Field=&amp;start=0&amp;ck=false&amp;file=" target="_blank">Luật kinh tế</a>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                     <a href="/fts?q=grid:2%20mj:%22Marketing%22&amp;Field=&amp;start=0&amp;ck=false&amp;file=" target="_blank">Marketing</a>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                     <a href="/fts?q=grid:2%20mj:%22Ng&ocirc;n%20ngữ%20Anh%22&amp;Field=&amp;start=0&amp;ck=false&amp;file=" target="_blank">Ng&ocirc;n ngữ Anh</a>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                     <a href="/fts?q=grid:2%20mj:%22Quản%20trị%20dịch%20vụ%20du%20lịch%20v&agrave;%20lữ%20h&agrave;nh%22&amp;Field=&amp;start=0&amp;ck=false&amp;file=" target="_blank">Quản trị dịch vụ du lịch v&agrave; lữ h&agrave;nh</a>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                     <a href="/fts?q=grid:2%20mj:%22Quản%20trị%20doanh%20nghiệp%22&amp;Field=&amp;start=0&amp;ck=false&amp;file=" target="_blank">Quản trị doanh nghiệp</a>
                                </td>
                            </tr>                           
                            <tr>
                                <td>
                                     <a href="/fts?q=grid:2%20mj:%22Quản%20trị%20kh&aacute;ch%20sạn%22&amp;Field=&amp;start=0&amp;ck=false&amp;file=" target="_blank">Quản trị kh&aacute;ch sạn</a>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                     <a href="/fts?q=grid:2%20mj:%22Quản%20trị%20kinh%20doanh%22&amp;Field=&amp;start=0&amp;ck=false&amp;file=" target="_blank">Quản trị kinh doanh</a>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                     <a href="/fts?q=grid:2%20mj:%22Quản%20trị%20marketing%22&amp;Field=&amp;start=0&amp;ck=false&amp;file=" target="_blank">Quản trị marketing</a>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                     <a href="/fts?q=grid:2%20mj:%22Quản%20trị%20ngoại%20thương%22&amp;Field=&amp;start=0&amp;ck=false&amp;file=" target="_blank">Quản trị ngoại thương</a>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                     <a href="/fts?q=grid:2%20mj:%22Quản%20trị%20t&agrave;i%20ch&iacute;nh%20v&agrave;%20đầu%20tư%20chứng%20kho&aacute;n%22&amp;Field=&amp;start=0&amp;ck=false&amp;file=" target="_blank">Quản trị t&agrave;i ch&iacute;nh v&agrave; đầu tư chứng kho&aacute;n</a>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                     <a href="/fts?q=grid:2%20mj:%22T&agrave;i%20ch&iacute;nh%20-%20Ng&acirc;n%20h&agrave;ng%22&amp;Field=&amp;start=0&amp;ck=false&amp;file=" target="_blank">T&agrave;i ch&iacute;nh - Ng&acirc;n h&agrave;ng</a>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                     <a href="/fts?q=grid:2%20mj:%22Thiết%20kế%20đồ%20họa%22&amp;Field=&amp;start=0&amp;ck=false&amp;file=" target="_blank">Thiết kế đồ họa</a>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                     <a href="/fts?q=grid:2%20mj:%22Thiết%20kế%20nội%20thất%22&amp;Field=&amp;start=0&amp;ck=false&amp;file=" target="_blank">Thiết kế nội thất</a>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                     <a href="/fts?q=grid:2%20mj:%22Thiết%20kế%20thời%20trang%22&amp;Field=&amp;start=0&amp;ck=false&amp;file=" target="_blank">Thiết kế thời trang</a>
                                </td>
                            </tr>                            
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
