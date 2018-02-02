import {IEffect}  from 'sdk@Plugin.Drawing'
export interface BlurEffectConfig{
    sigma:number
}
export class BlurEffect implements IEffect{
    public readonly Name: string="Blur";
    ConfigJson: string;
    Config:BlurEffectConfig;
}