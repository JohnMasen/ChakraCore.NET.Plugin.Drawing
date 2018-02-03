import { LoadEffect } from 'sdk@Plugin.Drawing';
export function LoadImageSharpEffect(c) {
    var tmp = new c();
    return LoadEffect(tmp.Name);
}
export class ImageSharpEffectBase {
    constructor(name) {
        this.Name = name;
    }
}
export class BlurEffect extends ImageSharpEffectBase {
    constructor() {
        super("Blur");
    }
}
