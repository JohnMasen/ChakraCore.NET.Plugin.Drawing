import {IEffect,LoadEffect}  from 'sdk@Plugin.Drawing'

export function LoadImageSharpEffect<T extends IEffect>(c:{new():T}){
    var tmp=new c();
    return LoadEffect(tmp.Name) as T;
}

export class ImageSharpEffectBase<T extends object> implements IEffect {
    Name: string;
    constructor(name:string){
        this.Name=name;
    }
    Config:T
}

export interface BlurEffectConfig{
    sigma:number
}
export class BlurEffect extends ImageSharpEffectBase<BlurEffectConfig>
{
    constructor(){
        super("Blur");
    }
}