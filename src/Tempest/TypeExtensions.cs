using System;
using System.Reflection;

namespace Tempest
{
    public static class TypeExtensions
    {
        public static bool IsConcrete(this Type type) => !type.GetTypeInfo().IsAbstract;
        public static bool IsSubclassOf(this Type type, Type from) => type.GetTypeInfo().IsSubclassOf(from);
    }
}