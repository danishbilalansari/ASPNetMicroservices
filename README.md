# ASP Net Microservices
## ASP Net Microservices

* In .Net 8, the default ASP.NET Core port configured in .NET container images has been updated from port 80 to 8080.

## Docker

* To start the Docker containers define the following command

> docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d

* To stop and remove the Docker containers, networks and volumes, define the following command

> docker-compose -f docker-compose.yml -f docker-compose.override.yml down

## Mongo

* For Mongo Docker Image, following command is used. 'shopping-name' is the name parameter. 27017 is the official port no

> docker run -d -p 27017:27107 --name shopping-mongo mongo

* To get the interactive terminal in mongo, following command is used. 'shopping-mongo' is the name parameter

> docker exec -it shopping-mongo /bin/bash
 
* For Mongo GUI Options, following command is used for MongoDb Docker Image

> docker run -d -p 3000:3000 mongoclient/mongoclient

## Redis

* For Redis Docker Image, following command is used. 'aspnetrun-redis' is the name parameter

> docker run -d -p 6379:6379 --name aspnetrun-redis

* To get the interactive terminal in redis, following command is used. 'aspnetrun-redis' is the name parameter

> docker exec -it aspnetrun-redis /bin/bash

## Portainer

The password for portainer is as follows:

Username: admin <br>
Password: admin@123456
