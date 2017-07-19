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
namespace EPapersoftWindowsServiceTemplateWizard
{
    using EnvDTE;
    using EnvDTE80;
    using EPapersoft.Framework.VisualStudioEnvironment;
    using Microsoft.VisualStudio.TemplateWizard;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Xml.Linq;
    using VSLangProj;

    public class ProjectsWizard : IWizard
    {
        private static Dictionary<string, string> _parentTemplates = new Dictionary<string, string>();

        private DTE _dte;

        private Solution2 _sln;

        private Dictionary<string, string> _replacementsDictionary;

        private IWizard _dependentWizard = null;

        private bool _isDependentWizardRunning = false;

        private IList<Tuple<string, string>> _childrenTemplates;

        private DTEAssistant _dteAssistant;

        private bool _isRootTemplate;

        private string _currentTemplate;

        public void BeforeOpeningFile(EnvDTE.ProjectItem projectItem)
        {
        }

        public void ProjectFinishedGenerating(EnvDTE.Project project)
        {
            if (_isDependentWizardRunning)
                _dependentWizard.ProjectFinishedGenerating(project);

            if (this._isRootTemplate)
            {
                foreach (Tuple<string, string> childTemplate in _childrenTemplates)
                {
                    string destDir = _replacementsDictionary["$destinationdirectory$"] + childTemplate.Item2;
                    string projName = _replacementsDictionary["$projectname$"] + childTemplate.Item2;
                    _sln.AddFromTemplate(childTemplate.Item1, destDir, projName, false);
                }

                VSProject startProj = _dteAssistant.FindVSProject(_replacementsDictionary["$projectname$"], _dte);
                _sln.SolutionBuild.StartupProjects = startProj.Project.UniqueName;
                foreach (Configuration cfg in startProj.Project.ConfigurationManager)
                {
                    if (cfg.ConfigurationName.Equals("Debug", StringComparison.OrdinalIgnoreCase))
                        cfg.Properties.Item("StartArguments").Value = "-c";
                }
            }
        }

        public void ProjectItemFinishedGenerating(EnvDTE.ProjectItem projectItem)
        {
        }

        public void RunFinished()
        {
            if (_isDependentWizardRunning)
                _dependentWizard.RunFinished();
        }

        public void RunStarted(object automationObject,
            Dictionary<string, string> replacementsDictionary,
            WizardRunKind runKind,
            object[] customParams)
        {
            _dte = automationObject as DTE;
            _sln = this._dte.Solution as Solution2;
            _dteAssistant = new DTEAssistant(_dte);
            _replacementsDictionary = replacementsDictionary;
            _replacementsDictionary.Add("$fileinputname$", "");

            foreach (var parentTemplate in _parentTemplates)
            {

                _replacementsDictionary.Add(parentTemplate.Key, parentTemplate.Value);
            }

            // Out the box, customparams in 0 index contains current template path.
            _currentTemplate = Path.GetFileName(customParams[0].ToString());
            _isRootTemplate = false;
            bool mustLoadDependentWizard = false;
            string nugetWizardData = null;

            if (_replacementsDictionary.ContainsKey("$loadnugetwizard$"))
            {
                mustLoadDependentWizard = Convert.ToBoolean(_replacementsDictionary["$loadnugetwizard$"]);
            }

            if (_replacementsDictionary.ContainsKey("$wizarddata$"))
            {
                XDocument xdoc = XDocument.Load(customParams[0].ToString());

                var dependentsNode = xdoc.Root.Elements()
                    .Where(el => el.Name.LocalName == "WizardData")
                    .Descendants().Where(el => el.Name.LocalName == "templates")
                    .FirstOrDefault();

                if (dependentsNode != null)
                {
                    _isRootTemplate = true;
                    if (_parentTemplates.ContainsKey("$ext_safeprojectname$"))
                    {
                        _parentTemplates["$ext_safeprojectname$"] = _replacementsDictionary["$safeprojectname$"];
                    }
                    else
                    {
                        _parentTemplates.Add("$ext_safeprojectname$", _replacementsDictionary["$safeprojectname$"]);
                    }

                    string templateZipPath = Path.GetDirectoryName(Path.GetDirectoryName(customParams[0].ToString()));

                    if (_childrenTemplates == null)
                    {
                        _childrenTemplates = new List<Tuple<string, string>>();
                    }

                    foreach (XElement dependentElement in dependentsNode.Elements())
                    {
                        String templatePath = Path.Combine(templateZipPath, $"{dependentElement.Value}.zip", $"{dependentElement.Value}.vstemplate");

                        _childrenTemplates.Add(new Tuple<string, string>(templatePath, dependentElement.Attribute("suffixToAdd").Value));
                    }
                }
            }


            if (mustLoadDependentWizard)
            {
                LoadDependentWizard(automationObject, runKind, customParams, nugetWizardData);
            }
        }

        private void LoadDependentWizard(object automationObject, WizardRunKind runKind, object[] customParams, string dependentWizardData = null)
        {
            Dictionary<string, string> replacementsDependentDictionary = _replacementsDictionary.Select(kvp => kvp).ToDictionary(kvp => kvp.Key, kvp2 => kvp2.Value);
            _isDependentWizardRunning = true;
            Assembly asm = Assembly.Load("NuGet.VisualStudio.Interop, Version=1.0.0.0, Culture=Neutral, PublicKeyToken=b03f5f7f11d50a3a");
            _dependentWizard = (IWizard)asm.CreateInstance("NuGet.VisualStudio.TemplateWizard");

            if (dependentWizardData != null)
            {
                replacementsDependentDictionary["$wizarddata$"] = dependentWizardData;
            }

            _dependentWizard.RunStarted(automationObject, replacementsDependentDictionary, runKind, customParams);
        }

        public bool ShouldAddProjectItem(string filePath)
        {
            _replacementsDictionary["$fileinputname$"] = filePath;
            return true;
        }
    }
}