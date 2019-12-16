# dotnet.core.templates #

Code examples for following posts:

* <a href="https://automationrhapsody.com/create-project-for-net-core-custom-template/">Create project for .NET Core custom template</a>
* <a href="https://automationrhapsody.com/optimize-the-size-of-docker-images/">Optimize the size of Docker images</a>
* <a href="https://automationrhapsody.com/create-net-core-health-checks-with-custom-response-payload/">Create .NET Core health checks with custom response payload</a>
* <a href="https://automationrhapsody.com/serialize-and-deserialize-enum-values-to-custom-string-in-c-with-json-net/">Serialize and deserialize enum values to custom string in C# with Json.NET</a>



## microservice ##
This is custom .NET Core 3.0 application template that has health checks, AWS SQS functionality, unit and integration tests.

### Install ###

dotnet new -i <PATH_TO_FOLDER>

### Create Project ###

dotnet new microservice --ProjectName SampleMicroservice

### Uninstall ###

dotnet new -u <PATH_TO_FOLDER>

dotnet new --debug:reinit