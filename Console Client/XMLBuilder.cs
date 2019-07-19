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
        /// method
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
        /// C'tor no-arg
        /// </summary>
        public XMLBuilder()
        {
            linked = new LinkedList<IComposite>();
            root = new XMLBranch("root");
            linked.AddFirst(root);
        }
        public void BuildBranch(string name)
        {
            XMLBranch branch = new XMLBranch(name);
            depth++;

            linked.First().AddChild(branch);
            linked.AddFirst(branch);
        }
        public void BuildLeaf(string name, string content)
        {
            XMLLeaf leaf = new XMLLeaf(name, content);
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

    // XMLBranch class
    public class XMLBranch : IComposite
    {
        /// <summary>
        /// method
        /// </summary>
        private string strKey;
        private List<IComposite> children;

        /// <summary>
        /// C'tor 1 arg
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

    // XMLLeaf class
    public class XMLLeaf : IComposite
    {
        /// <summary>
        /// method
        /// </summary>
        private string strKey;
        private string strValue;

        /// <summary>
        /// C'tor 2 args
        /// </summary>
        public XMLLeaf(string key, string value)
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

            return $"{tabs}<{strKey}>{strValue}</{strKey}>";
        }
        public void AddChild(IComposite child) { } // leaf do not have children
    } // end class
}// end namespace