public class CustomerRepository
{
    private readonly List<CustomerAggregate> _customerAggregates = new();
    public async Task<Result<CustomerAggregate>> SaveAsync(CustomerAggregate aggregate)
    {
        if (_customerAggregates.Any(a => a.Id == aggregate.Id))
            return Result.Failure<CustomerAggregate>("duplicated stuff");
        
        _customerAggregates.Add(aggregate);
        
        return aggregate;
    }
    
    public Task<Result<CustomerAggregate>> LoadByIdAsync(Guid id) =>
        id == Guid.Empty
            ? Task.FromResult(Result.Failure<CustomerAggregate>("repository not found"))
            : Task.FromResult(Result.Success(new CustomerAggregate
            {
                Id = id,
                Name = $"Customer Name for {id}"
            }));
}