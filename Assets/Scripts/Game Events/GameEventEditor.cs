using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
[CustomEditor(typeof(GameEvent))]
public class GameEventEditor : Editor
{
        public override void OnInspectorGUI(){
            DrawDefaultInspector();
            GameEvent gameEvent = (GameEvent)target;

            if (GUILayout.Button("Raise an Event!"))
            {
                gameEvent.Raise();
            }
        }
}
#endif