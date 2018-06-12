/* Name: general.h
 * Author: Dylan Hawkes
 * Description: File that contains basic functions and definitions
 *              that makes the Teensy much easier to use
 */

#ifndef general__
#define general__

#include "teensy_general.h"
#include <stdlib.h>

/*** Definitions ***/
typedef struct port {
	unsigned int * DDR;
	unsigned int * PORT;
	unsigned int * PIN;
} Port;

#define PORTB_S ((Port) {&DDRB, &PORTB, &PINB})
#define PORTD_S ((Port) {&DDRD, &PORTD, &PIND})
#define PORTC_S ((Port) {&DDRC, &PORTC, &PINC})
#define PORTF_S ((Port) {&DDRF, &PORTF, &PINF})


typedef struct pin {
	Port port;
	char num;
} Pin;

#define PB(num) ((Pin) {PORTB_S, (num)})
#define PD(num) ((Pin) {PORTD_S, (num)})
#define PC(num) ((Pin) {PORTC_S, (num)})
#define PF(num) ((Pin) {PORTF_S, (num)})

typedef struct ocr {
	unsigned int * ocr;
	int reg1;
	int reg0;
	Pin pin;
} OCR;

#define OCR1A_DEF ((OCR) {&OCR1A, COM1A1, COM1A0, PB(5)})
#define OCR1B_DEF ((OCR) {&OCR1B, COM1B1, COM1B0, PB(6)})
#define OCR1C_DEF ((OCR) {&OCR1C, COM1C1, COM1C0, PB(7)})

typedef struct timer {
	unsigned int * tcnt;
	unsigned int * icr;
	unsigned int * controlA;
	unsigned int * controlB;
} Timer;

#define TIMER3 ((Timer) {&TCNT3, &ICR3, &TCCR3A, &TCCR3B})
#define TIMER1 ((Timer) {&TCNT1, &ICR1, &TCCR1A, &TCCR1B})


typedef struct motor {
	Pin pin1;
	Pin pin2;
	Timer timer;
	OCR ocr;
} Motor;

/*** Functions ***/
void output(Pin p);						// set pin as output
void input(Pin p);						// set pin as input
unsigned char read(Pin p);				// read pin value
void write(Pin p, unsigned char val);	// write pin value
void enable_pullup(Pin p);				// enable the internal pull up resistor
void disable_pullup(Pin p);				// disable the internal pull up resistor

void pwm(Timer t, int freq, int duty, OCR ocr);		// run a pwm function using a specified timer
void drive_motor(Motor m, int speed);				// run the specified motor with the given direction and speed

void m_usb_new_line();	// print a new line

#endif /* general__ */
