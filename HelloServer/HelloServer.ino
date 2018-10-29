#include <ESP8266WiFi.h>
#include <WiFiClient.h>
#include <ESP8266WebServer.h>
#include <ESP8266mDNS.h>
#include <math.h>

const char* ssid = "Telecom-85517507";
const char* password = "Canguro9.900";
WiFiClient client;


WiFiServer server(8052);


char incomingPacket[255];
char  replyPacekt[] = "Hi there! Got the message :-)";

bool RESET_MACHINE = true;

// the setup function runs once when you press reset or power the board
void setup() {
  pinMode(2, OUTPUT);
  //digitalWrite(2, 1);
  delay(1000);
  Serial.begin(115200);
  Serial.println();
  WiFi.mode(WIFI_STA);
  WiFi.begin(ssid, password);
  
  while (WiFi.status() != WL_CONNECTED) {
    delay(500);
    Serial.print(".");
  }


  Serial.println("");
  Serial.println("WiFi connected");  
  Serial.println("IP address: ");
  Serial.println(WiFi.localIP());

  // Start the server
  server.begin();
  Serial.println("Server started");

  // Print the IP address
  Serial.println(WiFi.localIP());
}





void loop() 
{
  if(RESET_MACHINE == true)
  {     
    // Check if a client has connected
    client = server.available(); 
    if (!client) {
      return;
    } 

    RESET_MACHINE = false;


    // Wait until the client sends some data
    Serial.println("new client");
    while(!client.available()){
      delay(1);
    }
    
    // Read the first line of the request
    String req = client.readStringUntil('\r');
    Serial.println(req);
    client.flush();
    
  }
  else
  {
    if(client.connected())
    {
      // Prepare the response
      byte buff[255];
      buff[0] = 1;

      // Send the response to the client
      client.write(buff,1);      
      delay(1);
      
    }
    else
    {
      client.stop();
      delay(1);
      RESET_MACHINE = true;
      Serial.println("Client disonnected");  
    }
  }
  delay(1000);

}
