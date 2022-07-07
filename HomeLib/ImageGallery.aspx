<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ImageGallery.aspx.cs" Inherits="HomeLib_ImageGallery" %>


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
                    <span class="current">> Ngày sách và văn hóa đọc - Thư viện HUTECH</span>
                </div>
            </div>
        <!-- Page Content -->
        <div class="container page-top">
            <div>
                  <h2 class="mb-2">NGÀY SÁCH VÀ VĂN HÓA ĐỌC</h2>
            </div>
            <div class="text-white" style="font-family:Arial; font-size:1em">
                <p>Chào mừng Ngày Sách và Văn hoá đọc Việt Nam 21/4, Ngày Sách và Bản quyền Thế giới 23/4, Thư viện HUTECH tổ chức triển lãm sách 2021 nhằm khuyến đọc và phát triển văn hóa đọc.</p>
                <p>Chủ đề Sách - Văn hóa đọc trong thời đại công nghệ 4.0 được tổ chức nhằm khuyến khích, phát triển phong trào đọc sách trong nhà trường, khẳng định tầm quan trọng của sách và văn hóa đọc trong sự nghiệp phát triển văn hóa, khoa học, giáo dục… của đất nước; Phát triển văn hóa đọc trong việc xây dựng một xã hội tri thức, hướng tới việc đọc sách trở thành một thói quen và một nét đẹp không thể thiếu trong sinh viên và cán bộ giảng viên.</p>
                <p>Triển lãm Sách - Văn hóa đọc trong thời đại công nghệ 4.0, trưng bày, giới thiệu khoảng hơn 1.000 cuốn sách theo nhiều nội dung: Lịch sử Việt Nam hiện đại, tiếp cận tri thức, nuôi dưỡng tâm hồn,  kỹ năng sống,</p>
                <p>Bên cạnh sách in (truyền thống), Thư viện còn trưng bày “Thư viện sách ảo”, bằng cách thông qua mã QR các bạn có thể khám phá và tiếp cận “Thư viện sách ảo” với loại hình tài liệu số có tại thư viện. Tham gia triển lãm này, các bạn được trải nghiệm hoạt động đọc sách trên thiết bị động và đọc bất kỳ đâu nếu có kết nối Internet.</p>
                <p>Thông qua các đầu sách được trưng bày, khẳng định sứ mệnh của Sách chính là phương tiện, công cụ lưu giữ sự tiến bộ, văn minh và chặng đường lịch sử phát triển của con người, đồng thời là nguồn cung cấp tri thức bất tận và món quà dành tặng tâm hồn và đọc sách là con đường ngắn nhất để tiếp cận và tiếp thu tinh hoa tri thức của nhân loại, tạo tiền đề cho sự phát triển của mỗi cá nhân và toàn xã hội.</p>
                <p>Triển làm sách tại thư viện (có ở cơ sở Thư viện Điện Biên Phủ-Tầng 3-Khu B và Thư viện E3-Tầng 2-Khu Công nghệ cao) mở cửa cho tất cả những ai yêu quý Sách!</p>
            </div><br />
            <div class="row">              
                <div class="col-lg-3 col-md-4 col-xs-6 thumb">
                    <a href="../ImageSlideShow/ImageTemp/image001.jpg?auto=compress&cs=tinysrgb&h=650&w=940" class="fancybox" rel="ligthbox">
                        <img src="../ImageSlideShow/ImageTemp/image001.jpg?auto=compress&cs=tinysrgb&h=650&w=940" class="zoom img-fluid " alt="">
                    </a>
                </div>
                <div class="col-lg-3 col-md-4 col-xs-6 thumb">
                    <a href="../ImageSlideShow/ImageTemp/image006.jpg?auto=compress&cs=tinysrgb&h=650&w=940" class="fancybox" rel="ligthbox">
                        <img src="../ImageSlideShow/ImageTemp/image006.jpg?auto=compress&cs=tinysrgb&h=650&w=940" class="zoom img-fluid" alt="">
                    </a>
                </div>

                <div class="col-lg-3 col-md-4 col-xs-6 thumb">
                    <a href="../ImageSlideShow/ImageTemp/image008.jpg?auto=compress&cs=tinysrgb&dpr=2&h=650&w=940" class="fancybox" rel="ligthbox">
                        <img src="../ImageSlideShow/ImageTemp/image008.jpg?auto=compress&cs=tinysrgb&dpr=2&h=650&w=940" class="zoom img-fluid " alt="">
                    </a>
                </div>

                <div class="col-lg-3 col-md-4 col-xs-6 thumb">
                    <a href="../ImageSlideShow/ImageTemp/image010.jpg?auto=compress&cs=tinysrgb&dpr=2&h=650&w=940" class="fancybox" rel="ligthbox">
                        <img src="../ImageSlideShow/ImageTemp/image010.jpg?auto=compress&cs=tinysrgb&dpr=2&h=650&w=940" class="zoom img-fluid " alt="">
                    </a>
                </div>

                <div class="col-lg-3 col-md-4 col-xs-6 thumb">
                    <a href="../ImageSlideShow/ImageTemp/image012.jpg?auto=compress&cs=tinysrgb&h=650&w=940" class="fancybox" rel="ligthbox">
                        <img src="../ImageSlideShow/ImageTemp/image012.jpg?auto=compress&cs=tinysrgb&h=650&w=940" class="zoom img-fluid " alt="">
                    </a>
                </div>

                <div class="col-lg-3 col-md-4 col-xs-6 thumb">
                    <a href="../ImageSlideShow/ImageTemp/image016.jpg?auto=compress&cs=tinysrgb&h=650&w=940" class="fancybox" rel="ligthbox">
                        <img src="../ImageSlideShow/ImageTemp/image016.jpg?auto=compress&cs=tinysrgb&h=650&w=940" class="zoom img-fluid " alt="">
                    </a>
                </div>

                <div class="col-lg-3 col-md-4 col-xs-6 thumb">
                    <a href="../ImageSlideShow/ImageTemp/image013.jpg?auto=compress&cs=tinysrgb&dpr=2&h=650&w=940" class="fancybox" rel="ligthbox">
                        <img src="../ImageSlideShow/ImageTemp/image013.jpg?auto=compress&cs=tinysrgb&dpr=2&h=650&w=940" class="zoom img-fluid " alt="">
                    </a>
                </div>

                <div class="col-lg-3 col-md-4 col-xs-6 thumb">
                    <a href="../ImageSlideShow/ImageTemp/image004.jpg?auto=compress&cs=tinysrgb&dpr=2&h=650&w=940" class="fancybox" rel="ligthbox">
                        <img src="../ImageSlideShow/ImageTemp/image004.jpg?auto=compress&cs=tinysrgb&dpr=2&h=650&w=940" class="zoom img-fluid " alt="">
                    </a>
                </div>

                 <div class="col-lg-12 col-md-12 col-xs-12 text-center text-white " style="font-family:Roboto">
                     <h4>SÁCH TRƯNG BÀY TẠI THƯ VIỆN KHU AB (Điện Biên Phủ)</h4>
                        <iframe class="col-lg-12 col-md-12 col-xs-12 thumb" src="https://www.google.com/maps/embed?pb=!4v1618804907295!6m8!1m7!1sCAoSLEFGMVFpcFAtZ0l6d0tRaUlreXo3cW45VFVkdXFpUXlFRDFfRjVHQnpFNEpR!2m2!1d10.8022143!2d106.7146287!3f177.07!4f21.950000000000003!5f0.5970117501821992" height="500" frameborder="0" style="border:0;" allowfullscreen="" aria-hidden="false" tabindex="0"></iframe>
                    <p>Ảnh 360 (nhấn giữ chuột và xoay)</p>
                 </div>                
                 <div class="col-lg-12 col-md-12 col-xs-12 mb-5 text-center text-white" style="font-family:Roboto">
                     <h4>SÁCH TRƯNG BÀY TẠI THƯ VIỆN KHU E3 (Khu công nghệ cao)</h4>
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


