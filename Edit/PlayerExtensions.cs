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
            Tool data;
            player.Metadata.GetMetadata(SelectedToolMetadataName, out data);
            return data;
        }

        public static void SetSelectedTool(this Player player, Tool data)
        {
            player.Metadata.SetMetadata(SelectedToolMetadataName, data);
        }

        public static Stack<Point> GetWandStack(this Player player)
        {
            Stack<Point> data;
            player.Metadata.GetMetadata(WandStackMetadataName, out data);

            if (data == null)
            {
                data = new Stack<Point>();
                player.SetWandStack(data);
            }
            return data;
        }

        public static void SetWandStack(this Player player, Stack<Point> data)
        {
            player.Metadata.SetMetadata(WandStackMetadataName, data);
        }

        public static Clipboard GetClipboard(this Player player)
        {
            Clipboard data;
            player.Metadata.GetMetadata(ClipboardMetadataName, out data);
            return data;
        }

        public static void SetClipboard(this Player player, Clipboard data)
        {
            player.Metadata.SetMetadata(ClipboardMetadataName, data);
        }
    }
}