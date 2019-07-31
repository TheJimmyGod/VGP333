using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class ServiceLocator
{
    static private readonly Dictionary<System.Type, object> _systems = new Dictionary<System.Type, object>();
    static public T Register<T>(object target)
    {
        if(_systems.ContainsKey(typeof(T)))
        {
            Debug.Log("There is already a system of type: " + typeof(T) + " that exists");
        }
        else
        {
            Debug.Log("Register " + typeof(T));
            _systems.Add(typeof(T), target);
        }
        return (T)target;
    }

    static public T Get<T>()
    {
        object ret = null;
        if(_systems.TryGetValue(typeof(T), out ret))
        {
            return (T)ret;
        }
        Debug.Log("No System of type " + typeof(T) + " was found");
        return (T)ret;
    }
}
