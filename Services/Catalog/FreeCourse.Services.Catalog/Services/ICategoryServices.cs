using FreeCourse.Services.Catalog.Dtos;
using FreeCourse.Services.Catalog.Model;
using FreeCourse.Shared.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreeCourse.Services.Catalog.Services
{
    public interface ICategoryServices
    {
        Task<Response<List<CategoryDto>>> GetAllAsync();
        Task<Response<CategoryDto>> GetByIdAsync(string id);
        Task<Response<CategoryDto>> CreateAsync(CategoryCreateDto categoryCreateDto);
        Task<Response<NoContents>> UpdateAsync(CategoryUpdateDto categoryUpdateDto);
        Task<Response<NoContents>> DeleteAsync(string id);
    }
}
