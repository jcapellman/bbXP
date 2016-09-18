var Router = require('restify-router').Router;
var router = new Router();
var RedisFactoryClient = require("./dbFactory");

function getListing(request, response, next) {
    return RedisFactoryClient("PostArchive", response);
};

function getMonthPosts(request, response, next) {
    var key = request.params.year + "-" + request.params.month;

    return RedisFactoryClient(key, response);
};

router.get('/node/PostArchive', getListing);
router.get('/node/PostArchive/:year/:month', getMonthPosts);

module.exports = router;