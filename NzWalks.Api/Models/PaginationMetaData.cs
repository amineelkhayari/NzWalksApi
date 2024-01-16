namespace NzWalks.Api.Models
{
    public class PaginationMetaData
    {
        public PaginationMetaData(int totalCount, int currentPage,  int itemPerPage)
        {
            TotalCount = totalCount;
            CurrentPage = currentPage;
            TotalPages = (int) Math.Ceiling(totalCount / (double) itemPerPage);


            
        }
        public int CurrentPage { get; private set; }
        public int TotalCount { get; private set; }
        public int TotalPages { get; private set; }
        public bool HasPrev => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;



    }
}
