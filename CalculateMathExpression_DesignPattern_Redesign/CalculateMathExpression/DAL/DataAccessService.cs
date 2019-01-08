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
    class DataAccessService : IDataAccessService
    {
        public async Task<Dictionary<string,bool>> GetButtonPermissions()
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
                permissions.Add(code,isEnable);

            }
            return permissions;

        }

        public void Save(Dictionary<string,bool> buttonPermissions)
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
                Write(lines);

            
            
        }
        public static async void Write(List<string> lines)
        {
            const string TEXT_FILE_NAME = "data.txt";
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
    }
}
