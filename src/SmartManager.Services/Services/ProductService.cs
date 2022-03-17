using System.Linq.Expressions;
using AutoMapper;
using SmartManager.Core.Exceptions;
using SmartManager.Domain.Entities;
using SmartManager.Entities;
using SmartManager.Infra.Interfaces;
using SmartManager.Services.DTOS;
using SmartManager.Services.Interfaces;
using SmartManager.Services.Requests;

namespace SmartManager.Services.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Product> CreateAsync(CreateProductRequest request) {
            var productEntity = _mapper.Map<Product>(request);

            return await _repository.Create(productEntity);
        }

        public async Task<Product> UpdateAsync(UpdateProductRequest request)
        {
            var productFind = await _repository.Get(request.Id);    

            if(productFind == null) {
                throw new DomainException("O produto informado não existe");
            }

            var productUpdated = await _repository.Update(_mapper.Map<Product>(request));

            return productUpdated;
        }
    }
}