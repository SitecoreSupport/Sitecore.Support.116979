namespace Sitecore.Support.Shell.Framework.Commands
{
    using Sitecore.Configuration;
    using Sitecore.Data.Items;
    using Sitecore.Diagnostics;
    using Sitecore.Shell.Framework.Commands;
    using System;

    [Serializable]
    public class SetDisplayName : Sitecore.Shell.Framework.Commands.SetDisplayName
    {
        public override CommandState QueryState(CommandContext context)
        {
            Assert.ArgumentNotNull(context, "context");
            if (context.Items.Length != 1)
            {
                return CommandState.Disabled;
            }
            Item item = context.Items[0];
            if (!base.HasField(item, FieldIDs.DisplayName))
            {
                return CommandState.Hidden;
            }
            if (item.Appearance.ReadOnly)
            {
                return CommandState.Disabled;
            }
            if (!item.Access.CanWrite())
            {
                return CommandState.Disabled;
            }
            if (Command.IsLockedByOther(item))
            {
                return CommandState.Disabled;
            }
            if (!Command.CanWriteField(item, FieldIDs.DisplayName))
            {
                return CommandState.Disabled;
            }
            if (!item.Access.CanWriteLanguage())
            {
                return CommandState.Disabled;
            }
            if (item.Versions.Count == 0)
            {
                return CommandState.Disabled;
            }
            if (!item.Locking.HasLock() && !Context.IsAdministrator)
            {
                return CommandState.Disabled;
            }
            return base.QueryState(context);
        }
    }
}