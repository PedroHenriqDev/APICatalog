
namespace APICatalog.Filters
{
    internal class ResponseErrorsJson
    {
        private IList<string> list;

        public ResponseErrorsJson(IList<string> list)
        {
            this.list = list;
        }
    }
}