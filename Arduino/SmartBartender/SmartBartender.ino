#include <SoftwareSerial.h>

SoftwareSerial BT(12, 13);

int Pump1 = 5; //Elderflower Syrup
int Pump2 = 6; //Sparkling Water
int Pump3 = 9; //Lemon Juice
int Pump4 = 10; //Mint Syrup

//HC-SR04 ultrasonic sensor pins
int trigPin = 11;
int echoPin = 3;

long duration;
int distance;

char state;

void runPump(int pumpPin, int pwm, int duration, const char* pumpName)
{
    BT.print(pumpName);
    BT.println(" ON");

    analogWrite(pumpPin, pwm);
    delay(duration);

    analogWrite(pumpPin, 0);

    BT.print(pumpName);
    BT.println(" OFF");
}

//Measure distance between sensor and glass
int getDistance()
{
    digitalWrite(trigPin, LOW);
    delayMicroseconds(2);

    digitalWrite(trigPin, HIGH);
    delayMicroseconds(10);

    digitalWrite(trigPin, LOW);

    duration = pulseIn(echoPin, HIGH);

    distance = duration * 0.034 / 2;

    return distance;
}

void primePumps()
{
    BT.println("PUMPING PUMPS");

    // Pump1 - Elderflower Syrup
    runPump(Pump1, 255, 2000, "Pump1");

    // Pump2 - Sparkling Water
    runPump(Pump2, 200, 2000, "Pump2");

    // Pump3 - Lemon Juice
    runPump(Pump3, 255, 2000, "Pump3");

    // Pump4 - Mint Syrup
    runPump(Pump4, 255, 2000, "Pump4");

    BT.println("PUMPING PUMPS COMPLETED!");

    state = 0;
}

bool isGlassDetected()
{
    int glassDistance = getDistance();

    if(glassDistance > 15)
    {
        Serial.println("No glass detected!");
        state = 0;
        return false;
    }

    return true;
}

void setup() 
{
    Serial.begin(9600);
    
    BT.begin(9600);  

    pinMode(Pump1, OUTPUT);
    pinMode(Pump2, OUTPUT);
    pinMode(Pump3, OUTPUT);
    pinMode(Pump4, OUTPUT);
    
    pinMode(trigPin, OUTPUT);
    pinMode(echoPin, INPUT);
    
    analogWrite(Pump1, 0);
    analogWrite(Pump2, 0);
    analogWrite(Pump3, 0);
    analogWrite(Pump4, 0);

    BT.println("Bluetooth connected!");
}

void loop() 
{
    if(BT.available() > 0) 
    {
        state = BT.read();

        BT.print("Received command: ");
        BT.println(state);
    }

    //Pumping Pumps
    if(state == 'P')
    {
        primePumps();
    }
    
    // Elderflower Soda
    if(state == 'E')
    {
        if(!isGlassDetected())
            return;

        BT.println("Preparing Elderflower Soda");

        //Elderflower Syrup
        runPump(Pump1, 255, 2500, "Pump1");
        delay(500);

        //Sparkling Water
        runPump(Pump2, 200, 11000, "Pump2");
        delay(500);

        //Lemon Juice
        runPump(Pump3, 255, 2000, "Pump3");

        BT.println("Elderflower Soda completed!");
        
        state = 0;
    }

    // Hugo Mocktail
    if(state == 'H')
    {
        if(!isGlassDetected())
            return;

        BT.println("Preparing Hugo Mocktail");

        //Elderflower Syrup  
        runPump(Pump1, 255, 2000, "Pump1");
        delay(500);

        //Sparkling Water
        runPump(Pump2, 200, 11000, "Pump2");
        delay(500);

        //Lemon Juice
        runPump(Pump3, 255, 2000, "Pump3");
        delay(500);

        //Mint Syrup
        runPump(Pump4, 255, 1500, "Pump4");
        
        BT.println("Hugo Mocktail completed!");

        state = 0;
    }

    // Mint Lemonade
    if(state == 'M')
    {
        if(!isGlassDetected())
            return;

        BT.println("Preparing Mint Lemonade");

        //Lemon Juice
        runPump(Pump3, 255, 3000, "Pump3");
        delay(500);
        
        //Sparkling Water
        runPump(Pump2, 200, 12000, "Pump2");
        delay(500);

        //Mint Syrup
        runPump(Pump4, 255, 1500, "Pump4");
        delay(500);

        BT.println("Mint Lemonade completed!");
        
        state = 0;
    }

    // Green Soda
    if(state == 'G')
    {
        if(!isGlassDetected())
            return;

        BT.println("Preparing Green Soda");

        //Mint Syrup
        runPump(Pump4, 255, 2500, "Pump4");

        //Sparkling Water
        runPump(Pump2, 200, 13000, "Pump2");
        delay(500);

        BT.println("Green Soda completed!");

        state = 0;
    }

    // Manual Pump1
    if(state == '1')
    {
        if(!isGlassDetected())
            return;

        BT.println("Manual Pump1");

        runPump(Pump1, 255, 2000, "Pump1");

        state = 0;

        BT.println("Pump 1 completed.");
    }

    // Manual Pump2
    if(state == '2')
    {
       if(!isGlassDetected())
            return;

        BT.println("Manual Pump2");

        runPump(Pump2, 200, 2000, "Pump2");

        state = 0;

        BT.println("Pump 2 completed.");
    }

    // Manual Pump3
    if(state == '3')
    {
        if(!isGlassDetected())
            return;

        BT.println("Manual Pump3");

        runPump(Pump3, 255, 2000, "Pump3");

        state = 0;

        BT.println("Pump 3 completed.");
    }

    // Manual Pump4
    if(state == '4')
    {
        if(!isGlassDetected())
            return;

        BT.println("Manual Pump4");

        runPump(Pump4, 255, 2000, "Pump4");

        state = 0;

        BT.println("Pump 4 completed.");
    }

    //Stop All Pumps
    if(state == 'S')
    {
        BT.println("STOP ALL PUMPS");

        analogWrite(Pump1, 0);
        Serial.println("Pump1 OFF");
        analogWrite(Pump2, 0);
        Serial.println("Pump2 OFF");
        analogWrite(Pump3, 0);
        Serial.println("Pump3 OFF");
        analogWrite(Pump4, 0);
        Serial.println("Pump4 OFF");

        state = 0;

        BT.println("STOP ALL PUMPS COMPLETED");
    }
}
