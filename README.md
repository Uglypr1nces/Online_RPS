Two-Player Online Rock-Paper-Scissors Game
---------------------------------------------------------------------------------------------------------------------------------------
This project is an implementation of a classic rock-paper-scissors game that allows two players to play against each other online.
Players can connect to the game server, choose their moves (rock, paper, or scissors), and see the outcome of each round in real-time.

Features:
---------------------------------------------------------------------------------------------------------------------------------------

Multiplayer Online Gameplay: Players can connect to the server using ngrok and play against each other from different locations.
Real-Time Communication: Utilizes sockets to enable real-time communication between players and the server.
User Authentication: Players can register their usernames and log in to track their game statistics.
Scalable Architecture: Designed with scalability in mind to accommodate multiple simultaneous game sessions.
Responsive UI: Provides an intuitive user interface for an enjoyable gaming experience on different devices.

Technologies Used:
---------------------------------------------------------------------------------------------------------------------------------------
Python: Backend server implementation using Python for socket programming.
C# (WinForms): Frontend client implementation using C# with WinForms for the graphical user interface.
Ngrok: Route IP Address
GitHub: Version control and collaboration platform for managing project code.

How to Play running a local server:
---------------------------------------------------------------------------------------------------------------------------------------

## Local Network
- Install Project 
    ```bash
    git clone https://github.com/Uglypr1nces/Online_RPS.git
  
- Install .NET Core SDK: You can download and install the .NET Core SDK from the official .NET website. For example, for MAC
```bash
brew install dotnet-sdk
```

- Make sure you have python3 installed as well

- Start the server:
```bash
    python server.py
```

4. Start Game:
   ```bash
    dotnet build Online_RPS.sln
   ```

5. Enjoy :)

## Wide area Network

0. Install Project 
    ```bash
    git clone https://github.com/Uglypr1nces/Online_RPS.git
    
1. Install .NET Core SDK: You can download and install the .NET Core SDK from the official .NET website, Make sure you have python3 installed aswell

2. Create an ngrok account at https://ngrok.com/ and follow the install guide

3. Move into the Directory in which ngrok is installed

4. Create the Server at port 8000
   ```bash
    ngrok.exe tcp 8000
   
5. Locate the Form1.cs in the Game folder (Online_RPS) and change the 57 to your ngrok url

6. Start the server:
   ```bash
    python server.py

7. Start Game:
   ```bash
    dotnet build Online_RPS.sln

8. Enjoy :)

Contributing:
---------------------------------------------------------------------------------------------------------------------------------------
Contributions are welcome! If you'd like to contribute to the project, feel free to fork the repository, make your changes, and submit a pull request.
Any improvements, bug fixes, or feature enhancements are highly appreciated.
