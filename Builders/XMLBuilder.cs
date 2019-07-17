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
        private Stack<IComposite> stack;
        private XMLComponent root;

        /// <summary>
        /// C'tor no-arg
        /// </summary>
        public XMLBuilder()
        {
            stack = new Stack<IComposite>();
            root = new XMLComponent("root");
            stack.Push(root);
        }
        public void BuildBranch(string name)
        {
            XMLComponent component = new XMLComponent(name);
            depth++;

            stack.Peek().AddChild(component);
            stack.Push(component);
        }
        public void BuildLeaf(string name, string content)
        {
            XMLLeaf leaf = new XMLLeaf(name, content);
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

    // XMLComponent class
    public class XMLComponent : IComposite
    {
        /// <summary>
        /// method
        /// </summary>
        private string strKey;
        private List<IComposite> children;

        /// <summary>
        /// C'tor 1 arg
        /// </summary>
        public XMLComponent(string key)
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
            //tab (\t) takes bigger space than example console's. So, I each tabbing is as same as example's by using .PadRight() function. 
            //string space = "\t";
            string space = "";
            string tabs = String.Concat(Enumerable.Repeat(space.PadRight(4), depth));

            return $"{tabs}<{strKey}>{strValue}</{strKey}>";
        }
        public void AddChild(IComposite child) { } // leaf do not have children
    } // end class
}// end namespace