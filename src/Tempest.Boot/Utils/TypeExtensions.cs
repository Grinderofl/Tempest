using System;
using System.Linq;
using System.Reflection;

namespace Tempest.Boot.Utils
{
    public static class TypeExtensions
    {
        public static bool IsConcrete(this Type type) => !type.GetTypeInfo().IsAbstract;
        public static bool IsSubclassOf(this Type type, Type from) => type.GetTypeInfo().IsSubclassOf(from);
        public static bool Implements(this Type type, Type @interface) => type.GetTypeInfo().ImplementedInterfaces.Contains(@interface);
    }
}