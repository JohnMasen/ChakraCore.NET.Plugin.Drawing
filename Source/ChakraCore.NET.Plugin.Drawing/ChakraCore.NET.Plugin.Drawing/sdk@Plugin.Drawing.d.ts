import * as api from "api";
export declare enum BlendModeEnum {
    Normal = 0,
    Multiply = 1,
    Add = 2,
    Substract = 3,
    Screen = 4,
    Darken = 5,
    Lighten = 6,
    Overlay = 7,
    HardLight = 8,
    Src = 9,
    Atop = 10,
    Over = 11,
    In = 12,
    Out = 13,
    Dest = 14,
    DestAtop = 15,
    DestOver = 16,
    DestIn = 17,
    DestOut = 18,
    Clear = 19,
    Xor = 20,
}
export declare function GetDrawingSurface(size: api.Size, expetectProfileName: string): DrawingSurface;
export declare function LoadTexutre(name: string): api.ITexture;
export declare function IsProfileSupported(profileName: string): boolean;
export declare class Color {
    readonly value: string;
    constructor(hex: string);
}
export declare class SpritBatch {
    reference: api.ISpritBatch;
    constructor(source: api.ISpritBatch);
    Begin(blend: BlendModeEnum): void;
    End(): void;
    DrawText(position: api.Point, text: string, color: Color, penWidth?: number): void;
    DrawLine(points: Array<api.Point>, color: Color, penWidth?: number): void;
    DrawRectangle(position: api.Point, size: api.Size, color: Color, penWidth?: number, isFill?: boolean): void;
    DrawEclipse(position: api.Point, size: api.Size, color: Color, penWidth?: number, isFill?: boolean): void;
    DrawImage(position: api.Point, size: api.Size, texture: api.ITexture, opacity: number): void;
    Fill(color: Color, region: api.Rectangle): void;
    Translate(value: api.Point): void;
    Scale(value: api.Point): void;
    Rotate(angel: number): void;
    PushMatrix(): number;
    PopMatrix(): number;
    ResetMatrix(): void;
}
export declare class DrawingSurface {
    reference: api.IDrawingSurface;
    constructor(source: api.IDrawingSurface);
    CreateSpritBatch(): SpritBatch;
    GetCurrentProfile(): string;
}
