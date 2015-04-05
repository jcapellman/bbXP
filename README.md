# bbXP
My ASP.NET MVC/WebAPI Content Management System

Currently under going a complete rewrite to take advantage of all the new functionality in ASP.NET 5 and EntityFramework 7.

A very brief game plan as far as architecture:

Backend:

-EntityFramework 7 with async calls
-Change Tracking (configurable in the config file)
-POCO PCL usage so the eventual mobile apps can just use the same models

Middle Tier:

-Business logic only exposed via a Class Library (thinking down the road to offline applications)

Frontend:

-Angular JS for MVVM
-Bootstrap usage for responsive design

Also all functionality will be exposed via a WebAPI Service (planning ahead for the mobile apps)
