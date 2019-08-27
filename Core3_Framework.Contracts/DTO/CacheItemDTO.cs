using System.ComponentModel.DataAnnotations;

namespace Core3_Framework.Contracts.DTO
{
    public class CacheItemDTO
    {
        [Editable(false)]
        [Key]
        public int Id { get; set; }

        [MaxLength(50)]
        public string Ad { get; set; }
    }
}
