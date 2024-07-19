#include <SoftwareSerial.h>
#include <AccelStepper.h>
#include <TestClass.h>

const int manualPin13=13;

const int stepsPerRevolution = 2048 * 2;
const int JoystickMaxValue = 32;

const int manualDirection = A0;
const int manualSpeed = A1;
const int manualSetTo0 = 8;

const String CommandLogLevel = "loglevel";
const String CommandLog = "log";
const String CommandManual = "manual";
const String CommandCW = "cw";
const String CommandCCW = "ccw";
const String CommandLocation = "location";
const String CommandStatus = "status";
const String CommandReset = "reset";
const String CommandSteps = "steps";
const String CommandStop = "stop";

bool doLoop = false;
String buffer = "";
int value = 0;
int lastSwitchValue = 1;
bool mustResetTo0 = false;
long lastLoggedPosition=0;

typedef enum
{
    None = 0,
    Verbose = 1,
    Command = 2,
    Warning = 3,
    Error = 4
} LogLevel;

LogLevel minimumLogLevel = LogLevel::Command;

AccelStepper stepper = AccelStepper(AccelStepper::HALF4WIRE, 9, 11, 10, 12);
SoftwareSerial BT(2,3);
TestClass tcls = TestClass();

void setup()
{
    Serial.begin(115200);
    while (!Serial)
    {
    }

    BT.end();
    BT.begin(9600);        

    pinMode(LED_BUILTIN, OUTPUT);

    stepper.setMaxSpeed(stepsPerRevolution / 4);
    stepper.setAcceleration(stepsPerRevolution / 16);

    pinMode(manualDirection, INPUT);
    pinMode(manualSpeed, INPUT);
    pinMode(manualSetTo0, INPUT_PULLUP);
    pinMode(manualPin13,INPUT);
}

void loop()
{
    if (digitalRead(manualPin13) == LOW)
    {
        int newVal = (map(analogRead(manualDirection), 0, 1024, 0, JoystickMaxValue) - (JoystickMaxValue / 2));
        if (value != newVal)
        {
            value = newVal;
        }

        if (value < -2 or value > 2)
        {
            int speed = map(analogRead(manualSpeed), 0, 1024, 1, 6);
            int step = value * speed;
            WriteCommand(CommandManual, String(step));
            stepper.move(step);
            delay((5 - speed) * 10);
        }
    }

    int currSwitchValue = digitalRead(manualSetTo0);
    if (currSwitchValue == 1 and lastSwitchValue == 0)
    {
        ResetPosition();
    }
    lastSwitchValue = currSwitchValue;

    if (!stepper.run())
    {
        if (doLoop)
        {
            doLoop = false;
            digitalWrite(LED_BUILTIN, LOW);
            LogLocation(true);
        }
    }
    else
    {
        digitalWrite(LED_BUILTIN, HIGH);
        LogLocation(false);
    }

    if (Serial.peek() >= 0)
    {
        int val = Serial.read();
        if ((val >= 33 and val <= 90) or (val >= 97 and val <= 122) or val == 124)
        {
            char charVal = val;
            buffer += charVal;
        }
        if (val == 91 and buffer.length() > 0) // [
        {
            buffer = "";
            WriteLog(LogLevel::Verbose, "Buffer cleared.", false);
        }
        if (val == 93) // ]
        {
            DecodeString(buffer);
            buffer = "";
        }
    }

    if (BT.peek() >= 0)
    {
        int val = BT.read();
        if ((val >= 33 and val <= 90) or (val >= 97 and val <= 122) or val == 124)
        {
            char charVal = val;
            buffer += charVal;
        }
        if (val == 91 and buffer.length() > 0) // [
        {
            buffer = "";
            WriteLog(LogLevel::Verbose, "Buffer cleared.", false);
        }
        if (val == 93) // ]
        {
            DecodeString(buffer);
            buffer = "";
        }
    }
}

void DecodeString(String str)
{

    String inputstring = str;
    String *parts = Split(inputstring, '|');

    if (parts[0] == CommandReset)
    {
        ResetPosition();
    }
    else if (parts[0] == CommandCW)
    {
        float rotations = parts[1].toFloat();
        long steps = stepsPerRevolution * rotations;
        Run(steps);
    }
    else if (parts[0] == CommandCCW)
    {
        float rotations = parts[1].toFloat();
        long steps = (stepsPerRevolution * rotations) * -1;
        Run(steps);
    }
    else if (parts[0] == CommandSteps)
    {
        long runto = stepper.currentPosition() + parts[1].toInt();
        Run(runto);
    }
    else if (parts[0] == CommandLogLevel)
    {
        minimumLogLevel = static_cast<LogLevel>(parts[1].toInt());
        WriteLog(LogLevel::Verbose,  "Loglevel -> " + String(minimumLogLevel), true);
    }
    else if (parts[0] == CommandStatus)
    {
        LogLocation(true);
        WriteCommand(CommandLogLevel, String(minimumLogLevel));
    }
    else if (parts[0] == CommandStop)
    {
        stepper.setCurrentPosition(stepper.currentPosition());
       LogLocation(true);
    }
    else
    {
        int inp = inputstring.toInt();
        Run(inp);
    }
}

void ResetPosition()
{
    stepper.setCurrentPosition(0);
    stepper.setMaxSpeed(stepsPerRevolution / 4);
    WriteCommand( CommandReset, "0");
}

void Run(long runto)
{
    stepper.moveTo(runto);
    doLoop = true;
    WriteLog(LogLevel::Verbose, "Start rotating to " + String(stepper.targetPosition()), false);
}

void LogLocation(bool force)
{
    long currentPosition = stepper.currentPosition();
    if (force or abs(abs(lastLoggedPosition) - abs(currentPosition)) > 50)
    {
        WriteCommand(CommandLocation, String(currentPosition));
        lastLoggedPosition = currentPosition;
    }
}

void WriteCommand(String command, String str)
{
    Serial.print("[" + command + "|" + str + "]");
    BT.print("[" + command + "|" + str + "]");
}

void WriteCommand(String command, String str, String param)
{
    Serial.print("[" + command + "|" + str + "|" + param + "]");
    BT.print("[" + command + "|" + str + "|" + param + "]");
}

void WriteLog(LogLevel level, String str, bool force)
{
    if ((minimumLogLevel > 0 and level >= minimumLogLevel) or force or level==LogLevel::Command)
    {
        WriteCommand(CommandLog, str, String(level));
    }
}

String *Split(String str, char delimitter)
{

    int counter = 0;
    static String result[10];

    while (str.length() > 0)
    {
        int index = str.indexOf(delimitter);

        if (index == -1)
        {
            result[counter] = str;
            return result;
        }
        else
        {
            result[counter++] = str.substring(0, index);
            str = str.substring(index + 1);
        }
    }
    return result;
}