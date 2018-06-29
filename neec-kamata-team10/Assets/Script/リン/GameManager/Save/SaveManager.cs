//------------------------------------------------------
// 作成日：2018.6.25
// 作成者：林 佳叡
// 内容：プレイヤーデータを管理するクラス
//------------------------------------------------------
using System.IO;

public class SaveManager
{
    private string path = "Assets/Save/";
    private string fileName = "sv.dat";

    /// <summary>
    /// 保存
    /// </summary>
    public void Save(UnlockManager unlockManager)
    {
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        if (File.Exists(path + fileName))                       //Dataある場合
        {
            SaveData(unlockManager, FileMode.Open);
            return;
        }
        SaveData(unlockManager, FileMode.CreateNew);
    }

    /// <summary>
    /// 上書き
    /// </summary>
    private void SaveData(UnlockManager unlockManager, FileMode mode)
    {
        FileStream fs = new FileStream(path + fileName, mode);
        StreamWriter sw = new StreamWriter(fs);

        int stageCount = (int)EScene.StageNull;                 //stage数
        for (int i = 1; i < stageCount; ++i)
        {
            string saveString = unlockManager.SaveString(i);    //Saveの文字列を取得
            sw.WriteLine(saveString);                           //Save
        }

        sw.Close();                                             //終了処理
        fs.Close();                                             //終了処理
    }

    /// <summary>
    /// ロード
    /// </summary>
    public void Load(UnlockManager unlockManager)
    {
        if (!File.Exists(path + fileName))                      //存在しなかったら戻る
            return;

        FileStream fs = new FileStream(path + fileName, FileMode.Open);
        StreamReader sr = new StreamReader(fs);

        while (!sr.EndOfStream)
        {
            unlockManager.Load(sr.ReadLine());                  //ロード処理
        }

        sr.Close();                                             //終了処理
        fs.Close();                                             //終了処理
    }
}
