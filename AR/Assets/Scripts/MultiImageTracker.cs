using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class MultiImageTracker : MonoBehaviour
{
    [SerializeField] ARTrackedImageManager imageManager;

    [SerializeField] GameObject dragonPrefab;

    [SerializeField] GameObject magicianPrefab;


    private void OnEnable()
    {
        imageManager.trackedImagesChanged += OnImageChange;
    }

    private void OnDisable()
    {
        imageManager.trackedImagesChanged -= OnImageChange;
    }

    private void OnImageChange(ARTrackedImagesChangedEventArgs args)
    {
        // 새로운 이미지가 발견되었을 때
        foreach (ARTrackedImage trackedImage in args.added)
        {
            // 이미지 라이브러리에서 이미지의 이름을 확인
            string imageName = trackedImage.referenceImage.name;

            // 새로운 게임오브젝트를 트래킹한 이미지의 자식으로 생성
            switch (imageName)
            {
                case "Dragon":
                    GameObject dragon = Instantiate(dragonPrefab, trackedImage.transform.position, trackedImage.transform.rotation);
                    dragon.transform.parent = trackedImage.transform;
                    break;
                case "Magician":
                    GameObject magician = Instantiate(magicianPrefab, trackedImage.transform.position, trackedImage.transform.rotation);
                    magician.transform.parent = trackedImage.transform;
                    break;
            }
        }

        // 기존의 이미지가 변경(이동, 회전) 되었을 때
        foreach (ARTrackedImage trackedImage in args.updated)
        {
            // 이미지의 변경사항이 있는 경우 자식으로 있던 게임오브젝트를 위치와 회전을 갱신
            trackedImage.transform.GetChild(0).position = trackedImage.transform.position;
            trackedImage.transform.GetChild(0).rotation = trackedImage.transform.rotation;
        }

        // 기존의 이미지가 사라졌을 때
        foreach (ARTrackedImage trackedImage in args.removed)
        {
            // 이미지가 사라진 경우 자식으로 있었던 게임오브젝트를 삭제
            Destroy(trackedImage.transform.GetChild(0).gameObject);
        }
    }
}
