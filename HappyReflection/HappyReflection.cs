using HappyReflection.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace HappyReflection
{
    public static class HappyReflection
    {
        public static HappyReflectionConfiguration GlobalConfiguration { get; set; } = new HappyReflectionConfiguration();

        public static List<Assembly> AllAssemblies { get; private set; } = new List<Assembly>();
        public static List<Type> AllTypes { get; private set; } = new List<Type>();

        public static string AssemblyDirectory
        {
            get
            {
                var assymblyLocation = Assembly.GetExecutingAssembly().Location;
                return Path.GetDirectoryName(assymblyLocation);
            }
        }

        /// <summary>
        /// Initializes HappyReflection and searches for available Assemblies. 
        /// </summary>
        private static void Initialize()
        {
            if (GlobalConfiguration is null)
            {
                throw new MissingMemberException(nameof(GlobalConfiguration));
            }

            if (AllAssemblies.Any())
            {
                return;
            }

            var allDllsPaths = Directory.GetFiles(AssemblyDirectory, "*.dll", GlobalConfiguration.AssemblySearchOption);

            var includedDllsPaths = allDllsPaths.Select(dllInAppDir =>
            Path.GetFileName(dllInAppDir)).Where(dllFileName => !GlobalConfiguration.ExcludedNamespaces.Any(y => dllFileName.ToLowerInvariant().StartsWith(y.ToLowerInvariant())));

            foreach (var dllPath in includedDllsPaths)
            {
                // wen need to load all assemblies, so we can search in the current AppDomain for the type.
                Assembly.Load(dllPath.Replace(".dll", ""));
            }

            AllAssemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
            AllTypes = AllAssemblies.SelectMany(x => x.GetTypes()).ToList();
        }

        /// <summary>
        /// Searches for all classes with an attribute of type <typeparamref name="TAttributeType">.
        ///
        /// <example>
        ///
        /// Code:
        /// <code>
        ///     [ILoveChocolateAttribute]
        ///     public class ChocolateFabric : IChocolateFabric {
        ///
        ///     }
        /// </code>
        ///
        /// HappyReflection implementation:
        /// <code>
        ///     HappyReflection.GetClassesByAttribute<ILoveChocolateAttribute>();
        ///
        ///     // returns list contatining one element
        ///     new <see cref="TypeWithInteface"/>() {
        ///         Interfaces = new List<Type> { typeof(IChocolateFabric) },
        ///         Type = typeof(ChocolateFabric)
        ///     }
        /// </code>
        /// </example>
        /// </summary>
        /// <typeparam name="TAttributeType">A list of all Types annotated with TAttributeType. If nothing was found an empty list will be returned.</typeparam>
        /// <returns>A list of <see cref="TypeWithInteface"/> containing a type and the interfaces implemented by the type. If nothing was found an empty list will be returned.</returns>
        public static IList<HappyReflectionTypeWithInterface> GetClassesWithInterfacesByAttribute<TAttributeType>() => GetClassesWithInterfacesByAttribute(typeof(TAttributeType));
        public static IList<HappyReflectionTypeWithInterface> GetClassesWithInterfacesByAttribute(Type attributeType)
        {
            Initialize();

            return GetClassesByAttribute(attributeType).Select(classType => new HappyReflectionTypeWithInterface
            {
                Interfaces = classType.GetInterfaces(),
                Type = classType
            }).ToList();
        }

        /// <summary>
        /// Searches for all classes with an attribute of type <typeparamref name="TAttributeType">.
        ///
        /// <example>
        ///
        /// Code:
        /// <code>
        ///     [ILoveChocolateAttribute]
        ///     public class ChocolateFabric {
        ///
        ///     }
        /// </code>
        ///
        /// HappyReflection implementation:
        /// <code>
        ///     HappyReflection.GetClassesByAttribute<ILoveChocolateAttribute>(); // returns list contatining type "ChocolateFabric"
        /// </code>
        /// </example>
        /// </summary>
        /// <exception cref="MissingMemberException">
        /// Thrown when GlobalConfiguration is null.
        /// </exception>
        /// <returns>A list of all Types annotated with TAttributeType. If nothing was found an empty list will be returned.</returns>
        public static IList<Type> GetClassesByAttribute<TAttributeType>() => GetClassesByAttribute(typeof(TAttributeType));
        public static IList<Type> GetClassesByAttribute(Type attributeType)
        {
            if (attributeType is null)
            {
                throw new ArgumentNullException(nameof(attributeType));
            }

            Initialize();

            return AllTypes.Where(type =>
                type.IsClass &&
                type.IsPublic &&
                type.IsDefined(attributeType, false))
                .ToList();
        }

        /// <summary>
        /// Returns all classes that implement the interface <typeparamref name="TInterfaceType">.
        ///
        /// <example>
        ///
        /// Code:
        /// <code>
        ///     public class ChocolateFabric : IChocolateFabric {
        ///
        ///     }
        /// </code>
        ///
        /// HappyReflection implementation:
        /// <code>
        ///     HappyReflection.GetClassesByInterface<IChocolateFabric>(); // returns list contatining type "ChocolateFabric"
        /// </code>
        /// </example>
        /// </summary>
        /// <typeparam name="TInterfaceType">The interface to search for.</typeparam>
        /// <exception cref="MissingMemberException">
        /// Thrown when GlobalConfiguration is null.
        /// </exception>
        /// <returns>A list of classes (Type) that implement the interface. If nothing was found an empty list will be returned.</returns>
        public static IList<Type> GetClassesByInterface<TInterfaceType>() => GetClassesByInterface(typeof(TInterfaceType));

        public static IList<Type> GetClassesByInterface(Type interfaceType)
        {
            if (interfaceType is null)
            {
                throw new ArgumentNullException(nameof(interfaceType));
            }

            Initialize();

            return AllTypes.Where(type =>
                type.IsClass &&
                type.IsPublic &&
                type.GetInterfaces().Any(interf => interf == interfaceType))
                .ToList();
        }

        /// <summary>
        /// Returns a type matching the search term <param name="typeName">.
        /// upper/lower case is ignored.
        /// <example>
        ///
        /// Code:
        /// <code>
        ///     public class ChocolateFabric {
        ///
        ///     }
        /// </code>
        ///
        /// HappyReflection implementation:
        /// <code>
        ///     HappyReflection.GetTypeByName("chocolatefabric"); // returns the type "ChocolateFabric"
        /// </code>
        /// </example>
        /// </summary>
        /// <param name="typeName">The search term.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when typeName is null or whitespace.
        /// </exception>
        /// <exception cref="MissingMemberException">
        /// Thrown when GlobalConfiguration is null.
        /// </exception>
        /// <returns>Returns a Type. If the type was not found null will be returned. If multiple results are found, the
        /// first one will be returned (FirstOrDefault()).</returns>
        public static Type GetTypeByName(string typeName)
        {
            if (string.IsNullOrWhiteSpace(typeName))
            {
                throw new ArgumentNullException(nameof(typeName));
            }

            Initialize();

            return AllTypes.FirstOrDefault(type =>
                type.IsClass &&
                !type.IsAbstract &&
                type.IsPublic &&
                type.FullName.ToLowerInvariant().EndsWith($".{typeName.ToLowerInvariant()}"));
        }
    }
}
