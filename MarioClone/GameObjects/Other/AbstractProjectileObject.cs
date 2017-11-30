using MarioClone.Sprites;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioClone.GameObjects.Other
{
	public class AbstractProjectileObject : AbstractGameObject
	{

		public bool Destroyed { get; set; }
		public AbstractGameObject Owner { get; set; }
		public int CoolDown { get; set; }

		public AbstractProjectileObject(ISprite sprite, AbstractGameObject player, Vector2 position) : base(sprite, position, Color.Red)
		{
			Owner = player;
			Destroyed = false;
		}
		public override void FixClipping(Vector2 correction, AbstractGameObject obj1, AbstractGameObject obj2)
		{
			if ((obj1 is AbstractBlock && obj1.Visible) || obj1 is Mario)
			{
				Position = new Vector2(Position.X + correction.X, Position.Y + correction.Y);
				BoundingBox.UpdateHitBox(Position, Sprite);

			}
			else if ((obj2 is AbstractBlock && obj2.Visible) || obj2 is Mario)
			{
				Position = new Vector2(Position.X + correction.X, Position.Y + correction.Y);
				BoundingBox.UpdateHitBox(Position, Sprite);
			}
		}
	}
}
