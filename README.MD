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
### Pages

------

###### Login Page

`/`

![pJz12.png](https://picr.eu/images/2020/05/05/pJz12.png)

login with your credentials

------

###### Register Page

`/register`



​								![pJOzu.png](https://picr.eu/images/2020/05/05/pJOzu.png)


​	Register with your email, username and password

------

###### Home Page

`/home`

![pJPl4.png](https://picr.eu/images/2020/05/05/pJPl4.png)

	
