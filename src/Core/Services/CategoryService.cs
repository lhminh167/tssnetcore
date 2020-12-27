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
        private readonly IRepository<Category> _categoryRepo;
        private readonly IAuthHelper _authHelper;
        private readonly IRepository<Product> _productRepo;

        public CategoryService(
            IUnitOfWork unitOfWork,
            IRepository<Category> categoryRepo,
            IAuthHelper authHelper,
            IRepository<Product> productRepo
        )
        {
            _unitOfWork = unitOfWork ?? throw new System.ArgumentNullException(nameof(unitOfWork));
            _categoryRepo = categoryRepo ?? throw new System.ArgumentNullException(nameof(categoryRepo));
            _authHelper = authHelper ?? throw new System.ArgumentNullException(nameof(authHelper));
            _productRepo = productRepo ?? throw new System.ArgumentNullException(nameof(productRepo));
        }

        public async Task<List<Category>> Get()
        {
            return await _categoryRepo.Get();
        }

        public async Task<CategoryGetResponse> Get(int categoryId)
        {
            Category category = await _categoryRepo.GetOne(new GetCategoryByIdSpecification(categoryId, _authHelper));

            ProductGetRequest request = new ProductGetRequest(
                0, 8, true, null, true, null, null, null,
                new List<string> { category.Name },
                0, 0, 0
            );
            PaginatedList<Product> product = await _productRepo.GetPaginated(new GetProductSpecification(
                _authHelper,
                request
            ));

            return new CategoryGetResponse(
                category,
                product
            );
        }

        // public async Task<Category> Create(CategoryCreateRequest input)
        // {
        //     Category Category = await _categoryRepo.GetOne(new GetCategoryByNameSpecification(input.Name, _authHelper));
        //     Guard.Against.AlreadyExist(Category, nameof(Category));

        //     Category = new Category(input.Name);
        //     _categoryRepo.Post(Category);
        //     await _unitOfWork.SaveAsync();
        //     return Category;
        // }

        // public async Task<Category> Update(Category input)
        // {
        //     Category Category = await _categoryRepo.Get(input.CategoryId);
        //     Guard.Against.NotFound(Category, nameof(Category));

        //     Category.Update(input.Name);
        //     _categoryRepo.Put(Category);
        //     await _unitOfWork.SaveAsync();
        //     return Category;
        // }

        // public async Task<Category> Delete(int CategoryId)
        // {
        //     Category Category = await _categoryRepo.Get(CategoryId);
        //     Guard.Against.NotFound(Category, nameof(Category));

        //     _categoryRepo.Delete(Category);
        //     await _unitOfWork.SaveAsync();
        //     return Category;
        // }
    }
}