<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GeneralReportHost.aspx.cs" Inherits="MvcFramework.Mvc.WebFormReportHosts.GeneralReportHost" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Reports</title>
</head>
<body>
    <script src="../Scripts/jquery-1.7.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui-1.8.16.min.js" type="text/javascript"></script>
    <link href="../Content/redmond/jquery-ui-1.8.7.custom.css" rel="stylesheet" type="text/css" />
    <script>
        $(function () {
            $(".datepicker").datepicker();
        });
    </script>
    <asp:Literal ID="LiteralScriptInjection" Mode="PassThrough" runat="server"></asp:Literal>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div id="report-options">
          
        </div>
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" SizeToReportContent="True" AsyncRendering="True" ShowRefreshButton="False"
            OnPreRender="ReportViewer1_PreRender">
        </rsweb:ReportViewer>
    </div>
    </form>
</body>
</html>
