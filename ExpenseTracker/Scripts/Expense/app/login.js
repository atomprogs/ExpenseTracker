(function (angular) {
    var login = angular.module('logIn', []);

    login.controller('LoginController', ['$scope', '$http', '$location', function ($scope, $http, $location) {
        // $scope.$parent.showPageHero = false;
        var LoginObj = this;
        LoginObj.RemmeberMe = false;
        LoginObj.Cancel = function () {
            LoginObj.User.Email = "";
            LoginObj.User.Password = "";
            LoginObj.RemmeberMe = false;
        };
        LoginObj.Submit = function () {
            var functiontoCall = {
                method: 'POST',
                url: '../ExpenseTracker/SignIn',
                data: JSON.stringify({ User: LoginObj.User })
            }

            $http(functiontoCall).then(function successCallback(response) {
                // this callback will be called asynchronously
                // when the response is available
                //console.log(response.data);
                //LoginObj.User = response.data;
                $location.path("/dashboard");
            }, function errorCallback(response) {
                // called asynchronously if an error occurs
                // or server returns response with an error status.
                //console.log(response.data);
                $scope.SignInmessage = response.data;
            });
        };
        LoginObj.User = { "Id": 0, "FirstName": null, "LastName": null, "Email": null, "Password": null, "PhoneNumber": null, "SignUpCode": null, "Setting": { "IsActive": false, "IsAdmin": false, "IsEnable": false, "CreatedDate": new Date(), "ModifiedDate": new Date() }, "MonthlyLimit": { "Month": 0, "Lowest": 0, "Highest": 0 } }
    }]);
    login.controller('SignUpController', ['$scope', '$http', '$location', 'modalService', function ($scope, $http, $location, modalService) {
        // $scope.$parent.showPageHero = false;

        //Configure Modal Popup
        var modalOptions = {
            closeButtonText: 'Cancel',
            actionButtonText: 'Ok',
            headerText: 'Alert',
            bodyText: 'Updated....'
        };
        var SignUp = this;
        SignUp.Cancel = function () {
            SignUp.User.Email = "";
            SignUp.User.Password = "";
            SignUp.User.FirstName = "";
            SignUp.User.LastName = "";
            SignUp.User.PhoneNumber = "";
            $scope.Message = "";
        };
        SignUp.Submit = function () {
            var functiontoCall = {
                method: 'POST',
                url: '../ExpenseTracker/SignUp',
                data: JSON.stringify({ User: SignUp.User })
            }

            $http(functiontoCall).then(function successCallback(response) {
                // this callback will be called asynchronously
                // when the response is available
                console.log(response.data);
                //LoginObj.User = response.data;
                if (response.data == "1") {
                    $location.path("/dashboard");
                }
                else
                    $scope.Message = response.data;
            }, function errorCallback(response) {
                // called asynchronously if an error occurs
                // or server returns response with an error status.
                console.log(response.data);
                // $scope.SignUpmessage=response.data;
            });
        };

        SignUp.GetCode = function () {
            var functiontoCall = {
                method: 'POST',
                url: '../ExpenseTracker/GetSignUpCode',
                data: JSON.stringify({ User: SignUp.User })
            }
            if (SignUp.User.Email == null || SignUp.User.Email == "") {
                modalOptions.bodyText = "Invalid email id.";
                modalOptions.headerText = "Alert";
                modalService.showModal({}, modalOptions).then(function (result) {
                    if (result === 'ok') {
                    }
                });
                return;
            }
            $http(functiontoCall).then(function successCallback(response) {
                // this callback will be called asynchronously
                // when the response is available
                console.log(response.data);
                //LoginObj.User = response.data;
                // $location.path("/LogIn");
                // $scope.Message = response.data;
                //Call to modalService
                modalOptions.bodyText = response.data;
                modalOptions.headerText = "Alert";
                modalService.showModal({}, modalOptions).then(function (result) {
                    if (result === 'ok') {
                    }
                });
            }, function errorCallback(response) {
                // called asynchronously if an error occurs
                // or server returns response with an error status.
                console.log(response.data);
            });
        };
        SignUp.User = { "Id": 0, "FirstName": null, "LastName": null, "Email": null, "Password": null, "PhoneNumber": null, "SignUpCode": null, "Setting": { "IsActive": false, "IsAdmin": false, "IsEnable": false, "CreatedDate": new Date(), "ModifiedDate": new Date() }, "MonthlyLimit": { "Month": 0, "Lowest": 0, "Highest": 0 } }
    }]);
    //login.controller('TabsController', function ($scope, $window) {
    //    this.tabs = [
    //      { title: 'Sign In', templateUrl: '../ExpenseTracker/SignInView', disabled: false },
    //      { title: 'Sign Up', templateUrl: '../ExpenseTracker/SignUpView', disabled: false }
    //    ];
    //});
    //push to main module
    window.mainapp.requires.push('logIn');
})(window.angular);