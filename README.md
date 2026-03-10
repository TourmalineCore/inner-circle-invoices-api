# inner-circle-invoices-api

This repo contains Inner Circle Invoices API.

This repo and its infrastructure tailored for VSCode/GitHub Codespaces Dev Container centric development experience in Docker to achieve better isolation of the environment as well as its cross-platform support out of the box. 
For instance, Visual Studio for Mac support stops at .NET 8, thus, we needed something else rather than native Visual Studios for Mac and Windows. We decided to give a shot to VSCode since we use it intensively in other stacks already.

Full info about Inner Circle .NET related code conventions, patterns, decisions, and reasoning can be found [here](https://github.com/TourmalineCore/inner-circle-documentation/blob/master/code-style/api-code-style.md).

More info about the Inner Circle project and its related repos can be found here: [inner-circle-documentation](https://github.com/TourmalineCore/inner-circle-documentation).

## Prerequisites

1. Install Docker Desktop (Windows, macOS) or Docker Engine (Linux)
>Note: It seems like there is Docker Engine for Linux (https://docs.docker.com/desktop/setup/install/linux/ubuntu/).
2. Install Visual Studio Code.
3. Install Visual Studio Code [Dev Containers](https://marketplace.visualstudio.com/items?itemName=ms-vscode-remote.remote-containers) Extension.

## Develop in Dev Container

Open this repo's folder in VSCode/Codespaces, it might immediately propose you to re-open it in a Dev Container or you can click on `Remote Explorer`, find plus button and choose the `Open Current Folder in Container` option and wait when it is ready.

When your Dev Container is ready, the VS Code window will be re-opened. Open a new terminal in this Dev Container which will be executing the commands under this prepared Linux container where we have already pre-installed and pre-configured Inner Circle APIs .NET related dependencies. 

### Run API

```cli
dotnet run --project ./Api
```

### Run Unit and Integrational Tests

To run xUnit unit and integrational tests execute the following script in Terminal:
```cli
dotnet test --verbosity detailed
```

### Run Only Unit Tests

To run xUnit unit tests execute the following script in Terminal:
```cli
dotnet test --verbosity detailed --filter "Type=Unit"
```

### Run Only Integrational Tests

To run xUnit integrational tests execute the following script in Terminal:
```cli
dotnet test --verbosity detailed --filter "Type=Integration"
```

### Run E2E Tests

To run Karate E2E tests execute the following script in Terminal:
```cli
java -jar /karate.jar .
```

### Allocated Ports & Services

| Service Name               | Api in Dev Container/Codespaces | Api in IDE | Api in Docker Compose |  Db in Docker Compose | MockServer in Docker Compose | PgAdmin in Docker Compose |
| :------------------------- | :-----------------------------: | :--------: | :-------------------: | :-------------------: | :-------------------------: | :-------------------------: |
| inner-circle-invoices-api      |               4508              |    5508    |          6508         |          7508         |             8508            |             9508            |

Full docs about the allocated ports, reasoning, and the other services bindings in this infrastructre setup are available [here](https://github.com/TourmalineCore/inner-circle-documentation/blob/master/code-style/api-code-style.md#ports).

You can go to `Ports` tab in the `Terminal` parent panel to find available services.
