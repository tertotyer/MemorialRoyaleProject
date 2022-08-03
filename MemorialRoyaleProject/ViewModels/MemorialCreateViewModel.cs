using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace MemorialRoyaleProject.ViewModels
{
    public class MemorialCreateViewModel
    {
        public int Id { get; set; }
        [DisplayName("Модель")]
        public string Model { get; set; }
        [DisplayName("Размер стеллы")]
        public string Proportion { get; set; }
        [DisplayName("Гранит")]
        public string Granite { get; set; }
        [DisplayName("Комплект")]
        public string Nabor { get; set; }
        [DisplayName("Фото")]
        public IFormFile Image { get; set; }
    }
}
