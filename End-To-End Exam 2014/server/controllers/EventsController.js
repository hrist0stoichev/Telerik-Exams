var events = require('../data/events'),
    Event = require('mongoose').model('Event');

var CONTROLLER_NAME = 'events';

module.exports = {
    getCreate: function(req, res) {
        res.render(CONTROLLER_NAME + '/create', {currentUser: req.user});
    },
    postCreate: function(req, res, next) {
        var newEventData = req.body;

        if (newEventData.title.length <= 0) {
            req.session.error = 'You must enter a title!';
            res.redirect('/create-event');
        }
        else if (newEventData.description.length <= 0) {
            req.session.error = 'You must enter a brief description!';
            res.redirect('/create-event');
        }
        else {
            newEventData.creatorName = req.user.username;
            newEventData.creatorPhone = req.user.phoneNumber;
            events.create(newEventData, function(err, event) {
                if (err) {
                    console.log('Failed to register new event: ' + err);
                    res.status(400);
                    return res.send({reason: 'Failed to create an event, please try again later'});
                    return;
                }
                else {
                    res.redirect('/');
                }
            });
        }
    },
    listEvents: function(req, res) {
        var eventsToList = {};
        var page = req.params.page;
        Event.find().where('date').gt(new Date()).sort('date').exec(function(err, events) {
            var eventPages = ((events.length / 10) | 0) + 1;
            for (var i = (page - 1) * 10; i < page * 10; i++) {
                if (!events[i]) break;
                eventsToList[i - ((page - 1) * 10)] = events[i];
            }

            res.render(CONTROLLER_NAME + '/list-events', { events: eventsToList, pages: eventPages, currentUser: req.user});
        })
    },
    listPastEvents: function(req, res) {
        var eventsToList = {};
        var page = req.params.page;
        Event.find().where('date').lt(new Date()).sort('date').exec(function(err, events) {
            var eventPages = ((events.length / 10) | 0) + 1;
            for (var i = (page - 1) * 10; i < page * 10; i++) {
                if (!events[i]) break;
                eventsToList[i - ((page - 1) * 10)] = events[i];
            }
            res.render(CONTROLLER_NAME + '/list-past-events', { events: eventsToList, pages: eventPages, currentUser: req.user});
        })
    }
};