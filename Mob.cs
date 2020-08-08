using Godot;
using System;

public class Mob : RigidBody2D
{
    static private Random _random = new Random();

    [Export]
    public int MinSpeed = 150; // Minimum speed range.

    [Export]
    public int MaxSpeed = 250; // Maximum speed range.

    public override void _Ready()
    {
        var animSprite = GetNode<AnimatedSprite>("AnimatedSprite");
        var mobTypes = animSprite.Frames.GetAnimationNames();
        animSprite.Animation = mobTypes[_random.Next(0, mobTypes.Length)];
    }

    public void OnBodyEntered(PhysicsBody2D player) {
        player.EmitSignal("Hit");
    }

    public void OnVisibilityNotifier2DScreenExited() {
        QueueFree(); // delete on screen exit
    }
}
