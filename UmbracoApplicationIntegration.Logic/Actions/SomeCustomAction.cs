using Microsoft.Extensions.Logging;
using Umbraco.UIBuilder.Configuration;
using Umbraco.UIBuilder.Configuration.Actions;
using Umbraco.UIBuilder.Configuration.Builders;

namespace UmbracoApplicationIntegration.Logic.Actions;

public class SomeCustomAction(ILogger<SomeCustomAction> logger) : IAction
{
    public string Icon => "icon-umbraco";

    public string Alias => "someName";

    public string Name => "SomeName";

    // Set to true if you want confirmation before execution.
    public bool ConfirmAction => true;

    public SettingsConfig? Configure(ConfigBuilderContext context) =>
        null;

    public ActionResult Execute(string collectionAlias, object[] entityIds, object? settings)
    {
        foreach (var entityId in entityIds)
        {
            logger.LogInformation("Entity ID: {EntityId}", entityId);
        }

        return new ActionResult(
            true,
            new ActionNotification("Action successful", "Here are the full details"));
    }

    /// <summary>
    /// Determine visibility based on user or context.
    /// </summary>
    /// <remarks>Is not set to always true.</remarks>
    /// <param name="ctx">The context for determining visibility.</param>
    /// <returns>True if the action is visible, otherwise false.</returns>
    public bool IsVisible(ActionVisibilityContext ctx) =>
        true;
}
