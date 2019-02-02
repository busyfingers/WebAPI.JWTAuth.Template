# WebAPI.JWTAuth.Template
This is a ASP.NET Core Project Template for quickly scaffolding an application with JWT-based user authentication and background workers scheduled using cron expressions.

By default it uses an in-memory database but can easily be swapped to any database provider supported by Entity Framework Core.

## How to use
1. Open a terminal window.
2. Create a folder for your project and change directory to that folder.
3. Install the template from NuGet: ```dotnet new -i busyfingers.JWTauth.WebAPITemplate```
4. Create project using the template: ```dotnet new busyfingers.webapi.jwtauth```

## Routes
| Route         | Method | Auth required | Description      |
| ------------- |:------:|:-------------:|:-----------------|
| /api/users    | GET    | Yes | Lists all users  |
| /api/users/x  | GET    | Yes | Fetch information for user with id x |
| /api/users/x  | PUT    | Yes | Update user with id x |
| /api/users/x  | DELETE | Yes | Delete user with id x |
| /api/users/register  | POST | No | Register new user |
| /api/users/authenticate  | POST    | No | Authenticate user |

## Payloads

### Register user
```
{
	"FirstName": "John",
	"LastName": "Doe",
	"Username": "john",
	"Password": "qwerty123"
}
```

It is possible to require a specific signup code to restrict registration. If required, the signup code is provided via the querystring with the parameter name ``signupCode```. See below for more information on how to configure this.

### Authenticate
```
{
	"Username": "john",
	"Password": "qwerty123"
}
```

The response will contain a token that is valid for 30 minutes. This token must be included in the header when requesting the routes that require authorization (see table above).

## Application configuration
There are several configurations available, the only mandatory one is the **hashingsecret**, which is provided via the command line when starting the app.

| Configuration | Description |
| ------------- |:------------|
| hashingsecret | The string used to hash the user passwords |
| signupcode | A code that must be provided when registering a new user (default is none) |
| urls | Explicitly sets the address the app will listen to, e.g. https://192.168.1.255 or https://mydomain.com |
| certfile | Path to the HTTPS certificate file, e.g. "mycert.pfx" |
| certpass | The HTTPS certificate password |

## Background worker schedule configuration
To configure the schedule for a background worker, simply add a key-value pair in the section ```CronSettings``` in appsettings.json, where the key name is the same as the background worker class (the implementation) and the value is the cron expression. The template contains a worker implementation that compresses the logs older than today and puts them in an archive folder. The class is named ```ArchiveLogs``` and is configured like so:
```
  ...
  "CronSettings": {
    "ArchiveLogs": "59 23 * * *"
  },
  ...
```
This will compress the logs at 23:59 every day.
