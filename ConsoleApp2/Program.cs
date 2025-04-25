using System.Drawing;
using System.Drawing.Imaging;
using QRCoder;
using static QRCoder.PayloadGenerator;

namespace ConsoleApp2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Url generator = new Url("https://photos.app.goo.gl/3FL1eq1UZCEKj2TZ9");
            string payload = generator.ToString();
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(payload, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(100,
                System.Drawing.ColorTranslator.FromHtml("#CFB53B"),
                Color.Transparent, (Bitmap)Bitmap.FromFile("LogoBoda.png"), 20, 10);
            qrCodeImage.Save("test2.png", ImageFormat.Png);
        }
    }
}