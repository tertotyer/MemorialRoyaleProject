using System.ComponentModel.DataAnnotations.Schema;

namespace MemorialRoyaleProject.Models
{
    public class News
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [Column(TypeName = "text")]
        public string ShortDescription { get; set; }

        [Column(TypeName = "text")]
        public string Content { get; set; }
    }
}
