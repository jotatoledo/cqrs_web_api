# CQRS Example with .NET WebAPI

> ### ASP.NET WebAPI codebase containing a showcase project used for a workshop at Bosch

The codebase was created to demonstrate a fully fledged fullstack application built with ASP.NET WebAPI (with Feature orientation) including CRUD operations, routing, pagination, and more.

# Project introduction

As an introduction presentation for this project the following [Prezi presentation](https://prezi.com/p/j68uj1msnftw/) was used.
For an extensive explanation of the libraries, frameworks and techniques used in this repo please take a look in it.

# How it works

This is using ASP.NET WebAPI with:

- CQRS and [MediatR](https://github.com/jbogard/MediatR)
    - [Simplifying Development and Separating Concerns with MediatR](https://blogs.msdn.microsoft.com/cdndevs/2016/01/26/simplifying-development-and-separating-concerns-with-mediatr/)
    - [CQRS with MediatR and AutoMapper](https://lostechies.com/jimmybogard/2015/05/05/cqrs-with-mediatr-and-automapper/)
    - [Thin Controllers with CQRS and MediatR](https://codeopinion.com/thin-controllers-cqrs-mediatr/)
- [AutoMapper](https://github.com/AutoMapper/AutoMapper)
- [Entity Framework](https://github.com/aspnet/EntityFramework6) on Microsoft SQL Server for demo purposes.  Can easily be anything else EF supports.
- [AutoMapper EF6 Extensions](https://github.com/AutoMapper/AutoMapper.EF6)
- Built-in Swagger via [Swashbuckle for .NET WebAPI](https://github.com/domaindrivendev/Swashbuckle)

This basic architecture is based on this reference architecture for .NET Core: [https://github.com/gothinkster/aspnetcore-realworld-example-app](https://github.com/gothinkster/aspnetcore-realworld-example-app)