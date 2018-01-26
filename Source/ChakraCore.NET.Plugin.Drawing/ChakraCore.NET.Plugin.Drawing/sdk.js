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
    constructor(a, r, g, b) {
        this.value = a << 6 + r << 4 + g << 2 + b;
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
    DrawText(position, text, color) {
        this.reference.DrawText(position, text, color.value);
    }
    DrawLine(start, end, color, penWidth = 1) {
        this.reference.DrawLine(start, end, color.value, penWidth);
    }
    DrawRectangle(position, size, color, isFill = false) {
        this.reference.DrawRectangle(position, size, color.value, isFill);
    }
    DrawEclipse(position, region, color, isFill = false) {
        this.reference.DrawEclipse(position, region, color.value, isFill);
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
