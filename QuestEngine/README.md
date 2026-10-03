# AI Quest Generator Setup

This project uses Ollama for local model inference and ASP.NET Core for the API.

## Prerequisites

- Install .NET 10 SDK
- Install Ollama
- Ensure Ollama is running locally on port 11434

## 1. Start Ollama and load the model

Open a terminal and run:

```bash
ollama serve
ollama run llama3.2
```

If the model is not already downloaded, Ollama will pull it automatically.

## 2. Run the application

From the project root, start the API:

```bash
dotnet run
```

The app is configured to run on:

- http://localhost:5166
- https://localhost:7273 (if HTTPS is enabled)

## 3. Open the API documentation

Once the app is running, open:

```text
http://localhost:5166/scalar/v1
```

This exposes the interactive API docs for testing the endpoints.

## Troubleshooting

- If Ollama is not reachable, make sure the Ollama server is running on port 11434.
- If the app does not start, verify that the .NET SDK is installed and the project restores successfully with `dotnet restore`.
- If the port is different, check the launch profile in `Properties/launchSettings.json`.