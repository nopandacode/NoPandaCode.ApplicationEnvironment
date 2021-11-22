using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NoPandaCode.ApplicationEnvironment
{
    /// <summary>
    /// A <see cref="Command"/> helper class.
    /// </summary>
    public class Command
    {
        private Command(string? name, string? description, CommandType type, MethodInfo? methodInfo)
        {
            Name = name;
            Description = description;
            Type = type;
            
            this.methodInfo = methodInfo;
        }

        /// <summary>
        /// The name of the command.
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// The description of the command.
        /// </summary>
        public string? Description { get; set; }
        /// <summary>
        /// The type of the command.
        /// </summary>
        public CommandType Type { get; set; }

        /// <summary>
        /// Returns true if the command has parameters.
        /// </summary>
        public bool? HasParameters
        {
            get => ParameterLength > 0;
        }
        /// <summary>
        /// Returns the amount of parameters of the command.
        /// </summary>
        public int? ParameterLength
        {
            get
            {
                if (methodInfo == null) return null;
                return methodInfo.GetParameters().Length;
            }
        }

        private MethodInfo? methodInfo;

        /// <summary>
        /// Invoke the command with parameters.
        /// </summary>
        /// <param name="instance">A instance from the app.</param>
        /// <param name="parameters">The parameters to the command. Leave it null to send no parameters.</param>
        public void Run(object instance, object?[]? parameters)
        {
            if (methodInfo == null) return;
            methodInfo.Invoke(instance, parameters);
        }

        /// <summary>
        /// Create a <see cref="Command"/> from a <see cref="MethodInfo"/>.
        /// </summary>
        /// <param name="methodInfo">The <see cref="MethodInfo"/> from the command.</param>
        /// <returns>Returns the command. Null if the method has no <see cref="CommandAttribute"/>.</returns>
        public static Command? FromMethodInfo(MethodInfo methodInfo)
        {
            string? name = "";
            string? description = "";
            CommandType type;

            var attr = methodInfo.GetCustomAttribute<CommandAttribute>();
            if (attr == null) return null;

            if (attr.Name == null) name = methodInfo.Name.ToLower();
            else name = attr.Name.ToLower();
            description = attr.Description;
            type = attr.Type;

            return new Command(name, description, type, methodInfo);
        }
    }
}
