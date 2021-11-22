using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoPandaCode.ApplicationEnvironment
{
    /// <summary>
    /// The <see cref="ApplicationAttribute"/> allows you to mark your type as a app.
    /// </summary>
    public class ApplicationAttribute : Attribute
    {
        /// <summary>
        /// The name of the application.
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// The description of the application.
        /// </summary>
        public string? Description { get; set; }
    }
}
