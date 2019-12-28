using System;
using System.Linq;
using System.Reflection;

namespace DSharpExtensions.Internal
{
    internal static class ReflectionUtility
    {
        internal static object GetObjectFieldByType<TObj, TField>(object obj)
        {
            //I know this is horrid

            FieldInfo[] fields = typeof(TObj).GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);

            return fields.FirstOrDefault(f => f.FieldType == typeof(TField)).GetValue(obj);
        }
    }
}
