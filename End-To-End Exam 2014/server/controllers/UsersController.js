var encryption = require('../utilities/encryption'),
    users = require('../data/users'),
    Event = require('mongoose').model('Event'),
    User = require('mongoose').model('User');

var CONTROLLER_NAME = 'users';

module.exports = {
    getRegister: function(req, res) {
        res.render(CONTROLLER_NAME + '/register');
    },
    postRegister: function(req, res, next) {
        var newUserData = req.body;

        if (newUserData.password != newUserData.confirmPassword) {
            req.session.error = 'Passwords do not match!';
            res.redirect('/register');
        }
        else if (!(newUserData.username.length > 5 && newUserData.username.length < 21)) {
            req.session.error = 'Username must be between 6 and 20 characters';
            res.redirect('/register');
        }
        else if (newUserData.email.length <= 0) {
            req.session.error = 'Email is required';
            res.redirect('/register');
        }
        else if (newUserData.firstName.length <= 0) {
            req.session.error = 'First Name is required';
            res.redirect('/register');
        }
        else if (newUserData.lastName.length <= 0) {
            req.session.error = 'Last Name is required';
            res.redirect('/register');
        }
        else {
            newUserData.salt = encryption.generateSalt();
            newUserData.hashPass = encryption.generateHashedPassword(newUserData.salt, newUserData.password);
            users.create(newUserData, function(err, user) {
                if (err) {
                    console.log('Failed to register new user: ' + err);
                    res.status(400);
                    return res.send({reason: 'Registration unsuccessfull, please try again later'});
                    return
                }

                req.logIn(user, function(err) {
                    if (err) {
                        res.status(400);
                        return res.send({reason: 'Registration successfull, but failed to log in'});
                    }
                    else {
                        res.redirect('/');
                    }
                });
            });
        }
    },
    getLogin: function(req, res, next) {
        res.render(CONTROLLER_NAME + '/login');
    },
    getProfile: function(req, res) {
        createdEvents = {};
        joinedEvents = {};
        passedEvents = {};

        Event.find().where('creatorName').equals(req.user.username).sort('date').exec(function(err, events) {
            if (err) throw err;
            createdEvents = events;
        });

        Event.find().where('studentsJoined').equals(req.user.username).exec(function(err, events) {
            if (err) throw err;
            joinedEvents = events;
        });

        Event.find().where('studentsJoined').equals(req.user.username).where('date').lt(new Date()).sort('date').exec(function(err, events) {
            if (err) throw err;
            passedEvents = events;
        });
        setTimeout(function(){
            res.render(CONTROLLER_NAME + '/profile', { createdEvents: createdEvents, joinedEvents: joinedEvents, passedEvents: passedEvents, currentUser: req.user});
        }, 200);
    },
    updateProfile: function(req, res){
        var newProfileData = req.body;
        console.log(newProfileData.facebook);
        var newUser = {};
        User.findOne().where('username').equals(req.user.username).exec(function(err, user) {
            user.profileImage = newProfileData.profileImage || user.profileImage;
            user.phoneNumber = newProfileData.phoneNumber || user.phoneNumber;
            user.links.facebook = newProfileData.facebook || user.links.facebook;
            user.links.twitter = newProfileData.twitter || user.links.twitter;
            user.links.linkedIn = newProfileData.linkedIn || user.links.linkedIn;
            user.links.googlePlus = newProfileData.googlePlus || user.links.googlePlus;

            newUser = user;
            user.remove(function(err, success) {
                users.create(newUser,  function(err, user) {
                    if (err) {
                        console.log('Failed to update profile: ' + err);
                        res.status(400);
                        return res.send({reason: 'Failed to make changes, please try again later'});
                        return
                    }

                    req.logIn(user, function(err) {
                        if (err) {
                            res.status(400);
                            return res.send({reason: 'Profile updated successfully, please log in again'});
                        }
                        else {
                            res.redirect('/');
                        }
                    });
                })
            })
        });
    }
};