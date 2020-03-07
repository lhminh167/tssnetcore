using Tss.Core.Entities;
using Tss.Core.Interfaces;
using System.Threading.Tasks;
using System.Collections.Generic;
using Tss.Core.Specifications;
using Tss.Core.Models;
using Ardalis.GuardClauses;

namespace Tss.Core.Services
{
    public partial class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Category> _CategoryRepo;
        private readonly IAuthHelper _authHelper;

        public CategoryService(
            IUnitOfWork unitOfWork,
            IRepository<Category> CategoryRepo,
            IAuthHelper authHelper
        )
        {
            _unitOfWork = unitOfWork ?? throw new System.ArgumentNullException(nameof(unitOfWork));
            _CategoryRepo = CategoryRepo ?? throw new System.ArgumentNullException(nameof(CategoryRepo));
            _authHelper = authHelper ?? throw new System.ArgumentNullException(nameof(authHelper));
        }

        // public async Task<List<Category>> Get()
        // {
        //     return await _CategoryRepo.Get(new GetCategorySpecification(_authHelper));
        // }

        public async Task<Category> Get(int categoryId)
        {
            return await _CategoryRepo.GetOne(new GetCategoryByIdSpecification(categoryId, _authHelper));
        }

        // public async Task<Category> Create(CategoryCreateRequest input)
        // {
        //     Category Category = await _CategoryRepo.GetOne(new GetCategoryByNameSpecification(input.Name, _authHelper));
        //     Guard.Against.AlreadyExist(Category, nameof(Category));

        //     Category = new Category(input.Name);
        //     _CategoryRepo.Post(Category);
        //     await _unitOfWork.SaveAsync();
        //     return Category;
        // }

        // public async Task<Category> Update(Category input)
        // {
        //     Category Category = await _CategoryRepo.Get(input.CategoryId);
        //     Guard.Against.NotFound(Category, nameof(Category));

        //     Category.Update(input.Name);
        //     _CategoryRepo.Put(Category);
        //     await _unitOfWork.SaveAsync();
        //     return Category;
        // }

        // public async Task<Category> Delete(int CategoryId)
        // {
        //     Category Category = await _CategoryRepo.Get(CategoryId);
        //     Guard.Against.NotFound(Category, nameof(Category));

        //     _CategoryRepo.Delete(Category);
        //     await _unitOfWork.SaveAsync();
        //     return Category;
        // }
    }
}