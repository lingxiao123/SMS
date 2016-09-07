<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="SMS.Login" %>

<!DOCTYPE html>
<html>
<head>
<title>卧尔美短信平台</title>
<link href="css/style.css" rel='stylesheet' type='text/css' />
<meta name="viewport" content="width=device-width, initial-scale=1">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta name="keywords" content="" />
<script type="application/x-javascript"> addEventListener("load", function() { setTimeout(hideURLbar, 0); }, false); function hideURLbar(){ window.scrollTo(0,1); } </script>
<script src="Scripts/jquery-1.10.2.min.js"></script>
<script type="text/javascript">
    $(function(){
        document.getElementById("UserName").focus();
    });
</script>
</head>
<body onload="getRememberInfo();">
<!-- /start-main -->
    <h1></h1>
	<!-- /register -->	
	<!-- //register -->	
		<script>
		$('.button').click(function (e) {
		  e.preventDefault();
		  $(this).parent().toggleClass('expand');
		  $(this).parent().children().toggleClass('expand');
		});
		</script>
		  <!-- /login-inner -->	
			<div class="login-inner">
				<div class="log-head">
						<h2>卧尔美短信平台</h2>
						</div>
								<div class="login-top">
								 <form action="#" method="post">
									<input type="text" id="UserName" name="Email"  class="email" placeholder="用户名" required="" />
									<input type="password" id="Pwd" name="Password" class="password" placeholder="密码" required=""  />	
									<input type="checkbox" id="remember"  name="remember">
									<label for="remember"><span></span>记住我</label>
									<div class="login-bottom">
									<ul>
										<li>
											<a href="#"></a>
										</li>
										<li>
											<input type="button" value="登录"  id="Login" onclick="login();" />
										</li>
									</ul>
									<div class="clearfix"></div>
								</div>
								</form>								
								<div class="clearfix"></div>                                   		
							</div>
							<div class="social-icons">
						    </div>
						</div>	
						<!-- //login-inner -->	
	                    <div class="clearfix"> </div>	
                        <!-- /copy-right -->	
                        <div class="copy-right w3ls">
		                        <p> © 2016  赣州通友科技有限公司</p>
                        </div>
	                    <!-- //copy-right -->	
                        <!-- //end-main -->
                        <script type="text/javascript">
                            function login() {
                                var username = $("#UserName").val();
                                var pwd = $("#Pwd").val();
                                if (document.all.remember.checked) {
                                    setCookie("UserName", username, 24, "/");
                                }
                                $.ajax({
                                    url: "http://" + location.host + "/LoginHandler.ashx",
                                    type: "post",
                                    dataType:"text",
                                    data:{"username":username,"pwd":pwd},
                                    async: false,
                                    success: function (data) {
                                        if(data==1){
                                            window.location.href = "http://"+location.host+"/Index.aspx";
                                        } else if(data==0) {
                                            alert("对不起，该用户已被禁用,请联系管理员!");
                                        } else {
                                            alert("用户名或密码错误！");
                                        }
                                    }
                                });
                            }
                        </script>
                        <script type="text/javascript">
                            //新建cookie。
                            //hours为空字符串时,cookie的生存期至浏览器会话结束。hours为数字0时,建立的是一个失效的cookie,这个cookie会覆盖已经建立过的同名、同path的cookie（如果这个cookie存在）。
                            function setCookie(name, value, hours, path) {
                                var name = escape(name);
                                var value = escape(value);
                                var expires = new Date();
                                expires.setTime(expires.getTime() + hours * 3600000);
                                path = path == "" ? "" : ";path=" + path;
                                _expires = (typeof hours) == "string" ? "" : ";expires=" + expires.toUTCString();
                                document.cookie = name + "=" + value + _expires + path;
                            }
                            //获取cookie值
                            function getCookieValue(name) {
                                var name = escape(name);
                                //读cookie属性，这将返回文档的所有cookie
                                var allcookies = document.cookie;
                                //查找名为name的cookie的开始位置
                                name += "=";
                                var pos = allcookies.indexOf(name);
                                //如果找到了具有该名字的cookie，那么提取并使用它的值
                                if (pos != -1) { //如果pos值为-1则说明搜索"version="失败
                                    var start = pos + name.length; //cookie值开始的位置
                                    var end = allcookies.indexOf(";", start); //从cookie值开始的位置起搜索第一个";"的位置,即cookie值结尾的位置
                                    if (end == -1) end = allcookies.length; //如果end值为-1说明cookie列表里只有一个cookie
                                    var value = allcookies.substring(start, end); //提取cookie的值
                                    return unescape(value); //对它解码
                                }
                                else return ""; //搜索失败，返回空字符串
                            }
                            //获取cookie信息
                            function getRememberInfo() {
                                // alert("---获取cookie信息---");
                                try {
                                    var userName = "";
                                    var userPassword = "";
                                    userName = getCookieValue("UserName");
                                    document.getElementById("UserName").value = userName;
                                } catch (err) {
                                }
                            }
                        </script>
                        <script type="text/javascript">
	                        document.onkeydown=function(event){
	                            var e = event || window.event || arguments.callee.caller.arguments[0];           
	                             if(e && e.keyCode==13){ // enter 键
	                                 //要做的事情
	                                 login();
	                            }
	                        };
                        </script>
</body>
</html>

