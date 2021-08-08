using System;
using System.Collections.Generic;
using System.Drawing;

namespace MazeSolver
{
    public class MazeModel
    {
        /// <summary>
        /// Maze content with start location, end location, walls and optionaly Monter whitch hount you
        /// Legend:
        /// . - Valid  path
        /// # - Wall, barrier
        /// s - Start location
        /// x - end location
        /// M - Monster
        /// </summary>
        public char[,] MazeField { get; set; }
        private bool[,] visited { get; set; }
        /// <summary>
        /// Is maze containg monster?
        /// </summary>
        public bool isAdvanced { get; private set; }

        public MazeModel(char[,] _maze) {
            this.MazeField = _maze;
            this.visited = new bool[_maze.GetLength(0), _maze.GetLength(1)];
        }

        public string Solve() {
            //BFS algoritm
            bool[,] visited = new bool[MazeField.GetLength(0), MazeField.GetLength(1)];
            Queue<Point> points = new Queue<Point>();
            Point start = CoordinatesOf(this.MazeField, 's');
            points.Enqueue(start);
            visited[start.X, start.Y] = true;

            int movesCount = 0;
            int nodesLeftInLayer = 1;
            int nodesInNextLayer = 0;

            while (points.Count > 0) {
                Point currentPoint = points.Dequeue();
                nodesLeftInLayer--;
                if (this.MazeField[currentPoint.X, currentPoint.Y] == 'x') { return "Solved in " + movesCount + " moves"; }

                exploreNeighboours(currentPoint, ref points, ref visited, ref nodesInNextLayer);

                if (nodesLeftInLayer == 0)
                {
                    nodesLeftInLayer = nodesInNextLayer;
                    nodesInNextLayer = 0;
                    movesCount++;
                }
            }
            return null;
        }

        private void exploreNeighboours(Point currentPoint, ref Queue<Point> points, ref bool[,] visited, ref int nodesInNextLayer)
        {
            short[] rowVector = { -1, 1,0,0};
            short[] columnVector = { 0,0,1,-1};

            for (int i = 0; i < 4; i++)
            {
                
                Point x = new Point(currentPoint.X + rowVector[i], currentPoint.Y + columnVector[i]);
                if (x.X >= 0 && x.Y >= 0 && x.X < this.MazeField.GetLength(0) && x.Y < this.MazeField.GetLength(1)) {
                    if (!visited[x.X,x.Y] && this.MazeField[x.X,x.Y] != '#')
                    {
                        points.Enqueue(x);
                        visited[x.X, x.Y] = true;
                        nodesInNextLayer++;
                    }
                } 
            }
        }

        private string retraceMaze() {
            return new NotImplementedException().ToString();
        }

        private Point CoordinatesOf(char[,] matrix, char value)
        {
            int w = matrix.GetLength(0); // width
            int h = matrix.GetLength(1); // height

            for (int x = 0; x < w; ++x)
            {
                for (int y = 0; y < h; ++y)
                {
                    if (matrix[x, y].Equals(value))
                        return new Point(x,y);
                }
            }

            return new Point(-1, -1);
        }
    }
}
