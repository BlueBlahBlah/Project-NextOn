using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureInfo : MonoBehaviour
{
    public int HP;
    public string tag;
    public float speed;

    public virtual void SetHP(int hp)
    {
        this.HP = hp;
    }
    public virtual int GetHP()
    {
        return this.HP;
    }
    public virtual void SetTag(string tag){
        this.tag = tag;
    }
    public virtual string GetString()
    {
        return this.tag;
    }
    public virtual void SetSpeed(float speed)
    {
        this.speed = speed;
    }
    public virtual float GetSpeed()
    {
        return this.speed;
    }
}
