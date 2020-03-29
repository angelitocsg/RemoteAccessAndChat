using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.IO.Compression;

namespace Server
{
    [Serializable()]
    public class ImageWrapper
    {
        private byte[] _Bytes;

        public byte[] Bytes
        {
            get { return _Bytes; }
        }

        private Image _Image;

        private Bitmap Quantificar(Bitmap oBitmap, int iQuant)
        {
            Bitmap oNewBitmap = null;

            try
            {
                if (iQuant > 256)
                    iQuant = 256;
                else if (iQuant <= 0)
                    iQuant = 1;

                int dist = 256 / iQuant;

                int h = oBitmap.Width;
                int v = oBitmap.Height;
                int novo_valorR = 0;
                int novo_valorG = 0;
                int novo_valorB = 0;
                int faixaR = 0;
                int faixaG = 0;
                int faixaB = 0;
                oNewBitmap = new Bitmap(oBitmap.Width, oBitmap.Height);
                int i, u;

                for (i = 0; i < v; i++)
                {
                    for (u = 0; u < h; u++)
                    {
                        faixaR = oBitmap.GetPixel(u, i).R / dist;
                        faixaG = oBitmap.GetPixel(u, i).G / dist;
                        faixaB = oBitmap.GetPixel(u, i).B / dist;
                        int trasn = oBitmap.GetPixel(u, i).A;

                        novo_valorR = faixaR * dist + (dist / 2);
                        novo_valorG = faixaG * dist + (dist / 2);
                        novo_valorB = faixaB * dist + (dist / 2);

                        if (novo_valorR > 255)
                            novo_valorR = 255;

                        if (novo_valorG > 255)
                            novo_valorG = 255;

                        if (novo_valorB > 255)
                            novo_valorB = 255;

                        oNewBitmap.SetPixel(u, i, Color.FromArgb(trasn, novo_valorR, novo_valorG, novo_valorB));
                    }
                }
            }
            catch { }

            return oNewBitmap;
        }

        public ImageWrapper(byte[] bBytes)
        {
            _Bytes = bBytes;
        }

        public ImageWrapper(Image oImage, Int64 oQuality, bool bGrayScale)
        {
            EncoderParameters oEncoderParameters;
            System.Drawing.Imaging.Encoder oEncoder1;
            System.Drawing.Imaging.Encoder oEncoder2;
            ImageCodecInfo oImageCodecInfo;

            if (bGrayScale) { oImage = MakeGrayscale3(oImage); }

            oEncoder1 = System.Drawing.Imaging.Encoder.Quality;
            oEncoder2 = System.Drawing.Imaging.Encoder.ColorDepth;

            oEncoderParameters = new EncoderParameters(2);
            oEncoderParameters.Param[0] = new EncoderParameter(oEncoder1, oQuality);
            oEncoderParameters.Param[1] = new EncoderParameter(oEncoder2, 8L);

            oImageCodecInfo = GetEncoderInfo("image/jpeg");

            if (oImage == null)
            {
                throw new ArgumentNullException("oImage");
            }
            using (MemoryStream oMemoryStream = new MemoryStream())
            {
                oImage.Save(oMemoryStream, oImageCodecInfo, oEncoderParameters);
                _Bytes = oMemoryStream.ToArray();
                _Bytes = CompressFrame(oMemoryStream);
            }
        }

        private byte[] CompressFrame(MemoryStream oMemoryStreamIn)
        {
            var oMemoryStreamOut = new System.IO.MemoryStream();

            using (var tinyStream = new GZipStream(oMemoryStreamOut, CompressionMode.Compress))
            {
                oMemoryStreamIn.CopyTo(tinyStream);
            }

            byte[] bBytes = oMemoryStreamOut.ToArray();

            return bBytes;
        }

        private MemoryStream DecompressFrame(byte[] bBytes)
        {
            //Decompress                
            var oMemoryStream = new GZipStream(new MemoryStream(bBytes), CompressionMode.Decompress);
            var oMemoryStreamOut = new System.IO.MemoryStream();
            oMemoryStream.CopyTo(oMemoryStreamOut);

            return oMemoryStreamOut;
        }


        private static ImageCodecInfo GetEncoderInfo(String mimeType)
        {
            int j;
            ImageCodecInfo[] encoders;
            encoders = ImageCodecInfo.GetImageEncoders();
            for (j = 0; j < encoders.Length; ++j)
            {
                if (encoders[j].MimeType == mimeType)
                    return encoders[j];
            }
            return null;
        }

        public Image ToImage()
        {
            _Image = null;
            _Image = Image.FromStream(new MemoryStream(_Bytes));

            return _Image;
        }

        private static Image MakeGrayscale3(Image original)
        {
            //create a blank bitmap the same size as original
            Bitmap newBitmap = new Bitmap(original.Width, original.Height);

            //get a graphics object from the new image
            Graphics g = Graphics.FromImage(newBitmap);

            //create the grayscale ColorMatrix
            ColorMatrix colorMatrix = new ColorMatrix(
               new float[][] 
                  {
                     new float[] {.3f, .3f, .3f, 0, 0},
                     new float[] {.59f, .59f, .59f, 0, 0},
                     new float[] {.11f, .11f, .11f, 0, 0},
                     new float[] {0, 0, 0, 1, 0},
                     new float[] {0, 0, 0, 0, 1}
                  });

            //create some image attributes
            ImageAttributes attributes = new ImageAttributes();

            //set the color matrix attribute
            attributes.SetColorMatrix(colorMatrix);

            //draw the original image on the new image
            //using the grayscale color matrix
            g.DrawImage(original, new Rectangle(0, 0, original.Width, original.Height),
               0, 0, original.Width, original.Height, GraphicsUnit.Pixel, attributes);

            //dispose the Graphics object
            g.Dispose();

            return newBitmap;
        }
    }
}