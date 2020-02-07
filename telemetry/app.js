var express = require('express');
var bodyParser = require('body-parser');
var mongoose = require('mongoose');

var app = express();

//Import 
Simulation = require('./models/simulate');
SuitSwitch = require('./models/suitswitch');

//Database connector
mongoose.connect('mongodb://localhost/spacesuit');

//Returns all simulated data from the database
app.get('/api/suit', function(req, res){      
        Simulation.getSuitTelemetry(function (err, data) {
            if (err) {
                throw err;
                console.log(err);
            }
            res.json(data);
        });
});

app.get('/api/suit/recent', function(req, res){      
    Simulation.getSuitTelemetryByDate(function (err, data) {
        if (err) {
            throw err;
            console.log(err);
        }
        x = Date.now().toString; 
        console.log('Suit data accessed at!');
        res.json(data);
    });
});

app.get('/api/suitswitch', function(req, res){      
    SuitSwitch.getSuitSwitch(function (err, data) {
        if (err) {
            throw err;
            console.log(err);
        }
        res.json(data);
    });
});

app.get('/api/suitswitch/recent', function(req, res){      
    SuitSwitch.getSuitSwitchByDate(function (err, data) {
        if (err) {
            throw err;
            console.log(err);
        }
        console.log('Switch data accessed');
        res.json(data);
    });
});


app.listen(3000);
console.log('Server is running on port 3000...');

//Start simulation with time value
var time = Date.now(); 
var interval = setInterval(Simulation.suitTelemetry.bind(null, time),1000);
var interval_switch = setInterval(SuitSwitch.SuitSwitch,1000);
console.log("--------------Simulation started--------------")

