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
            // Request URL
            Console.WriteLine("Enter the URL to encode:");
            string? urlInput = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(urlInput))
            {
                Console.WriteLine("No URL entered. Exiting.");
                return;
            }
            Url generator = new Url(urlInput);
            string payload = generator.ToString();

            // Request color
            Console.WriteLine("Do you want to set a custom color? (y/n):");
            string colorChoice = Console.ReadLine() ?? "n";
            string colorHex = "#000000";
            if (colorChoice.Trim().ToLower() == "y")
            {
                Console.WriteLine("Enter the color hex code (e.g., #FF5733):");
                string? colorInput = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(colorInput))
                {
                    colorHex = colorInput;
                }
            }

            // Request image
            Console.WriteLine("Do you want to add a logo image? (y/n):");
            string imageChoice = Console.ReadLine() ?? "n";
            Bitmap? logoBitmap = null;
            bool noImage = true;
            if (imageChoice.Trim().ToLower() == "y")
            {
                noImage = false;
                Console.WriteLine("Enter the path to the image file:");
                string? imagePath = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(imagePath))
                {
                    try
                    {
                        logoBitmap = (Bitmap)Bitmap.FromFile(imagePath);
                    }
                    catch
                    {
                        Console.WriteLine("Could not load image. Proceeding without logo.");
                    }
                }
            }

            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(payload, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(100,
                System.Drawing.ColorTranslator.FromHtml(colorHex),
                Color.Transparent, logoBitmap, noImage ? 0: 20, noImage ? 0 : 20);
            qrCodeImage.Save("test2.png", ImageFormat.Png);
            Console.WriteLine("QR code saved as test2.png");
        }
    }
}