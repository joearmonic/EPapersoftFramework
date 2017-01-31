using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using EPSFramework.DAL;
using EPSFramework.DAL.Core;
using EPSFramework.DAL.Core.SA;
using EPSFramework.DAL.Core.SqlServer;
using EPSFramework.DAL.Mappings.Sample;
using EPSFramework.Entities.Sample;

namespace EPapersoftFramework.DAL.TestConsole
{
    internal class Program
    {
        private static bool isFinished = false;
        private static int _mode;

        private static IRepository<Client> clientRepo = null;
        private static TransactionKeeper tranKeeper = null;
        private static IDataProviderFactory dataFactory;
        private static TableMappingFactory saMappingFactory;

        private static void Main(string[] args)
        {
            WriteColoredLine(
                Assembly.GetExecutingAssembly().GetName().Name + " " + Assembly.GetExecutingAssembly().GetName().Version, 
                ConsoleColor.Gray);
            
            ITransactionService trans = null;

            try
            {
                WriteColoredLine("Initializing app...", ConsoleColor.DarkMagenta);
                InitializeRepositories();
                trans = EstablishMode();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Excepción al inicializar: ");
                Console.WriteLine(ex.Message);
                Console.WriteLine("Pulse una tecla para salir de la aplicación");
                Console.ReadLine();
                return;
            }

            WriteColoredLine("Repos initialized. App Started", ConsoleColor.Green);
            Console.WriteLine(Environment.NewLine);

            // The app goes cycling until "exit" command is done.
            RunCycles(trans);

            if (_mode == 2)
            {
                trans.UnRegisterRepositories();
                trans = null;
            }

            WriteColoredLine("End of execution (press any key to exit)!", ConsoleColor.Yellow);
            Console.ReadKey();
        }

        private static void RunCycles(ITransactionService trans)
        {
            while (!isFinished)
            {
                var command = MainMenu();
                if (_mode == 2 && tranKeeper == null || tranKeeper != null && tranKeeper.Disposed)
                {
                    tranKeeper = trans.BeginConnection();
                }

                try
                {
                    ExecuteCommand(command);
                }
                catch (Exception ex)
                {
                    WriteColoredLine($"Error executing: {ex.Message + ex?.InnerException.Message}", ConsoleColor.Red);
                }
            }
        }

        private static ITransactionService EstablishMode()
        {
            ITransactionService trans = null;

            _mode = DecideTransactionMode();
            if (_mode == 2)
            {
                trans = dataFactory.CreateTransactionService(saMappingFactory);
                clientRepo = trans.RegisterRepository<Client>(clientRepo);
            }

            return trans;
        }

        private static void InitializeRepositories()
        {
            /* Repo only needs
             * a connection string
             * and the full qualified name from where to load the factory for the db engine (could be by config).
             */
            dataFactory = new SAProviderFactory(ConfigurationManager.ConnectionStrings["SA"].ConnectionString);
            saMappingFactory = new SAMappingFactory();
            clientRepo = new ADONetRepository<Client>
           (
               dataFactory
               , saMappingFactory
            );
        }

        private static int DecideTransactionMode()
        {
            ConsoleKeyInfo commandKey;
            while (true)
            {
                WriteColoredLine("1. Simple execution.", ConsoleColor.DarkYellow);
                WriteColoredLine("2. Transaction execution.", ConsoleColor.DarkYellow);
                WriteColoredLine("q or e to abort execution.", ConsoleColor.DarkGray);
                commandKey = Console.ReadKey(true);
                switch (commandKey.Key)
                {
                    case ConsoleKey.Q:
                    case ConsoleKey.E:
                        throw new Exception("The execution has been aborted.");

                    case ConsoleKey.NumPad1:
                    case ConsoleKey.D1:
                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        return (int)commandKey.Key;

                    default:
                        WriteColoredLine("No such command exists! Only 1, 2, q or e are allowed.", ConsoleColor.Yellow);
                        break;
                }
            }
        }
    
        private static void ExecuteCommand(string command)
        {
            int id = 0;

            if (command.ToUpper().Equals("EXIT") || command.ToUpper().Equals("E"))
            {
                isFinished = true;
                if (_mode == 2)
                {
                    tranKeeper.Commit();
                    tranKeeper.Dispose();
                }
            }
            else if (command.ToUpper().StartsWith("GETALL"))
            {
                var cmdWithArgs = command.Split(' ');
                if (cmdWithArgs.Length > 1)
                {
                    if (cmdWithArgs[1].Equals(clientRepo.GetType().GetGenericArguments()[0].Name))
                    {
                        var entities = SelectEntities(clientRepo);
                        foreach (var entity in entities)
                        {
                            RenderEntityDef(entity, "Id");
                        }
                    }
                }
            }

            else if (Int32.TryParse(command, out id))
            {
                try
                {
                    switch (id)
                    {
                        case 1:
                            EntityOption(clientRepo);
                            break;
                        default:
                            throw new Exception("The option choosen is not available");
                    }
                }
                catch (DALException dalEx)
                {
                    if (_mode == 2)
                    {
                        if (tranKeeper != null)
                        {
                            tranKeeper.Rollback();
                            tranKeeper.Dispose();
                        }
                    }

                    WriteColoredLine(dalEx.Message, ConsoleColor.Red);
                    if (dalEx.InnerException != null)
                    {
                        WriteColoredLine(dalEx.InnerException.Message, ConsoleColor.Magenta);
                    }

                    Console.WriteLine(Environment.NewLine);
                }
                catch (Exception ex)
                {
                    WriteColoredLine(ex.Message, ConsoleColor.Red);
                    if (ex.InnerException != null)
                    {
                        WriteColoredLine(ex.InnerException.Message, ConsoleColor.Magenta);
                    }

                    Console.WriteLine(Environment.NewLine);
                }
            }
            else
            {
                Console.WriteLine("Wrong command!!");
                Console.WriteLine(Environment.NewLine);
            }
        }

        private static void EntityOption<T>(IRepository<T> repo) where T : class
        {
            Console.WriteLine(Environment.NewLine);

            if (repo == null)
            {
                throw new Exception("Repository not initialized. Cannot execute queries.");
            }

            int id = 0;
            T entity = default(T);
            string command = CRUDMenu();
            Int32.TryParse(command, out id);
            switch (id)
            {
                case 1:
                    entity = SelectEntity(repo, ref command);

                    RenderEntity(entity);
                    WriteColoredLine(String.Format("Rendered {0} at {1}", entity.GetType().Name, DateTime.Now), ConsoleColor.Green);
                    break;

                case 2:
                    WriteColoredLine("Entity to create:", ConsoleColor.Magenta);
                    entity = CreateEntity<T>().Fill(ReadInputProperties(typeof(T)));
                    int def = Convert.ToInt32(repo.Insert(entity));

                    WriteColoredLine(String.Format("{0} inserted!", entity.GetType().Name), ConsoleColor.Green);
                    WriteColoredLine(String.Format("Id {0}", def), ConsoleColor.Green);
                    break;

                case 3:
                    entity = SelectEntity(repo, ref command);
                    if (entity == null)
                    {
                        WriteColoredLine(string.Format("Cannot find entity with def {0}", command), ConsoleColor.Yellow);
                        break;
                    }

                    // WriteColoredLine(String.Format("Trying to update authority from {0} --> 4",
                    //   newDwell.AuthorityRef), ConsoleColor.Blue);
                    // newDwell.AuthorityRef = 4;
                    bool updated = repo.Update(entity);
                    if (updated)
                    {
                        WriteColoredLine("Updated entity successfully", ConsoleColor.Green);
                    }
                    else
                    {
                        WriteColoredLine("Update entity not acomplished, however there's no error in the operation", ConsoleColor.Yellow);
                    }

                    break;

                case 4:
                    entity = SelectEntity(repo, ref command);
                    if (entity == null)
                    {
                        WriteColoredLine(string.Format("Cannot find entity with def {0}", command), ConsoleColor.Yellow);
                        break;
                    }

                    WriteColoredLine(String.Format("Trying to delete entity {0}", entity.GetType().Name), ConsoleColor.Blue);

                    bool deleted = repo.Delete(entity);
                    if (deleted)
                    {
                        WriteColoredLine("Deleted entity successfully", ConsoleColor.Green);
                    }
                    else
                    {
                        WriteColoredLine("delete entity not acomplished, however the operation doesn't return an error.", ConsoleColor.Yellow);
                    }

                    break;

                default:
                    throw new Exception(String.Format("No option available for {0}", id));
            }
        }

        private static Dictionary<string, object> ReadInputProperties(Type typeOfInstance)
        {
            Dictionary<string, object> inputProperties = new Dictionary<string, object>();
            bool hasFinished = false;

            while (!hasFinished)
            {
                Console.WriteLine();
                WriteColoredLine("Write the property in the following format: 'Name:Value'(? for Help):", ConsoleColor.DarkCyan);
                var command = Console.ReadLine();
                switch (command.ToLower())
                {
                    case "f":
                        hasFinished = true;
                        break;

                    case "?":
                    case "h":
                    case "help":
                        WriteColoredLine("These are the properties that the current entity has:", ConsoleColor.Magenta);
                        Console.Write(typeOfInstance.GetProperties());
                        break;

                    default:
                        var cmpsCommand = command.Split(':');
                        if (typeOfInstance.GetProperties().Any(p => p.Name.Equals(cmpsCommand[0])))
                        {
                            inputProperties.Add(cmpsCommand[0], cmpsCommand[1]);
                        }
                        else
                        {
                            WriteColoredLine("Property invalid, bad format or command not recognized. Try again or 'f' to exit this submenu", ConsoleColor.Yellow);
                        }

                        break;
                }
            }

            return inputProperties;
        }

        private static string CRUDMenu()
        {
            Console.WriteLine("Choose which operation you want to test");
            Console.WriteLine("1. Select");
            Console.WriteLine("2. Insert");
            Console.WriteLine("3. Update");
            Console.WriteLine("4. Delete");
            Console.WriteLine("b. Back to previous menu.");

            string command = Console.ReadLine();
            return command;
        }

        private static string MainMenu()
        {
            WriteColoredLine("write the command with the required args:");
            WriteColoredLine("Entities to operate:", ConsoleColor.Green);
            WriteColoredLine(">> Client", ConsoleColor.Gray);
            WriteColoredLine(">> Order", ConsoleColor.Gray);
            WriteColoredLine(">> Product", ConsoleColor.Gray);
            Console.Write(Environment.NewLine);
            WriteColoredLine(" GET {EntityName} Id", ConsoleColor.DarkYellow);
            WriteColoredLine(" GETALL {EntityName} ('GETALL Dwelling')", ConsoleColor.DarkYellow);
            WriteColoredLine(" (E)xit. To finish the application", ConsoleColor.DarkYellow);
            Console.WriteLine(Environment.NewLine);
            Console.Write("> ");
            var command = Console.ReadLine();
            return command;
        }

        private static void WriteColoredLine(string text, ConsoleColor color = ConsoleColor.White)
        {
            if (color != ConsoleColor.White) Console.ForegroundColor = color;
            Console.WriteLine(text);
            if (color != ConsoleColor.White) Console.ResetColor();
        }

        private static T SelectEntity<T>(IRepository<T> repo, ref string command) where T : class
        {
            int id = 0;
            T newDwell = default(T);

            Console.WriteLine("ENTITY's PRIMARY ID to operate with?");
            command = Console.ReadLine();
            if (Int32.TryParse(command, out id))
            {
                Console.WriteLine("Obtaining ENTITY at " + DateTime.Now);
                newDwell = repo.Get(id);
                Console.WriteLine("Obtained ENTITY at " + DateTime.Now);
            }
            else
            {
                Console.WriteLine("There's no ENTITY with id " + command);
                Console.WriteLine(Environment.NewLine);
            }

            return newDwell;
        }

        private static IList<T> SelectEntities<T>(IRepository<T> repo) where T : class
        {
            WriteColoredLine("Obtaining ENTITIES at " + DateTime.Now, ConsoleColor.DarkGray);
            var entities = repo.GetAll();
            WriteColoredLine("Obtained ENTITIES at " + DateTime.Now, ConsoleColor.DarkGray);

            return entities;
        }

        private static void RenderEntity<T>(T res) where T : class
        {
            if (res == null)
            {
                throw new Exception("There's no entity to render");
            }

            Console.WriteLine(Environment.NewLine);
            System.Threading.Thread.Sleep(75);

            foreach (var prop in res.GetType().GetProperties())
            {
                WriteColoredLine(prop.Name + ": " + prop.GetValue(res, null), ConsoleColor.Green);
                System.Threading.Thread.Sleep(50);
            }
        }

        private static void RenderEntityDef<T>(T res, string defName) where T : class
        {
            if (res == null)
            {
                throw new Exception("There's no entity to render");
            }

            Console.WriteLine(Environment.NewLine);
            System.Threading.Thread.Sleep(20);
            WriteColoredLine(defName + ":" + res.GetType().GetProperty(defName).GetValue(res, null).ToString(), ConsoleColor.Green);
        }

        private static T CreateEntity<T>()
        {
            return Activator.CreateInstance<T>();
        }
    }

    internal static class EntityExtensions
    {
        internal static T Fill<T>(this T instance, Dictionary<string, object> properties)
        {
            switch (instance.GetType().Name)
            {
                case "Client":
                    instance.GetType().GetProperties().ToList().ForEach(
                        p =>
                        {
                            if (properties.Any(pr => pr.Key == p.Name))
                            {
                                p.SetValue(instance, Convert.ChangeType(properties[p.Name], p.PropertyType), null);
                            }
                        });
                    break;

                default:
                    throw new Exception(String.Format("Fill extension method for {0} is not implemented yet", instance.GetType().Name));
            }

            return instance;
        }
    }
}