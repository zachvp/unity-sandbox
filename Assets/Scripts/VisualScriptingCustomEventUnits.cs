using Unity.VisualScripting;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

// This file declare custom events for the game. When you write your own event, don't forget to Regenerate Units through
// the "Regenerate Unit" button in the Visual Script section of the Project Settings

// todo: refactor event hook names to use short enum string defs

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
        action = ValueOutput<CustomInputAction>(nameof(action));
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
}

[UnitCategory("Events/Core")]
[UnitTitle(nameof(JumpInputEventUnit))]
// todo: convert to generic 'inputbuttonevent' with input event args
// todo: remove other button type units
public sealed class JumpInputEventUnit : EventUnit<bool>
{
    public static string Hook = EnumHelper.GetStringID(CustomHook.JumpInput);

    protected override bool register => true;
    public override EventHook GetHook(GraphReference r) => Hook;

    [DoNotSerialize]
    public ValueOutput value { get; private set; }

    protected override void Definition()
    {
        base.Definition();
        value = ValueOutput<bool>(nameof(value));
    }

    protected override void AssignArguments(Flow flow, bool args)
    {
        flow.SetValue(value, args);
    }
}

[UnitCategory("Events/Core")]
[UnitTitle(nameof(MoveInputEventUnit))]
// todo: rename to 'inputAxis1DEvent'
public sealed class MoveInputEventUnit : EventUnit<short>
{
    public static string Hook = EnumHelper.GetStringID(CustomHook.MoveInput);

    protected override bool register => true;
    public override EventHook GetHook(GraphReference r) => Hook;

    [DoNotSerialize]
    public ValueOutput value { get; private set; }

    protected override void Definition()
    {
        base.Definition();
        value = ValueOutput<short>(nameof(value));
    }

    protected override void AssignArguments(Flow flow, short args)
    {
        flow.SetValue(value, args);
    }
}

[UnitCategory("Events/Core")]
[UnitTitle(nameof(GestureInputEventUnit))]
// todo: rename to 'inputAxis2DEvent'
public sealed class GestureInputEventUnit : EventUnit<Vector2>
{
    public static string Hook = EnumHelper.GetStringID(CustomHook.GestureInput);

    protected override bool register => true;
    public override EventHook GetHook(GraphReference r) => Hook;

    [DoNotSerialize]
    public ValueOutput value { get; private set; }

    protected override void Definition()
    {
        base.Definition();
        value = ValueOutput<Vector2>(nameof(value));
    }

    protected override void AssignArguments(Flow flow, Vector2 args)
    {
        flow.SetValue(value, args);
    }
}

[UnitCategory("Events/Core")]
[UnitTitle(nameof(GripInputEventUnit))]
public sealed class GripInputEventUnit : EventUnit<bool>
{
    public static string Hook = EnumHelper.GetStringID(CustomHook.GripInput);

    protected override bool register => true;
    public override EventHook GetHook(GraphReference r) => Hook;

    [DoNotSerialize]
    public ValueOutput value { get; private set; }

    protected override void Definition()
    {
        base.Definition();
        value = ValueOutput<bool>(nameof(value));
    }

    protected override void AssignArguments(Flow flow, bool args)
    {
        flow.SetValue(value, args);
    }
}

[UnitCategory("Events/Core")]
[UnitTitle(nameof(ThrowInputEventUnit))]
public sealed class ThrowInputEventUnit : EventUnit<bool>
{
    public static string Hook = EnumHelper.GetStringID(CustomHook.ThrowInput);

    protected override bool register => true;
    public override EventHook GetHook(GraphReference r) => Hook;

    [DoNotSerialize]
    public ValueOutput value { get; private set; }

    protected override void Definition()
    {
        base.Definition();
        value = ValueOutput<bool>(nameof(value));
    }

    protected override void AssignArguments(Flow flow, bool args)
    {
        flow.SetValue(value, args);
    }
}

/* End input event units */

[UnitCategory("Events/Core")]
[UnitTitle(nameof(JumpEventUnit))]
public sealed class JumpEventUnit : GameObjectEventUnit<short>
{
    public static string EventHook = EnumHelper.GetStringID(CustomHook.Jump);

    protected override string hookName => EventHook;

    [DoNotSerialize]
    public ValueOutput value { get; private set; }

    protected override void Definition()
    {
        base.Definition();
        value = ValueOutput<short>(nameof(value));
    }

    protected override void AssignArguments(Flow flow, short args)
    {
        flow.SetValue(value, args);
    }

    public override Type MessageListenerType { get; }
}

[UnitCategory("Events/Core")]
[UnitTitle(nameof(MoveEventUnit))]
public sealed class MoveEventUnit : GameObjectEventUnit<short>
{
    public static string EventHook = EnumHelper.GetStringID(CustomHook.Move);

    protected override string hookName => EventHook;

    [DoNotSerialize]
    public ValueOutput value { get; private set; }

    protected override void Definition()
    {
        base.Definition();
        value = ValueOutput<short>(nameof(value));
    }

    protected override void AssignArguments(Flow flow, short args)
    {
        flow.SetValue(value, args);
    }

    public override Type MessageListenerType { get; }
}

[UnitCategory("Events/Core")]
[UnitTitle(nameof(WallClingEventUnit))]
public sealed class WallClingEventUnit : GameObjectEventUnit<bool>
{
    public static string EventHook = EnumHelper.GetStringID(CustomHook.WallCling);

    protected override string hookName => EventHook;

    [DoNotSerialize]
    public ValueOutput value { get; private set; }

    protected override void Definition()
    {
        base.Definition();
        value = ValueOutput<bool>(nameof(value));
    }

    protected override void AssignArguments(Flow flow, bool args)
    {
        flow.SetValue(value, args);
    }

    public override Type MessageListenerType { get; }
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
