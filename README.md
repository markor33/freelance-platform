# Freelancer platform
Freelancer platform is a Minimal Viable Product (MVP) developed for educational purposes. This project is a freelance job marketplace platform designed to demonstrate the implementation of microservices within the .NET ecosystem. It allows clients to post jobs and hire freelancers, while freelancers can showcase their skills and experiences, browse available jobs, and submit proposals.

<i><b>Note:</b> The primary intent of this project is to serve as a learning resource for understanding and implementing microservices in .NET. As such, while it does possess the functionality of a freelance platform, it may not include all the features found in a fully-fledged commercial product.</i>

## Architecture overview
Each function of the platform is decoupled into several autonomous microservices, each with their own data and isolated responsibilities.

Each microservice in our architecture has its own implementation approach. Some services follow a simple CRUD model (also known as the anemic domain model), while others are designed using Domain-Driven Design (DDD) and Command Query Responsibility Segregation (CQRS) patterns.

Communication between microservices is achieved asynchronously using an event bus, with RabbitMQ as the underlying messaging system. This ensures that all our services can communicate effectively and that they remain loosely coupled, promoting a more maintainable and resilient system architecture.

The following diagram provides an rough overview of the architecture:

![Architecture Overview](architecture-overview.png)
