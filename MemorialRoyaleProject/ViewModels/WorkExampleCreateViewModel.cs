using Microsoft.AspNetCore.Http;

namespace MemorialRoyaleProject.ViewModels
{
    public class WorkExampleCreateViewModel
    {
        public int Id { get; set; }
        public IFormFile Image { get; set; }
    }
}
