# Description  
a small Todo Asp.NET Core Backend with ODATA Filter capabilities and missing Authentication (planned with OAuth2).
Furthermore, there will be a Angular Frontend here using this Backend.

It uses a PostgreSQL DB Docker Container.

# Installation  
Bc of easier PostgreSQL configuration, I require Docker to run this Application. 

to run the Backend:  
	```docker-compose -f ./TodoDataAPI/docker-compose.yml up```

## Generating a HTTPS Devlopment certificate: 
	dotnet dev-certs https --clean
	dotnet dev-certs https -ep https/todo_cert.pfx -p pass1234
	dotnet dev-certs https --trust