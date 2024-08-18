# FPTW Hotels

This web app is part of my "5 projects in 10 weeks" programme.
I'm doing this to gain more experience and further my portfolio.
More info is available on my [blog](https://dimitar.gdn/2024/08/5-projects-in-10-weeks/).

## Goals of this project
- Improve my knowledge in Blazor and ASP.NET Core
- Build a useful application that can be used in a small hotel

## Features
- Authentication with user levels (guest, receptionist, maintenance, manager)
- For receptionists:
  - [ ] Room manager - rooms by floors and kind
  - [ ] Reservation management - list of reservations, whether they have been cancelled / paid, guests, etc.
  - [ ] Mark room as needing maintenance/cleaning
  - [ ] Mark last cleaning time (when a room has not been cleaned in a day - time configurable, show it on a special page for rooms pending cleaning)
  - [ ] See guest feedback
- Maintenance - for cleaners and maintenance workers
  - [ ] List of all rooms that need servicing / cleaning
  - [ ] Mark room as cleaned/serviced
- Administration - for hotel manager
  - [ ] Set cleaning interval hotel-wide
  - [ ] Access to all other features
  - [ ] Modify types of users
- Public side - for guests
  - [ ] Book reservations in hotel
  - [ ] Submit feedback

## Build and run
This is a standard Blazor Web App, and can be run with the default configurations.

## License
As this is a learning project, I've decided on using the MIT license.
The license is available in the LICENSE file in the root directory of the project.