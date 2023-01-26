public class CustomerAggregate
{
    public Guid Id { get; set; }
    public string? Name { get; init; }

    public bool Active { get; set; } = true;

    public Result ApplySomeBehavior(string notes) 
        => string.IsNullOrEmpty(notes) ? Result.Failure("error on applying some behavior") : Result.Success();
}