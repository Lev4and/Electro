using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Electro.Model.Database.AuxiliaryTypes
{
    public class ProductsFilters
    {
        public int NumberPage { get; set; }

        public int ItemsPerPage { get; set; }

        public string SearchString { get; set; }

        public Guid? CategoryId { get; set; }

        public Guid? ManufacturerId { get; set; }

        public SortingOption SortingOption { get; set; }
    }
}
