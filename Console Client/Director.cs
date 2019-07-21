/*
 * Program:         INFO3137_Project2 (Document Builder Console Client)
 * Module:          Director.cs
 * Date:            July 17, 2019
 * Author:          Youngmin Chung
 * Description:                  
 */

using System;

namespace INFO3137_Project2
{
    /// <summary>
    /// Director should be implemented for the Console client
    /// </summary>
    public class Director : IDirector
    {
        private Branch branch;
        private IBuilder iBuilder;

        /// <summary>
        /// C'tor 1 arg
        /// </summary>
        public Director(IBuilder builder)
        {
            branch = new Branch();
            iBuilder = builder;
        }
        
        public void BuildBranch()
        {
            iBuilder.BuildBranch(branch.GetBranch());
        }

        public void BuildLeaf()
        {
            Leaf<string, string> leaf = branch.GetLeaf();
            iBuilder.BuildLeaf(leaf.GetKey(), leaf.GetValue());
        }

        public void CloseBranch()
        {
            iBuilder.CloseBranch();
        }

        /// <summary>
        /// Printing a branchosite should return a string representing its full content
        /// </summary>
        public void Print()
        {
            Console.WriteLine(iBuilder.GetDocument().Print(0));
        }
    } // end class

    public class Branch
    {
        private static string branch;
        private static Leaf<string, string> leaf;

        /// <summary>
        /// Branches can have children, but no text content
        /// </summary>
        public Branch()
        {
            leaf = new Leaf<string, string>();
        }

        /// <summary>
        /// Getter and Setter
        /// </summary>
        public String GetBranch() { return branch; }
        public void SetBranch(string[] props)
        {
            branch = props[1];
        }

        public Leaf<string, string> GetLeaf() { return leaf; }
        public void SetLeaf(string[] props)
        {
            leaf = new Leaf<string, string>(props[1], props[2]);
        }
    } // end class

    // Leaf class
    public class Leaf<TKey, TValue>
    {
        /// <summary>
        /// method
        /// </summary>
        private TKey tKey { get; set; }
        private TValue tValue { get; set; }

        /// <summary>
        /// Leaves can have text content, but no children
        /// </summary>
        public Leaf() { }

        public Leaf(TKey key, TValue val)
        {
            tKey = key;
            tValue = val;
        }

        /// <summary>
        /// Getter and Setter
        /// </summary>
        public TKey GetKey() { return tKey; }
        public void SetKey(TKey key) { tKey = key; }

        public TValue GetValue() { return tValue; }
        public void SetValue(TValue value) { tValue = value; }
    } // end class
}