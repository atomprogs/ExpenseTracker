(function (angular) {
    window.mainapp = angular.module('expenseTracker', ['ngRoute', 'ngAnimate', 'ngMessages', 'angular-loading-bar', 'ui.bootstrap', 'ngTouch']);
    var app = window.mainapp;

    app.controller('routeController', ['$scope', '$location', function ($scope, $location) {
        // $scope.showmenubar = true;
        // $scope.$location = $location;
        // $scope.showmenubar = $scope.$location.path() === '/LogIn';
    }]);
    app.directive('menubar', function ($route, $routeParams, $location) {
        //console.log($location.path());
        //if ($location.path() === "" || $location.path() === "/LogIn")
        //    return {};
        var directive = {};
        directive.restrict = 'E';
        directive.templateUrl = '../Static/menubar.html'

        return directive;
    });
    //app.controller('WelcomeController', ['$window', '$location', '$scope', function ($window, $location, $scope) {
    //    var welcome = this;
    //    this.goNext = function (_path) {
    //        //$location.path(_path);
    //        $window.location.href = _path;
    //    }
    //}]);

    //app.directive("welcomepage", function () {
    //    var directive = {};
    //    directive.restrict = 'E';
    //    directive.templateUrl = "../IvizIntegration/welcomepage";
    //    // directive.controller.$inject = ['dataservice', '$http']
    //    //directive.controller = ['$window', '$location', '$scope', function ($window,$location, $scope) {
    //    //    this.goNext = function (_path) {
    //    //        //$location.path(_path);
    //    //        $window.location.href = _path;
    //    //    }
    //    //}];
    //    //directive.controllerAs="welcomeCtrl"
    //    return directive;
    //});
    //app.directive('script', function () {
    //    return {
    //        restrict: 'E',
    //        scope: false,
    //        link: function (scope, elem, attr) {
    //            if (attr.type === 'text/javascript-lazy') {
    //                var s = document.createElement("script");
    //                s.type = "text/javascript";
    //                var src = elem.attr('src');
    //                if (src !== undefined) {
    //                    s.src = src;
    //                }
    //                else {
    //                    var code = elem.text();
    //                    s.text = code;
    //                }
    //                document.head.appendChild(s);
    //                elem.remove();
    //            }
    //        }
    //    };
    //});
    app.config(['$routeProvider', function ($routeProvider) {
        $routeProvider.

        when('/LogIn', {
            title: 'LogIn - Expense Tracker',
            templateUrl: '../ExpenseTracker/LogIn',
            controller: 'LoginController'
        }).
        when('/dashboard', {
            title: 'Dashboard - Expense Tracker',
            templateUrl: '../ExpenseTracker/Dashboard'
        }).

    //when('/ClientDB', {
    //    title: 'Configure ClientDB Database',
    //    templateUrl: '../IvizIntegration/ClientDB',
    //    controller: 'DatabaseController'
    //}).
    //when('/Welcome', {
    //    title: 'Welcome to ivizintegration',
    //    templateUrl: '../IvizIntegration/welcomepage',
    //    controller: 'WelcomeController'
    //}).
    //when('/ClientConfiguration', {
    //    title: 'View and edit client configuration',
    //    templateUrl: '../IvizIntegration/ClientConfiguration'
    //    // controller: 'WelcomeController'
    //}).
    //when('/IvizConfiguration', {
    //    title: 'View and edit iviz configuration',
    //    templateUrl: '../IvizIntegration/IvizConfiguration'
    //    // controller: 'WelcomeController'
    //}).
    otherwise({
        redirectTo: '/LogIn'
    });
    }]);
    //Improving Angular performance with 1 line of code
    app.config(['$compileProvider', function ($compileProvider) {
        $compileProvider.debugInfoEnabled(false);
    }]);
    //    .controller('routeController', ['$scope', '$location', function ($scope, $location) {
    //    $scope.showPageHero = $location.path() === '/LogIn';
    //}]);
    //app.config(['$stateProvider', '$urlRouterProvider', function ($stateProvider, $urlRouterProvider) {
    //    $stateProvider

    //      // State
    //        .state('LogIn', {
    //            url: "/LogIn",
    //            templateUrl: "../ExpenseTracker/LogIn",
    //            data: { pageTitle: 'LogIn' },
    //            controller: "LoginController",
    //            resolve: { // Any property in resolve should return a promise and is executed before the view is loaded
    //                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
    //                    // you can lazy load files for an existing module
    //                    return $ocLazyLoad.load('/Scripts/Expense/app/login.js');
    //                }]
    //            }
    //            //resolve: {
    //            //    deps: ['$ocLazyLoad', function($ocLazyLoad) {
    //            //        return $ocLazyLoad.load([{
    //            //            name: 'ui.select',
    //            //            // add UI select css / js for this state
    //            //            files: [
    //            //                'css/ui-select/select.min.css',
    //            //                'js/ui-select/select.min.js'
    //            //            ]
    //            //        }, {
    //            //            name: 'myApp',
    //            //            files: [
    //            //                'js/controllers/GeneralController.js'
    //            //            ]
    //            //        }]);
    //            //    }]
    //            //}
    //        });
    //}]);
    // change Page Title based on the routers
    app.run(['$location', '$rootScope', function ($location, $rootScope) {
        $rootScope.$on('$routeChangeSuccess', function (event, current, previous) {
            if (current.$$route)
                $rootScope.title = current.$$route.title;
        });
    }]);
})(window.angular);