using System.Collections.Generic;
public class Data { }
public class Data<T> : Data
{
    public T value;
}
public class Data<T, T1> : Data
{
    public T value;
    public T1 value1;
}
public class Data<T, T1, T2> : Data
{
    public T value;
    public T1 value1;
    public T2 value2;
}

public class DataManager : Singleton<DataManager>
{
    Dictionary<string, Data> dataDic = new Dictionary<string, Data>();


    public void SaveData(string key, Data data)
    {
        dataDic[key] = data;
    }

    public Data GetData(string key)
    {
        if (dataDic.ContainsKey(key))
        {
            return dataDic[key];
        }
        return null;
    }
}
