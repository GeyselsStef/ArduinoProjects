#include "Commands.h"

Stream* Commands::_serial;

Commands::Commands()
{
	_buffer = new uint8_t[_bufferSize];
}

void Commands::begin(Stream* serial)
{
	_serial = serial;
}

void Commands::setCommand(CommandRecieved command)
{
	_command = command;
}

void Commands::addCommand(int index, CommandRecieved func)
{
	if (index >= _numberOfCommands)
	{
		if (_log)
		{
			Serial.println("Index is greater than the number of commands, resizing array");
		}

		int newSize = index + 5;
		CommandRecieved* newCommands = new CommandRecieved[newSize];
		for (int i = 0; i < _numberOfCommands; i++)
		{
			newCommands[i] = _commands[i];
		}
		delete[] _commands;
		_commands = newCommands;
		_numberOfCommands = newSize;
	}
	_commands[index] = func;

}

void Commands::readOne()
{
	int c;
	if (_serial and _serial->available() and _serial->peek() >= 0)
	{
		c = _serial->read();
		if (_log) { Serial.println(c); }
		if (c == _startBit)
		{
			if (_log) { Serial.println("Clearing buffer"); }
			clearBuffer();
		}
		else if (c == _stopBit)
		{
			if (_messageSize == _bufferIndex)
			{
				processCommand(_buffer);
			}
			else if (_log) {
				Serial.print("Message size \"");
				Serial.print(_messageSize);
				Serial.print("\" is not equal to the buffersize: ");
				Serial.println(_bufferIndex);
			}
		}
		else
		{
			if (_messageSize == 0)
			{
				if (_log) {
					Serial.print("Message size: ");
					Serial.println(c);
				}
				_messageSize = c;
			}
			else
			{
				addToBuffer(c);
			}
		}
	}
}

void Commands::setLog(bool log)
{
	_log = log;
}

void Commands::processCommand(uint8_t* data)
{
	if (_log) { Serial.println("Processing command"); }

	int adr1 = data[0] * 100;
	int adr2 = data[1] * 10;
	int adr3 = data[2];

	int address = adr1 + adr2 + adr3;

	int messageSize = _messageSize - 3;
	uint8_t* newBuffer = new uint8_t[messageSize];
	if (_log) {
		Serial.print("Message buffer size: ");
		Serial.println(messageSize);
	}
	for (int i = 0; i < messageSize; i++)
	{
		newBuffer[i] = data[i + 3];
	}

	if (_command != NULL)
	{
		_command(address, newBuffer, messageSize);
	}

	if (address < _numberOfCommands && _commands[address] != NULL)
	{
		_commands[address](address, newBuffer, messageSize);
	}

	delete[] newBuffer;

	clearBuffer();
}

void Commands::addToBuffer(int c)
{
	if (_log) {
		Serial.print("Add to buffer index ");
		Serial.print(_bufferIndex);
		Serial.print(": ");
		Serial.println(c);
	}
	if (_bufferIndex >= _bufferSize - 1)
	{
		int newSize = _bufferSize * 2;
		uint8_t* newBuffer = new uint8_t[newSize];
		for (int i = 0; i < _bufferSize; i++)
		{
			newBuffer[i] = _buffer[i];
		}
		delete[] _buffer;
		_buffer = newBuffer;
		_bufferSize = newSize;
	}

	_buffer[_bufferIndex] = c;
	_bufferIndex++;
}

void Commands::clearBuffer()
{
	for (int i = 0; i < sizeof(_buffer); i++)
	{
		_buffer[i] = 0;
	}
	_messageSize = 0;
	_bufferIndex = 0;
}

Commands Cmd;