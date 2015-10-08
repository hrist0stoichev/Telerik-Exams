app.factory('statsData', function statsData($resource, baseServiceUrl) {
    var resource = $resource(baseServiceUrl + '/api/Stats', null, {
        'getStats': { method: 'GET', isArray: false}
    });

    return {
        getStats: function() {
            return resource.getStats();
        }
    }
});