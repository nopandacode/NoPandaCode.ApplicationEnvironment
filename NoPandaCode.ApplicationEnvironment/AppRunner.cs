using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoPandaCode.ApplicationEnvironment
{
    /// <summary>
    /// The <see cref="AppRunner"/> can run applications based on attributes.
    /// </summary>
    public static class AppRunner
    {
        /// <summary>
        /// The <see cref="AppRunnerResult"/> is a enum that contains all return codes when you ran a application.
        /// </summary>
        public enum AppRunnerResult
        {
            /// <summary>
            /// The app was successfully executed.
            /// </summary>
            SuccessfullyExecuted = 1,
            /// <summary>
            /// The app type is null.
            /// </summary>
            AppIsNull = 2,
            /// <summary>
            /// The instance of the app is null.
            /// </summary>
            InstanceIsNull = 4,
            /// <summary>
            /// The command was not found and the app has no default command.
            /// </summary>
            CommandNotFound = 8,
            /// <summary>
            /// Either the parameter type is incorrect or you sent too much/less arguments.
            /// </summary>
            ParameterError = 16,
            /// <summary>
            /// There was an error at the execution.
            /// </summary>
            ExecutionError = 32,
            /// <summary>
            /// Contains all null-errors.
            /// </summary>
            NullError = AppIsNull | InstanceIsNull,
            /// <summary>
            /// Contains all errors.
            /// </summary>
            Error = NullError | CommandNotFound | ParameterError | ExecutionError
        }

        /// <summary>
        /// Run a application with args.
        /// </summary>
        /// <typeparam name="TApp">The app type</typeparam>
        /// <param name="command">The command that you want to execute. Leave it null to run the default command.</param>
        /// <param name="args">The arguments for the command.</param>
        /// <returns>A <see cref="AppRunnerResult"/></returns>
        public static AppRunnerResult Run<TApp>(string? command, string?[]? args = null)
        {
            return Run(typeof(TApp), command, args);
        }
        /// <summary>
        /// Run a application with args.
        /// </summary>
        /// <param name="appType">The app type</param>
        /// <param name="command">The command that you want to execute. Leave it null to run the default command.</param>
        /// <param name="args">The arguments for the command.</param>
        /// <returns>A <see cref="AppRunnerResult"/></returns>
        public static AppRunnerResult Run(Type appType, string? command, string?[]? args = null)
        {
            ApplicationInfo? appInfo = ApplicationInfo.FromType(appType);
            if (appInfo == null) return AppRunnerResult.AppIsNull;

            Command? foundCommand = null;
            if (command == null)
            {
                foundCommand = appInfo.Commands.Where(cmd => cmd.Type == CommandType.DefaultCommand).FirstOrDefault();
            }
            else
            {
                foundCommand = appInfo.Commands.Where(cmd => cmd.Name == command.ToLower()).FirstOrDefault();
            }
            if (foundCommand == null) return AppRunnerResult.CommandNotFound;

            object? instance = Activator.CreateInstance(appType);
            if (instance == null) return AppRunnerResult.InstanceIsNull;

            if (foundCommand.HasParameters != null && foundCommand.HasParameters == true && args != null)
            {
                // Parameters
                
                // More Parameters
                if (foundCommand.ParameterLength == 2)
                {
                    List<string> flags = new List<string>();
                    Dictionary<string, string> options = new Dictionary<string, string>();

                    foreach (var arg in args)
                    {
                        if (arg == null) continue;
                        if (!arg.Contains('='))
                        {
                            flags.Add(arg);
                            continue;
                        }
                        string[] parts = arg.Split('=');
                        if (parts.Length != 2) flags.Add(arg);
                        options.Add(parts[0], parts[1]);
                    }

                    try
                    {
                        foundCommand.Run(instance, new object[] { flags, options });
                        return AppRunnerResult.SuccessfullyExecuted;
                    }
                    catch { return AppRunnerResult.ExecutionError | AppRunnerResult.ParameterError; }
                }
                else
                {
                    try
                    {
                        foundCommand.Run(instance, new[] { args });
                        return AppRunnerResult.SuccessfullyExecuted;
                    }
                    catch { return AppRunnerResult.ExecutionError | AppRunnerResult.ParameterError; }
                }
            }
            else
            {
                // No Parameters
                try
                {
                    foundCommand.Run(instance, null);
                    return AppRunnerResult.SuccessfullyExecuted;
                }
                catch { return AppRunnerResult.ExecutionError | AppRunnerResult.ParameterError; }
            }
        }
    }
}
