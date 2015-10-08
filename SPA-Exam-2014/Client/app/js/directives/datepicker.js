app.directive('datepicker', function() {
    return {
        restrict: 'A',
        link: function(scope, element, attribute) {
            element.datetimepicker();
        }
    }
});