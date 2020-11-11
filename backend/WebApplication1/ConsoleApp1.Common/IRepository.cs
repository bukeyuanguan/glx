using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsoleApp1.Common
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