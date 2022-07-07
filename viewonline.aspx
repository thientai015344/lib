<%@ Page Language="C#" AutoEventWireup="true" CodeFile="viewonline.aspx.cs" Inherits="viewonline" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
 <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
<link href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" rel="stylesheet"/>
<link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css"/>
<script src="https://code.jquery.com/jquery-1.12.0.min.js"></script>       
<script src='https://www.google.com/recaptcha/api.js'></script> 
</head>
<body>
    <form id="form1" runat="server">
        <div class="container-fluid">
            <div class="row">
                <div class="text-center">
                    <asp:PlaceHolder ID="PlaceHolderView" runat="server"></asp:PlaceHolder>
                </div>
            </div>
        </div>
          
    </form>
</body>
</html>
