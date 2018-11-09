#include <ADCTouch.h>

#define AVG_NUM 5

int ref0, ref1;     //reference values to remove offset
int avg[AVG_NUM];

int delta_old = 0;

void setup() 
{
    // No pins to setup, pins can still be used regularly, although it will affect readings

    Serial.begin(9600);

    ref0 = ADCTouch.read(A0, 1500);    //create reference values to 
    ref1 = ADCTouch.read(A1, 1500);    //account for the capacitance of the pad
} 

void loop() 
{
    int value0 = ADCTouch.read(A0,300);   //no second parameter
    int value1 = ADCTouch.read(A1,300);   //   --> 100 samples

    value0 -= ref0;       //remove offset
    value1 -= ref1;


    int delta = (value1 - value0) / 10 * 10;
    byte delta_new = delta - delta_old;
    delta_old = delta;

    if(delta_new != 0)
    {
      Serial.write(delta_new);
    }
    delay(10);
}
