﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NUnit.Framework;

namespace IntegrationTestsPOC.IntegrationTests.Controllers
{
    [TestFixture]
    public class PeopleControllerTests : BaseServerTests
    {
        [Test]
        public async Task ShouldGetAll()
        {
            // arrange
            var expectedJson = JsonConvert.SerializeObject(new List<dynamic>
            {
                new {id = 1, firstName = "Andrew", lastName = "Peters"},
                new {id = 2, firstName = "Brice", lastName = "Lambson"},
                new {id = 3, firstName = "Rowan", lastName = "Miller"}
            });

            var mimeType = "application/json";

            // act & assert
            Action<HttpResponseMessage> assert = response =>
            {
                response.ShouldContainContent(mimeType, expectedJson);
            };

            await RequestGetForAsync("api/people", mimeType, assert);
        }

        [Test]
        public async Task ShouldAddNewPersonOnPost()
        {
            // arrange
            var mimeType = "application/json";
            var postBody = new { firstName = "Mike", lastName = "Morelez" };

            var expectedJson = JsonConvert.SerializeObject
            (
                new { id = 4, firstName = "Mike", lastName = "Morelez" }
            );

            // act & assert
            Action<HttpResponseMessage> assert = response =>
            {
                response.ShouldContainContent(mimeType, expectedJson);
            };

            await RequestPostForAsync("api/people", mimeType, postBody, assert);
        }
    }
}