using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using islProcedure.Models;

namespace islProcedure.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public ValuesController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> CreateKayit([FromBody] CreateRequestDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var connectionString = _configuration.GetConnectionString("LogoDb");

            try
            {
                using (var conn = new SqlConnection(connectionString))
                using (var cmd = new SqlCommand("dbo.usp_TBLCASABIT_0212_Insert_Test_v3", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@SUBE_KODU", SqlDbType.SmallInt).Value = dto.SubeKodu;
                    cmd.Parameters.Add("@ISLETME_KODU", SqlDbType.SmallInt).Value = dto.IsletmeKodu;
                    cmd.Parameters.Add("@CARI_KOD", SqlDbType.NVarChar, 15).Value = dto.CariKod;
                    cmd.Parameters.Add("@CARI_TEL", SqlDbType.NVarChar, 20).Value = (object?)dto.CariTel ?? DBNull.Value;
                    cmd.Parameters.Add("@CARI_IL", SqlDbType.NVarChar, 50).Value = (object?)dto.CariIl ?? DBNull.Value;
                    cmd.Parameters.Add("@ULKE_KODU", SqlDbType.NVarChar, 4).Value = (object?)dto.UlkeKodu ?? DBNull.Value;
                    cmd.Parameters.Add("@CARI_ISIM", SqlDbType.NVarChar, 100).Value = dto.CariIsim;
                    cmd.Parameters.Add("@CARI_TIP", SqlDbType.NVarChar, 1).Value = dto.CariTip;
                    cmd.Parameters.Add("@CARI_ADRES", SqlDbType.NVarChar, 255).Value = (object?)dto.CariAdres ?? DBNull.Value;
                    cmd.Parameters.Add("@DETAY_KODU", SqlDbType.SmallInt).Value = dto.DetayKodu;
                    cmd.Parameters.Add("@KAYITYAPANKUL", SqlDbType.NVarChar, 12).Value = dto.KayitYapanKul;

                    await conn.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                }

                return Ok(new { message = "Kayıt başarıyla eklendi." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Kayıt sırasında bir hata oluştu.",
                    detail = ex.Message
                });
            }
        }
    }
}
