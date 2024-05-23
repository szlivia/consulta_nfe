using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class NfeController : ControllerBase
{
    private readonly NfeConsultaService _nfeConsultaService;

    public NfeController(NfeConsultaService nfeConsultaService)
    {
        _nfeConsultaService = nfeConsultaService;
    }

    [HttpGet("consultar/{chaveNfe}")]
    public async Task<IActionResult> ConsultarNfe(string chaveNfe)
    {
        var certificado = CertificateHelper.GetCertificate("caminho/do/certificado.pfx", "senha_do_certificado");
        var resultado = await _nfeConsultaService.ConsultarNfeAsync(chaveNfe, certificado);
        return Ok(resultado);
    }
}
