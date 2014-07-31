using CupCake.Messages.Blocks;
using CupCake.World;

namespace Edit
{
    public class Clipboard
    {
        private Clipboard(int width, int height, WorldBlock[,,] blocks)
        {
            this.Blocks = blocks;
            this.Width = width;
            this.Height = height;
        }

        public int Height { get; private set; }
        public int Width { get; private set; }

        public WorldBlock[,,] Blocks { get; private set; }

        public static Clipboard FromWorld(WorldService worldService, int x, int y, int width, int height)
        {
            var blocks = new WorldBlock[2, width, height];
            for (int l = 0; l <= 1; l++)
                for (int pX = 0; pX <= width - 1; pX++)
                {
                    for (int pY = 0; pY <= height - 1; pY++)
                    {
                        blocks[l, pX, pY] = worldService[(Layer)l, x + pX, y + pY].Clone();
                    }
                }

            return new Clipboard(width, height, blocks);
        }
    }
}