var Router = require('restify-router').Router;
var router = new Router();
var RedisClient = require('./dbFactory');

function respond(request, response, next) {
    var argId = request.params.id;

    RedisClient.set(argId.toString(), 2);

    return response.json({ message: true });
}

router.get('/v1/Posts', respond);

module.exports = router;