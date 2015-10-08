'use strict';

app.controller('TripsCtrl', function($scope, tripsData, citiesData, $location, identity, tripsInfoData) {
    $scope.cities = citiesData.getCities();
    $scope.orderBy = 'asc';
    $scope.sortBy = 'date';
    $scope.currentPage = 1;
    $scope.isDisabled = true;
    if(identity.isAuthenticated()) {
        $scope.isAuth = false;
    }
    else {
        $scope.isAuth = true;
    }

    var id = $location.url().substr(7);
    if (id.length === 36){
        $scope.tripInfo = tripsInfoData.getTripsInfo(id);
    }

    filter(1, 'date', 'asc');

    $scope.joinIn = function(id) {
        tripsInfoData.join(id);
    };

    $scope.filterTrips = function(page, orderBy, orderType, from, to, finished, onlyMine) {
        filter(page, orderBy, orderType, from, to, finished, onlyMine);
    };

    $scope.createTrip = function(trip) {
        tripsData.create(trip);
        $location.url('/trips');
    };

    $scope.nextPage = function() {
        $scope.currentPage++;
        filter($scope.currentPage, $scope.sortBy, $scope.orderBy, $scope.from, $scope.to, $scope.includeFinished, $scope.onlyMine);
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

        filter($scope.currentPage, $scope.sortBy, $scope.orderBy, $scope.from, $scope.to, $scope.includeFinished, $scope.onlyMine);
    };

    function filter(page, orderBy, orderType, from, to, finished, onlyMine) {
        $scope.trips = tripsData.getTripsPrivate(page, orderBy,  orderType, from, to, finished, onlyMine);
    }
});