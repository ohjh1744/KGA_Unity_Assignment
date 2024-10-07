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
        //�߰����� �󱼿� �������(��ġ, ȸ��)�� ���� ��
        if(args.updated.Count > 0) // ����� �ν�����â���� �ν�num�� ���� �� �ϳ��� �����ϴ� ��
        {
            // ARFace�� �����ͼ�
            ARFace face = args.updated[0];

            //�󱼿� �ִ� ��� ���� 
            for(int i = 0; i < face.vertices.Length; i++)
            {
                //�� ������ ��ġ�� ������ġ�� ��ȯ
                Vector3 vertPos = face.transform.TransformPoint(face.vertices[i]);

                // ������ ť����� ������ ��ġ�� �̵�
                cubes[i].transform.position = vertPos;
            }
        }
    }
}
