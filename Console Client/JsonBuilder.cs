using System;
using System.Collections.Generic;
using System.Linq;


namespace INFO3137_Project2
{
    // JSONBuilder class
    public class JSONBuilder : IBuilder
    {
        /// <summary>
        /// method
        /// </summary>
        private int depth = 1;
        private Stack<IComposite> stack;
        JSONBranch root;

        /// <summary>
        /// C'tor no-arg
        /// </summary>
        public JSONBuilder()
        {
            stack = new Stack<IComposite>();
            root = new JSONBranch("root");
            stack.Push(root);
        }
        public void BuildBranch(string name)
        {
            JSONBranch branch = new JSONBranch(name);
            depth++;

            stack.Peek().AddChild(branch);
            stack.Push(branch);
        }
        public void BuildLeaf(string name, string content)
        {
            JSONLeaf leaf = new JSONLeaf(name, content);
            stack.Peek().AddChild(leaf);
        }
        public void CloseBranch()
        {
            depth--;
            if (stack.Count() > 1)
            {
                stack.Pop();
            }
        }
        public IComposite GetDocument()
        {
            return root;
        }
    } // end class

    // JSONBranch class
    public class JSONBranch : IComposite
    {
        /// <summary>
        /// method
        /// </summary>
        private string strKey;
        private List<IComposite> children;

        /// <summary>
        /// C'tor 1 arg
        /// </summary>
        public JSONBranch(string key)
        {
            strKey = key;
            children = new List<IComposite>();
        }
        public void AddChild(IComposite child)
        {
            children.Add(child);
        }
        public string Print(int depth)
        {
            //tab (\t) takes bigger space than example console's. So, I each tabbing is as same as example's by using .PadRight() function. 
            //string space = "\t";
            string space = "";
            string tabs = String.Concat(Enumerable.Repeat(space.PadRight(4), depth));
            string text = "";

            /// <summary>
            /// The root node for the JSON document should always be nothing more than curly braces
            /// </summary>
            if (strKey == "root")
            {
                text += "{\n";
            }
            else
            {
                text += $"{tabs}\'{strKey}\': \n{tabs}{{\n";
            }

            foreach (var child in children)
            {
                text += $"{child.Print(depth + 1)}";
                if (child != children[children.Count() - 1])
                {
                    text += ",\n";
                }
                else
                {
                    text += "\n";
                }
            }
            text += $"{tabs}}}";
            return text;
        }
    } // end class

    // JSONLeaf class
    public class JSONLeaf : IComposite
    {
        /// <summary>
        /// method
        /// </summary>
        private string strKey;
        private string strValue;

        /// <summary>
        /// C'tor 2 args
        /// </summary>
        public JSONLeaf(string key, string value)
        {
            strKey = key;
            strValue = value;
        }
        public string Print(int depth)
        {
            //tab (\t) takes bigger space than Kyle's example. So, each tabbing is as same as example's by using .PadRight() function. 
            //string space = "\t";
            string space = "";
            string tabs = String.Concat(Enumerable.Repeat(space.PadRight(4), depth));
            return $"{tabs}\'{strKey}\':\'{strValue}\'";
        }
        public void AddChild(IComposite child) {} // leaf do not have children
    } // end class
}// end namespace