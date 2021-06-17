using System;
using System.Collections.Generic;
using System.Text;

namespace ASearch
{
    public class Board
    {
        private Stack<Node> _openList;
        private Stack<Node> _closedList;
        private Node[,] _board;
        private Node _startNode;
        private Node _endNode;
        public Board()
        {
            _board = new Node[15, 15];
            _openList = new Stack<Node>();
            _closedList = new Stack<Node>();
        }
        public void Initialize()
        {
            int blockedNodesCount = 0; // 
            for(var i = 0; i < 15; i++)
            {
                for(var j = 0; j < 15; j++)
                {
                    Node node = new Node();
                    if (blockedNodesCount < 23)
                    {
                        if (GetIfNodeBlocked())
                        {
                            node.Type = 1;
                            blockedNodesCount++;
                        }
                        else
                            node.Type = 0;
                    }
                    else
                        node.Type = 0;
                                        
                    node.Row = i;
                    node.Col = j;
                    _board[i,j] = node;
                }
            }
        }

        private bool GetIfNodeBlocked()
        {
            var rng = new Random();
            return rng.Next(0, 10) == 9 ? true : false;
        }

        public bool SetStartNode(int row, int col)
        {
            if (_board[row, col].Type == 0)
            {
                this._startNode = _board[row, col];
                this._startNode.G = 0;
                this._startNode.H = 0;
                this._openList.Push(_startNode);                
                return true;
            }
            return false;            
        }

        public bool SetEndNode(int row, int col)
        {
            if(_board[row,col].Type == 0)
            {
                this._endNode = _board[row, col];
                return true;
            }
            return false;
        }

        public void PrintBoard(bool showIntroText)
        {
            if (showIntroText)
            {
                Console.WriteLine("Initial Game Board State...");
                Console.WriteLine("Xs represent blocked nodes, Os are open nodes ");
            }
            for (var i = 0; i < 15; i++)
            {
                for(var j = 0; j < 15; j++)
                {
                    Console.Write(_board[i, j].Type == 1 ? " X " : " O ");
                }
                Console.WriteLine();
            }
        }

        public void SetHeuristicValues()
        {
            for(var i = 0; i < 15; i++)
            {
                for (var j = 0; j < 15; j++)
                {
                    if (!_board[i, j].IsEqualTo(_startNode))
                    {
                        // distance between two nodes is abs of node1.row - node2.row + abs of node1.col - row2.col
                        // h is movement cost to move from square to final desination
                        _board[i, j].H = Math.Abs(i - _endNode.Row) + Math.Abs(j - _endNode.Col);
                        // g is movement cost from starting node to current node
                        _board[i, j].G = Math.Abs(i - _startNode.Row) + Math.Abs(j - _startNode.Col);
                    }
                }
            }       
        }

        public bool PerformAlgorithm()
        {
            while(_openList.Count > 0)
            {
                var parentNode = _openList.Pop();
                List<Node> successors;
                // maximum of 8 successors
                try
                {
                    successors = GetSuccessors(parentNode.Row, parentNode.Col);
                }
                catch
                {
                    return false;
                }
                // set successor nodes parent property to node popped off stack
                foreach (var successor in successors)
                {
                    successor.Parent = parentNode;
                    // check if node is target node
                    if (successor.IsEqualTo(_endNode))
                    {
                        _closedList.Push(_endNode);
                        Console.WriteLine("Solution Found.");
                        // do some logic here, success found
                        return true;
                    }
                    CheckForAdd(successor);
                }
                _closedList.Push(parentNode);
            }
            return true;
        }

        private void CheckForAdd(Node successor)
        {
            // if a node with same parent as successor is in Open list with lower F, skip
            // if a node with same parent as successor is in Closed list with lower F, skip
            bool condition = false;
            for(var i = 0; i < _openList.Count-1; i++)
            {
                // has to be one with same parent and lower F
                if (_openList.ToArray()[i].Parent == successor.Parent && _openList.ToArray()[i].F < successor.F || successor.F == 0)
                    condition = true;
                               
            }
            for(var j = 0; j < _closedList.Count - 1; j++)
            {
                if (_closedList.ToArray()[j].IsEqualTo(successor))
                    condition = true;
                if (_closedList.ToArray()[j].Parent == successor.Parent && _closedList.ToArray()[j].F < successor.F || successor.F == 0)
                    condition = true;
            }
            if (!condition)
                _openList.Push(successor);

        }

        private List<Node> GetSuccessors(int row, int col)
        {

            var list = new List<Node>();
            if(row > 0)
                if(_board[row-1, col].Type != 1)
                    list.Add(_board[row - 1, col]);
            if (row < 14)
                if (_board[row + 1, col].Type != 1)
                    list.Add(_board[row + 1, col]);
            if(col > 0)
                if(_board[row, col-1].Type != 1)
                    list.Add(_board[row, col - 1]);
            if(col < 14)
                if(_board[row, col+1].Type != 1)
                    list.Add(_board[row, col + 1]);
            if (list.Count == 0)
                throw new Exception("no path could be found.");
            return list;           
        }

        public void PrintLists()
        {
            var outputArray = _closedList.ToArray();
            var openArray = _openList.ToArray();
            Console.WriteLine();
            Console.WriteLine("Path ");       
            for (var j = _closedList.Count -1; j > 0 ; j--)
            {
                Console.WriteLine(outputArray[j].ToString());             
            }
            for(var i = _openList.Count -1; i > 0; i--)
            {
                Console.WriteLine(openArray[i].ToString());
            }
            
        }


    }
}
