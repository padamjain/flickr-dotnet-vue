using Microsoft.AspNetCore.Mvc;
using planday.Services.ImagesService;

namespace planday.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class ImageController : ControllerBase
  {
    private readonly ImageProviderService ImageProvider;

    public ImageController(ImageProviderService AImageService)
    {
            ImageProvider = AImageService;
    }

    [HttpGet]
    public IActionResult Get(string tags = "")
    {
      return Ok(ImageProvider.Search(tags).Result);
    }

    [HttpPost]
    public IActionResult Post(string tags = "")
    {
      return Ok(ImageProvider.Search(tags).Result);
    }

  }
}
