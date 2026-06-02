# Anthemis Restaurant

Anthemis Restaurant is an ASP.NET Core MVC web application developed for a restaurant reservation and menu management system. Users can view the restaurant menu, register, log in, create table reservations, and view their own reservations. Admin users can manage menu items and view reservation requests.

## Features

* Home page with restaurant introduction
* Menu page listing food and drink items from the database
* User registration and login system
* Simple session-based authentication
* User and Admin role separation
* Table reservation system
* My Reservations page for users
* Admin panel
* Menu item CRUD operations
* Reservation status management
* Bootstrap-based responsive design
* Custom CSS styling
* SQLite database with Entity Framework Core

## Technologies Used

* ASP.NET Core MVC
* Entity Framework Core
* SQLite
* Razor Views
* Bootstrap
* HTML5
* CSS3
* C#

## User Roles

### Guest

Guests can view the homepage and menu page. They can also access the login and register pages.

### User

Registered users can create table reservations and view their own reservations.

### Admin

Admin users can manage menu items, view all reservations, and update reservation statuses.

## Main Pages

* Home
* Menu
* Reservation
* My Reservations
* Admin Panel
* Login
* Register

## Database Tables

The project uses three main database tables:

* Users
* MenuItems
* Reservations

## Live Demo

```text
http://anthemis.runasp.net/
```

## Project Purpose

This project was created as a web programming project to demonstrate ASP.NET Core MVC structure, database connection, basic authentication, CRUD operations, Bootstrap design, and role-based page access.
