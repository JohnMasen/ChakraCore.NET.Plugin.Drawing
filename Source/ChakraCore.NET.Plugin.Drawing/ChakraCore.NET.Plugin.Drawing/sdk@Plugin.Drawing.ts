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
    public readonly value: string;
        constructor(hex:string) {
            this.value = hex;
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
    DrawText(position: api.Point, text: string, color: Color,penWidth:number=1): void {
        this.reference.DrawText(position, text, color.value, penWidth);
    }
    DrawLine(points:Array<api.Point> , color: Color, penWidth: number=1): void {
        this.reference.DrawLines(points, color.value, penWidth);
    }
    DrawRectangle(position: api.Point, size: api.Size, color: Color, penWidth: number = 1, isFill: boolean=false): void {
        
        if (isFill) {
            this.reference.Fill(color.value, { X:position.X, Y:position.Y, Width:size.Width, Height:size.Height });
        }
        else {
            let points: Array<api.Point> = new Array<api.Point>();
            points.push(position);//top left
            points.push({ X: position.X + size.Width, Y: position.Y });//top right
            points.push({ X: position.X + size.Width, Y: position.Y + size.Height });//bottom right
            points.push({ X: position.X, Y: position.Y + size.Height });//botom left
            points.push(position);//top left
            this.reference.DrawLines(points, color.value, penWidth);
        }
        
    }
    DrawEclipse(position: api.Point, size: api.Size, color: Color, penWidth:number=1, isFill: boolean=false): void {
        this.reference.DrawEclipse(position, size, color.value, penWidth, isFill);
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

