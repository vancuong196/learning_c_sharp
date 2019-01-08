using CalculateMathExpression.Models;
using System.Collections.Generic;
using System.IO;
using Windows.Storage;
using System;
using System.Threading.Tasks;

namespace CalculateMathExpression.DAL
{
    class DataAccessService : IDataAccessService
    {
        public async Task<List<ButtonPermission>> GetButtonPermissions()
        {
            var file = await ApplicationData.Current.LocalFolder.GetFileAsync("data.txt");

            var lines = await FileIO.ReadLinesAsync(file);
            List<ButtonPermission> permissions = new List<ButtonPermission>();
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
                ButtonPermission permission = new ButtonPermission();
                permission.Code = lineParts[0];
                permission.IsEnable = false;
                if (lineParts[0] == "1")
                {
                    permission.IsEnable = true;
                }
                permissions.Add(permission);

            }
            return permissions;

        }

        public void Save(List<ButtonPermission> buttonPermissions)
        {

                List<string> lines = new List<string>();
                foreach (ButtonPermission buttonPermission in buttonPermissions)
                {
                    string temp = "0";
                    if (buttonPermission.IsEnable)
                    {
                        temp = "1";
                    }
                    lines.Add(buttonPermission.Code + "  " + temp);
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

        public Task<List<ButtonPermission>> GetButtonPermissionAsync()
        {
            return GetButtonPermissions();
        }
    }
}
