/* Name: general.c
 * Author: Dylan Hawkes
 */

#include "general.h"

void output(Pin p) {
	set(*p.port.DDR, p.num);
}

void input(Pin p) {
	clear(*p.port.DDR, p.num);
}

unsigned char read(Pin p) {
	return get(*p.port.PIN, p.num);
}

void write(Pin p, unsigned char val) {
	if(val) {
		set(*p.port.PORT, p.num);
	}
	else {
		clear(*p.port.PORT, p.num);
	}
}

void enable_pullup(Pin p) {
	write(p, ON);
}

void disable_pullup(Pin p) {
	write(p, OFF);
}


void pwm(Timer t, int freq, int duty, OCR ocr) {
	// set prescalar
	float clock_freq;
	if(freq >= 245 && freq <= 16000000) {
		clock_freq = 16000000;
		set(*t.controlB, 0);
		clear(*t.controlB, 1);
	} else if(freq >= 31) {
		clock_freq = 2000000;
		clear(*t.controlB, 0);
		set(*t.controlB, 1);
	} else if(freq >= 4) {
		clock_freq = 250000;
		set(*t.controlB, 0);
		set(*t.controlB, 1);
	}
	else {
		return;
	}

	// set the output compare pin to output mode
	output(ocr.pin);

	// turn on PWM mode 14
	set(*t.controlB, 4);
	set(*t.controlB, 3);
	set(*t.controlA, 1);

	duty = (duty > 100 ? 100 : duty) < 0 ? 0 : duty;	// clamp the duty cycle to acceptable values
	*t.icr = clock_freq / freq;							// set the clock reset value
	*ocr.ocr = (duty * (clock_freq / freq)) / 100;		// set the pin toggle point

	// set the pin to clear on comparison, and set at rollover
	set(*t.controlA, ocr.reg1);
	clear(*t.controlA, ocr.reg0);
}

void drive_motor(Motor m, int speed) {
	// set the control pins to output mode
	output(m.pin1);
	output(m.pin2);

	// set the output pins to drive based on the direction
	if(speed > 5) {			// forward
		write(m.pin1, ON);
		write(m.pin2, OFF);
	}
	else if(speed < -5) {	// backward
		write(m.pin1, OFF);
		write(m.pin2, ON);
	}
	else {					// break
		write(m.pin1, OFF);
		write(m.pin2, OFF);
	}

	// set the speed using a pwm function
	pwm(m.timer, 490, abs(speed), m.ocr);
}

void m_usb_new_line() {
	// print newline characters to the usb connection
	m_usb_tx_char('\n');
	m_usb_tx_char('\r');
}
