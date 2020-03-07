const Gpio = require('pigpio').Gpio;

// The number of microseconds it takes sound to travel 1cm at 20 degrees celcius
const MICROSECDONDS_PER_CM = 1e6/34321;

const MIN_VALID_TO_ALERT = 10;

const trigger = new Gpio(23, {mode: Gpio.OUTPUT});
const echo = new Gpio(24, {mode: Gpio.INPUT, alert: true});
let previousStatus = false;

trigger.digitalWrite(0); // Make sure trigger is low

console.log('running');

const watchHCSR04 = () => {
  let startTick;

  echo.on('alert', (level, tick) => {
    if (level == 1) {
      startTick = tick;
    } else {
      const endTick = tick;
      const diff = (endTick >> 0) - (startTick >> 0); // Unsigned 32 bit arithmetic
      const distance = diff / 2 / MICROSECDONDS_PER_CM
      if (distance <= MIN_VALID_TO_ALERT && !previousStatus) {
        previousStatus = true;
        emitEvent(previousStatus);
      } else if (distance > MIN_VALID_TO_ALERT && previousStatus) {
        previousStatus = false;
        emitEvent(previousStatus);
      }
    }
  });
};

function emitEvent(obstruded) {
  console.log('obstruded', obstruded);
}

watchHCSR04();

// Trigger a distance measurement once per second
setInterval(() => {
  trigger.trigger(10, 1); // Set trigger high for 10 microseconds
}, 1000);
