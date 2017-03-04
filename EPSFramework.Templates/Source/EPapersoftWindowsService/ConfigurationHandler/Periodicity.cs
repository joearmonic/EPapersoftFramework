//-----------------------------------------------------------------------
// <copyright file="$fileinputname$.cs" company="$registeredorganization$">
//     Copyright ® $registeredorganization$
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace TunstallWindowsService.Infraestructure
{
    public class Periodicity
    {
        private static Periodicity instance;

        private List<String> loadedPeriodicities;

        private List<String> definedPeriodicities;
        
        private Periodicity()
        {
            loadedPeriodicities = new List<string>();
            definedPeriodicities = new List<string>();
        }

        internal string GetPeriodicity(string periodicityCandidateValue)
        {
            if (!definedPeriodicities.Contains(periodicityCandidateValue))
                throw new IndexOutOfRangeException($"The value {periodicityCandidateValue} is not a defined value for the programmings periodicity.");

            return periodicityCandidateValue;
        }

        public static Periodicity Create()
        {
            if(instance == null)
            {
                instance = new Periodicity();
            }

            return instance;
        }

        internal void SetPeriodicity(String periodicity)
        {
            if (!loadedPeriodicities.Contains(periodicity))
                throw new IndexOutOfRangeException($"The value {periodicity} is not a loaded value for the programmings periodicity.");

            if (!definedPeriodicities.Contains(periodicity))
                definedPeriodicities.Add(periodicity);
        }

        internal void SetLoadedPeriodicity(string periodicityToLoad)
        {
            if(!loadedPeriodicities.Contains(periodicityToLoad))
                loadedPeriodicities.Add(periodicityToLoad);
        }
    }
}