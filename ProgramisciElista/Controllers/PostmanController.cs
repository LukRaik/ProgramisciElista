﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web.Http;
using System.Web.Http.Description;
using Core.Interfaces;
using Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Schema;
using ProgramisciElista.Postman;

namespace WebApiServer.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class PostmanController : ApiController
    {
        private ILogger _logger;
        public PostmanController(ILogger logger)
        {
            logger.Log("init Postman Api Controller");
            logger.Log("Postman api at http://localhost:50000/Postman/Get see postman for more info");
            _logger = logger;
        }
        [AllowAnonymous]
        public HttpResponseMessage Get()
        {

            _logger.Log("Get api");
            var collection = Configuration.Properties.GetOrAdd("postmanCollection", k =>
            {
                var requestUri = Request.RequestUri;
                string baseUri = requestUri.Scheme + "://" + requestUri.Host + ":" + requestUri.Port + "";
                var postManCollection = new PostmanCollection();
                postManCollection.id = Guid.NewGuid();
                postManCollection.name = "Elista collection";
                postManCollection.timestamp = DateTime.Now.Ticks;
                postManCollection.requests = new Collection<PostmanRequest>();
                foreach (var apiDescription in Configuration.Services.GetApiExplorer().ApiDescriptions)
                {
                    var sendObj = apiDescription.ActionDescriptor.GetParameters().FirstOrDefault();
                    _logger.Log($"Found {apiDescription.ActionDescriptor.ActionName}", "PostmanApi");
                    var request = new PostmanRequest
                    {
                        collectionId = postManCollection.id,
                        id = Guid.NewGuid(),
                        method = apiDescription.HttpMethod.Method,
                        url = baseUri.TrimEnd('/') + "/" + apiDescription.RelativePath,
                        description = apiDescription.Documentation,
                        name = apiDescription.RelativePath,
                        data = sendObj != null && sendObj.ParameterType!=typeof(string) ? JsonConvert.SerializeObject(Activator.CreateInstance(sendObj.ParameterType)) : "",
                        headers = "",
                        dataMode = "raw",
                        timestamp = 0
                    };
                    postManCollection.requests.Add(request);
                }
                return postManCollection;
            }) as PostmanCollection;

            _logger.Log("Api generated :)");
            return Request.CreateResponse<PostmanCollection>(HttpStatusCode.OK, collection, "application/json");
        }
    }
}