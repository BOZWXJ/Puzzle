if (!Array.prototype.indexOf) {
    Array.prototype.indexOf = function (searchElement /*, fromIndex */ ) {
        'use strict';
        if (this == null) {
            throw new TypeError();
        }
        var n, k, t = Object(this),
            len = t.length >>> 0;

        if (len === 0) {
            return -1;
        }
        n = 0;
        if (arguments.length > 1) {
            n = Number(arguments[1]);
            if (n != n) { // shortcut for verifying if it's NaN
                n = 0;
            } else if (n != 0 && n != Infinity && n != -Infinity) {
                n = (n > 0 || -1) * Math.floor(Math.abs(n));
            }
        }
        if (n >= len) {
            return -1;
        }
        for (k = n >= 0 ? n : Math.max(len - Math.abs(n), 0); k < len; k++) {
            if (k in t && t[k] === searchElement) {
                return k;
            }
        }
        return -1;
    };
}

(function ($) {
  
  
  $.GoogleAnalytics = function (options) {
    options = $.extend({}, $.GoogleAnalytics.options, options);
    for (var key in options) {
      if (options.hasOwnProperty(key) && typeof $.GoogleAnalytics[key] === "function") {
        $.GoogleAnalytics[key]();
      }
    }
  };

  $.GoogleAnalytics.options = {
    clickLinks: true,
    clickSocial: true
  };

  $.GoogleAnalytics.clickLinks = function () {
    $('a').on('click', function (event) {
      var $this = $(this);
      var href = ($this.prop('href') || '').split('?')[0];
      var ext = href.split('.').pop();
      var target = $this.attr('target') || '';

      if (!href.match(/^javascript:/i)) {
          // mailto
          if (href.toLowerCase().indexOf('mailto:') === 0) {
            ga('send', 'event', 'mailto', href.substr(7));
          }
          // tel
          if (href.toLowerCase().indexOf('tel:') === 0) {
            ga('send', 'event', 'tel', href.substr(7));
          }
          // download
          if ('xls,xlsx,doc,docx,ppt,pptx,pdf,txt,zip,rar,exe,wma,flv,mov,avi,wmv,mp3,csv,tsv'.split(',').indexOf(ext.toLowerCase()) != -1) {
            ga('send', 'event', 'download', ext, href);
          
          // move internal site
          } else if ((this.protocol === 'http:' || this.protocol === 'https:') && this.hostname.indexOf(document.location.hostname) != -1) {
            if (target == '_blank'){
              ga('send', 'event', 'internal', this.hostname, this.pathname,{
                'hitCallback' : function(){}
              });
            } else {
              ga('send', 'event', 'internal', this.hostname, this.pathname);
            }
          // move external site
          } else if ((this.protocol === 'http:' || this.protocol === 'https:') && this.hostname.indexOf(document.location.hostname) ==  -1) {
            ga('send', 'event', 'external', this.hostname, href,{
                'hitCallback' : function(){}
            });
            ga('send', 'event', 'from-external', location.pathname, href);
          }
      }
      return event.result;
    });
  };

  // facebook
  function clickFacebook() {
    try {
      FB.Event.subscribe("edge.create", function (opt_target) {
        ga('send', 'social', 'facebook', 'like', opt_target);
      });
      FB.Event.subscribe("edge.remove", function (opt_target) {
        ga('send', 'social', 'facebook', 'unlike', opt_target);
      });
      FB.Event.subscribe("message.send", function (opt_target) {
        ga('send', 'social', 'facebook', 'send', opt_target);
      });
      
    } catch (e) {}
  }
  $.GoogleAnalytics.clickFacebook = clickFacebook;

  // Twitter
  function clickTwitter() {
    function trackTwitterHandler(intent_event) {
      if (intent_event && intent_event.type === "tweet" || intent_event.type === "click") {
        var socialAction = intent_event.type + (intent_event.type === "click" ? "-" + intent_event.region : "");
        ga('send', 'social', 'twitter', socialAction);
      }
    }

    try {
      twttr.events.bind("click", trackTwitterHandler);
      twttr.events.bind("tweet", trackTwitterHandler);
    } catch (e) {}
  }
  $.GoogleAnalytics.clickTwitter = clickTwitter;

  $.GoogleAnalytics.clickSocial = function (waitInSeconds) {
    $(window).load(function () {
      setTimeout(function () {
        clickFacebook();
        clickTwitter();
      }, waitInSeconds || 4000);
    });
  };

}(jQuery));

jQuery(function($) {
  $.GoogleAnalytics();
});

window.googlePlus = function (data) {
  //ga('send', 'social', 'googleplus');
}
