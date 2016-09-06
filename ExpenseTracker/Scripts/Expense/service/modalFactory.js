angular.module('ivizIntegration')
       .factory('modalfactory', ['$q', '$http', '$uibModal', '$log', function ($q, $http, $uibModal, $log) {
           var bootstrapModal = this;

           bootstrapModal.open = function (size, title, body,animation) {
               var modalInstance = $uibModal.open({
                   animation: animation == undefined ? true : animation,
                   templateUrl: 'static/modalPopup.html',
                   controller: 'ModalInstanceCtrl',
                   controllerAs: 'MoInsCtrl',
                   size: size,
                   resolve: {
                       items: function () {
                           return { title: title, content: body }
                       }
                   }
               });

               modalInstance.result.then(function (selectedItem) {
                   // $scope.selected = selectedItem;
               }, function () {
                   $log.info('Modal dismissed at: ' + new Date());
               });
           };

       }]).controller('ModalInstanceCtrl', function ($scope, $uibModalInstance, items) {
           var ModalInstance = this;
           ModalInstance.title = items.title;
           ModalInstance.content = items.content;
           ModalInstance.ok = function () {
               $uibModalInstance.close();
           };

           ModalInstance.cancel = function () {
               $uibModalInstance.dismiss('cancel');
           };
       });