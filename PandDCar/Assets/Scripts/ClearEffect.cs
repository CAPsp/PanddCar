﻿using UnityEngine;

        // スコアを通知
        GetComponentInChildren<HighScoreControl>().CheckScore( timerControl_.GetRemainingSec() );