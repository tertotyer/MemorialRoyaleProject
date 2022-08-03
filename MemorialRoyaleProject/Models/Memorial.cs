using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace MemorialRoyaleProject.Models
{
    public class Memorial
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
        public string ImagePath { get; set; }

    }
}
