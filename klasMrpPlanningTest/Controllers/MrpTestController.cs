using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Globalization;
using klasMrpPlanningTest.Dtos.Mrp; // namespace'ini düzelt

namespace YourProject.Controllers;

[ApiController]
[Route("api/v1/test/mrp")]
public sealed class MrpTestController : ControllerBase
{
    private readonly IConfiguration _cfg;

    public MrpTestController(IConfiguration cfg)
    {
        _cfg = cfg;
    }

    // Swagger’dan çağır:
    // GET /api/v1/test/mrp/purchase-order-lines?startDate=2025-07-01&endDate=2025-07-31
    [HttpGet("purchase-order-lines")]
    public async Task<IActionResult> GetPurchaseOrderLines([FromQuery] string startDate, [FromQuery] string endDate)
    {
        if (!TryParseDate(startDate, out var sd) || !TryParseDate(endDate, out var ed))
            return BadRequest("startDate/endDate format must be YYYY-MM-DD");

        if (sd > ed)
            return BadRequest("startDate cannot be greater than endDate");

        var cs = _cfg.GetConnectionString("UzserDbConnection"); //db bağlantısı
        if (string.IsNullOrWhiteSpace(cs))
            return Problem("Connection string 'Default' not found in appsettings.json");

        const string sql = @"
SELECT
    FATIRS_NO   AS FatirsNo,
    CARI_ISIM   AS CariIsim,
    STOK_KODU   AS StokKodu,
    STOK_ADI    AS StokAdi,
    OLCU_BR1    AS OlcuBr1,
    STHAR_GCMIK AS StharGcmik,
    STHAR_BF    AS StharBf,
    DEPO_KODU   AS DepoKodu,
    TARIH       AS Tarih
FROM dbo.vw_MRP_SiparisKalemleri_Test 
WHERE TARIH >= @StartDate
    AND TARIH < DATEADD(DAY, 1, @EndDate)";

        await using var conn = new SqlConnection(cs);
        var rows = await conn.QueryAsync<MrpOrderLineDto>(sql, new
        {
            StartDate = sd,
            EndDate = ed
        });

        return Ok(rows); // test için direkt liste dönüyoruz
    }

    private static bool TryParseDate(string value, out DateTime date)
        => DateTime.TryParseExact(value, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out date);
}

