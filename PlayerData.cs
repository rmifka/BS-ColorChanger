using System;
using System.Collections.Generic;


namespace BeatSaber
{
    public class PlayerData
    {
        public string version { get; set; }
        public Player[] localPlayers { get; set; }
        public Player[] guestPlayers { get; set; }

    }
    public class Player
    {
        public string playerId { get; set; }
        public string playerName { get; set; }
        public bool shouldShowTutorialPrompt { get; set; }
        public bool shouldShow360Warning { get; set; }
        public bool agreedToEula { get; set; }
        public bool didSelectLanguage { get; set; }
        public bool agreedToMultiplayerDisclaimer { get; set; }
        public bool avatarCreated { get; set; }
        public PlayerAgreements playerAgreements { get; set; }
        public int lastSelectedBeatmapDifficulty { get; set; }
        public string lastSelectedBeatmapCharacteristicName { get; set; }
        public GameplayModifiers gameplayModifiers { get; set; }
        public PlayerSpecificSettings playerSpecificSettings { get; set; }
        public PracticeSettings practiceSettings { get; set; } 
        public PlayerAllOverallStatsData playerAllOverallStatsData {get; set; }
        public List<LevelsStatsData> levelsStatsData { get; set; }
        public List<MissionsStatsData> missionsStatsData { get; set; }
        public string[] showedMissionHelpIds { get; set; }
        public ColorSchemesSettings colorSchemesSettings { get; set; }
        public OverrideEnvironmentSettings overrideEnvironmentSettings { get; set; }
        public string[] favoritesLevelIds { get; set; }
        public MultiplayerModeSettings multiplayerModeSettings { get; set; }
        public int currentDlcPromoDisplayCount { get; set; }
        public string currentDlcPromoId { get; set; }
    }
    public class PlayerAgreements
    {
        public int eulaVersion { get; set; }
        public int privacyPolicyVersion { get; set; }
        public int healthAndSafetyVersion { get; set; }
    }
    public class GameplayModifiers
    {
        public int energyType { get; set; }
        public bool instaFail { get; set; }
        public bool failOnSaberClash { get; set; }
        public int enabledObstacleType { get; set; }
        public bool fastNotes { get; set; }
        public bool strictAngles { get; set; }
        public bool disappearingArrows { get; set; }
        public bool ghostNotes { get; set; }
        public bool noBombs { get; set; }
        public int songSpeed { get; set; }
        public bool noArrows { get; set; }
        public bool noFailOn0Energy { get; set; }
        public bool proMode { get; set; }
        public bool zenMode { get; set; }
        public bool smallCubes { get; set; }

    }
    public class PlayerSpecificSettings
    {
        public bool staticLights { get; set; }
        public bool leftHanded { get; set; }
        public double playerHeight { get; set; }
        public bool automaticPlayerHeight { get; set; }
        public double sfxVolume { get; set; }
        public bool reduceDebris { get; set; }
        public bool noTextsAndHuds { get; set; }
        public bool advancedHud { get; set; }
        public float saberTrailIntensity { get; set; }
        public int _noteJumpDurationTypeSettingsSaveData { get; set; }
        public float noteJumpFixedDuration { get; set; }
        public bool autoRestart { get; set; }
        public bool noFailEffects { get; set; }
        public float noteJumpBeatOffset { get; set; }
        public bool hideNoteSpawnEffect { get; set; }
        public bool adaptiveSfx { get; set; }
        public int environmentEffectsFilterDefaultPreset { get; set; }
        public int environmentEffectsFilterExpertPlusPreset { get; set; }

    }
    public class PracticeSettings
    {
        public double startSongTime { get; set; }
        public float songSpeedMul { get; set; }
    }
    public class PlayerAllOverallStatsData
    {
        public StatsData campaignOverallStatsData { get; set; }
        public StatsData soloFreePlayOverallStatsData { get; set; }
        public StatsData partyFreePlayOverallStatsData { get; set; }
        public StatsData onlinePlayOverallStatsData { get; set; }
    }
    public class StatsData
    {
        public int goodCutsCount { get; set; }
        public int badCutsCount { get; set; }
        public int missedCutsCount { get; set; }
        public int totalScore { get; set; }
        public int playedLevelsCount { get; set; }
        public int cleardLevelsCount { get; set; }
        public int failedLevelsCount { get; set; }
        public int fullComboCount { get; set; }
        public double timePlayed { get;set; }
        public int handDistanceTravelled { get; set; }
        public int cummulativeCutScoreWithoutMultiplier { get; set; }
    }
    public class LevelsStatsData
    {
        public string levelId { get; set; }
        public int difficulty { get; set; }
        public string beatmapCharacteristicName { get; set; }
        public int highScore { get; set; }
        public int maxCombo { get; set; }
        public bool fullCombo { get; set; }
        public int maxRank { get; set; }
        public bool validScore { get; set; }
        public int playCount { get; set; }
    }
    public class MissionsStatsData
    {
        public string missionId { get; set; }
        public bool cleared { get; set; }
    }
    public class ColorSchemesSettings
    {
        public bool overrideDefaultColors { get; set; }
        public string selectedColorSchemeId { get; set; }
        public List<Colorscheme> colorSchemes { get; set; } 
    }
    public class Colorscheme
    {
        public string colorSchemeId { get; set; }
        public Color saberAColor { get; set; }
        public Color saberBColor { get; set; }
        public Color environmentColor0 { get; set; }
        public Color environmentColor1 { get; set; }
        public Color obstaclesColor { get; set; }
    }
    public class Color
    {
        public double r { get; set; }
        public double g { get; set; }
        public double b { get; set; }
        public double a { get; set; }
    }
    public class OverrideEnvironmentSettings
    {
        public bool overrideEnvironments { get; set; }
        public string overrideNormalEnvironmentName { get; set; }
        public string override360EnvironmentName { get; set; }

    }
    public class MultiplayerModeSettings
    {
        public int createServerNumberOfPlayers { get; set; }
        public string quickPlayDifficulty { get; set; }
        public QuickPlaySongPackMask[] quickPlaySongPackMask { get; set; }
        public string quickPlaySongPackMaskSerializedName { get; set; }
        public bool quickPlayEnableLevelSelection { get; set; }
    }
    public class QuickPlaySongPackMask
    {

    }
}
