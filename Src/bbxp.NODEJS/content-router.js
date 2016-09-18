var Router = require('restify-router').Router;
var router = new Router();
var RedisFactoryClient = require("./dbFactory");

function getContent(request, response, next) {
    return RedisFactoryClient(request.params.urlArg, response);
};

router.get('/node/Content/:urlArg', getContent);

module.exports = router;