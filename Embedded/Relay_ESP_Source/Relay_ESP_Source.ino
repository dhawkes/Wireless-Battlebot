#include <ESP8266WiFi.h>
#include <WiFiUdp.h>

// WiFi settings
char * ssid = "Go Diego Go!";
char * pwd = "metateamsix";

// UDP settings
WiFiUDP cmd_server;
const unsigned int cmd_packet_size = 5;
byte cmd_buffer[cmd_packet_size];
const unsigned int cmd_port = 2380;

// IP Addresses
const IPAddress myIP(192, 168, 1, 44);
const IPAddress gateway(192, 168, 1, 1);
const IPAddress subnet(255, 255, 255, 0);

void setup() {
  // Enable serial connection
  Serial.begin(115200);

  // Turn on the onboard LED
  pinMode(D0, OUTPUT);
  digitalWrite(D0, LOW);

  // Setup wifi
  WiFi.begin(ssid, pwd);
  WiFi.config(myIP, gateway, subnet);

  // Connect to the wifi
  while(WiFi.status() != WL_CONNECTED) {
    delay(500);
    Serial.print(".");
  }
  Serial.print("Connected to network ");
  Serial.println(ssid);

  // Connect to UDP port
  cmd_server.begin(cmd_port);
}

void loop() {
  // Read messages from serial and relay them over UDP
  Serial.readBytes(cmd_buffer, cmd_packet_size);
  cmd_server.beginPacket(myIP, cmd_port);
  cmd_server.write(cmd_buffer, cmd_packet_size);
  cmd_server.endPacket();
  delay(50);
}

