using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedConfig.Util
{
    /// <summary>
    /// Data structure of tree, a tree node can have multiple childs.
    /// </summary>
    /// <typeparam name="T">type of the Node value.</typeparam>
    public class TreeNode<T>
    {
        public TreeNode(T value) { this.Value = value; }

        private IList<TreeNode<T>> childs = new List<TreeNode<T>>();

        public TreeNode<T> AddChild(T newChildValue)
        {
            var newNode = new TreeNode<T>(newChildValue);
            this.childs.Add(newNode);
            return newNode;
        }

        public bool Remove(TreeNode<T> target)
        {
            return this.childs.Remove(target);
        }

        public T Value { get; set; }
        public IEnumerable<TreeNode<T>> Childs => this.childs;
    }
}
