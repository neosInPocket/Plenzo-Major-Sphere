using System.Linq;
using UnityEngine;

public class TrackController : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    private void Awake()
    {
        var possible = GameObject.FindObjectsOfType<TrackController>();
        var different = possible.FirstOrDefault(x => x.gameObject.scene.name == "DontDestroyOnLoad");

        if (different != null && different != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        audioSource.enabled = OptionsLoader.Options.mainTrackEnabled;
    }

    public bool Toggle()
    {
        audioSource.enabled = !audioSource.enabled;
        OptionsLoader.Options.mainTrackEnabled = audioSource.enabled;
        OptionsLoader.SaveOptions();
        return audioSource.enabled;
    }
}
