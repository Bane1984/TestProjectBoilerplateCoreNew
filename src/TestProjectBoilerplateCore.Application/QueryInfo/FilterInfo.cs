using System.Collections.Generic;
using Abp.Domain.Entities;

namespace TestProjectBoilerplateCore.QueryInfo
{
    public class FilterInfo
    {
        public string Condition { get; set; }
        public List<RuleInfo> Rules = new List<RuleInfo>();
    }
}
