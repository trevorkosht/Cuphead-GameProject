using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class PlayerJumpAnimation : IAnimation {
    private Texture2D texture;
    private static int updatesPerFrame = 5;
    private static int frameCount = 8;
    public SpriteBatch _spriteBatch;
    public List<Texture2D> animationFrames;
    private int updateCount = 0;
    private int currentFrame = 0;
    private int xPos;
    private int yPos;

    public void load(ContentManager content) {
        //this.texture = content.Load<Texture2D>("ch-jump");

        //Adding each frame of jump animation
        animationFrames.Add(content.Load<Texture2D>("PlayerJumpTextures/cuphead_jump_0001"));
        animationFrames.Add(content.Load<Texture2D>("PlayerJumpTextures/cuphead_jump_0002"));
        animationFrames.Add(content.Load<Texture2D>("PlayerJumpTextures/cuphead_jump_0003"));
        animationFrames.Add(content.Load<Texture2D>("PlayerJumpTextures/cuphead_jump_0004"));
        animationFrames.Add(content.Load<Texture2D>("PlayerJumpTextures/cuphead_jump_0005"));
        animationFrames.Add(content.Load<Texture2D>("PlayerJumpTextures/cuphead_jump_0006"));
        animationFrames.Add(content.Load<Texture2D>("PlayerJumpTextures/cuphead_jump_0007"));
        animationFrames.Add(content.Load<Texture2D>("PlayerJumpTextures/cuphead_jump_0008"));
    }

    public void update(GameTime gameTime, int x, int y) {
        this.xPos = x;
        this.yPos = y;

        //Handles animation speed by only updating displayed frame every updatesPerFrame game updates
        if (updateCount >= updatesPerFrame) {
            updateCount = 0;

            if(currentFrame + 1 < frameCount) {
                currentFrame++;
            }
            else {
                currentFrame = 0;
            }
        }
        else {
            updateCount++;
        }
    }
    public void draw(SpriteBatch spriteBatch) {

    }
}
