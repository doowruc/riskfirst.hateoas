using RiskFirst.Hateoas.Models;

namespace RiskFirst.Hateoas.BasicSample.netcore3.Models
{
    public class ValueInfo : LinkContainer
    {
        public int Id { get; set; }
        public string Value { get; set; }
    }
}