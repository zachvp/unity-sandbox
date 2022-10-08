using Unity.VisualScripting;
using UnityEngine;
using System;

// This file declare custom events for the game. When you write your own event, don't forget to Regenerate Units through
// the "Regenerate Unit" button in the Visual Script section of the Project Settings

[UnitCategory("Events/Test")]
[UnitTitle("Test_0")]
public sealed class Test_0 : GameObjectEventUnit<short>
{
    public static string EventHook = nameof(Test_0);

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
[UnitTitle(nameof(JumpInputEventUnit))]
public sealed class JumpInputEventUnit : GameObjectEventUnit<bool>
{
    public static string EventHook = nameof(JumpInputEventUnit);

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

[UnitCategory("Events/Core")]
[UnitTitle(nameof(JumpEventUnit))]
public sealed class JumpEventUnit : GameObjectEventUnit<short>
{
    public static string EventHook = nameof(JumpInputEventUnit);

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
[UnitTitle(nameof(MoveInputEventUnit))]
public sealed class MoveInputEventUnit : GameObjectEventUnit<short>
{
    public static string EventHook = nameof(MoveInputEventUnit);

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
    public static string EventHook = nameof(MoveEventUnit);

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