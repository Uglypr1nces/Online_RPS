import socket
import threading

port = 5555
server = "192.168.1.53"
HEADER = 64
ADDR = (server, port)
FORMAT = "utf-8"
DISCONNECT = "!bye"

playeroneoption = ""
playertwooption = ""

playeronechoicecount = 0
playertwochoicecount = 0

player1_conn = None
player2_conn = None

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
    global playeronechoicecount
    global playertwochoicecount
    global player1_conn
    global player2_conn

    connected = True
    try:
        while connected:
            msg = conn.recv(1024).decode(FORMAT)
            if msg == DISCONNECT:
                connected = False

            if player1_conn is None:
                player1_conn = conn
                conn.send("name:player1".encode(FORMAT))
            elif player2_conn is None:
                player2_conn = conn
                conn.send("name:player2".encode(FORMAT))

            if msg.startswith("player1"):
                playeroneoption = msg[len("player1"):]
                playeronechoicecount += 1
            elif msg.startswith("player2"):
                playertwooption = msg[len("player2"):]
                playertwochoicecount += 1

            if playeronechoicecount + playertwochoicecount == 2:
                if checkwinner(playeroneoption, playertwooption):
                    print("Player one won")
                    player1_conn.send("you won".encode(FORMAT))
                    player2_conn.send("you lost".encode(FORMAT))
                    playeronechoicecount = 0
                    playertwochoicecount = 0
                    playeronechoicecount = ""
                    playertwochoicecount = ""
                else:
                    print("player two won")
                    player2_conn.send("you won".encode(FORMAT))
                    player1_conn.send("you lost".encode(FORMAT))
                    playeronechoicecount = 0
                    playertwochoicecount = 0
                    playeronechoicecount = ""
                    playertwochoicecount = ""
                    
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
        thread = threading.Thread(target=handle_client, args=(conn, addr))
        thread.start()

start()
