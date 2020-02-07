var mongoose = require('mongoose');

var fullTime = 10*60*60*1000;

//Create a schema - this is like a blueprint
var suitTelemetrySchema = new mongoose.Schema({
    create_date: {
        type: Date,
        default: Date.now
    },
    heart_bpm: String,
    p_suit: String,
    p_sub: String,
    t_sub:  String,
    v_fan: String,
    t_eva: String,
    p_o2: String,
    rate_o2: String,
    cap_battery: String,
    p_h2o_g: String,
    p_h2o_l: String,
    p_sop: String,
    rate_sop: String,
    t_battery:String,
    t_oxygen:String,
    t_water:String
});

var SuitData = mongoose.model('Moon', suitTelemetrySchema);

module.exports.suitTelemetry = function(t){
    var itemOne = SuitData({
        heart_bpm: heartBeat(),
        p_suit: pressureSuit(),
        p_sub: pressureSUB(),
        t_sub: tempSub(),
        v_fan: velocFan(),
        p_o2:pressureOxygen(),
        rate_o2:rateOxygen(),
        cap_battery:capacityBattery(),
        p_h2o_g:pressureWaterGas(),
        p_h2o_l:pressureWaterLiquid(),
        p_sop:pressureSOP(),
        rate_sop:rateSOP(),
        t_battery:batteryLife(t),
        t_oxygen:oxygenLife(t),
        t_water:waterLife(t)
        }).save(function(err){
        if (err) throw (err); 
    });
}

function secondsToHms(d) {
    d = Number(d);
    var h = Math.floor(d / 3600);
    var m = Math.floor(d % 3600 / 60);
    var s = Math.floor(d % 3600 % 60);

    var time = h + ':' + m + ':' + s;
    return time
}

function batteryLife(t){  
    var elapsed = Date.now() - t;
    t_remaining = fullTime - (elapsed*10); 
    t_battery = secondsToHms(Math.floor(t_remaining/1000));
    console.log(Math.floor(elapsed/1000) + ' s');
    return t_battery; 
};

function oxygenLife(t){  
    var elapsed = Date.now() - t;
    t_remaining = fullTime - elapsed; 
    t_oxygen = secondsToHms(Math.floor(t_remaining/1000));
    return t_oxygen;
};

function waterLife(t){  
    var elapsed = Date.now() - t;
    t_remaining = fullTime - elapsed; 
    t_water = secondsToHms(Math.floor(t_remaining/1000));
    return t_water; 
};

function heartBeat(){
    max = 90; 
    min = 85; 
    heart_bpm = Math.random() * (max - min) + min;
    return heart_bpm.toFixed(0); 
};

function pressureSuit(){
    max = 4.00; 
    min = 3.92; 
    p_suit = Math.random() * (max - min) + min;
    return p_suit.toFixed(2); 
};


function pressureSUB(){
    max = 7.99; 
    min = 7.85; 
    p_sub = Math.random() * (max - min) + min;
    return p_sub.toFixed(2); 
};

function tempSub(){
    max = 6; 
    min = 4; 
    t_sub = Math.random() * (max - min) + min;
    return t_sub.toFixed(0); 
};

function velocFan(){
    max = 40000; 
    min = 39900; 
    v_fan = Math.random() * (max - min) + min;
    return v_fan.toFixed(0); 
};

function pressureOxygen(){
    max = 16; 
    min = 15; 
    p_o2 = Math.random() * (max - min) + min;
    return p_o2.toFixed(0); 
};

function rateOxygen(){
    max = 1; 
    min = 0.8; 
    rate_o2 = Math.random() * (max - min) + min;
    return rate_o2.toFixed(1); 
};

function capacityBattery(){
    max = 30; 
    min = 28; 
    cap_battery = Math.random() * (max - min) + min;
    return cap_battery.toFixed(0); 
};

function pressureWaterGas(){
    max = 16; 
    min = 15; 
    p_h2o_g = Math.random() * (max - min) + min;
    return p_h2o_g.toFixed(0); 
};

function pressureWaterLiquid(){
    max = 16; 
    min = 15; 
    p_h2o_l = Math.random() * (max - min) + min;
    return p_h2o_l.toFixed(0); 
};

function pressureSOP(){
    max = 950; 
    min = 850; 
    p_sop = Math.random() * (max - min) + min;
    return p_sop.toFixed(0); 
};

function rateSOP(){
    max = 1; 
    min = 0.9; 
    rate_sop = Math.random() * (max - min) + min;
    return rate_sop.toFixed(1); 
};

//Function to return all data from the database
module.exports.getSuitTelemetry = function (callback, limit) {
	SuitData.find({},{_id:0, __v:0},callback);
}

//Function to return the most recently created dataset
module.exports.getSuitTelemetryByDate = function (callback, limit) {
	SuitData.find({},{_id:0, __v:0},callback).sort({'create_date':-1}).limit(1);
}