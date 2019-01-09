using CalculateMathExpression.Models;
using System.Collections.Generic;
using System.IO;
using Windows.Storage;
using System;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Collections.Concurrent;

namespace CalculateMathExpression.DAL
{
    class LocalFileAccessService : IDataAccessService
    {
        private async Task<Dictionary<string,bool>> GetButtonPermissions()
        {
            try
            {
                var file = await ApplicationData.Current.LocalFolder.GetFileAsync("data.txt");

                var lines = await FileIO.ReadLinesAsync(file);
                Dictionary<string, bool> permissions = new Dictionary<string, bool>();
                foreach (string line in lines)
                {

                    if (line == null || line == "")
                    {
                        break;
                    }
                    string[] lineParts = line.Split("  ");
                    if (lineParts.Length != 2 || !"01".Contains(lineParts[1]) || lineParts[0] == "")
                    {
                        continue;
                    }

                    string code = lineParts[0];
                    Debug.WriteLine(code);
                    bool isEnable = false;
                    if (lineParts[1] == "1")
                    {
                        isEnable = true;
                    }
                    permissions.Add(code, isEnable);

                }
                return permissions;
            } catch (Exception e)
            {
                Debug.WriteLine("Data file not found exception!");
                return null;
            }
        }
        private async Task<Dictionary<string, string>> GetFormulasFromFile()
        {
            try
            {
                var file = await ApplicationData.Current.LocalFolder.GetFileAsync("formula.txt");
                var lines = await FileIO.ReadLinesAsync(file);
                Dictionary<string, string> fs = new Dictionary<string, string>();
                foreach (string line in lines)
                {

                    if (line == null || line == "")
                    {
                        break;
                    }
                    string[] lineParts = line.Split("  ");
                    if (lineParts.Length == 1)
                    {
                        fs.Add(lineParts[0], "");
                        continue;
                    }

                    if (lineParts.Length == 2)
                    {
                        fs.Add(lineParts[0], lineParts[1]);
                        continue;
                    }

                }
                return fs;
            }
            catch (Exception e)
            {
                Debug.WriteLine("Formula file not found exception!");
                return null;
            }
        }

        public void SavePermissions(Dictionary<string,bool> buttonPermissions)
        {

                List<string> lines = new List<string>();
                foreach (string code in buttonPermissions.Keys)
                {
                    bool isEnable = true;
                    buttonPermissions.TryGetValue(code, out isEnable);
                    string temp = "0";
                    if (isEnable)
                    {
                        temp = "1";
                    }
                    lines.Add(code + "  " + temp);
                }
                Write(lines, "data.txt");

        }
        public void SaveFormulas(Dictionary<string, string> buttonPermissions)
        {

            List<string> lines = new List<string>();
            foreach (string code in buttonPermissions.Keys)
            {
                string f = "";
                buttonPermissions.TryGetValue(code, out f);
                lines.Add(code + "  " + f);
            }
            Write(lines, "formula.txt");

        }
        public static async void Write(List<string> lines, string fileName)
        {
            string TEXT_FILE_NAME = fileName;
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            StorageFolder tempFolder = ApplicationData.Current.TemporaryFolder;
            await localFolder.CreateFileAsync(TEXT_FILE_NAME, CreationCollisionOption.ReplaceExisting);
            StorageFile dataFile = await localFolder.GetFileAsync(TEXT_FILE_NAME);    
            await FileIO.AppendLinesAsync(dataFile, lines);

        }

        public Task<Dictionary<string,bool>> GetButtonPermissionAsync()
        {
            return GetButtonPermissions();
        }


        public Task<Dictionary<string, string>> GetFormulas()
        {
            return GetFormulasFromFile();
        }
    }
}
