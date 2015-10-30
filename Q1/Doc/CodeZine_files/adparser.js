
var ads_node = new Array(); //Node
var ads_hs_node = new Array(); //Node
var ads_mode = 1; //1:im 2:ss
var impApid = Math.round(Math.random() * 100000000);

/*
 * 広告を表示します 
 * <adv:ov 
 *   count="3" 
 *   type="2" 
 *   area="right" 
 *   rowcss="interestmatch" 
 *   titleclass="ov_title" 
 *   descclass="ov_desc"
 *   hostclass="host" 
 *   isfooter="1" 
 *   />
 * do_adv_parsing を呼んだ後読んでください。
 *
*/
function adv_write(){
  
  //////// ---------IM / SS ------ ////////

  if(typeof(zSr) == "undefined"){
    return ;
  }
  zSr.splice(0,6);
  
  
  //click パラメータ
  var paths = location.pathname.split("/");
  var path  = (paths.length > 1 ? paths[1] : "top");
  if (location.pathname.indexOf("search") == 0){
    path = "search";
  }
  var oreg  = new RegExp("^[a-z]+$");
  if (!path.match(oreg)){
    path = (paths.length > 2 ? paths[2] : "top");
  }
  if (path.indexOf('%') != -1){
    path = "local";
  }
  
  
  for (var i = ads_node.length -1 ; i >= 0 ; i-- ) {
    var html = '';
    
    var type        = ads_node[i].attr('type');         //表示タイプ
    var title_class = ads_node[i].attr('titleclass');   //タイトルクラス名
    var all_class   = ads_node[i].attr('rowcss');       //行ごとのクラス名
    var desc_class  = ads_node[i].attr('descclass');    //詳細のクラス名
    var host_class  = ads_node[i].attr('hostclass');    //ホストのクラス名
    var area        = ads_node[i].attr('area');         //Clickパラメタ
    var isfooter    = ads_node[i].attr('isfooter');     //インタレストマッチのフッタの有無

    var hit = 0;
    for (var k = ads_node.length -1 ; k >= 0 ; k-- ){
      if (area == ads_node[k].attr('area')){
        hit++;
      }
    }
    if(hit > 1){
      hit = 0;
      for (var k = ads_node.length -1 ; k >= i ; k-- ){
        if (area == ads_node[k].attr('area')){
          hit++;
        }
      }
      area = area + "-" + hit;
    }

    for (var count = ads_node[i].attr('count') -1 ; count >= 0 ; count--){
      var descr = zSr.shift();
      var unused1 = zSr.shift();
      var clickURL = zSr.shift();
      var title = zSr.shift();
      var sitehost = zSr.shift();
      var unused2 = zSr.shift();
      if (title == undefined){
        break;
      }
      html += "<div class ='" + all_class + "'>";
      html += '<a class="ad" href="' + clickURL + '"';
      html += 'onclick="ov_click_count(location.href,\'' ;
      html += path + area +  '\',\'' + title + '\');" >';
      
      //1行
      if (type == 1) {
        html +=  "<span class ='" + title_class + "'>" + title + '</span>';
        html +=  "<span class ='" + desc_class + "'>" + descr + '</span>';
      }
      //2行(詳細横にホスト名)
      if (type == 2) {
        html +=  "<span class ='" + title_class + "'>" + title + '</span>';
        html +=  "<span class ='" + desc_class + "'>" + descr ;
        html +=  "<span class ='" + host_class + "'>" + sitehost + '</span>'+ '</span>';
      }
      //2行(タイトル横にホスト名)
      if (type == 3) {
        html += "<span class ='" + title_class + "'>" + title + '</span>';
        html += "<span class ='" + host_class + "'>" + sitehost + '</span>';
        html += "<span class ='" + desc_class + "'>" + descr + '</span>';
      }
      
      html += '</a></div>';
      
    }
    
    //フッタがいるなら
    if (isfooter == 1 && ads_mode == 1) {
      html +=  '<div align="right" class="interest_footer"><a href="http://ov.yahoo.co.jp/service/int/index.html?o=IM0028"  target="_blank">インタレストマッチ</a></div>';
    } else {
      if(location.pathname.indexOf("/article/detail/") != -1){
        html += '<div align="right" class="interest_footer"><img src="/static/common/images/pr.gif"></div>';
      }
    }
    
    //親オブジェクトに広告書き込み
    if(navigator.userAgent.indexOf("Opera") != -1){
      var div_id = ads_node[i][0].parentNode.id;
      var element = document.getElementById(div_id); 
      var newElement = document.createElement("div"); 
      newElement.innerHTML = html; 
      element.insertBefore(newElement,element.firstChild);
    } else{
      ads_node[i][0].parentNode.innerHTML = html ;
    }
    if (navigator.userAgent.indexOf("MSIE")!=-1){
      //ads_node[i][0].parentNode.innerHTML += ""; //for IE
    }
    //<adv:ov> タグ削除
    //ads_node[i].remove();
  }
  
  //////// ---------HotSpot ------ ////////

  var ctxtids = Array("管理職 研修","管理職 心得","管理職 求人","管理職 年収",
                    "管理職 悩み","中間管理職","マインドマネージャー","ITILマネージャー",
                    "システムマネージャー","プロダクトマネージャー","メンタルヘルスマネジメント",
                    "モチベーションマネジメント","ISOマネジメント","リスクマネジメント",
                    "マネジメント研修","マネジメントゲーム","マネジメントスキル","リーダーシップ",
                    "リーダーシップスキル","リーダーシップ 研修","フリーエンジニア","システムエンジニア",
                    "データベースエンジニア","ネットワークエンジニア","エンジニア転職","ウェブデザイナー",
                    "DTPデザイナー","デザイナー転職","キーワードマーケティング","PPCマーケティング",
                    "Googleマーケティング","プロダクトマーケティング","エモーショナルマーケティング",
                    "ダイレクトマーケティング","マーケティングリサーチャー","満足度調査","海外SEO",
                    "モバイルSEO","SEOソフト","SEO価格","SEO裏技","SEO 被リンク","SEO 内部対策",
                    "eコマース SEO","アクセス解析","IT関連 資格","IT業界 資格","無料セミナー",
                    "スキルアップセミナー","FXセミナー","NLPセミナー","マネーセミナー","ビジネスマナー セミナー",
                    "Linux セミナー","ITIL セミナー","WEB広告","広告代理店 一覧","無料広告",
                    "動画広告","広告制作会社","WEB製作会社","広告業界 転職","FLASH 制作会社",
                    "ロゴ制作","モバイルアフィリエイト","派遣会社 一覧","eラーニング研修",
                    "企画書","プレゼンテーション");
  
  for (var i = ads_hs_node.length -1 ; i >= 0 ; i-- ) {
    var html        = '';
    var ref    = path + 'keyword';    
    var type        = ads_hs_node[i].attr('type');         //表示タイプ
    var all_class   = ads_hs_node[i].attr('rowcss');       //行ごとのクラス名
    
    for (var count = ads_hs_node[i].attr('count') -1 ; count >= 0 ; count--){
      var randindex  = Math.floor( Math.random() * ctxtids.length );
      var s           = ctxtids[randindex];
      ctxtids.splice(randindex,1);
      
      //2行
      if (type == 2 && (ads_hs_node[i].attr('count') / 2) ==  (count + 1)  ){
        html += '</div>';
        html += '<div class="hs_2col">';
      }
      if (type == 2 && count == ads_hs_node[i].attr('count') -1 ){
        html += '<div class="hs_2col">';
      }
      
      html +=
        '<div class="' + all_class + '"><a href="http://search.markezine.jp/sponsor/?Keywords=' + 
        encodeURI(s) + '&ref=' + ref + '" ' + 
        'onclick="ov_click_count(location.href,\'' + ref + '\',\'' + s + '\')"' + 
        'target="_blank">' + s + '</a></div>';
      
    }
    //親オブジェクトに広告書き込み
    if(navigator.userAgent.indexOf("Opera") != -1){
      var div_id = ads_hs_node[i][0].parentNode.id;
      var element = document.getElementById(div_id); 
      var newElement = document.createElement("div"); 
      newElement.innerHTML = html; 
      element.insertBefore(newElement,element.firstChild);
    } else {
      ads_hs_node[i][0].parentNode.innerHTML = html ;
    }
    if (navigator.userAgent.indexOf("MSIE")!=-1){
      //ads_hs_node[i][0].parentNode.innerHTML += ""; //for IE
    }
    //<adv:hs> タグ削除
    //ads_hs_node[i].remove();
    
  }
  
  
}
/*
 *  広告初期処理
 *  Footer で呼んでください。
 *  
 *
 *
*/
function do_adv_parsing( element ) {

  //////// ---------IM / SS ------ ////////
  
  //-- Keyword があれば SS になる ---
  //1ページにIMとSSが一緒になることはない
  var keyword = '';
  if(location.pathname.indexOf("/search") != -1){
    keyword = get_param_keyword(location.search);
  }
  if(location.pathname.indexOf("/article/tag/") != -1){
    var metas = document.getElementsByTagName("meta");
    for(var i = 0 ; i < metas.length ; i++ ){
      if (metas[i].name == "keywords"){
        keyword = metas[i].content;
        break;
      }
    }
  }
  if(location.pathname.indexOf("/article/detail/") != -1){
    var matchKeywords = Array("マーケティング","SEO","アクセス解析","ネット広告","広報",
                      "PR","リサーチ","ユーザビリティ","ウェブ製作","携帯サイト","広告代理店",
                      "コンサルティング","電子マネー","ベンチャー","CMS","動画配信","広告効果測定",
                      "メール配信","検索連動型広告",
                      "ウェブデザイナー","ビジネス英会話","都心マンション","英会話 上達","タワーマンション","新築マンション",
                      "英会話","中小企業診断士","通信講座","テープお越し","TOEIC 高得点","中国語","外資系転職",
                      "英会話レッスン","ヘッドハント","おしゃれな家","お手軽エステ","ホワイトニング");
    
    var metas = document.getElementsByTagName("meta");
    for(var i = 0 ; i < metas.length ; i++ ){
      if (metas[i].name == "keywords"){
        var metakeywords = metas[i].content.split(',');
        for(var ii = 0 ; ii < matchKeywords.length ; ii++ ){
            if (matchKeywords[ii].indexOf(metakeywords) != -1 ){
                keyword = metakeywords[ii];
                break;
            }
        }
        break;
      }
    }
  }



  var includes = adv_get_subelements_by_name(element, 'adv:ov'); 
  var includes_total = includes.length+1;
  var includes_ad_count = 0;

  //広告数取得およびNode取得 :includeの競合排除
  for (var i = includes.length -1 ; i >= 0 ; i-- ) {
    var incl = $(includes[i]);
    var count = incl.attr('count');
    includes_ad_count = eval(includes_ad_count) + eval(count);
    ads_node.push(incl);
  }
  
  var paths = location.pathname.split("/");
  var path  = (paths.length > 2 ? paths[1] : "top");
  var oreg  = new RegExp("^[a-z]+$");
  if (!path.match(oreg)){
    path = (paths.length > 3 ? paths[2] : "top");
  }
  if (path.indexOf('%') != -1){
    path = "local";
  }
  
  path = encodeURIComponent(path);
  var config  = "23228933181";
  var source  = "ecnavi_jp_markezine_cat_ctxt";
  var ctxtUrl = encodeURIComponent(location.protocol + "//" + location.host + location.pathname);
  
  var ctxtids = Array("boo0102","boo0105","sof0100","sof0200","ele0200",
                      "ele0300","spo0200","hea0101","hea0102","hea0204",
                      "hea0400","car0200","edu0400","ent0300","fin0100",
                      "tra0000","com0100","com0200","com0300","com0400",
                      "com0500","com0600","com0800","com0900","com1001",
                      "com1003","rea0103","rea0301","pro1100","pro1200");
  
  var ctxtid  = ctxtids[Math.floor( Math.random() * ctxtids.length )];
  var type    = "im_mz_" + path + "_" + ctxtid;

  // keyrowd がないので IM
  if (keyword == '' || typeof keyword =="undefined"){
    document.write('<s' + 'cript type="text/javascript" language="javascript" src="http://im.ecnavi.ov.yahoo.co.jp/js_flat/?source=' 
                    + source + '&type=' + type + '&ctxtId=' + ctxtid + '&maxCount=' 
                    + includes_ad_count + '&outputCharEnc=utf8&ctxtUrl=' + ctxtUrl + '"></s' + 'cript>');
    ads_mode = 1;
  // keyrowd があるので SS
  } else {
      document.write('<s' + 'cript type="text/javascript" language="javascript" src="/search/xml_overture?keywords=' 
                      + encodeURI(keyword) + '&maxcount=' + includes_ad_count + '&ua=' 
                      + escape(escape(navigator.userAgent)) + '"></s' + 'cript>');
      ads_mode = 2;
  }


  //////// ---------HotSpot ------ ////////
  includes = adv_get_subelements_by_name(element, 'adv:hs'); 
  includes_total = includes.length+1;
  includes_ad_count = 0;

  //広告数取得およびNode取得
  for (var i = includes.length -1 ; i >= 0 ; i-- ) {
    var incl = $(includes[i]);
    var count = incl.attr('count');
    includes_ad_count = eval(includes_ad_count) + eval(count);
    ads_hs_node.push(incl);
  }


}

function adv_get_subelements_by_name(element,elementname) {
  var found = new Array();
  elementname = elementname.toLowerCase();
  if (element.nodeType == 9 || element.nodeType == 1) {
    var children = element.childNodes;
    for (var i = 0; i < children.length ; i++ ) {
      var elem = children[i];
      if (elem.nodeType == 1) {
        var tagname = elem.tagName.toLowerCase();
        if (tagname == elementname) {
          found.push(element.childNodes[i]);
        }
        if ( elem.childNodes.length > 0) {
          var res = adv_get_subelements_by_name(elem,elementname);
          found = found.concat(res);
        }
        
      }
    }
  }
  return found;
}

function get_param_keyword(str){
  var dec = decodeURIComponent;
  var par = new Array, itm;
  if(typeof(str) == 'undefined') return par;
  if(str.indexOf('?', 0) > -1) str = str.split('?')[1];
  str = str.split('&');
  for(var i = 0; str.length > i; i++){
    itm = str[i].split("=");
    if(itm[0] == 'q'){
      return typeof(itm[1]) == 'undefined' ? true : dec(itm[1]);
    }
  }
  
}

// create flash control
function CreateFlash(flashid, srcpath, width, height, cssclass) {
  var flash_tag = '<object classid="clsid:d27cdb6e-ae6d-11cf-96b8-444553540000" codebase="http://fpdownload.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=7,0,0,0" width="'+width+'" height="'+height+'" id="'+flashid+'" align="middle">'+
  '<param name="allowScriptAccess" value="sameDomain" />' +
  '<param name="movie" value="'+srcpath+'" />' +
  '<param name="quality" value="high" />' +
  '<embed src="'+srcpath+'" quality="high" width="'+width+'" height="'+height+'" name="'+flashid+'" align="middle" allowScriptAccess="sameDomain" type="application/x-shockwave-flash" pluginspage="http://www.macromedia.com/go/getflashplayer" />'+
  '</object>';
  document.write('<div class="'+cssclass+'">' + flash_tag + '</div>');
}

//-----------------------------------------------------------------------------
// impAct AD CODE
function impA_rotate(list) {
  var i = list.length;
  while (--i) {
  var j = Math.floor(Math.random() * (i + 1));
  if (i == j) continue;
  var k = list[i]; list[i] = list[j]; list[j] = k;
  }
  for ( i = 0; i <= list.length; i++){ if ( list[i] == "" ) list.splice(i,1);}
  return list;
}
//-----------------------------------------------------------------------------
// overture AD CODE
var ov_ads = new Array();
var zSr_i = 6;

var OvParts = function (ad_count, add_id, css_class) {
  this.ad_count = ad_count
  this.add_id = add_id
  this.css_class = css_class
}


// ホットスポット置換
function replace_hotspot(parag, hs_words) {

  var re_words = new RegExp(hs_words.join("|"), "ig");
  // 重複置換チェック用
  var save_words = [];

  parag.each( function() { 
    $(this).contents().each( function() {
      // テキストのみ置換
      if ($(this)[0].nodeName == '#text') {
        var html = $(this)[0].nodeValue;
        html = html.replace(re_words, function(keyword, subkey) {
          if (keyword in save_words) {
            return keyword;
          } else {
            save_words[keyword]=1;
            var hs_link='http://search.markezine.jp/sponsor/?Keywords='+encodeURIComponent(keyword)+'&ref=articleinner';
            return '<a href="'+hs_link+'" title="「'+keyword+'」を検索" class="hslink">'+keyword+'</a>';
          }
        });
        
        // ノード置換のためspanタグにする
        var span = document.createElement('span'); 
        span.innerHTML = html;

        $(this).replaceWith(span);
      }
    })
  })
}

// auto hotspot pでテキストのみの箇所だけ置換
/*
$('#article').ready(function(){
  if ($('#article').html() == null) return;
  // only news
  if (!$('#head_news>a').text().match(/MarkeZineニュース/)) return;

  var hs_words=['カメラ','市場','SEO','広告','マーケティング','求人','英語','就職','給料','給与','高給'];
  
  var p_led = $('#lead>p');
  var p_art = $('#article>div>p');
  if (p_art.length==0) {
    p_art = $('#article>p');
  }
  
  replace_hotspot(p_led, hs_words);
  replace_hotspot(p_art, hs_words);
});
*/
