using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace HashLauncher.Model
{
    public class AppLibrary
    {
        public List<App> apps = new List<App>();

        public AppLibrary()
        {
            //Form1.player.SoundLocation = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "opensound.wav");
        }

        public class App
        {
            public string name;
            public string path;
            public string args;

            public void Open()
            {
                try
                {
                    //Form1.player.Play();
                    if (string.IsNullOrWhiteSpace(args))
                    {
                        Process.Start(@path);
                    }
                    else
                    {
                        ProcessStartInfo startInfo = new ProcessStartInfo();
                        startInfo.FileName = path;
                        startInfo.Arguments = args;
                        Process.Start(startInfo);
                    }
                }
                catch
                {
                    System.Windows.MessageBox.Show("Something went wrong");
                }
            }

            public void Clicked(object sender, EventArgs args)
            {
                Open();
            }
        }
    }
}
