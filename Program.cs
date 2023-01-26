global using CSharpFunctionalExtensions;

var customerRepository = new CustomerRepository();
var customerFactory = new CustomerFactory();
var customerMapper = new CustomerMapper();

var createCustomerRequestHandler = new CreateCustomerRequestHandler(
    customerFactory, 
    customerRepository,
    customerMapper,
    new EventPublisher());

var createCustomerRequest = new CreateCustomerRequest
{
    Id = Guid.NewGuid(),
    Name = "Chico Science Vive"
};

var result = await createCustomerRequestHandler.Handle(createCustomerRequest);

Console.Write(result.IsSuccess ? result.Value : result.Error);