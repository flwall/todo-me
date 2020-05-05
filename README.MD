# Description  
A Awesome Todo App built upon ASP.NET Core 3.1 and Angular "Ivy" 9

It uses a PostgreSQL DB Docker Container.

# Installation  
Bc of easier PostgreSQL configuration, I require Docker to run this Application. 

### Generating a HTTPS certificate: (this is required!!!)
	cd TodoDataAPI/
	dotnet dev-certs https --clean
	dotnet dev-certs https -ep https/todo_cert.pfx -p pass1234
	dotnet dev-certs https --trust  

to run the Application:  
	```docker-compose up```  

After that, the application should run under **[https://localhost:9011/](https://localhost:9011/)**

------
# API Endpoints
API Endpoint docs:

| Endpoint             | HTTP Request Methods | Description              |
| -------------------- | -------------------- | ------------------------ |
| /api/auth/login      | POST                 | login with existing user |
| /api/auth/register   | POST                 | register a new user      |
| /api/todos           | GET                  | get all todos            |
| /api/todos/{todoid}  | POST                 | add new todo             |
| /api/todos/{todoid}  | PUT                  | edit todo                |
| /api/todos//{todoid} | DELETE               | delete todo              |	
------
# Pages
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
------


	
