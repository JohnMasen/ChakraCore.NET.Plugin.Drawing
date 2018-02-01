let native = RequireNative("Plugin.Drawing");
export var BlendModeEnum;
(function (BlendModeEnum) {
    //
    // Summary:
    //     Default blending mode, also known as "Normal" or "Alpha Blending"
    BlendModeEnum[BlendModeEnum["Normal"] = 0] = "Normal";
    //
    // Summary:
    //     Blends the 2 values by multiplication.
    BlendModeEnum[BlendModeEnum["Multiply"] = 1] = "Multiply";
    //
    // Summary:
    //     Blends the 2 values by addition.
    BlendModeEnum[BlendModeEnum["Add"] = 2] = "Add";
    //
    // Summary:
    //     Blends the 2 values by subtraction.
    BlendModeEnum[BlendModeEnum["Substract"] = 3] = "Substract";
    //
    // Summary:
    //     Multiplies the complements of the backdrop and source values, then complements
    //     the result.
    BlendModeEnum[BlendModeEnum["Screen"] = 4] = "Screen";
    //
    // Summary:
    //     Selects the minimum of the backdrop and source values.
    BlendModeEnum[BlendModeEnum["Darken"] = 5] = "Darken";
    //
    // Summary:
    //     Selects the max of the backdrop and source values.
    BlendModeEnum[BlendModeEnum["Lighten"] = 6] = "Lighten";
    //
    // Summary:
    //     Multiplies or screens the values, depending on the backdrop vector values.
    BlendModeEnum[BlendModeEnum["Overlay"] = 7] = "Overlay";
    //
    // Summary:
    //     Multiplies or screens the colors, depending on the source value.
    BlendModeEnum[BlendModeEnum["HardLight"] = 8] = "HardLight";
    //
    // Summary:
    //     returns the source colors
    BlendModeEnum[BlendModeEnum["Src"] = 9] = "Src";
    //
    // Summary:
    //     returns the source over the destination
    BlendModeEnum[BlendModeEnum["Atop"] = 10] = "Atop";
    //
    // Summary:
    //     returns the detination over the source
    BlendModeEnum[BlendModeEnum["Over"] = 11] = "Over";
    //
    // Summary:
    //     the source where the desitnation and source overlap
    BlendModeEnum[BlendModeEnum["In"] = 12] = "In";
    //
    // Summary:
    //     the destination where the desitnation and source overlap
    BlendModeEnum[BlendModeEnum["Out"] = 13] = "Out";
    //
    // Summary:
    //     the destination where the source does not overlap it
    BlendModeEnum[BlendModeEnum["Dest"] = 14] = "Dest";
    //
    // Summary:
    //     the source where they dont overlap othersie dest in overlapping parts
    BlendModeEnum[BlendModeEnum["DestAtop"] = 15] = "DestAtop";
    //
    // Summary:
    //     the destnation over the source
    BlendModeEnum[BlendModeEnum["DestOver"] = 16] = "DestOver";
    //
    // Summary:
    //     the destination where the desitnation and source overlap
    BlendModeEnum[BlendModeEnum["DestIn"] = 17] = "DestIn";
    //
    // Summary:
    //     the source where the desitnation and source overlap
    BlendModeEnum[BlendModeEnum["DestOut"] = 18] = "DestOut";
    //
    // Summary:
    //     the clear.
    BlendModeEnum[BlendModeEnum["Clear"] = 19] = "Clear";
    //
    // Summary:
    //     clear where they overlap
    BlendModeEnum[BlendModeEnum["Xor"] = 20] = "Xor";
})(BlendModeEnum || (BlendModeEnum = {}));
export function GetDrawingSurface(size, expetectProfileName) {
    return new DrawingSurface(native.GetDrawingSurface(size, expetectProfileName));
}
export function LoadTexture(name) {
    return native.LoadTexture(name);
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
    Begin(blend) {
        this.reference.Begin(blend);
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
    DrawRectangle(rect, color, penWidth = 1, isFill = false) {
        this.reference.DrawRectangle(rect, color.value, penWidth, isFill);
    }
    DrawTriangle(a, b, c, color, penWidth = 1, isFill = false) {
        this.reference.DrawTriangle(a, b, c, color.value, penWidth, isFill);
    }
    DrawEclipse(position, size, color, penWidth = 1, isFill = false) {
        this.reference.DrawEclipse(position, size, color.value, penWidth, isFill);
    }
    DrawImage(position, size, texture, opacity) {
        this.reference.DrawImage(position, size, texture, opacity);
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
