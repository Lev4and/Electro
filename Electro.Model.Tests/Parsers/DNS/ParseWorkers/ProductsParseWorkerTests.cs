using Electro.Model.Parsers.DNS;
using Electro.Model.Parsers.DNS.ParseWorkers;
using FluentAssertions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Electro.Model.Tests.Parsers.DNS.ParseWorkers
{
    public class ProductsParseWorkerTests
    {
        private readonly PaginationParseWorker<Pagination> _paginationParseWorker;
        private readonly ProductsParseWorker<List<string>> _productsParseWorker;

        public ProductsParseWorkerTests()
        {
            _paginationParseWorker = new PaginationParseWorker<Pagination>(new PaginationParser());
            _productsParseWorker = new ProductsParseWorker<List<string>>(new ProductsParser());
        }

        [Fact]
        public async Task DoWork_WithLinkAndNumberPageParam_NotBeNullResult()
        {
            var listProductLinks = new List<string>();
            var pagination = await _paginationParseWorker.DoWork("catalog/17a8a1f916404e77/derzhateli-dlya-smartfonov/");

            for(int i = 1; i <= pagination.CountPages; i++)
            {
                listProductLinks.AddRange(await _productsParseWorker.DoWork("catalog/17a8a1f916404e77/derzhateli-dlya-smartfonov/", i, ""));
            }

            listProductLinks.Should().NotBeNull();
            listProductLinks.Should().HaveCountGreaterThan(0);
        }
    }
}
