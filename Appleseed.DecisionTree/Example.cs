using System;
using System.Collections.Generic;

namespace Appleseed.DecisionTree
{
    public class Example
    {
        /// <summary>
        /// What the this example is classified as
        /// </summary>
        public string classification { get; }

        /// <summary>
        /// Dictionary that maps attribute names to the values that
        /// they con    tain for the current example
        /// </summary>
        public Dictionary<int, object> attributes { get; }

        public Example(string classification)
        {
            this.classification = classification;
            attributes = new Dictionary<int, object>();
        }

        public void AddAttribute(int name, object value)
        {
            System.Diagnostics.Debug.WriteLineIf(attributes.ContainsKey(name), 
                "Warning: changing existing attribute");

            attributes.Add(name, value);
        }
    }
}