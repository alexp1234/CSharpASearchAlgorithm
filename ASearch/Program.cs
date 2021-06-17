using System;

namespace ASearch
{
    class Program
    {
        static void Main(string[] args)
        {
            // Initialize Board and show what it looks like visualized
            var board = new Board();
            board.Initialize();
            board.PrintBoard(true);

            Console.WriteLine("Enter Starting Node Row (0-14): ");
            var startRow = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter Starting Node Column (0-14):");
            var startCol = int.Parse(Console.ReadLine());
            
            while(board.SetStartNode(startRow, startCol) == false)
            {
                Console.WriteLine("Sorry, a blocked node can't be used for start.");
                Console.WriteLine("Enter Starting Node Row (0-14): ");
                startRow = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter Starting Node Column (0-14):");
                startCol = int.Parse(Console.ReadLine());
            }

            Console.WriteLine("Enter Goal Node Row (0-14): ");
            var endRow = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter Goal Node Col (0-14): ");
            var endCol = int.Parse(Console.ReadLine());
            while(board.SetEndNode(endRow, endCol) == false)
            {
                Console.WriteLine("Sorry, a blocked node can't be used for target node.");
                Console.WriteLine("Enter Goal Node Row (0-14): ");
                endRow = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter Goal Node Col (0-14): ");
                endCol = int.Parse(Console.ReadLine());
            }

            // set values for each node now that we know start and end node
            board.SetHeuristicValues();
           // board.PrintFScore();
           // only print success message if solution is found, otherwise print failure message.
           if (board.PerformAlgorithm())
                board.PrintLists();

            Console.ReadLine();
             
        }
    }
}
