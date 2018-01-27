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
    DrawText(position: Point, text: string, color: string, penWidth: number): void;
    DrawLines(points: Array<Point>, color: string, penWidth: number): void;
    DrawEclipse(position: Point, region: Size, color: string, penWidth: number, isFill: boolean): void;
    DrawImage(position: Point, size: Size, texture: ITexture): void;
    Fill(color: string, region: Rectangle): void;
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
    LoadTexutre(resourceName: string): ITexture;
    IsProfileSupported(profileName: string): boolean;
    LoadFont(resourceName: string): boolean;
}