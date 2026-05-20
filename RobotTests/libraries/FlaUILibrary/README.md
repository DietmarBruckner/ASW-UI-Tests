FlaUILibrary (HTTP keyword server)

This component implements a minimal HTTP-based keyword server that exposes basic FlaUI operations as JSON endpoints. A lightweight Python Robot Framework client (`robot_flaulib.py`) is provided to call the server keywords.

Phase 2 wrapper behavior:
- The Python wrapper uses a persistent `requests.Session`.
- On first use it checks server health via `POST /keyword/get_window_title`.
- If the server is not reachable, it auto-starts `bin/Release/net48/FlaUILibrary.exe`.
- The server also exposes `GET /ping` returning `{"result":"pong"}`.

Build and run (Windows, MSBuild):

1. Build with MSBuild (adjust path if needed):

```powershell
"C:\Program Files (x86)\Microsoft Visual Studio\2022\BuildTools\MSBuild\Current\Bin\MSBuild.exe" FlaUILibrary.csproj /t:Build /p:Configuration=Release
```

2. Run the server executable:

```powershell
.\bin\Release\net48\FlaUILibrary.exe
```

3. Use the Python Robot wrapper from Robot Framework tests:

- Ensure `requests` is installed:

```powershell
pip install requests
```

- In your Robot test, add the library:

```robot
Library    RobotTests/libraries/FlaUILibrary/robot_flaulib.py    server_url=http://localhost:5000
```

- Example usage:

```robot
*** Test Cases ***
Init IDE
		Initialize Automation Studio    C:\\Path\\To\\pg.exe    30
		Take Screenshot    myshot.png
		Close Application    True
```

Notes & Next Steps
- This server is a minimal prototype. For production use:
	- Harden the HTTP API, add authentication, and validate inputs.
	- Implement the full set of keywords required by `RobotTests/keywords/*.robot`.
	- Consider using XML-RPC Remote library protocol for direct Robot Framework compatibility.
	- Add robust error handling and logging.

- The server currently implements:
	- Keyword endpoints under `POST /keyword/{name}`.
	- `GET /ping` for health checks.
	- Save-prompt aware close handling (`close_application`).
	- Robust bool parsing for JSON boolean and string inputs.

- Extend `FlaUILibraryServer.ExecuteKeyword` to add more keyword handlers (click, find, type, etc.) and map them in `robot_flaulib.py`.
