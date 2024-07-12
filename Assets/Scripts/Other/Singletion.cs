public class Singleton<T> where T : Singleton<T>
{
  private static T _instance;

  public static T Instance
  {
    get
    {
      if (_instance == null)
      {
        // _instance = (T)System.Activator.CreateInstance(typeof(T));
        _instance = System.Activator.CreateInstance<T>();
      }
      return _instance;
    }
  }
}