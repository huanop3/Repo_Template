using System;
using System.Collections.Generic;

namespace web_api_base.Service_FE.ViewModel
{

    public partial class ListingVM
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
        
        public decimal StartingPrice { get; set; }

        public string? Seller { get; set; }

       
    }
   
}