using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyEvents.Core
{
    public class FieldsBuilder
    {
        private readonly StringBuilder _builder = new StringBuilder();

        public FieldsBuilder WithField(string filed)
        {
            if (_builder.Length == 0)
                _builder.Append(filed);
            else
                _builder.Append("," + filed);

            return this;
        }

        public string Build()
        {
            return _builder.ToString();
        }
    }
}
