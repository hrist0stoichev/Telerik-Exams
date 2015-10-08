var mongoose = require('mongoose');

module.exports.init = function() {
    var eventSchema = mongoose.Schema({
        title: { type: String, required: '{PATH} is required', unique: true },
        description: { type: String, required: '{PATH} is required'},
        location: {
            latitude: String,
            longitude: String
        },
        category: String,
        type: {
            initiative: String,
            season: String
        },
        creatorName: String,
        creatorPhone: String,
        comments: [String],
        date: { type: Date, required: '{PATH} is required' },
        studentsJoined: [String]
    });

    var Event = mongoose.model('Event', eventSchema);
};


