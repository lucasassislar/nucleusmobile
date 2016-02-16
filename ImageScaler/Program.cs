using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ImageScaler
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Input images folder path:");
            string path = Console.ReadLine();

            Console.WriteLine();
            Console.WriteLine("Input image average size in centimeters:");
            string s = Console.ReadLine();
            double size = double.Parse(s);

            Console.WriteLine();
            if (!Path.IsPathRooted(path))
            {
                path = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), path);
                Console.WriteLine("Using path: " + path);
            }

            // Android DPIs: 160, 320, 480, 640
            //         PPCM:  60, 120, 190, 250 

            string[] images = Directory.GetFiles(path);
            for (int i = 0; i < images.Length; i++)
            {
                string img = images[i];
                ProcessImage(img, size);
            }

            Console.WriteLine();
            Console.WriteLine("Scaled " + images.Length + " images");

            Console.WriteLine();
            Console.ReadLine();
        }

        private static double[] PPCM = new double[]{
            60, 120, 190, 250
        };

        private static string[] PPCM_Name = new string[]{
            "low", "med", "high", "ultra"
        };

        private static void ProcessImage(string path, double size)
        {
            string noExt = Path.Combine(Path.GetDirectoryName(path), Path.GetFileNameWithoutExtension(path));

            using (Bitmap bitmap = (Bitmap)Image.FromFile(path))
            {
                double maxPpcm = bitmap.Width / size;

                for (int i = 0; i < PPCM.Length; i++)
                {
                    double val = PPCM[i];
                    double factor = val / maxPpcm;
                    if (factor > 1)
                    {
                        Console.WriteLine("Upscaling of " + factor + " at PPCM " + val);
                    }
                    else
                    {
                        Console.WriteLine("Image with factor " + factor);
                    }

                    using (Bitmap sca = new Bitmap((int)(bitmap.Width * factor), (int)(bitmap.Height * factor)))
                    {
                        using (Graphics g = Graphics.FromImage(sca))
                        {
                            g.DrawImage(bitmap, new Rectangle(0, 0, sca.Width, sca.Height));
                        }
                        string p = noExt + "-" + PPCM_Name[i] + ".png";
                        if (File.Exists(p))
                        {
                            File.Delete(p);
                        }
                        sca.Save(p);
                    }
                }
            }
        }

    }
}
