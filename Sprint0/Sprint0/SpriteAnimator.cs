﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;


public class SpriteAnimator : IComponent {
    public GameObject GameObject { get; set; }
    public bool enabled { get; set; }
    public Rectangle destRectangle { get; set; }
    public bool isFacingRight { get; set; }

    private KeyValuePair<string, Animation> currentAnimation;
    private Dictionary<string, Animation> spriteAnimations = new Dictionary<string, Animation>();

    public SpriteAnimator(GameObject gameObject, bool enabled, Rectangle destRectangle, bool isFacingRight) {
        GameObject = gameObject;
        this.enabled = enabled;
        this.destRectangle = destRectangle;
        this.isFacingRight = isFacingRight;
    }

    public void addAnimation(string animationName, Animation animation) {
        spriteAnimations.Add(animationName, animation);
    }

    public bool removeAnimation(string animationName) {
        return spriteAnimations.Remove(animationName);
    }

    public void loadAllAnimations() {
        foreach (var animationKVPair in spriteAnimations) {
            if(animationKVPair.Value.Frames.Count == 0) {
                animationKVPair.Value.loadFrames();
            }
        }
    }

    public void changeDirection() {
        isFacingRight = !isFacingRight;
    }

    public void moveAnimation(int deltaX, int deltaY) {
        destRectangle = new Rectangle(destRectangle.Location.X + deltaX, destRectangle.Location.Y + deltaY, destRectangle.Width, destRectangle.Height);
    }

    public void moveAnimation(Rectangle newDestRectangle) {
        destRectangle = newDestRectangle;
    }

    public void setAnimation(string animationName) {
        if (spriteAnimations.ContainsKey(animationName)) {
            spriteAnimations[animationName].resetAnimation();
            currentAnimation = new KeyValuePair<string, Animation>(animationName, spriteAnimations[animationName]);
        }
    }

    public void Update(GameTime gameTime) {  
        spriteAnimations[currentAnimation.Key].updateAnimation();
    }

    public void Draw(SpriteBatch spriteBatch) {
        spriteAnimations[currentAnimation.Key].draw(spriteBatch, destRectangle, isFacingRight);
    }

}