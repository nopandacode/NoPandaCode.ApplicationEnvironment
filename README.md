# NoPandaCode.ApplicationEnvironment
This is a package with some application helpers.

* [AppRunner](#AppRunner)

## AppRunner
The AppRunner allows you to run specific apps created by yourself. An app is just a class with the Application Attribute. In your app you can create commands with, optionally, flags and options.
A flag is just an argument and a option is almost the same but they can contain a value. (For example.: <b>Flag:</b> "hello_this_is_a_flag", <b>Option:</b> "yourname=paul")

### Create an app
Just I said, an is a class with an Application Attribute.
```cs
[Application(Name = "TestApplication")] // <--- The Attribute
public class MyApplication
{
    // ...
}
```

### Create a command
A command is a method with a CommandAttrbute. If you not set the name, it will be automaticly take the name from the method.
```cs
[Command(Name = "testcommand")]
public void Test()
{
    // ...
}
```
#### Add Description
It can be helpful if you add a description to your command. For example, you're creating a console application with some off these apps. It might be useful that the user can see what the command is doing.
```cs
[Command(Name = "testcommand", Description = "This command does nothing.")]
public void Test()
{
    // ...
}
```
#### Default Command
If you want to only execute the app and not a command you can create a "default command". Just set the Type to CommandType.DefaultCommand.
```cs
[Command(Type = CommandType.DefaultCommand)]
public void ThisIsDefault()
{
    // ...
}
```
#### Parameters (Flags, Options)
Just I said, you can add flags and options to your command. Just add a string List and a string Dictionary to your method parameters.
```cs
[Command(Name = "testcommand", Description = "This command does nothing.")]
public void Test(List<string> flags, Dictionary<string, string> options)
{
    // ...
}
```
If you only want to get all arguments, then just add a string array.
```cs
[Command(Name = "testcommand", Description = "This command does nothing.")]
public void Test(string[] args)
{
    // ...
}
```

