using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WebApiOptimization.Application.Commands.Category;
using WebApiOptimization.Application.Mappers;
using WebApiOptimization.Application.Responses;
using WebApiOptimization.Core.Repositories;

namespace WebApiOptimization.Application.Handlers.CommandHandlers.CategoryHandlers
{
    public class DeleteCategoryHandler : IRequestHandler<DeleteCategoryCommand, CategoryResponse>
    {
        private readonly ICategoryRepository _categoryRepository;
        public DeleteCategoryHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<CategoryResponse> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var categoryToDeleteEntity = _categoryRepository.GetById(request.Id);
            if(categoryToDeleteEntity == null)
            {
                return null;
            }

            _categoryRepository.Delete(categoryToDeleteEntity);
            var response = CategoryMapper.Mapper.Map<CategoryResponse>(categoryToDeleteEntity);
            return response;
        }
    }
}
