using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageTextHandler : MonoBehaviour
{
    public GameObject damageTextPrefab;
    public float inactiveTimeBeforeDestroy = 10f;
    public Vector3 offset = new Vector3(0, 10, 10);

    private GameObject activeDamageText;
    private float timeSinceLastDamage;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (activeDamageText)
        {
            Transform damageTextTransform = activeDamageText.transform;
            damageTextTransform.LookAt(2 * damageTextTransform.position - Camera.main.transform.position); // make text always face camera so it is always readable

        }

        if (Time.time >= timeSinceLastDamage + inactiveTimeBeforeDestroy)
        {
            Destroy(activeDamageText); // if there has been a certain amount of time since the last time the enemy was damaged, destroy the floating damage text
        }
    }

    public void NewDamage (int damage) // takes in the damage done to the enemy and creates/updates a floating damage text object
    {
        if (activeDamageText) // if there is already floating damage text for this enemy, then add the new damage to the existing damage being shown
        {
            DamageText damageText = activeDamageText.GetComponent<DamageText>();
            int existingDamage = int.Parse(damageText.Get());
            damageText.Set(existingDamage + damage);
        }
        else // otherwise create a floating damage text
        {
            activeDamageText = Instantiate(damageTextPrefab, transform.position, Quaternion.identity, transform);
            activeDamageText.GetComponent<DamageText>().Set(damage);
            activeDamageText.transform.localPosition += offset;
        }
        timeSinceLastDamage = Time.time;
    }
}
