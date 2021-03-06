#include <Arduino.h>
#line 1 "c:\\Users\\douwe\\Projects\\WizardsGlovesAdventure\\Arduino Source\\ArduinoProtocol\\src\\ArduinoProtocol.ino"
#include <Arduino_LSM9DS1.h>
#define LeftHand false

#if LeftHand
#define thumbPort A0
#define indexPort A2
#define middlePort A4
#define ringPort A5
#define pinkPort A7
#else
#define thumbPort A7
#define indexPort A6
#define middlePort A2
#define ringPort A1
#define pinkPort A0
#endif

// Filter coeff. (LOWPASS)
const float a2L = 1.16826067, a3L = -0.42411821, b1L = 0.06396438,
            b2L = 0.12792877, b3L = 0.06396438, b1 = 1.61912007490729,
            b2 = -3.07974939580975, b3 = 1.61912007490729,
            a2 = 1.33159324959517, a3 = -0.4900840036;

// Thresholds
float upper_threshold_thumb, lower_threshold_thumb,
    upper_threshold_index, lower_threshold_index,
    upper_threshold_middle, lower_threshold_middle,
    upper_threshold_ring, lower_threshold_ring,
    upper_threshold_pinky, lower_threshold_pinky;

// Movement sensor values
float xg, yg, zg, xa, ya, za, xm, ym, zm;

// Touch sensor values
boolean thumb_finger = false, index_finger = false, middle_finger = false, ring_finger = false, pinky_finger = false;

// Vibration motor values
int j = 0, flag = 0;

// Data processing arrays
int iterator_dif = 0, dif_size = 5;
float thumb_raw_data[3] = {0, 0, 0}, thumb_LP_data[3] = {0, 0, 0},
      thumb_dif_data[5] = {0, 0, 0, 0, 0}, thumb_dif_avg_data[5] = {0, 0, 0, 0, 0};

float index_raw_data[3] = {0, 0, 0}, index_LP_data[3] = {0, 0, 0},
      index_dif_data[5] = {0, 0, 0, 0, 0}, index_dif_avg_data[5] = {0, 0, 0, 0, 0};

float middle_raw_data[3] = {0, 0, 0}, middle_LP_data[3] = {0, 0, 0},
      middle_dif_data[5] = {0, 0, 0, 0, 0}, middle_dif_avg_data[5] = {0, 0, 0, 0, 0};

float ring_raw_data[3] = {0, 0, 0}, ring_LP_data[3] = {0, 0, 0},
      ring_dif_data[5] = {0, 0, 0, 0, 0}, ring_dif_avg_data[5] = {0, 0, 0, 0, 0};

float pinky_raw_data[3] = {0, 0, 0}, pinky_LP_data[3] = {0, 0, 0},
      pinky_dif_data[5] = {0, 0, 0, 0, 0}, pinky_dif_avg_data[5] = {0, 0, 0, 0, 0};

#line 57 "c:\\Users\\douwe\\Projects\\WizardsGlovesAdventure\\Arduino Source\\ArduinoProtocol\\src\\ArduinoProtocol.ino"
void setup();
#line 64 "c:\\Users\\douwe\\Projects\\WizardsGlovesAdventure\\Arduino Source\\ArduinoProtocol\\src\\ArduinoProtocol.ino"
void loop();
#line 74 "c:\\Users\\douwe\\Projects\\WizardsGlovesAdventure\\Arduino Source\\ArduinoProtocol\\src\\ArduinoProtocol.ino"
void startConnection();
#line 79 "c:\\Users\\douwe\\Projects\\WizardsGlovesAdventure\\Arduino Source\\ArduinoProtocol\\src\\ArduinoProtocol.ino"
void initializeMovementSensors();
#line 89 "c:\\Users\\douwe\\Projects\\WizardsGlovesAdventure\\Arduino Source\\ArduinoProtocol\\src\\ArduinoProtocol.ino"
void receiveMovementData();
#line 105 "c:\\Users\\douwe\\Projects\\WizardsGlovesAdventure\\Arduino Source\\ArduinoProtocol\\src\\ArduinoProtocol.ino"
void setThresholds();
#line 135 "c:\\Users\\douwe\\Projects\\WizardsGlovesAdventure\\Arduino Source\\ArduinoProtocol\\src\\ArduinoProtocol.ino"
void receiveTouchData();
#line 158 "c:\\Users\\douwe\\Projects\\WizardsGlovesAdventure\\Arduino Source\\ArduinoProtocol\\src\\ArduinoProtocol.ino"
void receiveAndFilterTouchSensor(float raw_data[3], float LP_data[3], uint8_t port);
#line 169 "c:\\Users\\douwe\\Projects\\WizardsGlovesAdventure\\Arduino Source\\ArduinoProtocol\\src\\ArduinoProtocol.ino"
void processDifferentialTouchData(boolean &state, float LP_value, float dif_data[], float avg_data[], float upper_threshold, float lower_threshold);
#line 186 "c:\\Users\\douwe\\Projects\\WizardsGlovesAdventure\\Arduino Source\\ArduinoProtocol\\src\\ArduinoProtocol.ino"
void sendData();
#line 202 "c:\\Users\\douwe\\Projects\\WizardsGlovesAdventure\\Arduino Source\\ArduinoProtocol\\src\\ArduinoProtocol.ino"
void receiveVibrationTrigger();
#line 57 "c:\\Users\\douwe\\Projects\\WizardsGlovesAdventure\\Arduino Source\\ArduinoProtocol\\src\\ArduinoProtocol.ino"
void setup()
{
  startConnection();
  initializeMovementSensors();
  setThresholds();
}

void loop()
{
  unsigned long current_time = micros();
  receiveMovementData();
  receiveTouchData();
  //  sendData();
  receiveVibrationTrigger();
  delayMicroseconds(10000 - (micros() - current_time)); // Delay until 10ms is achieved
}

void startConnection()
{
  Serial.begin(9600);
}

void initializeMovementSensors()
{
  if (!IMU.begin())
  {
    Serial.println("Failed to initialize IMU!");
    while (1)
      ;
  }
}

void receiveMovementData()
{
  if (IMU.magneticFieldAvailable())
  {
    IMU.readMagneticField(xm, ym, zm);
  }
  if (IMU.gyroscopeAvailable())
  {
    IMU.readGyroscope(xg, yg, zg);
  }
  if (IMU.accelerationAvailable())
  {
    IMU.readAcceleration(xa, ya, za);
  }
}

void setThresholds()
{
  if (LeftHand)
  {
    // upper_threshold_thumb = 40, lower_threshold_thumb = -40,
    // upper_threshold_index = 40, lower_threshold_index = -40,
    // upper_threshold_middle = 30, lower_threshold_middle = -30,
    // upper_threshold_ring = 25, lower_threshold_ring = -25,
    // upper_threshold_pinky = 70, lower_threshold_pinky = -70;
    upper_threshold_thumb = 40, lower_threshold_thumb = -10,
    upper_threshold_index = 40, lower_threshold_index = -10,
    upper_threshold_middle = 30, lower_threshold_middle = -10,
    upper_threshold_ring = 25, lower_threshold_ring = -10,
    upper_threshold_pinky = 70, lower_threshold_pinky = -10;
  }
  else
  {
    // upper_threshold_thumb = 40, lower_threshold_thumb = -45,
    // upper_threshold_index = 30, lower_threshold_index = -40,
    // upper_threshold_middle = 25, lower_threshold_middle = -30,
    // upper_threshold_ring = 25, lower_threshold_ring = -40,
    // upper_threshold_pinky = 15, lower_threshold_pinky = -20;
    upper_threshold_thumb = 40, lower_threshold_thumb = -10,
    upper_threshold_index = 30, lower_threshold_index = -10,
    upper_threshold_middle = 25, lower_threshold_middle = -10,
    upper_threshold_ring = 25, lower_threshold_ring = -10,
    upper_threshold_pinky = 15, lower_threshold_pinky = -10;
  }
}

void receiveTouchData()
{
  // THUMB
  receiveAndFilterTouchSensor(thumb_raw_data, thumb_LP_data, thumbPort);
  processDifferentialTouchData(thumb_finger, thumb_LP_data[0], thumb_dif_data, thumb_dif_avg_data, upper_threshold_thumb, lower_threshold_thumb);
  // INDEX
  receiveAndFilterTouchSensor(index_raw_data, index_LP_data, indexPort);
  processDifferentialTouchData(index_finger, index_LP_data[0], index_dif_data, index_dif_avg_data, upper_threshold_index, lower_threshold_index);
  // MIDDLE
  receiveAndFilterTouchSensor(middle_raw_data, middle_LP_data, middlePort);
  processDifferentialTouchData(middle_finger, middle_LP_data[0], middle_dif_data, middle_dif_avg_data, upper_threshold_middle, lower_threshold_middle);
  // RING
  receiveAndFilterTouchSensor(ring_raw_data, ring_LP_data, ringPort);
  processDifferentialTouchData(ring_finger, ring_LP_data[0], ring_dif_data, ring_dif_avg_data, upper_threshold_ring, lower_threshold_ring);
  // PINKY
  receiveAndFilterTouchSensor(pinky_raw_data, pinky_LP_data, pinkPort);
  processDifferentialTouchData(pinky_finger, pinky_LP_data[0], pinky_dif_data, pinky_dif_avg_data, upper_threshold_pinky, lower_threshold_pinky);
  //   After all arrays are updated set index to next element
  iterator_dif++;
  if (iterator_dif == dif_size)
    iterator_dif = 0;
}

void receiveAndFilterTouchSensor(float raw_data[3], float LP_data[3], uint8_t port)
{
  raw_data[0] = analogRead(port);
  float temp = a2 * LP_data[1] + a3 * LP_data[2] + b1 * raw_data[0] + b2 * raw_data[1] + b3 * raw_data[2];
  LP_data[0] = a2L * LP_data[1] + a3L * LP_data[2] + b1L * temp + b2L * raw_data[1] + b3L * raw_data[2];
  raw_data[2] = raw_data[1];
  raw_data[1] = raw_data[0];
  LP_data[2] = LP_data[1];
  LP_data[1] = LP_data[0];
}

void processDifferentialTouchData(boolean &state, float LP_value, float dif_data[], float avg_data[], float upper_threshold, float lower_threshold)
{
  float difference = LP_value - dif_data[iterator_dif];
  avg_data[iterator_dif] = difference;
  dif_data[iterator_dif] = LP_value;
  float avg = 0;
  for (int i = 0; i < dif_size; i++)
  {
    avg += avg_data[i];
  }
  avg /= dif_size;
  if (avg > upper_threshold)
    state = true;
  else if (avg < lower_threshold)
    state = false;
}

void sendData()
{
  String payload = LeftHand ? "Lhand:" : "Rhand:";
  payload = payload + "{";
  payload = payload + (thumb_finger ? "1" : "0") + ";";
  payload = payload + (index_finger ? "1" : "0") + ";";
  payload = payload + (middle_finger ? "1" : "0") + ";";
  payload = payload + (ring_finger ? "1" : "0") + ";";
  payload = payload + (pinky_finger ? "1" : "0") + ";";
  payload = payload + xm + ";" + ym + ";" + zm + ";";
  payload = payload + xg + ";" + yg + ";" + zg + ";";
  payload = payload + xa + ";" + ya + ";" + za;
  payload = payload + "}";
  Serial.println(payload);
}

void receiveVibrationTrigger()
{
  int incomingByte = 0;
  if (Serial.available() > 0)
  {
    incomingByte = Serial.read() - '0';
    if (incomingByte == 1)
      flag = 1;
    j = 0;
  }
  if (flag == 1)
  {
    digitalWrite(D3, HIGH);
    j++;
  }
  if (j >= 50)
  {
    digitalWrite(D3, LOW);
    flag = 0;
  }
}

