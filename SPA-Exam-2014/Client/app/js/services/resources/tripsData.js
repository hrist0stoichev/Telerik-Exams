app.factory('tripsData', function tripsData($resource, baseServiceUrl, identity, authorization) {
    if(identity.isAuthenticated()) {
        var headers = authorization.getAuthorizationHeader();
    }

    var resource = $resource(baseServiceUrl + '/api/trips?page=:page&orderBy=:orderBy&orderType=:orderType&from=:from&to=:to&finished=:finished&onlyMine=:onlyMine', null, {
        'getTrips': { method: 'GET', isArray: true},
        'getTripsPrivate': { method: 'GET', isArray: true, headers: headers},
        'create': { method: 'POST', isArray:false, headers: headers}
    });

    return {
        getTrips: function() {
            return resource.getTrips();
        },
        getTripsPrivate: function(page, orderBy, orderType, from, to, finished, onlyMine) {
            return resource.getTripsPrivate({
                page: page,
                orderBy: orderBy,
                orderType: orderType,
                from: from,
                to: to,
                finished: finished,
                onlyMine: onlyMine
            });
        },
        create: function(trip) {
            resource.create(trip);
        }
    }
});