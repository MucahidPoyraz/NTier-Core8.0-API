using AutoMapper;
using BL.Abstract;
using Common.Enums;
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

        // ✅ Tüm Kategorileri Getir
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? filter, [FromQuery] string[]? includeProperties)
        {
            // ✅ Predicate Oluştur (Filtreleme)
            Expression<Func<Category, bool>> predicate = !string.IsNullOrEmpty(filter) ? c => c.Name.Contains(filter) : null;

            // ✅ Include Properties'i Dinamik Olarak İşle
            var includeExpressions = includeProperties?
                .Select(CreateIncludeExpression)
                .Where(expression => expression != null)
                .ToArray();

            // ✅ Veriyi Servisten Çek
            var response = await _categoryManager.GetAllAsync(predicate, includeExpressions);
            if (response.ResponseType != ResponseType.Success) return BadRequest(response.Message);

            return Ok(_mapper.Map<List<CategoryDto>>(response.Data));
        }

        // ✅ ID'ye Göre Kategori Getir (Include Destekli)
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id, [FromQuery] string[] includeParams)
        {
            var includeExpressions = includeParams?
                .Select(CreateIncludeExpression)
                .Where(expression => expression != null)
                .ToArray();

            var response = await _categoryManager.GetByGuidAsync(id, includeExpressions);
            if (response.ResponseType != ResponseType.Success) return BadRequest(response.Message);
            if (response.Data == null) return NotFound("Kategori bulunamadı.");

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

            return CreatedAtAction(nameof(GetById), new { id = response.Data.Id }, response.Data);
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

            return Ok(dto);
        }

        // ✅ Kategori Sil
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _categoryManager.DeleteAsync(id);
            if (response.ResponseType == ResponseType.NotFound) return NotFound("Kategori bulunamadı.");
            if (response.ResponseType != ResponseType.Success) return BadRequest(response.Message);

            return Ok("Kategori başarıyla silindi.");
        }

        // ✅ Kategori Var mı? (Filtre ile)
        [HttpGet("any")]
        public async Task<IActionResult> Any([FromQuery] string name)
        {
            if (string.IsNullOrEmpty(name)) return BadRequest("Name parameter is required.");

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
            catch
            {
                return null;
            }
        }
    }
}
