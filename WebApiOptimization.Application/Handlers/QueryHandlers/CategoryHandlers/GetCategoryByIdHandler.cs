using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WebApiOptimization.Application.Mappers;
using WebApiOptimization.Application.Queries.CategoryQueries;
using WebApiOptimization.Application.Responses;
using WebApiOptimization.Core.Repositories;

namespace WebApiOptimization.Application.Handlers.QueryHandlers.CategoryHandlers
{
    public class GetCategoryByIdHandler : IRequestHandler<GetCategoryByIdQuery, ResponseBuilder<CategoryResponse>>
    {
        private readonly ICategoryRepository _categoryRepository;
        public GetCategoryByIdHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<ResponseBuilder<CategoryResponse>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var categoryEntity = _categoryRepository.GetById(request.Id);
            if(categoryEntity == null)
            {
                return new ResponseBuilder<CategoryResponse> { Message = $"Category with id={request.Id} not found!", Data = null };
            }

            var response = CategoryMapper.Mapper.Map<CategoryResponse>(categoryEntity);
            return new ResponseBuilder<CategoryResponse> { Message = "OK.", Data = response };
        }
    }
}
