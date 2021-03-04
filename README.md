# WebDay2021-ASP.NET-Core-SignalR

This repository contains slides and demo source code for [Web Day 2021](https://www.ugidotnet.org/e/1938/Web-Day-2021) ["ASP.NET Core SignalR, Applicazioni Real-Time nel Browser...ed oltre"](https://www.ugidotnet.org/e/sessione/2173/ASP-NET-Core-SignalR-Applicazioni-Real-Time-nel-Browser--ed-oltre) Session.

Inside the **Slides** folder, there is the PDF presentation used during the session.

Inside the **Demo** folder there are 3 different Visual Studio 2019 Solutions, each one in a dedicated subfolder:
 - **WebDay2021.SignalR.SimpleMultiplatformChat**: a simple chat with both HTML and WPF clients.
 - **WebDay2021.SignalR.SimpleFruitShop**: shows the ASP.NET Core authentication and MessagePack support; ASP.NET Core Identity is configured to use SQLite.
 - **WebDay2021.SignalR.SensorDashboard**: highlights the Groups feature and uses a Redis backpane authentication; it requires a local Redis installation, or a container running Redis.
