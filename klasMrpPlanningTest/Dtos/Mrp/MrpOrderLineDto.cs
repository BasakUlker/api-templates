using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace klasMrpPlanningTest.Dtos.Mrp // <-- burayı projene göre düzelt
{
    public sealed class MrpOrderLineDto
    {
        [Required]
        [StringLength(15)]
        [Column(TypeName = "varchar(15)")]
        public string FatirsNo { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        [Column(TypeName = "varchar(100)")]
        public string CariIsim { get; set; } = string.Empty;

        [Required]
        [StringLength(35)]
        [Column(TypeName = "varchar(35)")]
        public string StokKodu { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        [Column(TypeName = "varchar(100)")]
        public string StokAdi { get; set; } = string.Empty;

        [Required]
        [StringLength(2)]
        [Column(TypeName = "varchar(2)")]
        public string OlcuBr1 { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "decimal(28,8)")]
        public decimal StharGcmik { get; set; }

        [Required]
        [Column(TypeName = "decimal(28,8)")]
        public decimal StharBf { get; set; }

        [Required]
        [Column(TypeName = "smallint")]
        public short DepoKodu { get; set; }

        [Required]
        [Column(TypeName = "datetime")]
        public DateTime Tarih { get; set; }
    }
}

