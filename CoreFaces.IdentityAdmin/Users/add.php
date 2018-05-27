<?php include("../header.php"); ?>

<div class="content-wrap">
    <!-- main page content. the place to put widgets in. usually consists of .row > .col-lg-* > .widget.  -->
    <main id="content" class="content" role="main">
       

        <div class="row">

            <div class="col-md-8">
                <div class="widget-body"  ng-controller="userCtrl">
                    <form class="form-horizontal" role="form">
                        <fieldset>
                            <legend><strong>Üye</strong> Ekle</legend>
                            <div class="form-group row">
                                <label for="normal-field" class="col-md-4 form-control-label text-md-right">Name</label>
                                <div class="col-md-7">
                                    <input type="text" ng-model="Name" class="form-control" placeholder="Name">
                                </div>
                            </div>

                            <div class="form-group row">
                                <label for="normal-field" class="col-md-4 form-control-label text-md-right">SurName</label>
                                <div class="col-md-7">
                                    <input type="text" ng-model="SurName" class="form-control" placeholder="SurName">
                                </div>
                            </div>

                            <div class="form-group row">
                                <label for="normal-field" class="col-md-4 form-control-label text-md-right">Email</label>
                                <div class="col-md-7">
                                    <input type="text" ng-model="Email" class="form-control" placeholder="Email">
                                </div>
                            </div>

                            <div class="form-group row">
                                <label for="normal-field" class="col-md-4 form-control-label text-md-right">Password</label>
                                <div class="col-md-7">
                                    <input type="text" ng-model="Password" class="form-control" placeholder="Password">
                                </div>
                            </div>


                          
                        </fieldset>
                        <div class="form-actions">
                            <div class="row">
                                <div class="col-md-offset-4 col-md-7">
                                    <a class="btn btn-inverse btn-sm" href="#" ng-click="add()">Save</a>
                                    
                                </div>
                            </div>
                        </div>
                    </form>
                </div>
            </div>

        </div>
    </main>
</div>



<?php include("../footer.php"); ?>