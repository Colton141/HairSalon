# _Hair Salon_

#### By _**Colton Lacey**_

## Description

_A program that will let a Hair Salon add and remove stylist and their clients from the program._

| Behavior | Input | Output |
| ------------- |:-------------:| -----:|
| The program should allow employees(stylists) to input new stylists and description into the database | "Colton" | Colton |
| The program should allow employees input new client with their stylist | "tom" | tom, Stylist: colton |
| The program should not allow employees to input clients if there are no stylist | -- | -- |
| The program should let the employees view all the stylist and their description| colton | name, description |
| The program should let the employees view all the client that a stylist has | colton | tom, bob, john |
| The program should let employees remove stylist and clients from the lists | -- | -- |

## Setup/Installation Requirements

* _Clone project from github__
* _run the fallowing commands in the terminal in order to connect you're database_
* _"CREATE DATABASE colton_lacey;"_
* _"USE colton_lacey;"_
* _"CREATE TABLE stylists (id serial PRIMARY KEY, name VARCHAR(255), description VARCHAR(255));"_
* _"CREATE TABLE clients (id serial PRIMARY KEY, name VARCHAR(255), stylist_id VARCHAR(255));"_
* _use "dotnet restore" and "dotnet build" and "dotnet run" in the production folder (HairSalonList)_
* _View at localhost:5000_
* _use the UI to modify stylists and clients_

## Known Bugs

_None_

## Support and contact details

_No support offered_

## Technologies Used

_C#_

### License

Copyright <2019> <Colton Lacey>
