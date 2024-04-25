using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Reward : ScriptableObject
{
    [SerializeField]
    private Sprite icon;
    [SerializeField]
    private string description;
    [SerializeField]
    private int quantity;

    public Sprite Icon => icon;
    public string Description => description;
    public int Quantity => quantity;

    public abstract void Give(Quest quest);
}
