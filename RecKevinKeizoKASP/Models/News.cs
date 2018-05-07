using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Web;

namespace RecKevinKeizoKASP.Models
{
    public class News
    {
       
        [Key]
        public int NewsId { get; set; }
        [Required]
        [Display(Name = "Título")]
        public string Title { get; set; }
        
        [Required]
        public int CategoryId { get; set; }    
        [JsonIgnore]
        public virtual Category Category { get; set; }
        [Required]
        public string PostDate { get; set; }

        [Display(Name = "Imagem")]
        [DataType(DataType.ImageUrl)]
        public string Photo { get; set; }
        [NotMapped]
        [DataType(DataType.ImageUrl)]
        [Display(Name = "Imagem")]
        public HttpPostedFileBase LogoFile { get; set; }

        [Display(Name = "Texto")]
        [Required]
        public string Text { get; set; }
        [Display(Name = "Resumo Texto")]
        [MaxLength(100, ErrorMessage = "O Resumo não pode ter mais de 100 caracteres.")]
        [Required]
        public string ResumeText { get; set; }
        [Display(Name = "Autor")]
        [Required]
        public string Author { get; set; }

    }
}