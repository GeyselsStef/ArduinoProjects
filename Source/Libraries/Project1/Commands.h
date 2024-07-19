#pragma once
#ifndef Commands_h
#define Commands_h

#include "Arduino.h"
// #include <Delegates.h>

typedef void (*CommandRecieved)(int, byte*, int);

class Commands
{
public:
    Commands();
    void begin(Stream* stream);
    void setCommand(CommandRecieved command);
    void addCommand(int index, CommandRecieved command);
    void readOne();
	void setLog(bool log);

private:
	bool _log = false;

    uint8_t* _buffer;
    int _messageSize = 0;
    int _bufferSize = 32;
	int _bufferIndex = 0;

	int _startBit = 254;
	int _stopBit = 255;

	static Stream* _serial;
	CommandRecieved _command = NULL;
	int _numberOfCommands = 10;
	CommandRecieved* _commands = new CommandRecieved[_numberOfCommands];

	void processCommand(uint8_t* data);
	void addToBuffer(int c);
	void clearBuffer();
};

extern Commands Cmd;

#endif

#pragma hdrstop

