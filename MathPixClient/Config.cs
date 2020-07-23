using System;
using System.Windows;
using System.Collections.Generic;

namespace MathPixClient
{
    public class Config
    {
        public List<SubstitutionGroup> Groups { get; set; } = new List<SubstitutionGroup>();
    }

    public class SubstitutionGroup
    {
        public string Name { get; set; }
        public List<(string, string)> Substitutions { get; set; }

        public SubstitutionGroup(string Name)
        {
            if (Name == null || Name == "")
                throw new Exception("Substitution Group Name cannot be empty");

            this.Name = Name;
        }
    }
}