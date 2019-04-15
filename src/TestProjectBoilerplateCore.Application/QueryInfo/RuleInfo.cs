using System.Collections.Generic;
using Abp.Domain.Entities;

namespace TestProjectBoilerplateCore.QueryInfo
{
    public class RuleInfo
    {
        public string Property { get; set; }
        public string Operator { get; set; }
        public string Value { get; set; }
        public string Condition { get; set; }
        public List<RuleInfo> Rules = new List<RuleInfo>();
    }
}
