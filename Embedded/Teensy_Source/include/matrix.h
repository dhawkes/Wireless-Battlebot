/* Name: matrix.h
 * Author: Dylan Hawkes
 * Description: Library to run a MAX72XX led matrix, including
 *              a software SPI implementation
 */

#ifndef matrix_h
#define matrix_h

#include "general.h"
#include "binary.h"

/* Data Types */
typedef unsigned char* Sprite;

typedef struct ledMatrix {
  Pin data;
  Pin clock;
  Pin load;
} LedMatrix;

/* Public Functions */
void led_init(LedMatrix led);
void led_brightness(LedMatrix led, unsigned char val);
void led_scan_limit(LedMatrix led, unsigned char val);
void led_clear(LedMatrix led);
void led_write_sprite(LedMatrix led, Sprite sprite);


#endif /* matrix_h */
