// thanks to Ruiwei Bu/darkfall
// https://gist.github.com/darkfall/1656050

using System.Drawing;
using System.IO;

namespace PngToIcon
{
    public static class PngToIcon
    {
        /// <summary>
        /// convert .png to .ico
        /// </summary>
        /// <param name="input"></param>
        /// <param name="output"></param>
        /// <returns></returns>
        public static bool Convert(Stream input, Stream output)
        {
            int size = 32;
            Bitmap input_bit = (System.Drawing.Bitmap)System.Drawing.Bitmap.FromStream(input);
            if (input_bit != null)
            {
                int width, height;
                width = size;
                height = input_bit.Height / input_bit.Width * size;

                System.Drawing.Bitmap image_bit = new System.Drawing.Bitmap(input_bit, new System.Drawing.Size(width, height));
                if (image_bit != null)
                {
                    // save png to a memory stream
                    System.IO.MemoryStream memory_stream = new System.IO.MemoryStream();
                    image_bit.Save(memory_stream, System.Drawing.Imaging.ImageFormat.Png);

                    System.IO.BinaryWriter icon_binary = new System.IO.BinaryWriter(output);
                    if (output != null && icon_binary != null)
                    {
                        icon_binary.Write((byte)0);
                        icon_binary.Write((byte)0);
                        icon_binary.Write((short)1);
                        icon_binary.Write((short)1);
                        icon_binary.Write((byte)width);
                        icon_binary.Write((byte)height);
                        icon_binary.Write((byte)0);
                        icon_binary.Write((byte)0);
                        icon_binary.Write((short)0);
                        icon_binary.Write((short)32);
                        icon_binary.Write((int)memory_stream.Length);
                        icon_binary.Write((int)(6 + 16));
                        // write memory stream
                        icon_binary.Write(memory_stream.ToArray());
                        // flush and icon_binary will equal null
                        icon_binary.Flush();
                        return true;
                    }
                }
                return false;
            }
            return false;
        }
        /// <summary>
        /// run convert, close streams
        /// </summary>
        /// <param name="input"></param>
        /// <param name="output"></param>
        /// <returns></returns>
        public static bool Convert(string input, string output)
        {
            System.IO.FileStream input_file_stream = new System.IO.FileStream(input, System.IO.FileMode.Open);
            System.IO.FileStream output_file_stream = new System.IO.FileStream(output, System.IO.FileMode.OpenOrCreate);
            bool is_converted = Convert(input_file_stream, output_file_stream);
            input_file_stream.Close();
            output_file_stream.Close();
            return is_converted;
        }
    }
}
