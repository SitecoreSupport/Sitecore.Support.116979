using Sitecore.Configuration;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Shell.Framework.Commands;

namespace Sitecore.Support.Shell.Framework.Commands
{
    public class SetDisplayName : Sitecore.Shell.Framework.Commands.SetDisplayName
    {
        public override CommandState QueryState(CommandContext context)
        {
            Assert.ArgumentNotNull((object)context, "context");
            if (context.Items.Length != 1)
                return CommandState.Disabled;
            Item obj = context.Items[0];
            if (!this.HasField(obj, FieldIDs.DisplayName))
                return CommandState.Hidden;
            return obj.Appearance.ReadOnly || !obj.Access.CanWrite() || (Command.IsLockedByOther(obj) || !Command.CanWriteField(obj, FieldIDs.DisplayName)) || (!obj.Access.CanWriteLanguage() || obj.Versions.Count == 0) || Settings.RequireLockBeforeEditing && !obj.Locking.HasLock() && !Context.IsAdministrator ? CommandState.Disabled : CommandState.Enabled;
        }
    }
}