using System.Collections.Generic;

namespace SparkEjs
{
    public class RulesEngine
    {
        public RulesEngine()
        {
            BaseRules = GetRules();
        }

        public List<string> BaseRules { get; set; }

        public void RefreshList()
        {
            BaseRules = GetRules();
        }

        private List<string> GetRules()
        {
            var list = new List<string>
            {
                "<p if= | <p QuieroEsteIfDe esta manera",
                "<root | <maat",
                "else | elseif",
                "<a | <anchor"
            };
            return list;
        }
    }
}