using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Task_Manager_Prism.DatabaseAccess;
using Task_Manager_Prism.Models;
using Task_Manager_Prism.Ultils;
using Task_Manager_Prism.Utils;

namespace Task_Manager_Prism.DAL
{
    
   

    class DatabaseAccessServiceProxy : IDatabaseAccessService

    {
        private TokenModel _tokenModel;
        private IMessageService _messageService;
        public DatabaseAccessServiceProxy(IMessageService messageService)
        {
            _messageService = messageService;
        }

     

        async public void AddTagItem(string tagName)
        {
            try
            {
                _tokenModel = ApplicaitonShareDataService.GetCurrent().Token;
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _tokenModel.access_token);
                client.BaseAddress = new Uri(Constants.ApiBaseUrl);
                var response = await client.PostAsJsonAsync(
                 "tag", new TagItem(tagName));
                if (!(response.StatusCode == HttpStatusCode.OK))

                {
                    _messageService.ShowMessage("Error", "Can not add tag!");

                }
            } catch
            {
                _messageService.ShowMessage("Error", "Can not connect to server!");
            }

        }

        async public void AddTaskItem(TaskItem item)
        {
            try
            {
                _tokenModel = ApplicaitonShareDataService.GetCurrent().Token;
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _tokenModel.access_token);
                client.BaseAddress = new Uri(Constants.ApiBaseUrl);
                var response = await client.PostAsJsonAsync(
                 "task", item);
                if (!(response.StatusCode == HttpStatusCode.OK))

                {
                    _messageService.ShowMessage("Error", "Can not add task!");

                }
            } catch
            {
                _messageService.ShowMessage("Error", "Can not connect to server!");
            }
          

        }

        async public void DeleteTaskItem(int id)
        {
            try
            {
                _tokenModel = ApplicaitonShareDataService.GetCurrent().Token;
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _tokenModel.access_token);
                client.BaseAddress = new Uri(Constants.ApiBaseUrl);
                var response = await client.DeleteAsync(
                        "task/" + id.ToString());
                if (response.StatusCode == HttpStatusCode.NotFound)

                {
                    _messageService.ShowMessage("Error", "Can not found task to delete!");

                }
                if (response.StatusCode == HttpStatusCode.BadRequest)

                {
                    _messageService.ShowMessage("Error", "Can not delete task because of database's error!");
                }
            } catch
            {
                _messageService.ShowMessage("Error", "Can not connect to server!");
            }

        }

        async public Task<List<TagItem>> GetTags()
        {
            _tokenModel = ApplicaitonShareDataService.GetCurrent().Token;
            List<TagItem> tags = new List<TagItem>();
            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _tokenModel.access_token);
                    httpClient.BaseAddress = new Uri(Constants.ApiBaseUrl);
                    var responseTask = httpClient.GetAsync("tag");                
                    var result = await responseTask;
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
                _messageService.ShowMessage("Error", "Can not connect to server!");
            }
            return tags;
        }

        async public Task<List<TaskItem>> GetTasks()
        {
            _tokenModel = ApplicaitonShareDataService.GetCurrent().Token;
            List<TaskItem> tasks = new List<TaskItem>();
            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _tokenModel.access_token);
                    httpClient.BaseAddress = new Uri(Constants.ApiBaseUrl);
                    var responseTask = httpClient.GetAsync("task");
                    responseTask.Wait();
                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {

                        var readTask = result.Content.ReadAsAsync<TaskItem[]>();
                        var taskItems = await readTask;

                        foreach (var item in taskItems)
                        {
                            tasks.Add(item);
                        }
                    }
                }
            } catch (Exception e)
                {
                _messageService.ShowMessage("Error", "Can not connect to server!");
            }
                return tasks;
        }

        async public void UpdateTaskItem(TaskItem item)
        {
            try
            {
                _tokenModel = ApplicaitonShareDataService.GetCurrent().Token;
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _tokenModel.access_token);
                client.BaseAddress = new Uri(Constants.ApiBaseUrl);
                var response = await client.PutAsJsonAsync(
                 "task", item);

                if (response.StatusCode == HttpStatusCode.NotFound)

                {
                    _messageService.ShowMessage("Error", "Can not update task because task doesn't exist!");
                }
                if (response.StatusCode == HttpStatusCode.BadRequest)

                {
                    _messageService.ShowMessage("Error", "Can not update task because of database's error!");
                }
            } catch
            {
                _messageService.ShowMessage("Error", "Can not connect to server!");
            }
        }
    }
}
