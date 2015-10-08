var auth = require('./auth'),
    controllers = require('../controllers'),
    Event = require('mongoose').model('Event');

module.exports = function(app) {
    app.get('/register', controllers.users.getRegister);
    app.post('/register', controllers.users.postRegister);

    app.get('/login', controllers.users.getLogin);
    app.post('/login', auth.login);

    app.get('/logout', auth.isAuthenticated, auth.logout);

    app.get('/profile', auth.isAuthenticated, controllers.users.getProfile);
    app.post('/profile', auth.isAuthenticated, controllers.users.updateProfile)

    app.get('/events/:page', auth.isAuthenticated, controllers.events.listEvents);
    app.get('/past-events/:page', auth.isAuthenticated, controllers.events.listPastEvents);

    app.get('/create-event', auth.isAuthenticated, controllers.events.getCreate);
    app.post('/create-event', auth.isAuthenticated, auth.hasPhoneNumber, controllers.events.postCreate);

    app.get('/', function(req, res) {
        Event.find().where('date').lt(new Date()).exec(function(err, events) {
            if (err) throw err;
            res.render('index', {currentUser: req.user, passedEvents: events});
        });
    });

    app.get('*', function(req, res) {
        Event.find().where('date').lt(new Date()).exec(function(err, events) {
            if (err) throw err;
            res.render('index', {currentUser: req.user, passedEvents: events});
        });
    });
};