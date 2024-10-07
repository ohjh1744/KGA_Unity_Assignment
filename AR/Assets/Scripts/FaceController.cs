using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class FaceController : MonoBehaviour
{
    [SerializeField] ARFaceManager faceManager;

    [SerializeField] List<GameObject> cubes = new List<GameObject>(486);
    [SerializeField] GameObject cubePrefab;

    private void Awake()
    {
        for(int i = 0; i < 486; i++)
        {
            GameObject cube = Instantiate(cubePrefab);
            cubes.Add(cube);
        }
    }
    private void OnEnable()
    {
        faceManager.facesChanged += OnFaceChange;
    }

    private void OnDisable()
    {
        faceManager.facesChanged -= OnFaceChange;
    }
    
    private void OnFaceChange(ARFacesChangedEventArgs args)
    {
        //추격중인 얼굴에 변경사항(위치, 회전)이 있을 때
        if(args.updated.Count > 0) // 현재는 인스펙터창에서 인식num에 따라 얼굴 하나만 적용하는 중
        {
            // ARFace를 가져와서
            ARFace face = args.updated[0];

            //얼굴에 있는 모든 점을 
            for(int i = 0; i < face.vertices.Length; i++)
            {
                //얼굴 기준의 위치를 월드위치로 변환
                Vector3 vertPos = face.transform.TransformPoint(face.vertices[i]);

                // 생성한 큐브들을 기준의 위치로 이동
                cubes[i].transform.position = vertPos;
            }
        }
    }
}
