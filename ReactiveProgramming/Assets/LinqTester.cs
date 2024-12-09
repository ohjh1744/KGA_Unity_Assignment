using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LinqTester : MonoBehaviour
{
    private List<MonsterData> monsters = new List<MonsterData>() {
        new MonsterData(MonsterType.Normal, 10, "꼬렛"),
        new MonsterData(MonsterType.Fire, 7, "파이리"),
        new MonsterData(MonsterType.Water, 12, "꼬부기"),
        new MonsterData(MonsterType.Grass, 8, "이상해씨"),
        new MonsterData(MonsterType.Normal, 5, "꼬리선"),
    };

    [SerializeField] List<int> database = new List<int>() {1, 2, 3, 4, 5, 6, 7, 8, 9 };

    private List<GameObject> targets = new List<GameObject>();

    private void Start()
    {
        //var quary = from element in database
        //            where element > 5
        //            where element > 3
        //            orderby element descending
        //            select element;

        //List<int> list = quary.ToList();

        //for(int i = 0; i < list.Count; i++)
        //{
        //    Debug.Log(list[i]);
        //}

        var q = from target in Physics.OverlapSphere(transform.position, 3f)
                where target.gameObject.layer == LayerMask.NameToLayer("Monster")
                where Physics.Linecast(transform.position, target.transform.position) == false
                select target;


        var quary = from monster in monsters
                    where monster.type == MonsterType.Normal

                    orderby monster.level ascending, monster.name ascending
                    select monster;
        List<MonsterData> list = quary.ToList();

        for(int i = 0; i < list.Count; i++)
        {
            Debug.Log($"{list[i].name} {list[i].level} {list[i].type}");
        }


    }

    // linq를 이용안한다면 작성해야하는 함수
    public List<int> Search()
    {
        List<int> result = new List<int>();

        foreach(int element in database)
        {
            if(element > 5)
            {
                result.Add(element);
            }
        }

        return result;
    }

    public enum MonsterType { Normal, Fire, Water, Grass }

    public class MonsterData
    {
        public MonsterType type;
        public int level;
        public string name;

        public MonsterData(MonsterType type, int level, string name)
        {
            this.type = type;
            this.level = level;
            this.name = name;
        }
    }

}
