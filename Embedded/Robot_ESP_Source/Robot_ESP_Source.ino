#include <Servo.h>
#include <ESP8266WiFi.h>
#include <WiFiUdp.h>
#include <math.h>
#include <SPI.h>

// WiFi settings
char * ssid = "Go Diego Go!";
char * pwd = "metateamsix";

// UDP settings
WiFiUDP cmd_server;
const unsigned int cmd_port = 2380;
const unsigned int cmd_packet_size = 5;

// SPI Settings
SPISettings spi_settings(100000, MSBFIRST, SPI_MODE0);

// Robot settings

// Struct for processing command packets
typedef struct control {
  byte left_motor;
  byte right_motor;
  byte servo;
  byte buttons;
  byte health;
} Control;

int dead = 0;
Control control;

// IP Addresses
const IPAddress myIP(192, 168, 1, 44);
const IPAddress gateway(192, 168, 1, 1);
const IPAddress subnet(255, 255, 255, 0);

// Read data variables
byte cmd_buffer[cmd_packet_size];

// Servo variables
Servo arm;
int arm_angle = 0;

void setup() {
  // Set pin modes
  pinMode(D0, OUTPUT);
  pinMode(D1, OUTPUT);
  pinMode(D2, OUTPUT);
  pinMode(D6, OUTPUT);

  // Set the health display to full
  analogWrite(D2, 1023);

  // Enable serial connection
  Serial.begin(115200);

  // WiFi setup
  WiFi.begin(ssid, pwd);
  WiFi.config(myIP, gateway, subnet);

  // Connect to the wifi
  while(WiFi.status() != WL_CONNECTED) {
    delay(500);
    Serial.print(".");
  }
  Serial.print("Connected to network ");
  Serial.println(ssid);

  // Begin monitoring the UDP port for commands
  cmd_server.begin(cmd_port);
  Serial.print("Monitoring UDP port ");
  Serial.print(cmd_port);
  Serial.println(" for commands");

  // Initalize the servo
  arm.attach(D1);
  arm.write(100);

  // Set initial motor conditions
  control.left_motor = 100;
  control.right_motor = 100;
  control.servo = 1;
  control.buttons = 0;

  // Initialize SPI communication
  SPI.begin();
}

void loop() {
  // If a commmand packet is recieved
  if(handleCommandPacket()) {
    // Update health display
    if(dead) {
      // Turn off the health display if dead
      delay(1);
      analogWrite(D2, 0);
    }
    else {
      // Otherwise, light the display based on health
      if(control.health < 15) {
        analogWrite(D2, ceil((1024.0 / 99.0) * 15));
      }
      else {
        analogWrite(D2, ceil((1024.0 / 99.0) * control.health));
      }
    }
    
    // Update servo position
    int pos = arm_angle + (20 * (control.servo - 1));
    pos = pos > 150 ? 150 : pos < 20 ? 20 : pos;
    if(pos != arm_angle && !dead) {
      arm.write(pos);
      arm_angle = pos;
    }
  }

  // Communicate motor positions to the Teensy
  broadcastControl();

  // Turn on the hit indication LED if an appropriate signal is received
  if(analogRead(A0) >= 210 && !dead) {
    digitalWrite(D0, HIGH);
  }
  else {
    digitalWrite(D0, LOW);
  }
}

int handleCommandPacket() {
  // Check for command packet
  if(cmd_server.parsePacket() == cmd_packet_size) {
    // Read command data into a struct for later use
    cmd_server.read(cmd_buffer, cmd_packet_size);
    memcpy(&control, cmd_buffer, cmd_packet_size);

    // Update death state based on new health
    dead = control.health == 0;
    return 1;
  }
  return 0;
}

// Inform the Teensy of the current commanded motor states and death state via SPI
void broadcastControl() {
  SPI.beginTransaction(spi_settings);
  digitalWrite(D6, LOW);              // Flip the slave select line
  SPI.transfer(220);                  // Begin with a distinct byte outside of the regular data range (0 - 200) for ease of detection
  SPI.transfer(control.left_motor);   // Write left motor position
  SPI.transfer(control.right_motor);  // Write right motor position
  SPI.transfer((byte)dead);           // Write the value indicating whether the robot is currently dead
  SPI.transfer(221);                  // End with a different distinct byte for ease of detection
  digitalWrite(D6, HIGH);             // Flip the slave select line
  SPI.endTransaction();
}

