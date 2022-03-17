using SmartManager.API.Response;
using SmartManager.Domain.Entities;
using SmartManager.Entities;
using SmartManager.Services.DTOS;
using SmartManager.Services.Requests;

namespace SmartManager.Services.Interfaces
{
    public interface IProductService {
        Task<Product> CreateAsync(CreateProductRequest request);

        Task<Product> UpdateAsync(UpdateProductRequest request);
    }
}