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
    Begin(): void;
    End(): void;
    DrawText(position: Point, text: string, color: number): void;
    DrawLine(start: Point, end: Point, color: number, penWidth: number): void;
    DrawRectangle(position: Point, size: Size, color: number, isFill: boolean): void;
    DrawEclipse(position: Point, region: Rectangle, color: number, isFill: boolean): void;
    DrawImage(position: Point, size: Size, texture: ITexture): void;
    Fill(color: number, region: Rectangle): void;
    Translate(value: Point): void;
    Scale(value: Point): void;
    Rotate(angel: number): void;
    PushMatrix(): number;
    PopMatrix(): number;
    ResetMatrix(): void;
}
export interface ITexture {
    Size: Size;
}

export interface IDrawingSurface {
    CreateSpritBatch(): ISpritBatch;
    GetCurrentProfile(): string;
}

export
    interface INativeAPI {
    GetDrawingSurface(size: Size, expetectProfileName: string): IDrawingSurface;
    LoadTexutre(name: string): ITexture;
    IsProfileSupported(profileName: string): boolean;
}