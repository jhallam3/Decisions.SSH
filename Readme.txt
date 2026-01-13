SSH steps that work in Decisions.com. 

## Setup Instructions

### Prerequisites
1. .NET Core 3.1 SDK or later
2. DecisionsFramework.dll from your Decisions installation

### Building the Project
1. Create a `lib` directory at the root of the repository
2. Copy `DecisionsFramework.dll` from your Decisions Server installation to the `lib` directory
   - Default location: `C:\Program Files\Decisions\Decisions Server\bin\DecisionsFramework.dll`
3. Run `dotnet restore` in the DecisionsSSH directory
4. Build the solution

Note: The `lib` directory is excluded from version control as it contains proprietary binaries.