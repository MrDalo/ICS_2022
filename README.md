# Introduction 
**Application name:** Travel Agency\
**OS:** Windows\
**Idle:** Visual Studio 2022\
**Development hosted:** Azure DevOps

ICS Travel Agency is a desktop app for Windows OS that serves to connect people offering share rides by car. This app was created as a project for subject C# seminar at FIT BUT. It was created in C# (Entity framework Core - DB, Fluent API, WPF - front-end) with using OOP principes/design patterns/concepts as factories, mappers, unit of work, facades, repositories, models, wrappers and design pattern MVVM.

## Team members
- Patrik Sehnoutek
- Marcel Feiler
- Dalibor Králik
- Ján Svitana
- Lukáš Chytil

## GUI
Mock-up was created in Figma.\
**Link to prototype:** https://bit.ly/3PzUWjo 

## Build and Test
Automatic tests in Azure DevOps in the form of CI pipeline.

Tests are located in three different folders:
- BL.Tests (tests for business layer)
- DAL.Tests (tests for data access layer)
- Common.Tests

Firstly, open project **TravelAgency.sln** in Visual Studio and build it.
