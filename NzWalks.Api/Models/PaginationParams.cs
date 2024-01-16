using Microsoft.Identity.Client;

namespace NzWalks.Api.Models
{
    public class PaginationParams
    {
      
           private int _maxPageSize = 10;
        public int page { get; set; } = 1;
        public int itemPerPage { get; set; } = 5;
       

    } 

        }
    

