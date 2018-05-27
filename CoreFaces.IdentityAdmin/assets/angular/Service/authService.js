'use strict';
app.service('authService', ['$http', function ($http, $location) {

    var authServiceFactory = {};

    var _authentication = {
        isAuth: false,
        userName: ""
    };

    var _isLogin = function ()
    {
        return window.localStorage.getItem("isLogin");
    };

    var _isLoginByRedirect = function () {
        if(window.location.href.indexOf("login.php") >= 0)
        {
            if (window.localStorage.getItem("isLogin")) 
            {
              //window.location = "/";
            }
                else
                {
                    if(window.location.href.indexOf("login.php") < 0)
                    { window.location = "/login.php";}
                   
                }
            var currentDate = new Date().toLocaleString()
            var user = _getUser();
            /*if(user.jwt.deadLine>currentDate)
            {
                window.location = "/login.php";
            }*/
        }
    };

    var _getUser = function () {
        var user = window.localStorage.getItem("apiUser");
        user = JSON.parse(user);
        return user;
    };

    var _getToken = function () {
        var user = window.localStorage.getItem("apiUser");
        user = JSON.parse(user);
        return user.jwt.token;
    };

    var _login = function (loginData) {

        var dataJSON = {
            Email: loginData.Email,
            Password: loginData.Password
        };

        var result = $http.post(apiUrl + 'api/Login', JSON.stringify(dataJSON),
            {
                headers: { 'Content-Type': 'application/json; charset=utf-8' }

            }).success(function (response) {
                debugger;
                if (response.status === true) {
                    window.localStorage.setItem("isLogin", true);
                    window.localStorage.setItem("apiUser", JSON.stringify(response.result));
                    
                }
              

            }).error(function (err, status) {

            });


        return result;

    };

    var _add = function (user) {

       
        var result = $http.post(apiUrl + 'Users', JSON.stringify(user),
            {
                headers: { 'Content-Type': 'application/json; charset=utf-8', 'Token': Token }

            }).success(function (response) {

                console.log(response.status);

            }).error(function (err, status) {

            });


        return result;

    };


    authServiceFactory.login = _login;
    authServiceFactory.add = _add;
    authServiceFactory.isLogin = _isLogin;
    authServiceFactory.getUser = _getUser;
    authServiceFactory.getToken = _getToken;
    authServiceFactory.isLoginByRedirect = _isLoginByRedirect;
    return authServiceFactory;
}]);
