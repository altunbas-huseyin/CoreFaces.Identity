
<!DOCTYPE html>
<html>
<head>
    <title>Sing - Login</title>
    <link href="/assets/css/application.min.css" rel="stylesheet">
    <!-- as of IE9 cannot parse css files with more that 4K classes separating in two files -->
    <!--[if IE 9]>
        <link href="/assets/css/application-ie9-part2.css" rel="stylesheet">
    <![endif]-->
    <link rel="shortcut icon" href="/assets/img/favicon.png">
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no">
    <meta name="description" content="">
    <meta name="author" content="">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <script>
        /* yeah we need this empty stylesheet here. It's cool chrome & chromium fix
         chrome fix https://code.google.com/p/chromium/issues/detail?id=167083
         https://code.google.com/p/chromium/issues/detail?id=332189
         */
    </script>

    <script src="https://ajax.googleapis.com/ajax/libs/angularjs/1.5.6/angular.min.js"></script>
    <script src="/assets/angular/app.js"></script>
    <script src="/assets/angular/Service/authService.js"></script>
    <script src="/assets/angular/Controller/loginCtrl.js"></script>

    <script>

         //var apiUrl = "http://localhost:56037/";
         var apiUrl = "http://identity.kuaforx.com/";
         //var user = window.localStorage.getItem("apiUser");
         //user = JSON.parse(user);
         //var Token = user.jwt.token;

    </script>


</head>
<body class="login-page"  ng-app="myApp">

    <div class="container"  ng-controller="loginCtrl">
        <main id="content" class="widget-login-container" role="main">
            <div class="row">
                <div class="col-xl-4 col-md-6 col-xs-10 col-xl-offset-4 col-md-offset-3 col-xs-offset-1">
                    <h5 class="widget-login-logo animated fadeInUp">
                        <i class="fa fa-circle text-gray"></i>
                        sing
                        <i class="fa fa-circle text-warning"></i>
                    </h5>
                    <section class="widget widget-login animated fadeInUp">
                        <header>
                            <h3>Login to your Sing App</h3>
                        </header>
                        <div class="widget-body">
                            <p class="widget-login-info">
                                Use Facebook, Twitter or your email to sign in.
                            </p>
                            <p class="widget-login-info">
                                Don't have an account? Sign up now!
                            </p>
                            <form class="login-form mt-lg">
                                <div class="form-group">
                                    <input type="text" class="form-control" ng-model="Email"  placeholder="Username">
                                </div>
                                <div class="form-group">
                                    <input class="form-control" ng-model="Password"  type="password" placeholder="Password">
                                </div>
                                <div class="clearfix">
                                    <div class="btn-toolbar pull-xs-right">
                                        <button type="button" class="btn btn-secondary btn-sm">Create an Account</button>
                                        <a class="btn btn-inverse btn-sm" href="#" ng-click="login()" >Login</a>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6 col-md-push-6">
                                        <div class="clearfix">
                                            <div class="abc-checkbox widget-login-info pull-xs-right ml-n-lg">
                                                <input type="checkbox" id="checkbox1" value="1">
                                                <label for="checkbox1">Keep me signed in </label>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-6 col-md-pull-6">
                                        <a class="mr-n-lg" href="/assets/#">Trouble with account?</a>
                                    </div>
                                </div>
                            </form>
                        </div>
                    </section>
                </div>
            </div>
        </main>
        <footer class="page-footer">
            2014 &copy; Sing. Admin Dashboard Template.
        </footer>
    </div>
    <!-- The Loader. Is shown when pjax happens -->
    <div class="loader-wrap hiding hide">
        <i class="fa fa-circle-o-notch fa-spin-fast"></i>
    </div>

    <!-- common libraries. required for every page-->
    <script src="/assets/vendor/jquery/dist/jquery.min.js"></script>
    <script src="/assets/vendor/jquery-pjax/jquery.pjax.js"></script>
    <script src="/assets/vendor/tether/dist/js/tether.js"></script>
    <script src="/assets/vendor/bootstrap/js/dist/util.js"></script>
    <script src="/assets/vendor/bootstrap/js/dist/collapse.js"></script>
    <script src="/assets/vendor/bootstrap/js/dist/dropdown.js"></script>
    <script src="/assets/vendor/bootstrap/js/dist/button.js"></script>
    <script src="/assets/vendor/bootstrap/js/dist/tooltip.js"></script>
    <script src="/assets/vendor/bootstrap/js/dist/alert.js"></script>
    <script src="/assets/vendor/slimScroll/jquery.slimscroll.min.js"></script>
    <script src="/assets/vendor/widgster/widgster.js"></script>

    <!-- common app js -->
    <script src="/assets/js/settings.js"></script>
    <script src="/assets/js/app.js"></script>

    <!-- page specific libs -->
    <!-- page specific js -->
</body>
</html>