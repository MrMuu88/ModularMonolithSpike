# ModularMonolithSpike
the purpose of this project to figure out, how can we create a onolithic but modular backend Api in .Net 6

# Problems
Each module should configure it's own Service registrations, configure it's own middlewares, and swagger options, without blocking any global settings from the main API
without the main Api hard referencing the projects

# Base structure
the controllers, middle wares etc should exist in their on respective "module" project,and each one should build a Class library
the Main api project should not directly reference these module projects, but load them dinamically on startup and load only those that are listed in the software Lincence

#Not Covered
This Spike does not contains any Lincencing solution as it is not in scope. In the Appsetting.json a simple string[] value declares which module should be loaded.
Authentication/Authorization is modularized here to have properly complex module for the spike, but token generation and validation is not implemented.
for code sharing between the modules, should exist a "Core" proejct hard referenced by all, but this spike does not contain any functional code, hence no need for this.

# Key Points
each of the module Projects must contain a Startup.cs which has to implement the IStartup interface coming from the Microsoft.Extensions.DependencyInjection package
any gine module has to do it's on setup in it's own startup file wholely.

In the main api startup, the code will scan the execution directory for module dll-s (only those that are listed in the appsetting.json) gets the Startup class from their assembly (the exact name is important!!) , then executes the ConfigureServices and Configure methods for all loaded modules

on DEBUG Each module should has it's Build output redirected into the Main Api output directory, on RELEASE each project should build into it's own directory
