using Microsoft.Xna.Framework;
using MonoGame.Extended.Collisions.Layers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace Cuphead.Player
{
    internal class PlayerState
    {
        public GameObject GameObject { get; set; }

        public PlayerState(GameObject playerObject)
        {
            this.GameObject = playerObject;
        }

        //all of the variable that player uses goes here

        //bools
        public bool enabled { get; set; } = true;
        public bool IsDucking = false;
        public bool IsRunning = false;
        public bool IsInvincible = false;
        public bool isDuckingYAdjust = false;
        public bool isShooting = false;
        public bool IsDashing = false;
        public bool IsDead = false;
        public bool IsSpawning = true;

        //floats
        public float Speed { get; set; } = 700f;
        public float JumpForce { get; set; } = -1150f;
        public bool IsGrounded { get; set; } = false;
        public float GroundLevel { get; set; } = 500f;
        public float Gravity { get; set; } = 2000f;
        public float timeTillNextBullet { get; set; } = .2f;
        public float timeTillNextHit { get; set; } = .4f;

        public float dashDuration = 0.5f; //about 1 second

        public float InvincibilityDuration = 1f;

        public float dashSpeed = 1500f; // about 750 pixel

        public float airTime = 0f;

        public float shootTime = 0f;

        public float hitTime = 0f;

        public float dashTime = 0f;

        //ints
        public int TimeTillNextDash { get; set; } = 500;
        public int height;
        public int playerHeight = 130;
        public int playerWidth = 100;
        public int floorY;
        public int DuckingYOffset = 50;

        public Vector2 velocity;
        public int Health { get; set; } = 100;
        public bool[] projectileUnlock = { true, true, false, false, false, false, false };
        public enum projectiletype { Peashooter = 1, Spreadshot = 2, Chaser = 3, Lobber = 4, Roundabout = 5 }
        public ProjectileType currentProjectileType = ProjectileType.Peashooter;

    }
}
