using MarioClone.Collision;
using MarioClone.Commands;
using MarioClone.Controllers;
using MarioClone.Factories;
using MarioClone.GameObjects;
using MarioClone.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace MarioClone.Level
{
    public class LevelCreator
    {
        private const int KoopaHeight = 112;
        private const int GoombaHeight = 62;
        private const int MarioHeight = 128;


        private const int BlockWidth = 64;
        private const int BlockHeight = 64;

        private Bitmap _image;
        public GameGrid Grid { get; set; }

        public LevelCreator (string file, GameGrid grid)
        {
            using (var stream = new FileStream(file, FileMode.Open))
            {
                _image = new Bitmap(stream);
            }   
            Grid = grid;
        }

        public void Create()
        {
            for (int i = 0; i < _image.Width; i++)
            {
                for (int j = 0; j < _image.Height; j++)
                {
                    MakeObject(_image.GetPixel(i, j), i * BlockWidth, j * BlockHeight);
                }
            }
		}

        private void MakeObject(System.Drawing.Color pixel, int x, int y)
        {
            Func<System.Drawing.Color, System.Drawing.Color, bool> sameColor = (c1, c2) => (c1.R == c2.R && c1.G == c2.G && c1.B == c2.B);
            var position = new Vector2(x, y);

            if (!sameColor(pixel, Colors.Empty))
            {
                if (sameColor(pixel, Colors.MarioSpawn) && Mario.Instance == null)
                {
                    position = new Vector2(position.X, position.Y - (MarioHeight - 64));
                    var mario = MarioFactory.Create(position);

                    Grid.Add(mario);
                }
                else if (sameColor(pixel, Colors.MarioSpawn) && Mario.Instance != null)
                {
                    Mario.Instance.Position = new Vector2(position.X, position.Y - (MarioHeight - 64));
                    Mario.Instance.ActionState = MarioIdle.Instance;
                    Mario.Instance.Velocity = new Vector2(0, 0);
                    Mario.Instance.PowerupState = MarioNormal.Instance;
                    Mario.Instance.SpriteFactory = NormalMarioSpriteFactory.Instance;
                    Mario.Instance.PreviousActionState = MarioIdle.Instance;
                    Mario.Instance.Sprite = NormalMarioSpriteFactory.Instance.Create(MarioAction.Idle);
                    Mario.Instance.Orientation = Facing.Right;

                    var mario = Mario.Instance;        

                    Grid.Add(mario);
                }
                else if (sameColor(pixel, Colors.QuestionBlockRedMushroom))
                {
                    var questionBlock = BlockFactory.Instance.Create(BlockType.QuestionBlock, position);
                    Grid.Add(questionBlock);
                }
                else if (sameColor(pixel, Colors.QuestionBlockGreenMushroom))
                {
                    var questionBlock = BlockFactory.Instance.Create(BlockType.QuestionBlock, position);
                    Grid.Add(questionBlock);
                }
                else if (sameColor(pixel, Colors.QuestionBlockFireFlower))
                {
                    var questionBlock = BlockFactory.Instance.Create(BlockType.QuestionBlock, position);
                    Grid.Add(questionBlock);
                }
                else if (sameColor(pixel, Colors.BrickBlock))
                {
                    var brickBlock = BlockFactory.Instance.Create(BlockType.BreakableBrick, position);
                    Grid.Add(brickBlock);
                }
                else if (sameColor(pixel, Colors.UsedBlock))
                {
                    var usedBlock = BlockFactory.Instance.Create(BlockType.UsedBlock, position);
                    Grid.Add(usedBlock);
                }
                else if (sameColor(pixel, Colors.FloorBlock))
                {
                    var floorBlock = BlockFactory.Instance.Create(BlockType.FloorBlock, position);
                    Grid.Add(floorBlock);
                }
                else if (sameColor(pixel, Colors.StairBlock))
                {
                    var stairBlock = BlockFactory.Instance.Create(BlockType.StairBlock, position);
                    Grid.Add(stairBlock);
                }
                else if (sameColor(pixel, Colors.HiddenBlock))
                {
                    var brickBlock = BlockFactory.Instance.Create(BlockType.HiddenBlock, position);
                    Grid.Add(brickBlock);
                }
                else if (sameColor(pixel, Colors.Goomba))
                {
                    position = new Vector2(position.X, position.Y - (GoombaHeight - 64));
                    var goomba = EnemyFactory.Create(EnemyType.Goomba, position);
                    Grid.Add(goomba);
                }
                else if (sameColor(pixel, Colors.GreenKoopa))
                {
                    position = new Vector2(position.X, position.Y - (KoopaHeight - 64)); 
                    var greenKoopa = EnemyFactory.Create(EnemyType.GreenKoopa, position);
                    Grid.Add(greenKoopa);
                }
                else if (sameColor(pixel, Colors.RedKoopa))
                {
                    position = new Vector2(position.X, position.Y - (KoopaHeight - 64));
                    var redKoopa = EnemyFactory.Create(EnemyType.RedKoopa, position);
                    Grid.Add(redKoopa);
                }
                else if (sameColor(pixel, Colors.RedMushroom))
                {
                    var redMushroom = PowerUpFactory.Create(PowerUpType.RedMushroom, position);
                    Grid.Add(redMushroom);
                }
                else if (sameColor(pixel, Colors.GreenMushroom))
                {
                    var greenMushroom = PowerUpFactory.Create(PowerUpType.GreenMushroom, position);
                    Grid.Add(greenMushroom);
                }
                else if (sameColor(pixel, Colors.FireFlower))
                {
                    var fireFlower = PowerUpFactory.Create(PowerUpType.Flower, position);
                    Grid.Add(fireFlower);
                }
                else if (sameColor(pixel, Colors.Coin))
                {
                    var coin = PowerUpFactory.Create(PowerUpType.Coin, position);
                    Grid.Add(coin);
                }
                /*else if (sameColor(pixel, Colors.Pipe))
                {
                    var coin = PowerUpFactory.Create(PowerUpType.Coin, position);
                    Grid.Add(coin);
                }*/
            }

		}
    }
}
