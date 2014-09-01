using CupCake;
using CupCake.Command;
using CupCake.Command.Source;
using CupCake.Permissions;

namespace Edit.Commands
{
    public sealed class None : CupCakeMuffinPart<Edit>
    {
        protected override void Enable()
        {
        }

        [Command("none")]
        [MinGroup(Group.Moderator)]
        [CorrectUsage("")]
        private void Run(IInvokeSource source, ParsedCommand message)
        {
            var pSource = source as PlayerInvokeSource;
            if (pSource == null)
                throw new CommandException("You can only use this command in game!");

            pSource.Player.SetSelectedTool(Tool.None);

            source.Reply("No tool is selected.");
        }
    }
}