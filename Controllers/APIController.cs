using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class APIController : Controller
    {
        private readonly WebService _web;

        public APIController(WebService web)
        {
            _web = web;
        }

        [HttpGet()]
        [Route("[action]")]
        public async Task<ActionResult> GetAllData()
        {
            // Return Bad Request if View Model is not valid
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var albumResults = await _web.GetAlbums();
            var photoResults = await _web.GetAllPhotos();

            var results = new { albumResults, photoResults };

            return Ok(results);
        }

        [HttpGet()]
        [Route("[action]/{userId}")]
        public async Task<ActionResult> GetData([FromRoute] int userId)
        {
            // Return Bad Request if View Model is not valid
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var albumResults = await _web.GetAlbums(userId);

            var photoResults = _web.GetAllPhotos().Result.Where(x => albumResults.Select(y => y.Id).ToList().Contains(x.Id)).ToList();

            ////Slower to fetch only the required data based on a loop.
            //var photoList = new List<Photo>();

            //foreach (var res in albumResults)
            //{
            //    var photoResult = await _web.GetPhoto(res.Id);

            //    if (photoResult != null)
            //    {
            //        photoList.AddRange(photoResult);
            //    }
            //}

            var results = new { albumResults, photoResults, /*photoList*/ };

            return Ok(results);
        }
    }
}
