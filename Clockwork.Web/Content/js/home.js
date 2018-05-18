window.onload = GetAllTimeZones();

function GetLatest() {
    var xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            var item = JSON.parse(this.responseText);

            document.getElementById("latestTime").innerHTML = formatDateTime(new Date(item.time).toString());
            CloseHistory();
        }
    };
    xhttp.open("GET", window.APIBaseUrl + '/api/currenttime/latest', true);
    xhttp.setRequestHeader("Content-type", "application/json");
    xhttp.send();
}

function GetAll() {
    var xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            var response = JSON.parse(this.responseText);
            var historyList = document.getElementById("historyList");
            var historyItems = document.getElementById("historyItems");

            var pIndex = historyList.style.height.indexOf('p');

            if (pIndex < 0 || historyList.style.height.substr(0, pIndex) < 20)
                OpenHistory(response);
            else
                CloseHistory()
        }
    };
    xhttp.open("GET", window.APIBaseUrl + '/api/currenttime/all', true);
    xhttp.setRequestHeader("Content-type", "application/json");
    xhttp.send();
}

function CloseHistory() {
    var historyList = document.getElementById("historyList");
    var historyItems = document.getElementById("historyItems");

    historyItems.innerHTML = "";
    historyList.style.height = '18px';
}

function OpenHistory(items) {
    var historyList = document.getElementById("historyList");
    var historyItems = document.getElementById("historyItems");

    historyList.style.height = (items.length * 154) + 'px';
    historyItems.innerHTML = GenerateList(items);
}

function GenerateList(items) {
    var returnVal = "<ul style='padding-left: 5px; padding-right: 5px;'>";

    for (var i = 0; i < items.length; i++) {
        returnVal += "<li style='list-style:none;'>";
        returnVal += "<p style=''><strong>Id</strong>: " + items[i].currentTimeQueryId + "</p>";
        returnVal += "<p style=''><strong>Ip</strong>: " + items[i].clientIp + "</p>";
        returnVal += "<p style=''><strong>Local Time</strong>: " + formatDateTime(new Date(items[i].time).toString()) + "</p>";
        returnVal += "<p style=''><strong>utc Time</strong>: " + new Date(items[i].utcTime).toUTCString() + "</p>";
        returnVal += "</li>";

        if (i != items.length - 1)
            returnVal += "<hr>"
    }

    returnVal += "</ul>"

    return returnVal;
}

function GetAllTimeZones() {
    var xhttp = new XMLHttpRequest();

    xhttp.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200 && !this.hasTimeZones) {
            var selector = document.getElementById('tzSelector');
            selector.innerHTML = generateTimeZoneList(JSON.parse(this.responseText));
            this.hasTimeZones = true;
        }
    };

    xhttp.open("GET", window.APIBaseUrl + '/api/timeZones', true);
    xhttp.setRequestHeader("Content-type", "application/json");
    xhttp.send();
}

function generateTimeZoneList(items) {
    var list = "";

    for (var i = 0; i < items.length; i++)
        list += "<option value='" + items[i] + "'>" + items[i];

    return list;
}

function updateTimeZone() {

    var xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            CloseHistory();
            GetLatest();
        }
    };

    var selector = document.getElementById('tzSelector');
    var uri = window.APIBaseUrl + '/api/update/' + encodeParameter(selector.value)

    xhttp.open("GET", uri, true);
    xhttp.setRequestHeader("Content-type", "application/json");
    xhttp.send();
}

function encodeParameter(param) {
    return param.replace('/', '%2F');
}

function formatDateTime(param) {
    return param.substr(0, param.indexOf('GMT') - 1);
}