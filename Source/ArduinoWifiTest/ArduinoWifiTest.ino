#include <WiFiEsp.h>
#include <WiFiEspServer.h>
#include <HardwareSerial.h>
#include <Wire.h>
#include <LiquidCrystal_I2C.h>
#include <Commands.h>
#include <WiFiManager.h>

// Emulate Serial1 on Mega using Serial3 for ESP8266
#define ESP8266_BAUD 115200
#define ESP8266_SERIAL SERIAL_PORT_HARDWARE3

char ssid[] = "Geysels_2.4"; // your network SSID (name)
char pass[] = "WW4home!";    // your network password

int status = WL_IDLE_STATUS;
long time;
long lastDisplayUpdateTime;
bool ssidToggle=false;
String buffer="";
bool continuWriting;

WiFiEspServer server(80);
WiFiEspClient client;
LiquidCrystal_I2C lcd(0x27, 16, 2);
//Commands cmd;

extern HardwareSerial Serial1;
#define HAVE_HWSERIAL1
extern HardwareSerial Serial3;
#define HAVE_HWSERIAL3

byte backslash[8] = {0x00, 0x10, 0x08, 0x04, 0x02, 0x01, 0x00, 0x00};
byte wifi2[8] = {0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x08, 0x18};
byte wifi3[8] = {0x00, 0x00, 0x00, 0x00, 0x00, 0x04, 0x0c, 0x1c};
byte wifi1[8] = {0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x10};
byte wifi4[8] = {0x00, 0x00, 0x00, 0x00, 0x02, 0x06, 0x0e, 0x1e};
byte wifi5[8] = {0x00, 0x00, 0x00, 0x01, 0x03, 0x07, 0x0f, 0x1f};
byte clientDisconnected[8] = {0x1f, 0x11, 0x11, 0x11, 0x11, 0x11, 0x11, 0x1f};
byte clientConnected[8] = {0x1f, 0x11, 0x1b, 0x15, 0x15, 0x1b, 0x11, 0x1f};

void setup()
{
    // initialize serial for debugging
    // lcd.begin(16, 2, 0U);
    lcd.init();
    lcd.load_custom_character(0, backslash);
    lcd.load_custom_character(1, wifi1);
    lcd.load_custom_character(2, wifi2);
    lcd.load_custom_character(3, wifi3);
    lcd.load_custom_character(4, wifi4);
    lcd.load_custom_character(5, wifi5);
    lcd.load_custom_character(6, clientDisconnected);
    lcd.load_custom_character(7, clientConnected);
    printLcd("Starting", "Arduino", true);
    delay(500);

    Serial.begin(115200);
    Serial1.begin(115200);
    Serial3.begin(115200);

    //Cmd.begin(&Serial1);
    Cmd.setLog(true);
    Cmd.setCommand(ProcessCmd);
    Cmd.addCommand(1,CmdGoTo);

    pinMode(LED_BUILTIN, OUTPUT);

    // initialize ESP module
    printLcd("Starting", "Wifi", true);
    WiFi.init(&Serial3);

    // check for the presence of the shield
    if (WiFi.status() == WL_NO_SHIELD)
    {
        Serial.println("WiFi shield not present");
        printLcd("Connection failed.", "No Wifi shield", true);
        while (true)
            ;
    }

    // attempt to connect to WiFi network
    printLcd("Connecting Wifi", String(ssid), true);
        WiFi.disconnect();
    while (status != WL_CONNECTED)
    {
        Serial.print("Attempting to connect to SSID: ");
        Serial.println(ssid);
        status = WiFi.begin(ssid, pass);

        for (int counter = 1; counter < 41; counter++)
        {
            lcdStatus(15, 1, counter, 250);
            Serial.println(WiFi.localIP());            
        }
    }

    Serial.println("Connected to wifi");
    printWifiStatus();
    printLcdStatus(true);

    // start the server
    server.begin();
}

void loop()
{
    printLcdStatus(false);

    if (false and Serial1.peek() >= 0)
    {
        int val = Serial1.read();
        if ((val >= 33 and val <= 90) or (val >= 97 and val <= 122) or val == 124)
        {
            char charVal = val;
            buffer += charVal;
        }
        if (val == 91 and buffer.length() > 0) // [
        {
            buffer = "";
            // WriteLog(LogLevel::Verbose, "Buffer cleared.", false);
        }
        if (val == 93) // ]
        {
            Serial.println("Serial1: [" + buffer + "]");
            if (client and client.connected())
            {
                client.print("[" + buffer + "]");
            }
            // DecodeString(buffer);
            buffer = "";
        }
    }

    // check if a client has connected
    // if (!client or !client.connected())
    // {
        WiFiEspClient newClient = server.available();
        if (newClient)
        {
            Serial.println("New Client " + String(newClient.status()));
            client = newClient;
            Cmd.begin(&newClient);
            client.onError(t);
            // continuWriting=true;
            // while (continuWriting)
            // {
            //     client.write('.');
            // }
        }
    // }
    ProcessClient();

    if (false and millis() - time > 5000)
    {
        time = millis();
        if (client and client.connected())
        {
            digitalWrite(LED_BUILTIN, LOW);
            Serial.println(String(time));
            client.println(String(time));
        }
    }
}

void ProcessCmd(int command, uint8_t *data,int messageSize)
{
    Serial.print("Command ");
    Serial.print(command);
    Serial.print(": ");

    for (int i = 0; i < messageSize; i++)
    {
        Serial.print(data[i]);
    }
    Serial.println();
}

void CmdGoTo(int cmdIndex, uint8_t *data, int messageSize)
{
    String finalValue = "[";
    if (data[0] == 0)
    {
        finalValue += "-";
    }

    int value = data[1];
    if (messageSize > 2)
    {
        int factor = data[2];
        value *= factor;
    }

    finalValue += value;
    finalValue += "]";

    Serial.println(finalValue);
    Serial1.print(finalValue);
}

int t(int f)
{
    Serial.println("OnError " + String(f));
    continuWriting = false;
    client.clearWriteError();
    delay(500);
    return 4000;
}

void ProcessClient()
{
    // if (client.connected()){
    //     while (client.peek()>=0)
    //     {
           Cmd.readOne();     /* code */
    //     }        
    // }

    return;

    // Serial.println("New client connected");
    if (client.connected())
    {
        lcd.setCursor(15, 0);
        lcd.write(7);

        String currentLine = "";
        String command = "";
        while (client.connected() and client.available())
        {
            char c = client.read();
            Serial.write(c);
            if (c == '\n')
            {
                if (currentLine.length() == 0)
                {
                    // send HTTP response
                    String response = "<html><body><h1>Hello, World!</h1></body></html>";
                    client.println("HTTP/1.1 200 OK");
                    delay(100);
                    client.println("Content-type:text/html");
                    delay(100);
                    client.println("Content-length:" + String(response.length()));
                    delay(100);
                    client.println();
                    client.print(response);
                    client.println();
                    break;
                }
                else
                {
                    currentLine = "";
                }
            }
            else if (c == '[')
            {
                command = "";
            }
            else if (c == ']')
            {
                Serial.println();
                digitalWrite(LED_BUILTIN, HIGH);
                Serial1.print("["+command+"]");
                client.println(command);
                command = "";
                // printWifiStatus();
            }
            else if (c != '\r')
            {
                currentLine += c;
                command += c;
            }
        }
    }
    if (!client.connected() or client.status()==CLOSED)
    {
        lcd.setCursor(15, 0);
        lcd.write(6);
    }
}

void printWifiStatus()
{
    // print the SSID of the network you're connected to
    Serial.print("SSID: ");
    Serial.println(WiFi.SSID());

    // print your WiFi shield's IP address
    IPAddress ip = WiFi.localIP();
    Serial.print("IP Address: ");
    Serial.println(ip);

    // print the received signal strength
    long rssi = WiFi.RSSI();
    Serial.print("Signal strength (RSSI):");
    Serial.print(rssi);
    Serial.println(" dBm");
}

void lcdStatus(int column, int row, int progressStep, int delayTime)
{
    lcd.setCursor(column, row);
    switch (progressStep % 4)
    {
    case 1:
        lcd.write(0);
        break;
    case 2:
        lcd.print("|");
        break;
    case 3:
        lcd.print("/");
        break;
    case 0:
        lcd.print("-");
        break;

    default:
        lcd.print("?");
        break;
    }
    delay(delayTime);
}

void printLcdStatus(bool force)
{
    if (force or millis() - lastDisplayUpdateTime > 2000)
    {
        lastDisplayUpdateTime = millis();

        String line1 = "";
        if (!ssidToggle)
        {
            line1 = WiFi.SSID();
        }
        else
        {
            IPAddress ipAdress = WiFi.localIP();
            line1 = String(ipAdress[0]) + "." + String(ipAdress[1]) + "." + String(ipAdress[2]) + "." + String(ipAdress[3]);
        }
        ssidToggle = !ssidToggle;

        if (line1.length() > 16)
        {
            line1 = line1.substring(0, 14);
        }
        while (line1.length() < 15)
        {
            line1 += " ";
        }

        long rssiLng = WiFi.RSSI();
        printLcd(line1, "RSSI " + String(rssiLng) + "dBm    ", force);

        int level = 0;
        if (rssiLng > -80)
        {
            level = 1;
        }
        if (rssiLng > -70)
        {
            level = 2;
        }
        if (rssiLng > -67)
        {
            level = 3;
        }
        if (rssiLng > -60)
        {
            level = 4;
        }
        if (rssiLng > -50)
        {
            level = 5;
        }
        lcd.setCursor(15, 1);
        if (level > 0)
        {
            lcd.write(level);
        }
        else
        {
            lcd.print("X");
        }
    }
}

void printLcd(String firstLine, String secondLine, bool clear)
{

    if (clear)
    {
        lcd.clear();
    }

    lcd.backlight();

    lcd.setCursor(0, 0);
    lcd.print(firstLine);

    lcd.setCursor(0, 1);
    lcd.print(secondLine);
}
