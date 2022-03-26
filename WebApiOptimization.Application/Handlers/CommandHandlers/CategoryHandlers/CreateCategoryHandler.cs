using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WebApiOptimization.Application.Commands.Category;
using WebApiOptimization.Application.Mappers;
using WebApiOptimization.Application.Responses;
using WebApiOptimization.Core.Entities;
using WebApiOptimization.Core.Repositories;

namespace WebApiOptimization.Application.Handlers.CommandHandlers.CategoryHandlers
{
    public class CreateCategoryHandler : IRequestHandler<CreateCategoryCommand, CategoryResponse>
    {
        private readonly ICategoryRepository _categoryRepository;
        public CreateCategoryHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<CategoryResponse> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var categoryEntity = CategoryMapper.Mapper.Map<Category>(request);
            if (categoryEntity == null)
            {
                return null;
            }

            var newCategory = _categoryRepository.Add(categoryEntity);
            var categoryResponse = CategoryMapper.Mapper.Map<CategoryResponse>(newCategory);
            return categoryResponse;
        }
    }
}
