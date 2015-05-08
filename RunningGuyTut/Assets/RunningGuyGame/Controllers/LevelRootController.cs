using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;


public class LevelRootController : LevelRootControllerBase {
    
    public override void InitializeLevelRoot(LevelRootViewModel levelRoot)
    {
        levelRoot._CoinsProperty
            .Where(c => c.Action == NotifyColllectionChangedAction.Add)
            .Select(c => c.NewItem[0] as CoinViewModel)
            .Subscribe(coin => CoinAdded(levelRoot, coin));
    }

    private void CoinAdded(LevelRootViewModel levelRoot, CoinViewModel coin)
    {
        coin.PickUp.Subscribe(_ =>
        {
            levelRoot.Coins.Remove(coin);
        });
    }
}
