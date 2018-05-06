using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Garage
{
    public class Tree<T> where T : class
    {
        public Tree<T> Parent { get; set; }
        public List<Tree<T>> childrens;

        public Tree()
        {
            childrens = new List<Tree<T>>();
        }

        public Tree<T> GetRoot() {
            Tree<T> ptr = this;
            while (ptr.Parent != null)
                ptr = ptr.Parent;
            return ptr;
        }

        public bool IsRoot() {
            return Parent == null;
        }

        public bool IsLeaf()
        {
            return childrens.Count == 0;
        }

        public void AddChild(Tree<T> child) {
            child.Parent = this;
            childrens.Add(child);
        }
    }
}
