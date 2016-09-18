var Router = require('restify-router').Router;
var router = new Router();
var RedisClient = require('./dbFactory');

function respond(request, response, next) {
    RedisClient.get("PostListing", function (err, reply) {
        if (reply == null) {
            return response.json('');
        }

        return response.json(reply);
    });
};

router.get('/node/Posts', respond);

module.exports = router;