import * as api from "api";
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
    Begin(): void;
    End(): void;
    DrawText(position: api.Point, text: string, color: Color, penWidth?: number): void;
    DrawLine(points: Array<api.Point>, color: Color, penWidth?: number): void;
    DrawRectangle(position: api.Point, size: api.Size, color: Color, penWidth?: number, isFill?: boolean): void;
    DrawEclipse(position: api.Point, size: api.Size, color: Color, penWidth?: number, isFill?: boolean): void;
    DrawImage(position: api.Point, size: api.Size, texture: api.ITexture): void;
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