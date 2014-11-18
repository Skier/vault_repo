/*
 *  $Id: language.js 1907 2006-01-24 15:04:28Z vitaly $
 *
 *  Copyright (C) 2006 Deka-Soft
 */

function getLanguage() {
    lang = navigator.systemLanguage;
    if (undefined != lang) {
        if (2 < lang.length) {
            lang = lang.substr(0, 2);
        }
    }
    if (undefined == lang) {
        return 'ua';
    }
    return lang;
}
