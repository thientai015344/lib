<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Vanhoadoc.aspx.cs" Inherits="HomeLib_ImageGallery" %>


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
                    <span class="current">> Ngày Sách và Văn hóa đọc </span>
                    
                </div>            
            </div>
        <!-- Page Content -->
        <div class="container page-top">
            <div>
                  <h2 class="mb-2">Ngày Sách và Văn hóa đọc Việt Nam (21/4), Ngày Sách và Bản quyền thế giới (23/4)</h2>
            </div>
			<div class="text-white" style="font-family: Arial; font-size: 1em">
				<p>
					<span style="font-size: 16px"><span style="font-family: Arial,Helvetica,sans-serif">Xin ch&agrave;o c&aacute;c bạn!<br />
						H&ograve;a c&ugrave;ng kh&ocirc;ng kh&iacute; ch&agrave;o mừng Ng&agrave;y S&aacute;ch v&agrave; Văn h&oacute;a đọc Việt Nam (21/4), Ng&agrave;y S&aacute;ch v&agrave; Bản quyền thế giới (23/4), Thư viện Hutech tổ chức trưng b&agrave;y &quot;S&Aacute;CH ƠI MỞ RA&quot; mang đến cho bạn đọc cảm nhận s&acirc;u sắc về gi&aacute; trị của văn h&oacute;a đọc .Th&ocirc;ng qua những banner nhiều sắc m&agrave;u được trưng b&agrave;y b&ecirc;n ngo&agrave;i khu&ocirc;n vi&ecirc;n. Thư viện h&acirc;n hoan giới thiệu đến bạn đọc c&aacute;c t&aacute;c phẩm best-seller như (Sapiens: lược sử lo&agrave;i người, Mu&ocirc;n kiếp nh&acirc;n sinh, Tuổi trẻ đ&aacute;ng gi&aacute; bao nhi&ecirc;u, ...) mang đến trải nghiệm đọc s&aacute;ch mới lạ cho bạn đọc.<br />
						Ngo&agrave;i ra, Thư viện Hutech c&ograve;n tiếp nhận nhiều t&agrave;i liệu chuy&ecirc;n ng&agrave;nh kh&aacute;c nhau từ Qu&yacute; thầy c&ocirc; gửi tặng g&oacute;p phần l&agrave;m phong ph&uacute; th&ecirc;m nguồn học liệu Thư viện.<br />
						Th&acirc;n mời bạn đến &quot;MỞ S&Aacute;CH&quot; v&agrave; trải nghiệm!</span></span>
				</p>
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


