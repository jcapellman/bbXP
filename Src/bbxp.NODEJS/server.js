var restify = require("restify");
var settings = require("./config");

var postsRouter = require("./posts-router");
var postArchiveRouter = require("./postarchive-router");

var server = restify.createServer();

server.use(restify.queryParser());

postsRouter.applyRoutes(server);
postArchiveRouter.applyRoutes(server);

server.listen(settings.HTTP_SERVER_PORT);