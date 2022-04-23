using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using WebApiOptimization.Application.Commands.CategoryCommands;
using WebApiOptimization.Application.Helpers;
using WebApiOptimization.Application.Mappers;
using WebApiOptimization.Application.Responses;
using WebApiOptimization.Core.Entities;
using WebApiOptimization.Core.Repositories;

namespace WebApiOptimization.Application.Handlers.CommandHandlers.CategoryHandlers
{
    public class UpdateCategoryHandler : IRequestHandler<UpdateCategoryCommand, ResponseBuilder<CategoryResponse>>
    {
        private readonly ICategoryRepository _categoryRepository;

        public UpdateCategoryHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<ResponseBuilder<CategoryResponse>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var categoryToUpdate = await _categoryRepository.GetByIdAsync(request.CategoryId);
            if(categoryToUpdate == null)
            {
                return new ResponseBuilder<CategoryResponse> { Message = $"Category with id={request.CategoryId} not found!", Data = null };
            }

            try
            {
                var categoryToUpdateEntity = CategoryMapper.Mapper.Map<Category>(request);
                if (categoryToUpdateEntity == null)
                {
                    return new ResponseBuilder<CategoryResponse> { Message = ResponseBuilderHelper.InvalidData, Data = null };
                }

                _categoryRepository.Update(categoryToUpdateEntity);
                var response = CategoryMapper.Mapper.Map<CategoryResponse>(categoryToUpdateEntity);
                return new ResponseBuilder<CategoryResponse> { Message = "Category updated.", Data = response };
            }
            catch(Exception e)
            {
                return new ResponseBuilder<CategoryResponse> { Message = $"Category not updated! Error: {e.InnerException.Message}", Data = null};
            }
        }
    }
}
