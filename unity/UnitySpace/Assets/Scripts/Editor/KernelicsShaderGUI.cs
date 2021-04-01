using UnityEngine;
using UnityEditor;

public class KernelicsShaderGUI : ShaderGUI
{

    MaterialEditor _editor;
    MaterialProperty[] _properties;

    public override void OnGUI(MaterialEditor editor, MaterialProperty[] properties)
    {
        _editor = editor;
        _properties = properties;
        DoMain();
        DoAdvanced();
    }

    void DoMain()
    {
        GUILayout.Label("Main Maps", EditorStyles.boldLabel);

        MaterialProperty mainTex = FindProperty("_MainTex");
        _editor.TextureScaleOffsetProperty(mainTex);
    }

    void DoAdvanced()
    {
        GUILayout.Label("Advanced Options", EditorStyles.boldLabel);

        _editor.EnableInstancingField();
    }

    MaterialProperty FindProperty(string name)
    {
        return FindProperty(name, _properties);
    }

}