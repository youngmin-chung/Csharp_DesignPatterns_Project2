/*
 * Program:         INFO3137_Project2 (Document Builder Console Client)
 * Module:          Director.cs
 * Date:            July 17, 2019
 * Author:          Youngmin Chung
 * Description:                  
 */

using System;
using ClassLibrary;

namespace ConsoleClient
{
    /// <summary>
    /// Director should be implemented for the Console client
    /// </summary>
    public class Director : IDirector
    {
        private Branch branch;
        private IBuilder iBuilder;


        public Director(IBuilder builder)
        {
            branch = new Branch();
            iBuilder = builder;
        }

        /// <summary>
        /// BuildBranch()
        /// </summary>
        public void BuildBranch()
        {
            iBuilder.BuildBranch(branch.GetBranch());
        }

        /// <summary>
        /// BuildLeaf()
        /// </summary>
        public void BuildLeaf()
        {
            Leaf leaf = branch.GetLeaf();
            iBuilder.BuildLeaf(leaf.GetKey(), leaf.GetValue());
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
    } // end class

    public class Branch
    {
        private static string branch;
        private static Leaf leaf;

        /// <summary>
        /// Branches can have children, but no text content
        /// </summary>
        public Branch()
        {
            leaf = new Leaf();
        }

        /// <summary>
        /// Getter and Setter
        /// </summary>
        public String GetBranch() { return branch; }
        public void SetBranch(string[] props)
        {
            branch = props[1];
        }

        public Leaf GetLeaf() { return leaf; }
        public void SetLeaf(string[] props)
        {
            leaf = new Leaf(props[1], props[2]);
        }
    } // end class

    // Leaf class

    public class Leaf
    {
        private string key;
        private string value;

        public Leaf() {}

        public Leaf (string key, string value)
        {
            this.key = key;
            this.value = value;
        }

        public string GetKey() { return key; }
        public void SetKey(string[] key)
        {
            this.key = key[0];
        }

        public string GetValue() { return value; }
        public void SetValue(string[] value)
        {
            this.value = value[1];
        }
    }
}// end namespace