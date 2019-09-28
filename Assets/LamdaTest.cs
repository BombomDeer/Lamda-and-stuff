using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//대리자(delegate)
//함수 포인터와 유사한 개념이다
//함수의 대리 역할을 수행할 수 있도록 구현
//함수 대입 가능하며 리턴 값과 매개변수의 타입과 개수가 맞아야한다
public delegate void Do();
public delegate int Fdo(int a, int b);

//람다식(Lamda)
//=> 연산자는 람다식을 표현할때사용
//=> 연산자를 기준으로 왼쪽은 매개변수를 의미하고
//오른쪽은 식이나 문 블럭을 의미
//매개변수가 없는 람다는 아래와 같이 표현
// () => x * x;
//두 수를 곱해서 리턴을 해준다. 매개변수가 없는 식 람다라 한다. 코드블럭이 없기 때문이다.

//매개변수가 없는 문 람다
//() => {
//          code goes here
//      };

//Nullable
//값타입의 변수에 null을 대입할 수 있다
struct Data
{
    public string strName;
    public float fStrength;
}


public class LamdaTest : MonoBehaviour
{
    List<Data> list = new List<Data>();
    MultiMap<string, Data> multimap = new MultiMap<string, Data>();//멀티맵 생성

    //delegate를 쓸려면 변수를 생성해줘야 한다
    Do doSomething = null;
    Fdo doFdo = null;

    // Start is called before the first frame update
    void Start()
    {
        Data mdata_1;
        mdata_1.strName = "바바리안";
        mdata_1.fStrength = 100f;
        multimap.AddData(mdata_1.strName, mdata_1);

        Data mdata_2;
        mdata_2.strName = "바바리안";
        mdata_2.fStrength = 3f;
        multimap.AddData(mdata_2.strName, mdata_2);

        Data mdata_3;
        mdata_3.strName = "바바리안";
        mdata_3.fStrength = 45f;
        multimap.AddData(mdata_3.strName, mdata_3);

        List<Data> datas = multimap.GetData("바바리안");
        //결과로 받아온 리스트의 데이터를 fStrength 변수로 정렬       
        datas.Sort((Data aa, Data b) =>
        aa.fStrength.CompareTo(b.fStrength));//this whole thing is a sorting thing

        for(int i=0;i<datas.Count;i++)
        {
            Debug.Log(datas[i].fStrength.ToString());
        }

        doSomething = Test;//이름만 대입
        doSomething();

        doFdo = Plus;
        int nReturn = doFdo(10, 20); //Plus() 함수 호출
        Debug.Log(nReturn.ToString());
        doFdo = Minus;
        nReturn = doFdo(10, 20); //Minus() 함수 호출
        Debug.Log(nReturn.ToString());

        Do tmp = Test;
        //SetDo(Test);
        SetDo(tmp);

        //Do tt = () =>
        //{
        //    Debug.Log("tt with Lamda");
        //};

        //SetDo(tt);


        SetDo(() =>
        {
            Debug.Log("tt with Lamda");
        });//이 경우에는 세미콜론 제외
        //구글 상용화 할때 사용한다네 이 방식을

        //nullable in action
        //add ? to the end of a data type to make it nullable i guess
        int? a = null;
        a = 150;
        //Debug.Log(a);

        if(a.HasValue)//this is how you use nullable i guess
        {
            Debug.Log(a.Value);
        }

        Data tmp_1;
        tmp_1.strName = "홍길동";
        tmp_1.fStrength = 100f;
        list.Add(tmp_1);

        Data tmp_2;
        tmp_2.strName = "가이오가";
        tmp_2.fStrength = 21545f;
        list.Add(tmp_2);

        Data? dReturn = list.Find(o => (o.strName == "가가가1"));//this is how you use nullable
        if (dReturn.HasValue)
        {
            Debug.Log(dReturn.Value.strName);
        }
        else
            Debug.Log("dReturn is null");

        //c#에서 미리 만들어 놓은 delegate들 needs using system
        //따로 선언이 필요없다
        //Func<T, TResult> : 반환값이 있는 delegate
        Func<float> func0 = () => 0.1f;//float이 반환값이다
        //int 형 매개변수 한개 와 float형 반환값
        Func<int, float> func1 = (d) => d * 0.1f;
        //Action : 반환값이 없는 delegate
    }

    void Test()//doSomething과 리턴값과 매개변수가 같으므로 된다
    {
        Debug.Log("Test");
    }

    int Plus(int a, int b)
    {
        return a + b;
    }

    int Minus(int a, int b)
    {
        return a - b;
    }

    public void SetDo(Do _do)//SetDo를 public으로 했으니 외부에서 실시간으로 함수를 만들어서 전달할 수 있다
    {
        doSomething = _do;
    }

    // Update is called once per frame
    void Update()
    {
        //why do we use delegates
        if(Input.GetMouseButton(0))
        {
            doSomething();
        }
    }
}
