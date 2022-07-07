<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ShowSlider.ascx.cs" Inherits="Book_ShowSlider" %>
<link href="Content/ShowSlide.css" rel="stylesheet" />
<link href="//maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" rel="stylesheet" id="bootstrap-css">

<!------ Include the above in your HEAD tag ---------->
<script src="https://cdnjs.cloudflare.com/ajax/libs/slick-carousel/1.6.0/slick.js"></script>
<script type="text/javascript">
    $(document).ready(function () {
        $('.customer-logos').slick({
            slidesToShow: 5,
            slidesToScroll: 1,
            autoplay: true,
            autoplaySpeed: 4000,
            arrows: false,
            dots: false,
            pauseOnHover: false,
            responsive: [{
                breakpoint: 768,
                settings: {
                    slidesToShow: 4
                }
            }, {
                breakpoint: 520,
                settings: {
                    slidesToShow: 3
                }
            }]
        });
    });
</script>

<style>
    h2 {
        text-align: center;
        padding: 20px;
    }
    /* Slider */

    .slick-slide {
        margin: 0px 20px;
    }

        .slick-slide img {
            width: 120px;
            height:170px
        }

    .slick-slider {
        position: relative;
        display: block;
        box-sizing: border-box;
        -webkit-user-select: none;
        -moz-user-select: none;
        -ms-user-select: none;
        user-select: none;
        -webkit-touch-callout: none;
        -khtml-user-select: none;
        -ms-touch-action: pan-y;
        touch-action: pan-y;
        -webkit-tap-highlight-color: transparent;
    }

    .slick-list {
        position: relative;
        display: block;
        overflow: hidden;
        margin: 0;
        padding: 0;
    }

        .slick-list:focus {
            outline: none;
        }

        .slick-list.dragging {
            cursor: pointer;
            cursor: hand;
        }

    .slick-slider .slick-track,
    .slick-slider .slick-list {
        -webkit-transform: translate3d(0, 0, 0);
        -moz-transform: translate3d(0, 0, 0);
        -ms-transform: translate3d(0, 0, 0);
        -o-transform: translate3d(0, 0, 0);
        transform: translate3d(0, 0, 0);
    }

    .slick-track {
        position: relative;
        top: 0;
        left: 0;
        display: block;
    }

        .slick-track:before,
        .slick-track:after {
            display: table;
            content: '';
        }

        .slick-track:after {
            clear: both;
        }

    .slick-loading .slick-track {
        visibility: hidden;
    }

    .slick-slide {
        display: none;
        float: left;
        height: 100%;
        min-height: 1px;
    }

    [dir='rtl'] .slick-slide {
        float: right;
    }

    .slick-slide img {
        display: block;
    }

    .slick-slide.slick-loading img {
        display: none;
    }

    .slick-slide.dragging img {
        pointer-events: none;
    }

    .slick-initialized .slick-slide {
        display: block;
    }

    .slick-loading .slick-slide {
        visibility: hidden;
    }

    .slick-vertical .slick-slide {
        display: block;
        height: auto;
        border: 1px solid transparent;
    }

    .slick-arrow.slick-hidden {
        display: none;
    }
</style>

<div class="container">
    <h4 class="text-left">Tìm kiếm phổ biến</h4>
    <section class="customer-logos slider">
        <div class="slide"><a href="#"><img src="../ImageSlideShow/100808.jpg">Dummies</a> </div>
        <div class="slide">
            <img src="../ImageSlideShow/100809.jpg"></div>
        <div class="slide">
            <img src="../ImageSlideShow/100810.jpg"></div>
        <div class="slide">
            <img src="../ImageSlideShow/100812.jpg"></div>
        <div class="slide">
            <img src="../ImageSlideShow/100813.jpg"></div>
        <div class="slide">
            <img src="../ImageSlideShow/100819.jpg"></div>
        <div class="slide">
            <img src="../ImageSlideShow/10082.jpg"></div>
        <div class="slide">
            <img src="../ImageSlideShow/100820.jpg"></div>
        <div class="slide">
            <img src="../ImageSlideShow/100822.jpg"></div>
    </section>
</div>
