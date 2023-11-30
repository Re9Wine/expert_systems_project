using AutoMapper;
using PocketBook.Domain.DTOs;
using PocketBook.Domain.Entities;
using PocketBook.Domain.Requests.MoneyTransactionRequests;

namespace PocketBook.BLL.Mappings.Profiles;

public class MoneyTransactionMapperConfiguration : Profile
{
    public MoneyTransactionMapperConfiguration()
    {
        CreateMap<CreateMoneyTransactionRequest, MoneyTransaction>()
            .ForMember(member => member.Date,
                expression => expression.MapFrom(_ => DateTime.UtcNow));

        CreateMap<UpdateMoneyTransactionRequest, MoneyTransaction>();

        CreateMap<MoneyTransaction, MoneyTransactionDTO>()
            .ForMember(member => member.Category,
                expression => expression.MapFrom(source => source.TransactionCategory.Name));

        CreateMap<IGrouping<DateTime, MoneyTransaction>, BarCharDTO>()
            .ForMember(member => member.Date, expression =>
            {
                expression.PreCondition(source => source.Any());
                expression.MapFrom(source => DateOnly.FromDateTime(source.Key));
            })
            .ForMember(member => member.Sum, expression =>
            {
                expression.PreCondition(source => source.Any());
                expression.MapFrom(source => source.Sum(transaction => transaction.Value));
            });

        CreateMap<IGrouping<string, MoneyTransaction>, DoughnutDTO>()
            .ForMember(member => member.Category, expression =>
            {
                expression.PreCondition(source => source.Any());
                expression.MapFrom(source => source.Key);
            })
            .ForMember(member => member.Sum, expression =>
            {
                expression.PreCondition(source => source.Any());
                expression.MapFrom(source => source.Sum(transaction => transaction.Value));
            });
    }
}