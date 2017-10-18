using System.Drawing;

namespace MarioClone.Level
{
    public static class Colors
    {
        public static readonly Color Empty = Color.FromArgb(255, 255, 255);

        public static readonly Color MarioSpawn = Color.FromArgb(136, 0, 27);

        // For the amount of coins in block, we should use one of the RGB values as a way to track how many coins.
        public static readonly Color QuestionBlock = Color.FromArgb(140, 255, 251);
        public static readonly Color UsedBlock = Color.FromArgb(185, 122, 86);
        public static readonly Color BrickBlock = Color.FromArgb(103, 62, 39);
        public static readonly Color FloorBlock = Color.FromArgb(0, 0, 0);
        public static readonly Color StairBlock = Color.FromArgb(255, 127, 39);
        public static readonly Color HiddenBlock = Color.FromArgb(189, 69, 0);

        public static readonly Color Goomba = Color.FromArgb(81, 47, 28);
        public static readonly Color GreenKoopa = Color.FromArgb(14, 209, 69);
        public static readonly Color RedKoopa = Color.FromArgb(236, 28, 36);

        public static readonly Color RedMushroom = Color.FromArgb(255, 127, 127);
        public static readonly Color GreenMushroom = Color.FromArgb(196, 255, 14);
        public static readonly Color FireFlower = Color.FromArgb(255, 86, 30);
        //public static readonly Color Starman = Color.FromArgb(255, 255, 255);

        public static readonly Color Coin = Color.FromArgb(255, 242, 0);
    }
}
