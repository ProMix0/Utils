# Utils
 
Set of my helpers

## DI utils

### AddTypeAndImplementation

Register service both as TService and TImplementation with `transient` lifetime

```csharp
IServiceCollection services = new();
services.AddTypeAndImplementation<IInterface, Service>();
```

### NotEndingBackgroundService

Wrapper for [`BetterHostedServices.CriticalBackgroundService`](https://github.com/GeeWee/BetterHostedServices)
Allows you to forget about `IApplicationEnder`

```csharp
class Service : NotEndingCriticalBackgroundService
{
    protected override Task Execute(CancellationToken token)
    {
        // Do stuff
    }
    
    protected override void OnError(Exception exception)
    {
        // Handle exception
    }
}
```

## Logging utils

### LogExceptionMessage

Allows you to fluent log exception before throw it. Default `LogLevel` is `Critical`

```csharp
throw new ArgumentException().LogExceptionMessage(logger);

throw new ArgumentException().LogExceptionMessage(logger, LogLevel.Debug);
```

## Options utils

### AddOptions

Overload that take Action<OptionBuilder> for more useful calls chaining

```csharp
services
.AddOptions<CustomizableField.Configuration>(builder =>
    builder
        .BindConfiguration(CustomizableField.Configuration.SectionName)
        .Validate(CustomizableField.Configuration.Validate))
.AddOptions<AiPlayer.AiPlayerBehaviour>(builder =>
    builder
        .BindConfiguration(AiPlayer.AiPlayerBehaviour.SectionName));
```