using System;
using System.Linq;
using System.Reflection;
using HarmonyLib;

namespace ApacheTech.Common.Extensions.Harmony;

public static class HarmonyReflectionExtensions
{
    #region Fields

    /// <summary>
    ///     Gets a field within the calling instanced object. This can be an internal or private field within another assembly.
    /// </summary>
    /// <typeparam name="T">The type of field to return.</typeparam>
    /// <param name="instance">The instance in which the field resides.</param>
    /// <param name="fieldName">The name of the field to return.</param>
    /// <returns>An object containing the value of the field, reflected by this instance.</returns>
    public static T? GetField<T>(this object instance, string fieldName) 
        => (T?)AccessTools.Field(instance.GetType(), fieldName).GetValue(instance);

    /// <summary>
    ///     Gets an array of fields within the calling instanced object, of a specified Type. These can be an internal or private fields within another assembly.
    /// </summary>
    /// <typeparam name="T">The type of field to return.</typeparam>
    /// <param name="instance">The instance in which the field resides.</param>
    /// <returns>An array containing the values of the fields of a specified Type, reflected by this instance.</returns>
    public static T?[]? GetFields<T>(this object instance) 
        => AccessTools
            .GetDeclaredFields(instance.GetType())?
            .Where(t => t.FieldType == typeof(T))?
            .Select(val => instance.GetField<T>(val.Name))
            .ToArray();

    /// <summary>
    ///     Sets a field within the calling instanced object. This can be an internal or private field within another assembly.
    /// </summary>
    /// <param name="instance">The instance in which the field resides.</param>
    /// <param name="fieldName">The name of the field to set.</param>
    /// <param name="setVal">The value to set the field to.</param>
    public static void SetField(this object instance, string fieldName, object setVal) 
        => AccessTools.Field(instance.GetType(), fieldName).SetValue(instance, setVal);

    #endregion

    #region Properties

    /// <summary>
    ///     Gets a property within the calling instanced object. This can be an internal or private property within another assembly.
    /// </summary>
    /// <typeparam name="T">The type of property to return.</typeparam>
    /// <param name="instance">The instance in which the property resides.</param>
    /// <param name="propertyName">The name of the property to return.</param>
    /// <returns>An object containing the value of the property, reflected by this instance.</returns>
    public static T? GetProperty<T>(this object instance, string propertyName) 
        => (T?)AccessTools.Property(instance.GetType(), propertyName).GetValue(instance);

    /// <summary>
    ///     Sets a property within the calling instanced object. This can be an internal or private property within another assembly.
    /// </summary>
    /// <param name="instance">The instance in which the property resides.</param>
    /// <param name="propertyName">The name of the property to set.</param>
    /// <param name="setVal">The value to set the property to.</param>
    public static void SetProperty(this object instance, string propertyName, object setVal) 
        => AccessTools.Property(instance.GetType(), propertyName).SetValue(instance, setVal);

    /// <summary>
    ///     Gets a static property within the calling type. This can be an internal or private property within another assembly.
    /// </summary>
    /// <typeparam name="T">The type of property to return.</typeparam>
    /// <param name="type">The type in which the property resides.</param>
    /// <param name="propertyName">The name of the property to return.</param>
    /// <returns>An object containing the value of the property, reflected by this instance.</returns>
    public static T? GetStaticProperty<T>(this Type type, string propertyName) 
        => (T?)AccessTools.Property(type, propertyName).GetValue(null);

    /// <summary>
    ///     Sets a static property within the calling type. This can be an internal or private property within another assembly.
    /// </summary>
    /// <param name="type">The type in which the property resides.</param>
    /// <param name="propertyName">The name of the property to set.</param>
    /// <param name="setVal">The value to set the property to.</param>
    public static void SetStaticProperty(this Type type, string propertyName, object setVal) 
        => AccessTools.Property(type, propertyName).SetValue(null, setVal);

    /// <summary>
    ///     Gets an array of properties within the calling instanced object, of a specified type. These can be internal or private properties within another assembly.
    /// </summary>
    /// <typeparam name="T">The type of property to return.</typeparam>
    /// <param name="instance">The instance in which the property resides.</param>
    /// <returns>An array containing the values of the properties of a specified type, reflected by this instance.</returns>
    /// <remarks>
    ///     This method is useful for extracting all properties of a given type from an object, even if they are not public.
    /// </remarks>
    public static T?[]? GetProperties<T>(this object instance) 
        => [.. AccessTools
            .GetDeclaredProperties(instance.GetType())
            .Where(t => t.PropertyType == typeof(T))
            .Select(x => instance.GetProperty<T>(x.Name))];

    #endregion

    #region Methods

    /// <summary>
    ///     Calls a method within an instance of an object, via reflection. This can be an internal or private method within another assembly.
    /// </summary>
    /// <typeparam name="T">The return type, expected back from the method.</typeparam>
    /// <param name="instance">The instance to call the method from.</param>
    /// <param name="method">The name of the method to call.</param>
    /// <param name="args">The arguments to pass to the method.</param>
    /// <returns>The return value of the reflected method call.</returns>
    public static T? CallMethod<T>(this object instance, string method, params object[] args) 
        => (T?)AccessTools.Method(instance.GetType(), method).Invoke(instance, args);

    /// <summary>
    ///     Calls a method within an instance of an object, via reflection. This can be an internal or private method within another assembly.
    /// </summary>
    /// <param name="instance">The instance to call the method from.</param>
    /// <param name="method">The name of the method to call.</param>
    /// <param name="args">The arguments to pass to the method.</param>
    public static void CallMethod(this object instance, string method, params object[] args) 
        => AccessTools.Method(instance.GetType(), method)?.Invoke(instance, args);

    /// <summary>
    ///     Calls a method within an instance of an object, via reflection. This can be an internal or private method within another assembly.
    /// </summary>
    /// <param name="instance">The instance to call the method from.</param>
    /// <param name="method">The name of the method to call.</param>
    public static void CallMethod(this object instance, string method) 
        => AccessTools.Method(instance.GetType(), method)?.Invoke(instance, null);

    /// <summary>
    ///     Calls a static method within an object, via reflection. This can be an internal or private method within another assembly.
    /// </summary>
    /// <param name="instance">The instance to call the method from.</param>
    /// <param name="method">The name of the method to call.</param>
    public static void CallStaticMethod(this object instance, string method) 
        => AccessTools.Method(instance.GetType(), method)?.Invoke(null, null);

    /// <summary>
    ///     Calls a static method within a type, via reflection. This can be an internal or private method within another assembly.
    /// </summary>
    /// <param name="type">The type to call the method from.</param>
    /// <param name="method">The name of the method to call.</param>
    public static void CallStaticMethod(this Type type, string method) 
        => AccessTools.Method(type, method)?.Invoke(null, null);

    /// <summary>
    ///     Gets the <see cref="MethodInfo"/> for a method within an instance of a class, via reflection. This can be an internal or private method within another assembly.
    /// </summary>
    /// <param name="instance">The instance to get the method from.</param>
    /// <param name="method">The method to gather info about.</param>
    /// <param name="parameters">An itemised method signature, used to distinguish between overloads.</param>
    /// <param name="generics">An itemised array of generic parameters, used to distinguish between overloads.</param>
    /// <returns>Returns a <see cref="MethodInfo"/> for the specified method.</returns>
    public static MethodInfo GetMethod(this object instance, string method, Type[]? parameters = null, Type[]? generics = null) 
        => AccessTools.Method(instance.GetType(), method, parameters, generics);

    /// <summary>
    ///     Calls a base class method on an instance of an object via reflection.
    ///     This can be used to invoke protected or private base class methods within another assembly.
    /// </summary>
    /// <typeparam name="TBaseClass">The type of the base class.</typeparam>
    /// <param name="instance">The instance to call the base method from.</param>
    /// <param name="method">The name of the base method to call.</param>
    /// <param name="args">The arguments to pass to the method.</param>
    /// <remarks>
    ///     If the base type does not match <typeparamref name="TBaseClass"/>, the method will not be called.
    /// </remarks>
    /// <exception cref="MissingMethodException">Thrown if the base method cannot be found.</exception>
    public static void CallBaseMethod<TBaseClass>(this object instance, string method, params object[] args)
    {
        var baseType = instance.GetType().BaseType;
        if (baseType?.FullName != typeof(TBaseClass).FullName) return;
        AccessTools.Method(baseType, method)?.Invoke(instance, args);
    }

    /// <summary>
    ///     Calls a base class method on an instance of an object via reflection and returns its result.
    ///     This can be used to invoke protected or private base class methods within another assembly.
    /// </summary>
    /// <typeparam name="TBaseClass">The type of the base class.</typeparam>
    /// <typeparam name="TValue">The return type expected from the base method.</typeparam>
    /// <param name="instance">The instance to call the base method from.</param>
    /// <param name="method">The name of the base method to call.</param>
    /// <param name="args">The arguments to pass to the method.</param>
    /// <returns>The return value of the reflected base method call, or <c>default</c> if the base type does not match <typeparamref name="TBaseClass"/>.</returns>
    /// <remarks>
    ///     This method is useful for retrieving values from base class methods that are not accessible through normal means.
    /// </remarks>
    /// <exception cref="MissingMethodException">Thrown if the base method cannot be found.</exception>
    public static TValue? CallBaseMethod<TBaseClass, TValue>(this object instance, string method, params object[] args)
    {
        var baseType = instance.GetType().BaseType;
        if (baseType is not TBaseClass) return default;
        return (TValue?)AccessTools.Method(baseType, method)?.Invoke(instance, args);
    }

    #endregion

    #region Types

    /// <summary>
    ///     Creates the instance of a specified Type. Be aware that this will ignore all Service Providers, and attempt to directly instantiate a class.
    /// </summary>
    /// <param name="type">The type to create an instance of.</param>
    /// <returns>A new instance of the specified type.</returns>
    public static object CreateInstance(this Type type) 
        => AccessTools.CreateInstance(type);

    /// <summary>
    ///     Gets the type of the class within an assembly, via reflection.
    /// </summary>
    /// <param name="assembly">The assembly the class resides in.</param>
    /// <param name="className">The name of the class.</param>
    /// <returns>The <see cref="Type"/> of the class, if found within the assembly, otherwise, returns <c>null</c>.</returns>
    public static Type? GetClassType(this Assembly assembly, string className) 
        => AccessTools.GetTypesFromAssembly(assembly).FirstOrDefault(t => t.Name == className);

    #endregion

    #region Objects

    /// <summary>
    ///     Makes a deep copy of any object.
    /// </summary>
    /// <typeparam name="T">The type of the instance that should be created; for legacy reasons, this must be a class or interface.</typeparam>
    /// <param name="source">The original object.</param>
    /// <returns>A copy of the original object but of type <typeparamref name="T" /></returns>
    public static T DeepClone<T>(this T source) where T : class 
        => AccessTools.MakeDeepCopy<T>(source);


    /// <summary>
    ///     Makes a deep copy of any object.
    /// </summary>
    /// <typeparam name="T">The type of the instance that should be created; for legacy reasons, this must be a class or interface.</typeparam>
    /// <param name="source">The original object.</param>
    /// <returns>A copy of the original object but of type <typeparamref name="T" /></returns>
    public static void DeepClone<T>(this T source, out T copy) where T : class
        => AccessTools.MakeDeepCopy(source, out copy);
    
    #endregion
}
