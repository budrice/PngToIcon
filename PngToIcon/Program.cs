using System;
using System.Drawing;
using System.IO;

namespace PngToIcon
{
    class Program
    {
        static void Main(string[] args)
        {
            string filepath = string.Empty;
            string png_filename = string.Empty;
            string ico_filename = string.Empty;

            Console.WriteLine("filepath: ");
            filepath = Console.ReadLine();
            Console.WriteLine("png filename: ");
            png_filename = Console.ReadLine();
            Console.WriteLine("icon filename: ");
            ico_filename = Console.ReadLine();
            FileStream input = File.OpenRead(filepath + "\\" + png_filename + ".png");

            using (FileStream stream = File.OpenWrite(filepath + "\\" + ico_filename + ".ico"))
            {
                PngToIcon.Convert(input, stream);
            }
        }
    }
}
