using AutoMapper;
using FreeCourse.Services.Catalog.Dtos;
using FreeCourse.Services.Catalog.Model;
using FreeCourse.Services.Catalog.Settings;
using FreeCourse.Shared.Dtos;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreeCourse.Services.Catalog.Services
{
    public class CategoryService : ICategoryServices
    {
        private readonly IMongoCollection<Category> _categoryCollection;
        private readonly IMapper _mapper;

        public CategoryService(IMapper mapper, IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);

            _categoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);
            _mapper = mapper;
        }

        public async Task<Response<List<CategoryDto>>> GetAllAsync()
        {
            var categories = await _categoryCollection.Find(category => true).ToListAsync();
            return Response<List<CategoryDto>>.Success(_mapper.Map<List<CategoryDto>>(categories), 200);
        }

        public async Task<Response<CategoryDto>> GetByIdAsync(string id)
        {
            var category = await _categoryCollection.Find<Category>(x => x.Id == id).FirstOrDefaultAsync();

            return Response<CategoryDto>.Success(_mapper.Map<CategoryDto>(category), 200);
        }

        public async Task <Response<CategoryDto>> CreateAsync(CategoryCreateDto categoryCreateDto)
        {
            var newCategory = _mapper.Map<Category>(categoryCreateDto);

            await _categoryCollection.InsertOneAsync(newCategory);

            return Response<CategoryDto>.Success(_mapper.Map<CategoryDto>(newCategory), 200);
        }

        public async Task<Response<NoContents>> UpdateAsync(CategoryUpdateDto categoryUpdateDto)
        {
            var updateCategory = _mapper.Map<Category>(categoryUpdateDto);
            var result = await _categoryCollection.ReplaceOneAsync(x => x.Id == categoryUpdateDto.Id, updateCategory);

            if (result.ModifiedCount > 0)
            {
                return Response<NoContents>.Success(204);
            }
            return Response<NoContents>.Fail("Category not found", 404);
        }

        public async Task<Response<NoContents>> DeleteAsync(string id)
        {
            var result = await _categoryCollection.DeleteOneAsync(x => x.Id == id);

            if (result.DeletedCount > 0)
            {
                return Response<NoContents>.Success(204);
            }
            return Response<NoContents>.Fail("Category not found", 404);
        }
    }
}
