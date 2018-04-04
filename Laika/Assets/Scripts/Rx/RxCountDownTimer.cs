using System;
using UniRx;

public class RxCountDownTimer
{
    private readonly IConnectableObservable<int> _countDownObservable;
    /// <summary>
    /// カウントダウンストリーム
    /// このObservableを各クラスがSubscribeする
    /// </summary>
    public IObservable<int> CountDownObservable{get {return _countDownObservable.AsObservable();}}

    public RxCountDownTimer(int CountTime)
    {
        //カウントのストリームを作成
        //PublishでHot変換
        _countDownObservable = CreateCountDownObservable(CountTime).Publish();
        _countDownObservable.Connect();
    }

    /// <summary>
    /// CountTimeだけカウントダウンするストリーム
    /// </summary>
    /// <param name="CountTime"></param>
    /// <returns></returns>
    private IObservable<int> CreateCountDownObservable(int CountTime)
    {
        return Observable
            .Timer(TimeSpan.FromSeconds(0), TimeSpan.FromSeconds(1))
            .Select(x => (int)(CountTime - x))
            .TakeWhile(x => x > 0);
    }
}
