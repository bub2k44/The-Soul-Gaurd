using UnityEngine;

public class SoundManager : MonoBehaviour
{
    static AudioSource audioSource;

    #region Static Audio Clips
    public static AudioClip sg_grassWalk_1;
    public static AudioClip sg_grassWalk_2;
    #endregion

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        sg_grassWalk_1 = Resources.Load<AudioClip>("SG_Grass_Walk_1");
        sg_grassWalk_2 = Resources.Load<AudioClip>("SG_Grass_Walk_2");
    }

    public static void PlaySound(string _clip)
    {
        switch (_clip)
        {
            case "SG_Grass_Walk_1":
                audioSource.PlayOneShot(sg_grassWalk_1);
                break;

            case "SG_Grass_Walk_2":
                audioSource.PlayOneShot(sg_grassWalk_2);
                break;

            default:
                break;
        }
    }
}
