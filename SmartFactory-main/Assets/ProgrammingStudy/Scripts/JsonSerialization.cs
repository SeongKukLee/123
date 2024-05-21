﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

// 컨테이너 클래스: 데이터 저장용 클래스
public class DeviceInfo
{
    public string name;             // 송출실린더
    public string serialNumber;     // 실린더A
    public int operationTime;       // Play버튼 누른 후의 누적시간
    public int operationCount;      // 실린더 전후진 횟수
    public string freeWarrenty;     // 하드웨어 무상 보증기간
    public string paidWarrenty;     // 하드웨어 유상 보증기간

    public DeviceInfo(string name, string serialNumber, int operationTime,
        int operationCount, string freeWarrenty, string paidWarrenty)
    {
        this.name = name;
        this.serialNumber = serialNumber;
        this.operationTime = operationTime;
        this.operationCount = operationCount;
        this.freeWarrenty = freeWarrenty;
        this.paidWarrenty = paidWarrenty;
    }
}

public class JsonSerialization : MonoBehaviour
{
    public static JsonSerialization Instance;
    public List<DeviceInfo> devices = new List<DeviceInfo>();

    // Object(Class) -> JSON
    public class Person
    {
        public string name;
        public int age;

        public Person(string name, int age)
        {
            this.name = name;
            this.age = age;
        }
    }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    // 버튼을 누르면, 모든 device의 정보를 전달한다.

    // Start is called before the first frame update
    void Start()
    {
        // 단일 데이터를 저장하는 방법
        DeviceInfo info = new DeviceInfo("신태욱", "123456", 55555, 5555, "2024.05.30", "2026.06.30");

        string json = JsonUtility.ToJson(info); // 직렬화(serialization)
        print(json);

        FileStream fs = new FileStream("Assets/file.json", FileMode.Create); // 파일을 열고, 닫는 기본적인 입출력 기능
        StreamWriter sw = new StreamWriter(fs); // 문자 단위로 데이터 쓰기, 인코딩 처리
        sw.Write(json);
        sw.Close();
        fs.Close();


        // 여러개의 데이터를 저장하는 방법
        DeviceInfo info1 = new DeviceInfo("신태욱", "123456", 55555, 5555, "2024.05.30", "2026.06.30");
        DeviceInfo info2 = new DeviceInfo("신태욱1", "123456", 55555, 5555, "2024.05.30", "2026.06.30");
        DeviceInfo info3 = new DeviceInfo("신태욱2", "123456", 55555, 5555, "2024.05.30", "2026.06.30");
        DeviceInfo info4 = new DeviceInfo("신태욱3", "123456", 55555, 5555, "2024.05.30", "2026.06.30");
        DeviceInfo info5 = new DeviceInfo("신태욱4", "123456", 55555, 5555, "2024.05.30", "2026.06.30");
        List<DeviceInfo> devices = new List<DeviceInfo>();
        devices.Add(info1);
        devices.Add(info2);
        devices.Add(info3);
        devices.Add(info4);
        devices.Add(info5);

        // 객체(Object) -> JSON Serialization
        string json2 = JsonConvert.SerializeObject(devices);
        print(json2);

        // 파일로 저장
        fs = new FileStream("Assets/file2.json", FileMode.Create); // 파일을 열고, 닫는 기본적인 입출력 기능
        sw = new StreamWriter(fs); // 문자 단위로 데이터 쓰기, 인코딩 처리
        sw.Write(json2);
        sw.Close();
        fs.Close();

        // DeviceInfo라는 컨테이너 클래스의 모양을 알고 있을 경우 사용
        List<DeviceInfo> newDevices = new List<DeviceInfo>();

        // JSON -> 객체(Object) Deserialization
        newDevices = JsonConvert.DeserializeObject<List<DeviceInfo>>(json2);
        DeviceInfo deviceFound = newDevices.Find(x => x.name == "신태욱3");
        print(deviceFound.freeWarrenty);

        // 복잡한 형태의 규칙인 경우, JObject, JArray
        string json3 = @"{
          'channel': {
            'title': 'ABC',
            'link': 'http://ABC.com',
            'description': 'ABC's blog.',
            'item': [
              {
                'title': 'Json.NET 1.3 + New license + Now on CodePlex',
                'description': 'Annoucing the release of Json.NET 1.3',
                'link': 'http://ABC.aspx',
                'categories': [
                  'Json.NET',
                  'CodePlex'
                ]
              },
              {
                'title': 'LINQ to JSON beta',
                'description': 'Annoucing LINQ to JSON',
                'link': 'http://ABC.aspx',
                'categories': [
                  'Json.NET',
                  'LINQ'
                ]
              }
            ]
          }
        }";

        JObject jObj = JObject.Parse(json3);
        string title = (string)jObj["channel"]["title"];
        string description = (string)jObj["channel"]["item"][1]["description"];


        
        Person person = new Person("신태욱", 20);

        // 객체(Object) -> JSON Serialization
/*        string json = JsonUtility.ToJson(person);

        print(json);

        // JSON -> 객체(Object) Deserialization
        Person person2 = JsonUtility.FromJson<Person>(json);
        print($"{person2.name}, {person2.age}");*/
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) 
        {
            FileStream fs = new FileStream("Assets/file.json", FileMode.Open);
            StreamReader sr = new StreamReader(fs);
            string json = sr.ReadToEnd();
            print(json);
            sr.Close();
            fs.Close();
        }
    }
}
