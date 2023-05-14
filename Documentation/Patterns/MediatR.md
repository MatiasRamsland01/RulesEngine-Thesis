# Mediator Behavioral Design Pattern

## Core

In simple terms, a mediator could also be explained as a man-in-the-middle. A mediator is used to restrict the direct communications between objects and only let them cooperate via a mediator object.

## Why use the Mediator pattern

The mediator approach reduces tight couplings between objects, which makes code easier to test, change and debug. The use of a mediator also reduces the dependencies in each class, as the the only dependency each class have is the mediator.

## Without a mediator

We can illustrate this using an airport as a real world example:

```mermaid
%%{
  init: {
    'theme': 'base',
    'themeVariables': {
      'primaryColor': '#6495ED',
      'primaryTextColor': '#fff',
      'primaryBorderColor': '#7C0000',
      'lineColor': '#F8B229',
      'secondaryColor': '#006100',
      'tertiaryColor': '#fff'
    }
  }
}%%
flowchart TB
subgraph Airport
    subgraph Air
    FreightJetPlane <--> PassengerJetPlane
    PrivatePropellerPlane <--> PassengerJetPlane
    FreightJetPlane <--> PrivatePropellerPlane
    end
    subgraph Ground
    FuelTruck <--> PassengerJetPlane
    FuelTruck <--> FreightJetPlane
    FuelTruck <--> PrivatePropellerPlane
    EmergencyVehicle <--> PassengerJetPlane
    EmergencyVehicle <--> FreightJetPlane
    EmergencyVehicle <--> PrivatePropellerPlane
    BaggageHandler <--> PassengerJetPlane
    BaggageHandler <--> FreightJetPlane
    BaggageHandler <--> PrivatePropellerPlane
    EmergencyVehicle <--> BaggageHandler
    end
    subgraph Exterior
    WeatherReports <--> PassengerJetPlane
    WeatherReports <--> PrivatePropellerPlane
    WeatherReports <--> FreightJetPlane
    end
end
```

From the airport example above using arrows as the dependencies, we can derive that is is not a very good system. On modern airports there is one or more controllers which monitors and directs all the different objects(vehicles) on the airport. Let us take a look at what it would look like if we were using a mediator like an airportcontroller.

## With a mediator

```mermaid
%%{init: {'theme':'dark'}}%%
flowchart TD

Controller <--> PassengerJetPlane & FreightJetPlane & PrivatePropellerPlane
Controller <--> FuelTruck & EmergencyVehicle & BaggageHandler
Controller <--> WeatherReports

subgraph Air
    PassengerJetPlane
    FreightJetPlane
    PrivatePropellerPlane
end

subgraph Ground
    FuelTruck
    EmergencyVehicle
    BaggageHandler
end

subgraph Ground
    FuelTruck
    EmergencyVehicle
    BaggageHandler
end

subgraph Exterior
    WeatherReports
end

```

The figure above illustrates much better coordination using a mediator. Every message has to go through the mediator which transfers the message to the correct object. In this instance the controller passes messages between vehicles and services on an airport.

## Structure

The structure of a mediator consists of components, the mediator interface and the concrete mediator. The components, also known as classes, contains reference to the mediator through the mediator interface. The concrete mediator encase the relations between the various classes/components. We can illustrate this using the airport example. The mediator interface is referred to as IAirportController while the concrete mediator is AirportController:

```mermaid
%%{init: {'theme':'dark'}}%%
classDiagram
direction RL

IAirportController <.. AirportController
IAirportController <-- EmergencyVehicle
IAirportController <-- WeatherReports
EmergencyVehicle <--* AirportController
PrivatePropellerPlane <--* AirportController
WeatherReports <--* AirportController
PassengerJetPlane <--* AirportController
IAirportController <-- PassengerJetPlane
IAirportController <-- PrivatePropellerPlane
class IAirportController{
    <<interface>>
    +Notify(sender)
}
class AirportController{
    -PassengerJetPlane
    -PrivatePropellerPlane
    -EmergencyVehicle
    -WeatherReports
    +Notify(sender)
    +DoSomethingWithPassengerJetPlane
    +DoSomethingWithPrivatePropellerPlane
    +DoSomethingWithEmergencyVehicle
    +DoSomethingWithWeatherReports
}

class PassengerJetPlane{
    -IMediator
    +OperationPassengerJetPlane()
}
class PrivatePropellerPlane{
    -IMediator
    +OperationPrivatePropellerPlane()
}
class EmergencyVehicle{
    -IMediator
    +OperationEmergencyVehicle()
}
class WeatherReports{
    -IMediator
    +OperationWeatherReports()
}
end
```

## MediatR Behaviors

```mermaid
%%{init: {'theme':'dark'}}%%
flowchart LR
    subgraph MediatR Behavior
        subgraph Pipeline
            subgraph MediatR
            direction TB
            Pre --> Next --> Post
            end
        end
    end
    A --> MediatR
    MediatR --> Logic
```

```mermaid
%%{init: {'theme':'dark'}}%%
flowchart LR
    Query(Get_Query_Request)
    Command(Post_Command_Request)
    QueryHandler(Handle_Query)
    CommandHandler(Handle_Command)
    subgraph MediatR
        
    end
    Query --> MediatR --> QueryHandler
    Command --> MediatR --> CommandHandler
```

## Implementation in project

(To be written..)
