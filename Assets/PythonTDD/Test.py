import socket
import time
import struct 
import random 

def send_udp_int_as_byte(value, port):
    # Create a UDP socket
    sock = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)

    # Convert the integer to a byte
    byte_value =unpacked = struct.pack('i', value)

    # Send the byte over UDP to localhost on the specified port
    sock.sendto(byte_value, ('localhost', port))

    # Close the socket
    sock.close()

while True:
    send_udp_int_as_byte(200000000+random.randint(0,99999999), 2561)
    time.sleep(1)