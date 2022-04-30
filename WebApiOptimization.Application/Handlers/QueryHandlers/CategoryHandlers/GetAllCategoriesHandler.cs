using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebApiOptimization.Application.Mappers;
using WebApiOptimization.Application.Queries.CategoryQueries;
using WebApiOptimization.Application.Responses;
using WebApiOptimization.Application.Wrappers;
using WebApiOptimization.Core.Entities;
using WebApiOptimization.Core.Repositories;

namespace WebApiOptimization.Application.Handlers.QueryHandlers.CategoryHandlers
{
    public class GetAllCategoriesHandler : IRequestHandler<GetAllCategoriesQuery, ResponseBuilder<IEnumerable<CategoryResponse>>>
    {
        private readonly ICategoryRepository _categoryRepository;

        public GetAllCategoriesHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<ResponseBuilder<IEnumerable<CategoryResponse>>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            List<Category> categories;
            List<CategoryResponse> categoriesDto;
            if(request.PageNumber == 0 || request.PageSize == 0)
            {
                categories = await _categoryRepository.GetAllAsync();
                categoriesDto = CategoryMapper.Mapper.Map<List<CategoryResponse>>(categories);
                return new ResponseBuilder<IEnumerable<CategoryResponse>> { Message = "OK", Data = categoriesDto };
            }

            categories = await _categoryRepository.GetAllPagedAsync(request.PageNumber, request.PageSize);
            categoriesDto = CategoryMapper.Mapper.Map<List<CategoryResponse>>(categories);
            return new PagedResponse<IEnumerable<CategoryResponse>>(categoriesDto, request.PageNumber, request.PageSize, "OK");
        }
    }
}
