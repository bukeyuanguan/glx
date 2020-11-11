using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleApp1.Common
{
    public class MemoryRepository
    {
        public static MemoryRepository Instance { get; } = new MemoryRepository();

        private readonly Dictionary<string, ToDoItem> _dic = new Dictionary<string, ToDoItem>();

        private MemoryRepository()
        {
            Init();
        }

        public async Task<ToDoItem> GetAsync(string id)
        {
            _dic.TryGetValue(id, out var item);
            return item;
        }

        public async Task UpsertAsync(ToDoItem model)
        {
            _dic[model.Id] = model;
        }

        public async Task DeleteAsync(string id)
        {
            if (_dic.ContainsKey(id))
                _dic.Remove(id);
        }

        public async Task<List<ToDoItem>> QueryAsync(
            string description, bool? done)
        {
            IEnumerable<ToDoItem> models = _dic.Values.ToList();

            if (!string.IsNullOrEmpty(description))
                models = models.Where(v => v.Description?.IndexOf(description, StringComparison.OrdinalIgnoreCase) >= 0);

            if (done != null)
                models = models.Where(v => v.Done == done.Value);

            return models.ToList();
        }

        public async Task<ToDoItem> UpdateAsync(string id, ToDoItemUpdateModel updateModel)
        {
            if (_dic.TryGetValue(id, out var item))
            {
                if (!string.IsNullOrEmpty(updateModel.Description))
                    item.Description = updateModel.Description;

                if (updateModel.Favorite != null)
                    item.Favorite = updateModel.Favorite.Value;

                if (updateModel.Done != null)
                    item.Done = updateModel.Done.Value;

                return item;
            }
            return null;
        }


        private void Init()
        {
            for (var i = 0; i < 2; i++)
            {
                var item = new ToDoItem()
                {
                    Id = i.ToString(),
                    CreatedTime = DateTime.UtcNow,
                    Description = $"test item {i}",
                    Done = false,
                    Favorite = false,
                    Children = null
                };
                _dic[item.Id] = item;
            }
        }


    }
}
