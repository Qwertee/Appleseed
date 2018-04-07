using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appleseed.DecisionTree
{
    public class ClassifyOutput
    {
        /// <summary>
        /// How many of the decisions that were made were randomized
        /// </summary>
        public double randomnessRatio { get; set; }

        public string classification { get; set; }



        public ClassifyOutput(string classification, double r = -1)
        {
            this.classification = classification;
            randomnessRatio = r;
        }

        public override string ToString()
        {
            return classification;
        }
    }
}
