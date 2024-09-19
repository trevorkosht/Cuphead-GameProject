using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Animation {
    private List<Rectangle> frames;
    private Texture2D spriteSheet; 
    private int update = 0;
    private int updatesPerFrame;
    private int frameCount;
    private int frameHeight;
    private int frameWidth;
    private int currentFrame = 0;

    public Animation(Texture2D spriteSheet, int updatesPerFrame, int frameCount, int frameHeight, int frameWidth) {
        this.spriteSheet = spriteSheet;
        this.updatesPerFrame = updatesPerFrame;
        this.frameCount = frameCount;
        this.frameHeight = frameHeight;
        this.frameWidth = frameWidth;

        this.loadFrames();
    }

    public void loadFrames() {
        for (int i = 0; i < frameCount; i++) {
            frames.Add(new Rectangle(i * frameWidth, 0, frameWidth, frameHeight));
        }
    }

    public int UpdatesPerFrame {get {return updatesPerFrame;} set {updatesPerFrame = value;}}
    public int FrameCount {get { return frameCount; } set {frameCount = value;}}
    public int CurrentFrame { get { return currentFrame; } set{currentFrame = value; }}
    public List<Rectangle> Frames { get { return frames; } }

    
    public void updateAnimation() {
        if(update < updatesPerFrame) {
            update++;
        }
        else {
            update = 0;
            updateFrame();
        }
      
    }
    private void updateFrame() {
        if(currentFrame != frameCount - 1) {
            currentFrame++;
        }
        else {
            currentFrame = 0;
        }
    }

    public void draw(SpriteBatch spriteBatch, Rectangle destRectangle) {
        spriteBatch.Begin();
        spriteBatch.Draw(spriteSheet, destRectangle, frames[currentFrame], Color.White);
        spriteBatch.End();
    }

    public void resetAnimation() {
        currentFrame = 0;
    }

}
