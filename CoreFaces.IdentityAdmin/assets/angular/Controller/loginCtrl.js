

app.controller("loginCtrl", function ($scope, authService) {

    authService.isLoginByRedirect();
    var user = authService.getUser();
    if(user)
    {

            $scope.Name = user.name;
            $scope.Surname = user.surName;
    }

    //var t = loginService.login("rrr", "ttt");
    //console.log(authService.getUser());
    //console.log(authService.getToken());
    //alert(authService.isLogin());

   
        $scope.message = "";

        $scope.login = function () {

            $scope.loginData = {
                Email: $scope.Email,
                Password: $scope.Password
            };

            authService.login($scope.loginData).then(function (response) {
                debugger;
                if (response.data.status === true)
                {
                    window.location = "/";
                }
                
                //console.log(response);

            },
             function (err) {
                 $scope.message = err.error_description;
             });
        };


});