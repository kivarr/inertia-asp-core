using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InertiaCore.Data.Models
{
    public class Donation
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        
        [Required]
        [Column("name")]
        public string Name { get; set; }
        
        [Required]
        [Column("email")]
        public string Email { get; set; }

        [Required] 
        [Column("need_receipt")] 
        public bool NeedReceipt { get; set; }
    }
}