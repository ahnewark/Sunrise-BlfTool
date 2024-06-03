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
using SunriseBlfTool.BlfChunks;
using static SunriseBlfTool.BlfChunks.ServiceRecordIdentity;

namespace SunriseBlfTool
{
    public class MultiplayerPlayers : IBLFChunk
    {
        public int unk1;
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

            unk1 = hoppersStream.Read<int>(32);


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

                player.machineIndex = hoppersStream.Read<byte>(8);
                player.playerIdentifier = hoppersStream.Read<ulong>(64);

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
                hoppersStream.SeekRelative(1);
                player.foregroundEmblem = hoppersStream.Read<byte>(8);
                player.backgroundEmblem = hoppersStream.Read<byte>(8);
                player.emblemFlags = hoppersStream.Read<byte>(8);
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

                player.unk18 = hoppersStream.Read<byte>(8);
                player.unk19 = hoppersStream.Read<byte>(8);
                player.unk20 = hoppersStream.Read<byte>(8);
                player.unk21 = hoppersStream.Read<byte>(8);
                player.unk22 = hoppersStream.Read<byte>(8);
                player.unk23 = hoppersStream.Read<int>(32);
                player.unk23 = hoppersStream.Read<int>(32);
                player.unk25 = hoppersStream.Read<int>(32);
                player.unk26 = hoppersStream.Read<int>(32);
                player.unk27 = hoppersStream.Read<int>(32);
                player.unk28 = hoppersStream.Read<int>(32);
                player.unk29 = hoppersStream.Read<byte>(8);
                player.unk30 = hoppersStream.Read<byte>(8);
                player.unk31 = hoppersStream.Read<byte>(8);
                player.unk32 = hoppersStream.Read<byte>(8);
                player.unk33 = hoppersStream.Read<int>(32);
                player.unk34 = hoppersStream.Read<int>(32);
                player.unk35 = hoppersStream.Read<int>(32);
                player.unk36 = hoppersStream.Read<byte>(8);
                player.unk37 = hoppersStream.Read<byte>(8);
                player.unk38 = hoppersStream.Read<byte>(8);
                player.unk39 = hoppersStream.Read<byte>(8);
                player.unk40 = hoppersStream.Read<int>(32);
                player.unk41 = hoppersStream.Read<int>(32);
                player.unk42 = hoppersStream.Read<int>(32);
                player.unk43 = hoppersStream.Read<int>(32);
                player.unk44 = hoppersStream.Read<int>(32);
                player.unk45 = hoppersStream.Read<int>(32);
                player.unk46 = hoppersStream.Read<int>(32);
                player.unk47 = hoppersStream.Read<int>(32);
                player.unk48 = hoppersStream.Read<ulong>(64);
                player.unk49 = hoppersStream.Read<byte>(8);
                player.unk50 = hoppersStream.Read<byte>(8);
                player.unk51 = hoppersStream.Read<byte>(8);
                player.unk52 = hoppersStream.Read<byte>(8);
                player.unk53 = hoppersStream.Read<byte>(8);
                player.unk54 = hoppersStream.Read<byte>(8);
                player.unk55 = hoppersStream.Read<byte>(8);
                player.unk56 = hoppersStream.Read<byte>(8);
                player.unk57 = hoppersStream.Read<byte>(8);
                player.unk58 = hoppersStream.Read<byte>(8);
                player.unk59 = hoppersStream.Read<byte>(8);
                player.unk60 = hoppersStream.Read<byte>(8);
                player.unk61 = hoppersStream.Read<int>(32);
                player.unk62 = hoppersStream.Read<int>(32);
                player.unk63 = hoppersStream.Read<int>(32);
                player.unk64 = hoppersStream.Read<int>(32);

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

                player.unk1 = hoppersStream.Read<int>(32);
                player.unk2 = hoppersStream.Read<int>(32);
                player.unk3 = hoppersStream.Read<byte>(8);
                player.unk4 = hoppersStream.Read<byte>(8);
                player.unk5 = hoppersStream.Read<byte>(8);
                player.unk6 = hoppersStream.Read<byte>(8);

                player.globalEXP = hoppersStream.Read<int>(32);
                player.rank = (Rank)hoppersStream.Read<int>(32) - 1;
                player.grade = (Grade)hoppersStream.Read<int>(32);

                player.unk7 = hoppersStream.Read<byte>(8);
                player.unk8 = hoppersStream.Read<byte>(8);
                player.unk9 = hoppersStream.Read<byte>(8);
                player.unk10 = hoppersStream.Read<byte>(8);
                player.unk11 = hoppersStream.Read<int>(32);
                player.unk12 = hoppersStream.Read<int>(32);
                player.unk13 = hoppersStream.Read<int>(32);
                player.unk14 = hoppersStream.Read<byte>(8);
                player.unk15 = hoppersStream.Read<byte>(8);
                player.unk16 = hoppersStream.Read<byte>(8);
                player.unk17 = hoppersStream.Read<byte>(8);

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
            public byte machineIndex;
            public byte machineIdentifier;
            [JsonConverter(typeof(XUIDConverter))]
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
            public byte unk18;
            public byte unk19;
            public byte unk20;
            public byte unk21;
            public byte unk22;
            public int unk23;
            public int unk24;
            public int unk25;
            public int unk26;
            public int unk27;
            public int unk28;
            public byte unk29;
            public byte unk30;
            public byte unk31;
            public byte unk32;
            public int unk33;
            public int unk34;
            public int unk35;
            public byte unk36;
            public byte unk37;
            public byte unk38;
            public byte unk39;
            public int unk40;
            public int unk41;
            public int unk42;
            public int unk43;
            public int unk44;
            public int unk45;
            public int unk46;
            public int unk47;
            [JsonConverter(typeof(XUIDConverter))]
            public ulong unk48;
            public byte unk49;
            public byte unk50;
            public byte unk51;
            public byte unk52;
            public byte unk53;
            public byte unk54;
            public byte unk55;
            public byte unk56;
            public byte unk57;
            public byte unk58;
            public byte unk59;
            public byte unk60;
            public int unk61;
            public int unk62;
            public int unk63;
            public int unk64;
            public string playerNameHost;
            public int unk1;
            public int unk2;
            public byte unk3;
            public byte unk4;
            public byte unk5;
            public byte unk6;
            public int globalEXP;
            [JsonConverter(typeof(StringEnumConverter))]
            public Rank rank;
            [JsonConverter(typeof(StringEnumConverter))]
            public Grade grade;
            public byte unk7;
            public byte unk8;
            public byte unk9;
            public byte unk10;
            public int unk11;
            public int unk12;
            public int unk13;
            public byte unk14;
            public byte unk15;
            public byte unk16;
            public byte unk17;


        }
    }
}
