/* Name: main.c
 * Author: Dylan Hawkes
 */

#include "matrix.h"
#include "general.h"
#include "input_capture.h"

#define PACKET_SIZE 4
#define FREQ 226
#define TOLERANCE 100
#define ANIMATION_LENGTH 1000

unsigned char buffer[PACKET_SIZE];
volatile int pos = 0;
volatile int process = 0;
int dead = 0;

// SPI interrupt
ISR(SPI_STC_vect) {
  unsigned char c = SPDR;

  // Wait until the unique starting byte is detected
  if(pos == 0) {
    if(c == 220) {
      pos++;
    }
    return;
  }

  buffer[pos - 1] = c;
  pos++;
  if(pos == PACKET_SIZE + 1) {
    // Only process the packet if it ends with the unique ending byte
    if(buffer[pos - 2] == 221) {
      process = 1;
    }
    pos = 0;
  }
}

int main(void)
{
  // LED sprites
  unsigned char one[8] =
  {
    B00001000,
    B00011000,
    B00111000,
    B00011000,
    B00011000,
    B00011000,
    B01111110,
    B01111110
  };
  unsigned char two[8] =
  {
    B00111100,
    B01000010,
    B01000010,
    B00000100,
    B00001000,
    B00010000,
    B00100000,
    B01111110
  };
  unsigned char three[8] =
  {
    B00111100,
    B01000010,
    B00000010,
    B00011100,
    B00000010,
    B01000010,
    B00111100,
    B00000000
  };
  unsigned char four[8] =
  {
    B1100110,
    B1100110,
    B1100110,
    B1111110,
    B1111110,
    B0000110,
    B0000110,
    B0000110
  };
  unsigned char explosion[8] =
  {
    B10011001,
    B01011010,
    B00111100,
    B11111111,
    B11111111,
    B00111100,
    B01011010,
    B10011001
  };
  unsigned char clear[8] =
  {
    B00000000,
    B00000000,
    B00000000,
    B00000000,
    B00000000,
    B00000000,
    B00000000,
    B00000000
  };
  unsigned char skull[8] =
  {
    B0111110,
    B1111111,
    B1001001,
    B1111111,
    B1110111,
    B0111110,
    B0101010,
    B0000000
  };

  // set up teensy
  teensy_clockdivide(0);  // set the clock speed to 16Mhz
  m_usb_init();

  // set pin intial states
  output(PD(6));
  write(PD(6), ON);
  output(PF(0));
  write(PF(0), OFF);

  // set up the LED matrix
  LedMatrix led;
  led.load = PD(0);
  led.clock = PD(1);
  led.data = PD(2);
  
  led_init(led);
  led_write_sprite(led, one);

  // begin input capture
  begin_input_capture();

  // setup SPI 
  set(SPCR, SPE);   // enable SPI
  set(SPCR, SPIE);  // enable the SPI interrupt
  sei();

  // define motors
  Motor left = (Motor) {PB(7), PF(4), TIMER1, OCR1A_DEF};
  Motor right = (Motor) {PF(5), PF(6), TIMER1, OCR1B_DEF};
  
  while(1) {
    // process SPI packet
    if(process) {
      process = 0;
      
      // turn off LEDs if dead
      if(buffer[2] == 1) {
        dead = 1;
        led_clear(led);
        write(PF(0), OFF);
        drive_motor(left, 0);
        drive_motor(right, 0);
      }
      else {
        // turn the led matrix back on if alive
        if(dead) {
          dead = 0;
          led_write_sprite(led, one);
        }

        // indicate whether a healing frequency is being detected
        write(PF(0), is_target_frequency(FREQ, TOLERANCE));

        // drive motors based on SPI command
        // PWM signals are halved in order to increase controllability
        drive_motor(left, ((int)buffer[0] - 100) / 2);
        drive_motor(right, ((int)buffer[1] - 100) / 2);
      }
    }
  }

  return 0; // never occurs
}
