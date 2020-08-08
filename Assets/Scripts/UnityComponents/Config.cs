using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/Configuration")]
public class Config : ScriptableObject
{
    public float PlayerSpeed = 1;
    public float BulletSpeed = 3;
    public float GameSpeed = 1;
    public float ShootCooldown = 1;
    public float AsteroidCooldown = 0.5f;
    public bool IsBothGunsShooting = false;

    public float Xsens = 1;
    public float Ysens = 1;
    public float MaxControlPower = 5;
    public float MaxMouseFollowSpeed = 10;

    public bool DragDrop = false;
    public bool JoyStick = true;
    public bool FollowMouse = false;

    public AudioClip explosion;
    public AudioClip win;
    public AudioClip shoot;
    public AudioClip reciveDmg;
    public AudioClip loseSound;
}
