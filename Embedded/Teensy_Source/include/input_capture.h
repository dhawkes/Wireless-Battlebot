/* Name: input_capture.h
 * Author: Dylan Hawkes
 * Description: File that detects the frequency of a square
 *              wave using Teensy hardware
 */

#ifndef input_capture_h
#define input_capture_h

#define BUFF_SIZE 12

#include "general.h"

volatile int frequency;
int freq_buffer[BUFF_SIZE];
int count;

ISR(TIMER3_CAPT_vect);
void begin_input_capture();
int is_target_frequency(int target, int tolerance);

#endif /* input_capture_h */