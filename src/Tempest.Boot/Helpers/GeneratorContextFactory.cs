using System;
using System.IO;
using Tempest.Core.Generator;
using Tempest.Core.Utils;

namespace Tempest.Boot.Helpers
{
    public class GeneratorContextFactory
    {
        public static GeneratorContext Create(Type generatorType)
        {
            return Create(generatorType, generatorType.GetAssembly().GetAssemblyDirectory().FullName);
        }

        public static GeneratorContext Create<T>() where T : IExecutableGenerator
        {
            return Create(typeof(T));
        }

        public static GeneratorContext Create<T>(string[] args) where T : IExecutableGenerator
        {
            var context = Create<T>();
            context.Arguments = args;
            return context;
        }

        public static GeneratorContext Create(Type generatorType, Action<GeneratorContext> action)
        {
            return Create(generatorType, Directory.GetCurrentDirectory(), action);
        }

        public static GeneratorContext Create<T>(Action<GeneratorContext> action) where T : IExecutableGenerator
        {
            return Create(typeof(T), action);
        }

        public static GeneratorContext Create(Type generatorType, string tempestDirectory)
        {
            return Create(generatorType, tempestDirectory, Directory.GetCurrentDirectory());
        }

        public static GeneratorContext Create<T>(string tempestDirectory) where T : IExecutableGenerator
        {
            return Create(typeof(T), tempestDirectory);
        }

        public static GeneratorContext Create(Type generatorType, string tempestDirectory,
            Action<GeneratorContext> action)
        {
            return Create(generatorType, tempestDirectory, Directory.GetCurrentDirectory(), action);
        }

        public static GeneratorContext Create<T>(string tempestDirectory, Action<GeneratorContext> action) where T : IExecutableGenerator
        {
            return Create(typeof(T), tempestDirectory, action);
        }

        public static GeneratorContext Create(Type generatorType, string tempestDirectory, string workingDirectory)
        {
            return Create(generatorType, tempestDirectory, workingDirectory, Path.Combine(tempestDirectory, "Template"));
        }

        public static GeneratorContext Create<T>(string tempestDirectory, string workingDirectory) where T : IExecutableGenerator
        {
            return Create(typeof(T), tempestDirectory, workingDirectory);
        }

        public static GeneratorContext Create(Type generatorType, string tempestDirectory,
            string workingDirectory, Action<GeneratorContext> action)
        {
            return Create(generatorType, tempestDirectory, workingDirectory,
                Path.Combine(tempestDirectory, "Template"), action);
        }

        public static GeneratorContext Create<T>(string tempestDirectory, string workingDirectory,
            Action<GeneratorContext> action) where T : IExecutableGenerator
        {
            return Create(typeof(T), tempestDirectory, workingDirectory, action);
        }

        public static GeneratorContext Create(Type generatorType, string tempestDirectory, string workingDirectory, string templateDirectory)
        {
            return Create(generatorType, new DirectoryInfo(tempestDirectory), new DirectoryInfo(workingDirectory),
                new DirectoryInfo(templateDirectory));
        }

        public static GeneratorContext Create(Type generatorType, DirectoryInfo tempestDirectory,
            DirectoryInfo workingDirectory, DirectoryInfo templateDirectory, params string[] arguments)
        {
            return new GeneratorContext()
            {
                GeneratorType = generatorType,
                TempestDirectory = tempestDirectory,
                WorkingDirectory = workingDirectory,
                TemplateDirectory = templateDirectory,
                Arguments = arguments
            };
        }

        public static GeneratorContext Create(Type generatorType, DirectoryInfo tempestDirectory,
            DirectoryInfo workingDirectory, DirectoryInfo templateDirectory, Action<GeneratorContext> action)
        {
            var context = Create(generatorType, tempestDirectory, workingDirectory, templateDirectory);
            action(context);
            return context;
        }

        public static GeneratorContext Create<T>(string tempestDirectory, string workingDirectory,
            string templateDirectory) where T : IExecutableGenerator
        {
            return Create(typeof(T), tempestDirectory, workingDirectory, templateDirectory);
        }

        public static GeneratorContext Create(Type generatorType, string tempestDirectory,
            string workingDirectory, string templateDirectory, Action<GeneratorContext> action)
        {
            var context = Create(generatorType, tempestDirectory, workingDirectory, templateDirectory);
            action(context);
            return context;
        }

        public static GeneratorContext Create<T>(string tempestDirectory, string workingDirectory,
            string templateDIrectory, Action<GeneratorContext> action) where T : IExecutableGenerator
        {
            return Create(typeof(T), tempestDirectory, workingDirectory, templateDIrectory, action);
        }
    }
}