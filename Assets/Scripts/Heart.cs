using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject explosionPrefab;
    public Sprite base_destroyed;
    public AudioClip dieAudio;
    
    public void baseDestroyed()
    {
        this.GetComponent<SpriteRenderer>().sprite = base_destroyed;
        explode();
    }
    private void explode()
    {
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        PlayerManager.Instance.isDefeat = true;
        AudioSource.PlayClipAtPoint(dieAudio, this.transform.position);
    }
}
