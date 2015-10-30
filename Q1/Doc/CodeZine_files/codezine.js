// Cookie Plugin
jQuery.cookie=function(name,value,options){if(typeof value!='undefined'){options=options||{};if(value===null){value='';options=$.extend({},options);options.expires=-1;}var expires='';if(options.expires&&(typeof options.expires=='number'||options.expires.toUTCString)){var date;if(typeof options.expires=='number'){date=new Date();date.setTime(date.getTime()+(options.expires*24*60*60*1000));}else{date=options.expires;}expires='; expires='+date.toUTCString();}var path=options.path?'; path='+(options.path):'';var domain=options.domain?'; domain='+(options.domain):'';var secure=options.secure?'; secure':'';document.cookie=[name,'=',encodeURIComponent(value),expires,path,domain,secure].join('');}else{var cookieValue=null;if(document.cookie&&document.cookie!=''){var cookies=document.cookie.split(';');for(var i=0;i<cookies.length;i++){var cookie=jQuery.trim(cookies[i]);if(cookie.substring(0,name.length+1)==(name+'=')){cookieValue=decodeURIComponent(cookie.substring(name.length+1));break;}}}return cookieValue;}};

$(function(){

//  PC Mobile mode Switch
  var pcStyle = $('.pcStyle');
  var mbStyle = $('.mbStyle');
  var mbNav   = $('.modeSwitch');
  var pcBtn   = $('.pcBtn');
  var mbBtn   = $('.mbBtn');
  var view    = $('meta[name=viewport]');
    var ua = navigator.userAgent;

  function pcView() {
    var siteWidth = 1020;
    var scale = $(window).width() / siteWidth;
        if(ua.indexOf('iPhone') > 0){
      view.attr("content", 'width='+siteWidth+', initial-scale='+scale+', user-scalable=no, maximum-scale='+scale+', minimum-scale='+scale);
          $(window).load(function(){
           view.attr("content", 'width='+siteWidth+', initial-scale='+scale+', user-scalable=yes, maximum-scale=1.6, minimum-scale=0.25');  
          });
        }else{
          view.attr("content", 'width='+siteWidth+', initial-scale='+scale);
        }
    pcStyle.attr("media","all");
    mbStyle.attr("media","speech");
    $.cookie("mode","pcMode",{expires : 7,path : '/'});
    pcBtn.removeClass('able').addClass('disable');
    mbBtn.removeClass('disable').addClass('able');
    $('.mbBtn2').show();
    $('html,body').animate({ scrollTop: 0 }, 'fast');
  }
  function mbView() {
    view.attr("content", "width=device-width, initial-scale=1.0, user-scalable=no, maximum-scale=1.0, minimum-scale=1.0");
        $(window).load(function(){
          view.attr("content", "width=device-width, initial-scale=1.0, user-scalable=yes, maximum-scale=1.0, minimum-scale=0.25");
        });
    pcStyle.attr("media","speech");
    mbStyle.attr("media","all");
    $.cookie("mode","mbMode",{expires : 7,path : '/'});
    pcBtn.removeClass('disable').addClass('able');
    mbBtn.removeClass('able').addClass('disable');
    $('.mbBtn2').hide();
    $('html,body').animate({ scrollTop: 0 }, 'fast')
    $('.carouNav li a').height(($(".carousel").height())/3-19);
  }

/*
// Navigation link
  var navBtn = $('.mainNav .nav07');
  var navBtn2 = $('.mainNav .nav08');
  var navLink = $('.mainNav .nav07 ul');
  var navLink2 = $('.mainNav .nav08 ul');

// Sub Menu Toggle
  function subMenuToggle() {
    navBtn.on('hover',function(){
      $(this).toggleClass('active');
      navLink.slideToggle(250);
      return false;
    });

    navBtn2.on('hover',function(){
      $(this).toggleClass('active');
      navLink2.slideToggle(250);
      return false;
    });
  }

  function subMenuClickToggle() {
    navBtn.on('click',function(){
      $(this).toggleClass('active');
      navLink.slideToggle(250);
      return false;
    });

    navBtn2.on('click',function(){
      $(this).toggleClass('active');
      navLink2.slideToggle(250);
      return false;
    });
  }


// Toutch Device
  
  if(ua.indexOf('iPhone') > 0 || ua.indexOf('iPod')>0 || (ua.indexOf('iPad')>0) || (ua.indexOf('Android')>0) || ua.indexOf('Windows Phone') > 0 || ua.indexOf('BlackBerry') > 0){

    window.onload = function(){
       setTimeout("scrollTo(0,1)", 50);
    }
    $("body").addClass('tDB');

    subMenuClickToggle();

  } else {

    subMenuToggle();

  }
*/

  if(ua.indexOf('iPhone') > 0 || ua.indexOf('iPod')>0 || (ua.indexOf('Android')>0 && ua.indexOf('Mobile')>0) || ua.indexOf('Windows Phone') > 0 || ua.indexOf('BlackBerry') > 0){

    $('#skip-link').after('<div class="mbBtn2"><a onclick="">スマートフォン表示へ戻す</a></div>')
    mbNav.show();

// Cookie Action

    if($.cookie("mode")=="pcMode"){
      pcView();
    } else {
      mbView();
    }

// Click Action
    pcBtn.on('click',function(){
      pcView();
            setTimeout(function(){location.reload();},500);
    });
    mbBtn.on('click',function(){
      mbView();
            setTimeout(function(){location.reload();},500);
    });
    $(".mbBtn2 a").on('click',function(){
      mbView();
            setTimeout(function(){location.reload();},500);
    });

  } else {

    $('.switch').hide();

  }

  if(ua.indexOf('Mac')){
    $('body').addClass('mac')
  }

});


$(function(){

// Dyrectory Checker
/*
  if(location.pathname != "/") {
    $('.mainNav li a[href^="/' + location.pathname.split("/")[1] + '"]').addClass('current');
  } else $('.mainNav li a:eq(0)').parent().addClass('current');
*/

// Window Open
/*
勝手にtarget削除されている
  $('a').removeAttr('target');
  $('a[href^="http"]').not('[href*="markezine.jp"]').click(function() {
    window.open($(this).attr("href"));
    return false;
  });
*/

// IE Number Counter
  $('.lt-ie8 .numList').each(function(){
    $(this).children('li').each(function(i){
      $(this).attr('class','num'+(i+1));
    });
  });

// Smooth Scroll
  $('.toTop a').on('click',function(){
    var speed = 500;
    var href= $(this).attr("href");
    var target = $(href == "#" || href == "" ? 'html' : href);
    var position = target.offset().top;
    $("html, body").animate({scrollTop:position}, speed, "swing");
    return false;
  });

});


// Author Tabs
$(function(){
  var aTab = $('.authorList > li');
  var aTabCont = $('.authorBox');

  aTab.eq(0).addClass('select');
  aTab.on('click',function(){
    var index = aTab.index(this);

    aTabCont.removeClass('active').eq(index).addClass('active');
    aTab.removeClass('select');
    $(this).addClass('select');
  });
  return false;

});


// Aside Tabs
$(function(){
  var aTab = $('.popularTab li');
  var aTabCont = $('.popularCont ul');

  //if ($.cookie( "popular" ) != '') popular_click($.cookie( "popular" ));

  aTab.on('click',function(event, aindex){
    popular_click(aTab.index(this));
  });

// Aside Navigation

  $('#Category .asideThemeBlock').insertAfter('.specialBlock');
  //$('#Detail aside .pastRankingBlock').remove();  // 2014/06/23 12:39 umi delete
});
function popular_click(index) {
  var aTab = $('.popularTab li');
  var aTabCont = $('.popularCont ul');
  $.cookie( "popular", index );
  aTabCont.removeClass('active').eq(index).addClass('active');
  aTab.removeClass('select');
  aTab.eq(index).addClass('select');
};


$(function(){
  var aTab = $('.asidenewBlockTab li');
  var aTabCont = $('.asidenewBlockCont > div');

  //if ($.cookie( "newtopic" ) != '') newtopic_click($.cookie( "newtopic" ));

  aTab.on('click',function(){
    newtopic_click(aTab.index(this));
  });
});
function newtopic_click(index) {
  var aTab = $('.asidenewBlockTab li');
  var aTabCont = $('.asidenewBlockCont > div');

  $.cookie( "newtopic", index );
  aTabCont.removeClass('active').eq(index).addClass('active');
  aTab.removeClass('select');
  aTab.eq(index).addClass('select');
};


// Narrow Wide Mode Changer
$(function(){
  var login = $('.loginBlock');
  var login01 = $('.login li').eq(0);
  var login02 = $('.login li').eq(1);
  var closeBtn = $('.close');
  var mbNav01 = $('.mbNav01');
  var mbNav02 = $('.mbNav02');
  var mainNav = $('.mainNav');

  var ua = navigator.userAgent;
  var mode;
  if (ua.indexOf('iPad') > 0 || (ua.indexOf('Android') > 0 && ua.indexOf('Mobile') == -1)) {
    mode = "tablet";
  } else if ((ua.indexOf('iPhone') > 0 || ua.indexOf('iPod') > 0 || (ua.indexOf('Android') > 0 && ua.indexOf('Mobile') > 0) || ua.indexOf('Windows Phone') > 0 || ua.indexOf('BlackBerry') > 0) && $.cookie("mode") != "pcMode") {
    mode = "sp";
  } else {
    mode = "pc";
  }

    if ( mode == "sp" ) {

// Login Menu
      mainNav.hide().prependTo('main');
      login.hide().prependTo('main');

    } else {
 
      mainNav.prependTo('nav .container').show();
      login.prependTo('aside').show();

    }

// Main Navigation Toggle
  closeBtn.clone(true).appendTo('.mainNav');
  function subMenuClose() {
    $('.nav07').removeClass('active');
    $('.nav07 ul').hide();
    $('.nav08').removeClass('active');
    $('.nav08 ul').hide();
  }

  mbNav02.on('click',function(){
    if(login.is(':visible')) {
      subMenuClose();
      login.slideToggle(250,'swing',function(){
        mainNav.slideToggle(250,'swing');
      });
    } else {
      subMenuClose();
      mainNav.slideToggle(250,'swing');
   }
  });


// Login Toggle
  mbNav01.on('click',function(){
    if(mainNav.is(':visible')) {
      mainNav.slideToggle(250,'swing',function(){
        login.slideToggle(250,'swing');
      });
    } else {
      login.slideToggle(250,'swing');
   }

  });

  closeBtn.live('click',function(){
    subMenuClose();
    login.slideUp(250,'swing')
    mainNav.slideUp(250,'swing')
  });

});


// Top Caorousel Area
function set_carousel(responsive) {
  var carousel = $(".carousel");

  if (!carousel.size()) return;

  $('.carouNav li').each(function(i) {
    $(this).addClass( 'itm'+i );
  });

  carousel.carouFredSel({
    auto : 4000,
    items : 1,
    height : 'auto',
    responsive : responsive,
    prev : ".prev",
    next : ".next",
    scroll : {
      duration :  500,
      easing :  "linear",
      fx :  "scroll",
      pauseOnHover :  "resume",
      onAfter : function(){
        var pos = $(this).triggerHandler( 'currentPosition' );
        $('.carouNav li').removeClass('selected');
        $('.carouNav li.itm'+pos).addClass('selected');
      }
    },
    swipe : {
      onTouch : true
    }
  });

  $('.carouNav a').click(function() {
    $('.carousel').trigger('slideTo', '#' + this.href.split('#').pop() );
    $('.carouNav li').removeClass('selected');
    $(this).parent().addClass('selected');
    return false;
  });

  var carNav = $('.carouNav');
  var carLi  = $('.carouNav li a');
  var maxHeight = 0;

  if(navigator.userAgent.indexOf('iPad') != -1){
    window.onorientationchange = orientCheck;
    function orientCheck() {
        carLi.height((carousel.height())/3-19);
    };
    orientCheck();
  } else {
    carLi.height((carousel.height())/3-20);
  }

}

$(function(){
  set_carousel(true);
  var carLi  = $('.carouNav li a');
  $(window).on('resize',function(){
    var carousel = $(".carousel");
    var carLi  = $('.carouNav li a');
    if (!carousel.size()) return;

    _height = $('.caroufredsel_wrapper ul li').outerHeight();

    $('.caroufredsel_wrapper, .caroufredsel_wrapper > ul').trigger( 'configuration', ['items.height',_height] );
    carLi.height((carousel.height())/3-20);
  });
  //$(window).trigger('resize');
});

$(function(){
  $('div[rel=term], section[rel=term]').each(function(){
    var me = $(this);
    var start  = new Date(me.attr('start'));
    var end  = new Date(me.attr('end') + ' 23:59:59');
    var today = new Date();
    if (start <= today && end >= today){
    } else {
      me.remove();
    }
  });
});

function subMenu() {
    var ua = navigator.userAgent;
// Navigation link
  var navBtn = $('.nav07');
  var navBtn2 = $('.mainNav .nav08');
  var navLink = $('.mainNav .nav07 ul');
  var navLink2 = $('.mainNav .nav08 ul');

// Sub Menu Toggle
  function subMenuToggle() {
    navBtn.hover(
      function(){
        $(this).addClass('active');
        navLink.slideDown(120);
        return false;
      },
      function(){
        $(this).removeClass('active');
        navLink.slideUp(120);
        return false;
      }
    );

    navBtn2.hover(
      function(){
        $(this).addClass('active');
        navLink2.slideDown(100);
        return false;
      },
      function(){
        $(this).removeClass('active');
        navLink2.slideUp(100);
        return false;
      }
    );
  }

  function subMenuClickToggle() {
    navBtn.click(function(){
      if ($(this).hasClass('active')) {
        $(this).toggleClass('active');
        navLink.slideToggle(250);
        return true;
      } else {
        $(this).toggleClass('active');
        navLink.slideToggle(250);
        return false;
      }
    });

    navBtn2.click(function(){
      if ($(this).hasClass('active')) {
        $(this).toggleClass('active');
        navLink2.slideToggle(250);
        return true;
      } else {
        $(this).toggleClass('active');
        navLink2.slideToggle(250);
        return false;
      }
    });
  }


// Toutch Device
  
  if(ua.indexOf('iPhone') > 0 || ua.indexOf('iPod')>0 || (ua.indexOf('iPad')>0) || (ua.indexOf('Android')>0) || ua.indexOf('Windows Phone') > 0 || ua.indexOf('BlackBerry') > 0){

    window.onload = function(){
       setTimeout("scrollTo(0,1)", 50);
    }
    $("body").addClass('tDB');

    subMenuClickToggle();

  } else {

    subMenuToggle();

  }
}

function get_view_mode() {
  var ua = navigator.userAgent;
  var mode = '';
  if (ua.indexOf('iPad') > 0 || (ua.indexOf('Android') > 0 && ua.indexOf('Mobile') == -1)) {
    mode = "tablet";
    if ($.cookie("mode") == "pcMode") {
      mode = 'pc';
    } else {
      mode = 'tablet';
    }
  } else if ((ua.indexOf('iPhone') > 0 || ua.indexOf('iPod') > 0 || (ua.indexOf('Android') > 0 && ua.indexOf('Mobile') > 0) || ua.indexOf('Windows Phone') > 0 || ua.indexOf('BlackBerry') > 0) && $.cookie("mode") != "pcMode") {
    if ($.cookie("mode") == "pcMode") {
      mode =  'pc';
    } else {
      mode = 'sp';
    }
  } else {
    mode = "pc";
  }

  return mode;
}
