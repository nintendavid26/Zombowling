using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class SpeedHead : ZombieHead {

    public float PlayerSpeedUp;
    public float ZombieSpeedUp;

    // Use this for initialization
    public override ZombieHead InitializeZombieHead()
    {
        base.InitializeZombieHead();
        zombie.speed *= ZombieSpeedUp;
        return this;
    }

    public override void GetPickedUp(Player picker)
    {
        base.GetPickedUp(picker);
        RigidbodyFirstPersonController r = picker.GetComponent<RigidbodyFirstPersonController>();
        r.movementSettings.ForwardSpeed *= PlayerSpeedUp;
        r.movementSettings.BackwardSpeed *= PlayerSpeedUp;
        r.movementSettings.StrafeSpeed *= PlayerSpeedUp;
        r.movementSettings.ForwardSpeed *= PlayerSpeedUp;
    }
    public override void Thrown(Player thrower)
    {
        RigidbodyFirstPersonController r = thrower.GetComponent<RigidbodyFirstPersonController>();
        r.movementSettings.ForwardSpeed /= PlayerSpeedUp;
        r.movementSettings.BackwardSpeed /= PlayerSpeedUp;
        r.movementSettings.StrafeSpeed /= PlayerSpeedUp;
        r.movementSettings.ForwardSpeed /= PlayerSpeedUp;
        base.Thrown(thrower);
    }
}
