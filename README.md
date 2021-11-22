## About project
WebChat is a simple chatting app writed for learning purposes.
It communicates between server and clients via SignalR with JWT authentication. <br>
<img width="300" alt="image" src="https://user-images.githubusercontent.com/44320848/142842039-4cf6262a-6eef-446f-91de-8430f46c8095.png"> &emsp; &emsp;
<img width="300" alt="image" src="https://user-images.githubusercontent.com/44320848/142842017-882cee96-7951-4e8a-a5d8-d1356925d91f.png">

## Requirements & Installation
To run this program you need `npm`, `dotnet`, and `mssql` server. 

1. Run the frontend (`WebChat.Web` directory) with npm server on `3000` port. In other case, you must change CORS settings in `WebChat/appsettings.Development.json` file before run the project.
2. Compile and run the backend. It should use `https` protocol and `5001` port. In other case, you must change hub connection settings in `WebChat.Web/src/App.js`. Compile solution with visual studio is recommended (instead of it you can use `dotnet /build` command). 

If you want change the DB connection string or JWT settings, look at `WebChat/appsettings.Development.json` file. 

<br><br>
## Dont Read Me

This chart perfectly shows my feelings 
about writing this program.

<img width="213" alt="chart" src="https://user-images.githubusercontent.com/44320848/142837203-1fade6df-2b5e-4f02-8248-bc9ed747f322.png">

- 60.4% of fun
- 20.8% of try to not break up the keyboard
- 18.8% of boring

ofc this values is percent of codes line.
Writing 20% of the code in JS took about 70% of the time spent on project
