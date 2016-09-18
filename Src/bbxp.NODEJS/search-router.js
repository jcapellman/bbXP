var Router = require('restify-router').Router;
var router = new Router();
var RedisFactoryClient = require("./dbFactory");

function searchPosts(request, response, next) {
    return RedisFactoryClient("bbxpSQ" + "_" + request.params.query, response);
};

router.get('/node/Search/:query', searchPosts);

module.exports = router;