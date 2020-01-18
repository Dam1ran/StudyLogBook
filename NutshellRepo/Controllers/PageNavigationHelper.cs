using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NutshellRepo.Controllers
{
    public class PageNavigationHelper
    {
        public readonly bool isLastPage;
        public readonly bool isFirstPage;
        public readonly int CurrentPage;
        public readonly int NumberOfPages;
        public readonly int PageSize;
        public readonly int PagesToSkip;

        public PageNavigationHelper(
            int aTotalNumberOfEntries,
            int aPageSize,
            int aCurrentPage,
            string aNavigateDirection
            )
        {
            //checks for wrong input data
            aTotalNumberOfEntries = aTotalNumberOfEntries < 0 ? 0 : aTotalNumberOfEntries;
            aPageSize = aPageSize < 1 ? 1 : aPageSize;
            aCurrentPage = aCurrentPage < 0 ? 0 : aCurrentPage;

            //assigns integer number of pages, dependent on total number of pages and page size
            NumberOfPages = aPageSize > 1 ? (int)Math.Ceiling((double)aTotalNumberOfEntries / aPageSize) : aTotalNumberOfEntries;

            //navigate through "pages" according to operation
            if (aNavigateDirection == "next")
            {
                if (aCurrentPage < NumberOfPages - 1)
                {
                    ++aCurrentPage;
                }
            }
            else if (aNavigateDirection == "prev")
            {
                if (aCurrentPage > 0)
                {
                    --aCurrentPage;
                }
            }
            else if (aNavigateDirection == "first")
            {
                aCurrentPage = 0;
            }
            else if (aNavigateDirection == "last")
            {
                aCurrentPage = NumberOfPages == 0 ? 0 : NumberOfPages - 1;
            }

            //assigning to fields
            CurrentPage = aCurrentPage;
            PageSize = aPageSize;
            PagesToSkip = CurrentPage * PageSize;
            isFirstPage = CurrentPage <= 0 ? true : false;
            isLastPage = CurrentPage+1 >= NumberOfPages ? true : false;

        }

    }

}
