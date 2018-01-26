import * as api  from "api";
declare function RequireNative(name: string): api.INativeAPI;
let native = RequireNative("Plugin.Drawing");
export function GetDrawingSurface(size: api.Size, expetectProfileName: string): DrawingSurface {
    return new DrawingSurface( native.GetDrawingSurface(size, expetectProfileName));
}
export function LoadTexutre(name: string): api.ITexture {
    return native.LoadTexutre(name);
}

export function IsProfileSupported(profileName: string): boolean {
    return native.IsProfileSupported(profileName);
}

export class Color {
    public readonly value: number;
        constructor(a: number, r: number, g: number, b: number) {
            this.value = a << 6 + r << 4 + g << 2 + b;
    }
}
export class SpritBatch  {
    reference: api.ISpritBatch;
    constructor(source: api.ISpritBatch) {
        this.reference = source;
    }
    Begin(): void {
        this.reference.Begin();
    }
    End(): void {
        this.reference.End();
    }
    DrawText(position: api.Point, text: string, color: Color): void {
        this.reference.DrawText(position, text, color.value);
    }
    DrawLine(start: api.Point, end: api.Point, color: Color, penWidth: number=1): void {
        this.reference.DrawLine(start, end, color.value, penWidth);
    }
    DrawRectangle(position: api.Point, size: api.Size, color: Color, isFill: boolean=false): void {
        this.reference.DrawRectangle(position, size, color.value, isFill);
    }
    DrawEclipse(position: api.Point, region: api.Rectangle, color: Color, isFill: boolean=false): void {
        this.reference.DrawEclipse(position, region, color.value, isFill);
    }
    DrawImage(position: api.Point, size: api.Size, texture: api.ITexture): void {
        this.reference.DrawImage(position, size, texture);
    }
    Fill(color: Color, region: api.Rectangle): void {
        this.reference.Fill(color.value, region);
    }
    Translate(value: api.Point): void {
        this.reference.Translate(value);
    }
    Scale(value: api.Point): void {
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
    reference: api.IDrawingSurface;
    constructor(source: api.IDrawingSurface) {
        this.reference = source;
    }
    CreateSpritBatch(): SpritBatch {
        return new SpritBatch(this.reference.CreateSpritBatch());
    }
    GetCurrentProfile(): string {
        return this.reference.GetCurrentProfile();
    }
}

