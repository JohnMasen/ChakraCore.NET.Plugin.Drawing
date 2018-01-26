"use strict";
class Color {
    constructor(a, r, g, b) {
        this.value = a << 6 + r << 4 + g << 2 + b;
    }
}
