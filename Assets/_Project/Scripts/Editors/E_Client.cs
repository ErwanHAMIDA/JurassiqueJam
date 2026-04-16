#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SO_Client))]
public class E_Client : Editor
{
    private SO_Client _client;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        _client = (SO_Client)target;

        GenericMenu menu = new GenericMenu();
        menu.AddItem(new GUIContent("Joyful"), _client.Satisfaction == S_ClientManager.ClientSatisfaction.Joyful, OnSatisfactionSelected, S_ClientManager.ClientSatisfaction.Joyful);
        menu.AddItem(new GUIContent("Happy"), _client.Satisfaction == S_ClientManager.ClientSatisfaction.Happy, OnSatisfactionSelected, S_ClientManager.ClientSatisfaction.Happy);
        menu.AddItem(new GUIContent("Unhappy"), _client.Satisfaction == S_ClientManager.ClientSatisfaction.Unhappy, OnSatisfactionSelected, S_ClientManager.ClientSatisfaction.Unhappy);
        menu.AddItem(new GUIContent("Sad"), _client.Satisfaction == S_ClientManager.ClientSatisfaction.Sad, OnSatisfactionSelected, S_ClientManager.ClientSatisfaction.Sad);

        if (EditorGUILayout.DropdownButton(new GUIContent(_client.Satisfaction.ToString()), FocusType.Keyboard))
        {
            menu.ShowAsContext();
        }
    }

    private void OnSatisfactionSelected(object args)
    {
        _client.Satisfaction = (S_ClientManager.ClientSatisfaction)args;
    }
}
#endif