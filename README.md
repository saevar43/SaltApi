# SaltApi Project
This project is hosted in a Docker repository on Docker Hub.

You need to have Docker installed to be able to run this project.
See: https://www.docker.com/get-started.

## How to run

### Terminal
To run the project follow these steps:

1. Open a terminal of your choice.
2. In terminal, navigate to the 'dockerpostgres' directory provided.
3. In terminal, run `docker-compose up` to start the database.
4. In terminal, run `docker pull saevar43/saltapi:latest` to ensure you have the latest version of the project.
5. In terminal, run `docker run --name SaltApi -it -p 5000:80 saevar43/saltapi` to run the project.
6. In terminal, run `docker network connect dockerpostgres_default SaltApi` to connect the project to the database.

You should now be able to access the project OAS at localhost:5000/swagger.

### Docker Desktop
To run the project follow these steps:

1. Open a terminal of your choice.
2. In terminal, navigate to the 'dockerpostgres' directory provided.
3. In terminal, run `docker-compose up` to start the database.
4. In terminal, run `docker pull saevar43/saltapi:latest` to ensure you have the latest version of the project.
5. In Docker Desktop run the saevar43/saltapi image, which should now be local. 
   Make sure to open the Optional Settings and set Container Name to SaltApi and map port 5000 to port 80, like so.

   ![How to set name and port][image]

6. In terminal, run `docker network connect dockerpostgres_default SaltApi` to connect the project to the database.

You should now be able to access the project OAS at localhost:5000/swagger.

## How to test
Controller unit tests can be run via the Visual Studio Test Explorer.

To do this, in VS, navigate to Test -> Test Explorer. 
There you can click the double arrow in the top left corner to run all tests.

[image]: ./set-name-and-port.png