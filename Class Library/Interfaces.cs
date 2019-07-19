/*
 * Program:         INFO3137_Project2 (Document Builder Console Client)
 * Module:          Interfaces.cs
 * Date:            July 17, 2019
 * Author:          Youngmin Chung
 * Description:                  
 */

namespace INFO3137_Project2
{
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
}