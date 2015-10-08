app.factory('driversData', function driversData($resource, baseServiceUrl, identity, authorization) {
    if(identity.isAuthenticated()) {
        var headers = authorization.getAuthorizationHeader();
    }

    var resource = $resource(baseServiceUrl + '/api/Drivers?page=:page&username=:username', null, {
        'getDrivers': { method: 'GET', isArray: true},
        'getDriversPrivate': {method: 'GET', isArray:true, headers: headers}
    });

    return {
        getDrivers: function() {
            return resource.getDrivers();
        },
        getDriversPrivate: function(page, username) {
            return resource.getDriversPrivate({
                page: page,
                username: username
            });
        }
    }
});