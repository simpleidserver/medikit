var NATIVE_HOST = "medikit.authenticate";

var K_SRC = "src";
var K_ORIGIN = "origin";
var K_NONCE = "nonce";
var K_RESULT = "result";
var K_TAB = "tab";
var K_EXTENSION = "extension";

var ports = {};

var missing = true;

console.log("Background page activated");

typeof chrome.runtime.onStartup !== 'undefined' && chrome.runtime.onStartup.addListener(function() {
	_testNativeComponent().then(function(result) {
		missing = false;
	});
});

function _killPort(tab) {
	if (tab in ports) {
		ports[tab].postMessage({});
	}
}

function _testNativeComponent() {
	return new Promise(function(resolve, reject) {
		chrome.runtime.sendNativeMessage(NATIVE_HOST, { type: "PING" }, function(response) {
			resolve(response);
		});
	});
}


typeof chrome.runtime.onInstalled !== 'undefined' && chrome.runtime.onInstalled.addListener(function(details) {
	if (details.reason === "install" || details.reason === "update") {
		_testNativeComponent().then(function(result) {
			console.log(result);
		});
	}
});

chrome.runtime.onMessage.addListener(function(request, sender, sendResponse) {
	console.log(request);
	console.log('toto');
	if(sender.id !== chrome.runtime.id && sender.extensionId !== chrome.runtime.id) {
		console.log('WARNING: Ignoring message not from our extension');
		return;
	}
	if (sender.tab) {
		// Check if page is DONE and close the native component without doing anything else
		if (request["type"] === "DONE") {
			console.log("DONE " + sender.tab.id);
			if (sender.tab.id in ports) {
				// FIXME: would want to use Port.disconnect() here
				_killPort(sender.tab.id);
			} 
		} else {
			request[K_TAB] = sender.tab.id;
			if (missing) {
				_testNativeComponent().then(function(result) {
					_forward(request);
				});
			} else {
				// TODO: Check if the URL is in allowed list or not
				// Either way forward to native currently
				_forward(request);
			}
		}
	}
});

// Send the message back to the originating tab
function _reply(tab, msg) {
	msg[K_SRC] = "background.js";
	msg[K_EXTENSION] = chrome.runtime.getManifest().version;
	chrome.tabs.sendMessage(tab, msg);
}

// Fail an incoming message if the underlying implementation is not
// present
function _fail_with(msg, result) {
	var resp = {};
	resp[K_NONCE] = msg[K_NONCE];
	resp[K_RESULT] = result;
	_reply(msg[K_TAB], resp);
}

// Forward a message to the native component
function _forward(message) {
	var tabid = message[K_TAB];
	console.log("SEND " + tabid + ": " + JSON.stringify(message));
	if(!ports[tabid]) {
		console.log("OPEN " + tabid + ": " + NATIVE_HOST);
		var port = chrome.runtime.connectNative(NATIVE_HOST);
		if (!port) {
			console.log("OPEN ERROR: " + JSON.stringify(chrome.runtime.lastError));
		}
		port.onMessage.addListener(function(response) {
			if (response) {
				console.log("RECV "+tabid+": " + JSON.stringify(response));
				_reply(tabid, response);
			} else {
				console.log("ERROR "+tabid+": " + JSON.stringify(chrome.runtime.lastError));
				_fail_with(message, "technical_error");
			}
		});
		port.onDisconnect.addListener(function() {
			console.log("QUIT " + tabid);
			delete ports[tabid];
			// TODO: reject all pending promises for tab, if any
		});
		ports[tabid] = port;
		ports[tabid].postMessage(message);
	} else {
		// Port already open
		ports[tabid].postMessage(message);
	}
}