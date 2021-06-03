# docker-AspNetCore-SqlServer-Sample
This is part of my personnel learning related to docker and docker compose


1: Test of program without sql server

In order to build the docker file in WebApiColourCore run the following command

docker build  -t finnbaunbjerg/webcolour -f WebApiColourCore/Dockerfile .

from folder docker-aspNetCore-sqlServer

In order to start container run the command

docker run -d -p 5001:80 -e ASPNETCORE_ENVIRONMENT=Development  finnbaunbjerg/webcolour

if ASPNETCORE_ENVIRONMENT=Development is not present, the swagger page will not be available

Check swagger page by typing this in browser: http://localhost:5001/swagger/index.html

2: Create an migration, package  Microsoft.EntityFrameworkCore.Design and Microsoft.EntityFrameworkCore.Tools must be added to the DbContext project

From package manager console run the following command

Add-Migration -Name initial -Project WebApiColourData -StartupProject WebApiColourCore -Context ColourContext 

3: To test that seeding is working correctly, do the following

a: Start a sql server docker image , see docker hub, but as 3/6-2021 it can be done like this
docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=yourStrong(!)Password' -p 1443:1433 -d mcr.microsoft.com/mssql/server, port 1443, user and password must match port in program,
The above docker cmd, starts a developer version of sql server.

Check that sql server is working by connecting from ssms, server: localhost,1443

b: Start ASP.Net core application from IIS. If everything is working, the db should be populated with data


4: 