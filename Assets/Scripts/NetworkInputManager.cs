// NetworkInputManager.cs
using Fusion;
using UnityEngine;

public class NetworkInputManager : MonoBehaviour
{
    private void Start()
    {
        // ������ NetworkRunner ã��
        NetworkRunner runner = FindObjectOfType<NetworkRunner>();
        if (runner != null)
        {
            runner.AddCallbacks(new NetworkCallbacks());
        }
        else
        {
            Debug.LogError("NetworkRunner �ν��Ͻ��� ã�� �� �����ϴ�. ���� NetworkRunner�� �߰��ϼ���.");
        }
    }
}
