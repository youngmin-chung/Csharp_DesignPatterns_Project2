﻿/*
 * Program:         INFO3137_Project2 (Document Builder Console Client)
 * Module:          Interfaces.cs
 * Date:            July 17, 2019
 * Author:          Youngmin Chung
 * Description:                  
 */

using System;
using System.Collections.Generic;
using System.Linq;

namespace INFO3137_Project2
{
    /// <summary>
    /// Interfaces... Never modify these three musketeers
    /// </summary>
    public interface IComposite
    {
        void AddChild(IComposite child);
        string Print(int depth);
    }

    public interface IBuilder
    {
        void BuildBranch(string name);
        void BuildLeaf(string name, string content);
        void CloseBranch();
        IComposite GetDocument();
    }

    public interface IDirector
    {
        void BuildBranch();
        void BuildLeaf();
        void CloseBranch();
    }

    /// <summary>
    /// Director should be implemented for the Console client
    /// </summary>
    public class Director : IDirector
    {
        //private Branch branch;
        private IBuilder iBuilder;
        private static string branch;
        private static string[] leafArr = { "", "" };


        public Director(IBuilder builder)
        {
            //branch = new Branch();
            iBuilder = builder;
        }

        /// <summary>
        /// BuildBranch()
        /// </summary>
        public void BuildBranch()
        {
            iBuilder.BuildBranch(this.GetBranch());
        }

        /// <summary>
        /// BuildLeaf()
        /// </summary>
        public void BuildLeaf()
        {
            string[] leaf = this.GetLeaf();
            iBuilder.BuildLeaf(leaf[0], leaf[1]);
        }

        public void CloseBranch()
        {
            iBuilder.CloseBranch();
        }

        /// <summary>
        /// GetDocument returns the root node
        /// </summary>
        public void Print()
        {
            Console.WriteLine(iBuilder.GetDocument().Print(0));
        }

        /// <summary>
        /// Getter() and Setter() for Branch
        /// </summary>
        public string GetBranch() { return branch; }
        public void SetBranch(string[] props)
        {
            branch = props[1];
        }

        /// <summary>
        /// Getter() and Setter() for LKeaf
        /// </summary>
        public string[] GetLeaf() { return leafArr; }
        public void SetLeaf(string[] props)
        {
            leafArr[0] = props[1];
            leafArr[1] = props[2];
        }
    } // end class

    // JSONBuilder class
    public class JSONBuilder : IBuilder
    {
        /// <summary>
        /// an int for depth will add tabs to the beginning of each line that is printed.
        /// </summary>
        private int depth = 1;

        /// <summary>
        /// In-class we used a Queue or List to do this. I try to learn and use "LinkedList", not "Stack".
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
            //each tabbing is as same as example's by using .PadRight() function. 
            //string space = "\t";
            string space = "";
            //number_of_spaces_or_tabs * depth
            string tabs = space.PadRight(4 * depth);
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
                text += $"{tabs}\'{strKey}\' : \n{tabs}{{\n";
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
            return $"{tabs}\'{strKey}\' : \'{strValue}\'";
        }
        public void AddChild(IComposite child) { } // leaf do not have children
    } // end class

    // XMLBuilder class
    public class XMLBuilder : IBuilder
    {
        /// <summary>
        /// an int for depth will add tabs to the beginning of each line that is printed.
        /// </summary>
        private int depth = 0;

        /// <summary>
        /// In-class we used a Queue or List to do this. I try to learn and use "LinkedList", not "Stack".
        /// </summary>
        private LinkedList<IComposite> linked;

        /// <summary>
        /// declare root
        /// </summary>
        private XMLBranch root = new XMLBranch("root");

        /// <summary>
        /// On creation, should create a root Branch for the document
        /// </summary>
        public XMLBuilder()
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
            //each tabbing is as same as example's by using .PadRight() function. 
            //string space = "\t";
            string space = "";
            //number_of_spaces_or_tabs * depth
            string tabs = space.PadRight(4 * depth);
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
}