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

        static void RunCartoon()
        {
            var src = new Mat("img1.jpeg");
            var gray = new Mat();
            var blur = new Mat();
            var edges = new Mat();
            var dst = new Mat();

            Cv2.CvtColor(src, gray, ColorConversionCodes.BGR2GRAY);
            Cv2.GaussianBlur(gray, blur, new Size(5,5), 2, 2);
            Cv2.AdaptiveThreshold(blur, edges, 255, AdaptiveThresholdTypes.MeanC, ThresholdTypes.Binary, 5, 5);
            Cv2.BitwiseAnd(src, src, dst, edges);

            using (new Window("src image", src)) 
            using (new Window("dst image", dst)) 
            {
                Cv2.WaitKey();
            }
        }

        static void RunFaceDetection()
        {
            var finder = new CascadeClassifier("haarcascade_frontalface_alt.xml");

            var src = new Mat("img1.jpeg");
            var gray = new Mat();
            var dst = new Mat();

            Cv2.CvtColor(src, gray, ColorConversionCodes.BGR2GRAY);
            var faces = finder.DetectMultiScale(gray, 1.08, 2, HaarDetectionTypes.ScaleImage, new Size(30, 30));

            src.CopyTo(dst);
            foreach(var rect in faces)
            {
                Cv2.Rectangle(dst, rect, new Scalar(255, 0, 0), 3);
            }
            
            using (new Window("src image", src)) 
            using (new Window("dst image", dst)) 
            {
                Cv2.WaitKey();
            }
        }

        static void RunCameraFaceDetection()
        {
            var capture = new VideoCapture(0);
            var finder = new CascadeClassifier("haarcascade_frontalface_alt.xml");

            var src = new Mat(capture.FrameWidth, capture.FrameHeight, MatType.CV_8UC4);
            var dst = new Mat(capture.FrameWidth, capture.FrameHeight, MatType.CV_8UC4);
            var gray = new Mat();

            var window = new Window("Camera");

            while(true)
            {
                capture.Read(src);
                //...
                src.CopyTo(dst);
                //...
                window.ShowImage(dst);
            }
        }

        static void Main(string[] args)
        {
            try
            {
                //RunBlur();
                //RunCartoon();
                //RunFaceDetection();
                RunCameraFaceDetection();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.ReadLine();
            }
        }        
    }
}
