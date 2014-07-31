using CupCake;
using CupCake.Command;
using CupCake.Command.Source;
using CupCake.Permissions;

namespace Edit.Commands
{
    public sealed class Wand : Command<Edit>
    {
        [MinGroup(Group.Moderator)]
        [Label("wand")]
        [CorrectUsage("")]
        protected override void Run(IInvokeSource source, ParsedCommand message)
        {
            var pSource = source as PlayerInvokeSource;
            if (pSource == null)
                throw new CommandException("You can only use this command in game!");

            pSource.Player.SetSelectedTool(Tool.Wand);

            source.Reply("Wand tool selected.");
        }
    }
}