using System;
using System.Collections.Generic;


public sealed class ServiceLocator
{
    // A series of globally accessible objects. The Type is the key and the object is the value, meaning there can only be one object of a given type in _services.
    private readonly static Dictionary<Type, object> _services = new Dictionary<Type, object>();

    // Sets the value of key <T> to service.
    public static void Register<T>(T service) where T : IService
    {
        _services[typeof(T)] = service;
    }

    // Gets the service of type <T>.
    public static T Get<T>()
    {
        return (T)_services[typeof(T)];
    }
}
