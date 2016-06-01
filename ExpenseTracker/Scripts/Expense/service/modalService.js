(function (angular) {
    var app = angular.module('expenseTracker')

    var injectParams = ['$uibModal'];

    var modalService = function ($uibModal) {
        var modalDefaults = {
            backdrop: true,
            keyboard: true,
            modalFade: true,
            templateUrl: 'static/modal.html'
        };

        var modalOptions = {
            closeButtonText: 'Close',
            actionButtonText: 'OK',
            headerText: 'Proceed?',
            bodyText: 'Perform this action?',
            tempObj: {}
        };

        this.showModal = function (customModalDefaults, customModalOptions) {
            if (!customModalDefaults) customModalDefaults = {};
            customModalDefaults.backdrop = 'static';
            return this.show(customModalDefaults, customModalOptions);
        };

        this.show = function (customModalDefaults, customModalOptions) {
            //Create temp objects to work with since we're in a singleton service
            var tempModalDefaults = {};
            var tempModalOptions = {};

            //Map angular-ui modal custom defaults to modal defaults defined in this service
            angular.extend(tempModalDefaults, modalDefaults, customModalDefaults);

            //Map modal.html $scope custom properties to defaults defined in this service
            angular.extend(tempModalOptions, modalOptions, customModalOptions);

            if (!tempModalDefaults.controller) {
                tempModalDefaults.controller = function ($scope, $uibModalInstance) {
                    $scope.modalOptions = tempModalOptions;
                    $scope.modalOptions.tempObj = {};
                    console.log($scope.modalOptions.tempObj);
                    $scope.modalOptions.ok = function (result) {
                        var obj = $scope.modalOptions.tempObj;
                        if (Object.keys(obj).length > 0) {
                            $uibModalInstance.close(obj);
                        }
                        else
                            $uibModalInstance.close("ok");
                    };
                    $scope.modalOptions.close = function (result) {
                        $uibModalInstance.close('cancel');
                    };
                };

                tempModalDefaults.controller.$inject = ['$scope', '$uibModalInstance'];
            }

            return $uibModal.open(tempModalDefaults).result;
        };
    };

    modalService.$inject = injectParams;

    app.service('modalService', modalService);
})(window.angular);