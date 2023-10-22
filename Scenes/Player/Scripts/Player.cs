using Godot;
using System;

public partial class Player : CharacterBody2D
{
    [Signal]
    public delegate void UpdateHealthEventHandler(int health, Vector2 position);    
    [Export]
    public float MaxSpeed = 350;                // The max movement speed for the player
    [Export]
    public float Acceleration = 1600;           // How fast the player accellerates
    [Export]
    public float Friction = 5f;                 // Friction for interpolating the speed down when the player is not moving	
	private CustomSignals _customSignals;       // The custom signals singleton
    private int _health = 100;                  // The player's health    	

    public override void _Ready()
	{
        _customSignals = GetNode<CustomSignals>("/root/CustomSignals"); // Get the custom signals singleton
        _customSignals.DamagePlayer += HandleDamagePlayer;              // Connect to the Damage player signal
	}	
	private void HandleDamagePlayer(int damageAmount)	
	{
		_health -= damageAmount;
        EmitSignal(SignalName.UpdateHealth, _health, this.Position);    // Emit the "UpdateHeath" signal, and pass in the current player health and position        
	}
    public override void _PhysicsProcess(double delta)
    {
        MovePlayer(delta);  // move the player
    }
    private void MovePlayer(double delta)
    {
        var inputVector = Vector2.Zero;                                                             // Initialize the input vector to zero
        inputVector.X = Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left");   // Get the horizontal input strength
        inputVector.Y = Input.GetActionStrength("ui_down") - Input.GetActionStrength("ui_up");      // Get the vertical input strength
        inputVector.Normalized();                                                                   // Normalize the lenght of the input vector

        // If the player is not moving
        if (inputVector == Vector2.Zero)
        {
            Velocity = Velocity.Lerp(Vector2.Zero, Friction * (float)delta);    // Interpolate the speed towards 0
        }
        // If the player is moving
        else
        {
            Velocity += inputVector * Acceleration * (float)delta;    // Calculate the velocity
            Velocity = Velocity.LimitLength(MaxSpeed);        // Limit velocity to max speed
        }
        MoveAndSlide();  // Update movement with Godot's in-built move and slide method
    }	
}
