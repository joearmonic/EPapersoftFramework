//-----------------------------------------------------------------------
// <copyright file="MapsSourceAttribute.cs" company="EPapersoft">
//     Copyright ® EPapersoft
// </copyright>
// <Historial>
//      <Entry Date="12/06/2015" Login="JLAlamo">Created</Entry>
// </Historial>
//-----------------------------------------------------------------------
namespace EPSFramework.DAL.EntityAnnotations
{
    using System;

    public class MapsSourceAttribute : Attribute
    {
        private string _dBname = null;

        public MapsSourceAttribute(string name)
        {
            this._dBname = name;
        }

        public string DBName
        {
            get { return _dBname; }
            set { _dBname = value; }
        }
    }
}