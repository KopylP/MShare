
# MShare
### Welcome to the MShare project! 

#### ***The MShare project is currently under development and not yet finished.***

*(I am grateful for the assistance provided by **ChatGPT** in composing the description of this project.)*

This project was created to allow for easy sharing of music links between different streaming services. 

Currently, the project supports sharing songs between Apple Music and Spotify, as well as sharing project cover images on Instagram. 

This repository contains the back-end implementation of the project, using a microservice architecture and implementing modern approaches such as **Domain-Driven Design (DDD)** and **Command-Query Responsibility Segregation (CQRS)**. 

As development continues, the project will gain more functionality and become more versatile.

# Micro service architecture

The MShare project utilizes a microservice architecture to organize its various components. The microservices are designed to work together to provide a cohesive system, while also being independently deployable and maintainable. The project currently contains four microservices: AppleProxy, SpotifyProxy, Songs and Identity.

The AppleProxy and SpotifyProxy microservices are designed to handle communication between the project and the respective music streaming service APIs (Apple Music and Spotify). These services act as intermediaries between the project and the APIs, allowing for easier management of API calls and hiding implementation details from the other microservices. They also expose an identical REST API for communication with the Songs microservice.

The Songs microservice is responsible for implementing the logic for sharing music between different streaming services. It communicates with the proxy services (AppleProxy and SpotifyProxy) to retrieve information about songs, albums, and artists.

The Identity microservice is responsible for manages user profiles and user identification and the creation of JSON Web Tokens (JWT) for authentication. It uses Apple identification for this.

In future, we are planning to add a Statistic microservice that will store statistics about shared songs, albums, and artists, allowing for better tracking and analysis of user interactions with the system.

## Technology stack
The MShare project utilizes the following technology stack and approach ***(for now)*** :
- **C#**
- **ASP.NET Core**
- **Entity Framework Core**
- **Mass Transit**
- **Docker**
- **PostgreSQL**
- **Domain-Driven Design (DDD)**
- **Command-Query Responsibility Segregation (CQRS)**

