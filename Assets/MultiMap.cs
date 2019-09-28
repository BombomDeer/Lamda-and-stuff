using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//멀티맵이란 키 하나에 여러개의 데이터를 리턴해주는 자료구조
//Dictionary는 키하나에 대응되는 값이 하나 존재하는데
//멀티맵은 키하나에 대응되는 값을 리스트 또는 스택, 큐로 리턴하도록 구현한다.
//여기서는 list 를 value로 넣었지만 꼭 그럴 필요는 없고 딴거도 넣어도 된다.

public class MultiMap<TKey, TValue>//this multimap in particular doesn't need monobehaviour. instead we use generic표현
{
    public Dictionary<TKey, List<TValue>> dic =
        new Dictionary<TKey, List<TValue>>();

    public void AddData(TKey key, TValue val)
    {
        List<TValue> list;
        if(dic.TryGetValue(key, out list))//같은 키의 데이터가 들어오면 그 리스트로 넣고
        {
            list.Add(val); //both work cause of out
            //dic[key].Add(val);
        }
        else//없는 키면 새로 그 키값으로 새로운 리스트를 만든다
        {
            list = new List<TValue>();
            list.Add(val);
            dic.Add(key, list);
        }
    }

    public List<TValue> GetData(TKey key)
    {
        List<TValue> list;
        if (dic.TryGetValue(key, out list))//있으면 전달
        {
            //list.Sort((TValue x, TValue y) => x.CompareTo(y));//x and y can be whatever

            return list;
        }
        else//없으면 null 전달
            return null;
    }
}
