angular.module('ivizIntegration')
       .factory('Integration', ['$q', '$http', function ($q, $http) {

           var baseUrl = '../IvizIntegration/';
          

           var integrationService = {};
           integrationService.modalSubGroupItems = [];
           // Search Customers
           integrationService.getModalDetails = function (text) {
               var deferred = $q.defer();
               return $http({
                   url: baseUrl + 'GetClientGrid',
                   method: 'POST',
                   cache: false
               }).success(function (data) {
                   deferred.resolve(
                       integrationService.modalSubGroupItems = data);
               }).error(function (error) {
                   deferred.reject(error);
               })
               return deferred.promise;
           }

           return integrationService;
       }]);