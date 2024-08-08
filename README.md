Tema: Event Management System

Deskripsi: 
Sistem ini memungkinkan pengguna untuk membuat, mengelola, dan mendaftar ke acara atau event. 
Pengguna dapat mengelola detail acara, peserta, serta pembayaran terkait acara. 
Sistem ini akan mencakup berbagai layanan mikro untuk menangani otentikasi pengguna, manajemen acara, pendaftaran, dan pembayaran.

Implementasi BackEnd : .NET Core 6 API 
Database : SQL Server

Service :
AuthService: Untuk otentikasi dan manajemen pengguna.
EventService: Untuk manajemen acara.
RegistrationService: Untuk pendaftaran acara.
PaymentService: Untuk manajemen pembayaran.

_______________________________________________________________

Step

1.Buka SQL Server Management Studio, lalu crate DB
2.CREATE DATABASE MandiriEvent;

3.clone repository,
4.open solution,
5.open terminal,
6.migration :

dotnet ef migrations add InitialCreate --context AuthDbContext
dotnet ef database update --context AuthDbContext

dotnet ef migrations add InitialCreate --context EventDbContext
dotnet ef database update --context EventDbContext

dotnet ef migrations add InitialCreate --context RegistrationDbContext
dotnet ef database update --context RegistrationDbContext

dotnet ef migrations add InitialCreate --context PaymentDbContext
dotnet ef database update --context PaymentDbContext

7.Run Project

