(function (angular) {
    var dashboard = angular.module('dashboard', []);

    dashboard.controller('DashboardController', ['$scope', '$http', '$location', function ($scope, $http, $location) {
        var dashboardObj = this;
        dashboardObj.DashboardData = { "NoOfItems": 0, "NoOfItemsText": "", "TotalSpending": 0, "TotalSpendingText": "", "WishList": 0, "WishListText": "", "User": "", "UserText": "" };

        dashboardObj.getDashboardData = function () {
            var functiontoCall = {
                method: 'POST',
                url: '../ExpenseTracker/GetDashboardData'
            }

            $http(functiontoCall).then(function successCallback(response) {
                // this callback will be called asynchronously
                // when the response is available
                console.log(response.data);
                dashboardObj.DashboardData = response.data;
            }, function errorCallback(response) {
                // called asynchronously if an error occurs
                // or server returns response with an error status.
                //console.log(response.data);
            });
        };
    }]);

    //push to main module
    window.mainapp.requires.push('dashboard');
})(window.angular);