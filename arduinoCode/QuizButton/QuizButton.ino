#include <Chatter.h>

Chatter chatt1(2);
Chatter chatt2(3);
Chatter chatt3(4);
Chatter chatt4(5);
Chatter chatt5(6);

int rate = 20;
int count1=0;
int count2=0;
int count3=0;
int count4=0;
int count5=0;

void setup() {
  // put your setup code here, to run once:
  Serial.begin(9600);
}

void loop() {
  // put your main code here, to run repeatedly:
  chatt1.chattering(count1, rate);
  Serial.print(":");
  chatt2.chattering(count2, rate);
  Serial.print(":");
  chatt3.chattering(count3, rate);
  Serial.print(":");
  chatt4.chattering(count4, rate);
  Serial.print(":");
  chatt5.chattering(count5, rate);
  Serial.println();
}
