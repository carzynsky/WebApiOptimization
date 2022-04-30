using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiOptimization.Application.Filter
{
    public class PaginationFilter
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public PaginationFilter()
        {
       
        }

        public PaginationFilter(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber <= 1 ? 1 : pageNumber;
            if(pageSize < 1)
            {
                PageSize = 1;
            }
            else if(pageSize > 25)
            {
                PageSize = 25;
            }
            else
            {
                PageSize = pageSize;
            }
        }
    }
}
