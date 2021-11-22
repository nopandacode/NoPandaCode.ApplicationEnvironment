using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NoPandaCode.ApplicationEnvironment
{
    /// <summary>
    /// The <see cref="ApplicationInfo"/> contains all information about a app.
    /// </summary>
    public class ApplicationInfo
    {
        private ApplicationInfo(string? name, string? description, List<Command> commands)
        {
            Name = name;
            Description = description;
            Commands = commands;
        }

        /// <summary>
        /// The name of the app.
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// The description of the app.
        /// </summary>
        public string? Description { get; set; }
        /// <summary>
        /// The commands of the app.
        /// </summary>
        public List<Command> Commands { get; set; }
        
        /// <summary>
        /// Create a <see cref="ApplicationInfo"/> from the app type.
        /// </summary>
        /// <typeparam name="TApp">The type of the app.</typeparam>
        /// <returns>A <see cref="ApplicationInfo"/>. Null if the app type has no <see cref="ApplicationAttribute"/>.</returns>
        public static ApplicationInfo? FromType<TApp>()
        {
            return FromType(typeof(TApp));
        }
        /// <summary>
        /// Create a <see cref="ApplicationInfo"/> from the app type.
        /// </summary>
        /// <param name="appType">The type of the app.</param>
        /// <returns>A <see cref="ApplicationInfo"/>. Null if the app type has no <see cref="ApplicationAttribute"/>.</returns>
        public static ApplicationInfo? FromType(Type appType)
        {
            string? name;
            string? description;
            List<Command> commands = new List<Command>();

            var typeInfo = appType.GetTypeInfo();

            ApplicationAttribute? appAttr = typeInfo.GetCustomAttribute<ApplicationAttribute>();
            if (appAttr == null) return null;

            name = appAttr.Name;
            description = appAttr.Description;

            var methods = typeInfo.GetMethods();
            foreach (var method in methods)
            {
                if (method == null) continue;

                Command? command = Command.FromMethodInfo(method);
                if (command == null) continue;

                commands.Add(command);
            }

            return new ApplicationInfo(name, description, commands);
        }
    }
}
