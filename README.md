
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

## How to run project
1. To run the MShare project, you need to have `Docker` and `Docker Compose` installed on your computer. 
2. After installation, you need to set up **system environment variables** to configure the project:

- `SPOTIFY_PROXY_SpotifyAuthCredentials`: Spotify basic token for accessing the Spotify API
- `MSHARE_Apple__Token__KeyId`: Apple authentication key for a p8 certificate
- `MSHARE_Apple__Token__Issuer`: Apple Team Id
- `MSHARE_Apple__Token__Secret`: Private key from an Apple p8 certificate

3. Navigate to the `"src"` folder of the MShare project.
4. Run the command: `docker-compose up`.

This will start the Docker containers for all the microservices defined in the Docker Compose file. The microservices will start, connect to the required services and databases, and be ready to handle requests.

## MShare Project Endpoints with Swagger Documentation 
MShare currently has the following endpoints:
- `AppleProxy:` <http://localhost:5001/swagger>
- `SpotifyProxy:` <http://localhost:5002/swagger>
- `Songs API:` <http://localhost:80/swagger>
- `Identity API:` <http://localhost:81/swagger>


