
using System.Drawing;

namespace Jiftle
{
      public class Draw
      {
        /// <summary>
        /// 创建缩略图
        /// </summary>
        /// <param name="filename">文件名</param>
        /// <param name="width">宽</param>
        /// <param name="height">高</param>
        /// <returns>位图</returns>
        /// <remarks>eg.
        /// Bitmap bmp = CreateThumbPicture(@"D:\pic.jpg", 100, 100);
        /// bmp.Save(@"d:\picThumb.jpg", System.Drawing.Imaging.ImageFormat.jpg);
        /// 常用jpg格式,jpg的图片体积小 
        /// </remarks>
          public static Bitmap CreatePicture_Thumb(string filename, int width, int height)
        {
            try
            {
                int oldWidth = 0;
                int oldHeight = 0;
                int newWidth = width;
                int newHeight = height;
                Bitmap newImg = null;

                if (System.IO.File.Exists(filename))
                {
                    Bitmap oldImg = new Bitmap(filename);
                    oldWidth = oldImg.Width;
                    oldHeight = oldImg.Height;

                    newImg = new Bitmap(oldImg, newWidth, newHeight);

                    Graphics g = Graphics.FromImage(newImg);
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High; //高质量插值法
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality; //高质量，低速度呈现平滑程度
                    g.Clear(Color.Transparent);
                    g.DrawImage(oldImg, new Rectangle(0, 0, newWidth, newHeight), new Rectangle(0, 0, oldWidth, oldHeight), GraphicsUnit.Pixel);
                    g.Dispose();
                }

                return newImg;
            }
            catch (System.Exception)
            {
                return null;
            }
        }

          /// <summary>
          /// 在右小角添加文字水印
          /// </summary>
          /// <param name="filename">需要添加水印的图片</param>
          /// <param name="AddText">添加的文字</param>
          /// <returns>位图</returns>
          public static Bitmap CreatePicture_ShuiYin_Text(string filename, string AddText)
          {
              try
              {
                  //加文字水印，注意，这里的代码和以下加图片水印的代码不能共存
                  System.Drawing.Image image = System.Drawing.Image.FromFile(filename);
                  Graphics g = Graphics.FromImage(image);
                  g.DrawImage(image, 0, 0, image.Width, image.Height);
                  Font f = new Font("Verdana", 32);
                  Brush b = new SolidBrush(Color.White);
                  SizeF size = g.MeasureString(AddText, f); 

                  //右下角
                  int left = 0;
                  int top = 0;
                  left = image.Width  - (int)(size.Width) - 5;
                  top = image.Height - (int)(size.Height) - 5;

                  g.DrawString(AddText, f, b, left, top);
                  g.Dispose();

                  Bitmap bmp = new Bitmap(image);

                  return bmp;
              }
              catch (System.Exception)
              {
                  return null;
              }
          }

          /// <summary>
          /// 为图片添加图片水印，图片要背景透明的，gif,png支持背景透明
          /// </summary>
          /// <param name="filename"></param>
          /// <param name="ShuiYinPicFileName"></param>
          /// <returns></returns>
          public static Bitmap CreatePicture_ShuiYin_Pic(string filename, string ShuiYinPicFileName)
          {
              try
              {
                  Bitmap bmp = null;
                  //加图片水印
                  System.Drawing.Image image = System.Drawing.Image.FromFile(filename);
                  System.Drawing.Image copyImage = System.Drawing.Image.FromFile(ShuiYinPicFileName);
                  Graphics g = Graphics.FromImage(image);
                  g.DrawImage(copyImage, new Rectangle(image.Width - copyImage.Width,
                      image.Height - copyImage.Height, copyImage.Width, copyImage.Height),
                      0, 0, copyImage.Width, copyImage.Height, GraphicsUnit.Pixel);
                  g.Dispose();

                  bmp =new Bitmap(image);
                  return bmp;
              }
              catch (System.Exception)
              {
                  return null;
              }
          }
      }
}