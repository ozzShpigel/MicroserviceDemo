<div align="center" style="margin-bottom:20px">
    <div align="center">
           <a href="https://github.com/ozzshpigel-organization/MicroserviceDemo/actions/workflows/ci.yml"><img alt="ci-status" src="https://github.com/ozzshpigel-organization/MicroserviceDemo/actions/workflows/ci.yaml/badge.svg"/></a>
    </div>
</div>

> **The main idea of creating this project is implementing an infrastructure for up and running distributed system with the latest technology and architecture like CQRS, DDD, RabbitMq, and we will not deal mainly with business.** π

# Table of Contents

- [The Goals of This Project](#the-goals-of-this-project)
- [Plan](#plan)
- [Technologies - Libraries](#technologies---libraries)
- [The Domain and Bounded Context - Service Boundary](#the-domain-and-bounded-context---service-boundary)
- [Structure of Project](#structure-of-project)
- [How to Run](#how-to-run)
  - [Docker Compose](#docker-compose)
  - [Kubernetes](#kubernetes)
- [Support](#support)
- [Contribution](#contribution)

## The Goals of This Project

- :sparkle: Using `Domain Driven Design (DDD)` to implement all business processes in microservices.
- :sparkle: Using `Rabbitmq` for `Event Driven Architecture` between our microservices.
- :sparkle: Using `CQRS` implementation with `MediatR` library.
- :sparkle: Using `Fluent Validation` and a `Validation Pipeline Behaviour` on top of `MediatR`.
- :sparkle: Using `Docker-Compose` and `Kubernetes` for our deployment mechanism.

## Plan

> πThis project is a work in progress, new features will be added over time.π

I will try to register future goals and additions in the [Issues](https://github.com/ozzshpigel-organization/MicroserviceDemo/issues) section of this repository.

High-level plan is represented in the table

| Feature           | Status         |
| ----------------- | -------------- |
| Cusomer Service  | Completed βοΈ   |
| Order Service    | Completed βοΈ   |

## :heart: Technologies - Libraries

- βοΈ **[`.NET 6`](https://dotnet.microsoft.com/download)** - .NET Framework and .NET Core, including ASP.NET and ASP.NET Core
with ASP.NET Web API, and ASP.NET Core
- βοΈ **[`EF Core`](https://github.com/dotnet/efcore)** - Modern object-database mapper for .NET. It supports LINQ queries, change tracking, updates, and schema migrations
- βοΈ **[`MediatR`](https://github.com/jbogard/MediatR)** - Simple, unambitious mediator implementation in .NET.
- βοΈ **[`FluentValidation`](https://github.com/FluentValidation/FluentValidation)** - Popular .NET validation library for building strongly-typed validation rules
- βοΈ **[`Swagger & Swagger UI`](https://github.com/domaindrivendev/Swashbuckle.AspNetCore)** - Swagger tools for documenting API's built on ASP.NET Core
Net Core
- βοΈ **[`xUnit.net`](https://github.com/xunit/xunit)** - A free, open source, community-focused unit testing tool for the .NET Framework.

## The Domain And Bounded Context - Service Boundary

- `Customer Service`: The Customer Service is a bounded context `CRUD` service to handle customer related operations.

- `Order Service`: The Order Service is a bounded context `CRUD` service to handle order related operations.

# Support

If you like my work, feel free to:

- β­ this repository. And we will be happy together :)

Thanks a bunch for supporting me!

## License
This project is made available under the MIT license. See [LICENSE](https://github.com/ozzShpigel/MicroserviceDemo/blob/main/LICENSE) for details.