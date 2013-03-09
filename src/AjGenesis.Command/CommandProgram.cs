using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AjGenesis.Transformers.GenericTransformer;
using System.IO;
using AjGenesis.Models.DynamicModel;

namespace AjGenesis.Command
{
    class CommandProgram
    {
        private static TopEnvironment environment = new TopEnvironment();
        private static string repositoryPath;

        static void Main(string[] args)
        {
            repositoryPath = System.Environment.GetEnvironmentVariable("AJG_REPOSITORY");

            if (repositoryPath == null)
                repositoryPath = ".";

            environment["RepositoryPath"] = repositoryPath;

            LoadModels(".");

            foreach (string arg in args)
            {
                if (arg.Contains('='))
                    ProcessAssignArgument(arg);
                else if (arg.EndsWith(".ajg"))
                    ProcessTask(arg);
            }
        }

        static void ProcessAssignArgument(string argument)
        {
            string[] words = argument.Split('=');
            environment[words[0]] = words[1];
        }

        static void ProcessTask(string taskfilename)
        {
            string filename = Path.Combine(repositoryPath, taskfilename);

            if (!File.Exists(filename))
            {
                System.Console.Error.WriteLine(string.Format("File '{0}' not found", filename));
                System.Environment.Exit(1);
            }

            FileUtilities.PushFilePath(filename);

            try
            {
                StreamReader reader = new StreamReader(filename);
                TextCompiler compiler = new TextCompiler(reader);
                Program program = compiler.Compile();
                program.Output = System.Console.Out;
                program.LogOutput = System.Console.Out;
                reader.Close();
                program.Execute(environment);
            }
            finally
            {
                FileUtilities.PopFilePath();
            }
        }

        static void LoadModels(string directoryName)
        {
            string modelPath = Path.Combine(directoryName, "Models");
            if (Directory.Exists(modelPath))
                LoadModelsFromDirectory(new DirectoryInfo(modelPath));

            if (!Directory.Exists(directoryName))
                return;

            var dinfo = new DirectoryInfo(directoryName);

            if (dinfo.Parent != null)
                LoadModels(dinfo.Parent.FullName);
        }

        static void LoadModelsFromDirectory(DirectoryInfo dinfo)
        {
            foreach (var finfo in dinfo.GetFiles("*.xml"))
            {
                string name = finfo.Name.Substring(0, finfo.Name.Length - 4);
                if (environment[name] != null)
                    continue;
                System.Console.WriteLine("Loading Model from " + finfo.FullName);
                var builder = new ObjectXmlBuilder();
                var obj = builder.GetObject(finfo.FullName);
                environment[name] = obj;
            }

            foreach (var finfo in dinfo.GetFiles("*.txt"))
            {
                string name = finfo.Name.Substring(0, finfo.Name.Length - 4);
                if (environment[name] != null)
                    continue;
                System.Console.WriteLine("Loading Model from " + finfo.FullName);
                var builder = new ObjectTextBuilder();
                var obj = builder.GetObject(finfo.FullName);
                environment[name] = obj;
            }
        }
    }
}
