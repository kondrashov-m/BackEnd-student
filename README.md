# Лабораторная работа №7
[![.NET 9](https://img.shields.io/badge/.NET-9.0-512BD4)](https://dotnet.microsoft.com/)
[![C# 13](https://img.shields.io/badge/C%23-13.0-239120)](https://docs.microsoft.com/dotnet/csharp/)
[![ASP.NET Core](https://img.shields.io/badge/ASP.NET_Core-9.0-512BD4)](https://dotnet.microsoft.com/apps/aspnet)
[![License](https://img.shields.io/badge/license-MIT-blue)](LICENSE)
```text
Тема: Работа с middleware в ASP.NET Core
```
```text
Освоить создание и подключение пользовательских middleware в ASP.NET Core, 
понять принцип работы конвейера обработки HTTP-запросов, научиться использовать 
HttpContext для передачи данных между middleware и формирования ответов.
```
## 🚀 Инструкция по установке

```bash
# Склонируйте репозиторий и перейдите в папку проекта
git clone https://github.com/kondrashov-m/BackEnd-student
cd BackEnd-student
git checkout lab6

# Запустите приложение
dotnet run
```
## ✨ О программе
Веб-API приложение, созданное в рамках лабораторной работы по дисциплине "BackEnd-разработка".

Демонстрирует создание и подключение пользовательских middleware:
1. BlockPathMiddleware — блокирует запросы к путям, начинающимся с /blocked (возвращает 403 Forbidden)
2. RequestTraceMiddleware — генерирует уникальный TraceId (Guid), добавляет заголовок X-Trace-Id и сохраняет его в HttpContext.Items
3. EndpointTimingMiddleware — измеряет время выполнения эндпоинта с помощью Stopwatch и добавляет заголовок X-Endpoint-Elapsed-Ms
   
## 🛠 Технологии
```text
.NET 9 – кросс-платформенная среда выполнения
```
```text
C# 13 – современный ООП язык
```
```text 
ASP.NET Core - с использованием контроллеров и Swagger
```
```text
Dependency Injection - встроенный DI-контейнер (Singleton/Scoped)
```

# 
<div align="center"><img width="148" height="34" alt="logo" src="https://github.com/user-attachments/assets/1ecfceb6-0999-4955-b38c-b66cd067c2c3" /></div>

