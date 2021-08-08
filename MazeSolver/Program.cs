using System;

namespace MazeSolver
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            char[,] x = { { 's', '.' , '#' , 'x' },
                          { '#', '.' , '.' , '.' }};
            MazeModel maze = new MazeModel { MazeField = x };
            maze.Solve();
        }
    }
}
