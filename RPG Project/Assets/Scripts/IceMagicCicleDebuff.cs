using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceMagicCicleDebuff : MagicCircle
{
    [SerializeField]
    private GameObject iceSpell;
    public override void MagicCircleDebuff() //Debuff ability
    {
        StartCoroutine(IceDebuff());
   
    }
    private IEnumerator IceDebuff() //Debuff ability
    {

        for (int i = 0; i < 3; i++)
        {
            GameObject q;
            q = Instantiate(iceSpell, transform.position, transform.rotation);
            q.GetComponent<SpellScript>().InitilizeV3(new Vector2(transform.position.x + 20, transform.position.y ), 4);
            q = Instantiate(iceSpell, transform.position, transform.rotation);
            q.GetComponent<SpellScript>().InitilizeV3(new Vector2(transform.position.x , transform.position.y + 20), 4);
            q = Instantiate(iceSpell, transform.position, transform.rotation);
            q.GetComponent<SpellScript>().InitilizeV3(new Vector2(transform.position.x - 20, transform.position.y), 4);
            q = Instantiate(iceSpell, transform.position, transform.rotation);
            q.GetComponent<SpellScript>().InitilizeV3(new Vector2(transform.position.x, transform.position.y - 20), 4);

            q = Instantiate(iceSpell, transform.position, transform.rotation);
            q.GetComponent<SpellScript>().InitilizeV3(new Vector2(transform.position.x + 20 , transform.position.y + 20 ), 4);
            q = Instantiate(iceSpell, transform.position, transform.rotation);
            q.GetComponent<SpellScript>().InitilizeV3(new Vector2(transform.position.x - 20 , transform.position.y + 20 ), 4);
            q = Instantiate(iceSpell, transform.position, transform.rotation);
            q.GetComponent<SpellScript>().InitilizeV3(new Vector2(transform.position.x - 20 , transform.position.y - 20), 4);
            q = Instantiate(iceSpell, transform.position, transform.rotation);
            q.GetComponent<SpellScript>().InitilizeV3(new Vector2(transform.position.x + 20 , transform.position.y - 20 ), 4);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
