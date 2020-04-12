using Tss.Core.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;
using Tss.Core.Models;

namespace Tss.Core.Interfaces
{
    public interface ICategoryService
    {
        // Task<List<Category>> Get();
        Task<CategoryGetResponse> Get(int categoryId);
        // Task<Category> Create(CategoryCreateRequest input);
        // Task<Category> Update(Category Category);
        // Task<Category> Delete(int CategoryId);
    }
}