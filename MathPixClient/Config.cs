﻿using System;
using System.Windows;
using System.Collections.Generic;

namespace MathPixClient
{
    public class Config
    {
        public List<SubstitutionGroup> Groups { get; set; } = new List<SubstitutionGroup>();

        public List<Substitution> ActiveSubstitutions { get; private set; }

        public int SelectedGroup { get; private set; }

        public List<string> GroupNames
        { get
            {
                List<string> L = new List<string>();
                foreach (var g in Groups)
                {
                    L.Add(g.Name);
                }
                return L;
            }
        }

        public void SetActiveGroup(int idx)
        {
            SelectedGroup = idx;
            ActiveSubstitutions = Groups[idx].Substitutions;
        }
    }

    public class SubstitutionGroup
    {
        public string Name { get; set; }
        public List<Substitution> Substitutions { get; set; }
            = new List<Substitution>();
        public SubstitutionGroup(string Name)
        {
            if (Name == null || Name == "")
                throw new Exception("Substitution Group Name cannot be empty");

            this.Name = Name;
        }
    }

    public class Substitution
    {
        public string From { get; set; }
        public string To { get; set; }
    }
}