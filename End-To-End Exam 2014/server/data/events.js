var Event = require('mongoose').model('Event');

module.exports = {
    create: function(user, callback) {
        Event.create(user, callback);
    }
};