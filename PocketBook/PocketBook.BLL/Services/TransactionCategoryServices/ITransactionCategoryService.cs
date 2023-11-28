using PocketBook.Domain.DTOs;
using PocketBook.Domain.Requests.TransactionCategoryRequests;

namespace PocketBook.BLL.Services.TransactionCategoryServices;

public interface ITransactionCategoryService
{
    Task<TransactionCategoryDTO?> CreateAsync(CreateTransactionCategoryRequest createCategoryRequest);
    Task<bool> UpdateAsync(UpdateTransactionCategoryRequest updateCategoryRequest);
    Task<bool> DeleteAsync(string name);
    Task<List<TransactionCategoryDTO>> GetByTypeAsync(bool isConsumption);
}