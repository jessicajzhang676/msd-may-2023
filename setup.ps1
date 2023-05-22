docker compose up -d
dotnet tool install --global dotnet-ef
Set-Location src\web\JobApplicationMvc\JobApplicationMvc
dotnet ef database update --context JobApplicationContext
dotnet ef database update --context JobApplicationsDataContext
