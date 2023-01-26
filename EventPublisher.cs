public class EventPublisher
{
    public Task<Result> Publish(CustomerCreatedEvent @event) 
        => Task.FromResult(Result.Success());
}