using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

public class Animation {
    private List<Rectangle> frames;
    private int updatesPerFrame;
    private int frameCount;
    private int frameHeight;
    private int frameWidth;
    private int currentFrame = 0;

    public Animation(Texture2D spriteSheet, int updatesPerFrame, int frameCount, int frameHeight, int frameWidth) {
        this.updatesPerFrame = updatesPerFrame;
        this.frameCount = frameCount;
        this.frameHeight = frameHeight;
        this.frameWidth = frameWidth;

        this.loadFrames(spriteSheet);
    }

    public void loadFrames(Texture2D spriteSheet) {
        for (int i = 0; i < this.frameCount; i++) {
            frames.Add(new Rectangle(i * frameWidth, 0, frameWidth, frameHeight));
        }
    }
    
    //Properties
    public int UpdatesPerFrame {get {return updatesPerFrame;} set {updatesPerFrame = value;}}
    public int FrameCount {get { return frameCount; } set {frameCount = value;}}
    public int CurrentFrame { get { return currentFrame; } set{currentFrame = value; }}

    public void updateFrame() {
        if(currentFrame != frameCount - 1) {
            currentFrame++;
        }
        else {
            currentFrame = 0;
        }
    }

    public void resetAnimation() {
        currentFrame = 0;
    }

}
