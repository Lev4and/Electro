using AngleSharp.Html.Parser;
using System.Threading.Tasks;

namespace Electro.Model.Parsers
{
    public abstract class BaseParseWorker<T> where T : class
    {
        protected private readonly IParser<T> _parser;
        protected private readonly HtmlParser _htmlParser;

        public BaseParseWorker(IParser<T> parser)
        {
            _parser = parser;
            _htmlParser = new HtmlParser();
        }

        public virtual async Task<T> DoWork()
        {
            return default(T);
        }

        public virtual async Task<T> DoWork(string link)
        {
            return default(T);
        }

        public virtual async Task<T> DoWork(string link, int numberPage)
        {
            return default(T);
        }

        public virtual async Task<T> DoWork(string link, int numberPage, string token)
        {
            return default(T);
        }
    }
}
