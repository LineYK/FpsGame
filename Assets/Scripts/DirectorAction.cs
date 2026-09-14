using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Playables;

public class DirectorAction : MonoBehaviour
{
    PlayableDirector pd;

    [SerializeField]
    private Camera targetCam;

    void Start()
    {
        pd = GetComponent<PlayableDirector>();
        pd.Play();
    }

    void Update()
    {
        // 현재 진행 중인 시간 전체 시간 비교
        if (pd.time >= pd.duration)
        {
            if (Camera.main ==  targetCam)
            {
                targetCam.GetComponent<CinemachineBrain>().enabled = false;
            }

            targetCam.gameObject.SetActive(false);

            gameObject.SetActive(false);
        }

    }
}
