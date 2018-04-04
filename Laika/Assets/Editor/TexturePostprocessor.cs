
using UnityEditor;
using UnityEngine;
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using STLExtensiton;

public class TexturePostprocessor : AssetPostprocessor
{

    #region     variable

    //対象のディレクトリへのパス
    private static readonly string[] SPRITE_DIRECTORY_PATH = new string[] {
        "Assets/LocalResources/Images/Single",
        "Assets/LocalResources/Images/Repeat",
        "Assets/LocalResources/Images/Multiple"
    };

    private static readonly Dictionary<string, Action<TextureImporter>> SpriteType = new Dictionary<string, Action<TextureImporter>>
    {
       { "Single", ConverteSingleSprite },
       { "Repeat", ConverteSingleRepeatSprite },
       { "Multiple", ConverteMultipleSprite}
    };
    #endregion  variable

    #region     ConvertIvent

    static void ConverteSingleRepeatSprite(TextureImporter textureImporter)
    {
        textureImporter.spriteImportMode = SpriteImportMode.Single;
        textureImporter.wrapMode = TextureWrapMode.Repeat;
        textureImporter.filterMode = FilterMode.Point;
    }
    static void ConverteSingleSprite(TextureImporter textureImporter)
    {
        textureImporter.spriteImportMode = SpriteImportMode.Single;
    }

    static void ConverteMultipleSprite(TextureImporter textureImporter)
    {
        textureImporter.spriteImportMode = SpriteImportMode.Multiple;
    }
    #endregion  ConvertIvent

    //ファイルが編集または追加されたら呼ばれる
    private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {
        //対象のディレクトリ内で移動動したら再インポートして、OnPreprocessTextureが呼ばれるように
        movedAssets.ForEach(path =>
        {
            if (SPRITE_DIRECTORY_PATH.Any(p => path.Contains(p)))
                AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);
        });
    }

    //Textureファイルのインポート設定 Textureファイルがインポートされる直前に呼び出される
    private void OnPreprocessTexture()
    {
        //インポート時のTextureファイルを設定するクラス
        TextureImporter textureImporter = assetImporter as TextureImporter;

        //対象のディレクトリ以外はスルー
        if (!SPRITE_DIRECTORY_PATH.Any(p => textureImporter.assetPath.Contains(p)))
            return;

        Converte(textureImporter);
    }

    void Converte(TextureImporter textureImporter)
    {
        // 指定のどのディレクトリか判断
        string directoryName = null;

        SpriteType.ForEach(sprite => 
        {
            bool isContains = Path.GetFileName(Path.GetDirectoryName(textureImporter.assetPath)).Contains(sprite.Key);
            if(isContains)
            {
                directoryName = sprite.Key;
                return;
            }
        });
        Path.GetFileName(Path.GetDirectoryName(textureImporter.assetPath));

        //  共通のテクスチャの設定
        textureImporter.textureType = TextureImporterType.Sprite; //テクスチャタイプをSpriteに
        //textureImporter.spritePackingTag = directoryName;              //タグをディレクトリ名に設定
        textureImporter.spritePixelsPerUnit = 1;              //Pixels Per Unitを変更

        var method = SpriteType.TryGetValueEx(directoryName, (TextureImporter t)=> { Debug.LogError(directoryName + " : key not found"); });
        method?.Invoke(textureImporter);

    }

}
