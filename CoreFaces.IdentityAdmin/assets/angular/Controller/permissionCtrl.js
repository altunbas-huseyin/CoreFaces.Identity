

app.controller("permissionCtrl", function ($scope, permissionService) {



    // custom logic start

    $scope.runKendo = function () {
        var dataSource = new kendo.data.DataSource({
            transport: {
                read: function (e) {
                    // on success
                    //e.success(sampleData);
                    // on failure
                    //e.error("XHR response", "status code", "error message");

                    permissionService.get().then(function (response) {
                        e.success(response.data.result);
                    },
                        function (err) {
                            $scope.message = err.error_description;
                        });

                },
                create: function (e) {
                    // assign an ID to the new item
                    ///e.data.ProductID = sampleDataNextID++;
                    // save data item to the original datasource
                    ///sampleData.push(e.data);
                    // on success
                    ///e.success(e.data);
                    // on failure
                    //e.error("XHR response", "status code", "error message");

                    debugger;
                    var ddd = e;
                    
                    permissionService.add(e.data).then(function (response) {
                        if (response.data.status === true) {
                            $scope.runKendo();
                        }

                        //console.log(response);

                    },
                        function (err) {
                            $scope.message = err.error_description;
                        });


                },
                update: function (e) {
                    // locate item in original datasource and update it
                    //sampleData[getIndexById(e.data.ProductID)] = e.data;
                    // on success
                    ///alert(e.data._id);
                    ///e.success();
                    // on failure
                    //e.error("XHR response", "status code", "error message");

                    permissionService.update(e.data).then(function (response) {
                        if (response.data.status === true) {
                            $scope.runKendo();
                        }

                        //console.log(response);

                    },
                        function (err) {
                            $scope.message = err.error_description;
                        });

                },
                destroy: function (e) {
                    // locate item in original datasource and remove it
                    ///sampleData.splice(getIndexById(e.data.ProductID), 1);
                    // on success
                    ///e.success();
                    // on failure
                    //e.error("XHR response", "status code", "error message");

                    permissionService.destroy(e.data).then(function (response) {
                        if (response.data.status === true) {
                            $scope.runKendo();
                        }

                        //console.log(response);

                    },
                        function (err) {
                            $scope.message = err.error_description;
                        });

                }
            },
            error: function (e) {
                // handle data operation error
                alert("Status: " + e.status + "; Error message: " + e.errorThrown);
            },
            pageSize: 10,
            batch: false,
            schema: {
                model: {
                    id: "_id",
                    fields: {
                        _id: { editable: false, nullable: true }
                    }
                }
            }
        });

        $("#grid").kendoGrid({
            dataSource: dataSource,
            pageable: true,
            toolbar: ["create"],
            columns: [
                { field: "_id", title: "Id" },
                { field: "name", title: "Name" },
                { field: "description", title: "Description" },
                { command: ["edit", "destroy"], title: "&nbsp;" }
            ],
            editable: "inline"
        });

    };

    $scope.runKendo();

    /*
       $scope.login = function () {

           $scope.loginData = {
               Email: $scope.Email,
               Password: $scope.Password
           };

           permissionService.get().then(function (response) {
              
              console.log(response.data);
               //console.log(response);

           },
            function (err) {
                $scope.message = err.error_description;
            });
       };
     */

});