using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void TreeVisitor<T>(T rm);
public class TreeStructure<T>
{
        public T root;
        public LinkedList<TreeStructure<T>> children;
        public TreeStructure(T root)
        {
            this.root = root;
            children = new LinkedList<TreeStructure<T>>();
        }
        public void AddChild(T r)
        {
            children.AddFirst(new TreeStructure<T>(r));
        }

        public TreeStructure<T> GetChild(int i)
        {
            foreach (TreeStructure<T> n in children)
                if (--i == 0)
                    return n;
            return null;
        }

        public void Traverse(TreeStructure<T> node, TreeVisitor<T> visitor)
        {
            visitor(node.root);
            foreach (TreeStructure<T> kid in node.children)
                Traverse(kid, visitor);
        }

    


}
