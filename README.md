# MMT Challenge
## CQRS – Mediator 
CQRS and Mediator pattern are used to decouple command/query requests

## Clean Architecture
OrdersApi is developed using Clean Architecture with projects Domain, Application, Infrastructure, OrdersApi
- Domain has the application entities. Domain doesn’t know about any other layers
- Application is where  business logics are including Commands, Queries handlers, Interfaces, Mapping, Logging, Custom exceptions, and Validations. 
- Infrastructure is where all the integrations take place such as Integrations to CustomerDetailsApi, Persistence layer and Services if there are any. 
- OrdersApi is the entry point with its api endpoints. It has references to Application and Infrastructure layers to send commands, queries to correct handlers and receive responses and then returns appropriate responses. 

## Improvement 
- Security: We can use an api key as authentication of the incoming requests
- CustomerDetails ApiClient: Improve the error handling (exceptions, null response, etc), logging, e.g retry, timeout, etc
- RequestValidation: Validating customer is done inmemory (OrderDetailsQueryValidator) - This needs to be done probably to make this production ready
- Testing in general is lacking - I'd like to add much more testing and that includes IntegrationTest and unittests especially arround Request Validations, CustomerDetails ApiClient
- Logging: there is no logging at the moment but some examples are 
  -- Log all requests
  -- Log long running requests
  -- Log errors such as UnhandledExceptions
- Security: 
  -- Sensitive data such as Customer's address might need to be masked in test/dev environments 
  -- SSL for communication with endpoint
- Devops: work together with devops to integrate the api into existing CI/CD pipelines for different environments 
