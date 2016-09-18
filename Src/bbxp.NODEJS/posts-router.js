var Router = require('restify-router').Router;
var router = new Router();
var RedisClient = require('./dbFactory');

function getListing(request, response, next) {
    RedisClient.get("PostListing", function (err, reply) {
        if (reply == null) {
            return response.write('');
        }

        response.writeHead(200, { 'Content-Type': 'application/json' });

        response.end(reply);

        return response;
    });
};

function getSinglePost(request, response, next) {
    var urlArg = request.params.urlArg;

    RedisClient.get(urlArg, function (err, reply) {        
        if (reply == null) {
            response.writeHead(200, { 'Content-Type': 'application/json' });

            response.end('');

            return response;
        }

        response.writeHead(200, { 'Content-Type': 'application/json' });

        response.end(reply);
        
        return response;
    });
};

router.get('/node/Posts', getListing);
router.get('/node/Posts/:urlArg', getSinglePost);

module.exports = router;