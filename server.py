import socket
import threading

port = 5555
server = "192.168.1.53"
print(server)
HEADER = 64
ADDR = (server, port)
FORMAT = "utf-8"
DISCONNECT = "!bye"

playeroneoption = ""
playertwooption = ""
choices_count = 0

Connections = []
# Creating a socket, picking the family, pick a type
server_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
server_socket.bind(ADDR)


def send_message(conn, msg):
    message = msg.encode(FORMAT)
    msg_length = len(message)
    send_length = str(msg_length).encode(FORMAT)
    send_length += b" " * (HEADER - len(send_length))

    conn.send(send_length)
    conn.send(message)

def checkwinner(playeroneoption,playertwooption):
    if playeroneoption == "scissors" and playertwooption == "paper":
        return True
    elif playeroneoption == "rock" and playertwooption == "scissors":
        return True
    elif playeroneoption == "paper" and playertwooption == "rock":
        return True
    elif playeroneoption == "scissors" and playertwooption == "rock":
        return False
    elif playeroneoption == "rock" and playertwooption == "paper":
        return False
    elif playeroneoption == "paper" and playertwooption == "scissors":
        return False
    else:
        print(f"draw, player one picked: {playeroneoption}, playertwo picked: {playertwooption}")
    

def handle_client(conn, addr):
    global playeroneoption
    global playertwooption
    global choices_count

    connected = True
    try:
        while connected:
            msg = conn.recv(1024).decode(FORMAT)
            if msg == DISCONNECT:
                connected = False
            if len(Connections) < 2:         
                conn.send("name:player1".encode(FORMAT))
            else:
                print("game starting")
                conn.send("gamestart".encode(FORMAT))
                conn.send("name:player2".encode(FORMAT)) 

            if msg == "player1scissors":
                playeroneoption = "scissors"
                choices_count += 1
            if msg == "player1rock":
                playeroneoption = "rock"
                choices_count += 1
            if msg == "player1paper":
                playeroneoption = "paper"
                choices_count += 1
            if msg == "player2scissors":
                playertwooption = "scissors"
                choices_count += 1
            if msg == "player2rock":
                playertwooption = "rock"
                choices_count += 1
            if msg == "player2paper":
                playertwooption = "paper"
                choices_count += 1

            if choices_count == 2:
                if checkwinner(playeroneoption, playertwooption) == True:
                    print("Player one won")
                else:
                    print("player two won")

            print(msg)
    except ConnectionResetError:
        print(f"Connection with {addr} was forcibly closed.")
    finally:
        conn.close()

def start():
    server_socket.listen()
    print("SERVER STARTING...")
    while True:
        conn, addr = server_socket.accept()  # Waits for a connection, when a connection occurs it will store the data
        print(f"addr = {addr}")
        Connections.append(addr)
        thread = threading.Thread(target=handle_client, args=(conn, addr))
        thread.start()

start()