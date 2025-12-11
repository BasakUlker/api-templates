using System.ComponentModel.DataAnnotations;

namespace islProcedure.Models
{
    public class CreateRequestDTO
    {
        [Required]
        public short SubeKodu { get; set; }

        [Required]
        public short IsletmeKodu { get; set; }

        [Required]
        [MaxLength(15)]
        public string? CariKod { get; set; } = string.Empty;

        [MaxLength(20)]
        public string? CariTel { get; set; }

        [MaxLength(50)]
        public string? CariIl { get; set; }

        [MaxLength(4)]
        public string? UlkeKodu { get; set; }

        [Required]
        [MaxLength(100)]
        public string? CariIsim { get; set; } = string.Empty;

        [Required]
        [MaxLength(1)]
        public string? CariTip { get; set; } = string.Empty;

        [MaxLength(255)]
        public string? CariAdres { get; set; }

        [Required]
        public short DetayKodu { get; set; }

        [Required]
        [MaxLength(12)]
        public string? KayitYapanKul { get; set; } = string.Empty;
    }
}
