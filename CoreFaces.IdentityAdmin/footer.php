
    <!-- The Loader. Is shown when pjax happens -->
    <div class="loader-wrap hiding hide">
        <i class="fa fa-circle-o-notch fa-spin-fast"></i>
    </div>
    <div id="rickshaw" class="chart-overflow-bottom hide"></div>
   
    <script src="https://ajax.googleapis.com/ajax/libs/angularjs/1.5.6/angular.min.js"></script>
    <script src="/assets/angular/app.js"></script>
    <script src="/assets/angular/Service/authService.js"></script>
     <script src="/assets/angular/Service/permissionService.js"></script>
    <script src="/assets/angular/Controller/loginCtrl.js"></script>
    <script src="/assets/angular/Controller/userCtrl.js"></script>
     <script src="/assets/angular/Controller/permissionCtrl.js"></script>

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
    <script src="/assets/vendor/slimScroll/jquery.slimscroll.js"></script>
    <script src="/assets/vendor/widgster/widgster.js"></script>
    <script src="/assets/vendor/pace.js/pace.js" data-pace-options='{ "target": ".content-wrap", "ghostTime": 1000 }'></script>
    <script src="/assets/vendor/jquery-touchswipe/jquery.touchSwipe.js"></script>
    <script src="/assets/js/bootstrap-fix/button.js"></script>

    <!-- common app js -->
    <script src="/assets/js/settings.js"></script>
    <script src="/assets/js/app.js"></script>

    <!-- page specific libs -->
    <script id="test" src="/assets/vendor/underscore/underscore.js"></script>
    <script src="/assets/vendor/jquery.sparkline/index.js"></script>
    <script src="/assets/vendor/jquery.sparkline/index.js"></script>
    <script src="/assets/vendor/d3/d3.min.js"></script>
    <script src="/assets/vendor/rickshaw/rickshaw.min.js"></script>
    <script src="/assets/vendor/raphael/raphael-min.js"></script>
    <script src="/assets/vendor/jQuery-Mapael/js/jquery.mapael.js"></script>
    <script src="/assets/vendor/jQuery-Mapael/js/maps/usa_states.js"></script>
    <script src="/assets/vendor/jQuery-Mapael/js/maps/world_countries.js"></script>
    <script src="/assets/vendor/bootstrap/js/dist/popover.js"></script>
    <script src="/assets/vendor/bootstrap_calendar/bootstrap_calendar/js/bootstrap_calendar.min.js"></script>
    <script src="/assets/vendor/jquery-animateNumber/jquery.animateNumber.min.js"></script>

    <!-- Kendo Grid -->
    <link rel="stylesheet" href="https://kendo.cdn.telerik.com/2017.2.504/styles/kendo.common-material.min.css" />
    <link rel="stylesheet" href="https://kendo.cdn.telerik.com/2017.2.504/styles/kendo.material.min.css" />
    <link rel="stylesheet" href="https://kendo.cdn.telerik.com/2017.2.504/styles/kendo.material.mobile.min.css" />
    <script src="https://kendo.cdn.telerik.com/2017.2.504/js/kendo.all.min.js"></script>

<script src="https://cdnjs.cloudflare.com/ajax/libs/limonte-sweetalert2/6.6.5/sweetalert2.min.js"></script>
<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/limonte-sweetalert2/6.6.5/sweetalert2.min.css">


    <script>

    function ajaxSettings()
    {
        
            $.ajaxSetup({
                beforeSend: function (xhr)
                {
                   xhr.setRequestHeader("Token", Token);     
                }
            });


            $(document).ajaxError(function myErrorHandler(event, xhr, ajaxOptions, thrownError) {
                console.log(xhr.responseJSON);
                //alert(xhr.responseJSON.errorMessage);

                for(var i = 0; i < xhr.responseJSON.errorMessage.length; i++) {
                        var obj = xhr.responseJSON.errorMessage[i];

                        console.log(obj.errorMessage);
                        alert(obj.errorMessage);
                    }
                
            });

    }
    ajaxSettings();
    </script>

    <!-- page specific js -->
    <script src="/assets/js/index.js"></script>
    
    
</body>
</html>