# CrudApp

This project was generated with [Angular CLI](https://github.com/angular/angular-cli) version 9.0.3.

## Development server Front-ed Angular

Run `ng serve` for a dev server. Navigate to `http://localhost:4200/`. The app will automatically reload if you change any of the source files.

## Install @angular-devkit/build-angular

If you have any problems or something like "Cannot find module '@angular-devkit/build-angular/package.json'"

type: npm install --save-dev @angular-devkit/build-angular

## Development server .Net Core Web API
Open visual studio and start the WEB API, the swagger documentation page will open.

## Development Postgres Database
Open the database.txt file to get the script of creation

## Fix .Net Core Web API commum error about assembly

If you have any problems about "[assembly: Microsoft.AspNetCore.Identity.UI.UIFrameworkAttribute("Bootstrap4")]"
Comment that code line in AssemblyInfo.cs

## Some advirtisements
All non required fields were configured refering the batch file.
The .Net Core Web API is configured to accept request from local host and specific front end port if you have something to do about it, open Startup.cs file and change for your necessity.
Is configured a general filter for exception so if you have any problem you can update the Startup.cs
The application is configured to use "postgres" user and password "123" if you want to change go to appsettings.json file


## Used concepts and techniques

DDD
          Rich Domain Objects
          Pure Domain
          Layered Architecture
          Layer responsability separation
          Services
          Domain Services
Unit tests
Aspect programming null checking using Fody/NullGuard nuget package
Unit of Work pattern
Explicity optional/null object using Maybe class
Functional programming
	Pure Functions
	No side effect
	Ummutability (Log Entity)
Dependency Injection
Avoiding exception flow control using Result.cs
Assyncronous Web API and methods that offers a better user experience
Application layer, adapters e view models to separate Domain Entites from end point data models
Swagger to WEB API documentation




