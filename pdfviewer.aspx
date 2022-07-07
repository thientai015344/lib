<%@ Page Language="C#" AutoEventWireup="true" CodeFile="pdfviewer.aspx.cs" Inherits="pdfviewer" %>

<!DOCTYPE html>
<!--[if IEMobile 7 ]>    <html class="no-js iem7"> <![endif]-->
<!--[if (gt IEMobile 7)|!(IEMobile)]><!--> <html class="no-js"> <!--<![endif]-->
  <head>
    <meta charset="utf-8">
    <title>Bootstrap PDF Viewer</title>
    <meta name="description" content="">
    <meta name="HandheldFriendly" content="True">
    <meta name="MobileOptimized" content="320">
    <meta name="viewport" content="initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no">
    <meta http-equiv="cleartype" content="on">

    <!-- For iOS web apps. Delete if not needed. https://github.com/h5bp/mobile-boilerplate/issues/94 -->    
    <meta name="apple-mobile-web-app-capable" content="yes">
    <meta name="apple-mobile-web-app-status-bar-style" content="black">
    <meta name="apple-mobile-web-app-title" content="">
    
    <!-- Twitter Bootstrap -->
    <link rel="stylesheet" href="lib/twitter-bootstrap/css/bootstrap.css">

    <!-- Font Awesome -->
    <link rel="stylesheet" href="lib/font-awesome/css/font-awesome.css">

    <!-- Bootstrap PDF Viewer -->
    <link rel="stylesheet" href="css/bootstrap-pdf-viewer.css">

      <link rel="stylesheet" type="text/css" href="css/flexpaper.css" />
    <script type="text/javascript" src="js/jquery.min.js"></script>
    <script type="text/javascript" src="js/flexpaper.js"></script>
    <script type="text/javascript" src="js/flexpaper_handlers.js"></script>
<script type="text/javascript">
    $('#documentViewer').FlexPaperViewer(
            {
                config: {

                    SWFFile: '\\Data\temp\102228.swf',

                    Scale: 0.6,
                    ZoomTransition: 'easeOut',
                    ZoomTime: 0.5,
                    ZoomInterval: 0.2,
                    FitPageOnLoad: true,
                    FitWidthOnLoad: false,
                    FullScreenAsMaxWindow: false,
                    ProgressiveLoading: false,
                    MinZoomSize: 0.2,
                    MaxZoomSize: 5,
                    SearchMatchAll: false,
                    InitViewMode: 'Portrait',
                    RenderingOrder: 'flash',
                    StartAtPage: '',

                    ViewModeToolsVisible: true,
                    ZoomToolsVisible: true,
                    NavToolsVisible: true,
                    CursorToolsVisible: true,
                    SearchToolsVisible: true,
                    WMode: 'window',
                    localeChain: 'en_US'
                }
            }
    );
</script>
      

  </head>
  <body>
      <div id="documentViewer" class="flexpaper_viewer" style="width:770px;height:500px"></div>

    
  </body>
</html>