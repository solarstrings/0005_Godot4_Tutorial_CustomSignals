using Godot;
using System;

public partial class HealthLabel : Label
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Text = "Health: full";		
	}
	private void OnPlayerUpdateHealth(int health, Vector2 position)
	{
		Text = $"Health: {health}. Last position player was hurt: {position}";		
	}
	
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
