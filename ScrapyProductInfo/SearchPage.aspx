﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SearchPage.aspx.cs" Inherits="ScrapyProductInfo.SearchPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
   <form id="form1" runat="server">
        <div>

            <asp:Label ID="Label2" runat="server" Text="下载文件夹："></asp:Label>
            <asp:TextBox ID="txtFolder" runat="server" Width="360px"></asp:TextBox>
            <br />
            <br />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="Label1" runat="server" Text="关键词："></asp:Label>
            <asp:TextBox ID="txtKeyWord" runat="server" Width="352px">山图</asp:TextBox>
            &nbsp;<asp:Button ID="btnSearch" runat="server" Text="搜索" OnClick="btnSearch_Click" />
        </div>

        <div>
            <table>
                <asp:Repeater ID="rpProduct" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td>
                                <%#Eval("name").ToString() %>
                            </td>
                            <td>
                                <img src='<%# Eval("imgUrl") %>' alt='<%# Server.HtmlEncode(Eval("name").ToString()) %>' />
                            </td>
                            <%--  <td>
                                选择下载路径: <input id="InputFolder"  runat="server" size="30"/>
                            </td>--%>
                            <td>
                                <%--<%# "<asp:Button ID=\"btnDownload\" runat=\"server\" Text=\"下载\" onclick=\"downloadImg\">"%>--%>
                                <asp:Button ID="btnDownload" runat="server" Text="下载" onlick="downloadImg('<%# Eval('goodsNo') %>')"/>
                                <%--onlick="downloadImg('<%# Eval('goodsNo') %>')"--%> 
                                <%--<asp:Button ID="btnDownload" runat="server" Text="下载" onlick="downloadImg('<%# Eval('goodsNo') %>')" />--%>
                            </td>

                        </tr>
                        <%--<a href="<%# Eval("HttpUrl") %>"   target="<%# Eval("target") %>">--%>
                        <%-- <img src="<%# Eval("imgUrl") %>" alt="<%# Server.HtmlEncode(Eval("skuName").ToString()) %>"/></a>--%>
                        <%--<img src="<%# Eval("imgUrl") %>") %>"/>--%>
                    </ItemTemplate>
                </asp:Repeater>
            </table>

        </div>
    </form>
</body>
</html>

<script type="text/javascript">

    function downloadImg(goodsNo) {
        var folder = $("txtFolder").val();
        $.ajax({
                //要用post方式      
                type: "Post",
                //方法所在页面和方法名      
                url: "SearchPage.aspx/DownloadDetail",
                contentType: "application/json; charset=utf-8",
                data: '{"folder":"' + folder + '" ,"goodsNo":" ' + goodsNo + '"}',
                dataType: "json",
                success: function (data) {
                    if (data.d >= 1) {
                        alert("下载成功!" );
                    }
                    else {
                        alert("下载失败!");
                    }
                },
                error: function (err) {
                    alert(err);
                }
        });
    }
   
</script>
