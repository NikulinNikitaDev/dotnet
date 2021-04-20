using System;

namespace MyBinaryTree
{
    public class Node
    {
        public int Value;
        public Node Left;
        public Node Right;
    }

    public class MyBinaryTree
    {
        private Node _root;

        public MyBinaryTree()
        {
            _root = null;
        }

        public void Add(int val)
        {
            if (_root != null)
                Add(val, _root);
            else
                _root = new Node
                {
                    Value = val,
                    Left = null,
                    Right = null
                };
        }

        private static void Add(int val, Node node)
        {
            while (true)
            {
                if (val < node.Value)
                {
                    if (node.Left != null)
                    {
                        node = node.Left;
                        continue;
                    }

                    node.Left = new Node
                    {
                        Value = val,
                        Left = null,
                        Right = null
                    };
                }
                else if (val >= node.Value)
                {
                    if (node.Right != null)
                    {
                        node = node.Right;
                        continue;
                    }

                    node.Right = new Node
                    {
                        Value = val,
                        Right = null,
                        Left = null
                    };
                }

                break;
            }
        }

        public Node Search(int val)
        {
            return Search(val, _root);
        }

        private static Node Search(int val, Node node)
        {
            while (true)
            {
                if (node != null)
                {
                    if (val == node.Value)
                        return node;

                    node = val < node.Value ? node.Left : node.Right;
                }
                else
                {
                    return null;
                }
            }
        }

        public void Delete(int val)
        {
            Delete(_root, val);
        }

        private static Node Delete(Node root, int val)
        {
            if (root == null)
                return null;

            if (val < root.Value)
                root.Left = Delete(root.Left, val);
            else if (val > root.Value)
                root.Right = Delete(root.Right, val);
            else
            {
                if (root.Left == null && root.Right == null)
                    return null;
                if (root.Left == null)
                    root = root.Right;
                else if (root.Right == null)
                    root = root.Left;
                else
                {
                    Node leftNode = root.Right;
                    Node parentNode = root;
                    while (leftNode.Left != null)
                    {
                        parentNode = leftNode;
                        leftNode = leftNode.Left;
                    }

                    root.Value = leftNode.Value;
                    if (leftNode.Right != null)
                    {
                        if (parentNode.Left.Value == leftNode.Value)
                            parentNode.Left = leftNode;
                        else
                            parentNode.Right = leftNode;
                    }
                    else if (parentNode.Left.Value == leftNode.Value)
                        parentNode.Left = null;
                    else
                        parentNode.Right = null;
                }
            }

            return root;
        }
        
        public void Print()
        {
            Print(_root);
            Console.WriteLine();
        }

        private static void Print(Node node)
        {
            while (true)
            {
                if (node != null)
                {
                    Print(node.Left);
                    Console.Write(node.Value.ToString() + ' ');
                    node = node.Right;
                    continue;
                }

                break;
            }
        }
    }

    public static class Program
    {
        public static void Main(string[] args)
        {
            MyBinaryTree tree = new MyBinaryTree();

            tree.Add(5);
            tree.Add(3);
            tree.Add(7);
            tree.Add(2);
            tree.Add(4);
            tree.Add(6);
            tree.Add(8);
            
            Console.Write("Create: ");
            
            tree.Print();
            
            Console.Write("Search 6: ");

            Node node = tree.Search(6);
            Console.WriteLine(node != null ? "Element found!" : "Element not found...");
            
            Console.Write("Search 10: ");
            
            node = tree.Search(10);
            Console.WriteLine(node != null ? "Element found!" : "Element not found...");
            
            Console.Write("Delete 7: ");
            
            tree.Delete(7);
            tree.Print();
        }
    }
}