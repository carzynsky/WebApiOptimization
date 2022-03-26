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
    public class UpdateCategoryHandler : IRequestHandler<UpdateCategoryCommand, CategoryResponse>
    {
        private readonly ICategoryRepository _categoryRepository;
        public UpdateCategoryHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<CategoryResponse> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var categoryToUpdate = _categoryRepository.GetById(request.CategoryId);
            if(categoryToUpdate == null)
            {
                return null;
            }

            var categoryToUpdateEntity = CategoryMapper.Mapper.Map<Category>(request);
            if(categoryToUpdateEntity == null)
            {
                return null;
            }

            _categoryRepository.Update(categoryToUpdateEntity);
            var response = CategoryMapper.Mapper.Map<CategoryResponse>(categoryToUpdateEntity);
            return response;
        }
    }
}
