using Electro.Model.Parsers.DNS;
using Electro.Model.Parsers.DNS.ParseWorkers;
using FluentAssertions;
using System.Threading.Tasks;
using Xunit;

namespace Electro.Model.Tests.Parsers.DNS.ParseWorkers
{
    public class PaginationParseWorkerTests
    {
        private readonly PaginationParseWorker<Pagination> _parseWorker;

        public PaginationParseWorkerTests()
        {
            _parseWorker = new PaginationParseWorker<Pagination>(new PaginationParser());
        }

        [Fact]
        public async Task DoWork_WithLinkParam_NotBeNullResult()
        {
            var pagination = await _parseWorker.DoWork("https://www.dns-shop.ru/catalog/17a892f816404e77/noutbuki/");

            pagination.Should().NotBeNull();
            pagination.CountPages.Should().BeGreaterThan(0);
        }
    }
}
