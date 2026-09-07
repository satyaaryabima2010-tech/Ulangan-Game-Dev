using UnityEngine;

public class Zombie : Enemy
{
 public bool flag = true;

    public override void Serang()
    {
        Debug.Log("Zombie Gigit");
    }
}
