using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Sewer56.BitStream;
using Sewer56.BitStream.ByteStreams;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarthogInc.BlfChunks;
using static WarthogInc.BlfChunks.ServiceRecordIdentity;

namespace Sunrise.BlfTool
{
    public class MultiplayerPlayers : IBLFChunk
    {
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

        public void ReadChunk(ref BitStream<StreamByteStream> hoppersStream)
        {
            byte playerCount = 16;

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Warning: mmpl chunk definition is incomplete.");
            Console.ResetColor();

            players = new MultiplayerPlayer[playerCount];
            for (int i = 0; i < playerCount; i++)
            {

                MultiplayerPlayer player = new MultiplayerPlayer();
                hoppersStream.SeekRelative(0x4);

                bool playerExists = hoppersStream.Read<byte>(8) > 0;
                if (!playerExists)
                {
                    hoppersStream.SeekRelative(0x11e - 5);
                    continue;
                }

                hoppersStream.SeekRelative(0xE - 5);
                //player.playerExists = hoppersStream.Read<byte>(8) > 0;
                //player.machineIdentifier = hoppersStream.Read<byte>(8);
                //player.playerIdentifier = hoppersStream.Read<ulong>(64);

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

                player.femaleVoice = hoppersStream.Read<byte>(8);
                player.primaryColor = (Color)hoppersStream.Read<byte>(8);
                player.secondaryColor = (Color)hoppersStream.Read<byte>(8);
                player.tertiaryColor = (Color)hoppersStream.Read<byte>(8);
                player.isElite = (PlayerModel)hoppersStream.Read<byte>(8);
                player.foregroundEmblem = hoppersStream.Read<byte>(8);
                player.backgroundEmblem = hoppersStream.Read<byte>(8);
                player.emblemFlags = hoppersStream.Read<byte>(8);
                hoppersStream.SeekRelative(1);
                player.emblemPrimaryColor = (Color)hoppersStream.Read<byte>(8);
                player.emblemSecondaryColor = (Color)hoppersStream.Read<byte>(8);
                player.emblemBackgroundColor = (Color)hoppersStream.Read<byte>(8);
                hoppersStream.SeekRelative(2);
                player.spartanHelmet = (SpartanHelmet)hoppersStream.Read<byte>(8);
                player.spartanLeftShounder = (SpartanShoulder)hoppersStream.Read<byte>(8);
                player.spartanRightShoulder = (SpartanShoulder)hoppersStream.Read<byte>(8);
                player.spartanBody = (SpartanBody)hoppersStream.Read<byte>(8);
                player.eliteHelmet = (EliteArmour)hoppersStream.Read<byte>(8);
                player.eliteLeftShoulder = (EliteArmour)hoppersStream.Read<byte>(8);
                player.eliteRightShoulder = (EliteArmour)hoppersStream.Read<byte>(8);
                player.eliteBody = (EliteArmour)hoppersStream.Read<byte>(8);

                LinkedList<byte> serviceTagBytes = new LinkedList<byte>();
                for (int si = 0; si < 5; si++)
                {
                    byte left = hoppersStream.Read<byte>(8);
                    byte right = hoppersStream.Read<byte>(8);
                    if (((left == 0 && right == 0) || si == 5) && player.serviceTag == null)
                    {
                        player.serviceTag = Encoding.BigEndianUnicode.GetString(serviceTagBytes.ToArray());
                    }
                    serviceTagBytes.AddLast(left);
                    serviceTagBytes.AddLast(right);
                }

                player.xuid = hoppersStream.Read<ulong>(64);

                player.isSilverOrGoldLive = hoppersStream.Read<byte>(8) > 0;
                player.isOnlineEnabled = hoppersStream.Read<byte>(8) > 0;
                player.isControllerAttached = hoppersStream.Read<byte>(8) > 0;
                player.playerLastTeam = hoppersStream.Read<byte>(8);
                player.desiresVeto = hoppersStream.Read<byte>(8) > 0;
                player.desiresRematch = hoppersStream.Read<byte>(8) > 0;
                player.hopperAccessFlags = hoppersStream.Read<byte>(8);
                player.isFreeLiveGoldAccount = hoppersStream.Read<byte>(8) > 0;
                player.isUserCreatedContentAllowed = hoppersStream.Read<byte>(8) > 0;
                player.isFriendCreatedContentAllowed = hoppersStream.Read<byte>(8) > 0;
                player.isGriefer = hoppersStream.Read<byte>(8) > 0;

                hoppersStream.SeekRelative(0x23 + 0x50 + 2);


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

                hoppersStream.SeekRelative(0xC);

                player.globalEXP = hoppersStream.Read<int>(32);
                player.rank = (Rank)hoppersStream.Read<int>(32) - 1;
                player.grade = (Grade)hoppersStream.Read<int>(32);


                hoppersStream.SeekRelative(0x10);

                players[i] = player;
            }
            hoppersStream.Seek(hoppersStream.NextByteIndex, 0);
        }

        public void WriteChunk(ref BitStream<StreamByteStream> hoppersStream)
        {
            throw new NotImplementedException();
        }

        public class MultiplayerPlayer
        {
            public bool playerExists;
            public byte machineIdentifier;
            public ulong playerIdentifier;
            public string playerNameClient; // wide, 16 chars
            public byte femaleVoice; // includes gender i think
            [JsonConverter(typeof(StringEnumConverter))]
            public Color primaryColor;
            [JsonConverter(typeof(StringEnumConverter))]
            public Color secondaryColor;
            [JsonConverter(typeof(StringEnumConverter))]
            public Color tertiaryColor;
            [JsonConverter(typeof(StringEnumConverter))]
            public PlayerModel isElite;
            public byte foregroundEmblem;
            public byte backgroundEmblem;
            public byte emblemFlags; // Whether the secondary layer is shown or not.
            [JsonConverter(typeof(StringEnumConverter))]
            public Color emblemPrimaryColor;
            [JsonConverter(typeof(StringEnumConverter))]
            public Color emblemSecondaryColor;
            [JsonConverter(typeof(StringEnumConverter))]
            public Color emblemBackgroundColor;
            [JsonConverter(typeof(StringEnumConverter))]
            public SpartanHelmet spartanHelmet;
            [JsonConverter(typeof(StringEnumConverter))]
            public SpartanShoulder spartanLeftShounder;
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
            public string serviceTag; // wide, 5 characters long for some reason
            [JsonConverter(typeof(XUIDConverter))]
            public ulong xuid;
            public bool isSilverOrGoldLive;
            public bool isOnlineEnabled;
            public bool isControllerAttached;
            public byte playerLastTeam;
            public bool desiresVeto;
            public bool desiresRematch;
            public byte hopperAccessFlags;
            public bool isFreeLiveGoldAccount;
            public bool isUserCreatedContentAllowed;
            public bool isFriendCreatedContentAllowed;
            public bool isGriefer;
            public string playerNameHost;
            public int globalEXP;
            [JsonConverter(typeof(StringEnumConverter))]
            public Rank rank;
            [JsonConverter(typeof(StringEnumConverter))]
            public Grade grade;


        }
    }
}
