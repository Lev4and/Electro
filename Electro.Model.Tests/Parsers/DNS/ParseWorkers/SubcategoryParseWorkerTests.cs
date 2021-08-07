using Electro.Model.Database;
using Electro.Model.Database.Entities;
using Electro.Model.Database.Repositories.EntityFramework;
using Electro.Model.Parsers.DNS;
using Electro.Model.Parsers.DNS.ParseWorkers;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Electro.Model.Tests.Parsers.DNS.ParseWorkers
{
    public class SubcategoryParseWorkerTests
    {
        private readonly CategoryParseWorker<Dictionary<string, Category>> _categoryParseWorker;
        private readonly SubcategoryParseWorker<Dictionary<string, Category>> _subcategoryParseWorker;

        public SubcategoryParseWorkerTests()
        {
            _categoryParseWorker = new CategoryParseWorker<Dictionary<string, Category>>(new CategoryParser());
            _subcategoryParseWorker = new SubcategoryParseWorker<Dictionary<string, Category>>(new SubcategoryParser());
        }

        private async Task<Dictionary<string, Category>> DoWorkAsync(Dictionary<string, Category> childrens)
        {
            if(childrens.Count > 0)
            {
                foreach(var children in childrens)
                {
                    var dictionary = await _subcategoryParseWorker.DoWork(children.Key.Substring(1));

                    dictionary = dictionary ?? new Dictionary<string, Category>();

                    children.Value.Children = (await DoWorkAsync(dictionary)).Values;
                }
            }

            return childrens;
        }

        [Fact]
        public async Task DoWork_WithLinkParam_NotBeNullResult()
        {
            var categories = await _categoryParseWorker.DoWork();

            foreach(var category in categories)
            {
                category.Value.Children = (await DoWorkAsync((await _subcategoryParseWorker.DoWork(category.Key
                    .Substring(1))))).Values;
            }

            var textJson = JsonConvert.SerializeObject(categories.Values);
            var fileName = $"Категории.json";

            File.WriteAllText($@"{fileName}", textJson);

            categories.Should().NotBeNull();
            categories.Should().HaveCountGreaterThan(0);
        }
    }
}
