<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="HomeLib_Home" MasterPageFile="HomeLib.master" %>

<%@ Register TagPrefix="SearchBoxControl" Src="~/Book/SearchBox.ascx" TagName="TagSearchBox" %>
<%@ Register Src="~/Book/FastMenu.ascx" TagPrefix="SearchBoxControl" TagName="FastMenu" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="ContentPlaceHolderHomeLib" runat="server">
    <link rel="stylesheet" href="HomeLib/css/style2.css?a=2" />
    <div class="container">
        <div class="row">
            <div class="owl-carousel slide-one-item ">
                <a href="#">
                    <img src="ImageSlideShow/BANNER 1.jpg?a=2" alt="Image" class="img-fluid" /></a>
                <a href="#">
                    <img src="ImageSlideShow/BANNER 2.jpg?a=2" alt="Image" class="img-fluid" /></a>
                <a href="#">
                    <img src="ImageSlideShow/BANNER 3.jpg?a=2" alt="Image" class="img-fluid" /></a>
                <a href="#">
                    <img src="ImageSlideShow/BANNER 4.jpg?a=2" alt="Image" class="img-fluid" /></a>
            </div>
        </div>
        <div class="row align-items-center block-4" style="background:#546D8C">
            <div class="col-lg-6 col-md-6 mb-lg-2">             
                <h5 class=" font-weight-bold text-white mt-2">HUTECH BOOKS</h5>             
                <span class="text-white " style="font-size: 1rem">Tìm kiếm Sách in, Giáo trình, Đề tài, NCKH, Tài liệu điện tử</span>
            </div>
            <div class="col-lg-6 col-md-6 mb-2">
                <SearchBoxControl:TagSearchBox ID="SB2" runat="server" />
            </div>
        </div>
        <div class="row justify-content-center mt-4 ">
            <h2 class="section-title-underline mb-3">
                <span>Ngày Sách và Văn hóa đọc</span>
            </h2>
            <a href="/Ngay-sach-va-hoa-doc" class="fancybox" rel="ligthbox">
                <img src="ImageSlideShow/ImageTemp/sach.jpg" class="img-thumbnail" />
            </a>
        </div>
    </div>

    <div class="site-section">
        <div class="container">
            <div class="row justify-content-center text-center">
                <div class="col-lg-4 mb-5">
                    <h2 class="section-title-underline">
                        <span>Truy cập nhanh danh mục</span>
                    </h2>
                </div>
            </div>
            <div class="row align-items-center text-black">
                <SearchBoxControl:FastMenu runat="server" ID="FastMenu" />
            </div>
        </div>
    </div>

    <div class="site-section">
        <div class="container">
            <div class="row text-center">
                <div class="col-lg-12">
                    <h2 class="section-title-underline ">
                        <span>Xem - Đọc</span>
                    </h2>
                </div>
            </div>
            <div class="row">
                <div class="col-12">
                    <div class="owl-slide-3 owl-carousel">
                        <div class="course-1-item">
                            <figure class="thumnail">
                                <a href="/co-so-du-lieu">
                                    <img src="ImageSlideShow/DOCTHEM-01.jpg" alt="Image" class="img-fluid"></a>
                                <div class="category">
                                    <h3>Cơ sở dữ liệu trực tuyến</h3>
                                </div>
                            </figure>
                            <div class="course-1-content pb-4">
                                <h2>Bao gồm hầu hết các lĩnh vực</h2>
                                <p><a href="/co-so-du-lieu" class="btn btn-primary rounded-0 px-4">Xem thêm</a></p>
                            </div>
                        </div>

                        <div class="course-1-item">
                            <figure class="thumnail">
                                <a href="/link-tai-app">
                                    <img src="ImageSlideShow/DOCTHEM-02.jpg" alt="Image" class="img-fluid"></a>
                                <div class="category">
                                    <h3>Dùng App LIBHUTECH tải tài liệu</h3>
                                </div>
                            </figure>
                            <div class="course-1-content pb-4">
                                <h2>App Thư viện HUTECH</h2>
                                <p><a href="/link-tai-app" class="btn btn-primary rounded-0 px-4">Xem thêm</a></p>
                            </div>
                        </div>

                        <div class="course-1-item">
                            <figure class="thumnail">
                                <a href="https://www.youtube.com/watch?v=rgxLqwyYDgk">
                                    <img src="ImageSlideShow/DOCTHEM-03.jpg" alt="Image" class="img-fluid"></a>
                                <div class="category">
                                    <h3>Dùng smartphone mượn/trả sách</h3>
                                </div>
                            </figure>
                            <div class="course-1-content pb-4">
                                <h2>Hướng dẫn mượn sách thông qua ứng dụng LIBHUTECH</h2>
                                <p><a href="https://www.youtube.com/watch?v=rgxLqwyYDgk" class="btn btn-primary rounded-0 px-4">Xem thêm</a></p>
                            </div>
                        </div>

                        <div class="course-1-item">
                            <figure class="thumnail">
                                <a href="http://www.stinet.gov.vn/">
                                    <img src="ImageSlideShow/DOCTHEM-04.jpg" alt="Image" class="img-fluid"></a>
                                <div class="category">
                                    <h3>Thư viện liên kết</h3>
                                </div>
                            </figure>
                            <div class="course-1-content pb-4">
                                <h2>Mạng liên kết nguồn lực thông tin khoa học và công nghệ Tp.HCM</h2>
                                <p><a href="http://www.stinet.gov.vn/" class="btn btn-primary rounded-0 px-4">Xem thêm</a></p>
                            </div>
                        </div>
                    </div>

                </div>
            </div>

        </div>
    </div>

</asp:Content>
