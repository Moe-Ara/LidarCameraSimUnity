from ctypes import sizeof
import socket
import sys
import json
from json import JSONEncoder
import this
import time
import random
import pickle
import struct

## We create a new Object 'Car'
class Car:
    def __init__(self, speed, yaw):
        self.speed = speed
        self.yaw = yaw

class ControlResultMessage:
    def __init__(self, speed_target, steering_angle_target,bht,mmt,lc,cca,ccal):
        self.speed_target=speed_target
        self.steering_angle_target=steering_angle_target
        self.brake_hydr_target=bht
        self.motor_moment_target=mmt
        self.lap_counter =lc
        self.cones_count_actual=cca
        self.cones_count_actual= ccal

## we write out own json encoder
class JsoEnc(JSONEncoder):
    def default(self, obj):
        return obj.__dict__

##accept connection
port=42003
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
        # carObj = Car(speedOfCar, yawOfCar)
        # carObjMsg = json.dumps(carObj, cls=JsoEnc)
        # bytearr=pickle.dumps(carObjMsg)
        # b=bytearray(carObjMsg, "utf8")
        Ctrlmsg= ControlResultMessage(speedOfCar,yawOfCar, 0, 0, 0,0,0)
        ctrlmsgjson=json.dumps(Ctrlmsg, cls=JsoEnc)
        print("[*] Sending: "+ str(bytearray(ctrlmsgjson,"utf8")))
        datasize= len(ctrlmsgjson)
        ba=struct.pack('<i',datasize)
        print("[*] data size is :" +str(len(ctrlmsgjson)))
        print("[*] data size is in Bytearray :" +ba)
        connection.send(ba)
        connection.sendall(ctrlmsgjson)
        time.sleep(3)
finally:
    print("server stopped")
    connection.close()
