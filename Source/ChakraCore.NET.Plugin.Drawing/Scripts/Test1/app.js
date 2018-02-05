import * as sdk from 'sdk@Plugin.Drawing';
import * as ImageSharpEffects from 'sdk@ImageSharpEffectSDK,ChakraCore.NET.Plugin.Drawing.ImageSharp';
export class App {
    constructor() {
        this.size = { Width: 640, Height: 480 };
    }
    Draw() {
        let sb = this.spritBatch;
        this.blur.Config.sigma = 2;
        // sb.Begin(sdk.BlendModeEnum.Normal,this.blur);
        sb.Begin(sdk.BlendModeEnum.Normal);
        this.test1();
        this.test3();
        sb.PushMatrix();
        sb.Translate({ X: 100, Y: 0 });
        for (var i = 0; i < 10; i++) {
            sb.Translate({ X: 10, Y: 10 });
            //sb.Rotate(5);
            //sb.Scale({ X: 1.02, Y: 1.02 });
            // this.test1();
            this.test2();
        }
        sb.PopMatrix();
        this.test2();
        sb.End();
    }
    test2() {
        let sb = this.spritBatch;
        sb.DrawImage({ X: 100, Y: 100 }, this.TArrow.GetSize(), this.TArrow, 0.2);
    }
    test1() {
        let sb = this.spritBatch;
        let black = new sdk.Color("#ff000000");
        let white = new sdk.Color("#ffffffff");
        let points = [{ X: 0, Y: 0 }, { X: 100, Y: 100 }];
        sb.DrawLine(points, this.getRandomColor());
        sb.DrawRectangle({ X: 0, Y: 0, Width: 100, Height: 100 }, this.getRandomColor());
        sb.DrawTriangle({ X: 50, Y: 0 }, { X: 100, Y: 100 }, { X: 0, Y: 100 }, this.getRandomColor());
        sb.DrawEclipse({ X: 50, Y: 50 }, { Width: 100, Height: 100 }, this.getRandomColor());
        this.f.Size = 10;
        sb.DrawText({ X: 0, Y: 0 }, "hello ", this.f, this.getRandomColor());
    }
    test3() {
        let sb = this.spritBatch;
        this.f.Size = 50;
        let text = "hello";
        let r = sdk.MeasureTextBound(text, this.f);
        let pos = {
            X: (this.size.Width - r.Width) / 2,
            Y: (this.size.Height - r.Height) / 2
        };
        sb.DrawText(pos, text, this.f, this.getRandomColor());
    }
    Init() {
        this.surface = sdk.GetDrawingSurface(this.size, "0.1");
        this.spritBatch = this.surface.CreateSpritBatch();
        this.spritBatch.Begin(sdk.BlendModeEnum.Normal);
        this.spritBatch.Fill(new sdk.Color("#ff000000"), { X: 0, Y: 0, Width: 640, Height: 480 });
        this.spritBatch.End();
        this.TArrow = sdk.LoadTexture("arrow.jpg");
        this.f = sdk.LoadFont("DigitalDream.ttf");
        this.blur = ImageSharpEffects.LoadImageSharpEffect(ImageSharpEffects.BlurEffect);
    }
    getRandomColor() {
        var letters = '0123456789ABCDEF';
        var color = '#';
        for (var i = 0; i < 6; i++) {
            color += letters[Math.floor(Math.random() * 16)];
        }
        return new sdk.Color(color);
    }
}
