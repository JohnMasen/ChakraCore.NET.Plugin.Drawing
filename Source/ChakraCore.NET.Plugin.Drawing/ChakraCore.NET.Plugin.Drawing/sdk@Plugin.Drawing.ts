declare function RequireNative(name: string): INativeAPI;
let native = RequireNative("Plugin.Drawing");
interface INativeAPI {
    GetDrawingSurface(size: Size, expetectProfileName: string): IDrawingSurface;
    LoadTexture(resourceName: string): ITexture;
    IsProfileSupported(profileName: string): boolean;
    LoadFont(resourceName: string): Font;
    MeasureTextBound(text: string, font: Font): Rectangle;
    LoadEffect(name: string): INativeEffect;
}

export interface Point {
    X: number;
    Y: number;
}

export interface Size {
    Width: number;
    Height: number;
}

export interface Rectangle extends Point, Size {

}
export interface ISpritBatch {
    Begin(blend: number,effect:INativeEffect): void;
    End(): void;
    DrawText(position: Point, text: string, font: Font, color: string, penWidth: number): void;
    DrawLines(points: Array<Point>, color: string, penWidth: number): void;
    DrawEclipse(position: Point, region: Size, color: string, penWidth: number, isFill: boolean): void;
    DrawImage(position: Point, size: Size, texture: ITexture, opacity: number): void;
    DrawRectangle(rect: Rectangle, color: string, penWidth: number, isFill: boolean): void;
    DrawTriangle(a: Point, b: Point, c: Point, color: string, penWidth: number, isFill: boolean): void;
    Fill(color: string, region: Rectangle): void;
    Translate(value: Point): void;
    Scale(value: Point): void;
    Rotate(angel: number): void;
    PushMatrix(): number;
    PopMatrix(): number;
    ResetMatrix(): void;
    
}
export interface ITexture {
    GetSize(): Size;
}

export interface IDrawingSurface {
    CreateSpritBatch(): ISpritBatch;
    GetCurrentProfile(): string;
    SaveToTexture(): ITexture;
}
export interface Font {
    Name: string;
    Size: number;
}

export enum BlendModeEnum {
    //
    // Summary:
    //     Default blending mode, also known as "Normal" or "Alpha Blending"
    Normal = 0,
    //
    // Summary:
    //     Blends the 2 values by multiplication.
    Multiply = 1,
    //
    // Summary:
    //     Blends the 2 values by addition.
    Add = 2,
    //
    // Summary:
    //     Blends the 2 values by subtraction.
    Substract = 3,
    //
    // Summary:
    //     Multiplies the complements of the backdrop and source values, then complements
    //     the result.
    Screen = 4,
    //
    // Summary:
    //     Selects the minimum of the backdrop and source values.
    Darken = 5,
    //
    // Summary:
    //     Selects the max of the backdrop and source values.
    Lighten = 6,
    //
    // Summary:
    //     Multiplies or screens the values, depending on the backdrop vector values.
    Overlay = 7,
    //
    // Summary:
    //     Multiplies or screens the colors, depending on the source value.
    HardLight = 8,
    //
    // Summary:
    //     returns the source colors
    Src = 9,
    //
    // Summary:
    //     returns the source over the destination
    Atop = 10,
    //
    // Summary:
    //     returns the detination over the source
    Over = 11,
    //
    // Summary:
    //     the source where the desitnation and source overlap
    In = 12,
    //
    // Summary:
    //     the destination where the desitnation and source overlap
    Out = 13,
    //
    // Summary:
    //     the destination where the source does not overlap it
    Dest = 14,
    //
    // Summary:
    //     the source where they dont overlap othersie dest in overlapping parts
    DestAtop = 15,
    //
    // Summary:
    //     the destnation over the source
    DestOver = 16,
    //
    // Summary:
    //     the destination where the desitnation and source overlap
    DestIn = 17,
    //
    // Summary:
    //     the source where the desitnation and source overlap
    DestOut = 18,
    //
    // Summary:
    //     the clear.
    Clear = 19,
    //
    // Summary:
    //     clear where they overlap
    Xor = 20
}

export function GetDrawingSurface(size: Size, expetectProfileName: string): DrawingSurface {
    return new DrawingSurface(native.GetDrawingSurface(size, expetectProfileName));
}
export function LoadTexture(name: string): ITexture {
    return native.LoadTexture(name);
}

export function IsProfileSupported(profileName: string): boolean {
    return native.IsProfileSupported(profileName);
}
export function LoadFont(filename: string): Font {
    return native.LoadFont(filename);
}
export function MeasureTextBound(text: string, font: Font): Rectangle {
    return native.MeasureTextBound(text, font);
}
export function LoadEffect(name: string): IEffect {
    let tmp = native.LoadEffect(name);
    let result: IEffect = { Name: tmp.Name, Config: {} };
    if (tmp.ConfigJson != "") {
        result.Config = JSON.parse(tmp.ConfigJson);
    }
    return result;
}

export class Color {
    public readonly value: string;
    constructor(hex: string) {
        this.value = hex;
    }
}
export interface IEffect {
    Name: string,
    Config:object
}
export interface INativeEffect {
    Name: string,
    ConfigJson: string
}
export class SpritBatch {
    private reference: ISpritBatch;
    constructor(source: ISpritBatch) {
        this.reference = source;
    }
    Begin(blend: BlendModeEnum, effect?: IEffect): void {
        if (effect == undefined) {
            this.reference.Begin(blend, { Name: "", ConfigJson: "" }  );
        }
        else {
            this.reference.Begin(blend, {
                Name: effect.Name,
                ConfigJson: JSON.stringify(effect.Config)
            });
        }
        
    }
    End(): void {
        this.reference.End();
    }
    DrawText(position: Point, text: string, font: Font, color: Color, penWidth: number = 1): void {
        this.reference.DrawText(position, text, font, color.value, penWidth);
    }
    DrawLine(points: Array<Point>, color: Color, penWidth: number = 1): void {
        this.reference.DrawLines(points, color.value, penWidth);
    }
    DrawRectangle(rect: Rectangle, color: Color, penWidth: number = 1, isFill: boolean = false): void {
        this.reference.DrawRectangle(rect, color.value, penWidth, isFill);
    }
    DrawTriangle(a: Point, b: Point, c: Point, color: Color, penWidth: number = 1, isFill: boolean = false): void {
        this.reference.DrawTriangle(a, b, c, color.value, penWidth, isFill);
    }
    DrawEclipse(position: Point, size: Size, color: Color, penWidth: number = 1, isFill: boolean = false): void {
        this.reference.DrawEclipse(position, size, color.value, penWidth, isFill);
    }
    DrawImage(position: Point, size: Size, texture: ITexture, opacity: number): void {
        this.reference.DrawImage(position, size, texture, opacity);
    }
    Fill(color: Color, region: Rectangle): void {
        this.reference.Fill(color.value, region);
    }
    Translate(value: Point): void {
        this.reference.Translate(value);
    }
    Scale(value: Point): void {
        this.reference.Scale(value);
    }
    Rotate(angel: number): void {
        this.reference.Rotate(angel);
    }
    PushMatrix(): number {
        return this.reference.PushMatrix();
    }
    PopMatrix(): number {
        return this.reference.PopMatrix();
    }
    ResetMatrix(): void {
        this.reference.ResetMatrix();
    }
    

}

export class DrawingSurface {
    private reference: IDrawingSurface;
    constructor(source: IDrawingSurface) {
        this.reference = source;
    }
    CreateSpritBatch(): SpritBatch {
        return new SpritBatch(this.reference.CreateSpritBatch());
    }
    GetCurrentProfile(): string {
        return this.reference.GetCurrentProfile();
    }
}

