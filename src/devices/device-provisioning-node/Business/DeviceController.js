const Gpio = require('pigpio').Gpio;

// The number of microseconds it takes sound to travel 1cm at 20 degrees celcius
const MICROSECDONDS_PER_CM = 1e6/34321;

const MIN_VALID_TO_ALERT = 10;

const trigger = new Gpio(23, {mode: Gpio.OUTPUT});
const echo = new Gpio(24, {mode: Gpio.INPUT, alert: true});

const greenLed = new Gpio(15, {mode: Gpio.OUTPUT});
const redLed = new Gpio(18, {mode: Gpio.OUTPUT});

let previousStatus = false; // true = obstruded
changeLedStatus(previousStatus);

trigger.digitalWrite(0); // Make sure trigger is low

console.log('running');

const watchHCSR04 = (callback) => {
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
        callback(previousStatus);
        changeLedStatus(previousStatus);
      } else if (distance > MIN_VALID_TO_ALERT && previousStatus) {
        previousStatus = false;
        callback(previousStatus);
        changeLedStatus(previousStatus);
      }
    }
  });
};

const changeLedStatus = (status) => {
    greenLed.digitalWrite(status ? 0 : 1);
    redLed.digitalWrite(status ? 1 : 0);
}

module.exports = {
    runDeviceController: function(statusChangedCallback) {
        watchHCSR04(statusChangedCallback);

        // Trigger a distance measurement once per second
        setInterval(() => {
          trigger.trigger(10, 1); // Set trigger high for 10 microseconds
        }, 1000);
    }
};
