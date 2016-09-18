var Router = require('restify-router').Router;
var router = new Router();
var RedisFactoryClient = require("./dbFactory");

function getListing(request, response, next) {
    return RedisFactoryClient("PostListing", response);
};

function getSinglePost(request, response, next) {
    return RedisFactoryClient(request.params.urlArg, response);    
};

router.get('/node/Posts', getListing);
router.get('/node/Posts/:urlArg', getSinglePost);

module.exports = router;