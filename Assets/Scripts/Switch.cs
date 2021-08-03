using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public string UniqueID;
    public Gate AssociatedGate;
    public bool Activated = false;
    public SpriteRenderer Sprite;
    public Sprite DeactivatedSprite;
    public Sprite ActivatedSprite;

    
    public void ActivateSwitch()
    {
        Activated = true;
        AssociatedGate.OpenGate();
        Sprite.sprite = ActivatedSprite;
    }
}
