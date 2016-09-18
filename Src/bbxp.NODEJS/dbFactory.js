var redis = require("redis");
var settings = require('./config');

var client = redis.createClient(settings.REDIS_DATABASE_PORT, settings.REDIS_DATABASE_HOSTNAME);

client.on("error", function (err) {
    console.log("Error " + err);
});

module.exports = client;