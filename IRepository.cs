using System.Collections.Generic;
using System.Threading.Tasks;
using Slb.Bootcamp.Service.ToDoList.Models;

namespace Slb.Bootcamp.Service.ToDoList
{
    public interface IRepository
    {
        Task DeleteAsync(string id);
        Task<ToDoItem> GetAsync(string id);
        Task<List<ToDoItem>> QueryAsync(string description, bool? done);
        Task<ToDoItem> UpdateAsync(string id, ToDoItemUpdateModel updateModel);
        Task UpsertAsync(ToDoItem model);
    }
}