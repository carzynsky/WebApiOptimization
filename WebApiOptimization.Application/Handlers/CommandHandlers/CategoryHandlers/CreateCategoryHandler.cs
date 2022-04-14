using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using WebApiOptimization.Application.Commands.CategoryCommands;
using WebApiOptimization.Application.Mappers;
using WebApiOptimization.Application.Responses;
using WebApiOptimization.Core.Entities;
using WebApiOptimization.Core.Repositories;

namespace WebApiOptimization.Application.Handlers.CommandHandlers.CategoryHandlers
{
    public class CreateCategoryHandler : IRequestHandler<CreateCategoryCommand, ResponseBuilder<CategoryResponse>>
    {
        private readonly ICategoryRepository _categoryRepository;

        public CreateCategoryHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<ResponseBuilder<CategoryResponse>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var categoryEntity = CategoryMapper.Mapper.Map<Category>(request);
            if (categoryEntity == null)
            {
                return new ResponseBuilder<CategoryResponse> { Message = "Invalid data provided!", Data = null };
            }

            try
            {
                var newCategory = await _categoryRepository.AddAsync(categoryEntity);
                var categoryResponse = CategoryMapper.Mapper.Map<CategoryResponse>(newCategory);
                return new ResponseBuilder<CategoryResponse> { Message = "Category created.", Data = categoryResponse };
            }
            catch(Exception e)
            {
                return new ResponseBuilder<CategoryResponse> { Message = $"Category not created! Error: {e.InnerException.Message}", Data = null };
            }
        }
    }
}
