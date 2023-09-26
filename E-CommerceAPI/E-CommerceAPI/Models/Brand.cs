using System.ComponentModel.DataAnnotations;

namespace E_CommerceAPI.Models
{
    public class Brand
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
