using System;
using System.Linq;
using UnityEngine;
using UnityEditor;
using UnityEditor.Build.Reporting;
using System.Collections.Generic;
using System.IO;
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


    
    [MenuItem("Build/BuildApk")]
    public static void BuildApk()
    {
        var outdir = System.Environment.CurrentDirectory + "/BuildOutPutPath/Android";
        var outputPath = Path.Combine(outdir, Application.productName + ".apk");
        //文件夹处理
        if (!Directory.Exists(outdir)) Directory.CreateDirectory(outdir);
        if (File.Exists(outputPath)) File.Delete(outputPath);

        //开始项目一键打包
        string[] scenes = new[] {"Assets/Scenes/SampleScene.unity"};
        UnityEditor.BuildPipeline.BuildPlayer(scenes, outputPath, BuildTarget.Android, BuildOptions.None);
        if (File.Exists(outputPath))
        {
            Debug.Log("Build Success :" + outputPath);
        }
        else
        {
            Debug.LogException(new Exception("Build Fail! Please Check the log! "));
        }
    }

}
