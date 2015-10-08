'use strict';

app.controller('DriversCtrl', function($scope, driversData, identity) {
    $scope.currentPage = 1;
    $scope.isDisabled = true;
    if(identity.isAuthenticated()) {
        $scope.isAuth = false;
    }
    else {
        $scope.isAuth = true;
    }

    filter($scope.currentPage, $scope.username);

    $scope.filterDrivers = function(page, username) {
        filter(page, username);
    };

    $scope.nextPage = function() {
        $scope.currentPage++;
        filter($scope.currentPage, $scope.username);
        if($scope.isDisabled) {
            $scope.isDisabled = false;
        }
    };

    $scope.previousPage = function() {
        if ($scope.currentPage <= 1) {
            return;
        }

        $scope.currentPage--;
        if ($scope.currentPage <= 1) {
            $scope.isDisabled = true;
        }

        filter($scope.currentPage, $scope.username);
    };

    function filter(currentPage, username) {
        $scope.drivers = driversData.getDriversPrivate(currentPage, username)
    }
});