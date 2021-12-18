using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoApi.Models
{
    /// <summary>
    /// TodoItem Model
    /// </summary>
    public class TodoItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Name cannot exceed more than 50 characters")]
        [RegularExpression(@"^[a-zA-Z0-9_ ]*$", ErrorMessage = "Name cannot have special character,numbers or space")]
        public string? Name { get; set; }

        public bool IsComplete { get; set; } = default;
    }
}