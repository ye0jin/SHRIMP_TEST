using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glass : MonoBehaviour
{
    [SerializeField] private GameObject glassFragment;
    [SerializeField] private GameObject glassParticle;
    [SerializeField] private int count;
    [SerializeField] private float rand;

    private IEnumerator Broke()
    {
        Animator anim = GetComponent<Animator>();

        anim.SetTrigger("Broke");
        yield return null;
        yield return new WaitForSeconds(anim.GetNextAnimatorClipInfo(0).Length);

        for (int i=0; i<count; i++)
        {
            Instantiate(glassFragment, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
        SoundManager.Instance.PlayGlassBrokenSound();

        Instantiate(glassParticle, transform.position, Quaternion.identity);

        yield break;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();

            if (player.IsDash)
            {
                StartCoroutine(Broke());
            }
        }
    }
}
