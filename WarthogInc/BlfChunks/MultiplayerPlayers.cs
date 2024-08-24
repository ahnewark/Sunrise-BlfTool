using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Sewer56.BitStream;
using Sewer56.BitStream.ByteStreams;
using SunriseBlfTool.BlfChunks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunriseBlfTool
{
    public class MultiplayerPlayers : IBLFChunk
    {
        public int unknown;
        public MultiplayerPlayer[] players;

        public ushort GetAuthentication()
        {
            return 1;
        }

        public uint GetLength()
        {
            return 0x11E4;
        }

        public string GetName()
        {
            return "mppl";
        }

        public ushort GetVersion()
        {
            return 2;
        }

        public void ReadChunk(ref BitStream<StreamByteStream> hoppersStream, BLFChunkReader reader)
        {
            byte playerCount = 16;
            byte validPlayerCount = 0;

            unknown = hoppersStream.Read<int>(32);

            players = new MultiplayerPlayer[playerCount];
            for (int i = 0; i < playerCount; i++)
            {
                MultiplayerPlayer player = new MultiplayerPlayer();

                bool playerExists = hoppersStream.Read<byte>(8) > 0;

                if (!playerExists)
                {
                    hoppersStream.SeekRelative(0x11D);
                    continue;
                }

                validPlayerCount++;

                player.machineIndex = hoppersStream.Read<byte>(8);
                player.playerIdentifier = hoppersStream.Read<ulong>(64);

                // Read Player Configuration from Client
                LinkedList<byte> nameBytes = new LinkedList<byte>();
                for (int si = 0; si < 16; si++)
                {
                    byte left = hoppersStream.Read<byte>(8);
                    byte right = hoppersStream.Read<byte>(8);
                    if (((left == 0 && right == 0) || si == 16) && player.playerNameClient == null)
                    {
                        player.playerNameClient = Encoding.BigEndianUnicode.GetString(nameBytes.ToArray());
                    }
                    nameBytes.AddLast(left);
                    nameBytes.AddLast(right);
                }

                player.appearanceFlags = hoppersStream.Read<byte>(8);
                player.primaryColor = (Color)hoppersStream.Read<byte>(8);
                player.secondaryColor = (Color)hoppersStream.Read<byte>(8);
                player.tertiaryColor = (Color)hoppersStream.Read<byte>(8);
                player.playerModelChoice = (PlayerModel)hoppersStream.Read<byte>(8);
                hoppersStream.SeekRelative(1);
                player.foregroundEmblem = hoppersStream.Read<byte>(8);
                player.backgroundEmblem = hoppersStream.Read<byte>(8);
                player.emblemFlags = hoppersStream.Read<byte>(8);
                player.emblemPrimaryColor = (Color)hoppersStream.Read<byte>(8);
                player.emblemSecondaryColor = (Color)hoppersStream.Read<byte>(8);
                player.emblemBackgroundColor = (Color)hoppersStream.Read<byte>(8);
                hoppersStream.SeekRelative(2);
                player.spartanHelmet = (SpartanHelmet)hoppersStream.Read<byte>(8);
                player.spartanLeftShoulder = (SpartanShoulder)hoppersStream.Read<byte>(8);
                player.spartanRightShoulder = (SpartanShoulder)hoppersStream.Read<byte>(8);
                player.spartanBody = (SpartanBody)hoppersStream.Read<byte>(8);
                player.eliteHelmet = (EliteArmour)hoppersStream.Read<byte>(8);
                player.eliteLeftShoulder = (EliteArmour)hoppersStream.Read<byte>(8);
                player.eliteRightShoulder = (EliteArmour)hoppersStream.Read<byte>(8);
                player.eliteBody = (EliteArmour)hoppersStream.Read<byte>(8);

                LinkedList<byte> serviceTagBytes = new LinkedList<byte>();
                for (int si = 0; si < 4; si++)
                {
                    byte left = hoppersStream.Read<byte>(8);
                    byte right = hoppersStream.Read<byte>(8);
                    if (((left == 0 && right == 0) || si == 4) && player.serviceTag == null)
                    {
                        player.serviceTag = Encoding.BigEndianUnicode.GetString(serviceTagBytes.ToArray());
                    }
                    serviceTagBytes.AddLast(left);
                    serviceTagBytes.AddLast(right);
                }
                hoppersStream.SeekRelative(2);

                player.xuid = hoppersStream.Read<ulong>(64);

                player.isSilverOrGoldLive = hoppersStream.Read<byte>(8) > 0;
                player.isOnlineEnabled = hoppersStream.Read<byte>(8) > 0;
                player.userSelectedTeamIndex = hoppersStream.Read<byte>(8);
                player.desiresVeto = hoppersStream.Read<byte>(8) > 0;
                player.desiresRematch = hoppersStream.Read<byte>(8) > 0;
                player.hopperAccessFlags = hoppersStream.Read<byte>(8);
                player.isFreeLiveGoldAccount = hoppersStream.Read<byte>(8) > 0;
                player.hasBetaPermissions = hoppersStream.Read<byte>(8) > 0;
                player.player_is_griefer = hoppersStream.Read<byte>(8) > 0;
                player.bungienetUserRole = hoppersStream.Read<byte>(8);
                player.gamerRegion = hoppersStream.Read<ushort>(16);
                player.gamerZone = hoppersStream.Read<byte>(8);
                hoppersStream.SeekRelative(3);
                player.campaignPercentage = hoppersStream.Read<uint>(32);
                player.campaignCompletion = hoppersStream.Read<uint>(32);
                player.cheatFlags = hoppersStream.Read<uint>(32);
                player.banFlags = hoppersStream.Read<uint>(32);
                player.repeatedPlayCoefficient = hoppersStream.Read<int>(32);
                player.experienceGrowthBanned = hoppersStream.Read<uint>(32);

                // Global Statistics
                player.queriedPlayerGlobalStatistics = new QueriedPlayerGlobalStatistics();
                player.queriedPlayerGlobalStatistics.valid = hoppersStream.Read<byte>(8) > 0;
                hoppersStream.SeekRelative(3);
                player.queriedPlayerGlobalStatistics.experienceBase = hoppersStream.Read<uint>(32);
                player.queriedPlayerGlobalStatistics.experiencePenalty = hoppersStream.Read<uint>(32);
                player.queriedPlayerGlobalStatistics.highestSkill = hoppersStream.Read<uint>(32);


                // Displayed Statistics
                player.queriedPlayerDisplayedStatistics = new QueriedPlayerDisplayedStatistics();
                hoppersStream.SeekRelative(3);
                player.queriedPlayerDisplayedStatistics.statsValid = hoppersStream.Read<byte>(8) > 0;
                player.queriedPlayerDisplayedStatistics.rankedPlayed = hoppersStream.Read<uint>(32);
                player.queriedPlayerDisplayedStatistics.rankedCompleted = hoppersStream.Read<uint>(32);
                player.queriedPlayerDisplayedStatistics.rankedWin = hoppersStream.Read<uint>(32);
                player.queriedPlayerDisplayedStatistics.unrankedPlayed = hoppersStream.Read<uint>(32);
                player.queriedPlayerDisplayedStatistics.unrankedCompleted = hoppersStream.Read<uint>(32);
                player.queriedPlayerDisplayedStatistics.unrankedWin = hoppersStream.Read<uint>(32);
                player.queriedPlayerDisplayedStatistics.customCompleted = hoppersStream.Read<uint>(32);
                player.queriedPlayerDisplayedStatistics.customWin = hoppersStream.Read<uint>(32);
                player.queriedPlayerDisplayedStatistics.firstPlayed = hoppersStream.Read<uint>(32);
                player.queriedPlayerDisplayedStatistics.lastPlayed = hoppersStream.Read<uint>(32);

                // Hopper (Statistics)
                player.queriedPlayerHopperStatistics = new QueriedPlayerHopperStatistics();
                player.queriedPlayerHopperStatistics.statsValid = hoppersStream.Read<byte>(8) > 0;
                hoppersStream.SeekRelative(1);
                player.queriedPlayerHopperStatistics.identifier = hoppersStream.Read<ushort>(16);
                player.queriedPlayerHopperStatistics.mu = hoppersStream.Read<uint>(32);
                player.queriedPlayerHopperStatistics.sigma = hoppersStream.Read<uint>(32);
                player.queriedPlayerHopperStatistics.hopperSkill = hoppersStream.Read<uint>(32);
                player.queriedPlayerHopperStatistics.gamesPlayed = hoppersStream.Read<uint>(32);
                player.queriedPlayerHopperStatistics.gamesCompleted = hoppersStream.Read<uint>(32);
                player.queriedPlayerHopperStatistics.gamesWon = hoppersStream.Read<uint>(32);

                // Player Configuration from Host
                nameBytes = new LinkedList<byte>();
                for (int si = 0; si < 16; si++)
                {
                    byte left = hoppersStream.Read<byte>(8);
                    byte right = hoppersStream.Read<byte>(8);
                    if (((left == 0 && right == 0) || si == 16) && player.playerNameHost == null)
                    {
                        player.playerNameHost = Encoding.BigEndianUnicode.GetString(nameBytes.ToArray());
                    }
                    nameBytes.AddLast(left);
                    nameBytes.AddLast(right);
                }

                player.playerTeam = hoppersStream.Read<int>(32);
                player.playerAssignedTeam = hoppersStream.Read<int>(32);
                player.globalStatsValid = hoppersStream.Read<byte>(8) > 0;
                hoppersStream.SeekRelative(3);
                player.globalExperience = hoppersStream.Read<int>(32);
                player.globalRank = hoppersStream.Read<int>(32);
                player.globalGrade = hoppersStream.Read<int>(32);
                player.hopperStatsValid = hoppersStream.Read<byte>(8) > 0;
                hoppersStream.SeekRelative(3);
                player.hopperSkillDisplay = hoppersStream.Read<int>(32);
                player.hopperSkill = hoppersStream.Read<int>(32);
                player.hopperWeight = hoppersStream.Read<int>(32);
                player.player_standing = hoppersStream.Read<byte>(8);
                hoppersStream.SeekRelative(1);
                player.player_score = hoppersStream.Read<ushort>(16);

                players[i] = player;
            }
            hoppersStream.Seek(hoppersStream.NextByteIndex, 0);

            Array.Resize(ref players, validPlayerCount);
        }

        public void WriteChunk(ref BitStream<StreamByteStream> hoppersStream)
        {
            throw new NotImplementedException();
        }
    }

    public class MultiplayerPlayer
    {
        public byte machineIndex;
        public ulong playerIdentifier;
        public string playerNameClient;
        public string playerNameHost;
        public byte appearanceFlags;
        [JsonConverter(typeof(StringEnumConverter))]
        public Color primaryColor;
        [JsonConverter(typeof(StringEnumConverter))]
        public Color secondaryColor;
        [JsonConverter(typeof(StringEnumConverter))]
        public Color tertiaryColor;
        [JsonConverter(typeof(StringEnumConverter))]
        public PlayerModel playerModelChoice;
        public byte foregroundEmblem;
        public byte backgroundEmblem;
        public byte emblemFlags;
        [JsonConverter(typeof(StringEnumConverter))]
        public Color emblemPrimaryColor;
        [JsonConverter(typeof(StringEnumConverter))]
        public Color emblemSecondaryColor;
        [JsonConverter(typeof(StringEnumConverter))]
        public Color emblemBackgroundColor;
        [JsonConverter(typeof(StringEnumConverter))]
        public SpartanHelmet spartanHelmet;
        [JsonConverter(typeof(StringEnumConverter))]
        public SpartanShoulder spartanLeftShoulder;
        [JsonConverter(typeof(StringEnumConverter))]
        public SpartanShoulder spartanRightShoulder;
        [JsonConverter(typeof(StringEnumConverter))]
        public SpartanBody spartanBody;
        [JsonConverter(typeof(StringEnumConverter))]
        public EliteArmour eliteHelmet;
        [JsonConverter(typeof(StringEnumConverter))]
        public EliteArmour eliteLeftShoulder;
        [JsonConverter(typeof(StringEnumConverter))]
        public EliteArmour eliteRightShoulder;
        [JsonConverter(typeof(StringEnumConverter))]
        public EliteArmour eliteBody;
        public string serviceTag;
        [JsonConverter(typeof(XUIDConverter))]
        public ulong xuid;
        public bool isSilverOrGoldLive;
        public bool isOnlineEnabled;
        public byte userSelectedTeamIndex;
        public bool desiresVeto;
        public bool desiresRematch;
        public byte hopperAccessFlags;
        public bool isFreeLiveGoldAccount;
        public bool hasBetaPermissions;
        public bool player_is_griefer;
        public byte bungienetUserRole;
        public uint campaignCompletion;
        public uint campaignPercentage;
        public ushort gamerRegion;
        public byte gamerZone;
        public uint cheatFlags;
        public uint banFlags;
        public int repeatedPlayCoefficient;
        public uint experienceGrowthBanned;

        // Added attributes as per the new structure
        public QueriedPlayerGlobalStatistics queriedPlayerGlobalStatistics;
        public QueriedPlayerDisplayedStatistics queriedPlayerDisplayedStatistics;
        public QueriedPlayerHopperStatistics queriedPlayerHopperStatistics;

        public int playerTeam;
        public int playerAssignedTeam;

        public bool globalStatsValid;
        public int globalExperience;
        public int globalRank;
        public int globalGrade;

        public bool hopperStatsValid;
        public int hopperSkill;
        public int hopperSkillDisplay;
        public int hopperWeight;

        public byte player_standing;
        public ushort player_score;
    }

    public class QueriedPlayerGlobalStatistics
    {
        public bool valid;
        public uint experienceBase;
        public uint experiencePenalty;
        public uint highestSkill;
    }

    public class QueriedPlayerDisplayedStatistics
    {
        public bool statsValid;
        public uint rankedPlayed;
        public uint rankedCompleted;
        public uint rankedWin;
        public uint unrankedPlayed;
        public uint unrankedCompleted;
        public uint unrankedWin;
        public uint customCompleted;
        public uint customWin;
        public uint firstPlayed;
        public uint lastPlayed;
    }

    public class QueriedPlayerHopperStatistics
    {
        public bool statsValid;
        public ushort identifier;
        public uint mu;
        public uint sigma;
        public uint hopperSkill;
        public uint gamesPlayed;
        public uint gamesCompleted;
        public uint gamesWon;
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum Color : byte
    {
        STEEL,
        SILVER,
        WHITE,
        RED,
        MAUVE,
        SALMON,
        ORANGE,
        CORAL,
        PEACH,
        GOLD,
        YELLOW,
        PALE,
        SAGE,
        GREEN,
        OLIVE,
        TEAL,
        AQUA,
        CYAN,
        BLUE,
        COBALT,
        SAPPHIRE,
        VIOLET,
        ORCHID,
        LAVENDER,
        CRIMSON,
        RUBY_WINE,
        PINK,
        BROWN,
        RAN,
        KHAKI
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum PlayerModel
    {
        Spartan,
        Elite
    }


    public enum SpartanHelmet : byte
    {
        DEFAULT,
        COBRA,
        INTRUDER,
        NINJA, // recon
        REGULATOR,
        RYU, // hyabusa
        MARATHON,
        SCOUT,
        ODST,
        MARKV,
        ROGUE,
    }

    public enum SpartanShoulder : byte
    {
        DEFAULT,
        COBRA,
        INTRUDER,
        NINJA, // recon
        REGULATOR,
        RYU, // hyabusa
        MARATHON,
        SCOUT
    }

    public enum SpartanBody : byte
    {
        DEFAULT,
        COBRA,
        INTRUDER,
        NINJA, // recon
        RYU, // hyabusa
        REGULATOR,
        SCOUT,
        KATANA,
        BUNGIE
    }

    public enum EliteArmour : byte
    {
        DEFAULT,
        PREDATOR,
        RAPTOR,
        BLADES,
        SCYTHE,
    }
}
