using System;
using System.Linq;
using UnityEngine;
using UnityEditor;
using UnityEditor.Build.Reporting;
using System.Collections.Generic;
using UnityEditor.Callbacks;

public class MobileBuild
{
    public static void TestBuild()
    {
        // ビルド対象シーンリスト
        string[] sceneList = {
            "./Assets/Scenes/SampleScene.unity",
        };


        // 実行
        BuildReport errorMessage = BuildPipeline.BuildPlayer(
            sceneList,                          //!< ビルド対象シーンリスト
            "C:/project/bin/GitHubActionsBuild.apk",   //!< 出力先
            BuildTarget.Android,      //!< ビルド対象プラットフォーム
            BuildOptions.Development            //!< ビルドオプション
        );
        PlayerSettings.SetScriptingBackend(BuildTargetGroup.Android, ScriptingImplementation.IL2CPP);

        // 結果出力
        if( !string.IsNullOrEmpty( errorMessage.ToString() ) )
            Debug.LogError( "[Error!] " + errorMessage );
        else
            Debug.Log( "[Success!]" );
    }
    

}
