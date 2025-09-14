# Harmony Reflection Extensions

This package provides a set of extension methods that make advanced reflection in C# simple and elegant, especially for accessing `internal` and `private` members. These methods are designed to work at the object level, allowing you to get, set, and invoke fields, properties, and methods—even those not normally accessible—using the power of [Harmony](https://github.com/pardeike/Harmony)'s `AccessTools`.

**What is Harmony?**  
[Harmony](https://github.com/pardeike/Harmony) is a popular library for patching, replacing, and modifying .NET methods at runtime. This extension package leverages Harmony's robust reflection utilities to provide a more convenient API for everyday use.

**Compatibility:**  
- .NET 8 or later
- Requires `Lib.Harmony` (not included)

**Quick Example:**
```csharp
using ApacheTech.Common.Extensions.Harmony;

// Access a private field
var value = myObject.GetField<int>("_privateField");

// Set a private property
myObject.SetProperty("InternalProperty", 42);

// Call a private method
myObject.CallMethod("HiddenMethod");
```

All that is required is that `Lib.Harmony` should be included within your project. This package does not supply Harmony. Once installed, add the following using statement to your class file.

```csharp
using ApacheTech.Common.Extensions.Harmony;
```

## Support the Author

If you find this library useful, and you would like to show appreciation for the work I produce; please consider supporting me, and my work, using one of the methods below. Every single expression of support is most appreciated, and makes it easier to produce updates, and new features for my libraries, moving fowards. Thank you.

 - [Join my Patreon!](https://www.patreon.com/ApacheTechSolutions?fan_landing=true)
 - [Donate via PayPal](http://bitly.com/APGDonate)
 - [Buy Me a Coffee](https://www.buymeacoffee.com/Apache)
 - [Subscribe on Twitch.TV](https://twitch.tv/ApacheGamingUK)
 - [Subscribe on YouTube](https://youtube.com/c/ApacheGamingUK)
 - [Purchase from my Amazon Wishlist](http://amzn.eu/7qvKTFu)
 - [Visit my website!](https://apachegaming.net)

## Extension Methods

The following tables list all extension methods provided by this library, grouped by category. Each entry includes the method signature and a brief description of its functionality.

## Fields

| Method | Description |
| --- | --- |
| `T? GetField<T>(this object instance, string fieldName)` | Gets a field within the calling instanced object. This can be an internal or private field within another assembly. |
| `T?[]? GetFields<T>(this object instance)` | Gets an array of fields within the calling instanced object, of a specified type. These can be internal or private fields within another assembly. |
| `void SetField(this object instance, string fieldName, object setVal)` | Sets a field within the calling instanced object. This can be an internal or private field within another assembly. |

## Properties

| Method | Description |
| --- | --- |
| `T? GetProperty<T>(this object instance, string propertyName)` | Gets a property within the calling instanced object. This can be an internal or private property within another assembly. |
| `void SetProperty(this object instance, string propertyName, object setVal)` | Sets a property within the calling instanced object. This can be an internal or private property within another assembly. |
| `T? GetStaticProperty<T>(this Type type, string propertyName)` | Gets a static property within the calling type. This can be an internal or private property within another assembly. |
| `void SetStaticProperty(this Type type, string propertyName, object setVal)` | Sets a static property within the calling type. This can be an internal or private property within another assembly. |
| `T?[]? GetProperties<T>(this object instance)` | Gets an array of properties within the calling instanced object, of a specified type. These can be internal or private properties within another assembly. |

## Methods

| Method | Description |
| --- | --- |
| `T? CallMethod<T>(this object instance, string method, params object[] args)` | Calls a method within an instance of an object, via reflection, and returns a value. This can be an internal or private method within another assembly. |
| `void CallMethod(this object instance, string method, params object[] args)` | Calls a method within an instance of an object, via reflection. This can be an internal or private method within another assembly. |
| `void CallMethod(this object instance, string method)` | Calls a method with no arguments within an instance of an object, via reflection. |
| `void CallStaticMethod(this object instance, string method)` | Calls a static method within an object, via reflection. |
| `void CallStaticMethod(this Type type, string method)` | Calls a static method within a type, via reflection. |
| `MethodInfo GetMethod(this object instance, string method, Type[]? parameters = null, Type[]? generics = null)` | Gets the `MethodInfo` for a method within an instance of a class, via reflection. |
| `void CallBaseMethod<TBaseClass>(this object instance, string method, params object[] args)` | Calls a base class method on an instance of an object via reflection. |
| `TValue? CallBaseMethod<TBaseClass, TValue>(this object instance, string method, params object[] args)` | Calls a base class method on an instance of an object via reflection and returns its result. |

## Types

| Method | Description |
| --- | --- |
| `object CreateInstance(this Type type)` | Creates the instance of a specified Type, using Harmony AccessTools. Be aware that this will ignore all Service Providers, and attempt to directly instantiate a class. |
| `Type? GetClassType(this Assembly assembly, string className)` | Gets the type of the class within an assembly, via reflection. |

## Objects

| Method | Description |
| --- | --- |
| `T DeepClone<T>(this T source) where T : class` | Makes a deep copy of any object. |
| `void DeepClone<T>(this T source, out T copy) where T : class` | Makes a deep copy of any object and outputs the copy via an out parameter. |