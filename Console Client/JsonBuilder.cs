/*
 * Program:         INFO3137_Project2 (Document Builder Console Client)
 * Module:          JSONBuilder.cs
 * Date:            July 17, 2019
 * Author:          Youngmin Chung
 * Description:                  
 */

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

        /// <summary>
        /// In-class we used a Queue or List to do this. I chose "LinkedList".
        /// </summary>
        private LinkedList<IComposite> linked;

        /// <summary>
        /// On creation, should create a root Branch for the document
        /// </summary>
        JSONBranch root;

        /// <summary>
        /// C'tor no-arg
        /// </summary>
        public JSONBuilder()
        {
            linked = new LinkedList<IComposite>();
            root = new JSONBranch("root");
            linked.AddFirst(root);
        }
        public void BuildBranch(string name)
        {
            JSONBranch branch = new JSONBranch(name);
            depth++;

            linked.First().AddChild(branch);
            linked.AddFirst(branch);
        }
        public void BuildLeaf(string name, string content)
        {
            JSONLeaf leaf = new JSONLeaf(name, content);
            linked.First().AddChild(leaf);
        }
        public void CloseBranch()
        {
            depth--;
            if (linked.Count() > 1)
            {
                linked.RemoveFirst();
            }
        }

        /// <summary>
        /// GetDocument returns the root node
        /// </summary>
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