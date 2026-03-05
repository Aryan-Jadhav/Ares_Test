using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private AudioSource sfxSource;

    [Header("Clips")]
    [SerializeField] private AudioClip cardToBeltClip;
    [SerializeField] private AudioClip crateDestroyClip;
    [SerializeField] private AudioClip beltToAcceptClip;

    private void Awake()
    {
        Instance = this;
    }

    public void PlayCardToBelt()
    {
        PlayWithVariation(cardToBeltClip);
    }

    public void PlayCrateDestroy()
    {
        PlayWithVariation(crateDestroyClip);
    }

    public void PlayBeltToAccept()
    {
        PlayWithVariation(beltToAcceptClip);
    }

    private void PlayWithVariation(AudioClip clip)
    {
        if (clip == null) return;

        sfxSource.pitch = Random.Range(0.95f, 1.05f);
        sfxSource.PlayOneShot(clip);
        sfxSource.pitch = 1f;
    }
}