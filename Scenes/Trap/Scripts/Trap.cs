using Godot;
using System;

public partial class Trap : Node2D
{
    [Export]
    public int TrapDamage = 5;              // How much damage the trap will inflict
    private CustomSignals _customSignals;   // The custom signals singleton
    private Sprite2D _trapSprung;           // The trap sprung sprite
    private AudioStreamPlayer _spikesUp;    // Sound effect for the spikes going up
    private AudioStreamPlayer _spikesDown;  // Sound effect for the spikes going down	

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_customSignals = GetNode<CustomSignals>("/root/CustomSignals"); // Get the custom signals singleton
        _trapSprung = GetNode<Sprite2D>("TrapSprung");                  // Get the trap sprung node
        _spikesUp = GetNode<AudioStreamPlayer>("SpikesUp");             // Get the SpikesUp node
        _spikesDown = GetNode<AudioStreamPlayer>("SpikesDown");         // Get the SpikesDown node				
	}

	private void OnArea2DBodyEntered(Player body)
	{
   		if (body == null) { return; }                                               	// If the body is null, return out of the method		
		_customSignals.EmitCustomSignal(nameof(CustomSignals.DamagePlayer),TrapDamage);	// Emit the DamagePlayer signal, and pass in the trap damage as damage amount
        _trapSprung.Visible = true;                                                 	// Set the trap sprung sprite to visible
        _spikesUp.Play();                                                           	// Play the spikes up sound effect		
	}

	private void OnArea2DBodyExited(Player body)
	{
        if (body == null) { return; }                                               	// If the body is null, return out of the method		
        _trapSprung.Visible = false;                                                 	// Set the trap sprung sprite to visible
        _spikesDown.Play();                                                           	// Play the spikes down sound effect		
	}
}
