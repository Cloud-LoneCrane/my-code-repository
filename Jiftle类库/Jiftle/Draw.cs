
using System.Drawing;

namespace Jiftle
{
      public class Draw
      {
        /// <summary>
        /// ��������ͼ
        /// </summary>
        /// <param name="filename">�ļ���</param>
        /// <param name="width">��</param>
        /// <param name="height">��</param>
        /// <returns>λͼ</returns>
        /// <remarks>eg.
        /// Bitmap bmp = CreateThumbPicture(@"D:\pic.jpg", 100, 100);
        /// bmp.Save(@"d:\picThumb.jpg", System.Drawing.Imaging.ImageFormat.jpg);
        /// ����jpg��ʽ,jpg��ͼƬ���С 
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
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High; //��������ֵ��
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality; //�����������ٶȳ���ƽ���̶�
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
          /// ����С���������ˮӡ
          /// </summary>
          /// <param name="filename">��Ҫ���ˮӡ��ͼƬ</param>
          /// <param name="AddText">��ӵ�����</param>
          /// <returns>λͼ</returns>
          public static Bitmap CreatePicture_ShuiYin_Text(string filename, string AddText)
          {
              try
              {
                  //������ˮӡ��ע�⣬����Ĵ�������¼�ͼƬˮӡ�Ĵ��벻�ܹ���
                  System.Drawing.Image image = System.Drawing.Image.FromFile(filename);
                  Graphics g = Graphics.FromImage(image);
                  g.DrawImage(image, 0, 0, image.Width, image.Height);
                  Font f = new Font("Verdana", 32);
                  Brush b = new SolidBrush(Color.White);
                  SizeF size = g.MeasureString(AddText, f); 

                  //���½�
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
          /// ΪͼƬ���ͼƬˮӡ��ͼƬҪ����͸���ģ�gif,png֧�ֱ���͸��
          /// </summary>
          /// <param name="filename"></param>
          /// <param name="ShuiYinPicFileName"></param>
          /// <returns></returns>
          public static Bitmap CreatePicture_ShuiYin_Pic(string filename, string ShuiYinPicFileName)
          {
              try
              {
                  Bitmap bmp = null;
                  //��ͼƬˮӡ
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