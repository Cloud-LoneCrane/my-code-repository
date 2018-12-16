using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace WindowsApplication1
{
    static class Program
    {
        int intArc;
        int intArrowHeight;

        public enum ArrowLocation
        {
            TopLeft,
            TopRight,
            BottomLeft,
            BottomRight
        }
        private void SetInfoWindowRegion()
        {
　        if (!this.IsHandleCreated)
　　        return; 
　        System.Drawing.Size windowSize = this.Size;
　        Point[] ArrowPoints = new Point[3];
　        Point topLeftPoint = Point.Empty;
　        Point bottomRightPoint = (Point)windowSize;
　        switch (this.GetArrowLocation)
　        {
　　        case ArrowLocation.TopLeft:
　　　        ´´
　　        case ArrowLocation.TopRight:
　　　        ´´
　　        case ArrowLocation.BottomLeft:
　　　        ´´
　　        case ArrowLocation.BottomRight:
　　　        ´´
　        }
　        ´´
　        ´´
　        if ((this.GetArrowLocation == ArrowLocation.TopLeft) ||
        (this.GetArrowLocation == ArrowLocation.TopRight))
　        {
　　        gPath.AddArc(topLeftPoint.X, rectY2 - arcRadius, arcDia, arcDia, 90, 90);
　　        gPath.AddLine(topLeftPoint.X, rectY2, topLeftPoint.X, rectY1);
　　        gPath.AddArc(topLeftPoint.X, topLeftPoint.Y, arcDia, arcDia, 180, 90);
　　        gPath.AddLine(rectX1, topLeftPoint.Y, ArrowPoints[0].X, topLeftPoint.Y);
　　        gPath.AddLines(ArrowPoints);
　　        gPath.AddLine(ArrowPoints[2].X, topLeftPoint.Y, rectX2, topLeftPoint.Y);
　　        gPath.AddArc(rectX2 - arcRadius, topLeftPoint.Y, arcDia, arcDia, 270, 90);
　　        gPath.AddLine(bottomRightPoint.X, rectY1, bottomRightPoint.X, rectY2);
　　        gPath.AddArc(rectX2 - arcRadius, rectY2 - arcRadius, arcDia, arcDia, 0, 90);
　　        gPath.AddLine(rectX2, bottomRightPoint.Y, rectX1, bottomRightPoint.Y);
　        }
　        else
　        {
　　        gPath.AddLine(rectX1, topLeftPoint.Y, rectX2, topLeftPoint.Y);
　　        gPath.AddArc(rectX2 - arcRadius, topLeftPoint.Y, arcDia, arcDia, 270, 90);
　　        gPath.AddLine(bottomRightPoint.X, rectY1, bottomRightPoint.X, rectY2);
　　        gPath.AddArc(rectX2 - arcRadius, rectY2 - arcRadius, arcDia, arcDia, 0, 90);
　　        gPath.AddLine(rectX2, bottomRightPoint.Y, ArrowPoints[0].X, bottomRightPoint.Y);
　　        gPath.AddLines(ArrowPoints);
　　        gPath.AddLine(ArrowPoints[2].X, bottomRightPoint.Y, rectX1, bottomRightPoint.Y);
　　        gPath.AddArc(topLeftPoint.X, rectY2 - arcRadius, arcDia, arcDia, 90, 90);
　　        gPath.AddLine(topLeftPoint.X, rectY2, topLeftPoint.X, rectY1);
　　        gPath.AddArc(topLeftPoint.X, topLeftPoint.Y, arcDia, arcDia, 180, 90);
　        }
　        gPath.CloseFigure();
　        this.Region = new Region(this.gPath);
        } 

        public static Point AnchorPointFromControl(Control anchorControl)
        {
　        if (anchorControl == null)
　        throw new ArgumentException(); 
　        Point controlLocation = anchorControl.Location;
　        System.Drawing.Size controlSize = anchorControl.Size;

　        if (anchorControl.Parent != null)
　　        controlLocation = anchorControl.Parent.PointToScreen(controlLocation);
　        return controlLocation + new Size(controlSize.Width / 2, controlSize.Height / 2);
        }
        /// <summary>
        /// 哘喘殻會議麼秘笥泣。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}