const api = "api/products/";
const uri = "api/orders/";
const url = "api/orderStrings/";


var items = null;
var order;
var x = "";
var j = 0;

document.addEventListener("DOMContentLoaded", function (event) {
    // Обработка кликов по кнопкам
    document.getElementById("loginBtn").addEventListener("click", logIn);
    document.getElementById("logoffBtn").addEventListener("click", logOff);
});

document.addEventListener("DOMContentLoaded", function (event) {
   // Product();
    loadProduct();
    getCurrentUser();
});

//function Product() {
//    var i;
//    var request = new XMLHttpRequest();
//    request.open("GET", url, false);
//    request.onload = function () {
//        var items = null;
//        items = JSON.parse(request.responseText);
//        for (i in items) {

//            if (items[i].orderId == order)
//            {
//                loadProduct(items.productId, items.id);
//            }
//        }

//        };
//    request.send();
//}
//функция для загрузки товаров
function loadProduct() {
    var i, y="";
    var request = new XMLHttpRequest();
    request.open("GET", api, false);
    request.onload = function () {
        var items = null;
        items = JSON.parse(request.responseText);
        
        
        for (i in items) {
            y += '<div class="col-4">';
            y += "<hr>";
            y += "<img src=\"" + items[i].image + "\" width=\"200\" height=\"250\">";
          //  y += "<h4> " + items[i].productId + "</h4>";
            y += "<h4> " + items[i].name + "</h4>";
            y += "<h4> " + " Цена " + items[i].costs + "</h4>";
            y += "<button type='button' class='btn btn-sm btn-outline-secondary' onclick='idorder(" + items[i].productId+", "+ items[i].costs +");'>Добавить в корзину</button>";
            y += "<h4> " + "  " + "</h4>";
            y += "<button type='button' class='btn btn-sm btn-outline-secondary' onclick='deleteproduct(" + items[i].productId + ");'>Удалить</button>";
            y += '</div>';

        }
        document.getElementById("productsDiv").innerHTML = y;
    };
    request.send();
}
//функция для загрузки строки заказа
function getStrings(id) {
    var i;
    var request = new XMLHttpRequest();
    request.open("GET", url, false);
    request.onload = function () {
        var items = null;
        items = JSON.parse(request.responseText);
        for (i in items) {
            if (id == items[i].orderId)
            {
            x += "<h4> " + items[i].orderStringId + "</h4>";
            x += "<h4> " + " Кол-во "+ items[i].count + " Цена " + items[i].cost + "</h4>";
            x += "<button type='button' class='btn btn-sm btn-outline-secondary' onclick='deletestring(" + items[i].orderStringId + ");'>Удалить</button>";
            }
        }
        document.getElementById("ordersDiv").innerHTML = x;
    };
    request.send();
}
//функция удаление товара
function deleteproduct(id) {
    var request = new XMLHttpRequest();
    var url = api + id;
    request.open("DELETE", url, false);
    request.onload = function () {
        // loadOrders();
        // Обработка кода ответа
        var msg = "";
        if (request.status === 401) {
            msg = "У вас не хватает прав для удаления";
        } else if (request.status === 201) {
            msg = "Запись удалена";
            getBlogs();
        } else {
            msg = "Неизвестная ошибка";
        }
        document.querySelector("#actionMsg").innerHTML = msg;
    };
    request.send();
}
//функция проверки актуальности заказа, при нажатии на кнопку добавить в корзину, проверяется, есть ли у пользователя активный заказ
function idorder(pid,cost) {
    var i;
    var request = new XMLHttpRequest();
    request.open("GET", uri, false);
    request.onload = function () {
        var items = null;
        items = JSON.parse(request.responseText);
        for (i in items) {

            if (items[i].act == 1) {
                j = items[i].orderId;
                order = items[i].orderId;
            }
        }
        //если нет активного заказа, он создается
        if (j == 0)
            createOrder(pid, cost);
        else add(j, cost, pid); //иначе идет добавление в заказ строки заказа

    };

    request.send();
}
//функция добавления строки заказа
function add(id,cost,pid) {
    var request = new XMLHttpRequest();
    request.open("POST", url, false);

    var itm = {
        "productId": pid,
        "orderId": id,
        "count": 1,
        "cost": cost
    };
     
        request.onload = function () {
      
        };
        request.setRequestHeader("Accepts", "application/json;charset=UTF-8");
        request.setRequestHeader("Content-Type", "application/json;charset=UTF-8");
        request.send(JSON.stringify(itm));
}
//функция загрузки заказов
function loadOrders() {
    var i, j;
    var request = new XMLHttpRequest();
    request.open("GET", uri, false);
    request.onload = function () {
        var items = null;
        items = JSON.parse(request.responseText);
       
        for (i in items) {
            x += "<hr>";
            x += "<h4> Заказ номер " + items[i].orderId+ " Дата заказа " + items[i].date +" Сумма "+ items[i].sum+"</a></h4>";
            x += "<button type='button' class='btn btn-sm btn-outline-secondary' onclick='deleteOrder(" + items[i].orderId + " );'>Удалить</button>";

            getStrings(items[i].orderId);
        }
        
        document.getElementById("ordersDiv").innerHTML = x;
        
    };
    
    request.send();
}
//функция удаления заказа
function deleteOrder(id) {
    var request = new XMLHttpRequest();
    var url = uri + id;
    request.open("DELETE", url, false);
    request.onload = function () {
       // loadOrders();
        // Обработка кода ответа
        var msg = "";
        if (request.status === 401) {
            msg = "У вас не хватает прав для создания";
        } else if (request.status === 201) {
            msg = "Запись добавлена";
            getBlogs();
        } else {
            msg = "Неизвестная ошибка";
        }
        document.querySelector("#actionMsg").innerHTML = msg;
    };
    request.send();
}
//функция удаления строки заказа из заказа
function deletestring(id) {
    var request = new XMLHttpRequest();
    var ur = url + id;
    request.open("DELETE", ur, false);
    request.onload = function () {
        //loadOrders();
        // Обработка кода ответа
        var msg = "";
        if (request.status === 401) {
            msg = "У вас не хватает прав для создания";
        } else if (request.status === 201) {
            msg = "Запись добавлена";
            loadOrders();
        } else {
            msg = "Неизвестная ошибка";
        }
        document.querySelector("#actionMsg").innerHTML = msg;
    };
    request.send();
}
//функция добавления заказа, если нет активного заказа у пользователя
function createOrder( id, cost) {
    let urlText = "";
    //urlText = document.querySelector("#createDiv").value;

    var request = new XMLHttpRequest();
    request.open("POST", uri);
    request.onload = function () {
        //getBlogs();
        idorder(id, cost);

       // document.querySelector("#createDiv").value = "";
    };
    request.setRequestHeader("Accepts", "application/json;charset=UTF-8");
    request.setRequestHeader("Content-Type", "application/json;charset=UTF-8");
    request.send(JSON.stringify({
        "Date": "2019.04.10",
        "sum": cost,
        "act": 1,
        "UserId": "5920baf4-a5a0-442c-bab6-bc7985a3ac5d"
    }));
    
}


function getCurrentUser() {
    let request = new XMLHttpRequest();
    request.open("POST", "/api/Account/isAuthenticated", true);
    request.onload = function () {
        let myObj = "";
        myObj = request.responseText !== "" ?
            JSON.parse(request.responseText) : {};
        document.getElementById("msg").innerHTML = myObj.message;
    };
    request.send();
}
//функция входа пользователя в систему
function logIn() {
    var email, password = "";
    // Считывание данных с формы
    email = document.getElementById("Email").value;
    password = document.getElementById("Password").value;
    var request = new XMLHttpRequest();
    request.open("POST", "/api/Account/Login");
    request.setRequestHeader("Content-Type", "application/json;charset=UTF-8");
    request.onreadystatechange = function () {
        // Очистка контейнера вывода сообщений
        document.getElementById("msg").innerHTML = "";
        var mydiv = document.getElementById('formError');
        while (mydiv.firstChild) {
            mydiv.removeChild(mydiv.firstChild);
        }
        // Обработка ответа от сервера
        if (request.responseText !== "") {
            var msg = null;
            msg = JSON.parse(request.responseText);
            document.getElementById("msg").innerHTML = msg.message;
            // Вывод сообщений об ошибках
            if (typeof msg.error !== "undefined" && msg.error.length > 0) {
                for (var i = 0; i < msg.error.length; i++) {
                    var ul = document.getElementsByTagName("ul");
                    var li = document.createElement("li");
                    li.appendChild(document.createTextNode(msg.error[i]));
                    ul[0].appendChild(li);
                }
            }
            document.getElementById("Password").value = "";
        }
    };
    // Запрос на сервер
    request.send(JSON.stringify({
        email: email,
        password: password
    }));
}
//функция выхода пользователя из системы
function logOff() {
    var request = new XMLHttpRequest();
    request.open("POST", "api/account/logoff");
    request.onload = function () {
        var msg = JSON.parse(this.responseText);
        document.getElementById("msg").innerHTML = "";
        var mydiv = document.getElementById('formError');
        while (mydiv.firstChild) {
            mydiv.removeChild(mydiv.firstChild);
        }
        document.getElementById("msg").innerHTML = msg.message;
    };
    request.setRequestHeader("Content-Type", "application/json;charset=UTF-8");
    request.send();

 }

