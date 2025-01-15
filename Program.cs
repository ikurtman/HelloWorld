using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public enum NotifyType
    {
        Mail = 1,
        Ivr = 2,
        Sms = 4
    }
    class Solution
    {
        private static Node _parent;
       
        static void Main(string[] args)
        {
            decimal value = 0.0896M;
            string x;
            if (value.Equals(decimal.Zero))
                x = "00000000000";
            else if (value.Equals(decimal.One))
                x = "00010000000";
            else
                x = value.ToString("0000.0000000").Replace(",", "").Replace(".", "");



                  x =  value.ToString().Split(',').First().PadLeft(4, '0') + value.ToString().Split(',').Last().PadRight(7, '0');


            var c = new Derived(2);
            var xy = c.X;

            int r = 5 / 3;


            List<object> lstObject = new List<object>();

            for(int ik = 0; ik<50; ik++)
            {
                lstObject.Add(new { P1 = "", P2 = DateTime.Now, P3 = 4.6M, P4 = double.MinValue });
            }


            int i = 0;
            i++;

            decimal datePart = Convert.ToDecimal($"{DateTime.Now.Year.ToString("00")}{DateTime.Now.Month.ToString("00")}{DateTime.Now.Day.ToString("00")}");

            NotifyType n = NotifyType.Mail | NotifyType.Sms;
            if(n == NotifyType.Mail)
            {
                string s = "";
            }

            StringBuilder sb1 = new StringBuilder();
            sb1.Append(1);
            sb1.Append(1);
            sb1.Append(1);
            sb1.Append(1);
            sb1.Append(1);
            sb1.Append(1);
            sb1.Append(1);
            sb1.Append(1);

            Console.Write(sb1.ToString());

            /* Enter your code here. Read input from STDIN. Print output to STDOUT */
            try
            {
                string treeInput = "(J,P) (J,O) (J,N) (J,P)";//Console.ReadLine();
                Dictionary<char, List<char>> nodeList = CheckInput(treeInput);
                _parent = CheckTree(nodeList);

                StringBuilder sb = new StringBuilder("(");
                PrintTree(_parent, sb);
                Console.WriteLine(sb.Append(")").ToString());
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private static Dictionary<char, List<char>> CheckInput(string treeInput)
        {
            if (treeInput.Contains(Environment.NewLine))
            {
                throw new Exception("E1");
            }
            if (char.IsWhiteSpace(treeInput[0]) || char.IsWhiteSpace(treeInput[treeInput.Length - 1]))
            {
                throw new Exception("E1");
            }

            Dictionary<char, List<char>> nodeList = new Dictionary<char, List<char>>();
            string[] nodes = treeInput.Split(' ');
            foreach (string node in nodes)
            {
                string[] nodeParts = node.Split(',');
                if (!nodeParts[0].StartsWith("(") || !nodeParts[1].EndsWith(")"))
                {
                    throw new Exception("E1");
                }
                if (nodeParts[0].Length != 2 || nodeParts[1].Length != 2)
                {
                    throw new Exception("E1");
                }
                char n1 = nodeParts[0][1];
                char n2 = nodeParts[1][0];
                if (!char.IsUpper(n1) || !char.IsUpper(n2))
                {
                    throw new Exception("E1");
                }
                if (!nodeList.ContainsKey(n1))
                {
                    nodeList.Add(n1, new List<char>());
                }
                List<char> subNodes = nodeList[n1];
                if (subNodes.Contains(n2))
                {
                    throw new Exception("E2");
                }
                //if (subNodes.Count() >= 2)
                //{
                //    throw new Exception("E3");
                //}
                subNodes.Add(n2);
            }
            foreach(var kvp in nodeList)
            {
                if(kvp.Value.Count > 2)
                    throw new Exception("E3");
            }
            return nodeList;
        }

        private static Node CheckTree(Dictionary<char, List<char>> nodeList)
        {
            Dictionary<char, Node> nodes = new Dictionary<char, Node>();
            List<Node> parents = new List<Node>();
            foreach (char key in nodeList.Keys)
            {
                if (!nodes.ContainsKey(key))
                {
                    nodes.Add(key, new Node(key));
                }
                Node currentNode = nodes[key];
                foreach (char subKey in nodeList[key])
                {
                    if (!nodes.ContainsKey(subKey))
                    {
                        nodes.Add(subKey, new Node(subKey));
                    }
                    Node subNode = nodes[subKey];
                    currentNode.SetChild(subNode);
                    subNode.SetParent(currentNode);
                    parents.Remove(subNode);
                }
                parents.Remove(currentNode);
                if (currentNode.Parent == null)
                {
                    parents.Add(currentNode);
                }
            }
            if (parents.Count != 1)
            {
                throw new Exception("E5");
            }
            return parents.ElementAt(0);
        }
        private static void PrintTree(Node parent, StringBuilder sb)
        {
            sb.AppendFormat("{0}", parent.Id);
            if (parent.Child1 != null)
            {
                sb.AppendFormat("(");
                PrintTree(parent.Child1, sb);
                sb.AppendFormat(")");
            }
            if (parent.Child2 != null)
            {
                sb.AppendFormat("(");
                PrintTree(parent.Child2, sb);
                sb.AppendFormat(")");
            }
        }
    }

    public class Node
    {
        public Node Parent { get; private set; }
        public Node Child1 { get; private set; }
        public Node Child2 { get; private set; }
        public char Id { get; }

        public Node(char id)
        {
            Id = id;
        }
        public void SetChild(Node child)
        {
            var tmpChildNode = this;
            while (tmpChildNode != null)
            {
                if (tmpChildNode.Parent == child)
                    throw new Exception("E4");
                tmpChildNode = tmpChildNode.Parent;
            }

            if (Child1 == null)
            {
                Child1 = child;
            }
            else
            {
                Child2 = child;
                if(child.Id < Child1.Id)
                {
                    Child2 = Child1;
                    Child1 = child;                    
                }               
            }
        }
        public void SetParent(Node parent)
        {
            //if (Parent != null && Parent != parent)
            //{
            //    throw new Exception("E4");
            //}
            Parent = parent;
        }
        //public Node GetRoot()
        //{
        //    if (Parent == null)
        //    {
        //        return this;
        //    }
        //    return Parent.GetRoot();
        //}
    }
}
