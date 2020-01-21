using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Hardcodet.Wpf.TaskbarNotification;
using HashLauncher.Model;

namespace HashLauncher
{
    class TaskbarControl
    {
        public TaskbarControl()
        {
            ReadApps();
        }
        
        public AppLibrary appLibrary = new AppLibrary();
        public ContextMenu contextMenu
        {
            get
            {
                return MainWindow.instance.myNotifyIcon.ContextMenu;
            }
            set
            {
                MainWindow.instance.myNotifyIcon.ContextMenu = value;
            }
        }

        public void ReadApps()
        {
            try
            {
                string filepath = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "apps.txt");
                string[] raw = File.ReadAllLines(filepath);
                appLibrary.apps.Clear();
                foreach (string line in raw)
                {
                    string[] split = line.Split(',');
                    if (split.Length > 1)
                    {
                        AppLibrary.App newApp = new AppLibrary.App()
                        {
                            name = split[0],
                            path = split[1]
                        };
                        if (split.Length > 2)
                        {
                            newApp.args = split[2];
                        }
                        appLibrary.apps.Add(newApp);
                    }
                }

                contextMenu = new ContextMenu();
                contextMenu.Items.Clear();
                for (int i = 0; i < appLibrary.apps.Count; i++)
                {
                    MenuItem newitem = new MenuItem();
                    newitem.Header = appLibrary.apps[i].name;
                    newitem.Click += appLibrary.apps[i].Clicked;
                    contextMenu.Items.Add(newitem);
                }
            }
            catch
            {
                contextMenu = new ContextMenu();
                contextMenu.Items.Clear();
            }
            finally
            {
                contextMenu.Items.Add(new Separator());
                
                MenuItem refresh = new MenuItem();
                refresh.Header = "Refresh List";
                refresh.Click += RefreshHandler;
                contextMenu.Items.Add(refresh);

                MenuItem edit = new MenuItem();
                edit.Header = "Edit List";
                edit.Click += EditListHandler;
                contextMenu.Items.Add(edit);

                MenuItem close = new MenuItem();
                close.Header = "Close";
                close.Click += CloseHandler;
                contextMenu.Items.Add(close);
            }
        }

        public void EditListHandler(object sender, EventArgs args)
        {
            //System.Diagnostics.Process.Start(@"apps.txt");

            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.FileName = @"explorer.exe";
            startInfo.Arguments = @"apps.txt";
            System.Diagnostics.Process.Start(startInfo);
        }

        public void RefreshHandler(object sender, EventArgs args)
        {
            ReadApps();
        }

        public void CloseHandler(object sender, EventArgs args)
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}
