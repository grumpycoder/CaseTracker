//jurisdiction-list.component.js
(function () {
    var module = angular.module('app');

    function controller($http, $modal) {
        var $ctrl = this;

        $ctrl.title = 'Jurisdiction Manager';
        $ctrl.subTitle = 'Jurisdictions';
        $ctrl.isBusy = false;

        $ctrl.$onInit = function () {
            console.log('jurisdiction list init');
            $ctrl.isBusy = true;
            $http.get('api/jurisdiction/list').then(function (r) {
                console.log('r', r);
                $ctrl.jurisdictions = r.data;
            }).finally(function () {
                $ctrl.isBusy = false;
            });
        }

        $ctrl.selectJurisidiction = function (j) {
            console.log('select', j);
            $ctrl.selectedJurisidiction = j;
        }

        $ctrl.openModal = function (jurisdiction) {
            console.log('selected', jurisdiction);
            $modal.open({
                component: 'jurisdictionEdit',
                bindings: {
                    modalInstance: "<"
                },
                resolve: {
                    jurisdiction: jurisdiction
                },
                size: 'md'
            }).result.then(function (result) {
                console.log('updated', result);
                if (jurisdiction === undefined) {
                    $ctrl.jurisdictions.unshift(result);
                } else {
                    angular.extend(jurisdiction, result);
                }
            }, function (reason) {});
        }
    }

    module.component('jurisdictionList', {
        templateUrl: 'app/jurisdictions/jurisdiction-list.component.html',
        controller: ['$http', '$uibModal', controller]
    });
})();