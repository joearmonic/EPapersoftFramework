using EnvDTE;
using Microsoft.VisualStudio.TemplateWizard;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Reflection;
using System.Windows.Forms;
using EnvProject = EnvDTE.Project;

namespace EPapersoftWindowsServiceTemplateWizard
{
    public class ProjectsWizard : IWizard
    {
        private DTE _env;
        Dictionary<string, string> _replacementsDictionary;
        IWizard wizard = null;
        bool isDependentWizardRunning = false;

        public void BeforeOpeningFile(EnvDTE.ProjectItem projectItem)
        {
        }

        public void ProjectFinishedGenerating(EnvDTE.Project project)
        {
            if(isDependentWizardRunning)
                wizard.ProjectFinishedGenerating(project);
        }

        public void ProjectItemFinishedGenerating(EnvDTE.ProjectItem projectItem)
        {
            // _envProject = projectItem.ContainingProject;
        }

        public void RunFinished()
        {
            if (isDependentWizardRunning)
                wizard.RunFinished();
        }

        public void RunStarted(object automationObject,
            Dictionary<string, string> replacementsDictionary,
            WizardRunKind runKind,
            object[] customParams)
        {
            _replacementsDictionary = replacementsDictionary;
            _replacementsDictionary.Add("$fileinputname$", "");
            _env = automationObject as DTE;
            if (_replacementsDictionary.ContainsKey("$loadnugetwizard$") && Convert.ToBoolean(_replacementsDictionary["$loadnugetwizard$"]) == true)
            {
                isDependentWizardRunning = true;
                Assembly asm = Assembly.Load("NuGet.VisualStudio.Interop, Version=1.0.0.0, Culture=Neutral, PublicKeyToken=b03f5f7f11d50a3a");
                wizard = (IWizard)asm.CreateInstance("NuGet.VisualStudio.TemplateWizard");
                wizard.RunStarted(automationObject, replacementsDictionary, runKind, customParams);
            }
        }

        public bool ShouldAddProjectItem(string filePath)
        {
            _replacementsDictionary["$fileinputname$"] = filePath;
            return true;
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
    }
}