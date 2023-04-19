<div align="center">

[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=ozzShpigel_MicroserviceDemo&metric=alert_status)](https://sonarcloud.io/dashboard?id=ozzShpigel_MicroserviceDemo)
&nbsp;
![.NET 6.0](https://img.shields.io/badge/Version-.NET%206.0-informational?style=flat&logo=dotnet)
&nbsp;
![Built With Docker](https://img.shields.io/badge/Built_With-Docker-informational?style=flat&logo=docker) 
&nbsp;
[![CI Build](https://github.com/ozzshpigel/MicroserviceDemo/actions/workflows/ci.yaml/badge.svg)](https://github.com/ozzshpigel/MicroserviceDemo/actions/workflows/ci.yml)
&nbsp;
[![FOSSA Status](https://app.fossa.com/api/projects/custom%2B37012%2Fgithub.com%2FozzShpigel%2FMicroserviceDemo.svg?type=shield)](https://app.fossa.com/projects/custom%2B37012%2Fgithub.com%2FozzShpigel%2FMicroserviceDemo?ref=badge_shield)
&nbsp;
[![codecov](https://codecov.io/github/ozzShpigel/MicroserviceDemo/branch/codecov/graph/badge.svg?token=X1J2W85RC1)](https://codecov.io/github/ozzShpigel/MicroserviceDemo)

</div>

> **The main idea of creating this project is implementing an infrastructure for up and running distributed system with the latest technology and architecture like CQRS, DDD, RabbitMq, and we will not deal mainly with business.** 🚀

# Table of Contents

- [Table of Contents](#table-of-contents)
  - [The Goals of This Project](#the-goals-of-this-project)
  - [Plan](#plan)
  - [:heart: Technologies - Libraries](#heart-technologies---libraries)
  - [The Domain And Bounded Context - Service Boundary](#the-domain-and-bounded-context---service-boundary)
  - [How to build development environment on minikube :](#how-to-build-development-environment-on-minikube-)
- [Support](#support)
  - [License](#license)

## The Goals of This Project

- :sparkle: Using `Domain Driven Design (DDD)` to implement all business processes in microservices.
- :sparkle: Using `Rabbitmq` for `Event Driven Architecture` between our microservices.
- :sparkle: Using `CQRS` implementation with `MediatR` library.
- :sparkle: Using `Fluent Validation` and a `Validation Pipeline Behaviour` on top of `MediatR`.
- :sparkle: Using `Docker-Compose` for our deployment mechanism.
- :sparkle: Using `GitHub Actions` for automation processes.
- :sparkle: Using `Terraform` for buiding the environment.

## Plan

> 🌀This project is a work in progress, new features will be added over time.🌀

I will try to register future goals and additions in the [Issues](https://github.com/ozzshpigel/MicroserviceDemo/issues) section of this repository.

High-level plan is represented in the table

| Feature         | Status      |
| --------------- | ----------- |
| Cusomer Service | Completed ✔️ |
| Order Service   | Completed ✔️ |

## :heart: Technologies - Libraries

- ✔️ **[`.NET 6`](https://dotnet.microsoft.com/download)** - .NET Framework and .NET Core, including ASP.NET and ASP.NET Core
with ASP.NET Web API, and ASP.NET Core
- ✔️ **[`EF Core`](https://github.com/dotnet/efcore)** - Modern object-database mapper for .NET. It supports LINQ queries, change tracking, updates, and schema migrations
- ✔️ **[`MediatR`](https://github.com/jbogard/MediatR)** - Simple, unambitious mediator implementation in .NET.
- ✔️ **[`FluentValidation`](https://github.com/FluentValidation/FluentValidation)** - Popular .NET validation library for building strongly-typed validation rules
- ✔️ **[`Swagger & Swagger UI`](https://github.com/domaindrivendev/Swashbuckle.AspNetCore)** - Swagger tools for documenting API's built on ASP.NET Core
Net Core
- ✔️ **[`xUnit.net`](https://github.com/xunit/xunit)** - A free, open source, community-focused unit testing tool for the .NET Framework.

## The Domain And Bounded Context - Service Boundary

- `Customer Service`: The Customer Service is a bounded context `CRUD` service to handle customer related operations.

- `Order Service`: The Order Service is a bounded context `CRUD` service to handle order related operations.

## How to build development environment on minikube :

Start Minikube:

```
minikube start
```

Build the docker images:

```
docker build -f "./CustomerApi/CustomerApi/Dockerfile" --force-rm -t  customer-app:latest "./CustomerApi"
docker build -f "./OrderApi/OrderApi/Dockerfile" --force-rm -t  order-app:latest "./OrderApi"
```

Load the local images into the minikube cluster:

```
minikube image load customer-app:latest
minikube image load order-app:latest
```

Then enter to helm repository and install the charts:

```
helm install release .\charts\ -f .\charts\values.yaml
```

For applying access all the endpoints with urls :

```
kubectl port-forward svc/customer-api-service 3001:8080
kubectl port-forward svc/order-api-service 3002:8080
minikube tunnel
```

Then access endpoints with urls :

```
http://localhost:3001/swagger/index.html
http://localhost:3002/swagger/index.html
http://localhost:1433
localhost,81
localhost,82
```

# Support

If you like my work, feel free to:

- ⭐ this repository. And we will be happy together :)

Thanks a bunch for supporting me!

## License

The code in this project is released under the [MIT License](LICENSE).

[![FOSSA Status](https://app.fossa.com/api/projects/custom%2B37012%2Fgithub.com%2FozzShpigel%2FMicroserviceDemo.svg?type=large)](https://app.fossa.com/projects/custom%2B37012%2Fgithub.com%2FozzShpigel%2FMicroserviceDemo?ref=badge_large)