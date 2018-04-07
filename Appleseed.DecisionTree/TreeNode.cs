using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appleseed.DecisionTree
{
    class TreeNode
    {
        /// <summary>
        /// The classification of the node if it is terminal.
        /// The majority classification of the children if it isn't
        /// </summary>
        public string classification;

        /// <summary>
        /// The attribute that this node splits on
        /// </summary>
        public int attribute;

        /// <summary>
        /// value that lead to this node
        /// </summary>
        public object value;

        /// <summary>
        /// A list of children of this node
        /// </summary>
        public List<TreeNode> children;

        /// <summary>
        /// Whether the current node terminal.
        /// A terminal node has no children.
        /// </summary>
        public bool terminal;

        public TreeNode(string classification, int attribute, object value, bool terminal)
        {
            this.classification = classification;
            this.attribute = attribute;
            this.terminal = terminal;
            this.value = value;

            // if the node is a terminal one, dont need children
            if (terminal)
            {
                children = null;
            }
            else
            {
                children = new List<TreeNode>();
            }
        }

        public void AddChild(TreeNode childNode)
        {
            if (children != null)
            {
                children.Add(childNode);
            }

            else
            {
                System.Diagnostics.Debug.WriteLine("Warning: trying to add child to terminal node");
            }
        }
    }
}
