'use strict'
// @ts-check

let runned = false;

const eventHandler = (e) => {
    const jsonObject = { key: e.type };
    window.chrome.webview.postMessage(jsonObject);
}

document.addEventListener('DOMContentLoaded', () => {
    document.addEventListener('click', (e) => {
        eventHandler(e);

        if (runned) return;

        // document.onblur = eventHandler;
        // window.onblur = eventHandler;
    
        runned = true
    });

    const stylesheet = document.styleSheets[0];
    const newRule = `
        div > div > div > div p, ol li {  
            font-size: 17px;
            line-height: 26px !important;
            opacity: 0.95;
            font-family: "yekan bakh light" !important;
            font-weight: 300 !important;
        }
    `
    
    stylesheet.insertRule(newRule);
    
    document.documentElement.style['user-select'] = 'auto';
})