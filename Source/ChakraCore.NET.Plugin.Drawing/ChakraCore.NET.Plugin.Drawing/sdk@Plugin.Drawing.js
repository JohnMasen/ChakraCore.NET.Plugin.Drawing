let native = RequireNative("Plugin.Drawing");
export function GetDrawingSurface(size, expetectProfileName) {
    return new DrawingSurface(native.GetDrawingSurface(size, expetectProfileName));
}
export function LoadTexutre(name) {
    return native.LoadTexutre(name);
}
export function IsProfileSupported(profileName) {
    return native.IsProfileSupported(profileName);
}
export class Color {
    constructor(hex) {
        this.value = hex;
    }
}
export class SpritBatch {
    constructor(source) {
        this.reference = source;
    }
    Begin() {
        this.reference.Begin();
    }
    End() {
        this.reference.End();
    }
    DrawText(position, text, color, penWidth = 1) {
        this.reference.DrawText(position, text, color.value, penWidth);
    }
    DrawLine(points, color, penWidth = 1) {
        this.reference.DrawLines(points, color.value, penWidth);
    }
    DrawRectangle(position, size, color, penWidth = 1, isFill = false) {
        if (isFill) {
            this.reference.Fill(color.value, { X: position.X, Y: position.Y, Width: size.Width, Height: size.Height });
        }
        else {
            let points = new Array();
            points.push(position); //top left
            points.push({ X: position.X + size.Width, Y: position.Y }); //top right
            points.push({ X: position.X + size.Width, Y: position.Y + size.Height }); //bottom right
            points.push({ X: position.X, Y: position.Y + size.Height }); //botom left
            points.push(position); //top left
            this.reference.DrawLines(points, color.value, penWidth);
        }
    }
    DrawEclipse(position, size, color, penWidth = 1, isFill = false) {
        this.reference.DrawEclipse(position, size, color.value, penWidth, isFill);
    }
    DrawImage(position, size, texture) {
        this.reference.DrawImage(position, size, texture);
    }
    Fill(color, region) {
        this.reference.Fill(color.value, region);
    }
    Translate(value) {
        this.reference.Translate(value);
    }
    Scale(value) {
        this.reference.Scale(value);
    }
    Rotate(angel) {
        this.reference.Rotate(angel);
    }
    PushMatrix() {
        return this.reference.PushMatrix();
    }
    PopMatrix() {
        return this.reference.PopMatrix();
    }
    ResetMatrix() {
        this.reference.ResetMatrix();
    }
}
export class DrawingSurface {
    constructor(source) {
        this.reference = source;
    }
    CreateSpritBatch() {
        return new SpritBatch(this.reference.CreateSpritBatch());
    }
    GetCurrentProfile() {
        return this.reference.GetCurrentProfile();
    }
}
