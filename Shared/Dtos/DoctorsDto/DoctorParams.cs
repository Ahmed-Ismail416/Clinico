using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos.DoctorsDto
{
    public class DoctorParams
    {
        private const int DefaultPageSize = 5;
        private const int MaxPageSize = 10;

        public int PageIndex { get; set; } = 1;

        // Search by doctor name
        public string? Search { get; set; } = default!;

        // Filter by specialty
        public string? Specialty { get; set; }

        // Sorting (ex: "name", "name_desc", "fee", "fee_desc")
        public DoctorSortingOptions? SortBy { get; set; } 

        private int _pageSize = DefaultPageSize;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value > MaxPageSize ? MaxPageSize : value;
        }
    }
}
