const int SA0 = A0;
const int SA1 = A1;
const int SA2 = A2;
const int SA3 = A3;
const int SA4 = A4;
const int SA5 = A5;
const int SA6 = A6;
const int SA7 = A7;
const int SA8 = A8;

const char vali = ',';

void setup() {
  // put your setup code here, to run once:
  Serial.begin(9600);
  
  pinMode(SA0, INPUT);
  pinMode(SA1, INPUT);
  pinMode(SA2, INPUT);
  pinMode(SA3, INPUT);
  pinMode(SA4, INPUT);
  pinMode(SA5, INPUT);
  pinMode(SA6, INPUT);
  pinMode(SA7, INPUT);
  pinMode(SA8, INPUT);


}

void loop() {
  // put your main code here, to run repeatedly:
  
  int Value0 = analogRead(SA0);
  int Value1 = analogRead(SA1);
  int Value2 = analogRead(SA2);
  int Value3 = analogRead(SA3);
  int Value4 = analogRead(SA4);
  int Value5 = analogRead(SA5);
  int Value6 = analogRead(SA6);
  int Value7 = analogRead(SA7);
  int Value8 = analogRead(SA8);
  
  /*  
    Serial.print("Sensori0: ");
    Serial.print(Value0);
Serial.print("  ");
    Serial.print("Sensori1: ");
    Serial.print(Value1);
Serial.print("  ");
    Serial.print("Sensori2: ");
    Serial.print(Value2);
Serial.print("  ");
    Serial.print("Sensori3: ");
    Serial.print(Value3);
Serial.print("  ");
    Serial.print("Sensori4: ");
    Serial.print(Value4);
Serial.print("  ");
    Serial.print("Sensori5: ");
    Serial.print(Value5);
Serial.print("  ");
    Serial.print("Sensori6: ");
    Serial.print(Value6);
Serial.print("  ");
    Serial.print("Sensori7: ");
    Serial.print(Value7);
Serial.print("  ");
    Serial.print("Sensori8: ");
    Serial.print(Value8);
Serial.println("  ");
    delay(1000);
*/

    Value0 = map(Value0, 0, 1023, 0, 99);
    Value1 = map(Value1, 0, 1023, 0, 99);
    Value2 = map(Value2, 0, 1023, 0, 99);
    Value3 = map(Value3, 0, 1023, 0, 99);
    Value4 = map(Value4, 0, 1023, 0, 99);
    Value5 = map(Value5, 0, 1023, 0, 99);
    Value6 = map(Value6, 0, 1023, 0, 99);
    Value7 = map(Value7, 0, 1023, 0, 99);
    Value8 = map(Value8, 0, 1023, 0, 99);

    char buf[50];                                     //Tehdään array nimeltä buf
   sprintf(buf,"%d%c%d%c%d%c%d%c%d%c%d%c%d%c%d%c%d",Value0,vali,Value1,vali,Value2,vali,Value3,vali,Value4,vali,Value5,vali,Value6,vali,Value7,vali,Value8,vali,Value8);  //Syötetään arvot arrayhin
   Serial.println(buf);                               //Printataan serialiin buf, eli arvot yhtenä litaniana.
   delay(100);
  


}
