import * as sdk from 'sdk@Plugin.Drawing';
export class App {
    Draw() {
        let sb = this.spritBatch;
        sb.Begin(sdk.BlendModeEnum.Normal);
        this.test1();
        sb.PushMatrix();
        sb.Translate({ X: 100, Y: 0 });
        for (var i = 0; i < 10; i++) {
            sb.Translate({ X: 10, Y: 10 });
            sb.Rotate(5);
            sb.Scale({ X: 1.02, Y: 1.02 });
            this.test1();
            // this.test2();
        }
        sb.PopMatrix();
        // this.test2();
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
        this.f.Size = 30;
        sb.DrawText({ X: 0, Y: 0 }, "hello111", this.f, this.getRandomColor());
    }
    Init() {
        this.surface = sdk.GetDrawingSurface({ Width: 640, Height: 480 }, "0.1");
        this.spritBatch = this.surface.CreateSpritBatch();
        this.spritBatch.Begin(sdk.BlendModeEnum.Normal);
        this.spritBatch.Fill(new sdk.Color("#ff000000"), { X: 0, Y: 0, Width: 640, Height: 480 });
        this.spritBatch.End();
        this.TArrow = sdk.LoadTexture("arrow.jpg");
        this.f = sdk.LoadFont("DigitalDream.ttf");
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
