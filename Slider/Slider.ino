#include <ADCTouch.h>

#define AVG_NUM 3

int ref0, ref1;     //reference values to remove offset
int avg[AVG_NUM];

int delta_old = 0;


int avg_index = 0;



bool SameSign(int x, int y)
{
    return (x >= 0) ^ (y < 0);
}


void setup() 
{
    // No pins to setup, pins can still be used regularly, although it will affect readings

    Serial.begin(9600);

    ref0 = ADCTouch.read(A0, 1500);    //create reference values to 
    ref1 = ADCTouch.read(A1, 1500);    //account for the capacitance of the pad
} 

void loop() 
{
    int value0 = ADCTouch.read(A0,200);   //no second parameter
    int value1 = ADCTouch.read(A1,200);   //   --> 100 samples

    value0 -= ref0;       //remove offset
    value1 -= ref1;


    int delta = (value1 - value0) / 10 * 10;
    int delta_new = delta - delta_old;
    delta_old = delta;

    // Controllo che la differenza dal valore precedente sia:
    if(delta_new != 0         // - Diversa da 0
    && delta_new < 40         // - All'interno di un range di valori non troppo grande,
    && delta_new > -40)       //    questo perchè grandi variazioni di delta stanno a significare che l'utente ha tolto il dito dallo slider
    {

      // Controllo che l'index della coda sia maggiore di zero
      if(avg_index > 0)
      {
        // Ci sono già elementi della coda
        // Verifico che il nuovo che sto per inserire abbia lo stesso segno dei precedenti
        if(SameSign(delta_new, avg[0]))
        {
          int mean = 0;
          // Shifto la coda e lo aggiungo, nel mentre calcolo la media
          for(int i=avg_index; i > 0; i--)
          {
            avg[i] = avg[i-1];
            mean += avg[i-1];
          }
          // aggiungo nuovo valore
          avg[0] = delta_new;
          mean += delta_new;
          mean /= AVG_NUM;
          if(avg_index == AVG_NUM-1)
          {
            // Invio
            /*Serial.write(mean >> 8);      // MSB                  
            Serial.write(mean & 0xFF);    // LSB*/
            Serial.println(mean);
          }
          else
          {
            // incremento il contatore;
            avg_index++; 
          }         
          
        }
        else
        {
          // valore con segno differente, azzero il buffer e riparto a contare
          avg_index = 0;
        }       
      }
      else
      {
       // Altrimenti aggiungo semplicemente il primo valore alla coda
       avg[avg_index++] = delta_new;
      }
    }
    delay(10);
}
