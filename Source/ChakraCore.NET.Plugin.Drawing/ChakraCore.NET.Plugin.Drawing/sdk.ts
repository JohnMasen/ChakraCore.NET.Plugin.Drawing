interface Point {
    X: number;
    Y: number;
}

interface Size {
    Width: number;
    Height: number;
}


class Color {
    public readonly value: number;
        constructor(a: number, r: number, g: number, b: number) {
            this.value = a << 6 + r << 4 + g << 2 + b;
    }
}


interface ISpritBatch {
    DrawText(position:Point,text:string,color:Color): void;
}

