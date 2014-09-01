using System;
using System.Collections.Generic;
using CupCake;
using CupCake.Core;
using CupCake.Messages.Blocks;
using CupCake.Messages.Send;
using CupCake.Players;
using CupCake.Upload;
using CupCake.World;
using Edit.Commands;

namespace Edit
{
    public sealed class Edit : CupCakeMuffin<Edit>
    {
        public const Block WandBlock = Block.DecorSpring2011Flower;
        private readonly HashSet<Point> _fillPoints = new HashSet<Point>();

        protected override void Enable()
        {
            this.EnablePart<None>();
            this.EnablePart<Wand>();
            this.EnablePart<Fill>();
            this.EnablePart<Copy>();
            this.EnablePart<Paste>();
            //this.EnablePart<Replace>();

            this.Events.Bind<PlaceWorldEvent>(this.OnPlace);
        }

        private void OnPlace(object sender, PlaceWorldEvent e)
        {
            WorldBlock b = e.WorldBlock;
            Player p = e.Player;

            if (p == null) return;

            if (p.GetSelectedTool() == Tool.Wand && b.Block == WandBlock)
            {
                Stack<Point> stack = p.GetWandStack();
                var point = new Point(b.X, b.Y);
                stack.Push(point);

                IBlockPlaceSendEvent ev = e.OldWorldBlock.ToEvent();
                this.Events.Raise(new UploadRequestEvent(ev));
            }

            if (this._fillPoints.Remove(new Point(e.WorldBlock.X, e.WorldBlock.Y)) ||
                p.GetSelectedTool() == Tool.Fill)
            {
                IBlockPlaceSendEvent oldBlock = e.OldWorldBlock.ToEvent();
                IBlockPlaceSendEvent ev = b.ToEvent();
                this.FillAround(ev, oldBlock);
            }
        }

        private void FillAround(IBlockPlaceSendEvent e, IBlockPlaceSendEvent oldBlock)
        {
            this.CheckAndFillAround(e, oldBlock, ev => { ev.X++; });
            this.CheckAndFillAround(e, oldBlock, ev => { ev.X--; });
            this.CheckAndFillAround(e, oldBlock, ev => { ev.Y++; });

            this.CheckAndFillAround(e, oldBlock, ev => { ev.Y--; });
        }

        private void CheckAndFillAround(IBlockPlaceSendEvent e, IBlockPlaceSendEvent oldBlock,
            Action<IBlockPlaceSendEvent> alterFunc)
        {
            IBlockPlaceSendEvent newE = e.CloneObject();
            alterFunc(newE);
            WorldBlock b = this.WorldService[newE.Layer, newE.X, newE.Y];
            if (b.IsSame(oldBlock))
            {
                this.SendAndFillAround(newE, oldBlock);
            }
        }

        private void SendAndFillAround(IBlockPlaceSendEvent e, IBlockPlaceSendEvent oldBlock)
        {
            var ev = new UploadRequestEvent(e);
            this._fillPoints.Add(new Point(e.X, e.Y));
            this.Events.Raise(ev);
        }
    }
}