using MarioClone.Collision;
using MarioClone.Commands;
using MarioClone.Controllers;
using MarioClone.Factories;
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
        private const int KoopaHeight = 56;
        private const int GoombaHeight = 31;
        private const int MarioHeight = 64;


        private const int BlockWidth = 32;
        private const int BlockHeight = 32;

        private Bitmap _image;
        private GameGrid _grid;
        private KeyboardController _controller;

        public LevelCreator (string file, GameGrid grid, KeyboardController controller)
        {
            using (var stream = new FileStream(file, FileMode.Open))
            {
                _image = new Bitmap(stream);
            }   
            _grid = grid;
            _controller = controller;
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
                if (sameColor(pixel, Colors.MarioSpawn))
                {
                    position = new Vector2(position.X, position.Y - (MarioHeight - 32));
                    var mario = MarioFactory.Create(position);

                    _controller.AddInputCommand((int)Keys.U, new BecomeSuperMarioCommand(mario));
                    _controller.AddInputChord((int)Modifier.LeftShift, (int)Keys.U, new BecomeSuperMarioCommand(mario));
                    _controller.AddInputCommand((int)Keys.Y, new BecomeNormalMarioCommand(mario));
                    _controller.AddInputChord((int)Modifier.LeftShift, (int)Keys.Y, new BecomeNormalMarioCommand(mario));
                    _controller.AddInputCommand((int)Keys.I, new BecomeFireMarioCommand(mario));
                    _controller.AddInputChord((int)Modifier.LeftShift, (int)Keys.I, new BecomeFireMarioCommand(mario));
                    _controller.AddInputCommand((int)Keys.O, new BecomeDeadMarioCommand(mario));
                    _controller.AddInputChord((int)Modifier.LeftShift, (int)Keys.O, new BecomeDeadMarioCommand(mario));

                    _controller.AddInputCommand((int)Keys.W, new JumpCommand(mario));
                    _controller.AddInputChord((int)Modifier.LeftShift, (int)Keys.W, new JumpCommand(mario));
                    _controller.AddInputCommand((int)Keys.Up, new JumpCommand(mario));


                    _controller.AddInputCommand((int)Keys.A, new MoveLeftCommand(mario));
                    _controller.AddInputChord((int)Modifier.LeftShift, (int)Keys.A, new MoveLeftCommand(mario));
                    _controller.AddInputCommand((int)Keys.Left, new MoveLeftCommand(mario));


                    _controller.AddInputCommand((int)Keys.S, new CrouchCommand(mario));
                    _controller.AddInputChord((int)Modifier.LeftShift, (int)Keys.S, new CrouchCommand(mario));
                    _controller.AddInputCommand((int)Keys.Down, new CrouchCommand(mario));


                    _controller.AddInputCommand((int)Keys.D, new MoveRightCommand(mario));
                    _controller.AddInputChord((int)Modifier.LeftShift, (int)Keys.D, new MoveRightCommand(mario));
                    _controller.AddInputCommand((int)Keys.Right, new MoveRightCommand(mario));

                    _grid.Add(mario);
                }
                else if (sameColor(pixel, Colors.QuestionBlock))
                {
                    var questionBlock = BlockFactory.Instance.Create(BlockType.QuestionBlock, position);
                    _grid.Add(questionBlock);
                }
                else if (sameColor(pixel, Colors.BrickBlock))
                {
                    var brickBlock = BlockFactory.Instance.Create(BlockType.BreakableBrick, position);
                    _grid.Add(brickBlock);
                }
                else if (sameColor(pixel, Colors.UsedBlock))
                {
                    var usedBlock = BlockFactory.Instance.Create(BlockType.UsedBlock, position);
                    _grid.Add(usedBlock);
                }
                else if (sameColor(pixel, Colors.FloorBlock))
                {
                    var floorBlock = BlockFactory.Instance.Create(BlockType.FloorBlock, position);
                    _grid.Add(floorBlock);
                }
                else if (sameColor(pixel, Colors.StairBlock))
                {
                    var stairBlock = BlockFactory.Instance.Create(BlockType.StairBlock, position);
                    _grid.Add(stairBlock);
                }
                else if (sameColor(pixel, Colors.HiddenBlock))
                {
                    var brickBlock = BlockFactory.Instance.Create(BlockType.HiddenBlock, position);
                    _grid.Add(brickBlock);
                }
                else if (sameColor(pixel, Colors.Goomba))
                {
                    position = new Vector2(position.X, position.Y - (GoombaHeight - 32));
                    var goomba = EnemyFactory.Create(EnemyType.Goomba, position);
                    _grid.Add(goomba);
                }
                else if (sameColor(pixel, Colors.GreenKoopa))
                {
                    position = new Vector2(position.X, position.Y - (KoopaHeight - 32)); 
                    var greenKoopa = EnemyFactory.Create(EnemyType.GreenKoopa, position);
                    _grid.Add(greenKoopa);
                }
                else if (sameColor(pixel, Colors.RedKoopa))
                {
                    position = new Vector2(position.X, position.Y - (KoopaHeight - 32));
                    var redKoopa = EnemyFactory.Create(EnemyType.RedKoopa, position);
                    _grid.Add(redKoopa);
                }
                else if (sameColor(pixel, Colors.RedMushroom))
                {
                    var redMushroom = PowerUpFactory.Create(PowerUpType.RedMushroom, position);
                    _grid.Add(redMushroom);
                }
                else if (sameColor(pixel, Colors.GreenMushroom))
                {
                    var greenMushroom = PowerUpFactory.Create(PowerUpType.GreenMushroom, position);
                    _grid.Add(greenMushroom);
                }
                else if (sameColor(pixel, Colors.FireFlower))
                {
                    var fireFlower = PowerUpFactory.Create(PowerUpType.Flower, position);
                    _grid.Add(fireFlower);
                }
                else if (sameColor(pixel, Colors.Coin))
                {
                    var coin = PowerUpFactory.Create(PowerUpType.Coin, position);
                    _grid.Add(coin);
                }
            }
        }
    }
}
