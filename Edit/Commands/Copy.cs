using System;
using System.Collections.Generic;
using CupCake;
using CupCake.Command;
using CupCake.Command.Source;
using CupCake.Core;
using CupCake.Permissions;

namespace Edit.Commands
{
    public sealed class Copy : Command<Edit>
    {
        [MinGroup(Group.Moderator)]
        [Label("copy")]
        [CorrectUsage("")]
        protected override void Run(IInvokeSource source, ParsedCommand message)
        {
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

            // Get the width and height 
            int x = minX;
            int y = minY;
            int width = maxX - minX + 1;
            int height = maxY - minY + 1;

            Clipboard clipboard = Clipboard.FromWorld(this.WorldService, x, y, width, height);
            pSource.Player.SetClipboard(clipboard);

            source.Reply("Copied.");
        }
    }
}