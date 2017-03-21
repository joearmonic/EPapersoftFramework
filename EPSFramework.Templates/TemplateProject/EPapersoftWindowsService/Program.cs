//----------------------------------------------------------------------------------------------
// <copyright file="$fileinputname$" company="EPapersoft">
// Copyright(C) $year$ EPapersoft
// </copyright>
//<license project=$projectname$>
//  This program is free software: you can redistribute it and/or modify it under the terms of 
//  the GNU General Public License as published by the Free Software Foundation, either version 
//  3 of the License, or (at your option) any later version.
//  This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; 
//  without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
//  See the LICENSE file in the project root for more information or
//  visit, see<http://www.gnu.org/licenses/>.
//</license>
//----------------------------------------------------------------------------------------------
[assembly: log4net.Config.XmlConfigurator(ConfigFileExtension = "log4net", Watch = true)]
namespace $safeprojectname$
{
    using System;
    using System.ComponentModel.Composition.Hosting;
    using System.ServiceProcess;
    using IoC;
    using log4net;

    internal static class Program
    {
        /// <summary>
        /// The container of parts to compose by MEF
        /// </summary>
        private static CompositionContainer container;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        private static void Main(String[] args = null)
        {            
            ILog rootLogger = LogManager.GetLogger("Root");
            // Initializer IOC container with main app class and external sources.
            container = new ContainerBootstrap(typeof(EPapersoftService)).GetContainer();

            rootLogger.InfoFormat("Starting Application {0} v{1}", getAppName(), getAppVersion());
            rootLogger.Info($"{System.Reflection.Assembly.GetExecutingAssembly().CodeBase}");
            if (args.Length > 0 && args[0] == "-c")
            {
                // Run as a console
                EPapersoftService service = container.GetExportedValue<ServiceBase>() as EPapersoftService;
                Console.Clear();
                Console.Title = String.Format("{0} {1} {2} v{3}", getAppName(), getCompany(), DateTime.Today.Year, getAppVersion());
                Console.WriteLine("*************************************************************************");
                Console.WriteLine("{0} {1} {2} v{3}", getAppName(), getCompany(), DateTime.Today.Year, getAppVersion());
                Console.WriteLine("*************************************************************************");
                
                try
                {
                    service.Start(null);
                }
                catch (Exception ex)
                {
                    rootLogger.Error(ex);
                }

                Console.ForegroundColor = ConsoleColor.DarkCyan;
                System.Console.WriteLine(">>> Press enter to stop the service...");
                ConsoleKeyInfo key;
                do
                {
                    key = System.Console.ReadKey();
                } while (key.Key != ConsoleKey.Enter);

                Console.Write(Environment.NewLine);
                service.Stop();
                Console.Write(Environment.NewLine);
                Console.WriteLine(">>> Disposing resources");
                service.Disposed += Service_Disposed;
                service.Dispose();
                while (container != null)
                {
                    System.Threading.Thread.Sleep(5000);
                    Console.Write(".");
                }

                Console.WriteLine("{0} >>>>> End (press any key to exit) <<<<<", Environment.NewLine);
                Console.ReadKey();
            }
            else
            {
                EPapersoftService service = container.GetExportedValue<ServiceBase>() as EPapersoftService;
                service.Disposed += Service_Disposed;
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[]
                {
                    service
                };

                ServiceBase.Run(ServicesToRun);
            }
        }

        /// <summary>
        /// Gets the application version.
        /// </summary>
        /// <returns></returns>
        private static string getAppVersion()
        {
            return System.Reflection.Assembly.GetCallingAssembly().GetName().Version.ToString();
        }

        /// <summary>
        /// Gets the company name.
        /// </summary>
        /// <returns></returns>
        private static string getCompany()
        {
            object[] attribs = System.Reflection.Assembly.GetCallingAssembly().GetCustomAttributes(typeof(System.Reflection.AssemblyCompanyAttribute), true);
            if (attribs.Length > 0)
            {
                return ((System.Reflection.AssemblyCompanyAttribute)attribs[0]).Company;
            }

            return "No company defined";
        }

        /// <summary>
        /// Gets the name of the application.
        /// </summary>
        /// <returns></returns>
        private static string getAppName()
        {
            return System.Reflection.Assembly.GetCallingAssembly().GetName().Name;
        }

        /// <summary>
        /// Handles the Disposed event of the Service component.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private static void Service_Disposed(object sender, EventArgs e)
        {
            container.Dispose();
            container = null;
        }
    }
}