//
//   CONTRIBUTORS: Montana Games (www.montana-games.com)                  
//   Victor Bisterfeld (www.gbviktor.de) viktor@montana-games.com    
//   -------------------------------------------------------------------------------------------------
//

using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(WayPointsPath2D))]
public class WayPointsPath2DEditor : Editor
{
    private enum EditorState
    {
        DEFAULT,
        NEWPOINT,
        DELETE
    }

    private EditorState editorState = EditorState.DEFAULT;
    private EditorState lastEditorState;
    private WayPointsPath2D system;

    private Vector3 lastPoint;
    Vector3 mousePosition;

    private Tool lastTool;
    private int deleteIndex;

    void OnEnable()
    {
        system = target as WayPointsPath2D;

        if (system == null)
            return;

        lastTool = Tools.current;
        Tools.current = Tool.None;
    }

    void OnSceneGUI()
    {

        if (lastEditorState != editorState)
        {
            Repaint();
            lastEditorState = editorState;
        }

        if (system == null)
            return;



        if (editorState == EditorState.DEFAULT)
        {
            deleteIndex = -1;
            DefaultSceneGUI();

            if (Event.current.shift)
            {
                deleteIndex = -1;
                NewPointSceneGUI();
            }
        }
        else if (editorState == EditorState.DELETE)
        {
            DefaultSceneGUI();
        }

        
            CheckGUIChanged();

    }
    void CheckGUIChanged()
    {
        if (GUI.changed)
        {
            SceneView.RepaintAll();
        }
    }
    void UpdateMouseInput()
    {
        mousePosition  = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition).origin;
    }

    private void NewPointSceneGUI()
    {
        UpdateMouseInput();
        ShowWayPointUnderMouse();

        if (Event.current.type == EventType.KeyUp && Event.current.shift)
        {
            Vector3 temp = GetPointInWorldSpace();

            if (temp != Vector3.zero)
            {
                system.AddNewPathPoint(temp);
                editorState = EditorState.DEFAULT;
                Event.current.Use();
            }
        }

        SceneView.RepaintAll();
    }

    private void DefaultSceneGUI()
    {

        if (!system.Loop)
            lastPoint = Vector3.zero;

        Handles.color = system.controlStyle;
        var evt = Event.current;


        for (int i = 0; i < system.GetPathPointsCount(); ++i)
        {

            if (editorState == EditorState.DELETE && i == deleteIndex)
                Handles.Label(system.GetPointByIndex(i) + Vector3.up * 1.2f, "Вы хотите удалить эту точку? \n Для подтверждения нажмите CTRL \n [" + i + "] Wait Time:" + system.GetTimeByIndex(i), EditorStyles.textArea);
            else
            if (system.ShowTooltips)
            {
                {
                    Handles.Label(system.GetPointByIndex(i) + Vector3.up * 1.2f, "[" + i + "] Wait Time:" + system.GetTimeByIndex(i),EditorStyles.textField);
                }
            }

            Vector3 screenPosition = Handles.matrix.MultiplyPoint(system.GetPointByIndex(i));
            //Если нажата мышь и редактор еще не в режиме удаления
            if (!evt.shift && evt.button == 1 && editorState != EditorState.DELETE && editorState != EditorState.NEWPOINT)
            {
                //Отрисовываем кнопки чтобы мышь смогла их выловить
                HandleUtility.AddControl(i, HandleUtility.DistanceToCircle(screenPosition, system.controlSize));
                //Если мышь попадает по существующему хэндлу
                //Переключаем редактор в режим удаления и заносим индекс выбранного обьекта для удаления
                if (HandleUtility.nearestControl == i && HandleUtility.nearestControl > 0)
                {
                    deleteIndex = HandleUtility.nearestControl;
                    editorState = EditorState.DELETE;
                    return;
                }
            }
            //Если редактор уже в режиме удаления и была нажата клавиша подтверждения удаления
            else if (evt.control && evt.button == 0 && deleteIndex < system.GetPathPointsCount() && deleteIndex >= 0)
            {
                //Удаляем к хуям поинт по айдишнику и сбрасываем его
                editorState = EditorState.DEFAULT;
                EditorUtility.SetDirty(system);
                system.RemovePointByIndex(deleteIndex);
                deleteIndex = -1;
                return;
            }
            else if (evt.control && deleteIndex < system.GetPathPointsCount() && deleteIndex >= 0)
            {
                editorState = EditorState.DEFAULT;
                deleteIndex = -1;
                return;
            }

            //отрисовываем нормальный хэндл
            system.SetPointByIndex(i, DrawHandleWithLine(system.GetPointByIndex(i)));

        }
    }

    public override void OnInspectorGUI()
    {
        if (system == null)
            return;

        if (editorState == EditorState.DEFAULT)
        {
            DefaultInspectorGUI();
        }
        else if (editorState == EditorState.DELETE)
        {

            DeleteInspectorGUI();
        }

        if (GUI.changed)
        {
            EditorUtility.SetDirty(system);
        }

    }

    private void DeleteInspectorGUI()
    {
        if (GUILayout.Button("Отмена"))
        {
            editorState = EditorState.DEFAULT;
        }
        EditorGUILayout.HelpBox("Вы хотите удалить Точку:" + deleteIndex + " \n(Wait Time:" + system.GetTimeByIndex(deleteIndex) + ")" + "\nчтобы удалить нажмите CTRL подтверждения действия.", MessageType.Warning);
    }


    private void DefaultInspectorGUI()
    {
        system.controlSize = EditorGUILayout.Slider("UI Size", system.controlSize,0.1f,3f);
        system.controlStyle = EditorGUILayout.ColorField("UI Color",system.controlStyle);
        
        system.Loop = DrawBoolField(system.Loop, "Loop Path");
        system.ShowTooltips = DrawBoolField(system.ShowTooltips, "Show Tooltips");

        for (int i = 0; i < system.GetPathPointsCount(); ++i)
        {
            EditorGUILayout.BeginHorizontal();
            system.SetTimeByIndex(i, EditorGUILayout.FloatField("Way Point" + i,system.GetTimeByIndex(i)));
            if (GUILayout.Button("Remove"))
            {
                if (EditorUtility.DisplayDialog("Вы точно хотите удалить...", "Вы уверены что хотите удалить Точку" + i + "?", "Да, удалить \"Enter\"", "Отмена"))
                {
                    system.RemovePointByIndex(i);
                }
            }
            EditorGUILayout.EndHorizontal();
        }
        EditorGUILayout.HelpBox("Для добавления новой Точки зажмите Shift и любую другую клавишу для подтверждения.", MessageType.Info);
        EditorGUILayout.HelpBox("Чтобы удалить Точку нажмите ПРАВОЙ кнопкой мыши на нём в сцене и нажми CTRL для подтверждения.", MessageType.Info);

    }

    void ShowWayPointUnderMouse()
    {
        DrawHandleWithLine(mousePosition);
        Handles.CircleCap(0, mousePosition, Quaternion.Euler(0, 0, 0), system.controlSize);

    }

    public Vector3 GetPointInWorldSpace()
    {
        return mousePosition;
    }
    public static bool DrawBoolField(bool value, string text)
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField(text);
        bool temp = EditorGUILayout.ToggleLeft("", value);
        EditorGUILayout.EndHorizontal();
        return temp;
    }

    public Vector3 DrawHandleWithLine(Vector3 target)
    {
        if (lastPoint != Vector3.zero)
        {
            Handles.DrawDottedLine(lastPoint, target, system.controlSize);
        }

        lastPoint = target;

        return Handles.FreeMoveHandle(target, Quaternion.identity, system.controlSize, Vector3.zero, Handles.SphereCap);
    }

    void OnDisable()
    {
        Tools.current = lastTool;
    }
}