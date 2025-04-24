using AutoMapper;
using Common.Enums;
using Common.Models;
using DTOs.BlogDtos;
using Entity;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private readonly IGenericManager<Blog> _blogManager;
        private readonly IMapper _mapper;

        public BlogsController(IGenericManager<Blog> blogManager, IMapper mapper)
        {
            _blogManager = blogManager;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] int? id,
            [FromQuery] int? categoryId,
            [FromQuery] DateTime? createdAfter,
            [FromQuery] string[]? includeProperties,
            [FromQuery] OrderType? orderType = OrderType.ASC,
            [FromQuery] int pageIndex = 1,  // Sayfa indeksini 1 olarak varsayıyoruz
            [FromQuery] int pageSize = 10   // Sayfa başına 10 eleman varsayıyoruz
        )
        {
            // Dinamik Filtreleme (Predicate)
            Expression<Func<Blog, bool>> predicate = x =>
                (!id.HasValue || x.Id == id.Value) &&
                (!categoryId.HasValue || x.CategoryId == categoryId.Value) &&
                (!createdAfter.HasValue || x.CreatedAt > createdAfter.Value);

            // IncludeProperty'leri Dinamik Olarak Oluştur
            var includeExpressions = includeProperties?
                .Select(CreateIncludeExpression)
                .Where(expression => expression != null)
                .ToArray();

            // Veriyi Getir (Sayfalama işlemi ile)
            var response = await _blogManager.GetPaginatedAsync(
                pageIndex, pageSize, predicate, x => x.CreatedAt,true, includeExpressions);

            if (response.ResponseType != ResponseType.Success)
                return BadRequest(response.Message);

            // Sıralama işlemi: response zaten sıralı gelecek.
            return Ok(response.Data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _blogManager.GetAllAsync(x => x.Id == id);
            if (response.ResponseType != ResponseType.Success)
                return NotFound("Blog bulunamadı.");

            return Ok(response.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBlogDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var blog = _mapper.Map<Blog>(dto);
            var response = await _blogManager.AddAsync(blog);
            if (response.ResponseType != ResponseType.Success)
                return BadRequest(response.Message);

            return CreatedAtAction(nameof(GetById), new { id = blog.Id }, response.Data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateBlogDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (id != dto.Id) return BadRequest("URL'deki ID ile gövdedeki ID eşleşmiyor.");

            var blog = _mapper.Map<Blog>(dto);
            var response = await _blogManager.UpdateAsync(blog);
            if (response.ResponseType != ResponseType.Success)
                return BadRequest(response.Message);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleteEntity = await _blogManager.GetAsync(x => x.Id == id);
            if (deleteEntity.ResponseType == ResponseType.Success)
            {
                var response = await _blogManager.DeleteAsync(deleteEntity.Data);
                if (response.ResponseType == ResponseType.NotFound)
                    return NotFound("Blog bulunamadı.");
                if (response.ResponseType != ResponseType.Success)
                    return BadRequest(response.Message);

                return Ok("Blog başarıyla silindi.");
            }
            else
            {
                return BadRequest(deleteEntity.Message);
            }
        }

        // IncludeProperty'leri Expression'a Çeviren Yardımcı Metot
        private static Expression<Func<Blog, object>> CreateIncludeExpression(string propertyName)
        {
            var parameter = Expression.Parameter(typeof(Blog), "b");
            var property = Expression.PropertyOrField(parameter, propertyName);

            return Expression.Lambda<Func<Blog, object>>(
                Expression.Convert(property, typeof(object)), parameter);
        }
    }
}
