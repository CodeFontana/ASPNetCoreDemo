# Welcome
This repository serves as a demo of various ASP.NET project types, as a starting point for understanding their differences:
- Razor Pages
- MVC
- Web API
- Blazor Server
- Blazor Client

## The Project
A simple food ordering system, demonstrating the basic CRUD actions in each project type.

## Angle of Approach
Like any other good scientific experiment, there's a scientific control here-- the same food ordering system and CRUD actions are being implemented in each of the project types. This enables us to see the similarities and differences between these project types more clearly and concisely. 

## Data Access Library
All projects share the exact same Data Access library project, which is implemented using Dapper to access a Microsoft SQL database. You could feel free to replace the database access with your favorite SQL or NoSQL DBMS, but that's not really the point of this repo. Though by good design principle, the Data Access is properly done in a separate Library project, so supporting your favorite SQL could be done fairly easily.

## Credit, Where Credit is Due
This code was created from the "Getting Started with ASP.NET Core", by Tim Corey. Check out https://www.iamtimcorey.com/, I highly recommend this course to anyone interesting in taking your C# to the World Wide Web.
