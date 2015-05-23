var cserver = require ("./CacheServer.js");
var clegserver = require ("./LegacyCacheServer.js");
var path = require('path');

/**
 * parse cmd line argument
 * @todo should use existing module, like optimist
 *
 * @return {Object} an object containing the parsed arguments if found
 */
function ParseArguments ()
{
	var res = {};
	res.legacy = true;
	res.legacyCacheDir = "./cache";
	res.cacheDir = "./cache5.0";
	
	for (var i = 2; i<process.argv.length; i++)
	{
		var arg = process.argv[i];

		if (arg.indexOf ("--size") == 0) 
		{
			res.size = parseInt (process.argv[++i]);
		} 
		else if (arg.indexOf ("--path") == 0) 
		{
			res.cacheDir = process.argv[++i];
		} 
		else if (arg.indexOf ("--legacypath") == 0) 
		{
			res.legacyCacheDir = process.argv[++i];
		} 
		else if (arg.indexOf ("--port") == 0) 
		{
			res.port = parseInt (process.argv[++i]);
		}
		else if (arg.indexOf ("--nolegacy") == 0) 
		{
			res.legacy = false;
		}
		else 
		{
			if (arg.indexOf ("--help") != 0)
			{
				console.log("Unknown option: " + arg);
			}
			console.log ("Usage: node main.js [--port serverPort] [--path pathToCache] [--legacypath pathToCache] [--size maximumSizeOfCache] [--nolegacy]\n" +
				     "--port: specify the server port, only apply to new cache server, default is 8126\n" +
				     "--path: specify the path of the cache directory, only apply to new cache server, default is ./cache5.0\n" +
				     "--legacypath: specify the path of the cache directory, only apply to legacy cache server, default is ./cache\n" +
				     "--size: specify the maximum allowed size of the LRU cache for both servers. Files that have not been used recently will automatically be discarded when the cache size is exceeded\n" +
				     "--nolegacy: do not start legacy cache server, otherwise legacy cache server will start on port 8125.");
			process.exit (0);
		}
	}

	return res;
}

var res = ParseArguments ();
if (res.legacy)
{
	if (res.port && res.port == clegserver.GetPort ())
	{
		console.log ("Cannot start Cache Server and Legacy Cache Server on the same port.");
		process.exit (1);
	}
	
	if (path.resolve (res.cacheDir) == path.resolve (res.legacyCacheDir))
	{
		console.log ("Cannot use same cache for Cache Server and Legacy Cache Server.");
		process.exit (1);
	}
	
	clegserver.Start (res.size, res.legacyCacheDir, null, function (res) 
	{
		console.log ("Unable to start Legacy Cache Server");
		process.exit (1);
	});

	setTimeout (function ()
	{
		clegserver.log (clegserver.INFO, "Legacy Cache Server on port " + clegserver.GetPort ());
		clegserver.log (clegserver.INFO, "Legacy Cache Server is ready");
	}, 50);
}

cserver.Start (res.size, res.port, res.cacheDir, null, function (res) 
{
	console.log ("Unable to start Cache Server");
	process.exit (1);
});

setTimeout (function ()
{
	// Inform integration tests that the cache server is ready
	cserver.log (cserver.INFO, "Cache Server on port " + cserver.GetPort ());
	cserver.log (cserver.INFO, "Cache Server is ready");
}, 50);
