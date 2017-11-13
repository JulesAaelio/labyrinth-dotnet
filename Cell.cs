using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace LABYRINTH
{
    public class Cell 
    {
        private int posX;
        private int posY;
        private int clientPosX; 
        private int clientPosY; 
        private Line _upperWall;
        private Line _lowerWall;
        private Line _leftWall;
        private Line _rightWall;
        private Canvas grid;
        private int size;
        private bool _visited;

        public Cell(int posX, int posY,int size = 10)
        {
            this.posX = posX;
            this.clientPosX = posX * size;
            this.posY = posY;
            this.clientPosY = posY * size;
            this.size = size;
            this._visited = false;
        }

        public Cell(int posX, int posY, Canvas grid,int size= 10) : this(posX, posY, size)
        {
            this.grid = grid;
            this.Init(); 
        }

        /// <summary>
        /// Create and display walls.
        /// </summary>
        public void Init()
        {
            this._lowerWall = Walls.CreateHorizontalWall(this.clientPosX, this.clientPosY,this.size);
            this._upperWall = Walls.CreateHorizontalWall(this.clientPosX, this.clientPosY +this.size,this.size);
            this._leftWall = Walls.CreateVerticalLine(this.clientPosX , this.clientPosY,this.size);
            this._rightWall = Walls.CreateVerticalLine(this.clientPosX + this.size , this.clientPosY,this.size);
            this.grid.Children.Add(this._lowerWall);
            this.grid.Children.Add(this._upperWall);
            this.grid.Children.Add(this._leftWall);
            this.grid.Children.Add(this._rightWall);
        }

        public bool IsUpperWallRaised()
        {
            return this.grid.Children.Contains(this._upperWall);
        }
        
        public bool IsLowerWallRaised()
                 {
            return this.grid.Children.Contains(this._lowerWall);
        }
        
        public bool IsLeftWallRaised()
        {
            return this.grid.Children.Contains(this._leftWall);
        }
        
        public bool IsRightWallRaised()
        {
            return this.grid.Children.Contains(this._rightWall);
        }

        public bool IsVisited()
        {
            return this._visited;
        }

        public bool IsSquare()
        {
            return (this.IsLeftWallRaised() && this.IsRightWallRaised() && this.IsUpperWallRaised() &&
                    this.IsLowerWallRaised());
        }

        public void BreakLowerWall()
        {
            this.grid.Children.Remove(this._lowerWall);
        }
        
        public void BreakUpperWall()
        {
            this.grid.Children.Remove(this._upperWall);
        }
        
        public void BreakLeftWall()
        {
            this.grid.Children.Remove(this._leftWall);
        }
        
        public void BreakRightWall()
        {
            this.grid.Children.Remove(this._rightWall);
        }

        public int PosX
        {
            get { return posX; }
        }

        public int PosY
        {
            get { return posY; }
        }

        public bool Visited
        {
            get { return _visited; }
            set { _visited = value; }
        }


        public void Highlight()
        {
            this._upperWall.Stroke = System.Windows.Media.Brushes.Blue;
            this._upperWall.StrokeThickness = 4;
            this._lowerWall.Stroke = System.Windows.Media.Brushes.Blue;
            this._lowerWall.StrokeThickness = 4;
            this._leftWall.Stroke = System.Windows.Media.Brushes.Blue;
            this._upperWall.StrokeThickness = 4;
            this._rightWall.Stroke = System.Windows.Media.Brushes.Blue;
            this._lowerWall.StrokeThickness = 4;
        }

        public void HighlightAsEntryPoint()
        {
           this.Highlight(System.Windows.Media.Brushes.LightGreen);
        }
        
        public void HighlightAsExitPoint()
        {
            this.Highlight(System.Windows.Media.Brushes.LightCoral);
        }
        
        public void HighlightAsPathMember()
        {
            this.Highlight(System.Windows.Media.Brushes.LightBlue);
        }

        public void Highlight(SolidColorBrush brush)
        {
            Rectangle rectangle = new Rectangle();
            rectangle.Height = this.size -2;
            rectangle.Width =  this.size -2;           
            // Create a blue and a black Brush
            rectangle.Fill = brush;
            this.grid.Children.Add(rectangle);
            Canvas.SetLeft(rectangle,this.clientPosX +1);
            Canvas.SetTop(rectangle,this.clientPosY +1 );
        }
    }
}