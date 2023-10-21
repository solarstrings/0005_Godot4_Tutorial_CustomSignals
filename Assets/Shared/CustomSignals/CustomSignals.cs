using Godot;

public partial class CustomSignals : Node
{
	[Signal]
	public delegate void DamagePlayerEventHandler(int damageAmount);

    public void EmitCustomSignal(StringName signalName, params Variant[] args)
    {
        EmitSignal(signalName, args);
    }	
}
