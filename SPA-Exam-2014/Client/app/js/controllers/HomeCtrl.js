app.controller('HomeCtrl', function HomeCtrl($scope, statsData, driversData, tripsData) {
    $scope.stats = statsData.getStats();

    $scope.drivers = driversData.getDrivers();

    $scope.trips = tripsData.getTrips();
});