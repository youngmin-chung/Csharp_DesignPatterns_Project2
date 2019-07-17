using System;

namespace INFO3137_Project2
{
    public class Director : IDirector
    {
        /// <summary>
        /// method
        /// </summary>
        private Component comp;
        private IBuilder iBuilder;

        /// <summary>
        /// C'tor 1 arg
        /// </summary>
        public Director(IBuilder builder)
        {
            comp = new Component();
            iBuilder = builder;
        }

        public void BuildBranch()
        {
            iBuilder.BuildBranch(comp.GetBranch());
        }
        public void BuildLeaf()
        {
            Leaf<string, string> leaf = comp.GetLeaf();
            iBuilder.BuildLeaf(leaf.GetKey(), leaf.GetValue());
        }
        public void CloseBranch()
        {
            iBuilder.CloseBranch();
        }

        public void Print()
        {
            Console.WriteLine(iBuilder.GetDocument().Print(0));
        }
    } // end class

    public class Component
    {
        /// <summary>
        /// method
        /// </summary>
        private static string branch;
        private static Leaf<string, string> leaf;

        /// <summary>
        /// C'tor no-arg
        /// </summary>
        public Component()
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
        /// default c'tor
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
