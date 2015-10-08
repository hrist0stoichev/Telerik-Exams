var mongoose = require('mongoose'),
    encryption = require('../../utilities/encryption');

module.exports.init = function() {
    function validateUsernameLength (val) {
        return val.length > 5 && val.length < 21;
    }

    var userSchema = mongoose.Schema({
        username: { type: String, required: '{PATH} is required', unique: true,
            validate: [
                { validator: validateUsernameLength, msg: 'Invalid {PATH} length' }
            ] },
        eventPoints: { type: Number, default: 0 },
        firstName: { type: String, required: '{Path} is required' },
        lastName: { type: String, required: '{Path} is required' },
        phoneNumber: String,
        email: { type: String, required: '{Path} is required' },
        telerikAcademyInitiatives: [String],
        joinedEvents: [String],
        profileImage: { data: Buffer, contentType: String },
        links: {
            facebook: String,
            twitter: String,
            linkedIn: String,
            googlePlus: String
        },
        salt: String,
        hashPass: String
    });

    userSchema.method({
        authenticate: function(password) {
            if (encryption.generateHashedPassword(this.salt, password) === this.hashPass) {
                return true;
            }
            else {
                return false;
            }
        }
    });

    var User = mongoose.model('User', userSchema);
};


