# Microservice Development

## Quote from Dave Farley

[Twitter Thread on 4/25/2023](https://twitter.com/davefarley77/status/1650826218952970240)

Microservices are distinct from Services, although they are commonly confused. 
Services, as an approach to software design, have been around for many decades. Service Oriented design was popular in the 1990s and early 2000s.
The big difference is not the size, despite the name, it is the independence of the service from other services. 

Most definitions of Microservices include:
- Independently Deployable
- Loosely coupled
- Organised around business capabilities
- Owned by a small team

Notice how this definition says nothing about technology and completely focusses on something else, the degree to which these things are independent of one another, that is we can change a Microservice, without affecting other code that interacts with that service. 

If you think about it, all of these properties are focused on that.

They are "Owned by a small team" so that team can make progress without collaborating with others. 

They are "Organised around business capabilities" as a means of decoupling them naturally. We can change the SalesTax service independently of changing the CustomerRegistration service.

They are "Loosely coupled" so that we can make changes to one service without forcing changes on other services.

...and all of these things are mechanisms to allow us to achieve the last. 
Microservices are "independently deployable"

This allows each small team to make progress independently of others, and when they have made a change, they can release it without affecting others. 

Microservices are primarily designed to be an "organisational scalability" tool. 
They free large orgs to make progress in many small teams, with each team working separately from the others.

So if you build your microservice, but before you release it into production, you need to test it with the current version of all the other services, it *isn't a microservice* it's something else.

The whole idea here is to prevent this coordinated, in-step, process of change, where teams can only make progress in lock-step with other teams. 

"Independently deployable" is hard, but that is the game.

If you can't deploy your uServices independently, then you probably have "Services", and your services, however they are stored in repos, or built, are part of a monolithic system, because they need to be tested together before release.

Microservices are the most scalable way to build software, but if you don't need to scale, they are often a less efficient way to organise your work.

## Topics in this Class

### Actually Doing It
We are going to emphasize the process of actually building a small MSA.

### Tech Topics
#### Integrating with RPCS:
- HTTP as RPC
- gRPC for RPC

#### Observability
- OpenTelemetry

