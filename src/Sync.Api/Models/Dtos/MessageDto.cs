using System.ComponentModel.DataAnnotations;

namespace Sync.Api.Models
{
    public class MessageDto
    {
        [Required]
        public string? Content { get; set; }
    }
}