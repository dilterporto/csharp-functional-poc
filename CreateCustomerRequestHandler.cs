public class CreateCustomerRequestHandler
{
    private readonly CustomerFactory _customerFactory;
    private readonly CustomerRepository _customerRepository;
    private readonly CustomerMapper _customerMapper;
    private readonly EventPublisher _eventPublisher;

    public CreateCustomerRequestHandler(
        CustomerFactory customerFactory, 
        CustomerRepository customerRepository,
        CustomerMapper customerMapper,
        EventPublisher eventPublisher)
    {
        _customerFactory = customerFactory;
        _customerRepository = customerRepository;
        _customerMapper = customerMapper;
        _eventPublisher = eventPublisher;
    }
    
    public async Task<Result<CustomerResponse>> Handle(CreateCustomerRequest request) =>
        await CreateCustomerAsync(request)
            .Check(ApplySomeBehavior)
            .Check(SaveAsync)
            .Check(PublishEventAsync)
            .Finally(CreateResponse);

    private Result<CustomerAggregate> CreateCustomerAsync(CreateCustomerRequest request)
        => _customerFactory.CreateAsync(request)
            .ToResult("error on factory creating customer aggregate")
            .Ensure(customer => customer.Active, "customer is inactive");
    private Result ApplySomeBehavior(CustomerAggregate customerAggregate) 
        => customerAggregate.ApplySomeBehavior("some note there");

    private Result<CustomerResponse> CreateResponse(Result<CustomerAggregate> result) =>
        result.IsSuccess
            ? _customerMapper.Mapper.Map<CustomerResponse>(result.Value)
            : Result.Failure<CustomerResponse>(result.Error);

    private Task<Result<CustomerAggregate>> SaveAsync(CustomerAggregate customerAggregate) 
        => _customerRepository.SaveAsync(customerAggregate);

    private Task<Result> PublishEventAsync(CustomerAggregate customerAggregate) 
        => _eventPublisher.Publish(_customerMapper.Mapper.Map<CustomerCreatedEvent>(customerAggregate));
}