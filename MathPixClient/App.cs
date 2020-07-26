using System;
using System.Windows;
using System.Collections.Generic;
using System.Configuration;
using System.Security.Cryptography;
using System.ComponentModel;

namespace MathPixClient
{
    class App : Application
    {
        public Config Cfg { get; }
            = new Config();

        [STAThread]
        public static void Main()
        {           
            App app = new App();

            MainWindow win = new MainWindow();

            app.Run(win);
        }

        public App()
        {
            LoadConfigOrDefault();

        }
   
        private void LoadConfigOrDefault()
        {
            Cfg.Groups.Add(new SubstitutionGroup("LaTeX"));
            Cfg.Groups[0].Substitutions.Add(new Substitution { From = "\\bar", To = "\\overline" });
            Cfg.Groups[0].Substitutions.Add(new Substitution { From = "\\backslash", To = "\\setminus" });
            Cfg.Groups.Add(new SubstitutionGroup("SMA"));
            Cfg.Groups[1].Substitutions = new List<Substitution>(Cfg.Groups[0].Substitutions);
            Cfg.Groups[1].Substitutions.Add(new Substitution { From = "\\(", To = "[$]" });
            Cfg.Groups[1].Substitutions.Add(new Substitution { From = "\\)", To = "[/$]" });
            Cfg.Groups[1].Substitutions.Add(new Substitution { From = "\\[", To = "<dd>[$$]" });
            Cfg.Groups[1].Substitutions.Add(new Substitution { From = "\\]", To = "[/$$]</dd>" });
            Cfg.SelectedGroup = Cfg.Groups[1];
        }
    }
}