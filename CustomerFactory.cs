public class CustomerFactory
{
    public Maybe<CustomerAggregate> CreateAsync(CreateCustomerRequest? request) =>
        request is null || request.Id == Guid.Empty
            ? Maybe<CustomerAggregate>.None
            : new CustomerAggregate
            {
                Id = request.Id,
                Name = request.Name
            };
}