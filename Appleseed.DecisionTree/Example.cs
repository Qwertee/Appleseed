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
        /// they contain for the current example
        /// </summary>
        public Dictionary<string, object> attributes { get; }

        public Example(string classification)
        {
            this.classification = classification;
            attributes = new Dictionary<string, object>();
        }

        void AddAttribute(string name, object value)
        {
            System.Diagnostics.Debug.WriteLineIf(attributes.ContainsKey(name), 
                "Warning: changing existing attribute");

            attributes.Add(name, value);
        }
    }
}