using CupCake;
using CupCake.Command;
using CupCake.Command.Source;
using CupCake.Permissions;

namespace Edit.Commands
{
    public sealed class Wand : CupCakeMuffinPart<Edit>
    {
        protected override void Enable()
        {
        }

        [Command("wand")]
        [MinGroup(Group.Moderator)]
        [CorrectUsage("")]
        private void WandCommand(IInvokeSource source, ParsedCommand message)
        {
            var pSource = source as PlayerInvokeSource;
            if (pSource == null)
                throw new CommandException("You can only use this command in game!");

            pSource.Player.SetSelectedTool(Tool.Wand);

            source.Reply("Wand tool selected.");
        }
    }
}