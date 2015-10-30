var gptadslots=[];
(function(){
  var useSSL = 'https:' == document.location.protocol;
  var src = (useSSL ? 'https:' : 'http:') + '//www.googletagservices.com/tag/js/gpt.js';
  document.write('<scr' + 'ipt src="' + src + '"></scr' + 'ipt>');
})();


function init_dfp(){
googletag.cmd.push(function() {
  // pc
  googletag.defineSlot('/5473248/CZ_PT_1', [145, 49], 'div-gpt-ad-1393229197983-0').addService(googletag.pubads());
  googletag.defineSlot('/5473248/CZ_PI_1', [145, 50], 'div-gpt-ad-1393228911687-0').addService(googletag.pubads());
  googletag.defineSlot('/5473248/CZ_PT_2', [145, 49], 'div-gpt-ad-1393229236273-0').addService(googletag.pubads());
  googletag.defineSlot('/5473248/CZ_PI_2', [145, 50], 'div-gpt-ad-1393228961100-0').addService(googletag.pubads());
  googletag.defineSlot('/5473248/CZ_PT_3', [145, 49], 'div-gpt-ad-1393229358759-0').addService(googletag.pubads());
  googletag.defineSlot('/5473248/CZ_PI_3', [145, 50], 'div-gpt-ad-1393228984749-0').addService(googletag.pubads());
  googletag.defineSlot('/5473248/CZ_PT_4', [145, 49], 'div-gpt-ad-1393229391220-0').addService(googletag.pubads());
  googletag.defineSlot('/5473248/CZ_PI_4', [145, 50], 'div-gpt-ad-1393229016288-0').addService(googletag.pubads());
  googletag.defineSlot('/5473248/CZ_PT_5', [145, 49], 'div-gpt-ad-1393229428709-0').addService(googletag.pubads());
  googletag.defineSlot('/5473248/CZ_PI_5', [145, 50], 'div-gpt-ad-1393229038023-0').addService(googletag.pubads());
  googletag.defineSlot('/5473248/CZ_PT_6', [145, 49], 'div-gpt-ad-1393229451841-0').addService(googletag.pubads());
  googletag.defineSlot('/5473248/CZ_PI_6', [145, 50], 'div-gpt-ad-1393229061793-0').addService(googletag.pubads());
  googletag.defineSlot('/5473248/CZ_PT_7', [145, 49], 'div-gpt-ad-1393229480634-0').addService(googletag.pubads());
  googletag.defineSlot('/5473248/CZ_PI_7', [145, 50], 'div-gpt-ad-1393229085168-0').addService(googletag.pubads());
  googletag.defineSlot('/5473248/CZ_PT_8', [145, 49], 'div-gpt-ad-1393229509372-0').addService(googletag.pubads());
  googletag.defineSlot('/5473248/CZ_PI_8', [145, 50], 'div-gpt-ad-1393229108826-0').addService(googletag.pubads());
  googletag.defineSlot('/5473248/CZ_PT_9', [145, 49], 'div-gpt-ad-1393229543720-0').addService(googletag.pubads());
  googletag.defineSlot('/5473248/CZ_PI_9', [145, 50], 'div-gpt-ad-1393229134807-0').addService(googletag.pubads());
  googletag.defineSlot('/5473248/CZ_PT_10', [145, 49], 'div-gpt-ad-1393229567441-0').addService(googletag.pubads());
  googletag.defineSlot('/5473248/CZ_PI_10', [145, 50], 'div-gpt-ad-1393229157020-0').addService(googletag.pubads());


  if (location.pathname == '/test/ad/') {
  } else {
  }
 var _UA = navigator.userAgent;
  if (_UA.indexOf('iPhone') > 0 || _UA.indexOf('iPod') > 0 || _UA.indexOf('iPad') > 0 || _UA.indexOf('Android') > 0) {
  } else {
  }
  //var width = window.innerWidth || document.documentElement.clientWidth;
  //if (width >= 768) {
  //    googletag.defineSlot('/5473248/MZ_AUB_Main1_428x60', [428, 60], 'div-gpt-ad-1391149162268-0').addService(googletag.pubads());
  //}
  googletag.pubads().collapseEmptyDivs(true);
  googletag.pubads().enableSingleRequest();
  googletag.pubads().enableSyncRendering();
  googletag.enableServices();
  });
}

function dfp_shuffle_array(array) {
  var n = array.length, t, i;

  while (n) {
    i = Math.floor(Math.random() * n--);
    t = array[n];
    array[n] = array[i];
    array[i] = t;
  }

  return array;
}

function dfp_ul_random_visible (id) {
  var arr = [];
  var ele = $(id);
  var flag = 0;
  ele.find("li").each(function() {
    $(this).find('div[id^="div-gpt-ad-"]').each(function() {
      if ($(this).html() != '') {
        arr.push($(this).html());
        flag++;
      }
    });  
  });  
  arr.sort(function() {
    return Math.random() - Math.random();
  });

  var parent_block = ele.parent();
  if (flag) {
    ele.empty();
    for( i = 0; i < arr.length; i++ ) {
      ele.append('<li>' + arr[i] + '</li>');
    };
    if (!parent_block.is(':visible')) parent_block.show();
  } else {
    if (parent_block.is(':visible')) parent_block.hide();
  }
}

function dfp_set_responsive (gpt_id, banner_width) {
  $(window).resize(function(){
    dfp_change_scale(gpt_id, banner_width);
  });
  $(function(){
    $(gpt_id).trigger('resize');
  });
}

function dfp_change_scale (gpt_id, banner_width) {
  var width = document.documentElement.clientWidth;
  var scale;
  var min_scale   = 0.2;
  var coefficient = 0.9;
  if (width >= 1024) {
    scale = 1;
  } else {
    if (!banner_width) banner_width = 768;
    scale = width / banner_width;
    if (coefficient) scale = scale * coefficient;
    if (min_scale) scale = scale < min_scale ? min_scale : scale;
  }

  $(gpt_id)
   .css('transform-origin',  '0px 50%')
   .css('transform',         'scale(' + scale + ')')
   .css('-o-transform',      'scale(' + scale + ')')
   .css('-webkit-transform', 'scale(' + scale + ')')
   .css('-moz-transform',    'scale(' + scale + ')')
   .css('-ms-transform',     'scale(' + scale + ')');
}
