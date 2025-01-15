using FreeCourse.Services.Catalog.Dtos;
using FreeCourse.Services.Catalog.Model;
using FreeCourse.Shared.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreeCourse.Services.Catalog.Services
{
    public interface ICourseServices
    {
        Task<Response<List<CourseDto>>> GetAllAsync();
        Task<Response<CourseDto>> GetByIdAsync(string id);
        Task<Response<List<CourseDto>>> GetAllByUserIdAsync(string userId);
        Task<Response<CourseDto>> CreateAsync(CourseCreateDto courseCreateDto);
        Task<Response<NoContents>> UpdateAsync(CourseUpdateDto courseUpdateDto);
        Task<Response<NoContents>> DeleteAsync(string id);
    }
}
