#include <Arduino.h>
#line 1 "C:\\Projects\\ArduinoProjects\\Source\\ArduinoSearchAllWifi\\ArduinoSearchAllWifi.ino"
#include <WiFiEsp.h>
#include <WiFiEspServer.h>
#include <HardwareSerial.h>

// Emulate Serial1 on Mega using Serial3 for ESP8266
#define ESP8266_BAUD 115200
#define ESP8266_SERIAL SERIAL_PORT_HARDWARE3

extern HardwareSerial Serial3;
#define HAVE_HWSERIAL3

#line 12 "C:\\Projects\\ArduinoProjects\\Source\\ArduinoSearchAllWifi\\ArduinoSearchAllWifi.ino"
void setup();
#line 35 "C:\\Projects\\ArduinoProjects\\Source\\ArduinoSearchAllWifi\\ArduinoSearchAllWifi.ino"
void loop();
#line 42 "C:\\Projects\\ArduinoProjects\\Source\\ArduinoSearchAllWifi\\ArduinoSearchAllWifi.ino"
void listNetworks();
#line 68 "C:\\Projects\\ArduinoProjects\\Source\\ArduinoSearchAllWifi\\ArduinoSearchAllWifi.ino"
void printEncryptionType(int thisType);
#line 92 "C:\\Projects\\ArduinoProjects\\Source\\ArduinoSearchAllWifi\\ArduinoSearchAllWifi.ino"
void printMacAddress(byte mac[]);
#line 12 "C:\\Projects\\ArduinoProjects\\Source\\ArduinoSearchAllWifi\\ArduinoSearchAllWifi.ino"
void setup() {
  //Initialize serial and wait for port to open:
  Serial.begin(115200);
  while (!Serial) {
    ; // wait for serial port to connect. Needed for native USB port only
  }

  // check for the WiFi module:
  Serial3.begin(115200);
  WiFi.init(&Serial3);
  if (WiFi.status() == WL_NO_SHIELD) {
    Serial.println("Communication with WiFi module failed!");
    // don't continue
    while (true);
  }

  // print your MAC address:
  byte mac[6];
  WiFi.macAddress(mac);
  Serial.print("MAC: ");
  printMacAddress(mac);
}

void loop() {
  // scan for existing networks:
  Serial.println("Scanning available networks...");
  listNetworks();
  delay(10000);
}

void listNetworks() {
  // scan for nearby networks:
  Serial.println("** Scan Networks **");
  int numSsid = WiFi.scanNetworks();
  if (numSsid == -1) {
    Serial.println("Couldn't get a wifi connection");
    while (true);
  }

  // print the list of networks seen:
  Serial.print("number of available networks:");
  Serial.println(numSsid);

  // print the network number and name for each network found:
  for (int thisNet = 0; thisNet < numSsid; thisNet++) {
    Serial.print(thisNet);
    Serial.print(") ");
    Serial.print(WiFi.SSID(thisNet));
    Serial.print("\tSignal: ");
    Serial.print(WiFi.RSSI(thisNet));
    Serial.print(" dBm");
    Serial.print("\tEncryption: ");
    printEncryptionType(WiFi.encryptionType(thisNet));
  }
}

void printEncryptionType(int thisType) {
  // read the encryption type and print out the title:
  switch (thisType) {
    case ENC_TYPE_WEP:
      Serial.println("WEP");
      break;
    case ENC_TYPE_WPA_PSK:
      Serial.println("WPA");
      break;
    case ENC_TYPE_WPA2_PSK:
      Serial.println("WPA2");
      break;
    case ENC_TYPE_NONE:
      Serial.println("None");
      break;
    case ENC_TYPE_WPA_WPA2_PSK:
      Serial.println("WPA WPA2 PSK");
      break;
     default:
      Serial.println("Unknown");
      break;
  }
}

void printMacAddress(byte mac[]) {
  for (int i = 5; i >= 0; i--) {
    if (mac[i] < 16) {
      Serial.print("0");
    }
    Serial.print(mac[i], HEX);
    if (i > 0) {
      Serial.print(":");
    }
  }
  Serial.println();
}
