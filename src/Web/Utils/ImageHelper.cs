using System.IO;
using Microsoft.AspNetCore.Hosting;
using ImageMagick;

namespace EC_Website.Utils
{
    public static class ImageHelper
    {
        public static void ResizeToQuadratic(Stream imageStream, string outputFile, int xySize = 225)
        {
            using var image = new MagickImage(imageStream);
            if (image.Height > xySize || image.Width > xySize)
            {
                image.Resize(xySize, xySize);
                image.Strip();
            }

            image.Write(outputFile);
        }

        public static void ResizeToRectangle(Stream imageStream, string outputFile, int width = 850)
        {
            using var image = new MagickImage(imageStream);
            if (image.Width > width)
            {
                var proportion = 1 - ((image.Width - width) / image.Width);
                var resizingHeight = image.Height * proportion;
                image.Resize(width, resizingHeight);
                image.Strip();
            }

            image.Write(outputFile);
        }

        public static void RemoveImage(string imgPath, IWebHostEnvironment hostEnvironment)
        {
            var imgFileName = Path.GetFileName(imgPath);
            var imgFullPath = Path.Combine(hostEnvironment.WebRootPath, "db_files", "img", imgFileName);

            if (File.Exists(imgFullPath))
            {
                File.Delete(imgFullPath);
            }
        }
    }
}
