var mongoose = require('mongoose');

//Create a schema - this is like a blueprint
var suitSwitchSchema = new mongoose.Schema({
    create_date: {
        type: Date,
        default: Date.now
    },
    sop_on: Boolean,
    sspe: Boolean,
    fan_error:  Boolean,
    vent_error: Boolean,
    vehicle_power: Boolean,
    h2o_off: Boolean,
    o2_off: Boolean
});

var SuitSwitch = mongoose.model('Mars', suitSwitchSchema);

module.exports.SuitSwitch = function(){
    var itemOne = SuitSwitch({
        sop_on: false,
        sspe: false,
        fan_error:  false,
        vent_error: false,
        vehicle_power: false,
        h2o_off: false,
        o2_off: false
        }).save(function(err){
        if (err) throw (err); 
    });
}

//Function to return all data from the database
module.exports.getSuitSwitch = function (callback, limit) {
	SuitSwitch.find({},{_id:0, __v:0},callback);
}

//Function to return the most recently created dataset
module.exports.getSuitSwitchByDate = function (callback, limit) {
	SuitSwitch.find({},{_id:0, __v:0},callback).sort({'create_date':-1}).limit(1);
}