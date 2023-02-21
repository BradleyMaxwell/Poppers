using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterMovement : MonoBehaviour
{
    [SerializeField] protected Character character;


    public void RotateCharacter(Quaternion newRotation, float speed) // rotates character in 3d space at a given speed using linear interpolation
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, speed * Time.deltaTime);
    }
}
