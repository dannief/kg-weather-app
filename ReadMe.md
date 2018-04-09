## Overview
KG.Weather app is a simple React/ASP.NET Core application that 
displays tomorrow's weather conditions for select cities in which a 
fictional company, (KG), operates. The backend component connects to a
weather API in order to retrieve the required weather data and 
is also responsible for notfiying workers of the functional company
whenever rain is forecasted. If rain is forecasted, workers are notified
that they are only required to work half-day.

> Note: This was a programming task done as a requirement for a job interview

The project was generated using the [ASP.NET Core React-with-Redux project template](https://docs.microsoft.com/en-us/aspnet/core/spa/react-with-redux)

## How to Run
1. Open project in Visual Studio 2017 
2. Allow VS to restore nuget packages
3. You may be required to install npm packages from the ClientApp folder
4. Select F5 on the keyboard to run

> Current appsettings assumes a 
default SQL Server instance at localhost and an SMTP server running on localhost, port 25.
Suggested local SMTP servers that may be used are 
[Papercut](https://github.com/ChangemakerStudios/Papercut) or 
[MailDev](http://danfarrelly.nyc/MailDev/). Database will be seeded at startup.

## Technologies and Patterns Used

**Backend**

- [ASP.NET Core 2.0](https://docs.microsoft.com/en-us/aspnet/core/)
- [Entity Framework Core](https://docs.microsoft.com/en-us/ef/)
- [MS SQL Server](https://docs.microsoft.com/en-us/sql/)
- [APIXU Weather API](https://www.apixu.com/)
- [SOLID architecture in slices](https://lostechies.com/jimmybogard/2015/07/02/ndc-talk-on-solid-in-slices-not-layers-video-online/)
- [Mediator pattern](https://github.com/jbogard/MediatR)
- [DNTScheduler.Core: lightweight ASP.NET Core background tasks runner and scheduler](https://github.com/VahidN/DNTScheduler.Core)
- [Bogus: Fake data generator](https://github.com/bchavez/Bogus)
- [Swagger](https://github.com/domaindrivendev/Swashbuckle.AspNetCore)

**Frontend**
- [React](https://reactjs.org/)
- [Redux](https://redux.js.org/)
- [Reselect](https://github.com/reactjs/reselect)
- [Recompose](https://github.com/acdlite/recompose)
- [Grid Styled: Responsive grid system](http://jxnblk.com/grid-styled/)

