<%@ Application Language="C#" %>
<%@ Import Namespace="System.Web.Routing" %>
<script runat="server">
    void RegisterRoutes(RouteCollection routes)
    {
        routes.MapPageRoute("tim-kiem", "tim-kiem", "~/HomeLib/SearchContent.aspx");
        //  routes.MapPageRoute("chi-tiet", "chi-tiet/{id}", "~/Bookdetails.aspx");
        routes.MapPageRoute("chi-tiet", "chi-tiet", "~/Bookdetails.aspx");
        routes.MapPageRoute("gio-tai-lieu", "gio-tai-lieu", "~/DocumentCart.aspx");
        routes.MapPageRoute("phu-luc", "phu-luc", "~/viewonline.aspx");
        routes.MapPageRoute("checkout", "checkout", "~/Checkout.aspx");
        routes.MapPageRoute("media", "media", "~/cd-english.aspx");
        routes.MapPageRoute("guest-down", "guest-down", "~/guestdown.aspx");
        routes.MapPageRoute("sach-muon", "sach-muon", "~/borrowingbook.aspx");
        routes.MapPageRoute("sach-moi", "sach-moi", "~/NewBookInfo.aspx");
        routes.MapPageRoute("dang-nhap", "st/dang-nhap", "~/Student/Login.aspx");
        routes.MapPageRoute("dang-ky", "st/dang-ky", "~/Student/Register.aspx");
        routes.MapPageRoute("danh-sach-tai", "st/danh-sach-tai", "~/Student/listdown.aspx");
        routes.MapPageRoute("quen-mat-ma", "quen-mat-ma", "~/ChangePassword.aspx");
        routes.MapPageRoute("ResetPassword", "ResetPassword", "~/ResetPassword.aspx");
        routes.MapPageRoute("Xac-nhan-email", "Xac-nhan-email", "~/ActiveEmail.aspx");
        routes.MapPageRoute("", "", "~/home.aspx");
        routes.MapPageRoute("Gioi-thieu", "Gioi-thieu", "~/Homelib/Gioi-thieu.aspx"); 
        routes.MapPageRoute("Login", "login", "~/Homelib/login.aspx"); 
        routes.MapPageRoute("Profile", "Profile", "~/Homelib/Profile.aspx"); 
        routes.MapPageRoute("Nganh-mon", "Nganh-mon", "~/Nganh-mon-hoc.aspx");
        routes.MapPageRoute("DownloadFile", "DownloadFile", "~/HomeLib/DownloadFile.aspx");
        routes.MapPageRoute("Schedule", "Schedule", "~/HomeLib/Schedule.aspx");
        routes.MapPageRoute("Noi-quy", "Noi-quy", "~/HomeLib/Noi-quy.aspx");
        routes.MapPageRoute("Nganh-hoc-mon-hoc", "Nganh-hoc-mon-hoc", "~/HomeLib/Majors.aspx");
        routes.MapPageRoute("co-so-du-lieu", "co-so-du-lieu", "~/HomeLib/LinkDatabase.aspx");
        routes.MapPageRoute("huong-dan-dang-ky-tai-tai-lieu", "huong-dan-dang-ky-tai-tai-lieu", "~/HomeLib/HuongDanDangKyTaiTaiLieu.aspx");
        routes.MapPageRoute("huong-dan-dang-ky-da-phuong-tien", "huong-dan-dang-ky-da-phuong-tien", "~/HomeLib/HuongDanDangKyDaPhuongTien.aspx");
        routes.MapPageRoute("huong-dan-dang-ky-the", "huong-dan-dang-ky-the", "~/HomeLib/HuongDanDangKyThe.aspx");
        routes.MapPageRoute("huong-dan-tra-cuu", "huong-dan-tra-cuu", "~/HomeLib/HuongDanTraCuu.aspx");
        routes.MapPageRoute("gia-hạn-tai-lieu", "gia-hạn-tai-lieu", "~/HomeLib/Giahantailieu.aspx");
        routes.MapPageRoute("doi-ngu-nhan-vien", "doi-ngu-nhan-vien", "~/HomeLib/DoiNguNhanVien.aspx");
        routes.MapPageRoute("tai-lieu-truy-cap-mo", "tai-lieu-truy-cap-mo", "~/HomeLib/Tailieutruycapmo.aspx");
        routes.MapPageRoute("dang-ky-tai-khoan", "dang-ky-tai-khoan", "~/HomeLib/Register.aspx");
        routes.MapPageRoute("fts", "fts", "~/HomeLib/SearchContent.aspx");
        routes.MapPageRoute("AdvancedSearch", "AdvancedSearch", "~/HomeLib/AdvancedSearch.aspx");
        routes.MapPageRoute("Ngay-sach-va-hoa-doc", "Ngay-sach-va-hoa-doc", "~/HomeLib/Vanhoadoc.aspx");
        routes.MapPageRoute("tai-lieu-theo-linh-vuc", "tai-lieu-theo-linh-vuc", "~/HomeLib/Tailieulinhvuc.aspx");
        routes.MapPageRoute("link-tai-app", "link-tai-app", "~/HomeLib/LinkApp.aspx");
        routes.MapPageRoute("do-an-tot-nghiep", "do-an-tot-nghiep", "~/HomeLib/Doan.aspx");
        routes.MapPageRoute("luan-van", "luan-van", "~/HomeLib/Luanvan.aspx");
        routes.MapPageRoute("ShoppingCart", "ShoppingCart", "~/HomeLib/ShoppingCart.aspx");
    }
    void Application_Start(object sender, EventArgs e)
    {

        // Code that runs on application startup
        RegisterRoutes(RouteTable.Routes);
    }

    void Application_End(object sender, EventArgs e)
    {
        //  Code that runs on application shutdown

    }
    void Application_Error(object sender, EventArgs e)
    {
        // Code that runs when an unhandled error occurs

    }

    void Session_Start(object sender, EventArgs e)
    {

        // Code that runs when a new session is started

    }

    void Session_End(object sender, EventArgs e)
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }

</script>
