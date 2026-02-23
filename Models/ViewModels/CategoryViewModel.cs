using System.ComponentModel.DataAnnotations;

namespace YumBlazor.Models.ViewModels
{
    public class CategoryViewModel
    {
        [Required(ErrorMessage = "Please enter a name.")]
        public string Name { get; set; }
    }
}
