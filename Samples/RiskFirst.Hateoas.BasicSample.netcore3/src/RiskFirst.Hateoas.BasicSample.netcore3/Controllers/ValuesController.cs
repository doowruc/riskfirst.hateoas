﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RiskFirst.Hateoas.BasicSample.netcore3.Models;
using RiskFirst.Hateoas.BasicSample.netcore3.Repository;
using RiskFirst.Hateoas.Models;

namespace RiskFirst.Hateoas.BasicSample.netcore3.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ValuesController : ControllerBase
    {
        private readonly IValuesRepository repo;
        private readonly ILinksService linksService;

        public ValuesController(IValuesRepository repo, ILinksService linksService)
        {
            this.repo = repo;
            this.linksService = linksService;
        }

        // GET api/values
        [FormatFilter]
        [HttpGet(Name = "GetAllValuesRoute")]
        public async Task<ItemsLinkContainer<ValueInfo>> Get()
        {
            var values = await GetAllValuesWithLinksAsync();

            var result = new ItemsLinkContainer<ValueInfo>()
            {
                Items = values
            };
            await linksService.AddLinksAsync(result);
            return result;
        }

        // GET api/values/5
        [FormatFilter]
        [HttpGet("{id}", Name = "GetValueByIdRoute")]
        [HttpGet("v2/{id}", Name = "GetValueByIdRouteV2")]
        [Links(Policy = "FullInfoPolicy")]
        public async Task<ValueInfo> Get(int id)
        {
            var value = await repo.GetValueAsync(id);
            await linksService.AddLinksAsync(value);
            return value;
        }

        // POST api/values
        [HttpPost(Name = "InsertValueRoute")]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}", Name = "UpdateValueRoute")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}", Name = "DeleteValueRoute")]
        public void Delete(int id)
        {
        }

        private async Task<List<ValueInfo>> GetAllValuesWithLinksAsync()
        {
            var values = await repo.GetAllValuesAsync();
            values.ForEach(async x => await linksService.AddLinksAsync(x));
            return values; ;
        }
    }
}