using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebApiOptimization.Application.Commands.CategoryCommands;
using WebApiOptimization.Application.Mappers;
using WebApiOptimization.Application.Responses;
using WebApiOptimization.Core.Repositories;

namespace WebApiOptimization.Application.Handlers.CommandHandlers.CategoryHandlers
{
    public class DeleteCategoryHandler : IRequestHandler<DeleteCategoryCommand, ResponseBuilder<CategoryResponse>>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductRepository _productRepository;

        public DeleteCategoryHandler(ICategoryRepository categoryRepository, IProductRepository productRepository)
        {
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
        }

        public async Task<ResponseBuilder<CategoryResponse>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var categoryToDeleteEntity = _categoryRepository.GetById(request.Id);
            if(categoryToDeleteEntity == null)
            {
                return new ResponseBuilder<CategoryResponse> { Message = $"Category with id={request.Id} not found!", Data = null };
            }

            try
            {
                // Find products with this categoryId
                var productsWithThisCategoryId = _productRepository.GetByCategoryId(request.Id).ToList();
                if (productsWithThisCategoryId.Any())
                {
                    // Set CategoryId as null for each product
                    productsWithThisCategoryId.ForEach(x => x.CategoryID = null);
                    _productRepository.UpdateRange(productsWithThisCategoryId);
                }

                _categoryRepository.Delete(categoryToDeleteEntity);
                var response = CategoryMapper.Mapper.Map<CategoryResponse>(categoryToDeleteEntity);
                return new ResponseBuilder<CategoryResponse> { Message = "Category deleted.", Data = response };
            }
            catch(Exception e)
            {
                return new ResponseBuilder<CategoryResponse> { Message = $"Category not deleted! Error: {e.InnerException.Message}", Data = null };
            }
        }
    }
}
