(function (window) {
    var klwTools = {
        tipTimer: null,

        /*
        * 判断是不是方法类型
        * obj: 变量
        */
        isFunction: function (obj) {
            return typeof obj === "function";
        },

        /*
        * 判断是不是字符串类型
        * obj: 变量
        */
        isString: function (obj) {
            return typeof obj === "string";
        },

        /*
        * 判断是不是数组类型
        * obj: 变量
        */
        isArray: function (obj) {
            return Object.prototype.toString.call(obj) === "[object Array]";
        },

        /*
        * 判断是不是bool值类型
        * obj: 变量
        */
        isBool: function (obj) {
            return typeof obj === "boolean";
        },

        /*
        * 判断是不是数字类型
        * obj: 变量
        */
        isNumber: function (obj) {
            return typeof obj === "number";
        },

        /*
        * 判断是不是对象/类
        * obj: 变量
        */
        isObject: function (obj) {
            return typeof obj === "object";
        },

        getSession: function (key) {
            var data = sessionStorage.getItem(key);
            if (data == undefined || data == null || data == '') {
                return '';
            }
            return data;
        },

        setSession: function (key, objData) {
            window.sessionStorage.removeItem(key);
            if (this.isString(objData)) {
                window.sessionStorage.setItem(key, objData);
            } else {
                window.sessionStorage.setItem(key, JSON.stringify(objData));
            }
        },

        getStorage: function (key) {
            var data = localStorage.getItem(key);
            if (data == undefined || data == null || data == '') {
                return '';
            }
            return data;
        },

        setStorage: function (key, objData) {
            window.localStorage.removeItem(key);
            if (this.isString(objData)) {
                window.localStorage.setItem(key, objData);
            } else {
                window.localStorage.setItem(key, JSON.stringify(objData));
            }
        },
    };
    window.KLW_Tools = klwTools;
})(window);

/**
 * 获取登录信息
 */
function getLoginInfo() {
    var userInfoJSON = KLW_Tools.getStorage("LONGIN_INFO_ZN");
    if (KLW_Tools.isString(userInfo) && userInfoJSON !== '') {
        var result = saveLoginInfo(userInfoJSON);
        if (!result) {

        }
    } else {    //没有登录

    }
}

/**
 * 获得随机字符串
 * @param {any} len 长度
 */
function randomString(len) {
    len = len || 32;
    var $chars = 'ABCDEFGHJKMNPQRSTWXYZabcdefhijkmnprstwxyz2345678';
    var maxPos = $chars.length;
    var pwd = '';
    for (var i = 0; i < len; i++) {
        pwd += $chars.charAt(Math.floor(Math.random() * maxPos));
    }
    return pwd;
}

/*
 * 移除字符串左右两边空格
 */
String.prototype.trim = function () {
    return this.replace(/(^\s*)|(\s*$)/g, "");
}

/*
 * 移除字符串左边的空格
 */
String.prototype.ltrim = function () {
    return this.replace(/(^\s*)/g, "");
}

/*
 * 移除字符串右边的空格
 */
String.prototype.rtrim = function () {
    return this.replace(/(\s*$)/g, "");
}

/*
 * 移除日期字符串里面的 T 
 */
String.prototype.formatDate = function () {
    if (this == null || this === '') {
        return '';
    }
    return this.substring(0, 19).replace("T", " ");
}

/*
 * 验证是不是手机号
 */
String.prototype.isPhone = function () {
    if (!(/^1[345789]\d{9}$/.test(this))) {
        return false;
    }
    return true;
}

/*
 * 验证是不是邮箱
 */
String.prototype.isEmail = function () {
    if (!(/^([a-zA-Z0-9]+[_|\_|\.]?)*[a-zA-Z0-9]+@([a-zA-Z0-9]+[_|\_|\.]?)*[a-zA-Z0-9]+\.[a-zA-Z]{2,3}$/.test(this))) {
        return false;
    }
    return true;
}