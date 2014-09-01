using CupCake;
using CupCake.Command;
using CupCake.Command.Source;
using CupCake.Permissions;

namespace Edit.Commands
{
    public sealed class Fill : CupCakeMuffinPart<Edit>
    {
        protected override void Enable()
        {
        }

        [Command("fill")]
        [MinGroup(Group.Moderator)]
        [CorrectUsage("")]
        private void Run(IInvokeSource source, ParsedCommand message)
        {
            var pSource = source as PlayerInvokeSource;
            if (pSource == null)
                throw new CommandException("You can only use this command in game!");

            pSource.Player.SetSelectedTool(Tool.Fill);

            source.Reply("Selected fill tool.");
        }
    }
}