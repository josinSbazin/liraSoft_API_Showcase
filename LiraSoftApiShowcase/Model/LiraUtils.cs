using System;
using System.Diagnostics;
using System.Windows;
using Microsoft.Win32;

namespace LiraSoftApiShowcase.Model
{
    public static class LiraUtils
    {
        public static void OpenFileByLira(string filePath)
        {
            var registryKey = Registry.LocalMachine;
            var softwareKey = registryKey.OpenSubKey("SOFTWARE");
            var liraKey = softwareKey?.OpenSubKey("Lira Soft");

            if (liraKey != null && liraKey.SubKeyCount == 1)
            {
                var currentLiraKey = liraKey.OpenSubKey(liraKey.GetSubKeyNames()[0]);
                var fileDirectory = currentLiraKey?.GetValue("BaseDirectory");
                var liraPrimePath = fileDirectory + "bin64\\Prime.exe";

                Process process = new Process();
                bool flag = false;

                try
                {
                    process.StartInfo.FileName = liraPrimePath;
                    process.StartInfo.Arguments = "\"" + filePath + "\"";
                    process.StartInfo.UseShellExecute = true;
                    process.StartInfo.CreateNoWindow = true;
                    process.Start();
                    flag = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка:\n" + ex.Message);
                }

                if (flag) return;
            }

            try
            {
                new Process
                {
                    StartInfo =
                    {
                        FileName = filePath,
                        UseShellExecute = true,
                        CreateNoWindow = true
                    }
                }.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка:\n" + ex.Message);
            }
        }
    }
}