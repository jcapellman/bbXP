# bbXP

## Status
[![Docker and K8s Deployment](https://github.com/jcapellman/bbXP/actions/workflows/cicd.yml/badge.svg)](https://github.com/jcapellman/bbXP/actions/workflows/cicd.yml)

## History
This project technically started with my first web page in 1996 on GeoCities.  However, the real development began in July 2003 when I decided to pick up PHP and MySQL before starting college in Maryland.  From 2003 to 2005, it was under very active development, and as my PHP and MySQL skills were, so did the level of complexity and cleanliness of the code.  In 2006 I started my first full-time programming job and they happen to be using Smarty Templates on the project I was working on which I immediately fell in love with and began updating the framework to utilize it to clean up my UI layer.  As fate would have Spring 2007 I switched to a different project that was largely ASP.NET 1.1 - which having not messed with yet I figured what better way to learn in my free time than port all of the PHP/MySQL code to ASP.NET 2.0/SQL Server.  The code in my personal SVN repo went through many variations until I finally moved to MVC and then onto ASP.NET Core in 2018. 2022 saw my desire to migrate to Kubernetes, break out the backend into REST Service and migrate to Digital Ocean. Akin to my move to ASP.NET in 2007, my employer requested everything be moved to Google Cloud in April 2024 and so to get hands on experience I migrated bbxp (among other projects to GCP). CI/CD from GitHub to Google Cloud was completed in early July 2024, and comments were returned via Comment Box.

The only remaining piece is a more stable and scalable editor.

## Major Change Log
### July 3rd, 2024 ###
-CI/CD migration to Google Cloud

-Clean up of the code to use the latest patterns and language features

### Februrary 10, 2023 ###
Refactoring and cleanup

### December 20, 2022 ###
Migration to a MVC, WebAPI, PSQL within a K8s cluster is underway.

### November 19th, 2022 ###
Updated to .NET 7

### December 7th, 2019 ###
Updated to Bootstrap 4.4.1 and ASP.NET Core 3.1 including clean up to make the posts and content to be responsive.

### January 6, 2019 ###
Updated all of the NuGet packages to 2.2 and .NET Core 2.2

### February 7, 2018 ###
Started the revamp to a unified asp.net core 2.0 mvc/webapi projects along with a .net standard 2.0 library (for use later in new mobile apps).

### September 16, 2016
In doing Node.js, MongoDB and Redis deep dives over the last 2 weeks I have decided to add Node.js/Redis for my caching implementation on bbxp (which I hadn't reinstituted since migrating to Core).  I hope to complete this implementation over the weekend.
