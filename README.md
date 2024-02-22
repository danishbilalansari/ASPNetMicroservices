# ASP Net Microservices
ASP Net Microservices

* In .Net 8, the default ASP.NET Core port configured in .NET container images has been updated from port 80 to 8080.
* To start the Docker containers define the following command

docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d

* To stop and remove the Docker containers, networks and volumes, define the following command

docker-compose -f docker-compose.yml -f docker-compose.override.yml down
 
* For Mongo GUI Options, following command is used for MongoDb Docker Image

docker run -d -p 3000:3000 mongoclient/mongoclient
