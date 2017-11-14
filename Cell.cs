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
        private DIRECTION _direction;
        private Rectangle highlight;
        private Polygon triangle;

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

        public void HighlightReset()
        {
            this.Highlight(System.Windows.Media.Brushes.White);
        }

        public void Highlight(SolidColorBrush brush)
        {
            if (this.highlight == null)
            {
                this.highlight = new Rectangle();
                this.highlight.Height = this.size -2;
                this.highlight.Width =  this.size -2;           
                this.grid.Children.Add(this.highlight);
                Canvas.SetLeft(this.highlight,this.clientPosX +1);
                Canvas.SetTop(this.highlight,this.clientPosY +1 );
            }
            this.highlight.Fill = brush;
        }

        public enum DIRECTION
        {
            UP,
            DOWN,
            LEFT,
            RIGHT
        }

        public void MarkDirection()
        {
            MarkDirection(this._direction);
        }

        public void MarkDirection(DIRECTION direction)
        {
            PointCollection myPointCollection = new PointCollection();
            switch (direction)
            {
                    case DIRECTION.UP :
                        myPointCollection.Add(new Point(0,0));
                        myPointCollection.Add(new Point(2,0));
                        myPointCollection.Add(new Point(1,2));
                        break;
                    case DIRECTION.DOWN : 
                        myPointCollection.Add(new Point(0,2));
                        myPointCollection.Add(new Point(2,2));
                        myPointCollection.Add(new Point(1,0));
                        break;
                    case DIRECTION.LEFT : 
                        myPointCollection.Add(new Point(0,0));
                        myPointCollection.Add(new Point(2,1));
                        myPointCollection.Add(new Point(0,2));
                        break;
                    case DIRECTION.RIGHT : 
                        myPointCollection.Add(new Point(0,1));
                        myPointCollection.Add(new Point(2,0));
                        myPointCollection.Add(new Point(2,2));
                        break;
            }
            
            this.triangle = new Polygon();
            this.triangle.Points = myPointCollection;
            this.triangle.Width = this.size/2 ;
            this.triangle.Height = this.size / 2;
            this.triangle.Stretch = Stretch.Fill;
            this.triangle.Stroke = Brushes.Black;
            this.triangle.StrokeThickness = 2;
            this.grid.Children.Add(triangle);
            Canvas.SetLeft(this.triangle,this.clientPosX + (this.size/4));
            Canvas.SetTop(this.triangle,this.clientPosY + (this.size/4) );
        }

        public void Reset()
        {
            this.grid.Children.Remove(this.triangle);
            this.HighlightReset();
        }

        public DIRECTION Direction
        {
            get { return _direction; }
            set { _direction = value; }
        }
    }
}