using AutoMapper;

public class CustomerMapper
{
    public IMapper Mapper;
    public CustomerMapper()
    {
        var configurarion = new MapperConfiguration(c =>
        {
            c.CreateMap<CreateCustomerRequest, CustomerAggregate>().ReverseMap();
            c.CreateMap<CustomerAggregate, CustomerResponse>().ReverseMap();
            c.CreateMap<CustomerAggregate, CustomerCreatedEvent>();
        });
        Mapper = configurarion.CreateMapper();
    }
}