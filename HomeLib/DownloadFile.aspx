<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DownloadFile.aspx.cs" Inherits="HomeLib_DownloadFile" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <meta charset="utf-8">
  <meta http-equiv="X-UA-Compatible" content="IE=edge">
  <meta name="viewport" content="width=device-width, initial-scale=1">
     <link rel="stylesheet" href="css/bootstrap.min.css">
     <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">
  <title></title>  
</head>

<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="container mt-5">
                <a href="/">Trang chủ</a>
                <span class="icon-keyboard_arrow_right"></span>
                <span class="current">> Download</span>
            </div>
            <div class="row mt-2 p-4">
                <h5><i class="fa fa-warning" style="font-size:18px"></i><asp:Label ID="Lab_Mes" runat="server" ></asp:Label></h5>
            </div>            
        </div>
    </form>
</body>
</html>
