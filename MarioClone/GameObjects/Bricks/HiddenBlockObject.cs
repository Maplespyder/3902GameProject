﻿using MarioClone.Sprites;
using MarioClone.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarioClone.Collision;
using static MarioClone.Collision.GameGrid;

namespace MarioClone.GameObjects
{
    public class HiddenBrickObject : AbstractBlock
    {

		public HiddenBrickObject(ISprite sprite, Vector2 position) : base(sprite, position)
        {
            Visible = false;
        }
        
        public override void Bump()
        {
            Visible = true;
        }

        public override void CollisionResponse(AbstractGameObject gameObject, Side side, GameTime gameTime)
        {
            if (gameObject is Mario && side == Side.Bottom)
            {
                Bump();
            }
        }
    }
}
