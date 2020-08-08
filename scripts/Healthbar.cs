using Godot;
using System;

public class Healthbar : TextureProgress
{
    public void OnHealthChanged(int amount) {
        this.Value += amount;
    }
}
