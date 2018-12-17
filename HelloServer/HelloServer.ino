#include <ESP8266WiFi.h>
#include <WiFiClient.h>
#include <ESP8266WebServer.h>
#include <ESP8266mDNS.h>
#include <math.h>

#define DEBUG

const char* ssid = "SiberNet";
const char* password = "fragoleAndroid";
WiFiClient client;
IPAddress ip(192, 168, 43, 20); //set static ip
IPAddress gateway(192, 168, 43, 1); //set getteway
IPAddress subnet(255, 255, 255, 0);//set subnet

WiFiServer server(8052);


char incomingPacket[255];
char  replyPacekt[] = "Hi there! Got the message :-)";

bool RESET_MACHINE = true;

// the setup function runs once when you press reset or power the board
void setup() {
  delay(1000);
  Serial.begin(9600);  
  //WiFi.mode(WIFI_STA);
  WiFi.config(ip, gateway, subnet);
  WiFi.begin(ssid, password);
  
  while (WiFi.status() != WL_CONNECTED) {
    delay(500);
#ifdef DEBUG
    Serial.print(".");
#endif
  }

#ifdef DEBUG
  Serial.println("");
  Serial.println("WiFi connected");  
  Serial.println("IP address: ");
  Serial.println(WiFi.localIP());
#endif

  server.begin();
}





void loop() 
{
  //Serial.println(analogRead(0));
  if(RESET_MACHINE == true)
  {     
    // Check if a client has connected
    client = server.available(); 
    if (!client) {
      return;
    } 

    RESET_MACHINE = false;


    // Wait until the client sends some data
#ifdef DEBUG
    Serial.println("new client");
#endif
    /*while(!client.available()){
      delay(1);
    }
    
    // Read the first line of the request
    String req = client.readStringUntil('\r');
    Serial.println(req);
    client.flush();*/
    
  }
  else
  {
    if(client.connected())
    {
      if (Serial.available() > 0)
      {
        if (Serial.available() > 1) {
          // Prepare the response
          byte buff[2];
          buff[1] = Serial.read();  // gets MSB byte from serial buffer     
          buff[0] = Serial.read();  //gets LSB byte from serial buffer

          // Send the int to the client
          client.write(buff,2); 
        }else{
          // 2 tentativo
          delay(1);
          if (Serial.available() > 1) {
            // Prepare the response
            byte buff[2];
            buff[1] = Serial.read();  // gets MSB byte from serial buffer     
            buff[0] = Serial.read();  //gets LSB byte from serial buffer
            
            // Send the int to the client
            client.write(buff,2); 
          }
          else
          {
            // pulisco il buffer seriale
            Serial.flush();   
          }
        }
        delay(1);
      }
      
    }
    else
    {
      client.stop();
      delay(1);
      RESET_MACHINE = true;
#ifdef DEBUG
      Serial.println("Client disonnected");  
#endif
    }
  }
  delay(10);

}
