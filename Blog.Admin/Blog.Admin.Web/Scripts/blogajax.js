// config { url: 'url', data: 'data', dataType: 'json', type: 'get', success: 'function()', error: 'function()'
var ajax = function ajax(config) {
	// Create XMLHttpRequestObject
	var xhr;
	 
	if (typeof XMLHttpRequest !== 'undefined') {
		xhr = new XMLHttpRequest();
	}
	else {
		var versions = ["MSXML2.XmlHttp.5.0", "MSXML2.XmlHttp.4.0", "MSXML2.XmlHttp.3.0", "MSXML2.XmlHttp.2.0", "Microsoft.XmlHttp"]
		for(var i = 0, len = versions.length; i < len; i++) {
			try {
				xhr = new ActiveXObject(versions[i]);
				break;
			}
			catch(e){}
		}
	}
	
	// Open the XMLHttpRequestObject
	xhr.open(config.type, config.url, true);
	
	// Set event handler when ajax request completes
	xhr.onreadystatechange = function  {
		if (xmlhttp.readyState == 4 ) {
			if(xmlhttp.status == 200){
				// Ajax request success  
				return (config.dataType && config.dataType.toLower() === 'json') ? config.success(JSON.parse(xhr.responseText)) : config.success(xhr.responseText);
			} else {
				// Ajax request error
				return config.error ? error(xhr) : xhr;
			}
        }
	};
	
	// Prepares the data to be sent to server
	if (config.data) {
		var query = [];
		for (var key in config.data) {
			query.push(encodeURIComponent(key) + '=' + encodeURIComponent(data[key]));
		}
	}
		
	// Determine if get or post then send to server
	if (config.type === 'get') {
		if (config.data) config.url += '?' + query.join('&');
		xhr.send(config.url);
	} else {
		xhr.setRequestHeader('Content-type', 'application/x-www-form-urlencoded');
		xhr.send(config.url);
	}
}