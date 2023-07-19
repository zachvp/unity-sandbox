using Unity.VisualScripting;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

// This file declare custom events for the game. When you write your own event, don't forget to Regenerate Units through
// the "Regenerate Unit" button in the Visual Script section of the Project Settings

// This class is unused and serves as a reference only.
public sealed class REFInputButtonEvent : EventUnit<InputButtonArgs>
{
    public static string Hook = EnumHelper.GetStringID(CustomHook.INPUT_BUTTON);

    protected override bool register => true;
    public override EventHook GetHook(GraphReference r) => Hook;

    public ValueOutput phase { get; private set; }
    public ValueOutput action { get; private set; }

    protected override void Definition()
    {
        base.Definition();
        phase = ValueOutput<InputActionPhase>(nameof(phase));
        action = ValueOutput<CoreActionMap.Player>(nameof(action));
    }

    protected override void AssignArguments(Flow flow, InputButtonArgs args)
    {
        flow.SetValue(phase, args.phase);
        flow.SetValue(action, args.action);
    }
}
/* End input event units */

/**  REFERENCE: Machine events, independent of gameobject */
[UnitCategory("Events/Core")]
public sealed class OnCustomInputTrigger : EventUnit<EmptyEventArgs>
{
    public static string Hook = nameof(OnCustomInputTrigger);

    protected override bool register => true;
    public override EventHook GetHook(GraphReference r) => Hook;
}

[UnitCategory("Events/Core")]
public sealed class OnCustomInputTriggerArgs : EventUnit<bool>
{
    public static string Hook = nameof(OnCustomInputTriggerArgs);

    protected override bool register => true;
    public override EventHook GetHook(GraphReference r) => Hook;

    [DoNotSerialize]// No need to serialize ports.
    public ValueOutput result { get; private set; }// The event output data to return when the event is triggered.

    protected override void Definition()
    {
        base.Definition();
        // Setting the value on our port.
        result = ValueOutput<bool>(nameof(result));
    }

    // Setting the value on our port.
    protected override void AssignArguments(Flow flow, bool data)
    {
        flow.SetValue(result, data);
    }
}
