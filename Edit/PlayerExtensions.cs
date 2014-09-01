using System.Collections.Generic;
using CupCake.Core;
using CupCake.Players;

namespace Edit
{
    public static class PlayerExtensions
    {
        public const string SelectedToolMetadataName = "EditSelectedTool";
        public const string WandStackMetadataName = "EditWandStack";
        public const string ClipboardMetadataName = "EditClipboard";

        public static Tool GetSelectedTool(this Player player)
        {
            return player.Get<Tool>(SelectedToolMetadataName);
        }

        public static void SetSelectedTool(this Player player, Tool data)
        {
            player.Set(SelectedToolMetadataName, data);
        }

        public static Stack<Point> GetWandStack(this Player player)
        {
            var data = player.Get<Stack<Point>>(WandStackMetadataName);

            if (data == null)
            {
                data = new Stack<Point>();
                player.SetWandStack(data);
            }
            return data;
        }

        public static void SetWandStack(this Player player, Stack<Point> data)
        {
            player.Set(WandStackMetadataName, data);
        }

        public static Clipboard GetClipboard(this Player player)
        {
            return player.Get<Clipboard>(ClipboardMetadataName);
        }

        public static void SetClipboard(this Player player, Clipboard data)
        {
            player.Set(ClipboardMetadataName, data);
        }
    }
}