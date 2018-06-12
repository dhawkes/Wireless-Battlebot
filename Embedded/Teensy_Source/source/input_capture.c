/* Name: input_capture.c
 * Author: Dylan Hawkes
 */

#include "input_capture.h"

void start_timer() {
	// start timer 3 at 2Mhz
	clear(TCCR3B, CS32);
	set(TCCR3B, CS31);
	clear(TCCR3B, CS30);

	// roll over at 0xFFFF
	clear(TCCR3B, WGM33);
	clear(TCCR3B, WGM32);
	clear(TCCR3A, WGM31);
	clear(TCCR3A, WGM30);
}

// input capture interrupt
ISR(TIMER3_CAPT_vect) {
	TCNT3 = 0;							// reset the timer
	frequency = 2000000 / ICR3;			// calculate the frequency based off of the stored time
	freq_buffer[count++] = frequency;	// keep a buffer of recent frequencies
	if (count == BUFF_SIZE) {
		count = 0;
	}
}

void begin_input_capture() {
	count = 0;
	input(PC(7));			// set C7 to input mode
	clear(TCCR3B, ICES3);	// set input capture on falling edge
	set(TIFR3, ICF3);		// reset the input capture flag
	set(TIMSK3, ICIE3);		// enable input capture
	start_timer();			// begin timer3
}

int is_target_frequency(int target, int tolerance) {
	// indicate that the proper frequency has been hit only if all of the frequencies
	// in the buffer are within the allowed tolerance (removes false positives)
	int i;
	for (i = 0; i < BUFF_SIZE; i++) {
		if (freq_buffer[i] > target + tolerance || freq_buffer[i] < target - tolerance) {
			return 0;
		}
	}
	return 1;
}