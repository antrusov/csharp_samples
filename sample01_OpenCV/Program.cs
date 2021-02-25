using System;
using OpenCvSharp;

namespace sample01_OpenCV
{
    class Program
    {
        static void RunBlur()
        {
            var src = new Mat("img1.jpeg");
            var dst = new Mat();

            Cv2.GaussianBlur(src, dst, new Size(15,15), 0);

            using (new Window("src image", src)) 
            using (new Window("dst image", dst)) 
            {
                Cv2.WaitKey();
            }
        }

        static void Main(string[] args)
        {
            RunBlur();
        }
    }
}
