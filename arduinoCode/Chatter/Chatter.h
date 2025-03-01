/*Chatter.h - Library for Tact switch code.
  Created by MTDEV, October 17, 2021.
  Released into the public domain.*/

#ifndef Chatter_h
#define Chatter_h
#include "Arduino.h"
#include "BluetoothSerial.h"

class Chatter{
  public:
    Chatter(int pin);
    void chattering(int &count, int &rate, BluetoothSerial &bts);
  private:
    int _pin;
};

#endif