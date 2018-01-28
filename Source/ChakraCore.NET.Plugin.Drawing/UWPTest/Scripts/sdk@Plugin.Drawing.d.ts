import * as api from "api";
export declare enum BlendModeEnum {
    Multiply = 0,
    Screen = 1,
    Darken = 2,
    Lighten = 3,
    Dissolve = 4,
    ColorBurn = 5,
    LinearBurn = 6,
    DarkerColor = 7,
    LighterColor = 8,
    ColorDodge = 9,
    LinearDodge = 10,
    Overlay = 11,
    SoftLight = 12,
    HardLight = 13,
    VividLight = 14,
    LinearLight = 15,
    PinLight = 16,
    HardMix = 17,
    Difference = 18,
    Exclusion = 19,
    Hue = 20,
    Saturation = 21,
    Color = 22,
    Luminosity = 23,
    Subtract = 24,
    Division = 25,
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
