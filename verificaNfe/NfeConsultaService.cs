using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

public class NfeConsultaService
{
    private static readonly string _url = "https://nfe.fazenda.sp.gov.br/ws/NfeConsulta2.asmx";

    public async Task<string> ConsultarNfeAsync(string chaveNfe, X509Certificate2 certificado)
    {
        var requestXml = GerarXmlConsulta(chaveNfe);

        using (var handler = new HttpClientHandler())
        {
            handler.ClientCertificates.Add(certificado);

            using (var client = new HttpClient(handler))
            {
                var content = new StringContent(requestXml, Encoding.UTF8, "text/xml");
                content.Headers.ContentType = new MediaTypeHeaderValue("text/xml");

                var response = await client.PostAsync(_url, content);
                response.EnsureSuccessStatusCode();

                var responseXml = await response.Content.ReadAsStringAsync();
                return responseXml;
            }
        }
    }

    private string GerarXmlConsulta(string chaveNfe)
    {
        var xmlDoc = new XmlDocument();
        var xmlRoot = xmlDoc.CreateElement("consSitNFe");
        xmlRoot.SetAttribute("xmlns", "http://www.portalfiscal.inf.br/nfe");
        xmlRoot.SetAttribute("versao", "4.00");

        var chaveElement = xmlDoc.CreateElement("chNFe");
        chaveElement.InnerText = chaveNfe;
        xmlRoot.AppendChild(chaveElement);

        xmlDoc.AppendChild(xmlRoot);
        return xmlDoc.OuterXml;
    }
}
