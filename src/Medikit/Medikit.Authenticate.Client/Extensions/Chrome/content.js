var inuse = false;

window.addEventListener("message", function(event) {
    if (event.source !== window)
        return;

    if (event.data.src && (event.data.src === "page.js")) {
        event.data["origin"] = location.origin;
        chrome.runtime.sendMessage(event.data, function(response) {});
        if (!inuse) {
            window.addEventListener("beforeunload", function(event) {
                chrome.runtime.sendMessage({src: 'page.js', type: 'DONE'});
            }, false);
            inuse = true;
        }
    }
}, false);

chrome.runtime.onMessage.addListener(function(request, sender, sendResponse) {
    window.postMessage(request, '*');
});

var s = document.createElement('script');
s.type = 'text/javascript';
s.innerHTML='// Promises \n\
var _eid_promises = {}; \n\
// Turn the incoming message from extension \n\
// into pending Promise resolving \n\
window.addEventListener("message", function(event) { \n\
    if(event.source !== window) return; \n\
    if(event.data.src && (event.data.src === "background.js")) { \n\
        console.log("Page received: "); \n\
        console.log(event.data); \n\
        // Get the promise \n\
        if(event.data.nonce) { \n\
            var p = _eid_promises[event.data.nonce]; \n\
			p.resolve(event.data); \n\
            delete _eid_promises[event.data.nonce]; \n\
        } else { \n\
            console.log("No nonce in event msg"); \n\
        } \n\
    } \n\
}, false); \n\
 \n\
 \n\
function MedikitExtension() { \n\
    function nonce() { \n\
        var val = ""; \n\
        var hex = "abcdefghijklmnopqrstuvwxyz0123456789"; \n\
        for(var i = 0; i < 16; i++) val += hex.charAt(Math.floor(Math.random() * hex.length)); \n\
        return val; \n\
    } \n\
 \n\
    function messagePromise(msg) { \n\
        return new Promise(function(resolve, reject) { \n\
            // amend with necessary metadata \n\
            msg["nonce"] = nonce(); \n\
            msg["src"] = "page.js"; \n\
            // send message \n\
            window.postMessage(msg, "*"); \n\
            // and store promise callbacks \n\
            _eid_promises[msg.nonce] = { \n\
                resolve: resolve, \n\
                reject: reject \n\
            }; \n\
        }); \n\
    } \n\
    this.getEIDAuth = function(pin) { \n\
        var msg = {type: "EID_AUTH", content: { pin: pin  }}; \n\
        return messagePromise(msg); \n\
    }; \n\
    this.getEHEALTHCertificateAuth = function() { \n\
        var msg = {type: "EHEALTH_AUTH" }; \n\
        return messagePromise(msg); \n\
    }; \n\
    this.getIdentityCertificates = function() { \n\
        var msg = {type: "GET_IDENTITIY_CERTIFICATES" }; \n\
        return messagePromise(msg); \n\
    }; \n\
    this.chooseIdentityCertificate = function(certificate, password) { \n\
        var msg = {type: "CHOOSE_IDENTITY_CERTIFICATE", content: { certificate: certificate, password: password } }; \n\
        return messagePromise(msg); \n\
    }; \n\
    this.getMedicalProfessions = function() { \n\
        var msg = {type: "GET_MEDICAL_PROFESSIONS" }; \n\
        return messagePromise(msg); \n\
    }; \n\
    this.chooseMedicalProfession = function(profession) { \n\
        var msg = {type: "CHOOSE_MEDICAL_PROFESSION", content: { profession: profession } }; \n\
        return messagePromise(msg); \n\
    }; \n\
}';

(document.head || document.documentElement).appendChild(s);