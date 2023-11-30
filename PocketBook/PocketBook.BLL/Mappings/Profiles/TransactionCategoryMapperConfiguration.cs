using AutoMapper;
using PocketBook.Domain.DTOs;
using PocketBook.Domain.Entities;
using PocketBook.Domain.Requests.TransactionCategoryRequests;

namespace PocketBook.BLL.Mappings.Profiles;

public class TransactionCategoryMapperConfiguration : Profile
{
    public TransactionCategoryMapperConfiguration()
    {
        CreateMap<CreateTransactionCategoryRequest, TransactionCategory>()
            .ForMember(member => member.IsConsumption,
                expression => expression.MapFrom(_ => true))
            .ForMember(member => member.IsChangeable,
                expression => expression.MapFrom(_ => true));

        CreateMap<UpdateTransactionCategoryRequest, TransactionCategory>()
            .ForMember(member => member.IsChangeable,
                expression => expression.MapFrom(_ => true))
            .ForMember(member => member.IsConsumption,
                expression => expression.MapFrom(_ => true));

        CreateMap<TransactionCategory, TransactionCategoryDTO>();
    }
}