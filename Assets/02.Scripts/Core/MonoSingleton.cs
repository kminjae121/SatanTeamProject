using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour
   where T : MonoBehaviour
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindAnyObjectByType<T>();
                if (instance == null)
                {
                    DontDestroyOnLoad(instance);
                    Debug.LogError($"���� Hierachy�� {typeof(T).Name} Script�� �����ϴ�. ");
                }
            }
            return instance;
        }
    }
}
