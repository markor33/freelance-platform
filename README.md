# Freelancer platform
Freelancer platform is a Minimal Viable Product (MVP) developed for educational purposes. This project is a freelance job marketplace platform designed to demonstrate the implementation of microservices within the .NET ecosystem. It allows clients to post jobs and hire freelancers, while freelancers can showcase their skills and experiences, browse available jobs, and submit proposals.

<i><b>Note:</b> The primary intent of this project is to serve as a learning resource for understanding and implementing microservices in .NET. As such, while it does possess the functionality of a freelance platform, it may not include all the features found in a fully-fledged commercial product.</i>

## Architecture overview
Each function of the platform is decoupled into several autonomous microservices, each with their own data and isolated responsibilities.

Each microservice in our architecture has its own implementation approach. Some services follow a simple <b>CRUD</b> model (also known as the anemic domain model), while others are designed using <b>Domain-Driven Design (DDD)</b> and <b>Command Query Responsibility Segregation (CQRS)</b> patterns.

<b>Communication</b> between microservices is achieved <b>asynchronously</b> using an event bus, with <b>RabbitMQ</b> as the underlying messaging system. This ensures that all our services can communicate effectively and that they remain loosely coupled, promoting a more maintainable and resilient system architecture.<br>
Synchronous communication (<b>gRPC</b>) is used in the <b>API gateway</b> or <b>Backends for Frontends (BFF)</b> for cases where data aggregation from several services is needed.

The following diagram provides an rough overview of the architecture:

![Architecture Overview](architecture-overview.png)

## Services

### Identity
The Identity Service serves as a centralized hub for managing authentication and authorization. This crucial service is built on top of the <b>IdentityServer4</b> framework and utilizes <b>ASP.NET Identity</b> for user management.

<br>

### ClientProfile/Employer
The Client profile/Employer service is a simple service for client/employer profile management.

<br>

### Freelancer
The Freelancer Service manages the profile details of freelancers, including profile summaries, education, certifications, employment history, ...
It's built using <b>Domain-Driven Design (DDD)</b> principles, <b>Clean Architecture</b>, and the <b>CQRS</b> pattern.<br><br>
Additionally, Freelancer aggregate is implemented using <b>Event Sourcing</b> pattern.<br>
Both data models are supported and always consistent no metter which aggregate loading strategy is set.<br>
&nbsp;&nbsp;<i><b>Note</b>: Aggregate loading strategy is configured in appsettings.json, section <b>LoadingStrategy</b></i><br><br>
To get faster reads and easier scalability <b>MongoDB</b> is used for read model, profiles are aggregated into single document.<br>

<br>

### Job
The Job Service orchestrates the job pipeline: job creation, proposal submission, acceptance of a proposal (contract creation), to finalizing the job.
It's built using <b>Domain-Driven Design (DDD)</b> principles, <b>Clean Architecture</b>, and the <b>CQRS</b> pattern.<br><br>
Job aggregate is also implemented using <b>Event Sourcing</b> pattern.

<br>

### Feedback
The Feedback Service allows clients and freelancers to provide feedback upon completion of a contract.

<br>

### Chat/Notifications
Service that provides simple chat and notification system. 
It's built using <b>SignalR</b> for real-time communication and <b>MongoDB</b> for data storage.

<br>

### API Gateway/BFF
This service functions as an API Gateway using Ocelot and as a Backends-for-Frontends (BFF) for efficient data aggregation. In other cases, it simply routes requests to the relevant service.
