app.factory('tripsInfoData', function tripsInfoData($resource, baseServiceUrl, identity, authorization) {
    if(identity.isAuthenticated()) {
        var headers = authorization.getAuthorizationHeader();
    }

    var resource = $resource(baseServiceUrl + '/api/trips/:id', null, {
        'getTripsInfo': { method: 'GET', isArray: false, headers:headers},
        'join': {method: 'PUT', params: {id: '@id'}, isArray: true, headers:headers}
    });

    return {
        getTripsInfo: function(id) {
            return resource.getTripsInfo({id: id});
        },
        join: function(id) {
            return resource.join(id);
        }
    }
});