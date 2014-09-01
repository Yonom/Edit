using System;
using System.Collections.Generic;
using CupCake;
using CupCake.Command;
using CupCake.Command.Source;
using CupCake.Core;
using CupCake.Messages.Blocks;
using CupCake.Permissions;

namespace Edit.Commands
{
    public class Replace : CupCakeMuffinPart<Edit>
    {
        protected override void Enable()
        {
        }

        [Command("replace")]
        [MinArgs(2)]
        [MinGroup(Group.Moderator)]
        [CorrectUsage("blockSearch blockReplace")]
        private void Run(IInvokeSource source, ParsedCommand message)
        {
            var block1 = (Block)Enum.Parse(typeof(Block), message.Args[0], true);
            var block2 = (Block)Enum.Parse(typeof(Block), message.Args[1], true);

            // No support for Block.GravityNothing as it can be found both in fg and bg
            if (block1 == 0)
                throw new CommandException("Searching for GravityNothing is not supported! :(");

            var pSource = source as PlayerInvokeSource;
            if (pSource == null)
                throw new CommandException("You can only use this command in game!");

            Stack<Point> stack = pSource.Player.GetWandStack();
            if (stack.Count < 2)
                throw new CommandException("You must first select two points!");

            // Get the last two points
            Point p1 = stack.Pop();
            Point p2 = stack.Pop();
            stack.Clear();

            // Calculate the rectangle selected using the points
            int minX = Math.Min(p1.X, p2.X);
            int maxX = Math.Max(p1.X, p2.X);
            int minY = Math.Min(p1.Y, p2.Y);
            int maxY = Math.Max(p1.Y, p2.Y);

            for (int pX = minX; pX <= maxX; pX++)
            {
                for (int pY = minY; pY <= maxY; pY++)
                {
                    for (int l = 0; l <= 1; l++)
                    {
                        if (this.WorldService[(Layer)l, pX, pY].Block == block1)
                        {
                            this.UploadService.UploadBlock(pX, pY, block2);
                        }
                    }
                }
            }

            source.Reply("Replaced.");
        }
    }
}