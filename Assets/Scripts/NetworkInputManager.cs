// NetworkInputManager.cs
using Fusion;
using UnityEngine;

public class NetworkInputManager : MonoBehaviour
{
    private void Start()
    {
        // 씬에서 NetworkRunner 찾기
        NetworkRunner runner = FindObjectOfType<NetworkRunner>();
        if (runner != null)
        {
            runner.AddCallbacks(new NetworkCallbacks());
        }
        else
        {
            Debug.LogError("NetworkRunner 인스턴스를 찾을 수 없습니다. 씬에 NetworkRunner를 추가하세요.");
        }
    }
}
