

public static class Config
{
    public static string Surrounding = "Surrounding";

    // Animation trigger name
    public static string Run = "Run";
    public static string Turn = "Turn";
    public static string Dead = "Dead";
    public static string Jump = "Jump";

    // Tag
    public static string TPlayer = "Player";
    public static string TCoin = "Coin";
    public static string TBarrier = "Barrier";
    public static string TBomb = "Bomb";
    public static string TBlood = "Blood";
    public static string TStatic = "Static";
    public static string TPawn = "Pawn";
    public static string TTutorail = "Tutorial";
    public static string TAI = "AI";
    public static string TTipJump = "TipJump";

    // Prefab path
    //public static string GoodsPath = "Goods/";

    // Table text title
    public static string TableGoods = "GOODS";
    public static string TableItem = "ITEM";
    public static string TableBuilding = "BUILDING";
    public static string TableMapIndex = "MAPINDEX";

    // Back ground music
    public static string BgM0 = "BgM0";
    public static string BgM1 = "BgM1";
    public static string BgM2 = "BgM2";
    public static string BgM3 = "BgM3";
    public static string MBomb = "Bomb";
    public static string MBenefit = "Benefit";
    public static string MCoin = "Coin";
    public static string MHit = "Hit";
    public static string MJump = "Jump";
    public static string MHurt = "Hurt";
    public static string MHitZombie = "HitZombie";



    // Chinese 
    public static string CDistance
    {
        get
        {
            if (LanguageSelector.Instacne.CurrentSystemLanguage == UnityEngine.SystemLanguage.Chinese)
            {
                return "距离：";
            }
            else if (LanguageSelector.Instacne.CurrentSystemLanguage == UnityEngine.SystemLanguage.English)
            {
                return "Distance：";
            }
            else if (LanguageSelector.Instacne.CurrentSystemLanguage == UnityEngine.SystemLanguage.Korean)
            {
                return "거리.：";
            }

            return "Distance：";
        }
    }

    public static string CScore
    {
        get
        {
            if (LanguageSelector.Instacne.CurrentSystemLanguage == UnityEngine.SystemLanguage.Chinese)
            {
                return "分数：";
            }
            else if (LanguageSelector.Instacne.CurrentSystemLanguage == UnityEngine.SystemLanguage.English)
            {
                return "Score：";
            }
            else if (LanguageSelector.Instacne.CurrentSystemLanguage == UnityEngine.SystemLanguage.Korean)
            {
                return "점수.：";
            }

            return "Score：";
        }
    }

    public static string CPoint
    {
        get
        {
            if (LanguageSelector.Instacne.CurrentSystemLanguage == UnityEngine.SystemLanguage.Chinese)
            {
                return "分";
            }
            else if (LanguageSelector.Instacne.CurrentSystemLanguage == UnityEngine.SystemLanguage.English)
            {
                return "PTS";
            }
            else if (LanguageSelector.Instacne.CurrentSystemLanguage == UnityEngine.SystemLanguage.Korean)
            {
                return "점";
            }

            return "PTS";
        }
    }

    public static string CMeter
    {
        get
        {
            if (LanguageSelector.Instacne.CurrentSystemLanguage == UnityEngine.SystemLanguage.Chinese)
            {
                return "米";
            }
            else if (LanguageSelector.Instacne.CurrentSystemLanguage == UnityEngine.SystemLanguage.English)
            {
                return "M";
            }
            else if (LanguageSelector.Instacne.CurrentSystemLanguage == UnityEngine.SystemLanguage.Korean)
            {
                return "쌀";
            }

            return "M";
        }
    }


    public static string WRankPlayer
    {
        get
        {
            if (LanguageSelector.Instacne.CurrentSystemLanguage == UnityEngine.SystemLanguage.Chinese)
            {
                return "你的排名：";
            }
            else if (LanguageSelector.Instacne.CurrentSystemLanguage == UnityEngine.SystemLanguage.English)
            {
                return "Your Ranking:";
            }
            else if (LanguageSelector.Instacne.CurrentSystemLanguage == UnityEngine.SystemLanguage.Korean)
            {
                return "너 의 순위:";
            }

            return "你的排名：";
        }
    }


    // Particular name
    public static string PGainScore = "GainScore";
    public static string PHurt = "Hurt";
    public static string PRecover = "Recover";
    public static string PBomb = "Bomb";
    public static string PInvincible = "Invincible";
    public static string PMagnet = "Magnet";
    public static string PHitZombie = "HitZombie";


    // Item effect
    public static float ESpeedUpPercent = 2.0f;
    public static float ESpeedUpLastSecond = 10.0f;

    // Tutorial
    public static string TTRTurnLeft = "TurnLeft";
    public static string TTRTurnRight = "TurnRight";
    public static string TTRJump = "Jump";
    public static string TTRWatching = "Watching";

    // Mask
    public static string MPlayer = "Player";
    public static string MGround = "Ground";

    
}