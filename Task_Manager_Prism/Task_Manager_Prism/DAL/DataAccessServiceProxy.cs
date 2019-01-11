using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Task_Manager_Prism.DatabaseAccess;
using Task_Manager_Prism.Models;
using Task_Manager_Prism.Ultils;

namespace Task_Manager_Prism.DAL
{
    class DatabaseAccessServiceProxy : IDatabaseAccessService

    {
       
        public DatabaseAccessServiceProxy()
        {
            
        }
        public void AddTagItem(string tagName)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(Constants.ApiBaseUrl);
            client.PostAsJsonAsync(
             "tag", new TagItem(tagName));

        }

        public void AddTaskItem(TaskItem item)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(Constants.ApiBaseUrl);
            client.PostAsJsonAsync(
             "task", item);

        }

        public void DeleteTaskItem(int id)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(Constants.ApiBaseUrl);
            client.DeleteAsync(
                    "task/"+id.ToString());
           
        }

        public Task<List<TagItem>> GetTags()
        {
            List<TagItem> tags = new List<TagItem>();
            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(Constants.ApiBaseUrl);
                    var responseTask = httpClient.GetAsync("tag");
                    responseTask.Wait();
                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {

                        var readTask = result.Content.ReadAsAsync<TagItem[]>();
                        readTask.Wait();

                        var taskItems = readTask.Result;

                        foreach (var item in taskItems)
                        {
                            tags.Add(item);
                        }
                    }
                }
            } catch (Exception e)
            {
                Debug.WriteLine("Problem when access API");
            }
            return Task.FromResult(tags);
        }

        public Task<List<TaskItem>> GetTasks()
        {
            List<TaskItem> tasks = new List<TaskItem>();
            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(Constants.ApiBaseUrl);
                    var responseTask = httpClient.GetAsync("task");
                    responseTask.Wait();
                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {

                        var readTask = result.Content.ReadAsAsync<TaskItem[]>();
                        readTask.Wait();

                        var taskItems = readTask.Result;

                        foreach (var item in taskItems)
                        {
                            tasks.Add(item);
                        }
                    }
                }
            } catch (Exception e)
                {
                    Debug.WriteLine("Problem when access API");
                }
                return Task.FromResult(tasks);
        }

        public void UpdateTaskItem(TaskItem item)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(Constants.ApiBaseUrl);
            client.PutAsJsonAsync(
             "task", item);
        }
    }
}
