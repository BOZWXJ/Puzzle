/* Beagle2.0 JS 20150220 */
if(typeof(adingoBeagle)=="undefined"){
    adingoBeagle = new Array();
    adingoBeagle.showCount = 1;
    adingoBeagle.outputCount = 0;
    adingoBeagle.maxCount = 3;
    adingoBeagle.loglyliftSiteId = '';
    adingoBeagle.deliveryType = '';
    adingoBeagle.loglyServerError = '0';
    adingoBeagle.loglylift_ad_num = 0;
    adingoBeagle.maxFloorPrice = 200;
    adingoBeagle.floorPrice = '';
    
    adingoBeagle.titleLength = 28;
    adingoBeagle.leadLength = 37;
    adingoBeagle.siteLength = 34;
    adingoBeagle.leader = '...';
    
    adingoBeagle.ctxtKeyword = null;
    if(typeof(adingoBeagleCtxtKeyword)!="undefined"){
		adingoBeagle.ctxtKeyword = adingoBeagleCtxtKeyword;
	}
    
    Beagle_zSr = [];
    adingoBeagle.adingoAdsRequest = function(){
        if(typeof(adingoBeagle.showCount)!="undefined" && adingoBeagle.outputCount!="undefined"){
            if(adingoBeagle.maxCount!=0 && (adingoBeagle.deliveryType == '2' || adingoBeagle.deliveryType == '3')){
				if((adingoBeagle.loglyServerError == 0 && adingoBeagle.deliveryType == '2' && Number(adingoBeagle.floorPrice) <= adingoBeagle.maxFloorPrice) || adingoBeagle.deliveryType == '3'){
document.write('<script type="text/javascript" src="http://liftapi.logly.co.jp/lift.json?p_id=69&site=_def&url='+encodeURIComponent(document.URL)+'&ref='+encodeURIComponent(document.referrer)+'&ad_count=3&media_type=&ip=106.130.163.193&callback=adingoBeagle.loglyliftCallback&floor_price=&ua='+encodeURIComponent(window.navigator.userAgent)+'&bcat='+encodeURIComponent('')+'&badv='+encodeURIComponent('')+'" charset="UTF-8"><\/script>');
				}else{
					if(adingoBeagle.deliveryType == '2'){
						adingoBeagle.adingoYDNRequest();
					}
				}
            }else{
				if(adingoBeagle.deliveryType == '4' && adingoBeagle.maxCount!=0){
					adingoBeagle.adingoYDNRequestAPP();
				}else if(adingoBeagle.maxCount!=0){
					adingoBeagle.adingoYDNRequest();
				}
			}
        }
    }

    adingoBeagle.showListing = function(id){
        if(typeof(adingoBeagle["showAdingoBeagle"+id])=="undefined") return;
		adingoBeagle["showAdingoBeagle"+id](id);
    }
    
    adingoBeagle.callback = function(zSrJson){
        if(typeof(zSrJson["Results"]["ResultSet"]["numResults"])=="undefined") return;
        var count = zSrJson["Results"]["ResultSet"]["numResults"];
        if (count == 0) return;
        for (var i = 0; i < 6; i++) Beagle_zSr[i] = "";
        Beagle_zSr[0] = zSrJson["Results"]["ResultSet"]["Label"]["InquiryUrl"];
        Beagle_zSr[1] = zSrJson["Results"]["ResultSet"]["Label"]["LabelText"];
        
        var j = 6;
        for(var i = 0; i < zSrJson["Results"]["ResultSet"]["numResults"]; i++){
            Beagle_zSr[j] = zSrJson["Results"]["ResultSet"]["Listing"][i]["description"];
            Beagle_zSr[j+1] = "";
            Beagle_zSr[j+2] = zSrJson["Results"]["ResultSet"]["Listing"][i]["ClickUrl"];
            Beagle_zSr[j+3] = zSrJson["Results"]["ResultSet"]["Listing"][i]["title"];
            Beagle_zSr[j+4] = zSrJson["Results"]["ResultSet"]["Listing"][i]["siteHost"];
            Beagle_zSr[j+5] = "";
            j = j + 6;
        }
    }

    adingoBeagle.loglyliftCallback = function(zSrJson){
		if(typeof(zSrJson)!="undefined"){
            adingoBeagle.loglylift_ad_num = zSrJson.length;

	        for (var i = 0; i < 6; i++) Beagle_zSr[i] = "";
	        Beagle_zSr[0] = 'https://www.logly.co.jp/privacy.html';
	        Beagle_zSr[1] = 'Sponsored';
	        var j = 6;
	        for(var i = 0; i < adingoBeagle.loglylift_ad_num; i++){
				if(zSrJson[i]["title"].length > adingoBeagle.titleLength){
					zSrJson[i]["title"] = zSrJson[i]["title"].substr(0, adingoBeagle.titleLength) + adingoBeagle.leader;
				}
				if(zSrJson[i]["lead"].length > adingoBeagle.leadLength){
					zSrJson[i]["lead"] = zSrJson[i]["lead"].substr(0, adingoBeagle.leadLength) + adingoBeagle.leader;
				}
				if(zSrJson[i]["site"].length > adingoBeagle.siteLength){
					zSrJson[i]["site"] = zSrJson[i]["site"].substr(0, adingoBeagle.siteLength) + adingoBeagle.leader;
				}
	            Beagle_zSr[j] = zSrJson[i]["lead"];
	            Beagle_zSr[j+1] = zSrJson[i]["beacon_url"];
	            Beagle_zSr[j+2] = zSrJson[i]["url"];
	            Beagle_zSr[j+3] = zSrJson[i]["title"];
	            Beagle_zSr[j+4] = zSrJson[i]["site"];
	            Beagle_zSr[j+5] = "";
	            j = j + 6;
	        }
		}
		if(adingoBeagle.loglylift_ad_num < adingoBeagle.maxCount && adingoBeagle.deliveryType == '2'){
			adingoBeagle.adingoYDNRequest();
		}
    }
    
    adingoBeagle.adingoYDNRequest = function(){
		if(adingoBeagle.ctxtKeyword != null && adingoBeagle.ctxtKeyword != ''){
document.write('<script type="text/javascript" src="http://im.ov.yahoo.co.jp/js_flat/v2/?source=ecnavi_jp_markezine_cat_ctxt&type=def&outputCharEnc=utf8&maxCount=3&callback=adingoBeagle.callback&ctxtUrl='+encodeURIComponent(document.URL)+'&ref='+encodeURIComponent(document.referrer)+'&ctxtKeywords='+adingoBeagle.ctxtKeyword+'&keywordCharEnc=utf8'+'"><\/script>');
		}else{
document.write('<script type="text/javascript" src="http://im.ov.yahoo.co.jp/js_flat/v2/?source=ecnavi_jp_markezine_cat_ctxt&type=def&outputCharEnc=utf8&maxCount=3&callback=adingoBeagle.callback&ctxtUrl='+encodeURIComponent(document.URL)+'&ref='+encodeURIComponent(document.referrer)+'"><\/script>');
		}
    }

    adingoBeagle.adingoYDNRequestAPP = function(){
document.write('<script type="text/javascript" src="http://fluct80.adingo.jp/js_flat/v2/?source=ecnavi_jp_markezine_cat_ctxt&type=def&outputCharEnc=utf8&maxCount=3&callback=adingoBeagle.callback&ctxtUrl='+encodeURIComponent(document.URL)+'&ref='+encodeURIComponent(document.referrer)+'"><\/script>');
    }
    adingoBeagle.adingoAdsRequest();
}
/* showAdingoBeagle0000000000001566 */
adingoBeagle["showAdingoBeagle"+"0000000000001566"] = function(id){
var tmp='';
if(typeof(Beagle_zSr)=="undefined") return;
var zsrStart = (adingoBeagle.outputCount+1)*6;
adingoBeagle.outputCount += 3;
var zsrEnd = (adingoBeagle.outputCount+1)*6;
if(Beagle_zSr.length<zsrEnd) zsrEnd = Beagle_zSr.length;
if(zsrStart >= zsrEnd) return;
var i=zsrStart;

tmp+="<!-- banner start-->";
tmp+="<div style=\"margin: 0; padding: 0; width: auto; height: auto; text-align: left !important; letter-spacing: normal !important; font-family: 'Lucida Grande','Hiragino Kaku Gothic Pro','ヒラギノ角ゴ Pro W3','ＭＳ Ｐゴシック',Geneva,Arial,Verdana,sans-serif !important;\">";
tmp+="<div style=\"border: 1px solid #ffffff; zoom: 1;\">";
for(var i=zsrStart; i<zsrEnd; i=i+6) {
tmp+="<div>";
tmp+="<a href=\"";
tmp += Beagle_zSr[i+2];
tmp+="\" style=\"padding: 4px 8px; height: auto; display: block; color: #0066ff; background: #ffffff; text-decoration: none;\" onmouseover=\"this.style.backgroundColor='#ffffcc';this.style.color='#cc3333';\" onmouseout=\"this.style.backgroundColor='#ffffff';this.style.color='#0066ff';\" target=\"_new\">";
tmp+="<span style=\"padding-bottom: 2px; line-height: 1.2; display: block; color: #0066ff; font-weight: bold; font-size: 12px; text-decoration: underline; clear:both;\" onmouseover=\"this.style.textDecoration='underline';this.style.color='#cc3333';\" onmouseout=\"this.style.textDecoration='underline';this.style.color='#0066ff';\">";
tmp += Beagle_zSr[i+3];
tmp+="</span>";
tmp+="<span style=\"width: auto; height: auto; line-height: 1.2; color: #666666; font-size: 12px; display: block; text-decoration: none;\">";
tmp += Beagle_zSr[i];
tmp+="</span>";
tmp+="<span style=\"line-height: 1.2; color: #0066CC; font-size: 10px; display: block; text-decoration: none;\" >";
tmp += Beagle_zSr[i+4];
tmp+="</span>";
tmp+="<img src=\"";
tmp += Beagle_zSr[i+1];
tmp+="\" style=\"margin:0;padding:0;width:0;height:0;display:none;border:0;\" />";
tmp+="</a>";
tmp+="</div>";
}
tmp+="<!-- overtureim -->";
tmp+="<div style=\"margin: 0; padding: 0; width: 100% !important; height: 16px; clear: both; background: #ffffff;\">";
tmp+="<ul style=\"margin: 0; padding: 3px 6px 0 0; display: block; color: #cccccc; font-size: 11px; text-align: right; list-style: none;\">";
tmp+="<li style=\"display: inline; font-size: 11px;\"><a href=\"";
tmp += Beagle_zSr[0];
tmp+="\" target=\"_blank\" style=\"margin: 0 0 0 3px; padding: 0; width: auto; height: auto; color: #cccccc; background: none; text-decoration: none; font-size: 11px; line-height: 1; display: inline; font-weight: normal;\">";
tmp += Beagle_zSr[1];
tmp+="</a></li>";
tmp+="</ul>";
tmp+="</div>";
tmp+="<!-- /overtureim -->";
tmp+="</div>";
tmp+="</div>";


adingoBeagle.showCount = Math.floor(Math.random()*1000000000000)+1;
document.write('<div id="adingoBeagle'+adingoBeagle.showCount+'"  ></div>')
document.getElementById('adingoBeagle'+adingoBeagle.showCount).innerHTML = tmp;
}

