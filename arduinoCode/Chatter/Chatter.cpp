/*
Chatter.cpp - Library for Tact switch code.
  Created by MTDEV, October 17, 2021.
  Released into the public domain.
*/

#include "Arduino.h"
#include "Chatter.h"
#include "BluetoothSerial.h"

Chatter::Chatter(int pin){
  pinMode(pin, INPUT_PULLUP);
  _pin = pin;
}

void Chatter::chattering(int &count, int &rate, BluetoothSerial& bts){
  if(digitalRead(_pin) == 1){  //押してる時
    if(count < rate)
    {
      bts.Serial.print(0);
      count++;
    }
    else if(count >= rate){
      bts.Serial.print(1);
    }
  }else{   //押してないとき
    count = 0;
    bts.Serial.print(0);
  }
}