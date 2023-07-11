using Unity.VisualScripting;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

// This file declare custom events for the game. When you write your own event, don't forget to Regenerate Units through
// the "Regenerate Unit" button in the Visual Script section of the Project Settings

public sealed class InputButtonEvent : EventUnit<InputButtonArgs>
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

public sealed class InputAxis1DEvent : EventUnit<InputAxis1DArgs>
{
    public static string Hook = EnumHelper.GetStringID(CustomHook.INPUT_BUTTON);

    protected override bool register => true;
    public override EventHook GetHook(GraphReference r) => Hook;

    // todo: expose arg properties
}

public sealed class InputAxis2DEvent : EventUnit<InputAxis2DArgs>
{
    public static string Hook = EnumHelper.GetStringID(CustomHook.INPUT_AXIS2D);

    protected override bool register => true;
    public override EventHook GetHook(GraphReference r) => Hook;

    // todo: expose arg properties
}
/* End input event units */

/** COMMAND EVENTS */
public sealed class CommandEvent : EventUnit<PCInputArgs>
{
    public static string Hook = EnumHelper.GetStringID(CustomHook.COMMAND);

    protected override bool register => true;
    public override EventHook GetHook(GraphReference r) => Hook;

    public ValueOutput command { get; private set; }

    protected override void Definition()
    {
        base.Definition();
        command = ValueOutput<InputActionPhase>(nameof(command));
    }

    protected override void AssignArguments(Flow flow, PCInputArgs args)
    {
        flow.SetValue(command, args);
    }
}

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
