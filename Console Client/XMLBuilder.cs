/*
 * Program:         INFO3137_Project2 (Document Builder Console Client)
 * Module:          XMLBuilder.cs
 * Date:            July 17, 2019
 * Author:          Youngmin Chung
 * Description:                  
 */

using System;
using System.Collections.Generic;
using System.Linq;

namespace INFO3137_Project2
{
    // XMLBuilder class
    public class XMLBuilder : IBuilder
    {
        /// <summary>
        /// an int for depth will add tabs to the beginning of each line that is printed.
        /// </summary>
        private int depth = 0;

        /// <summary>
        /// In-class we used a Queue or List to do this. I chose "LinkedList".
        /// </summary>
        private LinkedList<IComposite> linked;

        /// <summary>
        /// On creation, should create a root Branch for the document
        /// </summary>
        private XMLBranch root;

        /// <summary>
        /// On creation, should create a root Branch for the document
        /// </summary>
        public XMLBuilder()
        {
            linked = new LinkedList<IComposite>();
            root = new XMLBranch("root");
            linked.AddFirst(root);
        }

        /// <summary>
        /// BuildBranch
        /// When creating a Composite, the builder should maintain a reference to the last opened Branch
        /// </summary>
        public void BuildBranch(string name)
        {
            XMLBranch branch = new XMLBranch(name);
            depth++;

            linked.First().AddChild(branch);
            linked.AddFirst(branch);
        }

        /// <summary>
        /// BuildLeaf
        /// </summary>
        public void BuildLeaf(string name, string content)
        {
            XMLLeaf leaf = new XMLLeaf(name, content);
            linked.First().AddChild(leaf);
        }

        /// <summary>
        /// CloseBranch
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
    public class XMLBranch : IComposite
    {
        private string strKey;
        private List<IComposite> children;

        /// <summary>
        /// Branches can have children, but no text content
        /// </summary>
        public XMLBranch(string key)
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
            string text = $"{tabs}<{strKey}>\n";

            foreach (var child in children)
            {
                text += $"{child.Print(depth + 1)}\n";
            }
            text += $"{tabs}</{strKey}>";

            return text;
        }
    } // end class

    /// <summary>
    /// Needs a concrete class for both JSON and XML
    /// Needs to have separate implementations for Leaf
    /// </summary>
    public class XMLLeaf : IComposite
    {
        private string strKey;
        private string strValue;

        /// <summary>
        /// Leaves can have text content, but no children
        /// </summary>
        public XMLLeaf(string key, string value)
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

            return $"{tabs}<{strKey}>{strValue}</{strKey}>";
        }
        public void AddChild(IComposite child) { } // leaf do not have children
    } // end class
}// end namespace