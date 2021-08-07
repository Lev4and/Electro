using Electro.Model.Database.Entities;
using Electro.Model.Parsers.DNS;
using Electro.Model.Parsers.DNS.ParseWorkers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Electro.Model.Tests.Parsers.DNS.ParseWorkers
{
    public class ProductParseWorkerTests
    {
        private readonly ProductParseWorker<Product> _productParseWorker;
        private readonly CSRFTokenParseWorker<string> _cSRFTokenParseWorker;
        private readonly ProductsParseWorker<List<string>> _productsParseWorker;
        private readonly PaginationParseWorker<Pagination> _paginationParseWorker;
        private readonly CategoryParseWorker<Dictionary<string, Category>> _categoryParseWorker;
        private readonly SubcategoryParseWorker<Dictionary<string, Category>> _subcategoryParseWorker;

        public List<string> ProductLinks { get; set; }

        public ProductParseWorkerTests()
        {
            _productParseWorker = new ProductParseWorker<Product>(new ProductParser());
            _cSRFTokenParseWorker = new CSRFTokenParseWorker<string>(new CSRFTokenParser());
            _productsParseWorker = new ProductsParseWorker<List<string>>(new ProductsParser());
            _paginationParseWorker = new PaginationParseWorker<Pagination>(new PaginationParser());
            _categoryParseWorker = new CategoryParseWorker<Dictionary<string, Category>>(new CategoryParser());
            _subcategoryParseWorker = new SubcategoryParseWorker<Dictionary<string, Category>>(new SubcategoryParser());

            ProductLinks = new List<string>();
        }

        private async Task GetSubcategoriesAsync(List<string> links)
        {
            foreach (var link in links)
            {
                var subcategoriesDictionary = await _subcategoryParseWorker.DoWork(link.Substring(1));
                var subcategoriesLinks = subcategoriesDictionary != null ? new List<string>(subcategoriesDictionary.Keys) :
                    new List<string>();

                if (subcategoriesLinks.Count > 0)
                {
                    await GetSubcategoriesAsync(subcategoriesLinks);
                }
                else
                {
                    await GetPaginationAsync(link.Substring(1));
                }

                //try
                //{
                //    var subcategoriesDictionary = await _subcategoryParseWorker.DoWork(link.Substring(1));
                //    var subcategoriesLinks = subcategoriesDictionary != null ? new List<string>(subcategoriesDictionary.Keys) : 
                //        new List<string>();

                //    if (subcategoriesLinks.Count > 0)
                //    {
                //        await GetSubcategoriesAsync(subcategoriesLinks);
                //    }
                //    else
                //    {
                //        await GetPaginationAsync(link.Substring(1));
                //    }
                //}
                //catch
                //{
                //    continue;
                //}
            }
        }

        private async Task GetPaginationAsync(string link)
        {
            var listProductLinks = new List<string>();

            var pagination = await _paginationParseWorker.DoWork(link);

            for (int i = 1; i <= pagination.CountPages; i++)
            {
                var token = await _cSRFTokenParseWorker.DoWork(link, i);
                var links = (pagination.CountPages > 1 ? await _productsParseWorker.DoWork(link, i, token) :
                    await _productsParseWorker.DoWork(link, i));

                listProductLinks.AddRange(links);

                //Thread.Sleep(750);
            }

            ProductLinks.AddRange(listProductLinks);
        }

        private async Task GetAndSaveProducts(List<string> productLinks)
        {
            foreach (var productLink in productLinks)
            {
                var product = await _productParseWorker.DoWork(productLink);
                var productJson = JsonConvert.SerializeObject(product);

                File.WriteAllText($@"Товары/{Guid.NewGuid()}.json", productJson);

                Thread.Sleep(2000);
            }
        }

        [Fact]
        public async Task DoWork_WithLinkParam_NotBeNullResult()
        {
            //var categories = await _categoryParseWorker.DoWork();

            //foreach (var category in categories)
            //{
            //    var subcategoriesLinks = new List<string>((await _subcategoryParseWorker.DoWork(category.Key.Substring(1))).Keys);

            //    await GetSubcategoriesAsync(subcategoriesLinks);
            //}

            //File.WriteAllLines($@"Товары.txt", ProductLinks);

            ProductLinks = File.ReadAllLines($@"Товары.txt").ToList();

            var saveThreads = new List<Thread>();

            GetAndSaveProducts(ProductLinks.ToList()).Wait();

            for (int i = 0; i <= 10; i++)
            {
                var thread = new Thread(delegate ()
                {
                    GetAndSaveProducts(ProductLinks
                        .Skip(ProductLinks.Count / 10 * i)
                        .Take(ProductLinks.Count / 10)
                        .ToList()).Wait();
                });

                saveThreads.Add(thread);
                thread.Priority = ThreadPriority.Highest;
                thread.Start();
            }

            while (true)
            {
                if (saveThreads.All(saveThread => saveThread.ThreadState == ThreadState.Stopped))
                {
                    break;
                }

                await Task.Delay(1000);
            }

            //await GetAndSaveProducts(ProductLinks);

            //var product = await _productParseWorker.DoWork("product/f699d687bea12ff1/karta-oplaty-dostupa-my-book-premium-6-mesacev/");

            //product.Should().NotBeNull();
        }
    }
}
