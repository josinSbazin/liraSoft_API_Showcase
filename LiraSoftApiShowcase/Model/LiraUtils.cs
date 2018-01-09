using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using FEModel.Interfaces;
using Microsoft.Win32;
using ProjectIOHelper;
using PureAbstractions;

namespace LiraSoftApiShowcase.Model
{
    public static class LiraUtils
    {
        /// <summary>
        ///  Открывает файл по пути в Lira Soft
        /// </summary>
        /// <param name="filePath"></param>
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

        /// <summary>
        ///  Записывает в файл информацию
        /// </summary>
        public static void CreateModelFile(IModel model, string filePath)
        {
            var ioHeader = ProjectIOHelperTypeInfo.get_TypeInfo().CreateIOHeader();
            ioHeader.Clear();

            var binaryPersistent = (IBinaryPersistent)ioHeader;

            Stream output = File.Create(filePath);
            BinaryWriter pWriter = new BinaryWriter(output);

            output.Seek(0L, SeekOrigin.Begin);
            binaryPersistent.SaveState(pWriter, null);

            ioHeader.set_HeaderOffset(IProjectIOHeader.e_offset_type.OT_MODEL, pWriter.BaseStream.Position);
            model.SaveState(pWriter, null);
            output.Seek(0L, SeekOrigin.Begin);
            binaryPersistent.SaveState(pWriter, null);

            pWriter.Close();
            output.Close();
        }
    }
}