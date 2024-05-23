using System.Security.Cryptography.X509Certificates;

public class CertificateHelper
{
    public static X509Certificate2 GetCertificate(string certificatePath, string certificatePassword)
    {
        return new X509Certificate2(certificatePath, certificatePassword);
    }
}