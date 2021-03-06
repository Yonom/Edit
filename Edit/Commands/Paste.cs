﻿using System.Collections.Generic;
using CupCake;
using CupCake.Command;
using CupCake.Command.Source;
using CupCake.Core;
using CupCake.Messages.Send;
using CupCake.Permissions;
using CupCake.Upload;

namespace Edit.Commands
{
    public sealed class Paste : CupCakeMuffinPart<Edit>
    {
        protected override void Enable()
        {
        }

        [Command("paste")]
        [MinGroup(Group.Moderator)]
        [CorrectUsage("")]
        private void Run(IInvokeSource source, ParsedCommand message)
        {
            var pSource = source as PlayerInvokeSource;
            if (pSource == null)
                throw new CommandException("You can only use this command in game!");

            Clipboard clipboard = pSource.Player.GetClipboard();
            if (clipboard == null)
                throw new CommandException("You must first select copy something!");

            Stack<Point> stack = pSource.Player.GetWandStack();
            if (stack.Count < 1)
                throw new CommandException("You must first select a point!");

            // Get the last point
            Point p1 = stack.Pop();
            stack.Clear();

            for (int pX = 0; pX <= clipboard.Width - 1; pX++)
            {
                for (int pY = 0; pY <= clipboard.Height - 1; pY++)
                {
                    for (int l = 0; l <= 1; l++)
                    {
                        IBlockPlaceSendEvent ev = clipboard.Blocks[l, pX, pY].ToEvent();
                        ev.X = p1.X + pX;
                        ev.Y = p1.Y + pY;
                        this.Events.Raise((new UploadRequestEvent(ev)));
                    }
                }
            }

            source.Reply("Pasted.");
        }
    }
}