using System.Data;
using System.Windows;
using System.Windows.Shapes;
namespace LABYRINTH
{
    public static class Walls 
    {
        public static Line CreateHorizontalWall(int startX,int startY,int width = 20)
        {
            Line horizontalLine = new Line();
            horizontalLine.Stroke = System.Windows.Media.Brushes.Black;
            horizontalLine.X1 = startX;
            horizontalLine.X2 = startX + width;
            
            horizontalLine.Y1 = startY;
            horizontalLine.Y2 = startY;
//            horizontalLine.HorizontalAlignment = HorizontalAlignment.Left;
//            horizontalLine.VerticalAlignment = VerticalAlignment.Center;
            horizontalLine.StrokeThickness = 1;
            return horizontalLine;
        }
        
        public static Line CreateVerticalLine(int startX,int startY,int heigth = 20)
        {
            Line horizontalLine = new Line();
            horizontalLine.Stroke = System.Windows.Media.Brushes.Black;
            horizontalLine.X1 = startX;
            horizontalLine.X2 = startX;
            horizontalLine.Y1 = startY;
            horizontalLine.Y2 = startY + heigth;
            horizontalLine.StrokeThickness = 1;
            return horizontalLine;
        }
        
    }
}