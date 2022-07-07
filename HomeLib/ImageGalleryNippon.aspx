<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ImageGalleryNippon.aspx.cs" Inherits="HomeLib_ImageGallery" %>


<!DOCTYPE html>
<link href="../Content/ImageGallery.css" rel="stylesheet" />
<link href="//maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css" rel="stylesheet" id="bootstrap-css" />
<script src="//maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js"></script>
<script src="//cdnjs.cloudflare.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
<!------ Include the above in your HEAD tag ---------->
<link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/fancybox/2.1.5/jquery.fancybox.min.css" media="screen" />
<script src="//cdnjs.cloudflare.com/ajax/libs/fancybox/2.1.5/jquery.fancybox.min.js"></script>

<script>
    $(document).ready(function () {
        $(".fancybox").fancybox({
            openEffect: "none",
            closeEffect: "none"
        });

        $(".zoom").hover(function () {

            $(this).addClass('transition');
        }, function () {

            $(this).removeClass('transition');
        });
    });
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <title>Thư viện Đại học Công nghệ Tp.HCM</title>
    <meta charset="utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no"/>
    <link rel="icon" href="logo/hutech.png" type="image/png"/>
    <meta property="fb:app_id" content="1603828266537773" />
    <!-- Chrome, Firefox OS and Opera -->
    <meta name="theme-color" content="#0063CD"/>

</head>

<body>
    <form id="form1" runat="server">
        <div class="custom-breadcrumns border-bottom  mt-5 text-white">
                <div class="container text-white">
                    <a class=" text-white" href="/">Trang chủ</a>
                    <span class="icon-keyboard_arrow_right"></span>
                    <span class="current">> Books for Understanding Japan </span>
                    
                </div>
            
            </div>
        <!-- Page Content -->
        <div class="container page-top">
            <div>
                  <h2 class="mb-2">Books for Understanding Japan</h2>
            </div>
            <div class="text-white" style="font-family:Arial; font-size:1em">
                <p>The Nippon Foundation is honered to endow the HUTECH Library, Ho Chi Minh City University of Technology with a selection of "Books for Understanding Japan" throught the Japan Science Society as a token of the friendship between Viet Nam and Japan.</p>
                <p class="font-italic text-justify">(Tạm dịch: Quỹ Nippon hân hạnh trao tặng Thư viện HUTECH (thuộc trường Đại học Công nghệ TP.HCM) tuyển chọn "Sách hiểu về Nhật Bản" thông qua Hội Khoa học Xã hội Nhật Bản. Đây là một minh chứng của tình hữu nghị giữa Việt Nam và Nhật Bản)</p>
                <p>Trên đây là chấp bút của ngài Yohei Sasakawa, chủ tịch Quỹ Nippon gửi đến thư viện HUTECH trong dự án tặng sách về Nhật Bản của Quỹ Nippon.</p>
                <p>Quỹ Nippon là đơn vị hỗ trợ cho dự án “READ JAPAN PROJECT” của Hội Khoa học Xã hội Nhật Bản. Hội Khoa học Xã hội Nhật Bản là một tổ chức phi lợi nhuận tại nước Nhật.  Nhiệm vụ của dự án “READ JAPAN PROJECT” nhằm để thúc đẩy sự hiểu biết về Nhật Bản trên toàn thế giới thông qua sách. Mục tiêu của dự án là sử dụng những cuốn sách tiêu biểu để cung cấp cho độc giả trên khắp thế giới một bức tranh chính xác về Nhật Bản. Đặc biệt, quỹ hy vọng có thể tiếp cận với các nhà nghiên cứu trẻ quan tâm đến Nhật Bản, cũng như các nhà lãnh đạo có quan điểm và trí thức chuyên về các lĩnh vực khác nghiên cứu thêm về Nhật Bản.</p>
                <p>Lần này, Quỹ Nippon chọn trường Đại học Công nghệ TPHCM (HUTECH) để tặng sách. Với số lượng sách 155 quyển sách tiếng Anh giới thiệu về Nhật Bản, đây là nguồn tài liệu quý và thiết thực để phục vụ cho giảng viên và sinh viên trường. </p>
                <p>Thư viện Trường Đại học Công nghệ TPHCM (HUTECH) trân trọng cảm ơn sự quan tâm và hỗ trợ từ dự án "READ JAPAN PROJECT" và quỹ Nippon.</p>
                <p>Hiện tại, 155 cuốn sách của dự án tặng và các sách khác về Nhật bản (thư viện đã có trước đó) cùng được trưng bày tại thư viện cơ sở Điện Biên Phủ và được xếp theo các chủ đề như:</p>
                <p>1.	Đồ gốm </p>
                <p>2.	Nghệ thuật điêu khắc </p>
                <p>3.	Vật dụng nghi lễ Phật giáo</p>
                <p>4.	Việt Nam – Nhật Bản</p>
                <p>5.	Sưu tập hiện vật trao đổi văn hóa</p>
                <p>6.	Ngôn ngữ Nhật</p>                
                <p>Tuy nhiên đang trong thời gian phục vụ online do tình hình dịch Covid, trước mắt kính mời bạn đọc xem hình ảnh trưng bày những quyển sách này qua hình chụp google 3D.</p>
                <p>Hẹn bạn đọc khi thư viện phục vụ trực tiếp, các bạn sẽ nhanh chóng được tiếp cận kịp thời số sách hữu ích này nhé.</p>
            </div><br />
            <div class="row">              
                <div class="col-lg-3 col-md-4 col-xs-6 thumb">
                    <a href="../ImageSlideShow/ImageTemp/image001.jpg?auto=compress&cs=tinysrgb&h=650&w=940" class="fancybox" rel="ligthbox">
                        <img src="../ImageSlideShow/ImageTemp/image001.jpg?auto=compress&cs=tinysrgb&h=650&w=940" class="zoom img-fluid " alt="">
                    </a>
                </div>
                <div class="col-lg-3 col-md-4 col-xs-6 thumb">
                    <a href="../ImageSlideShow/ImageTemp/image002.jpg?auto=compress&cs=tinysrgb&h=650&w=940" class="fancybox" rel="ligthbox">
                        <img src="../ImageSlideShow/ImageTemp/image002.jpg?auto=compress&cs=tinysrgb&h=650&w=940" class="zoom img-fluid" alt="">
                    </a>
                </div>

                <div class="col-lg-3 col-md-4 col-xs-6 thumb">
                    <a href="../ImageSlideShow/ImageTemp/image004.jpg?auto=compress&cs=tinysrgb&dpr=2&h=650&w=940" class="fancybox" rel="ligthbox">
                        <img src="../ImageSlideShow/ImageTemp/image004.jpg?auto=compress&cs=tinysrgb&dpr=2&h=650&w=940" class="zoom img-fluid " alt="">
                    </a>
                </div>

                <div class="col-lg-3 col-md-4 col-xs-6 thumb">
                    <a href="../ImageSlideShow/ImageTemp/image005.jpg?auto=compress&cs=tinysrgb&dpr=2&h=650&w=940" class="fancybox" rel="ligthbox">
                        <img src="../ImageSlideShow/ImageTemp/image005.jpg?auto=compress&cs=tinysrgb&dpr=2&h=650&w=940" class="zoom img-fluid " alt="">
                    </a>
                </div>

                <div class="col-lg-3 col-md-4 col-xs-6 thumb">
                    <a href="../ImageSlideShow/ImageTemp/image006.jpg?auto=compress&cs=tinysrgb&h=650&w=940" class="fancybox" rel="ligthbox">
                        <img src="../ImageSlideShow/ImageTemp/image006.jpg?auto=compress&cs=tinysrgb&h=650&w=940" class="zoom img-fluid " alt="">
                    </a>
                </div>

                <div class="col-lg-3 col-md-4 col-xs-6 thumb">
                    <a href="../ImageSlideShow/ImageTemp/image007.jpg?auto=compress&cs=tinysrgb&h=650&w=940" class="fancybox" rel="ligthbox">
                        <img src="../ImageSlideShow/ImageTemp/image007.jpg?auto=compress&cs=tinysrgb&h=650&w=940" class="zoom img-fluid " alt="">
                    </a>
                </div>

                <div class="col-lg-3 col-md-4 col-xs-6 thumb">
                    <a href="../ImageSlideShow/ImageTemp/image008.jpg?auto=compress&cs=tinysrgb&dpr=2&h=650&w=940" class="fancybox" rel="ligthbox">
                        <img src="../ImageSlideShow/ImageTemp/image008.jpg?auto=compress&cs=tinysrgb&dpr=2&h=650&w=940" class="zoom img-fluid " alt="">
                    </a>
                </div>

                <div class="col-lg-3 col-md-4 col-xs-6 thumb">
                    <a href="../ImageSlideShow/ImageTemp/image003.jpg?auto=compress&cs=tinysrgb&dpr=2&h=650&w=940" class="fancybox" rel="ligthbox">
                        <img src="../ImageSlideShow/ImageTemp/image003.jpg?auto=compress&cs=tinysrgb&dpr=2&h=650&w=940" class="zoom img-fluid " alt="">
                    </a>
                </div>

                 <div class="col-lg-12 col-md-12 col-xs-12 text-center text-white " style="font-family:Roboto">
                     <h4>SÁCH TRƯNG BÀY TẠI THƯ VIỆN KHU AB (Điện Biên Phủ)</h4>
                        <iframe class="col-lg-12 col-md-12 col-xs-12 thumb" src="https://www.google.com/maps/embed?pb=!4v1637570105505!6m8!1m7!1sCAoSLEFGMVFpcE9JTUh3SWdzeEYyVlM4cjRnWnZuRGhEcU5qaktkQnAtTFlOdHdH!2m2!1d10.8021294!2d106.7146547!3f279.8109955461667!4f-24.281346783729802!5f0.7820865974627469" height="500" frameborder="0" style="border:0;" allowfullscreen="" aria-hidden="false" tabindex="0"></iframe>
                    <p>Ảnh 360 (nhấn giữ chuột và xoay)</p>
                 </div>                
                <div class="col-lg-12 col-md-12 col-xs-12 mb-5 text-center text-white" style="font-family:Roboto">
                     <h4>TÀI LIỆU THƯ VIỆN KHU E3 (Khu công nghệ cao)</h4>
                       <iframe class="col-lg-12 col-md-12 col-xs-12 thumb" src="https://www.google.com/maps/embed?pb=!4v1618805041187!6m8!1m7!1sCAoSLEFGMVFpcE9pVUlxM2VFUFVkWW5YNnd6NGdRMHdic1JMZGt4NktxZTgzOGEx!2m2!1d10.8556247!2d106.7855882!3f335!4f0!5f0.7820865974627469"  height="500" frameborder="0" style="border:0;" allowfullscreen="" aria-hidden="false" tabindex="0"></iframe>
                      <p>Ảnh 360 (nhấn giữ chuột và xoay)</p>
                 </div>
            </div>
            
            <div >
              
            </div>
            
        </div>
    </form>
</body>
</html>


