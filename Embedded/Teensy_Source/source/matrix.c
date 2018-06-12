/* Name: matrix.c
 * Author: Dylan Hawkes
 * Lab: 4
 */

#include "matrix.h"

#define REG_NOOP   0x00
#define REG_DIGIT0 0x01
#define REG_DIGIT1 0x02
#define REG_DIGIT2 0x03
#define REG_DIGIT3 0x04
#define REG_DIGIT4 0x05
#define REG_DIGIT5 0x06
#define REG_DIGIT6 0x07
#define REG_DIGIT7 0x08
#define REG_DECODEMODE  0x09
#define REG_INTENSITY   0x0A
#define REG_SCANLIMIT   0x0B
#define REG_SHUTDOWN    0x0C
#define REG_DISPLAYTEST 0x0F

/* Private Functions */
void put_byte(LedMatrix led, unsigned char data)
{
  int i = 8;
  unsigned char mask;
  while(i > 0) {
    mask = 0x01 << (i - 1);   // get bitmask
    write(led.clock, LOW);    // set the clock low
    if (data & mask) {        // choose bit
      write(led.data, HIGH);  // write a 1
    } else {
      write(led.data, LOW);   // write a 0
    }
    write(led.clock, HIGH);   // set the clock high
    --i;
  }
}

void set_register(LedMatrix led, unsigned char reg, unsigned char data)
{
  write(led.load, LOW);   // begin setting register
  put_byte(led, reg);     // specify the register
  put_byte(led, data);    // send data
  write(led.load, HIGH);  // latch the data
}

/* Public Functions */
void led_init(LedMatrix led) {
  // set SPI pins as output
  output(led.data);
  output(led.clock);
  output(led.load);

  // set initial matrix settings
  led_brightness(led, 0x04);
  led_scan_limit(led, 0x07);
  set_register(led, REG_SHUTDOWN, 0x01);
  set_register(led, REG_DECODEMODE, 0x00);
  set_register(led, REG_DISPLAYTEST, 0x00);
  led_clear(led);
}

// command a brightness (0 - 14)
void led_brightness(LedMatrix led, unsigned char val) {
  set_register(led, REG_INTENSITY, val & 0x0F);
}

// command a scan limit (0 - 7)
void led_scan_limit(LedMatrix led, unsigned char val)
{
  set_register(led, REG_SCANLIMIT, val & 0x07);
}

// clear the screen
void led_clear(LedMatrix led) {
  int i;
  for(i = 0; i < 8; i++) {
    set_register(led, i + 1, 0x00);
  }
}

// write a sprite to the led matrix
void led_write_sprite(LedMatrix led, Sprite sprite) {
  int i;
  for(i = 0; i < 8; i++) {
    set_register(led, i + 1, sprite[i]);
  }
}
