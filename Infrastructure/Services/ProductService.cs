using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using web_api_base.Models.dbebay;

public interface IProductService
{
   
    Task<HTTPResponseClient<IEnumerable<dynamic>>> GetAllProductListing();
}

public class ProductService(IUnitOfWork _unitOfWork) : IProductService
{
    public async Task<HTTPResponseClient<IEnumerable<dynamic>>> GetAllProductListing()
    {
        
        var res = await _unitOfWork._listingRepository.GetAllAsync();
        HTTPResponseClient<IEnumerable<dynamic>> data = new HTTPResponseClient<IEnumerable<dynamic>>()
        {
            StatusCode = 200,
            Data = res.Skip(0).Take(20).Select(n => new{
                Id = n.Id,
                Title = n.Title,
                Description = n.Description,
                StartingPrice = n.StartingPrice,
                Seller = n.Seller.Username,
            }).ToList(),
            DateTime = DateTime.Now,
            Message = "Success"
        };
        return data;
    }
    
}