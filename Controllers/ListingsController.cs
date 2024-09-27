using Microsoft.AspNetCore.Mvc;
using RealEstateListingApi.Data;
using RealEstateListingApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace RealEstateListingApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ListingsController : ControllerBase
    {
        //private readonly ApplicationDbContext _context;
        private readonly IListingsService _listingsService;
        public ListingsController(IListingsService listingsService)  //(ApplicationDbContext context)
        {
            // _context = context;
            _listingsService = listingsService;
        }

        // Tag this operation as "Listings Retrieval"
        [HttpGet]
        [Tags("Listings Retrieval")]
        public ActionResult<IEnumerable<Listing>> GetAllListings()
        {
            return _listingsService.GetAll().ToList(); // _context.Listings.ToList();
        }

        // Tag this operation as "Listings Management"
        [HttpPost]
        [Tags("Listings Management")]
        public async Task<ActionResult<Listing>> AddListing([FromBody] Listing listing)
        {
            // _context.Listings.Add(listing);
            // _context.SaveChanges();

            if(ModelState.IsValid){
                var result = await _listingsService.Insert(listing);
                if(result.Success){
                    return CreatedAtAction(nameof(GetListingById), new { id = result.Data.Id }, listing);
                }
                return Ok(result);
            }
            return Ok(ModelState);
        }

        // Tag this operation as "Listings Retrieval"
        [HttpGet("{id}")]
        [Tags("Listings Retrieval")]
        public async Task<ActionResult<Listing>> GetListingById(string id)
        {
            var listing = await _listingsService.GetById(id); //_context.Listings.FirstOrDefault(l => l.Id == id);
            if (listing == null)
            {
                return NotFound();
            }
            return listing;
        }

        [HttpDelete]
        [Tags("Remove Listing")]
        public async Task<ActionResult> Delete(string id){

            var listing = await _listingsService.GetById(id); //_context.Listings.FirstOrDefault(l => l.Id == id);
            if (listing == null)
            {
                return NotFound();
            }
            var result = await _listingsService.Delete(listing);
            return Ok(result);
        }
    }
}
