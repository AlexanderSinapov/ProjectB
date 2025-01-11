using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace ProjectB.Models
{
    public class Lessons
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Title cannot exceed 50 characters")]
        public string Title { get; set; }

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        public string? Description { get; set; }

        public string? VideoUrl { get; set; }

        public string? DocumentUrl { get; set; }

        [StringLength(50000, ErrorMessage = "Text content cannot exceed 50000 characters")]
        public string? TextContent { get; set; }

        [AllowNull]
        public int UserId { get; set; }

        [ForeignKey("Id")]
        public virtual SignUp User { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
