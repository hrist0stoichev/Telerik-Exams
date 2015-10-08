app.factory('citiesData', function citiesData($resource, baseServiceUrl) {
    var resource = $resource(baseServiceUrl + '/api/cities', null, {
        'getCities': { method: 'GET', isArray: true}
    });

    return {
        getCities: function() {
            return resource.getCities();
        }
    }
});