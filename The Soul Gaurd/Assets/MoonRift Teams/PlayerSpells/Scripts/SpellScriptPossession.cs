using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellScriptPossession : SpellScript
{
    private MyPlayerInput myPlayerInput;
    private Animal animal;

    private GameObject myGameObject;

    protected override void OnTriggerEnter(Collider other)
    {
        //base.OnTriggerEnter(other);
        if (other.CompareTag("HitBox") && other.transform == MyTarget)
        {
            speed = 0;

            other.GetComponentInParent<Animal>().TakeDamage(damage);
            
            Debug.Log("Impact");
            Instantiate(explosion, transform.position, Quaternion.identity);
            rb.velocity = Vector3.zero;
            MyTarget = null;
            Destroy(gameObject);

            if (other.gameObject.GetComponentInParent<Animal>().CompareTag("Bear"))
            {
                //myPlayerInput.GetComponentInParent<MyPlayerInput>();
                myPlayerInput = FindObjectOfType<MyPlayerInput>().GetComponent<MyPlayerInput>();

                myPlayerInput.isBear = true;
                myPlayerInput.isPlayer = false;
                myPlayerInput.isWolf = false;

                myPlayerInput.bear.SetActive(true);
                myPlayerInput.player.SetActive(false);
                myPlayerInput.wolf.SetActive(false);
                myPlayerInput.myAnimations.animator = myPlayerInput.transform.GetChild(1).GetComponent<Animator>();
                myPlayerInput.canJump = false;
            }

            myGameObject = other.GetComponentInParent<Animal>().gameObject;
            myGameObject.SetActive(false);
            //other.GetComponentInParent<Animal>().gameObject.SetActive(false);
            UIManager.MyInstance.HideTargetFrame();

            //myGameObject.StartCoroutine(myGameObject.ResetAnimal());
            
            //animal.isActive = false;
            
        }
    }

    //public IEnumerator ResetAnimal()
    //{
    //    yield return new WaitForSeconds(30);
    //    myGameObject.SetActive(true);
    //}
}
