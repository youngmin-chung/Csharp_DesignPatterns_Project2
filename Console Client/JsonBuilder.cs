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
        /// an int for depth will add tabs to the beginning of each line that is printed.
        /// </summary>
        private int depth = 1;

        /// <summary>
        /// In-class we used a Queue or List to do this. I chose "LinkedList".
        /// </summary>
        private LinkedList<IComposite> linked;

        /// <summary>
        /// declare root
        /// </summary>
        JSONBranch root = new JSONBranch("root");

        /// <summary>
        /// On creation, should create a root Branch for the document
        /// </summary>
        public JSONBuilder()
        {
            linked = new LinkedList<IComposite>();
            linked.AddFirst(root);
        }

        /// <summary>
        /// BuildBranch
        /// When creating a Composite, the builder should maintain a reference to the last opened Branch
        /// </summary>
        public void BuildBranch(string name)
        {
            JSONBranch branch = new JSONBranch(name);
            depth++;

            linked.First().AddChild(branch);
            linked.AddFirst(branch);
        }

        /// <summary>
        /// BuildLeaf
        /// </summary>
        public void BuildLeaf(string name, string content)
        {
            JSONLeaf leaf = new JSONLeaf(name, content);
            linked.First().AddChild(leaf);
        }

        /// <summary>
        /// CloseBranch
        /// When CloseBranch is called, the maintained Branch reference should be changed to the current Branch's parent
        /// </summary>
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

    /// <summary>
    /// Needs a concrete class for both JSON and XML
    /// Needs to have separate implementations for Branch
    /// </summary>
    public class JSONBranch : IComposite
    {
        private string strKey;
        private List<IComposite> children;

        /// <summary>
        /// Branches can have children, but no text content
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

        /// <summary>
        /// Printing a composite should return a string representing its full content
        /// The print method accepts an int for depth. 
        /// Branch : print all of their child nodes
        /// </summary>
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

    /// <summary>
    /// Needs a concrete class for both JSON and XML
    /// Needs to have separate implementations for Leaf
    /// </summary>
    public class JSONLeaf : IComposite
    {
        private string strKey;
        private string strValue;

        /// <summary>
        /// Leaves can have text content, but no children
        /// </summary>
        public JSONLeaf(string key, string value)
        {
            strKey = key;
            strValue = value;
        }

        /// <summary>
        /// Printing a composite should return a string representing its full content
        /// The print method accepts an int for depth. This will add tabs to the beginning of each line that is printed.
        /// Leaf: only print their content
        /// </summary>
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