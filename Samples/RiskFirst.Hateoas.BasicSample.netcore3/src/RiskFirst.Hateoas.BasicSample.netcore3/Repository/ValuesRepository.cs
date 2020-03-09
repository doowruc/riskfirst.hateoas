﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RiskFirst.Hateoas.BasicSample.netcore3.Models;

namespace RiskFirst.Hateoas.BasicSample.netcore3.Repository
{
    public interface IValuesRepository
    {
        Task<List<ValueInfo>> GetAllValuesAsync();

        Task<ValueInfo> GetValueAsync(int id);
    }

    public class ValuesRepository : IValuesRepository
    {
        private static IDictionary<int, string> values = new Dictionary<int, string>()
        {
            {1,"Value One" },
            {2,"Value Two" },
            { 3,"Value Three" },
        };

        public async Task<List<ValueInfo>> GetAllValuesAsync()
        {
            return values.Select(v => new ValueInfo()
            {
                Id = v.Key,
                Value = v.Value
            }).ToList();
        }

        public async Task<ValueInfo> GetValueAsync(int id)
        {
            return (values.ContainsKey(id))
                ? new ValueInfo()
                {
                    Id = id,
                    Value = values[id]
                }
                : null;
        }
    }
}