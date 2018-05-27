

app.controller("userCtrl", function ($scope, authService) {

  
   
        
        $scope.message = "";

        $scope.add = function () {
            
            $scope.user = {
                Name: $scope.Name,
                SurName: $scope.SurName,
                Email: $scope.Email,
                Password: $scope.Password
            };
            //var data = { "Id": 3, "FirstName": "Test", "LastName": "User", "Username": "testuser", "IsApproved": true, "IsOnlineNow": true, "IsChecked": true };
            //var data = { "Name": $scope.Name, "SurName": $scope.SurName, };
            authService.add($scope.user).then(function (response) {
                
                if (response.data.status === true) {
                    alert("Kayıt Başarılı");
                }
                else
                {
                    alert(response.data.errorMessage);
                }
                //console.log(response);
           
            },
             function (err) {
                 $scope.message = err.error_description;
             });
        };


    
});