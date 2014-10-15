
/*设置Cookie*/
function SetCookie(c_name, value, expiredays) {
    var exdate = new Date()
    exdate.setDate(exdate.getDate() + expiredays)
    document.cookie = c_name + "=" + escape(value) + ((expiredays == null) ? "" : ";expires=" + exdate.toGMTString()) + ";path=/";
}

/*读取Cookie*/
function GetCookie(c_name) {
    if (document.cookie.length > 0) {
        c_start = document.cookie.indexOf(c_name + "=")
        if (c_start != -1) {
            c_start = c_start + c_name.length + 1
            c_end = document.cookie.indexOf(";", c_start)
            if (c_end == -1) c_end = document.cookie.length
            return unescape(document.cookie.substring(c_start, c_end))
        }
    }
    return ""
}

/*读取带子键Cookie*/
function GetChildCookie(c_name, c_key) {
    var c_value = "";
    if (document.cookie.length > 0) {
        c_start = document.cookie.indexOf(c_name + "=")
        if (c_start != -1) {
            c_start = c_start + c_name.length + 1
            c_end = document.cookie.indexOf(";", c_start)
            if (c_end == -1) c_end = document.cookie.length
            var item = unescape(document.cookie.substring(c_start, c_end)).split("&");
            for (var i = 0; i < item.length; i++) {
                var arr = item[i].split("=");
                if (c_key == arr[0]) {
                    c_value = arr[1];
                    break;
                }
            } 
        }
    }
    return c_value;
}

/*删除Cookie*/
function DelCookie(name) {
    var exp = new Date();
    exp.setTime(exp.getTime() - 1);
    document.cookie = name + "=; expires=" + exp.toGMTString();
}

/*读取URL参数*/
function GetQueryString(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return unescape(r[2]); return null;
}

/*判断字串长度*/
function StrLen(strTemp) {
    var i, sum;
    sum = 0;
    for (i = 0; i < strTemp.length; i++) {
        if ((strTemp.charCodeAt(i) >= 0) && (strTemp.charCodeAt(i) <= 255))
            sum = sum + 1;
        else
            sum = sum + 2;
    }
    return sum;
}

/*获取字符串长度*/
String.prototype.GetLength = function () {
    var cArr = this.match(/[^\x00-\xff]/ig);
    return this.length + (cArr == null ? 0 : cArr.length);
}

/*获取数组是否包含指定字符*/
Array.prototype.Contains = function (item) {
    return RegExp(item).test(this);
};

/*删除字符串前后空格*/
String.prototype.Trim = function () {
    var m = this.match(/^\s*(\S+(\s+\S+)*)\s*$/);
    return (m == null) ? "" : m[1];
}

/*判断字符串是否为空*/
String.prototype.IsNull = function () {
    if (this.replace(/(^\s*)|(\s*$)/g, '').length <= 0) {//为空
        return true;
    }
    else {//不为空
        return false;
    }
}
//日期转换
function GetOtherTime(data) {
    var str = data.split(".")[0].replace(/T/g, "   ");
    var da = parseDate(str);
    return da.getFullYear() + "年" + (da.getMonth() + 1) + "月" + da.getDate() + "日" + da.getHours() + ":" + da.getMinutes() + ":" + da.getSeconds();
}
function GetOtherDate(data) {
    var str = data.split(".")[0].replace(/T/g, "   ").replace(/Z/g, "");
    var da = parseDate(str);
    //da = da == null ?new Date() : da;
    var month = (da.getMonth() + 1);
    month = month.toString().length == 1 ? "0" + month : month;
    var day = da.getDate();
    day = day.toString().length == 1 ? "0" + day : day;
    return da.getFullYear() + "-" + month + "-" + day ;
}
function parseDate(str) {
    if (typeof str == 'string') {
        var results = str.match(/^ *(\d{4})-(\d{1,2})-(\d{1,2}) *$/);
        if (results && results.length > 3)
            return new Date(parseInt(results[1]), parseInt(results[2]) - 1, parseInt(results[3]));
        results = str.match(/^ *(\d{4})-(\d{1,2})-(\d{1,2}) +(\d{1,2}):(\d{1,2}):(\d{1,2}) *$/);
        if (results && results.length > 6)
            return new Date(parseInt(results[1]), parseInt(results[2]) - 1, parseInt(results[3]), parseInt(results[4]), parseInt(results[5]), parseInt(results[6]));
        results = str.match(/^ *(\d{4})-(\d{1,2})-(\d{1,2}) +(\d{1,2}):(\d{1,2}):(\d{1,2})\.(\d{1,9}) *$/);
        if (results && results.length > 7)
            return new Date(parseInt(results[1]), parseInt(results[2]) - 1, parseInt(results[3]), parseInt(results[4]), parseInt(results[5]), parseInt(results[6]), parseInt(results[7]));
    }
    return null;
}

function checkAll() {
    var code_Values = document.getElementsByTagName("input");
    if ($("#hd_check").val() == "0") {
        for (i = 0; i < code_Values.length; i++) {
            if (code_Values[i].type == "checkbox") {
                code_Values[i].checked = true;
            }
        }
        $("#hd_check").val("1");
    }
    else {
        for (i = 0; i < code_Values.length; i++) {
            if (code_Values[i].type == "checkbox") {
                code_Values[i].checked = false;
            }
        }
        $("#hd_check").val("0");
    }

}