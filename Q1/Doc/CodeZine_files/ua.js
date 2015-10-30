(function () {

  function getCookie(c_name) {
    var st = "";
    var ed = "";
    if (document.cookie.length > 0) {
      st = document.cookie.indexOf(c_name + "=");
      if (st != -1) {
        st = st + c_name.length + 1;
        ed = document.cookie.indexOf(";", st);
        if (ed == -1) ed = document.cookie.length;
        return unescape(document.cookie.substring(st, ed));
      }
    }
    return "";
  }

  var mainStyle = $('#mainStyle');
  var subStyle = $('#subStyle');
  var mode = getCookie("mode");

  var ua = navigator.userAgent;
  if (ua.indexOf('iPad') > 0 || (ua.indexOf('Android') > 0 && ua.indexOf('Mobile') == -1)) {
    mainStyle.attr("href","/lib/css/tablet.css");
				subStyle.remove();
  } else if ((ua.indexOf('iPhone') > 0 || ua.indexOf('iPod') > 0 || (ua.indexOf('Android') > 0 && ua.indexOf('Mobile') > 0) || ua.indexOf('Windows Phone') > 0 || ua.indexOf('BlackBerry') > 0)) {
    if (mode == "pcMode") {
      mainStyle.attr("href","/lib/css/mobile.css").attr("media", "speech").addClass("mbStyle");
      subStyle.attr("href","/lib/css/pc.css").attr("media", "all").addClass("pcStyle");
    } else {
      mainStyle.attr("href","/lib/css/mobile.css").attr("media", "all").addClass("mbStyle");
      subStyle.attr("href","/lib/css/pc.css").attr("media", "speech").addClass("pcStyle");
    }
  } else {
			 subStyle.remove();
		}

})();