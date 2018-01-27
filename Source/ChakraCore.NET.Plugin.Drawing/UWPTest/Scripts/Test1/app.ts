import * as sdk from 'sdk@Plugin.Drawing';
import { Point } from './api';
export 
    class App {
    surface: sdk.DrawingSurface;
    spritBatch: sdk.SpritBatch;
    Draw() {
        let sb = this.spritBatch;
        let black = new sdk.Color("#ff000000"); 
        let white = new sdk.Color("#ffffffff");
        let points: Array<Point> = [{ X: 0, Y: 0 }, { X: 100, Y: 100 }];

        sb.DrawLine(points, white);
        sb.DrawRectangle({ X: 0, Y: 0 }, { Width: 100, Height: 100 }, white);
        sb.DrawEclipse({ X: 50, Y: 50 }, { Width: 100, Height: 100 }, white);
        sb.PushMatrix();
        sb.Translate({ X: 200, Y: 200 });
        sb.Rotate(15);
        sb.Scale({ X: 2, Y: 2 });
        sb.DrawLine(points, white);
        sb.DrawRectangle({ X: 0, Y: 0 }, { Width: 100, Height: 100 }, white);
        sb.DrawEclipse({ X: 50, Y: 50 }, { Width: 100, Height: 100 }, white);
        sb.PopMatrix();
    }
    Init() {
        this.surface = sdk.GetDrawingSurface({ Width: 640, Height: 480 }, "");
        this.spritBatch = this.surface.CreateSpritBatch();
        this.spritBatch.Fill(new sdk.Color("#ff000000"), { X: 0, Y: 0, Width: 640, Height: 480 });
    }
}
