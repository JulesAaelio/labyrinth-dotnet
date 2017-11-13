using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Odbc;
using System.Data.SqlClient;
using System.Security.Policy;
using System.Windows;
using System.Windows.Controls;
 
 namespace LABYRINTH
 {
     public class Labyrinth
     {
         private Cell[,] cells;
         private Cell _entryPoint;
         private Cell _endPoint;
         private Stack<Cell> path = new Stack<Cell>();
         private List<Cell> nogo = new List<Cell>();
 
         /// <summary>
         /// Create a graphical labyrinht with size * size area.
         /// </summary>
         /// <param name="grid"> Grid element on wich the labyrinth will be drawed </param>
         /// <param name="size"> Size on one dimension on the labyrith .</param>
         public Labyrinth(Canvas grid, int size = 10)
         {
             this.cells = new Cell[size,size];
             for (int x = 0; x < cells.GetLength(0); x++)
             {
                 for (int y = 0; y < cells.GetLength(1); y++)
                 {
                     this.cells[x,y] = new Cell(x,y,grid,40);
                 }
             }             
         }


         public void Generate()
         {
             Random randomGen = new Random();
             
             int i = randomGen.Next(this.cells.GetLength(0));
             int j = randomGen.Next(this.cells.GetLength(1));
             Generate(i,j);
         }
         /// <summary>
         /// Generate the perfect labyrinth. 
         /// </summary>
         public void Generate(int x, int y)
         {
             Cell currentCell = null;
             Cell neighborCell = null; 
             Stack<Cell> history = new Stack<Cell>();
             Random randomGen = new Random();
             
             currentCell = cells[x, y];
             this._entryPoint = currentCell;
             this._endPoint = cells[0,0];
             this._endPoint.HighlightAsExitPoint();
             
             currentCell.HighlightAsEntryPoint();
             bool exit = false;

             while (exit == false)
             {
                 List<Cell> neigbors = this.GetNeighbors(currentCell);
                 if (neigbors.Count > 0)
                 {

                     neighborCell = neigbors[randomGen.Next(neigbors.Count)];
                     BreakTheWall(currentCell, neighborCell);
                     currentCell.Visited = true;
                     neighborCell.Visited = true;

                     history.Push(currentCell);
                     currentCell = neighborCell;
                 }
                 else
                 {
                     if (history.Count > 0)
                     {
                         currentCell = history.Pop();
                     }
                     else
                     {
                         exit = true;
                     }
                 }
             }
             
         }

         
         public void Resolve()
         {
             Cell currentCell = null;
             Cell neighborCell = null; 
             Random randomGen = new Random();

             currentCell = this._entryPoint;
             currentCell.HighlightAsEntryPoint();
             bool exit = false;
             while (exit == false)
             {
                 List<Cell> neigbors = this.GetPathNeigbors(currentCell);
                 if (neigbors.Count > 0)
                 {
                     path.Push(currentCell);
                     neighborCell = neigbors[randomGen.Next(neigbors.Count)];
                     currentCell = neighborCell;
                 }
                 else
                 {
                     if (currentCell == this._endPoint)
                     {
                         exit = true;
                     }
                     else
                     {
                         nogo.Add(currentCell);
                         currentCell = path.Pop();
                     }
                     
                 }
                 
             }

             foreach (var cell in path)
             {
                 cell.HighlightAsPathMember();
                 this._entryPoint.HighlightAsEntryPoint();
             }
         }
         
         /// <summary>
         /// Get the neighbors cells or the origin cell /!\ that has not been visited.
         /// </summary>
         /// <param name="cell">Origin cell</param>
         /// <returns>List of neigbors cells that has not been visited </returns>
         public  List<Cell> GetNeighbors(Cell cell)
         {
             List<Cell> neighbors = new List<Cell>();
             int y = cell.PosY;
             int x = cell.PosX;
             
             //Upper neighbor. 
             if (y < this.cells.GetLength(1) - 1 && !this.cells[x,y+1].IsVisited())
             {
                 neighbors.Add(this.cells[x,y+1]);
             }
             
             //Lower neighbor. 
             if (y > 0 && !this.cells[x,y - 1].IsVisited())
             {
                 neighbors.Add(this.cells[x,y -1]);
             }
             
             //Right neighbor. 
             if (x < this.cells.GetLength(0) - 1 && !this.cells[x +1,y].IsVisited())
             {
                 neighbors.Add(this.cells[x +1, y]);
             }
             
             //Left neighbor.  
             if (x > 0 && !this.cells[x -1,y].IsVisited())
             {
                 neighbors.Add(this.cells[x -1,y]);
             }
             
             return neighbors;
         }

         public  List<Cell> GetPathNeigbors(Cell cell)
         {
             List<Cell> neighbors = new List<Cell>();
             int y = cell.PosY;
             int x = cell.PosX;

             if (!cell.IsLeftWallRaised())
             {
                 if (!this.path.Contains(this.cells[x - 1, y]) && !this.nogo.Contains(this.cells[x - 1, y]))
                 {
                     neighbors.Add(this.cells[x - 1, y]);
                 }
             }
             
             if (!cell.IsRightWallRaised())
             {
                 if (!this.path.Contains(this.cells[x + 1, y]) && !this.nogo.Contains(this.cells[x + 1, y]))
                 {                    
                     neighbors.Add(this.cells[x + 1, y]);
                 }
             }
             
             if (!cell.IsUpperWallRaised())
             {
                 if (!this.path.Contains(this.cells[x, y + 1]) && !this.nogo.Contains(this.cells[x, y + 1]))
                 {                     
                     neighbors.Add(this.cells[x, y + 1]);
                 }
             }
             
             if (!cell.IsLowerWallRaised())
             {
                 if (!this.path.Contains(this.cells[x, y - 1]) && !this.nogo.Contains(this.cells[x, y - 1]))
                 {                    
                     neighbors.Add(this.cells[x, y - 1]);
                 }
             }
             
             return neighbors;
         }
         
        
         /// <summary>
         /// Break the walls between two cells. 
         /// </summary>
         /// <param name="cellA">First cell</param>
         /// <param name="cellB">Second cell</param>
         private void BreakTheWall(Cell cellA, Cell cellB)
         {
             if (cellA.PosX < cellB.PosX)
             {
                 cellA.BreakRightWall();
                 cellB.BreakLeftWall();
             }
             if (cellA.PosX > cellB.PosX)
             {
                 cellB.BreakRightWall();
                 cellA.BreakLeftWall();
             }
             if (cellA.PosY < cellB.PosY)
             {
                 cellA.BreakUpperWall();
                 cellB.BreakLowerWall();
             }
             
             if (cellA.PosY > cellB.PosY)
             {
                 cellB.BreakUpperWall();
                 cellA.BreakLowerWall();
             }
         }
     }
 }