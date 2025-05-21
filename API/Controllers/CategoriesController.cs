using AutoMapper;
using Common.Enums;
using Common.Models;
using DTOs.CategoryDtos;
using Entity;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly IGenericManager<Category> _categoryManager;
        private readonly IMapper _mapper;

        public CategoriesController(IGenericManager<Category> categoryManager, IMapper mapper)
        {
            _categoryManager = categoryManager;
            _mapper = mapper;
        }

        // ✅ Tüm Kategorileri Getir (Sayfalama ve Sıralama ile)
        [HttpGet]
        public async Task<IActionResult> GetAll(
             [FromQuery] string? name,
             [FromQuery] DateTime? createdDate,
             [FromQuery] string[]? includeProperties,
             [FromQuery] int pageIndex = 1,
             [FromQuery] int pageSize = 10,
             [FromQuery] OrderType orderType = OrderType.ASC
         )
        {
            // Dinamik predicate oluşturuluyor
            Expression<Func<Category, bool>> predicate = c =>
                (string.IsNullOrEmpty(name) || c.Name.Contains(name)) &&
                (!createdDate.HasValue || c.CreatedAt > createdDate.Value);

            // Include işlemleri
            var includeExpressions = includeProperties?
                .Select(CreateIncludeExpression)
                .Where(expression => expression != null)
                .ToArray();

            // Sıralama Name alanına göre
            Expression<Func<Category, object>> orderBy = x => x.Name;

            var response = await _categoryManager.GetPaginatedAsync(
                pageIndex, pageSize, predicate, orderBy, orderType == OrderType.ASC, includeExpressions);

            if (response.ResponseType != ResponseType.Success)
                return BadRequest(response.Message);

            return Ok(response.Data);
        }

        // ✅ ID'ye Göre Kategori Getir (Include Destekli)
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id, [FromQuery] string[]? includeParams)
        {
            if (id <= 0) return BadRequest("Geçersiz kategori ID.");

            var includeExpressions = includeParams?
                .Select(CreateIncludeExpression)
                .Where(expression => expression != null)
                .ToArray();

            var response = await _categoryManager.GetAsync(x => x.Id == id, includeExpressions);
            if (response.ResponseType == ResponseType.NotFound) return NotFound("Kategori bulunamadı.");

            return Ok(_mapper.Map<CategoryDto>(response.Data));
        }

        // ✅ Yeni Kategori Ekle
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateCategoryDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var category = _mapper.Map<Category>(dto);
            var response = await _categoryManager.AddAsync(category);

            if (response.ResponseType != ResponseType.Success) return BadRequest(response.Message);

            var categoryDto = _mapper.Map<CategoryDto>(response.Data);
            return CreatedAtAction(nameof(GetById), new { id = categoryDto.Id }, categoryDto);
        }

        // ✅ Kategori Güncelle
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCategoryDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (id != dto.Id) return BadRequest("URL'deki ID ile gövdedeki ID eşleşmiyor.");

            var category = _mapper.Map<Category>(dto);
            var response = await _categoryManager.UpdateAsync(category);
            if (response.ResponseType != ResponseType.Success) return BadRequest(response.Message);

            return NoContent();
        }

        // ✅ Kategori Sil
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return BadRequest("Geçersiz kategori ID.");
            var deleteEntity = await _categoryManager.GetAsync(x => x.Id == id);
            if (deleteEntity.ResponseType == ResponseType.Success)
            {
                var response = await _categoryManager.DeleteAsync(deleteEntity.Data);
                if (response.ResponseType == ResponseType.NotFound) return NotFound("Kategori bulunamadı.");
                return Ok("Kategori başarıyla silindi.");
            }
            else
            {
                return NotFound("Kategori bulunamadı.");
            }
        }

        // ✅ Kategori Var mı? (Filtre ile)
        [HttpGet("any")]
        public async Task<IActionResult> Any([FromQuery] string name)
        {
            if (string.IsNullOrEmpty(name)) return BadRequest("Kategori adı zorunludur.");

            Expression<Func<Category, bool>> predicate = c => c.Name == name;
            var response = await _categoryManager.AnyAsync(predicate);
            if (response.ResponseType != ResponseType.Success) return BadRequest(response.Message);

            return Ok(response.Data);
        }

        // ✅ Dinamik Include Desteği İçin Expression Metodu
        private static Expression<Func<Category, object>> CreateIncludeExpression(string propertyName)
        {
            try
            {
                var parameter = Expression.Parameter(typeof(Category), "c");
                var property = Expression.PropertyOrField(parameter, propertyName);
                return Expression.Lambda<Func<Category, object>>(Expression.Convert(property, typeof(object)), parameter);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Invalid property: {propertyName}, Error: {ex.Message}");
                return null;
            }
        }
    }
}
