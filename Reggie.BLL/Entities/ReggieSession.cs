using System;

namespace Reggie.BLL.Entities
{
    [Serializable]
    public class ReggieSession
    {
        /// <summary>
        /// Gets or sets the sample text with which to test the pattern.
        /// </summary>
        public string SampleText { get; set; }

        /// <summary>
        /// Gets or sets the regular expression pattern that is being tested.
        /// </summary>
        public string RegularExpressionPattern { get; set; }
    }
}
