////////////////////////////////////////////////////////////////////////////////module deqwas3.jsm
//// Version 3.1.0
function DeqwasAgent() {
  this.initialize.apply(this,arguments);
}

DeqwasAgent.prototype = {
  codeVersion:         'xxx',
  collectionElementId: 'deqwas-collection',
  screenElementId:     'deqwas-screen',
  application:         'edge',
  methods:             {collect: 'collect', vend: 'vend'},
  products:            {collect: 'any', vend: 'html'},

  initialize: function(site, role, deqwasObject, scene) {
    this.site = site;
    this.role = role;
    this.objectId = deqwasObject.id;

    this.parameters = {
      location:     deqwasObject.location ? deqwasObject.location : location.href,
      scene:        scene,
      viewer:       deqwasObject.viewer_id,
      name:         deqwasObject.name,
      image:        deqwasObject.image,
      value:        deqwasObject.value,
      category:     deqwasObject.category,
      caption:      deqwasObject.caption,
      information:  deqwasObject.info,
      logic:        '',
      place:        '',
      essential:    '',
      expression:   ''
    };

    this.logic = '';
    this.place = '';
    this.essential = '';
    this.exclusion = deqwasObject.option.exclusion;

    if (deqwasObject.option.silence) {
      this.essential = 'confirmation';
    }
    this.noscreen = deqwasObject.option.noscreen;
    if (deqwasObject.option.noscreen || deqwasObject.option.solitude) {
      this.expression =  deqwasObject.option.noscreen ? 'F' : 'X';
      this.expression += deqwasObject.option.solitude ? 'T' : 'X';
    }

    this.baseUrl = location.protocol + '//sv2.deqwas.net/';
    this.method = '';
    this.extension = '.xml';
    this.baseXmlUrl = '';

    this.xslName = 'deqwas.xsl';
    this.xslDirectory = 's';

    this.candidates = [];

    this.iframeStyle = {
      width: '0px',
      height: '0px'
    };
    this.iframeOptions = {
      frameBorder: '0',
      scrolling: 'no'
    };
  },

  getSafeProperty: function(property) {
    return Object.prototype.hasOwnProperty.call(window, property)
      ? window[property] : '' ;
  },

  _setCookie: function(key, val, path, expires) {
    var cookie = [ key +'='+ escape( val ),
                   'path' +'='+ path,
                   'expires' +'='+ expires
                 ].join(';');
    document.cookie = cookie;
  },

  _getCookie: function(key) {
    var cookieKey = key+'=';
    var val = null;
    var cookie = document.cookie+';';
    var index = cookie.indexOf(cookieKey);
    if (index != -1){
      var endIndex = cookie.indexOf(';',index);
      val = unescape(cookie.substring(index+cookieKey.length,endIndex));
    }
    return val;
  },

  deleteCookie: function(key, path) {
    var commingTime = new Date();
    commingTime.setFullYear(commingTime.getFullYear() - 1);
    var expires = commingTime.toGMTString( ) + ';';
    this._setCookie(key, '', path, expires);
  },

  _createRandomString: function() {
    var string = '';
    var charArray = ['1','2','3','4','5','6','7','8'];
    for ( var i = 0 ; i < 18 ; i++ ) {
      string += charArray[Math.floor(Math.random() * charArray.length)];
    }
    return string;
  },

  setViewerCookie: function() {
    var markCookieKey    = 'deqwasMark';
    var markParameterKey = 'mark';

    var markCookieValue = this._getCookie(markCookieKey) || this._createRandomString();

    var time = new Date();
    time.setTime(time.getTime()+(1000*60*60*24*365));
    var expires = time.toGMTString();

    this._setCookie(markCookieKey, markCookieValue, '/', expires);
    this.parameters[markParameterKey] = markCookieValue;
  },

  _createIframeSrc: function(parameters, targetUrl) {
    var parameterArray = [];
    // clean Object.prototype
    for (var parameter in new Object()) {
      parameters[parameter] = undefined;
    }

    for (var key in parameters) {
      if (parameters[key] && typeof parameters[key] != 'function') {
        parameters[key] = (typeof parameters[key] == 'string') ? parameters[key].replace(/[\f\n\r\t\v]/g,'') : parameters[key];
        parameterArray.push(key + '=' + encodeURIComponent(parameters[key]));
      }
    }
    var iframeSrc = targetUrl + '?' + parameterArray.join('&');
    return iframeSrc;
  },

  _createSourceParameter: function(baseXmlUrl, site, role, objectId, extension) {
    return [baseXmlUrl, site, role, objectId + extension].join('/');
  },

  setProduct: function(product) {
    this.product = product;
  },

  setBaseUrl: function(baseUrl) {
    this.baseUrl = baseUrl;
  },

  setExtraParameters: function(extraParameters) {
    for (var key in extraParameters) {
      this.parameters[key] = extraParameters[key];
    }
  },

  setLogic: function(logic) {
    this.logic = logic;
  },

  setPlace: function(place) {
    this.place = place;
  },

  setEssential: function(essential) {
    this.essential = essential;
  },

  setNextCandidateId: function(objectId) {
    this.candidates.push(this._createSourceParameter(
                      this.baseXmlUrl
                      ,this.site
                      ,this.role
                      ,objectId
                      ,this.extension));
  },

  setNextCandidateParameter: function(parameter) {
    this.candidates.push(parameter);
  },

  setExtension: function(extension) {
    this.extension = extension;
  },

  setBaseXmlUrl: function(baseXmlUrl) {
    this.baseXmlUrl = baseXmlUrl;
  },

  setXslName: function(xslName) {
    this.xslName = xslName;
  },

  setXslDirectory: function(xslDirectory) {
    this.xslDirectory = xslDirectory;
  },

  setScreenName: function(screenName) {
    this.screenElementId = [this.screenElementId, screenName].join('-');
  },

  setCollectionElementId: function(elementId) {
    this.collectionElementId = elementId;
  },

  setScreenElementId: function(elementId) {
    this.screenElementId = elementId;
  },

  setIframeStyle: function(width, height, styleObject) {
    if (!width || !height) return false;
    styleObject = styleObject || {};

    this.iframeStyle.width = width;
    this.iframeStyle.height = height;
    for (var key in styleObject) {
      this.iframeStyle[key] = styleObject[key];
    }
  },

  setIframeOptions: function(iframeOptions) {
    if (!iframeOptions) return false;
    for (var key in iframeOptions) {
      this.iframeOptions[key] = iframeOptions[key];
    }
  },

  appendIframeToElement: function() {
    var elementId;
    if (document.getElementById(this.screenElementId)) {
      this.method = this.methods.vend;
    }
    else if (document.getElementById(this.collectionElementId)) {
      this.method = this.methods.collect;
    }
    else {
      return false;
    }

    if (this.noscreen) {
      this.method = this.methods.collect;
    }

    //TODO
    //if (this.exclusion) {
      //symbol = 'x';
      //elementId = this.collectionElementId;
      //this.iframeStyle = {
        //width: '0px',
        //height: '0px'
      //};
    //}
    if (this.method == this.methods.vend) {
      elementId = this.screenElementId;
      this.product = this.products.vend;
      for (var i=0 ; i < this.candidates.length; i++ ) {
        this.parameters['source'+(i+2)] = this.candidates[i];
      }
      this.parameters.xsl = '/' + [this.xslDirectory
                                   ,this.site
                                   ,this.xslName].join('/');
    }
    else {
      elementId = this.collectionElementId;
      this.product = this.product ? this.product : this.products.collect;
      this.iframeStyle = {
        width: '0px',
        height: '0px'
      };
    }

    this.parameters.logic = this.logic;
    this.parameters.place = this.place;
    this.parameters.essential = this.essential;
    this.parameters.expression = this.expression;

    var iframeName = 'deqwas_'+this.method;
    var iframe = document.createElement('iframe');
    var targetUrl = this.baseUrl + [this.application, this.site, this.product, this.role, this.objectId, this.method].join('/');
    iframe.src = this._createIframeSrc(this.parameters, targetUrl);
    var iframeStyleString = '';
    for (var key in this.iframeStyle) {
      iframeStyleString += [key, ':', this.iframeStyle[key], ';'].join(' ');
    }
    iframe.style.cssText = iframeStyleString;
    for (var attr in this.iframeOptions) {
      iframe[attr] = this.iframeOptions[attr];
    }
    iframe.name = iframeName;

    //TODO check exist iframe
    document.getElementById(elementId).appendChild(iframe);
  },
  
  appendIframeToElement4Dex: function() {
    var elementId = this.collectionElementId;
    var iframeName = 'deqwas4dex';
    var iframe = document.createElement('iframe');
    var targetUrl = ('https:' == document.location.protocol ? 'https:' : 'http:') + '//dex00.deqwas.net/common/collectionx.aspx';
    iframe.src = this._createIframeSrc(this.parameters, targetUrl);
    var iframeStyleString = '';
    for (var key in this.iframeStyle) {
      iframeStyleString += [key, ':', this.iframeStyle[key], ';'].join(' ');
    }
    iframe.style.cssText = iframeStyleString;
    for (var attr in this.iframeOptions) {
      iframe[attr] = this.iframeOptions[attr];
    }
    iframe.name = iframeName;

    document.getElementById(elementId).appendChild(iframe);
  }
};

////////////////////////////////////////////////////////////////////////////////application x.js
(function(){
    var deqwasAgent = new DeqwasAgent("codezine", "item", deqwas, "view");
    deqwasAgent.setViewerCookie();
    deqwasAgent.setExtraParameters({
        sub01 : deqwas.serial,
        sub02 : deqwas.tag,
        sub03 : deqwas.author
    });
    deqwasAgent.setProduct('xml');
    deqwasAgent.appendIframeToElement();
})();

(function(){
    var item_id;
    item_id = deqwas.id;
    var deqwasAgentA = new DeqwasAgent("codezine", "item" , deqwas);
    deqwasAgentA.setExtraParameters({
        cid: 'codezine'
    });
    deqwasAgentA.appendIframeToElement4Dex('i');
})();