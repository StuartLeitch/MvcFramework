using System.Linq;
using System.Text;
using System.Collections.Specialized;

namespace Infrastructure.Core
{
    public class QueryStringBuilder : NameValueCollection
    {
        public QueryStringBuilder()
        {
        }

        public QueryStringBuilder(NameValueCollection queryString)
            : base(queryString)
        {
        }

        public override void Add(string name, string value)
        {
            if (this.Get(name) != null)
            {
                this.Set(name, value);
            }
            else
            {
                base.Add(name, value);
            }
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            foreach (string str in this.AllKeys)
                builder.Append(string.Format("&{0}={1}", str, base[str]));
            
            if (builder.Length > 0)
            {
                builder.Remove(0, 1);
                builder.Insert(0, "?");
            }
            
            return builder.ToString();
        }
    }
}
