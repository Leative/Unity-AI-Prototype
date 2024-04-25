using UnityEngine;

namespace AdvancedBlendAnimation
{
 public class AudioController : MonoBehaviour
 {
 
     [SerializeField]
     private AudioSource audioSource;
     [SerializeField]
     private AudioClip enemyFootstep;
     private int something;
 
     // Start is called before the first frame update
     void OnFootStep() {
         audioSource.PlayOneShot(enemyFootstep);
     }
 }
}
