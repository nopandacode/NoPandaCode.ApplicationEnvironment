using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoPandaCode.ApplicationEnvironment
{
    /// <summary>
    /// A type of command.
    /// </summary>
    public enum CommandType
    {
        /// <summary>
        /// The default command.
        /// </summary>
        DefaultCommand,
        /// <summary>
        /// The normal command.
        /// </summary>
        NormalCommand
    }

    /// <summary>
    /// The <see cref="CommandAttribute"/> allows you to register a command to a app.
    /// </summary>
    public class CommandAttribute : Attribute
    {
        /// <summary>
        /// The description of the command.
        /// </summary>
        public string? Description { get; set; }
        /// <summary>
        /// The name of the command. Leave it null to take the name from the method.
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// The type of the command.
        /// </summary>
        public CommandType Type { get; set; } = CommandType.NormalCommand;
    }
}
