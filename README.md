# README

Concerns the issue https://github.com/aspnet/EntityFrameworkCore/issues/19126

To reproduce this, just open a powershell in the `DevLab.EntityFrameworkCore.Migrations` project and type `dotnet ef migrations add Init`.
It will then throw the error.