Two-Player Online Rock-Paper-Scissors Game
---------------------------------------------------------------------------------------------------------------------------------------
This project is an implementation of a classic rock-paper-scissors game that allows two players to play against each other online.
Players can connect to the game server, choose their moves (rock, paper, or scissors), and see the outcome of each round in real-time.

Features:
---------------------------------------------------------------------------------------------------------------------------------------

Multiplayer Online Gameplay: Players can connect to the server and play against each other from different locations.
Real-Time Communication: Utilizes sockets to enable real-time communication between players and the server.
User Authentication: Players can register their usernames and log in to track their game statistics.
Scalable Architecture: Designed with scalability in mind to accommodate multiple simultaneous game sessions.
Responsive UI: Provides an intuitive user interface for an enjoyable gaming experience on different devices.

Technologies Used:
---------------------------------------------------------------------------------------------------------------------------------------
Python: Backend server implementation using Python for socket programming.
C# (WinForms): Frontend client implementation using C# with WinForms for the graphical user interface.
GitHub: Version control and collaboration platform for managing project code.

How to Play:
---------------------------------------------------------------------------------------------------------------------------------------
1. Install .NET Core SDK: You can download and install the .NET Core SDK from the official .NET website

2. locate the server.py file and change the ip address to yours (make sure you port forwarded it on the router)

3. Start the server
```bash
 python server.py

4. Start the game
```bash
 dotnet build Online_RPS.sln

5. Enjoy :)

Contributing:
---------------------------------------------------------------------------------------------------------------------------------------
Contributions are welcome! If you'd like to contribute to the project, feel free to fork the repository, make your changes, and submit a pull request.
Any improvements, bug fixes, or feature enhancements are highly appreciated.
