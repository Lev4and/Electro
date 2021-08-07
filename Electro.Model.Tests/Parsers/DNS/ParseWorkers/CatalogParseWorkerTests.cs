using Electro.Model.Database.Entities;
using Electro.Model.Parsers.DNS;
using Electro.Model.Parsers.DNS.ParseWorkers;
using FluentAssertions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Electro.Model.Tests.Parsers.DNS.ParseWorkers
{
    public class CatalogParseWorkerTests
    {
        private readonly CategoryParseWorker<Dictionary<string, Category>> _parseWorker;

        public CatalogParseWorkerTests()
        {
            _parseWorker = new CategoryParseWorker<Dictionary<string, Category>>(new CategoryParser());
        }

        [Fact]
        public async Task DoWork_WithoutParams_NotBeNullResult()
        {
            var result = await _parseWorker.DoWork();

            result.Should().NotBeNull();
            result.Should().HaveCountGreaterThan(0);
        }
    }
}
