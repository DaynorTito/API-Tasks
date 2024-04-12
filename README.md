# API Task management

The main purpose of the task management project is to manage, organize and track daily tasks, projects and goals. Where new tasks can be created, set due dates, assign priorities, add descriptions and track the progress of each task. Search and filter functions can be performed, users can quickly find the tasks they need to complete, which helps to be a task registry where CRUD operations can be performed, add tasks, list tasks, edit tasks or delete tasks. This project was developed in C# making use of a Rest. .NET SDK API (version 8.0 or later) and the ASP.NET Core Web API framework. The project was developed on Linux using the Rider IDE, but it is cross-platform, so it can be run on operating systems other than Linux.

## Steps to start the application

1. Clone repository

In order to deploy and run the application along with the database configuration, it is necessary to have the corresponding repository cloned on your machine. This can be achieved with the following command:

```bash
git clone git@gitlab.com:daynot/api-csharp-sd3-daynortito.git
```

2. Start docker compose 

After cloning the repository, navigate to the `resources` folder, which contains the files needed for the environment configuration

```bash
cd resources
```

Then, in order to start the Docker container with the configuration in the Docker Compose file, you must make use of the command:

```bash
sudo docker compose up -d
```

Then, at this point, you can verify that the container is running correctly with the following command on Linux operating systems:

```bash
sudo docker ps
```

3. Program execution

After starting the database you can make use of the API driver to verify the CRUD operation, to do this you must be located in the **ApiTask.Api** directory 

```bash
cd ..
cd ApiTask.Api
```

and execute the following command:

```bash
dotnet build
dotnet run
```

4. Verification

The server will be listening on 

```bash
http://localhost:5045
```

After it is running we can check the controller methods

### GetTasks
Description: Retrieves all tasks from the database.
HTTP Method: GET
Endpoint: 

```bash
/api/Task/task
```
Usage: Access this endpoint using a web browser or API client like Postman.
Response: Returns a JSON object with a message and an array of tasks.

### GetTaskById
Description: Retrieves a specific task by its ID.
HTTP Method: GET
Endpoint: 

```bash
/api/Task/task/{id}
```
Usage: Replace {id} with the ID of the task you want to retrieve. Access this endpoint using a web browser or API client like Postman.
Response: Returns a JSON object with a message and the task details if found, or a 404 status code if the task is not found.

### CreateTask
Description: Creates a new task.
HTTP Method: POST
Endpoint: 

```bash
/api/Task/create
```
Usage: Send a POST request with a JSON object representing the task to be created in the request body. Access this endpoint using an API client like Postman.
Response: Returns a JSON object with a message indicating the success or failure of the creation process, along with the details of the created task if successful.

### UpdateTask
Description: Updates an existing task.
HTTP Method: PUT
Endpoint: 

```bash
/api/Task/change/{id}
```
Usage: Replace {id} with the ID of the task you want to update. Send a PUT request with a JSON object representing the updated task in the request body. Access this endpoint using an API client like Postman.
Response: Returns a JSON object with a message indicating the success or failure of the update process, along with the details of the updated task if successful.

### DeleteTask
Description: Deletes a task.
HTTP Method: DELETE
Endpoint: 

```bash
/api/Task/delete/{id}
```

Usage: Replace {id} with the ID of the task you want to delete. Send a DELETE request to this endpoint. Access this endpoint using an API client like Postman.
Response: Returns a JSON object with a message indicating the success or failure of the deletion process.

### swagger

To access Swagger which is a tool for documenting and consuming APIs more easily and effectively. The url must be accessed from a browser:

```bash
http://localhost:5045/swagger/index.html
```

It allows you to describe the API structure, including available endpoints, supported HTTP methods, required and optional parameters, and expected responses.


## Stop and delete containers
If you are done using the endpoints you should terminate or remove the Docker container from the previously started database and if you want to stop and delete the container so that it no longer runs in the background, you can use the following commands:


```bash
cd ..
cd resoutves
```

```bash
sudo docker compose down -v
```
This command will stop the running containers, with the `-v` option, it will also remove the associated volumes, ensuring that no data remains.



