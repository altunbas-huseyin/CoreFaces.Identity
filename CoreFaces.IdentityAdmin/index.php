<?php include("header.php"); ?>



<div class="content-wrap">
    <!-- main page content. the place to put widgets in. usually consists of .row > .col-lg-* > .widget.  -->
    <main id="content" class="content" role="main">
        <h1 class="page-title">Dashboard <small><small>The Lucky One</small></small></h1>
        <div class="row">
            <div class="col-lg-8">
                <!-- minimal widget consist of .widget class. note bg-transparent - it can be any background like bg-gray,
                bg-primary, bg-white -->
                <section class="widget bg-transparent">
                    <!-- .widget-body is a mostly semantic class. may be a sibling to .widget>header or .widget>footer -->
                    <div class="widget-body">
                        <div id="map" class="mapael">
                            <div class="stats">
                                <h6 class="text-white m-t-1">GEO-LOCATIONS</h6>
                                <p class="h3 text-warning no-margin"><strong id="geo-locations-number">1 656 843</strong> <i class="fa fa-map-marker"></i></p>
                            </div>
                            <div class="map">
                                <span>Alternative content for the map</span>
                            </div>
                        </div>
                    </div>
                </section>
            </div>
            <div class="col-lg-4">
                <section class="widget bg-transparent">
                    <header>
                        <h5>
                            Map
                            <span class="fw-semi-bold">Statistics</span>
                        </h5>
                        <div class="widget-controls widget-controls-hover">
                            <a href="#"><i class="glyphicon glyphicon-cog"></i></a>
                            <a href="#"><i class="fa fa-refresh"></i></a>
                            <a href="#" data-widgster="close"><i class="glyphicon glyphicon-remove"></i></a>
                        </div>
                    </header>
                    <div class="widget-body">
                        <p>Status: <strong>Live</strong></p>
                        <p>
                            <span class="circle bg-warning"><i class="fa fa-map-marker"></i></span>
                            146 Countries, 2759 Cities
                        </p>
                        <div class="row progress-stats">
                            <div class="col-md-9">
                                <h6 class="name m-t-1">Foreign Visits</h6>
                                <p class="description deemphasize">Some Cool Text</p>
                                <div class="bg-white progress-bar">
                                    <progress class="progress progress-primary progress-sm js-progress-animate" value="100" max="100" style="width: 0%" data-width="60%"></progress>
                                </div>
                            </div>
                            <div class="col-md-3 text-xs-center">
                                <span class="status rounded rounded-lg bg-body-light">
                                    <small><span id="percent-1">75</span>%</small>
                                </span>
                            </div>
                        </div>
                        <div class="row progress-stats">
                            <div class="col-md-9">
                                <h6 class="name m-t-1">Local Visits</h6>
                                <p class="description deemphasize">P. to C. Conversion</p>
                                <div class="bg-white progress-bar">
                                    <progress class="progress progress-sm progress-danger js-progress-animate" value="100" max="100" style="width: 0%" data-width="39%"></progress>
                                </div>
                            </div>
                            <div class="col-md-3 text-xs-center">
                                <span class="status rounded rounded-lg bg-body-light">
                                    <small><span id="percent-2">84</span>%</small>
                                </span>
                            </div>
                        </div>
                        <div class="row progress-stats">
                            <div class="col-md-9">
                                <h6 class="name m-t-1">Sound Frequencies</h6>
                                <p class="description deemphasize">Average Bitrate</p>
                                <div class="bg-white progress-bar">
                                    <progress class="progress progress-sm progress-success js-progress-animate" value="100" max="100" style="width: 0%" data-width="80%"></progress>
                                </div>
                            </div>
                            <div class="col-md-3 text-xs-center">
                                <span class="status rounded rounded-lg bg-body-light">
                                    <small><span id="percent-3">92</span>%</small>
                                </span>
                            </div>
                        </div>
                        <h6 class="fw-semi-bold mt">Map Distributions</h6>
                        <p>Tracking: <strong>Active</strong></p>
                        <p>
                            <span class="circle bg-warning"><i class="fa fa-cog"></i></span>
                            391 elements installed, 84 sets
                        </p>
                        <div class="input-group mt">
                            <input type="text" class="form-control" placeholder="Search Map">
                            <span class="input-group-btn">
                                <button type="submit" class="btn btn-default">
                                    <i class="fa fa-search text-gray"></i>
                                </button>
                            </span>
                        </div>
                    </div>
                </section>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-4">
                <section class="widget">
                    <!-- .widget>header is generally a place for widget title and widget controls. see .widget in _base.scss -->
                    <header>
                        <h6>
                            USERBASE GROWTH
                        </h6>
                        <div class="widget-controls">
                            <a href="#"><i class="glyphicon glyphicon-cog"></i></a>
                            <a href="#" data-widgster="close"><i class="glyphicon glyphicon-remove"></i></a>
                        </div>
                    </header>
                    <div class="widget-body">
                        <div class="stats-row">
                            <div class="stat-item">
                                <h6 class="name fs-sm">Overall Growth</h6>
                                <p class="value">76.38%</p>
                            </div>
                            <div class="stat-item">
                                <h6 class="name fs-sm">Montly</h6>
                                <p class="value">10.38%</p>
                            </div>
                            <div class="stat-item">
                                <h6 class="name fs-sm">24h</h6>
                                <p class="value">3.38%</p>
                            </div>
                        </div>
                        <div class="bg-gray-lighter progress-bar">
                            <progress class="progress progress-xs progress-success" value="100" max="100" style="width: 60%"></progress>
                        </div>
                        <p>
                            <small><span class="circle bg-warning"><i class="glyphicon glyphicon-chevron-up"></i></span></small>
                            <span class="fw-semi-bold">17% higher</span>
                            than last month
                        </p>
                    </div>
                </section>
            </div>
            <div class="col-lg-4">
                <section class="widget">
                    <header>
                        <h6>
                            TRAFFIC VALUES
                        </h6>
                        <div class="widget-controls">
                            <a href="#"><i class="glyphicon glyphicon-cog"></i></a>
                            <a href="#" data-widgster="close"><i class="glyphicon glyphicon-remove"></i></a>
                        </div>
                    </header>
                    <div class="widget-body">
                        <div class="stats-row">
                            <div class="stat-item">
                                <h6 class="name fs-sm">Overall Values</h6>
                                <p class="value">17 567 318</p>
                            </div>
                            <div class="stat-item">
                                <h6 class="name fs-sm">Montly</h6>
                                <p class="value">55 120</p>
                            </div>
                            <div class="stat-item">
                                <h6 class="name fs-sm">24h</h6>
                                <p class="value">9 695</p>
                            </div>
                        </div>
                        <div class="bg-gray-lighter progress-bar">
                            <progress class="progress progress-xs progress-danger" value="100" max="100" style="width: 60%"></progress>
                        </div>
                        <p>
                            <small><span class="circle bg-warning"><i class="fa fa-chevron-down"></i></span></small>
                            <span class="fw-semi-bold">8% lower</span>
                            than last month
                        </p>
                    </div>
                </section>
            </div>
            <div class="col-lg-4">
                <section class="widget">
                    <header>
                        <h6>
                            RANDOM VALUES
                        </h6>
                        <div class="widget-controls">
                            <a href="#"><i class="glyphicon glyphicon-cog"></i></a>
                            <a href="#" data-widgster="close"><i class="glyphicon glyphicon-remove"></i></a>
                        </div>
                    </header>
                    <div class="widget-body">
                        <div class="stats-row">
                            <div class="stat-item">
                                <h6 class="name fs-sm">Overcome T.</h6>
                                <p class="value">104.85%</p>
                            </div>
                            <div class="stat-item">
                                <h6 class="name fs-sm">Takeoff Angle</h6>
                                <p class="value">14.29&deg;</p>
                            </div>
                            <div class="stat-item">
                                <h6 class="name fs-sm">World Pop.</h6>
                                <p class="value">7,211M</p>
                            </div>
                        </div>
                        <div class="bg-gray-lighter progress-bar">
                            <progress class="progress progress-primary progress-xs" value="100" max="100" style="width: 60%"></progress>
                        </div>
                        <p>
                            <small><span class="circle bg-warning"><i class="fa fa-plus"></i></span></small>
                            <span class="fw-semi-bold">8 734 higher</span>
                            than last month
                        </p>
                    </div>
                </section>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-4">
                <section class="widget">
                    <header>
                        <h6><span class="label label-danger">New</span> Messages</h6>
                        <div class="widget-controls">
                            <a href="#"><i class="fa fa-refresh"></i></a>
                            <a href="#" data-widgster="close"><i class="glyphicon glyphicon-remove"></i></a>
                        </div>
                    </header>
                    <div class="widget-body no-padding">
                        <div class="list-group list-group-lg">
                            <a class="list-group-item" href="#">
                                <span class="thumb-sm pull-xs-left mr">
                                    <img class="img-circle" src="/assets/demo/img/people/a2.jpg" alt="...">
                                    <i class="status status-bottom bg-success"></i>
                                </span>
                                <h6 class="no-margin">Chris Gray</h6>
                                <p class="help-block text-ellipsis no-margin">Hey! What's up? So many times since we</p>
                            </a>
                            <a class="list-group-item" href="#">
                                <span class="thumb-sm pull-xs-left mr">
                                    <img class="img-circle" src="/assets/demo/img/people/a4.jpg" alt="...">
                                    <i class="status status-bottom bg-success"></i>
                                </span>
                                <h6 class="no-margin">Jamey Brownlow</h6>
                                <p class="help-block text-ellipsis no-margin">Good news coming tonight. Seems they agreed to proceed</p>
                            </a>
                            <a class="list-group-item" href="#">
                                <span class="thumb-sm pull-xs-left mr">
                                    <img class="img-circle" src="/assets/demo/img/people/a1.jpg" alt="...">
                                    <i class="status status-bottom bg-warning"></i>
                                </span>
                                <h6 class="no-margin">Livia Walsh</h6>
                                <p class="help-block text-ellipsis no-margin">Check my latest email plz!</p>
                            </a>
                            <a class="list-group-item" href="#">
                                <span class="thumb-sm pull-xs-left mr">
                                    <img class="img-circle" src="/assets/demo/img/people/a5.jpg" alt="...">
                                    <i class="status status-bottom bg-danger"></i>
                                </span>
                                <h6 class="no-margin">Jaron Fitzroy</h6>
                                <p class="help-block text-ellipsis no-margin">What about summer break?</p>
                            </a>
                        </div>
                    </div>
                    <footer class="bg-body-light mt">
                        <input type="search" class="form-control form-control-sm" placeholder="Search">
                    </footer>
                </section>
            </div>
            <div class="col-lg-4">
                <section class="widget">
                    <header>
                        <h6>
                            Market <span class="fw-semi-bold">Stats</span>
                        </h6>
                        <div class="widget-controls">
                            <a href="#" data-widgster="close"><i class="glyphicon glyphicon-remove"></i></a>
                        </div>
                    </header>
                    <div class="widget-body">
                        <h3>$720 Earned</h3>
                        <p class="text-muted mb mt-sm">
                            Target <span class="fw-semi-bold">$820</span> day earnings
                            is <span class="fw-semi-bold">96%</span> reached.
                        </p>
                    </div>
                    <div class="widget-table-overflow">
                        <table class="table table-striped table-sm">
                            <thead class="no-bd">
                                <tr>
                                    <th>
                                        <div class="abc-checkbox">
                                            <input id="checkbox210" type="checkbox" data-check-all="">
                                            <label for="checkbox210"></label>
                                        </div>
                                    </th>
                                    <th></th>
                                    <th></th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td>
                                        <div class="abc-checkbox">
                                            <input id="checkbox212" type="checkbox">
                                            <label for="checkbox212"></label>
                                        </div>
                                    </td>
                                    <td>HP Core i7</td>
                                    <td class="text-xs-right fw-semi-bold">$346.1</td>
                                </tr>
                                <tr>
                                    <td>
                                        <div class="abc-checkbox">
                                            <input id="checkbox214" type="checkbox">
                                            <label for="checkbox214"></label>
                                        </div>
                                    </td>
                                    <td>Air Pro</td>
                                    <td class="text-xs-right fw-semi-bold">$533.1</td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                    <div class="widget-body mt-xlg">
                        <div id="rickshaw" class="chart-overflow-bottom"></div>
                    </div>
                </section>
            </div>
            <div class="col-lg-4">
                <section class="widget">
                    <header>
                        <h6>Calendar</h6>
                        <div class="widget-controls">
                            <a href="#"><i class="glyphicon glyphicon-cog"></i></a>
                            <a href="#" data-widgster="close"><i class="glyphicon glyphicon-remove"></i></a>
                        </div>
                    </header>
                    <div class="widget-body no-padding">
                        <div id="events-calendar" class="bg-primary-light"></div>
                        <div class="list-group">
                            <a href="#" class="list-group-item text-ellipsis">
                                <span class="label-pill label-warning pull-xs-right">6:45</span>
                                Weed out the flower bed
                            </a>
                            <a href="#" class="list-group-item text-ellipsis">
                                <span class="label-pill label-success pull-xs-right">9:41</span>
                                Stop world water pollution
                            </a>
                        </div>
                    </div>
                </section>
            </div>
        </div>
    </main>
</div>

<?php include("footer.php"); ?>