/* Yahoo! Media Player Loader, Build 0.5.5.  Copyright (c) 2011, Yahoo! Inc.  All rights reserved.
 * Your use of this Yahoo! Media Player is subject to the Yahoo! Terms of Service
 * located at http://info.yahoo.com/legal/us/yahoo/mediaplayer/details.html
 */
function yui_Namespace() {
    var b = arguments,
        g = null,
        e, c, f;
    for (e = 0; e < b.length; ++e) {
        f = b[e].split(".");
        g = YAHOO;
        for (c = (f[0] == "YAHOO") ? 1 : 0; c < f.length; ++c) {
            g[f[c]] = g[f[c]] || {};
            g = g[f[c]];
        }
    }
    return g;
}
if (typeof YAHOO == "undefined") {
    var YAHOO = {};
}
if (typeof YAHOO.namespace == "undefined") {
    YAHOO.namespace = yui_Namespace;
}
YAHOO.ympyui = YAHOO.ympyui || (function () {
    var a = {};
    if (typeof a == "undefined" || !a) {
        var a = {};
    }
    a.namespace = function () {
        var b = arguments,
            c = null,
            e, f, d;
        for (e = 0; e < b.length; e = e + 1) {
            d = ("" + b[e]).split(".");
            c = a;
            for (f = (d[0] == "YAHOO") ? 1 : 0; f < d.length; f = f + 1) {
                c[d[f]] = c[d[f]] || {};
                c = c[d[f]];
            }
        }
        return c;
    };
    a.log = function (c, b, d) {
        var e = a.widget.Logger;
        if (e && e.log) {
            return e.log(c, b, d);
        } else {
            return false;
        }
    };
    a.register = function (e, k, b) {
        var f = a.env.modules,
            d, g, h, j, c;
        if (!f[e]) {
            f[e] = {
                versions: [],
                builds: []
            };
        }
        d = f[e];
        g = b.version;
        h = b.build;
        j = a.env.listeners;
        d.name = e;
        d.version = g;
        d.build = h;
        d.versions.push(g);
        d.builds.push(h);
        d.mainClass = k;
        for (c = 0; c < j.length; c = c + 1) {
            j[c](d);
        }
        if (k) {
            k.VERSION = g;
            k.BUILD = h;
        } else {
            a.log("mainClass is undefined for module " + e, "warn");
        }
    };
    a.env = a.env || {
        modules: [],
        listeners: []
    };
    a.env.getVersion = function (b) {
        return a.env.modules[b] || null;
    };
    a.env.ua = function () {
        var f = function (k) {
            var j = 0;
            return parseFloat(k.replace(/\./g, function () {
                return (j++ == 1) ? "" : ".";
            }));
        },
            c = navigator,
            d = {
                ie: 0,
                opera: 0,
                gecko: 0,
                webkit: 0,
                mobile: null,
                air: 0,
                caja: c.cajaVersion,
                secure: false,
                os: null
            },
            g = navigator && navigator.userAgent,
            e = window && window.location,
            h = e && e.href,
            b;
        d.secure = h && (h.toLowerCase().indexOf("https") === 0);
        if (g) {
            if ((/windows|win32/i).test(g)) {
                d.os = "windows";
            } else {
                if ((/macintosh/i).test(g)) {
                    d.os = "macintosh";
                }
            }
            if ((/KHTML/).test(g)) {
                d.webkit = 1;
            }
            b = g.match(/AppleWebKit\/([^\s]*)/);
            if (b && b[1]) {
                d.webkit = f(b[1]);
                if (/ Mobile\//.test(g)) {
                    d.mobile = "Apple";
                } else {
                    b = g.match(/NokiaN[^\/]*/);
                    if (b) {
                        d.mobile = b[0];
                    }
                }
                b = g.match(/AdobeAIR\/([^\s]*)/);
                if (b) {
                    d.air = b[0];
                }
            }
            if (!d.webkit) {
                b = g.match(/Opera[\s\/]([^\s]*)/);
                if (b && b[1]) {
                    d.opera = f(b[1]);
                    b = g.match(/Opera Mini[^;]*/);
                    if (b) {
                        d.mobile = b[0];
                    }
                } else {
                    b = g.match(/MSIE\s([^;]*)/);
                    if (b && b[1]) {
                        d.ie = f(b[1]);
                    } else {
                        b = g.match(/Gecko\/([^\s]*)/);
                        if (b) {
                            d.gecko = 1;
                            b = g.match(/rv:([^\s\)]*)/);
                            if (b && b[1]) {
                                d.gecko = f(b[1]);
                            }
                        }
                    }
                }
            }
        }
        return d;
    }();
    (function () {
        a.namespace("util", "widget", "example");
        if ("undefined" !== typeof YAHOO_config) {
            var e = YAHOO_config.listener,
                b = a.env.listeners,
                c = true,
                d;
            if (e) {
                for (d = 0; d < b.length; d++) {
                    if (b[d] == e) {
                        c = false;
                        break;
                    }
                }
                if (c) {
                    b.push(e);
                }
            }
        }
    })();
    a.lang = a.lang || {};
    (function () {
        var j = a.lang,
            b = Object.prototype,
            c = "[object Array]",
            h = "[object Function]",
            d = "[object Object]",
            f = [],
            e = ["toString", "valueOf"],
            g = {
                isArray: function (k) {
                    return b.toString.apply(k) === c;
                },
                isBoolean: function (k) {
                    return typeof k === "boolean";
                },
                isFunction: function (k) {
                    return (typeof k === "function") || b.toString.apply(k) === h;
                },
                isNull: function (k) {
                    return k === null;
                },
                isNumber: function (k) {
                    return typeof k === "number" && isFinite(k);
                },
                isObject: function (k) {
                    return (k && (typeof k === "object" || j.isFunction(k))) || false;
                },
                isString: function (k) {
                    return typeof k === "string";
                },
                isUndefined: function (k) {
                    return typeof k === "undefined";
                },
                _IEEnumFix: (a.env.ua.ie) ?
                function (l, m) {
                    var n, o, k;
                    for (n = 0; n < e.length; n = n + 1) {
                        o = e[n];
                        k = m[o];
                        if (j.isFunction(k) && k != b[o]) {
                            l[o] = k;
                        }
                    }
                } : function () {},
                extend: function (k, o, l) {
                    if (!o || !k) {
                        throw new Error("extend failed, please check that " + "all dependencies are included.");
                    }
                    var m = function () {},
                        n;
                    m.prototype = o.prototype;
                    k.prototype = new m();
                    k.prototype.constructor = k;
                    k.superclass = o.prototype;
                    if (o.prototype.constructor == b.constructor) {
                        o.prototype.constructor = o;
                    }
                    if (l) {
                        for (n in l) {
                            if (j.hasOwnProperty(l, n)) {
                                k.prototype[n] = l[n];
                            }
                        }
                        j._IEEnumFix(k.prototype, l);
                    }
                },
                augmentObject: function (p, k) {
                    if (!k || !p) {
                        throw new Error("Absorb failed, verify dependencies.");
                    }
                    var n = arguments,
                        l, o, m = n[2];
                    if (m && m !== true) {
                        for (l = 2; l < n.length; l = l + 1) {
                            p[n[l]] = k[n[l]];
                        }
                    } else {
                        for (o in k) {
                            if (m || !(o in p)) {
                                p[o] = k[o];
                            }
                        }
                        j._IEEnumFix(p, k);
                    }
                },
                augmentProto: function (k, l) {
                    if (!l || !k) {
                        throw new Error("Augment failed, verify dependencies.");
                    }
                    var n = [k.prototype, l.prototype],
                        m;
                    for (m = 2; m < arguments.length; m = m + 1) {
                        n.push(arguments[m]);
                    }
                    j.augmentObject.apply(this, n);
                },
                dump: function (s, n) {
                    var q, o, l = [],
                        k = "{...}",
                        r = "f(){...}",
                        m = ", ",
                        p = " => ";
                    if (!j.isObject(s)) {
                        return s + "";
                    } else {
                        if (s instanceof Date || ("nodeType" in s && "tagName" in s)) {
                            return s;
                        } else {
                            if (j.isFunction(s)) {
                                return r;
                            }
                        }
                    }
                    n = (j.isNumber(n)) ? n : 3;
                    if (j.isArray(s)) {
                        l.push("[");
                        for (q = 0, o = s.length; q < o; q = q + 1) {
                            if (j.isObject(s[q])) {
                                l.push((n > 0) ? j.dump(s[q], n - 1) : k);
                            } else {
                                l.push(s[q]);
                            }
                            l.push(m);
                        }
                        if (l.length > 1) {
                            l.pop();
                        }
                        l.push("]");
                    } else {
                        l.push("{");
                        for (q in s) {
                            if (j.hasOwnProperty(s, q)) {
                                l.push(q + p);
                                if (j.isObject(s[q])) {
                                    l.push((n > 0) ? j.dump(s[q], n - 1) : k);
                                } else {
                                    l.push(s[q]);
                                }
                                l.push(m);
                            }
                        }
                        if (l.length > 1) {
                            l.pop();
                        }
                        l.push("}");
                    }
                    return l.join("");
                },
                substitute: function (k, z, r) {
                    var v, w, x, o, n, l, p = [],
                        y, u = "dump",
                        q = " ",
                        A = "{",
                        m = "}",
                        s, t;
                    for (;;) {
                        v = k.lastIndexOf(A);
                        if (v < 0) {
                            break;
                        }
                        w = k.indexOf(m, v);
                        if (v + 1 >= w) {
                            break;
                        }
                        y = k.substring(v + 1, w);
                        o = y;
                        l = null;
                        x = o.indexOf(q);
                        if (x > -1) {
                            l = o.substring(x + 1);
                            o = o.substring(0, x);
                        }
                        n = z[o];
                        if (r) {
                            n = r(o, n, l);
                        }
                        if (j.isObject(n)) {
                            if (j.isArray(n)) {
                                n = j.dump(n, parseInt(l, 10));
                            } else {
                                l = l || "";
                                s = l.indexOf(u);
                                if (s > -1) {
                                    l = l.substring(4);
                                }
                                t = n.toString();
                                if (t === d || s > -1) {
                                    n = j.dump(n, parseInt(l, 10));
                                } else {
                                    n = t;
                                }
                            }
                        } else {
                            if (!j.isString(n) && !j.isNumber(n)) {
                                n = "~-" + p.length + "-~";
                                p[p.length] = y;
                            }
                        }
                        k = k.substring(0, v) + n + k.substring(w + 1);
                    }
                    for (v = p.length - 1; v >= 0; v = v - 1) {
                        k = k.replace(new RegExp("~-" + v + "-~"), "{" + p[v] + "}", "g");
                    }
                    return k;
                },
                trim: function (l) {
                    try {
                        return l.replace(/^\s+|\s+$/g, "");
                    } catch (k) {
                        return l;
                    }
                },
                merge: function () {
                    var k = {},
                        m = arguments,
                        n = m.length,
                        l;
                    for (l = 0; l < n; l = l + 1) {
                        j.augmentObject(k, m[l], true);
                    }
                    return k;
                },
                later: function (l, r, k, p, o) {
                    l = l || 0;
                    r = r || {};
                    var q = k,
                        m = p,
                        n, s;
                    if (j.isString(k)) {
                        q = r[k];
                    }
                    if (!q) {
                        throw new TypeError("method undefined");
                    }
                    if (m && !j.isArray(m)) {
                        m = [p];
                    }
                    n = function () {
                        q.apply(r, m || f);
                    };
                    s = (o) ? setInterval(n, l) : setTimeout(n, l);
                    return {
                        interval: o,
                        cancel: function () {
                            if (this.interval) {
                                clearInterval(s);
                            } else {
                                clearTimeout(s);
                            }
                        }
                    };
                },
                isValue: function (k) {
                    return (j.isObject(k) || j.isString(k) || j.isNumber(k) || j.isBoolean(k));
                }
            };
        j.hasOwnProperty = (b.hasOwnProperty) ?
        function (l, k) {
            return l && l.hasOwnProperty(k);
        } : function (l, k) {
            return !j.isUndefined(l[k]) && l.constructor.prototype[k] !== l[k];
        };
        g.augmentObject(j, g, true);
        a.util.Lang = j;
        j.augment = j.augmentProto;
        a.augment = j.augmentProto;
        a.extend = j.extend;
    })();
    a.register("yahoo", a, {
        version: "2.8.0r4",
        build: "2449"
    });
    (function () {
        a.env._id_counter = a.env._id_counter || 0;
        var ao = a.util,
            ai = a.lang,
            aE = a.env.ua,
            at = a.lang.trim,
            aN = {},
            aJ = {},
            ag = /^t(?:able|d|h)$/i,
            y = /color$/i,
            aj = window.document,
            z = aj.documentElement,
            aM = "ownerDocument",
            aD = "defaultView",
            av = "documentElement",
            ax = "compatMode",
            aP = "offsetLeft",
            ae = "offsetTop",
            aw = "offsetParent",
            x = "parentNode",
            aF = "nodeType",
            aq = "tagName",
            af = "scrollLeft",
            aI = "scrollTop",
            ad = "getBoundingClientRect",
            au = "getComputedStyle",
            aQ = "currentStyle",
            ah = "CSS1Compat",
            aO = "BackCompat",
            aK = "class",
            an = "className",
            ak = "",
            ar = " ",
            ay = "(?:^|\\s)",
            aG = "(?= |$)",
            Y = "g",
            aB = "position",
            aL = "fixed",
            G = "relative",
            aH = "left",
            aC = "top",
            az = "medium",
            aA = "borderLeftWidth",
            ac = "borderTopWidth",
            ap = aE.opera,
            al = aE.webkit,
            am = aE.gecko,
            aa = aE.ie;
        ao.Dom = {
            CUSTOM_ATTRIBUTES: (!z.hasAttribute) ? {
                "for": "htmlFor",
                "class": an
            } : {
                "htmlFor": "for",
                "className": aK
            },
            DOT_ATTRIBUTES: {},
            get: function (g) {
                var d, b, f, h, e, c;
                if (g) {
                    if (g[aF] || g.item) {
                        return g;
                    }
                    if (typeof g === "string") {
                        d = g;
                        g = aj.getElementById(g);
                        c = (g) ? g.attributes : null;
                        if (g && c && c.id && c.id.value === d) {
                            return g;
                        } else {
                            if (g && aj.all) {
                                g = null;
                                b = aj.all[d];
                                for (h = 0, e = b.length; h < e; ++h) {
                                    if (b[h].id === d) {
                                        return b[h];
                                    }
                                }
                            }
                        }
                        return g;
                    }
                    if (a.util.Element && g instanceof a.util.Element) {
                        g = g.get("element");
                    }
                    if ("length" in g) {
                        f = [];
                        for (h = 0, e = g.length; h < e; ++h) {
                            f[f.length] = ao.Dom.get(g[h]);
                        }
                        return f;
                    }
                    return g;
                }
                return null;
            },
            getComputedStyle: function (b, c) {
                if (window[au]) {
                    return b[aM][aD][au](b, null)[c];
                } else {
                    if (b[aQ]) {
                        return ao.Dom.IE_ComputedStyle.get(b, c);
                    }
                }
            },
            getStyle: function (b, c) {
                return ao.Dom.batch(b, ao.Dom._getStyle, c);
            },
            _getStyle: function () {
                if (window[au]) {
                    return function (c, e) {
                        e = (e === "float") ? e = "cssFloat" : ao.Dom._toCamel(e);
                        var b = c.style[e],
                            d;
                        if (!b) {
                            d = c[aM][aD][au](c, null);
                            if (d) {
                                b = d[e];
                            }
                        }
                        return b;
                    };
                } else {
                    if (z[aQ]) {
                        return function (c, f) {
                            var b;
                            switch (f) {
                            case "opacity":
                                b = 100;
                                try {
                                    b = c.filters["DXImageTransform.Microsoft.Alpha"].opacity;
                                } catch (e) {
                                    try {
                                        b = c.filters("alpha").opacity;
                                    } catch (d) {}
                                }
                                return b / 100;
                            case "float":
                                f = "styleFloat";
                            default:
                                f = ao.Dom._toCamel(f);
                                b = c[aQ] ? c[aQ][f] : null;
                                return (c.style[f] || b);
                            }
                        };
                    }
                }
            }(),
            setStyle: function (c, d, b) {
                ao.Dom.batch(c, ao.Dom._setStyle, {
                    prop: d,
                    val: b
                });
            },
            _setStyle: function () {
                if (aa) {
                    return function (d, c) {
                        var b = ao.Dom._toCamel(c.prop),
                            e = c.val;
                        if (d) {
                            switch (b) {
                            case "opacity":
                                if (ai.isString(d.style.filter)) {
                                    d.style.filter = "alpha(opacity=" + e * 100 + ")";
                                    if (!d[aQ] || !d[aQ].hasLayout) {
                                        d.style.zoom = 1;
                                    }
                                }
                                break;
                            case "float":
                                b = "styleFloat";
                            default:
                                d.style[b] = e;
                            }
                        } else {}
                    };
                } else {
                    return function (d, c) {
                        var b = ao.Dom._toCamel(c.prop),
                            e = c.val;
                        if (d) {
                            if (b == "float") {
                                b = "cssFloat";
                            }
                            d.style[b] = e;
                        } else {}
                    };
                }
            }(),
            getXY: function (b) {
                return ao.Dom.batch(b, ao.Dom._getXY);
            },
            _canPosition: function (b) {
                return (ao.Dom._getStyle(b, "display") !== "none" && ao.Dom._inDoc(b));
            },
            _getXY: function () {
                if (aj[av][ad]) {
                    return function (l) {
                        var k, b, j, d, e, f, g, n, m, h = Math.floor,
                            c = false;
                        if (ao.Dom._canPosition(l)) {
                            j = l[ad]();
                            d = l[aM];
                            k = ao.Dom.getDocumentScrollLeft(d);
                            b = ao.Dom.getDocumentScrollTop(d);
                            c = [h(j[aH]), h(j[aC])];
                            if (aa && aE.ie < 8) {
                                e = 2;
                                f = 2;
                                g = d[ax];
                                if (aE.ie === 6) {
                                    if (g !== aO) {
                                        e = 0;
                                        f = 0;
                                    }
                                }
                                if ((g === aO)) {
                                    n = ab(d[av], aA);
                                    m = ab(d[av], ac);
                                    if (n !== az) {
                                        e = parseInt(n, 10);
                                    }
                                    if (m !== az) {
                                        f = parseInt(m, 10);
                                    }
                                }
                                c[0] -= e;
                                c[1] -= f;
                            }
                            if ((b || k)) {
                                c[0] += k;
                                c[1] += b;
                            }
                            c[0] = h(c[0]);
                            c[1] = h(c[1]);
                        } else {}
                        return c;
                    };
                } else {
                    return function (j) {
                        var b, h, g, e, d, f = false,
                            c = j;
                        if (ao.Dom._canPosition(j)) {
                            f = [j[aP], j[ae]];
                            b = ao.Dom.getDocumentScrollLeft(j[aM]);
                            h = ao.Dom.getDocumentScrollTop(j[aM]);
                            d = ((am || aE.webkit > 519) ? true : false);
                            while ((c = c[aw])) {
                                f[0] += c[aP];
                                f[1] += c[ae];
                                if (d) {
                                    f = ao.Dom._calcBorders(c, f);
                                }
                            }
                            if (ao.Dom._getStyle(j, aB) !== aL) {
                                c = j;
                                while ((c = c[x]) && c[aq]) {
                                    g = c[aI];
                                    e = c[af];
                                    if (am && (ao.Dom._getStyle(c, "overflow") !== "visible")) {
                                        f = ao.Dom._calcBorders(c, f);
                                    }
                                    if (g || e) {
                                        f[0] -= e;
                                        f[1] -= g;
                                    }
                                }
                                f[0] += b;
                                f[1] += h;
                            } else {
                                if (ap) {
                                    f[0] -= b;
                                    f[1] -= h;
                                } else {
                                    if (al || am) {
                                        f[0] += b;
                                        f[1] += h;
                                    }
                                }
                            }
                            f[0] = Math.floor(f[0]);
                            f[1] = Math.floor(f[1]);
                        } else {}
                        return f;
                    };
                }
            }(),
            getX: function (b) {
                var c = function (d) {
                    return ao.Dom.getXY(d)[0];
                };
                return ao.Dom.batch(b, c, ao.Dom, true);
            },
            getY: function (b) {
                var c = function (d) {
                    return ao.Dom.getXY(d)[1];
                };
                return ao.Dom.batch(b, c, ao.Dom, true);
            },
            setXY: function (c, b, d) {
                ao.Dom.batch(c, ao.Dom._setXY, {
                    pos: b,
                    noRetry: d
                });
            },
            _setXY: function (k, g) {
                var f = ao.Dom._getStyle(k, aB),
                    h = ao.Dom.setStyle,
                    c = g.pos,
                    b = g.noRetry,
                    e = [parseInt(ao.Dom.getComputedStyle(k, aH), 10), parseInt(ao.Dom.getComputedStyle(k, aC), 10)],
                    d, j;
                if (f == "static") {
                    f = G;
                    h(k, aB, f);
                }
                d = ao.Dom._getXY(k);
                if (!c || d === false) {
                    return false;
                }
                if (isNaN(e[0])) {
                    e[0] = (f == G) ? 0 : k[aP];
                }
                if (isNaN(e[1])) {
                    e[1] = (f == G) ? 0 : k[ae];
                }
                if (c[0] !== null) {
                    h(k, aH, c[0] - d[0] + e[0] + "px");
                }
                if (c[1] !== null) {
                    h(k, aC, c[1] - d[1] + e[1] + "px");
                }
                if (!b) {
                    j = ao.Dom._getXY(k);
                    if ((c[0] !== null && j[0] != c[0]) || (c[1] !== null && j[1] != c[1])) {
                        ao.Dom._setXY(k, {
                            pos: c,
                            noRetry: true
                        });
                    }
                }
            },
            setX: function (c, b) {
                ao.Dom.setXY(c, [b, null]);
            },
            setY: function (b, c) {
                ao.Dom.setXY(b, [null, c]);
            },
            getRegion: function (b) {
                var c = function (d) {
                    var e = false;
                    if (ao.Dom._canPosition(d)) {
                        e = ao.Region.getRegion(d);
                    } else {}
                    return e;
                };
                return ao.Dom.batch(b, c, ao.Dom, true);
            },
            getClientWidth: function () {
                return ao.Dom.getViewportWidth();
            },
            getClientHeight: function () {
                return ao.Dom.getViewportHeight();
            },
            getElementsByClassName: function (g, c, f, d, l, e) {
                c = c || "*";
                f = (f) ? ao.Dom.get(f) : null || aj;
                if (!f) {
                    return [];
                }
                var b = [],
                    m = f.getElementsByTagName(c),
                    j = ao.Dom.hasClass;
                for (var k = 0, h = m.length; k < h; ++k) {
                    if (j(m[k], g)) {
                        b[b.length] = m[k];
                    }
                }
                if (d) {
                    ao.Dom.batch(b, d, l, e);
                }
                return b;
            },
            hasClass: function (c, b) {
                return ao.Dom.batch(c, ao.Dom._hasClass, b);
            },
            _hasClass: function (b, d) {
                var c = false,
                    e;
                if (b && d) {
                    e = ao.Dom._getAttribute(b, an) || ak;
                    if (d.exec) {
                        c = d.test(e);
                    } else {
                        c = d && (ar + e + ar).indexOf(ar + d + ar) > -1;
                    }
                } else {}
                return c;
            },
            addClass: function (c, b) {
                return ao.Dom.batch(c, ao.Dom._addClass, b);
            },
            _addClass: function (b, d) {
                var c = false,
                    e;
                if (b && d) {
                    e = ao.Dom._getAttribute(b, an) || ak;
                    if (!ao.Dom._hasClass(b, d)) {
                        ao.Dom.setAttribute(b, an, at(e + ar + d));
                        c = true;
                    }
                } else {}
                return c;
            },
            removeClass: function (c, b) {
                return ao.Dom.batch(c, ao.Dom._removeClass, b);
            },
            _removeClass: function (g, b) {
                var f = false,
                    e, d, c;
                if (g && b) {
                    e = ao.Dom._getAttribute(g, an) || ak;
                    ao.Dom.setAttribute(g, an, e.replace(ao.Dom._getClassRegex(b), ak));
                    d = ao.Dom._getAttribute(g, an);
                    if (e !== d) {
                        ao.Dom.setAttribute(g, an, at(d));
                        f = true;
                        if (ao.Dom._getAttribute(g, an) === "") {
                            c = (g.hasAttribute && g.hasAttribute(aK)) ? aK : an;
                            g.removeAttribute(c);
                        }
                    }
                } else {}
                return f;
            },
            replaceClass: function (b, d, c) {
                return ao.Dom.batch(b, ao.Dom._replaceClass, {
                    from: d,
                    to: c
                });
            },
            _replaceClass: function (h, b) {
                var g, d, f, c = false,
                    e;
                if (h && b) {
                    d = b.from;
                    f = b.to;
                    if (!f) {
                        c = false;
                    } else {
                        if (!d) {
                            c = ao.Dom._addClass(h, b.to);
                        } else {
                            if (d !== f) {
                                e = ao.Dom._getAttribute(h, an) || ak;
                                g = (ar + e.replace(ao.Dom._getClassRegex(d), ar + f)).split(ao.Dom._getClassRegex(f));
                                g.splice(1, 0, ar + f);
                                ao.Dom.setAttribute(h, an, at(g.join(ak)));
                                c = true;
                            }
                        }
                    }
                } else {}
                return c;
            },
            generateId: function (c, b) {
                b = b || "yui-gen";
                var d = function (f) {
                    if (f && f.id) {
                        return f.id;
                    }
                    var e = b + a.env._id_counter++;
                    if (f) {
                        if (f[aM] && f[aM].getElementById(e)) {
                            return ao.Dom.generateId(f, e + b);
                        }
                        f.id = e;
                    }
                    return e;
                };
                return ao.Dom.batch(c, d, ao.Dom, true) || d.apply(ao.Dom, arguments);
            },
            isAncestor: function (d, b) {
                d = ao.Dom.get(d);
                b = ao.Dom.get(b);
                var c = false;
                if ((d && b) && (d[aF] && b[aF])) {
                    if (d.contains && d !== b) {
                        c = d.contains(b);
                    } else {
                        if (d.compareDocumentPosition) {
                            c = !! (d.compareDocumentPosition(b) & 16);
                        }
                    }
                } else {}
                return c;
            },
            inDocument: function (b, c) {
                return ao.Dom._inDoc(ao.Dom.get(b), c);
            },
            _inDoc: function (d, b) {
                var c = false;
                if (d && d[aq]) {
                    b = b || d[aM];
                    c = ao.Dom.isAncestor(b[av], d);
                } else {}
                return c;
            },
            getElementsBy: function (b, c, g, e, k, f, d) {
                c = c || "*";
                g = (g) ? ao.Dom.get(g) : null || aj;
                if (!g) {
                    return [];
                }
                var l = [],
                    m = g.getElementsByTagName(c);
                for (var j = 0, h = m.length; j < h; ++j) {
                    if (b(m[j])) {
                        if (d) {
                            l = m[j];
                            break;
                        } else {
                            l[l.length] = m[j];
                        }
                    }
                }
                if (e) {
                    ao.Dom.batch(l, e, k, f);
                }
                return l;
            },
            getElementBy: function (b, c, d) {
                return ao.Dom.getElementsBy(b, c, d, null, null, null, true);
            },
            batch: function (b, d, g, f) {
                var h = [],
                    e = (f) ? g : window;
                b = (b && (b[aq] || b.item)) ? b : ao.Dom.get(b);
                if (b && d) {
                    if (b[aq] || b.length === undefined) {
                        return d.call(e, b, g);
                    }
                    for (var c = 0; c < b.length; ++c) {
                        h[h.length] = d.call(e, b[c], g);
                    }
                } else {
                    return false;
                }
                return h;
            },
            getDocumentHeight: function () {
                var c = (aj[ax] != ah || al) ? aj.body.scrollHeight : z.scrollHeight,
                    b = Math.max(c, ao.Dom.getViewportHeight());
                return b;
            },
            getDocumentWidth: function () {
                var c = (aj[ax] != ah || al) ? aj.body.scrollWidth : z.scrollWidth,
                    b = Math.max(c, ao.Dom.getViewportWidth());
                return b;
            },
            getViewportHeight: function () {
                var b = self.innerHeight,
                    c = aj[ax];
                if ((c || aa) && !ap) {
                    b = (c == ah) ? z.clientHeight : aj.body.clientHeight;
                }
                return b;
            },
            getViewportWidth: function () {
                var b = self.innerWidth,
                    c = aj[ax];
                if (c || aa) {
                    b = (c == ah) ? z.clientWidth : aj.body.clientWidth;
                }
                return b;
            },
            getAncestorBy: function (b, c) {
                while ((b = b[x])) {
                    if (ao.Dom._testElement(b, c)) {
                        return b;
                    }
                }
                return null;
            },
            getAncestorByClassName: function (d, c) {
                d = ao.Dom.get(d);
                if (!d) {
                    return null;
                }
                var b = function (e) {
                    return ao.Dom.hasClass(e, c);
                };
                return ao.Dom.getAncestorBy(d, b);
            },
            getAncestorByTagName: function (d, c) {
                d = ao.Dom.get(d);
                if (!d) {
                    return null;
                }
                var b = function (e) {
                    return e[aq] && e[aq].toUpperCase() == c.toUpperCase();
                };
                return ao.Dom.getAncestorBy(d, b);
            },
            getPreviousSiblingBy: function (b, c) {
                while (b) {
                    b = b.previousSibling;
                    if (ao.Dom._testElement(b, c)) {
                        return b;
                    }
                }
                return null;
            },
            getPreviousSibling: function (b) {
                b = ao.Dom.get(b);
                if (!b) {
                    return null;
                }
                return ao.Dom.getPreviousSiblingBy(b);
            },
            getNextSiblingBy: function (b, c) {
                while (b) {
                    b = b.nextSibling;
                    if (ao.Dom._testElement(b, c)) {
                        return b;
                    }
                }
                return null;
            },
            getNextSibling: function (b) {
                b = ao.Dom.get(b);
                if (!b) {
                    return null;
                }
                return ao.Dom.getNextSiblingBy(b);
            },
            getFirstChildBy: function (c, b) {
                var d = (ao.Dom._testElement(c.firstChild, b)) ? c.firstChild : null;
                return d || ao.Dom.getNextSiblingBy(c.firstChild, b);
            },
            getFirstChild: function (b, c) {
                b = ao.Dom.get(b);
                if (!b) {
                    return null;
                }
                return ao.Dom.getFirstChildBy(b);
            },
            getLastChildBy: function (c, b) {
                if (!c) {
                    return null;
                }
                var d = (ao.Dom._testElement(c.lastChild, b)) ? c.lastChild : null;
                return d || ao.Dom.getPreviousSiblingBy(c.lastChild, b);
            },
            getLastChild: function (b) {
                b = ao.Dom.get(b);
                return ao.Dom.getLastChildBy(b);
            },
            getChildrenBy: function (d, e) {
                var b = ao.Dom.getFirstChildBy(d, e),
                    c = b ? [b] : [];
                ao.Dom.getNextSiblingBy(b, function (f) {
                    if (!e || e(f)) {
                        c[c.length] = f;
                    }
                    return false;
                });
                return c;
            },
            getChildren: function (b) {
                b = ao.Dom.get(b);
                if (!b) {}
                return ao.Dom.getChildrenBy(b);
            },
            getDocumentScrollLeft: function (b) {
                b = b || aj;
                return Math.max(b[av].scrollLeft, b.body.scrollLeft);
            },
            getDocumentScrollTop: function (b) {
                b = b || aj;
                return Math.max(b[av].scrollTop, b.body.scrollTop);
            },
            insertBefore: function (c, b) {
                c = ao.Dom.get(c);
                b = ao.Dom.get(b);
                if (!c || !b || !b[x]) {
                    return null;
                }
                return b[x].insertBefore(c, b);
            },
            insertAfter: function (c, b) {
                c = ao.Dom.get(c);
                b = ao.Dom.get(b);
                if (!c || !b || !b[x]) {
                    return null;
                }
                if (b.nextSibling) {
                    return b[x].insertBefore(c, b.nextSibling);
                } else {
                    return b[x].appendChild(c);
                }
            },
            getClientRegion: function () {
                var b = ao.Dom.getDocumentScrollTop(),
                    d = ao.Dom.getDocumentScrollLeft(),
                    e = ao.Dom.getViewportWidth() + d,
                    c = ao.Dom.getViewportHeight() + b;
                return new ao.Region(b, e, c, d);
            },
            setAttribute: function (d, c, b) {
                ao.Dom.batch(d, ao.Dom._setAttribute, {
                    attr: c,
                    val: b
                });
            },
            _setAttribute: function (b, d) {
                var c = ao.Dom._toCamel(d.attr),
                    e = d.val;
                if (b && b.setAttribute) {
                    if (ao.Dom.DOT_ATTRIBUTES[c]) {
                        b[c] = e;
                    } else {
                        c = ao.Dom.CUSTOM_ATTRIBUTES[c] || c;
                        b.setAttribute(c, e);
                    }
                } else {}
            },
            getAttribute: function (c, b) {
                return ao.Dom.batch(c, ao.Dom._getAttribute, b);
            },
            _getAttribute: function (d, c) {
                var b;
                c = ao.Dom.CUSTOM_ATTRIBUTES[c] || c;
                if (d && d.getAttribute) {
                    b = d.getAttribute(c, 2);
                } else {}
                return b;
            },
            _toCamel: function (d) {
                var b = aN;

                function c(f, e) {
                    return e.toUpperCase();
                }
                return b[d] || (b[d] = d.indexOf("-") === -1 ? d : d.replace(/-([a-z])/gi, c));
            },
            _getClassRegex: function (c) {
                var b;
                if (c !== undefined) {
                    if (c.exec) {
                        b = c;
                    } else {
                        b = aJ[c];
                        if (!b) {
                            c = c.replace(ao.Dom._patterns.CLASS_RE_TOKENS, "\\$1");
                            b = aJ[c] = new RegExp(ay + c + aG, Y);
                        }
                    }
                }
                return b;
            },
            _patterns: {
                ROOT_TAG: /^body|html$/i,
                CLASS_RE_TOKENS: /([\.\(\)\^\$\*\+\?\|\[\]\{\}\\])/g
            },
            _testElement: function (b, c) {
                return b && b[aF] == 1 && (!c || c(b));
            },
            _calcBorders: function (b, e) {
                var d = parseInt(ao.Dom[au](b, ac), 10) || 0,
                    c = parseInt(ao.Dom[au](b, aA), 10) || 0;
                if (am) {
                    if (ag.test(b[aq])) {
                        d = 0;
                        c = 0;
                    }
                }
                e[0] += c;
                e[1] += d;
                return e;
            }
        };
        var ab = ao.Dom[au];
        if (aE.opera) {
            ao.Dom[au] = function (d, c) {
                var b = ab(d, c);
                if (y.test(c)) {
                    b = ao.Dom.Color.toRGB(b);
                }
                return b;
            };
        }
        if (aE.webkit) {
            ao.Dom[au] = function (d, c) {
                var b = ab(d, c);
                if (b === "rgba(0, 0, 0, 0)") {
                    b = "transparent";
                }
                return b;
            };
        }
        if (aE.ie && aE.ie >= 8 && aj.documentElement.hasAttribute) {
            ao.Dom.DOT_ATTRIBUTES.type = true;
        }
    })();
    a.util.Region = function (d, c, b, e) {
        this.top = d;
        this.y = d;
        this[1] = d;
        this.right = c;
        this.bottom = b;
        this.left = e;
        this.x = e;
        this[0] = e;
        this.width = this.right - this.left;
        this.height = this.bottom - this.top;
    };
    a.util.Region.prototype.contains = function (b) {
        return (b.left >= this.left && b.right <= this.right && b.top >= this.top && b.bottom <= this.bottom);
    };
    a.util.Region.prototype.getArea = function () {
        return ((this.bottom - this.top) * (this.right - this.left));
    };
    a.util.Region.prototype.intersect = function (c) {
        var e = Math.max(this.top, c.top),
            d = Math.min(this.right, c.right),
            b = Math.min(this.bottom, c.bottom),
            f = Math.max(this.left, c.left);
        if (b >= e && d >= f) {
            return new a.util.Region(e, d, b, f);
        } else {
            return null;
        }
    };
    a.util.Region.prototype.union = function (c) {
        var e = Math.min(this.top, c.top),
            d = Math.max(this.right, c.right),
            b = Math.max(this.bottom, c.bottom),
            f = Math.min(this.left, c.left);
        return new a.util.Region(e, d, b, f);
    };
    a.util.Region.prototype.toString = function () {
        return ("Region {" + "top: " + this.top + ", right: " + this.right + ", bottom: " + this.bottom + ", left: " + this.left + ", height: " + this.height + ", width: " + this.width + "}");
    };
    a.util.Region.getRegion = function (e) {
        var c = a.util.Dom.getXY(e),
            f = c[1],
            d = c[0] + e.offsetWidth,
            b = c[1] + e.offsetHeight,
            g = c[0];
        return new a.util.Region(f, d, b, g);
    };
    a.util.Point = function (b, c) {
        if (a.lang.isArray(b)) {
            c = b[1];
            b = b[0];
        }
        a.util.Point.superclass.constructor.call(this, c, b, c, b);
    };
    a.extend(a.util.Point, a.util.Region);
    (function () {
        var x = a.util,
            y = "clientTop",
            t = "clientLeft",
            p = "parentNode",
            o = "right",
            b = "hasLayout",
            q = "px",
            d = "opacity",
            n = "auto",
            v = "borderLeftWidth",
            s = "borderTopWidth",
            j = "borderRightWidth",
            c = "borderBottomWidth",
            f = "visible",
            h = "transparent",
            l = "height",
            u = "width",
            r = "style",
            e = "currentStyle",
            g = /^width|height$/,
            k = /^(\d[.\d]*)+(em|ex|px|gd|rem|vw|vh|vm|ch|mm|cm|in|pt|pc|deg|rad|ms|s|hz|khz|%){1}?/i,
            m = {
                get: function (C, A) {
                    var B = "",
                        z = C[e][A];
                    if (A === d) {
                        B = x.Dom.getStyle(C, d);
                    } else {
                        if (!z || (z.indexOf && z.indexOf(q) > -1)) {
                            B = z;
                        } else {
                            if (x.Dom.IE_COMPUTED[A]) {
                                B = x.Dom.IE_COMPUTED[A](C, A);
                            } else {
                                if (k.test(z)) {
                                    B = x.Dom.IE.ComputedStyle.getPixel(C, A);
                                } else {
                                    B = z;
                                }
                            }
                        }
                    }
                    return B;
                },
                getOffset: function (C, B) {
                    var z = C[e][B],
                        G = B.charAt(0).toUpperCase() + B.substr(1),
                        F = "offset" + G,
                        E = "pixel" + G,
                        A = "",
                        D;
                    if (z == n) {
                        D = C[F];
                        if (D === undefined) {
                            A = 0;
                        }
                        A = D;
                        if (g.test(B)) {
                            C[r][B] = D;
                            if (C[F] > D) {
                                A = D - (C[F] - D);
                            }
                            C[r][B] = n;
                        }
                    } else {
                        if (!C[r][E] && !C[r][B]) {
                            C[r][B] = z;
                        }
                        A = C[r][E];
                    }
                    return A + q;
                },
                getBorderWidth: function (B, z) {
                    var A = null;
                    if (!B[e][b]) {
                        B[r].zoom = 1;
                    }
                    switch (z) {
                    case s:
                        A = B[y];
                        break;
                    case c:
                        A = B.offsetHeight - B.clientHeight - B[y];
                        break;
                    case v:
                        A = B[t];
                        break;
                    case j:
                        A = B.offsetWidth - B.clientWidth - B[t];
                        break;
                    }
                    return A + q;
                },
                getPixel: function (C, D) {
                    var A = null,
                        z = C[e][o],
                        B = C[e][D];
                    C[r][o] = B;
                    A = C[r].pixelRight;
                    C[r][o] = z;
                    return A + q;
                },
                getMargin: function (A, B) {
                    var z;
                    if (A[e][B] == n) {
                        z = 0 + q;
                    } else {
                        z = x.Dom.IE.ComputedStyle.getPixel(A, B);
                    }
                    return z;
                },
                getVisibility: function (A, B) {
                    var z;
                    while ((z = A[e]) && z[B] == "inherit") {
                        A = A[p];
                    }
                    return (z) ? z[B] : f;
                },
                getColor: function (z, A) {
                    return x.Dom.Color.toRGB(z[e][A]) || h;
                },
                getBorderColor: function (B, C) {
                    var A = B[e],
                        z = A[C] || A.color;
                    return x.Dom.Color.toRGB(x.Dom.Color.toHex(z));
                }
            },
            w = {};
        w.top = w.right = w.bottom = w.left = w[u] = w[l] = m.getOffset;
        w.color = m.getColor;
        w[s] = w[j] = w[c] = w[v] = m.getBorderWidth;
        w.marginTop = w.marginRight = w.marginBottom = w.marginLeft = m.getMargin;
        w.visibility = m.getVisibility;
        w.borderColor = w.borderTopColor = w.borderRightColor = w.borderBottomColor = w.borderLeftColor = m.getBorderColor;
        x.Dom.IE_COMPUTED = w;
        x.Dom.IE_ComputedStyle = m;
    })();
    (function () {
        var d = "toString",
            b = parseInt,
            e = RegExp,
            c = a.util;
        c.Dom.Color = {
            KEYWORDS: {
                black: "000",
                silver: "c0c0c0",
                gray: "808080",
                white: "fff",
                maroon: "800000",
                red: "f00",
                purple: "800080",
                fuchsia: "f0f",
                green: "008000",
                lime: "0f0",
                olive: "808000",
                yellow: "ff0",
                navy: "000080",
                blue: "00f",
                teal: "008080",
                aqua: "0ff"
            },
            re_RGB: /^rgb\(([0-9]+)\s*,\s*([0-9]+)\s*,\s*([0-9]+)\)$/i,
            re_hex: /^#?([0-9A-F]{2})([0-9A-F]{2})([0-9A-F]{2})$/i,
            re_hex3: /([0-9A-F])/gi,
            toRGB: function (f) {
                if (!c.Dom.Color.re_RGB.test(f)) {
                    f = c.Dom.Color.toHex(f);
                }
                if (c.Dom.Color.re_hex.exec(f)) {
                    f = "rgb(" + [b(e.$1, 16), b(e.$2, 16), b(e.$3, 16)].join(", ") + ")";
                }
                return f;
            },
            toHex: function (f) {
                f = c.Dom.Color.KEYWORDS[f] || f;
                if (c.Dom.Color.re_RGB.exec(f)) {
                    var g = (e.$1.length === 1) ? "0" + e.$1 : Number(e.$1),
                        h = (e.$2.length === 1) ? "0" + e.$2 : Number(e.$2),
                        j = (e.$3.length === 1) ? "0" + e.$3 : Number(e.$3);
                    f = [g[d](16), h[d](16), j[d](16)].join("");
                }
                if (f.length < 6) {
                    f = f.replace(c.Dom.Color.re_hex3, "$1$1");
                }
                if (f !== "transparent" && f.indexOf("#") < 0) {
                    f = "#" + f;
                }
                return f.toLowerCase();
            }
        };
    }());
    a.register("dom", a.util.Dom, {
        version: "2.8.0r4",
        build: "2449"
    });
    a.util.CustomEvent = function (e, f, g, b, d) {
        this.type = e;
        this.scope = f || window;
        this.silent = g;
        this.fireOnce = d;
        this.fired = false;
        this.firedWith = null;
        this.signature = b || a.util.CustomEvent.LIST;
        this.subscribers = [];
        if (!this.silent) {}
        var c = "_YUICEOnSubscribe";
        if (e !== c) {
            this.subscribeEvent = new a.util.CustomEvent(c, this, true);
        }
        this.lastError = null;
    };
    a.util.CustomEvent.LIST = 0;
    a.util.CustomEvent.FLAT = 1;
    a.util.CustomEvent.prototype = {
        subscribe: function (e, d, c) {
            if (!e) {
                throw new Error("Invalid callback for subscriber to '" + this.type + "'");
            }
            if (this.subscribeEvent) {
                this.subscribeEvent.fire(e, d, c);
            }
            var b = new a.util.Subscriber(e, d, c);
            if (this.fireOnce && this.fired) {
                this.notify(b, this.firedWith);
            } else {
                this.subscribers.push(b);
            }
        },
        unsubscribe: function (e, c) {
            if (!e) {
                return this.unsubscribeAll();
            }
            var d = false;
            for (var g = 0, b = this.subscribers.length; g < b; ++g) {
                var f = this.subscribers[g];
                if (f && f.contains(e, c)) {
                    this._delete(g);
                    d = true;
                }
            }
            return d;
        },
        fire: function () {
            this.lastError = null;
            var c = [],
                b = this.subscribers.length;
            var g = [].slice.call(arguments, 0),
                h = true,
                e, j = false;
            if (this.fireOnce) {
                if (this.fired) {
                    return true;
                } else {
                    this.firedWith = g;
                }
            }
            this.fired = true;
            if (!b && this.silent) {
                return true;
            }
            if (!this.silent) {}
            var f = this.subscribers.slice();
            for (e = 0; e < b; ++e) {
                var d = f[e];
                if (!d) {
                    j = true;
                } else {
                    h = this.notify(d, g);
                    if (false === h) {
                        if (!this.silent) {}
                        break;
                    }
                }
            }
            return (h !== false);
        },
        notify: function (e, h) {
            var j, c = null,
                f = e.getScope(this.scope),
                b = a.util.Event.throwErrors;
            if (!this.silent) {}
            if (this.signature == a.util.CustomEvent.FLAT) {
                if (h.length > 0) {
                    c = h[0];
                }
                try {
                    j = e.fn.call(f, c, e.obj);
                } catch (d) {
                    this.lastError = d;
                    if (b) {
                        throw d;
                    }
                }
            } else {
                try {
                    j = e.fn.call(f, this.type, h, e.obj);
                } catch (g) {
                    this.lastError = g;
                    if (b) {
                        throw g;
                    }
                }
            }
            return j;
        },
        unsubscribeAll: function () {
            var b = this.subscribers.length,
                c;
            for (c = b - 1; c > -1; c--) {
                this._delete(c);
            }
            this.subscribers = [];
            return b;
        },
        _delete: function (b) {
            var c = this.subscribers[b];
            if (c) {
                delete c.fn;
                delete c.obj;
            }
            this.subscribers.splice(b, 1);
        },
        toString: function () {
            return "CustomEvent: " + "'" + this.type + "', " + "context: " + this.scope;
        }
    };
    a.util.Subscriber = function (b, d, c) {
        this.fn = b;
        this.obj = a.lang.isUndefined(d) ? null : d;
        this.overrideContext = c;
    };
    a.util.Subscriber.prototype.getScope = function (b) {
        if (this.overrideContext) {
            if (this.overrideContext === true) {
                return this.obj;
            } else {
                return this.overrideContext;
            }
        }
        return b;
    };
    a.util.Subscriber.prototype.contains = function (b, c) {
        if (c) {
            return (this.fn == b && this.obj == c);
        } else {
            return (this.fn == b);
        }
    };
    a.util.Subscriber.prototype.toString = function () {
        return "Subscriber { obj: " + this.obj + ", overrideContext: " + (this.overrideContext || "no") + " }";
    };
    if (!a.util.Event) {
        a.util.Event = function () {
            var j = false,
                h = [],
                f = [],
                e = 0,
                l = [],
                d = 0,
                c = {
                    63232: 38,
                    63233: 40,
                    63234: 37,
                    63235: 39,
                    63276: 33,
                    63277: 34,
                    25: 9
                },
                b = a.env.ua.ie,
                k = "focusin",
                g = "focusout";
            return {
                POLL_RETRYS: 500,
                POLL_INTERVAL: 40,
                EL: 0,
                TYPE: 1,
                FN: 2,
                WFN: 3,
                UNLOAD_OBJ: 3,
                ADJ_SCOPE: 4,
                OBJ: 5,
                OVERRIDE: 6,
                CAPTURE: 7,
                lastError: null,
                isSafari: a.env.ua.webkit,
                webkit: a.env.ua.webkit,
                isIE: b,
                _interval: null,
                _dri: null,
                _specialTypes: {
                    focusin: (b ? "focusin" : "focus"),
                    focusout: (b ? "focusout" : "blur")
                },
                DOMReady: false,
                throwErrors: false,
                startInterval: function () {
                    if (!this._interval) {
                        this._interval = a.lang.later(this.POLL_INTERVAL, this, this._tryPreloadAttach, null, true);
                    }
                },
                onAvailable: function (o, s, q, p, r) {
                    var n = (a.lang.isString(o)) ? [o] : o;
                    for (var m = 0; m < n.length; m = m + 1) {
                        l.push({
                            id: n[m],
                            fn: s,
                            obj: q,
                            overrideContext: p,
                            checkReady: r
                        });
                    }
                    e = this.POLL_RETRYS;
                    this.startInterval();
                },
                onContentReady: function (o, n, m, p) {
                    this.onAvailable(o, n, m, p, true);
                },
                onDOMReady: function () {
                    this.DOMReadyEvent.subscribe.apply(this.DOMReadyEvent, arguments);
                },
                _addListener: function (y, A, p, v, r, m) {
                    if (!p || !p.call) {
                        return false;
                    }
                    if (this._isValidCollection(y)) {
                        var o = true;
                        for (var u = 0, s = y.length; u < s; ++u) {
                            o = this.on(y[u], A, p, v, r) && o;
                        }
                        return o;
                    } else {
                        if (a.lang.isString(y)) {
                            var w = this.getEl(y);
                            if (w) {
                                y = w;
                            } else {
                                this.onAvailable(y, function () {
                                    a.util.Event._addListener(y, A, p, v, r, m);
                                });
                                return true;
                            }
                        }
                    }
                    if (!y) {
                        return false;
                    }
                    if ("unload" == A && v !== this) {
                        f[f.length] = [y, A, p, v, r];
                        return true;
                    }
                    var z = y;
                    if (r) {
                        if (r === true) {
                            z = v;
                        } else {
                            z = r;
                        }
                    }
                    var x = function (B) {
                        return p.call(z, a.util.Event.getEvent(B, y), v);
                    };
                    var n = [y, A, p, x, z, v, r, m];
                    var t = h.length;
                    h[t] = n;
                    try {
                        this._simpleAdd(y, A, x, m);
                    } catch (q) {
                        this.lastError = q;
                        this.removeListener(y, A, p);
                        return false;
                    }
                    return true;
                },
                _getType: function (m) {
                    return this._specialTypes[m] || m;
                },
                addListener: function (r, o, m, q, p) {
                    var n = ((o == k || o == g) && !a.env.ua.ie) ? true : false;
                    return this._addListener(r, this._getType(o), m, q, p, n);
                },
                addFocusListener: function (m, n, p, o) {
                    return this.on(m, k, n, p, o);
                },
                removeFocusListener: function (m, n) {
                    return this.removeListener(m, k, n);
                },
                addBlurListener: function (m, n, p, o) {
                    return this.on(m, g, n, p, o);
                },
                removeBlurListener: function (m, n) {
                    return this.removeListener(m, g, n);
                },
                removeListener: function (v, w, p) {
                    var u, r, m;
                    w = this._getType(w);
                    if (typeof v == "string") {
                        v = this.getEl(v);
                    } else {
                        if (this._isValidCollection(v)) {
                            var o = true;
                            for (u = v.length - 1; u > -1; u--) {
                                o = (this.removeListener(v[u], w, p) && o);
                            }
                            return o;
                        }
                    }
                    if (!p || !p.call) {
                        return this.purgeElement(v, false, w);
                    }
                    if ("unload" == w) {
                        for (u = f.length - 1; u > -1; u--) {
                            m = f[u];
                            if (m && m[0] == v && m[1] == w && m[2] == p) {
                                f.splice(u, 1);
                                return true;
                            }
                        }
                        return false;
                    }
                    var t = null;
                    var s = arguments[3];
                    if ("undefined" === typeof s) {
                        s = this._getCacheIndex(h, v, w, p);
                    }
                    if (s >= 0) {
                        t = h[s];
                    }
                    if (!v || !t) {
                        return false;
                    }
                    var n = t[this.CAPTURE] === true ? true : false;
                    try {
                        this._simpleRemove(v, w, t[this.WFN], n);
                    } catch (q) {
                        this.lastError = q;
                        return false;
                    }
                    delete h[s][this.WFN];
                    delete h[s][this.FN];
                    h.splice(s, 1);
                    return true;
                },
                getTarget: function (o, m) {
                    var n = o.target || o.srcElement;
                    return this.resolveTextNode(n);
                },
                resolveTextNode: function (m) {
                    try {
                        if (m && 3 == m.nodeType) {
                            return m.parentNode;
                        }
                    } catch (n) {}
                    return m;
                },
                getPageX: function (m) {
                    var n = m.pageX;
                    if (!n && 0 !== n) {
                        n = m.clientX || 0;
                        if (this.isIE) {
                            n += this._getScrollLeft();
                        }
                    }
                    return n;
                },
                getPageY: function (n) {
                    var m = n.pageY;
                    if (!m && 0 !== m) {
                        m = n.clientY || 0;
                        if (this.isIE) {
                            m += this._getScrollTop();
                        }
                    }
                    return m;
                },
                getXY: function (m) {
                    return [this.getPageX(m), this.getPageY(m)];
                },
                getRelatedTarget: function (m) {
                    var n = m.relatedTarget;
                    if (!n) {
                        if (m.type == "mouseout") {
                            n = m.toElement;
                        } else {
                            if (m.type == "mouseover") {
                                n = m.fromElement;
                            }
                        }
                    }
                    return this.resolveTextNode(n);
                },
                getTime: function (o) {
                    if (!o.time) {
                        var m = new Date().getTime();
                        try {
                            o.time = m;
                        } catch (n) {
                            this.lastError = n;
                            return m;
                        }
                    }
                    return o.time;
                },
                stopEvent: function (m) {
                    this.stopPropagation(m);
                    this.preventDefault(m);
                },
                stopPropagation: function (m) {
                    if (m.stopPropagation) {
                        m.stopPropagation();
                    } else {
                        m.cancelBubble = true;
                    }
                },
                preventDefault: function (m) {
                    if (m.preventDefault) {
                        m.preventDefault();
                    } else {
                        m.returnValue = false;
                    }
                },
                getEvent: function (p, n) {
                    var m = p || window.event;
                    if (!m) {
                        var o = this.getEvent.caller;
                        while (o) {
                            m = o.arguments[0];
                            if (m && Event == m.constructor) {
                                break;
                            }
                            o = o.caller;
                        }
                    }
                    return m;
                },
                getCharCode: function (m) {
                    var n = m.keyCode || m.charCode || 0;
                    if (a.env.ua.webkit && (n in c)) {
                        n = c[n];
                    }
                    return n;
                },
                _getCacheIndex: function (s, p, o, q) {
                    for (var r = 0, m = s.length; r < m; r = r + 1) {
                        var n = s[r];
                        if (n && n[this.FN] == q && n[this.EL] == p && n[this.TYPE] == o) {
                            return r;
                        }
                    }
                    return -1;
                },
                generateId: function (n) {
                    var m = n.id;
                    if (!m) {
                        m = "yuievtautoid-" + d;
                        ++d;
                        n.id = m;
                    }
                    return m;
                },
                _isValidCollection: function (m) {
                    try {
                        return (m && typeof m !== "string" && m.length && !m.tagName && !m.alert && typeof m[0] !== "undefined");
                    } catch (n) {
                        return false;
                    }
                },
                elCache: {},
                getEl: function (m) {
                    return (typeof m === "string") ? document.getElementById(m) : m;
                },
                clearCache: function () {},
                DOMReadyEvent: new a.util.CustomEvent("DOMReady", a, 0, 0, 1),
                _load: function (m) {
                    if (!j) {
                        j = true;
                        var n = a.util.Event;
                        n._ready();
                        n._tryPreloadAttach();
                    }
                },
                _ready: function (m) {
                    var n = a.util.Event;
                    if (!n.DOMReady) {
                        n.DOMReady = true;
                        n.DOMReadyEvent.fire();
                        n._simpleRemove(document, "DOMContentLoaded", n._ready);
                    }
                },
                _tryPreloadAttach: function () {
                    if (l.length === 0) {
                        e = 0;
                        if (this._interval) {
                            this._interval.cancel();
                            this._interval = null;
                        }
                        return;
                    }
                    if (this.locked) {
                        return;
                    }
                    if (this.isIE) {
                        if (!this.DOMReady) {
                            this.startInterval();
                            return;
                        }
                    }
                    this.locked = true;
                    var p = !j;
                    if (!p) {
                        p = (e > 0 && l.length > 0);
                    }
                    var q = [];
                    var o = function (v, u) {
                        var w = v;
                        if (u.overrideContext) {
                            if (u.overrideContext === true) {
                                w = u.obj;
                            } else {
                                w = u.overrideContext;
                            }
                        }
                        u.fn.call(w, u.obj);
                    };
                    var m, n, r, s, t = [];
                    for (m = 0, n = l.length; m < n; m = m + 1) {
                        r = l[m];
                        if (r) {
                            s = this.getEl(r.id);
                            if (s) {
                                if (r.checkReady) {
                                    if (j || s.nextSibling || !p) {
                                        t.push(r);
                                        l[m] = null;
                                    }
                                } else {
                                    o(s, r);
                                    l[m] = null;
                                }
                            } else {
                                q.push(r);
                            }
                        }
                    }
                    for (m = 0, n = t.length; m < n; m = m + 1) {
                        r = t[m];
                        o(this.getEl(r.id), r);
                    }
                    e--;
                    if (p) {
                        for (m = l.length - 1; m > -1; m--) {
                            r = l[m];
                            if (!r || !r.id) {
                                l.splice(m, 1);
                            }
                        }
                        this.startInterval();
                    } else {
                        if (this._interval) {
                            this._interval.cancel();
                            this._interval = null;
                        }
                    }
                    this.locked = false;
                },
                purgeElement: function (r, q, o) {
                    var t = (a.lang.isString(r)) ? this.getEl(r) : r;
                    var p = this.getListeners(t, o),
                        s, n;
                    if (p) {
                        for (s = p.length - 1; s > -1; s--) {
                            var m = p[s];
                            this.removeListener(t, m.type, m.fn);
                        }
                    }
                    if (q && t && t.childNodes) {
                        for (s = 0, n = t.childNodes.length; s < n; ++s) {
                            this.purgeElement(t.childNodes[s], q, o);
                        }
                    }
                },
                getListeners: function (t, v) {
                    var q = [],
                        u;
                    if (!v) {
                        u = [h, f];
                    } else {
                        if (v === "unload") {
                            u = [f];
                        } else {
                            v = this._getType(v);
                            u = [h];
                        }
                    }
                    var o = (a.lang.isString(t)) ? this.getEl(t) : t;
                    for (var r = 0; r < u.length; r = r + 1) {
                        var m = u[r];
                        if (m) {
                            for (var p = 0, n = m.length; p < n; ++p) {
                                var s = m[p];
                                if (s && s[this.EL] === o && (!v || v === s[this.TYPE])) {
                                    q.push({
                                        type: s[this.TYPE],
                                        fn: s[this.FN],
                                        obj: s[this.OBJ],
                                        adjust: s[this.OVERRIDE],
                                        scope: s[this.ADJ_SCOPE],
                                        index: p
                                    });
                                }
                            }
                        }
                    }
                    return (q.length) ? q : null;
                },
                _unload: function (n) {
                    var t = a.util.Event,
                        q, r, s, o, p, m = f.slice(),
                        u;
                    for (q = 0, o = f.length; q < o; ++q) {
                        s = m[q];
                        if (s) {
                            u = window;
                            if (s[t.ADJ_SCOPE]) {
                                if (s[t.ADJ_SCOPE] === true) {
                                    u = s[t.UNLOAD_OBJ];
                                } else {
                                    u = s[t.ADJ_SCOPE];
                                }
                            }
                            s[t.FN].call(u, t.getEvent(n, s[t.EL]), s[t.UNLOAD_OBJ]);
                            m[q] = null;
                        }
                    }
                    s = null;
                    u = null;
                    f = null;
                    if (h) {
                        for (r = h.length - 1; r > -1; r--) {
                            s = h[r];
                            if (s) {
                                t.removeListener(s[t.EL], s[t.TYPE], s[t.FN], r);
                            }
                        }
                        s = null;
                    }
                    t._simpleRemove(window, "unload", t._unload);
                },
                _getScrollLeft: function () {
                    return this._getScroll()[1];
                },
                _getScrollTop: function () {
                    return this._getScroll()[0];
                },
                _getScroll: function () {
                    var n = document.documentElement,
                        m = document.body;
                    if (n && (n.scrollTop || n.scrollLeft)) {
                        return [n.scrollTop, n.scrollLeft];
                    } else {
                        if (m) {
                            return [m.scrollTop, m.scrollLeft];
                        } else {
                            return [0, 0];
                        }
                    }
                },
                regCE: function () {},
                _simpleAdd: function () {
                    if (window.addEventListener) {
                        return function (p, o, m, n) {
                            p.addEventListener(o, m, (n));
                        };
                    } else {
                        if (window.attachEvent) {
                            return function (p, o, m, n) {
                                p.attachEvent("on" + o, m);
                            };
                        } else {
                            return function () {};
                        }
                    }
                }(),
                _simpleRemove: function () {
                    if (window.removeEventListener) {
                        return function (p, o, m, n) {
                            p.removeEventListener(o, m, (n));
                        };
                    } else {
                        if (window.detachEvent) {
                            return function (m, o, n) {
                                m.detachEvent("on" + o, n);
                            };
                        } else {
                            return function () {};
                        }
                    }
                }()
            };
        }();
        (function () {
            var b = a.util.Event;
            b.on = b.addListener;
            b.onFocus = b.addFocusListener;
            b.onBlur = b.addBlurListener;
            if (b.isIE) {
                if (self !== self.top) {
                    document.onreadystatechange = function () {
                        if (document.readyState == "complete") {
                            document.onreadystatechange = null;
                            b._ready();
                        }
                    };
                } else {
                    a.util.Event.onDOMReady(a.util.Event._tryPreloadAttach, a.util.Event, true);
                    var c = document.createElement("p");
                    b._dri = setInterval(function () {
                        try {
                            c.doScroll("left");
                            clearInterval(b._dri);
                            b._dri = null;
                            b._ready();
                            c = null;
                        } catch (d) {}
                    }, b.POLL_INTERVAL);
                }
            } else {
                if (b.webkit && b.webkit < 525) {
                    b._dri = setInterval(function () {
                        var d = document.readyState;
                        if ("loaded" == d || "complete" == d) {
                            clearInterval(b._dri);
                            b._dri = null;
                            b._ready();
                        }
                    }, b.POLL_INTERVAL);
                } else {
                    b._simpleAdd(document, "DOMContentLoaded", b._ready);
                }
            }
            b._simpleAdd(window, "load", b._load);
            b._simpleAdd(window, "unload", b._unload);
            b._tryPreloadAttach();
        })();
    }
    a.util.EventProvider = function () {};
    a.util.EventProvider.prototype = {
        __yui_events: null,
        __yui_subscribers: null,
        subscribe: function (b, f, c, d) {
            this.__yui_events = this.__yui_events || {};
            var e = this.__yui_events[b];
            if (e) {
                e.subscribe(f, c, d);
            } else {
                this.__yui_subscribers = this.__yui_subscribers || {};
                var g = this.__yui_subscribers;
                if (!g[b]) {
                    g[b] = [];
                }
                g[b].push({
                    fn: f,
                    obj: c,
                    overrideContext: d
                });
            }
        },
        unsubscribe: function (g, e, c) {
            this.__yui_events = this.__yui_events || {};
            var b = this.__yui_events;
            if (g) {
                var d = b[g];
                if (d) {
                    return d.unsubscribe(e, c);
                }
            } else {
                var h = true;
                for (var f in b) {
                    if (a.lang.hasOwnProperty(b, f)) {
                        h = h && b[f].unsubscribe(e, c);
                    }
                }
                return h;
            }
            return false;
        },
        unsubscribeAll: function (b) {
            return this.unsubscribe(b);
        },
        createEvent: function (h, c) {
            this.__yui_events = this.__yui_events || {};
            var e = c || {},
                f = this.__yui_events,
                d;
            if (f[h]) {} else {
                d = new a.util.CustomEvent(h, e.scope || this, e.silent, a.util.CustomEvent.FLAT, e.fireOnce);
                f[h] = d;
                if (e.onSubscribeCallback) {
                    d.subscribeEvent.subscribe(e.onSubscribeCallback);
                }
                this.__yui_subscribers = this.__yui_subscribers || {};
                var b = this.__yui_subscribers[h];
                if (b) {
                    for (var g = 0; g < b.length; ++g) {
                        d.subscribe(b[g].fn, b[g].obj, b[g].overrideContext);
                    }
                }
            }
            return f[h];
        },
        fireEvent: function (e) {
            this.__yui_events = this.__yui_events || {};
            var c = this.__yui_events[e];
            if (!c) {
                return null;
            }
            var b = [];
            for (var d = 1; d < arguments.length; ++d) {
                b.push(arguments[d]);
            }
            return c.fire.apply(c, b);
        },
        hasEvent: function (b) {
            if (this.__yui_events) {
                if (this.__yui_events[b]) {
                    return true;
                }
            }
            return false;
        }
    };
    (function () {
        var b = a.util.Event,
            c = a.lang;
        a.util.KeyListener = function (k, e, j, h) {
            if (!k) {} else {
                if (!e) {} else {
                    if (!j) {}
                }
            }
            if (!h) {
                h = a.util.KeyListener.KEYDOWN;
            }
            var g = new a.util.CustomEvent("keyPressed");
            this.enabledEvent = new a.util.CustomEvent("enabled");
            this.disabledEvent = new a.util.CustomEvent("disabled");
            if (c.isString(k)) {
                k = document.getElementById(k);
            }
            if (c.isFunction(j)) {
                g.subscribe(j);
            } else {
                g.subscribe(j.fn, j.scope, j.correctScope);
            }
            function f(o, p) {
                if (!e.shift) {
                    e.shift = false;
                }
                if (!e.alt) {
                    e.alt = false;
                }
                if (!e.ctrl) {
                    e.ctrl = false;
                }
                if (o.shiftKey == e.shift && o.altKey == e.alt && o.ctrlKey == e.ctrl) {
                    var n, q = e.keys,
                        l;
                    if (a.lang.isArray(q)) {
                        for (var m = 0; m < q.length; m++) {
                            n = q[m];
                            l = b.getCharCode(o);
                            if (n == l) {
                                g.fire(l, o);
                                break;
                            }
                        }
                    } else {
                        l = b.getCharCode(o);
                        if (q == l) {
                            g.fire(l, o);
                        }
                    }
                }
            }
            this.enable = function () {
                if (!this.enabled) {
                    b.on(k, h, f);
                    this.enabledEvent.fire(e);
                }
                this.enabled = true;
            };
            this.disable = function () {
                if (this.enabled) {
                    b.removeListener(k, h, f);
                    this.disabledEvent.fire(e);
                }
                this.enabled = false;
            };
            this.toString = function () {
                return "KeyListener [" + e.keys + "] " + k.tagName + (k.id ? "[" + k.id + "]" : "");
            };
        };
        var d = a.util.KeyListener;
        d.KEYDOWN = "keydown";
        d.KEYUP = "keyup";
        d.KEY = {
            ALT: 18,
            BACK_SPACE: 8,
            CAPS_LOCK: 20,
            CONTROL: 17,
            DELETE: 46,
            DOWN: 40,
            END: 35,
            ENTER: 13,
            ESCAPE: 27,
            HOME: 36,
            LEFT: 37,
            META: 224,
            NUM_LOCK: 144,
            PAGE_DOWN: 34,
            PAGE_UP: 33,
            PAUSE: 19,
            PRINTSCREEN: 44,
            RIGHT: 39,
            SCROLL_LOCK: 145,
            SHIFT: 16,
            SPACE: 32,
            TAB: 9,
            UP: 38
        };
    })();
    a.register("event", a.util.Event, {
        version: "2.8.0r4",
        build: "2449"
    });
    a.register("yahoo-dom-event", a, {
        version: "2.8.0r4",
        build: "2449"
    });
    a.util.Event._load();
    return a;
})();
var isMWPSupported = true;
var badUserAgentStrings = ["NETSCAPE6", "NETSCAPE/7", "(IPAD;", "(IPHONE;", "(IPOD;"];
if (navigator) {
    var len = badUserAgentStrings.length;
    for (var i = 0; i < len; i++) {
        if (navigator.userAgent.toUpperCase().indexOf(badUserAgentStrings[i]) !== -1) {
            isMWPSupported = false;
        }
    }
}
if (isMWPSupported === true) {
    if (typeof YAHOO.mediaplayer == "undefined") {
        YAHOO.namespace("YAHOO.mediaplayer");
    }
    YAHOO.mediaplayer.playerAlreadyLoaded = function () {
        if (YAHOO.mediaplayer.bLoaded === true) {
            return true;
        }
        YAHOO.mediaplayer.bLoaded = true;
        return false;
    };
    if (YAHOO.mediaplayer.playerAlreadyLoaded() !== true) {
        YAHOO.mediaplayer.partnerId = "42858483";
        if (typeof YMPParams == "undefined") {
            window.YMPParams = {};
        }
        YMPParams["assetsroot"] = YMPParams["assetsroot"] || "http://l.yimg.com/pb/webplayer" + "/" + "0.5.5";
        YMPParams["wsroot"] = YMPParams["wsroot"] || "http://ws.mediaplayer.yahoo.com";
        YMPParams["wwwroot"] = YMPParams["wwwroot"] || "http://mediaplayer.yahoo.com";
        YMPParams["build_number"] = "0.5.5";
        if (typeof YMPParams === "object" && YMPParams.logging === true) {
            if (typeof(YAHOO) === "undefined" || typeof(YAHOO.ULT) === "undefined") {
                var ultScript = document.createElement("script");
                ultScript.type = "text/javascript";
                ultScript.src = "https://127.0.0.1:1080/Partner/Scripts/player/ylc_1.9.js";
                document.getElementsByTagName("head")[0].appendChild(ultScript);
            }
        }
        YAHOO.mediaplayer.loadPlayerScript = function () {
            if (Boolean(arguments.callee.bCalled)) {
                window.status = "asyncLoadPlayer Already Called! (webplayerloader)";
                return;
            }
            arguments.callee.bCalled = true;

            function a() {
                return "https://127.0.0.1:1080/Partner/Scripts/player/player-min.js";
            }
            var b = a();
            if (typeof(b) == "string" && b.length > 0) {
                YAHOO.mediaplayer.elPlayerSource = document.createElement("script");
                YAHOO.mediaplayer.elPlayerSource.type = "text/javascript";
                YAHOO.mediaplayer.elPlayerSource.src = b;
                document.getElementsByTagName("head")[0].appendChild(YAHOO.mediaplayer.elPlayerSource);
            }
        };
        YAHOO.ympyui.util.Event.addListener(window, "load", YAHOO.mediaplayer.loadPlayerScript);
        YAHOO.namespace("YAHOO.MediaPlayer");
        YAHOO.MediaPlayer = function () {
            this.controller = null;
        };
        YAHOO.MediaPlayer.onAPIReady = new YAHOO.ympyui.util.CustomEvent("onAPIReady", null, false, YAHOO.ympyui.util.CustomEvent.FLAT);
        YAHOO.MediaPlayer.isAPIReady = false;
        YAHOO.MediaPlayer.onAPIReadySubscribed = function (b, a) {
            if (YAHOO.MediaPlayer.isAPIReady === true) {
                if (typeof(a[0]) === "function") {
                    a[0]();
                }
            }
        };
        YAHOO.MediaPlayer.onAPIReady.subscribeEvent.subscribe(YAHOO.MediaPlayer.onAPIReadySubscribed);
    }
}