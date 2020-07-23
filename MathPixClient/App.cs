using System;
using System.Windows;
using System.Collections.Generic;
using System.Configuration;

namespace MathPixClient
{
    class App : Application
    {
        private Config _cfg;

        public List<string> GroupNames
        {
            get
            {
                return _GroupNames();
            } 
        }

        private List<string> _GroupNames()
        {
            List<string> names = new List<string>();

            foreach (var grp in _cfg.Groups)
            {
                names.Add(grp.Name);
            }

            return names;
        }

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
            _cfg = new Config();
            _cfg.Groups.Add(new SubstitutionGroup("LaTeX"));
            _cfg.Groups.Add(new SubstitutionGroup("SMA"));
        }
    }
}