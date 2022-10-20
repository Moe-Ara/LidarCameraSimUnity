from ctypes import sizeof
import socket
import sys
import json
from json import JSONEncoder
import this
import time
import random
import pickle

## We create a new Object 'Car'
class Car:
    def __init__(self, speed, yaw):
        self.speed = speed
        self.yaw = yaw

## we write out own json encoder
class JsoEnc(JSONEncoder):
    def default(self, obj):
        return obj.__dict__

##accept connection
port=10101
tcp_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
server_addr = ('localhost', port)
tcp_socket.bind(server_addr)
tcp_socket.listen(2)
print ("[*] Initializing Sockets ... Done")
print ("[*] Sockets Binded Successfully ...")
print("[*] Server Started Successfully [%d]\n" % (port))
print("waiting for connection")
connection, client = tcp_socket.accept()
print("Connected to : {}".format(client))



try:
    while 1:
        ## get user input and send it to unity
        speedOfCar=int(input("Enter Speed: "))
        yawOfCar= int(input("Enter a yaw rate: "))
        carObj = Car(speedOfCar, yawOfCar)
        carObjMsg = json.dumps(carObj, cls=JsoEnc)
        bytearr=pickle.dumps(carObjMsg)
        b=bytearray(carObjMsg, "utf8")
        print("[*] Sending: "+ str(b))
        connection.sendall(b)
        time.sleep(3)
finally:
    print("server stopped")
    connection.close()
