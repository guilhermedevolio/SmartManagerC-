using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartManager.Core.Exceptions;
using SmartManager.Services.DTOS;
using SmartManager.Services.Interfaces;
using SmartManager.Services.Requests;
using SmartManager.Services.Services;

namespace SmartManager.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase {
        
        private readonly IProductService _service;
        private readonly IMapper _mapper;

        public ProductController(IProductService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> createProduct([FromBody] CreateProductRequest request) {
            try {
                var create = await _service.CreateAsync(request);
                return Ok(new {
                    Status = 0,
                    Message = "Produto criado com sucesso"
                });
            } catch (DomainException ex) {
                return Ok(new {
                    Status = 1,
                    Message = ex.Message
                });
            }
        }


        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> updateProduct(UpdateProductRequest request) {
            try {
                var update = await _service.UpdateAsync(request);
                return Ok(new {
                    Status = 0,
                    Message = "Produto atualizado com sucesso"
                });
            } catch (DomainException ex) {
                return Ok(new {
                    Status = 1,
                    Message = ex.Message
                });
            }
        }

    }
}