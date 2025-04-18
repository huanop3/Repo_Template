using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using web_api_base.Models.dbebay;
//using dotnet_v8_blazor-master.Models;

namespace web_api_base.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(IProductService _productService, EbayContext _context) : ControllerBase
    {
        [HttpGet("GetAllProductListing")]
        [OutputCache(Duration = 50)]
        public async Task<ActionResult> GetAllProductListing()
        {
            var result = await _productService.GetAllProductListing();
            return Ok(result);
        }
        [HttpPost("")]
        public ActionResult<IActionResult> PostTModel(Order model)
        {

            try
            {


                // _context.Database.BeginTransaction();
                // _context.Orders.Add(model); //Data có trong db chưa ?
                // _context.SaveChanges(); //Sinh ra order id 
                // //Chạy for trên items giỏ hàng để tạo chi tiểt đơn hàng
                // OrderDetail ordetail = new OrderDetail();
                // ordetail.OrderId = model.Id; // null hoăc 0
                // _context.OrderDetails.Add(ordetail);
                // _context.SaveChanges();
                // _context.Database.CommitTransaction();
                _context.Database.BeginTransaction();

                model.OrderDetails.Add(new OrderDetail()
                {
                    OrderId = model.Id
                });
                _context.Orders.Add(model);
                _context.SaveChanges();
                _context.Database.CommitTransaction();


            }
            catch (Exception ex)
            {
                //Ghi log

            }






            return null;
        }



    }
}