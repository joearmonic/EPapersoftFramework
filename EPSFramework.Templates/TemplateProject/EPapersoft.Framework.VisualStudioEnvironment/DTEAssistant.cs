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
using System;
using System.Collections.Generic;
using System.Text;
using EnvDTE;
using VSLangProj;
using System.IO;

namespace EPapersoft.Framework.VisualStudioEnvironment
{
    class DTEAssistant
    {
        DTE _env;
        internal DTEAssistant(DTE env)
        {
            this._env = env;
        }


        void Apply(string projectName)
        {
            //    //XmlDocument doc = new XmlDocument();
            //    //var CorrectProjectName = "";
            //    //var FullyPathedProjectFileName = "";
            //    // We want to unload the project before saving our changes,
            //    // so the user does not get a message asking if he wants to reload it.
            //    // We will reload programmatically after the save.
            //    EnvDTE80.DTE2 dte = (EnvDTE80.DTE2)System.Runtime.InteropServices.Marshal.GetActiveObject("VisualStudio.DTE.12.0");
            string solutionName = Path.GetFileNameWithoutExtension(_env.Solution.FullName);
            //    var solExplorer = dte.ToolWindows.SolutionExplorer; // INSTANCIA SOL EXPLORER.
            //    solExplorer.Parent.Activate(); // SELECCIONA EL PADRE. o se la solución.
            //    //solExplorer.SelectDown(vsUISelectionType.vsUISelectionTypeSelect, 1); // seleccionan

            //    //solExplorer.GetItem(solutionName + @"\" + projectName).Select(vsUISelectionType.vsUISelectionTypeSelect);
            //    dte.ToolWindows.SolutionExplorer.GetItem(solutionName + @"\" + "TunstallWindowsService").Select(vsUISelectionType.vsUISelectionTypeSelect);
            //    dte.ExecuteCommand("Project.UnloadProject");
            //    //doc.Save(FullyPathedProjectFileName);
            //    dte.ExecuteCommand("Project.ReloadProject");

            EnvDTE.Project _envProject = (EnvDTE.Project)_env.ActiveSolutionProjects;
            System.Windows.Forms.MessageBox.Show(solutionName + @"\" + projectName);
            if (_envProject != null)
            {
                var projectFileName = _envProject.FileName;
                projectName = _envProject.Name;
                UnloadProject(projectName);
                // FixCsprojFile(projectFileName, _templateFileName);
                ReloadProject(projectName);
            }

            //var envTemplateFileItem = _env.Solution.FindProjectItem(_templateFileName);
            //if (envTemplateFileItem != null)
            //{
            //    envTemplateFileItem.Open();
            //    envTemplateFileItem.Document.Activate();
            //}
        }

        private void ReloadProject(string projectName)
        {
            SelectProject(projectName);
            _env.ExecuteCommand("Project.ReloadProject");
        }

        private void UnloadProject(string projectName)
        {
            SelectProject(projectName);

            _env.ExecuteCommand("File.SaveAll");
            _env.ExecuteCommand("Project.UnloadProject");
        }

        private void SelectProject(string projectName)
        {
            var solutionName = _env.Solution.Properties.Item("Name").Value.ToString();
            var projectPath = Path.Combine(solutionName, projectName);
            var solutionExplorer = _env.Windows.Item(Constants.vsWindowKindSolutionExplorer);
            solutionExplorer.Activate();
            var solutionHierarchy = (UIHierarchy)solutionExplorer.Object;
            var projectItem = solutionHierarchy.GetItem(projectPath);
            projectItem.Select(vsUISelectionType.vsUISelectionTypeSelect);
        }

        public VSProject FindVSProject(string projName, DTE _dte)
        {
            foreach (Project p in _dte.Solution.Projects)
            {
                if (p.Name.Equals(projName, StringComparison.CurrentCultureIgnoreCase))
                {
                    return p.Object as VSProject;
                }
            }
            return null;
        }
    }
}