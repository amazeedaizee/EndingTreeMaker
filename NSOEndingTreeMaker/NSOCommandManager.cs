using ngov3;
using System;

namespace NSOEndingTreeMaker
{
    [Serializable]
    public class CommandAction
    {
        public string id = "";
        public string name = "";
        public int followers;
        public int stress;
        public int affection;
        public int darkness;
        public int impact;
        public int experience;
        public int streamstreak;
        public int prebonus;
        public int gamer;
        public int movie;
        public int communication;
        public int tinfoil;
        public int daypart;

        public CommandAction() { }
        public CommandAction(string id, string name, int followers, int stress, int affection, int darkness, int impact, int experience, int streamstreak, int prebonus, int gamer, int movie, int communication, int tinfoil, int daypart)
        {
            this.id = id;
            this.name = name;
            this.followers = followers;
            this.stress = stress;
            this.affection = affection;
            this.darkness = darkness;
            this.impact = impact;
            this.experience = experience;
            this.streamstreak = streamstreak;
            this.prebonus = prebonus;
            this.gamer = gamer;
            this.movie = movie;
            this.communication = communication;
            this.tinfoil = tinfoil;
            this.daypart = daypart;
        }
    }
    public class NSOCommandManager
    {

        public static CommandAction hangoutPlay = new CommandAction("Hang Out", "Play Game", 0, -4, 2, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1);
        public static CommandAction hangoutTalk = new CommandAction("Hang Out: Ame", "Spend Time Together", 0, -3, 2, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1);
        public static CommandAction hangoutCuddle = new CommandAction("Hang Out: Ame", "Cuddle", 0, -6, 4, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1);
        public static CommandAction hangoutPity = new CommandAction("Hang Out: Ame", "Pity Party", 0, -10, 6, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1);
        public static CommandAction hangoutLove = new CommandAction("Hang Out: ***", "***", 0, -12, 15, -6, 0, 1, 0, 0, 0, 0, 0, 0, 2);
        public static CommandAction hangoutHighLove = new CommandAction("Hang Out: ***", "Have Chem***", 0, -25, 20, 6, 1, 1, 0, 0, 0, 0, 0, 0, 2);

        public static CommandAction sleepDusk = new CommandAction("Sleep", "Sleep Until Dusk", 0, -3, 0, -1, 0, 0, 0, 0, 0, 0, 0, 0, 1);
        public static CommandAction sleepNightOne = new CommandAction("Sleep", "Sleep Until Night", 0, -9, 0, -2, 0, 0, 0, 0, 0, 0, 0, 0, 2);
        public static CommandAction sleepNightTwo = new CommandAction("Sleep", "Sleep Until Night", 0, -3, 0, -1, 0, 0, 0, 0, 0, 0, 0, 0, 1);
        public static CommandAction sleepTomOne = new CommandAction("Sleep", "Sleep Until Tomorrow", 0, -18, 0, -4, 0, 0, 0, 0, 0, 0, 0, 0, 3);
        public static CommandAction sleepTomTwo = new CommandAction("Sleep", "Sleep Until Tomorrow", 0, -9, 0, -2, 0, 0, 0, 0, 0, 0, 0, 0, 2);
        public static CommandAction sleepTomThree = new CommandAction("Sleep", "Sleep Until Tomorrow", 0, -6, 0, -2, 0, 0, 0, 0, 0, 0, 0, 0, 1);

        public static CommandAction depazNormal = new CommandAction("Medication", "Prescription (recommended dose)", 0, -1, 0, -1, 0, 0, 0, 0, 0, 0, 0, 0, 1);
        public static CommandAction depazODOne = new CommandAction("Medication", "Prescription GO!", 0, -12, 0, 6, 1, 0, 0, 0, 0, 0, 0, 0, 2);
        public static CommandAction depazODTwo = new CommandAction("Medication", "Prescription GO!", 0, -4, 0, 6, 1, 0, 0, 0, 0, 0, 0, 0, 2);
        public static CommandAction depazODThree = new CommandAction("Medication", "Prescription GO!", 0, -2, 0, 6, 1, 0, 0, 0, 0, 0, 0, 0, 2);
        public static CommandAction depazODFour = new CommandAction("Medication", "Prescription GO!", 0, -1, 0, 6, 1, 0, 0, 0, 0, 0, 0, 0, 2);

        public static CommandAction dylsemNormal = new CommandAction("Medication", "OTC (recommended dose)", 0, -1, 0, -1, 0, 0, 0, 0, 0, 0, 0, 0, 1);
        public static CommandAction dylsemODOne = new CommandAction("Medication", "OTC GO!", 0, -12, 0, 6, 1, 0, 0, 0, 0, 0, 0, 0, 2);
        public static CommandAction dylsemODTwo = new CommandAction("Medication", "OTC GO!", 0, -4, 0, 6, 1, 0, 0, 0, 0, 0, 0, 0, 2);
        public static CommandAction dylsemODThree = new CommandAction("Medication", "OTC GO!", 0, -2, 0, 6, 1, 0, 0, 0, 0, 0, 0, 0, 2);

        public static CommandAction embianNormal = new CommandAction("Medication", "Sleeping Pills (recommended dose)!", 0, -1, 0, -1, 0, 0, 0, 0, 0, 0, 0, 0, 1);
        public static CommandAction embianODOne = new CommandAction("Medication", "Sleeping Pills GO!", 0, -12, 0, 6, 1, 0, 0, 0, 0, 0, 0, 0, 2);
        public static CommandAction embianODTwo = new CommandAction("Medication", "Sleeping Pills GO!", 0, -4, 0, 6, 1, 0, 0, 0, 0, 0, 0, 0, 2);

        public static CommandAction weedOne = new CommandAction("Medication", "Magic Grass", 0, -18, 0, 8, 1, 0, 0, 0, 0, 0, 0, 0, 2);
        public static CommandAction weedTwo = new CommandAction("Medication", "Magic Grass", 0, -12, 0, 8, 1, 0, 0, 0, 0, 0, 0, 0, 2);

        public static CommandAction paperOne = new CommandAction("Medication", "Magic Paper", 0, -18, 0, 8, 1, 0, 0, 0, 0, 0, 0, 0, 2);

        public static CommandAction tweetOne = new CommandAction("Internet: Social Media", "Daily Tweet", 1, 2, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1);
        public static CommandAction tweetTwo = new CommandAction("Internet: Social Media", "Business Tweet", 2, 5, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1);
        public static CommandAction tweetThree = new CommandAction("Internet: Social Media", "Muse", 5, 2, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1);

        public static CommandAction venttweetOne = new CommandAction("Internet: Social Media", "Vent on Main", 0, -4, 0, 3, 0, 0, 0, 1, 0, 0, 0, 0, 1);
        public static CommandAction venttweetTwo = new CommandAction("Internet: Social Media", "Vent on Priv", 0, -5, 0, 3, 0, 0, 0, 0, 0, 0, 0, 0, 1);
        public static CommandAction venttweetThree = new CommandAction("Internet: Social Media", "Bash Others", 0, -8, 0, 5, 0, 0, 0, 0, 0, 0, 0, 0, 1);

        public static CommandAction vanitySearch = new CommandAction("Internet: Vanity Search", "Vanity Search", 0, 0, -2, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1);

        public static CommandAction videoWatch = new CommandAction("Internet: Video Streaming", "Video Streaming", 0, -4, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1);

        public static CommandAction anonOne = new CommandAction("Internet: /st/", "Search Opinions", 0, 2, -2, 2, 0, 0, 0, 0, 0, 0, 0, 0, 1);
        public static CommandAction anonTwo = new CommandAction("Internet: /st/", "Go Undercover", 0, 2, -2, 4, 0, 0, 0, 0, 0, 0, 0, 0, 1);
        public static CommandAction anonThree = new CommandAction("Internet: /st/", "Stir Shit", 0, -2, -2, 6, 0, 0, 0, 0, 0, 0, 0, 0, 1);

        public static CommandAction dinderDate = new CommandAction("Internet: Dinder", "Dinder", 0, -8, -10, 4, 0, 1, 0, 0, 0, 0, 1, 0, 2);

        public static CommandAction goOutBase = new CommandAction("Go Out", "", 0, -10, 6, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2);
        public static CommandAction goOutHospital = new CommandAction("Go Out", "Hospital", 0, -10, 6, -10, 0, 0, 0, 0, 0, 0, 0, 0, 2);
        public static CommandAction goOutGalaxy = new CommandAction("Go Out", "Galactic Rail", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);

        public static CommandAction darknessOne = new CommandAction("Darkness", "Cut Wrists", 0, -15, -20, 10, 0, 0, 0, 0, 0, 0, 0, 0, 2);
        public static CommandAction darknessTwo = new CommandAction("Darkness", "Go Berserk", 0, -20, -35, 10, 0, 0, 0, 0, 0, 0, 0, 0, 2);

        public static CommandAction streamOne = new CommandAction("First Streams", "FirstStream", 1000, 5, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0);

        public static CommandAction streamChatOne = new CommandAction("Stream", "Chat & Chill 1", 1, 20, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1);
        public static CommandAction streamChatTwo = new CommandAction("Stream", "Chat & Chill 2", 4, 20, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1);
        public static CommandAction streamChatThree = new CommandAction("Stream", "Chat & Chill 3", 8, 20, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1);
        public static CommandAction streamChatFour = new CommandAction("Stream", "Chat & Chill 4", 16, 20, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1);
        public static CommandAction streamChatFive = new CommandAction("Stream", "Chat & Chill 5", 48, 20, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1);

        public static CommandAction streamGameOne = new CommandAction("Stream", "Letsplay 1", 3, 18, 0, 0, 0, 0, 1, 0, 1, 0, 1, 0, 1);
        public static CommandAction streamGameTwo = new CommandAction("Stream", "Letsplay 2", 6, 18, 0, 0, 0, 0, 1, 0, 1, 0, 1, 0, 1);
        public static CommandAction streamGameThree = new CommandAction("Stream", "Letsplay 3", 9, 18, 0, 0, 0, 0, 1, 0, 1, 0, 1, 0, 1);
        public static CommandAction streamGameFour = new CommandAction("Stream", "Letsplay 4", 20, 18, 0, 0, 0, 0, 1, 0, 1, 0, 1, 0, 1);
        public static CommandAction streamGameFive = new CommandAction("Stream", "Letsplay 5", 40, 18, 0, 0, 0, 0, 1, 0, 1, 0, 1, 0, 1);

        public static CommandAction streamNerdOne = new CommandAction("Stream", "Nerd Talk 1", 2, 14, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1);
        public static CommandAction streamNerdTwo = new CommandAction("Stream", "Nerd Talk 2", 6, 14, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1);
        public static CommandAction streamNerdThree = new CommandAction("Stream", "Nerd Talk 3", 12, 14, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1);
        public static CommandAction streamNerdFour = new CommandAction("Stream", "Nerd Talk 4", 18, 14, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1);
        public static CommandAction streamNerdFive = new CommandAction("Stream", "Nerd Talk 5", 30, 14, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1);

        public static CommandAction streamAdOne = new CommandAction("Stream", "Sponsorship 1", 10, 18, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1);
        public static CommandAction streamAdTwo = new CommandAction("Stream", "Sponsorship 2", 15, 18, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1);
        public static CommandAction streamAdThree = new CommandAction("Stream", "Sponsorship 3", 20, 18, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1);
        public static CommandAction streamAdFour = new CommandAction("Stream", "Sponsorship 4", 25, 18, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1);
        public static CommandAction streamAdFive = new CommandAction("Stream", "Sponsorship 5", 30, 18, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1);

        public static CommandAction streamConspireOne = new CommandAction("Stream", "Conspiracy Theories 1", 25, 20, 0, 8, 0, 0, 1, 0, 0, 0, 1, 1, 1);
        public static CommandAction streamConspireTwo = new CommandAction("Stream", "Conspiracy Theories 2", 25, 20, 0, 8, 0, 0, 1, 0, 0, 0, 1, 4, 1);
        public static CommandAction streamConspireThree = new CommandAction("Stream", "Conspiracy Theories 3", 25, 20, 0, 8, 0, 0, 1, 0, 0, 0, 1, 8, 1);
        public static CommandAction streamConspireFour = new CommandAction("Stream", "Conspiracy Theories 4", 25, 20, 0, 8, 0, 0, 1, 0, 0, 0, 1, 20, 1);
        public static CommandAction streamConspireFive = new CommandAction("Stream", "Conspiracy Theories 5", 25, 20, 0, 8, 0, 0, 1, 0, 0, 0, 1, 40, 1);

        public static CommandAction streamNetloreOne = new CommandAction("Stream", "Netlore 1", 3, 20, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1);
        public static CommandAction streamNetloreTwo = new CommandAction("Stream", "Netlore 2", 6, 20, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1);
        public static CommandAction streamNetloreThree = new CommandAction("Stream", "Netlore 3", 9, 20, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1);
        public static CommandAction streamNetloreFour = new CommandAction("Stream", "Netlore 4", 20, 20, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1);
        public static CommandAction streamNetloreFive = new CommandAction("Stream", "Netlore 5", 40, 20, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1);

        public static CommandAction streamASMROne = new CommandAction("Stream", "ASMR 1", 10, 24, 0, 1, 0, 0, 1, 0, 0, 0, 1, 0, 1);
        public static CommandAction streamASMRTwo = new CommandAction("Stream", "ASMR 2", 14, 24, 0, 1, 0, 0, 1, 0, 0, 0, 1, 0, 1);
        public static CommandAction streamASMRThree = new CommandAction("Stream", "ASMR 3", 20, 24, -4, 4, 0, 0, 1, 0, 0, 0, 1, 0, 1);
        public static CommandAction streamASMRFour = new CommandAction("Stream", "ASMR 4", 30, 24, -8, 4, 0, 0, 1, 0, 0, 0, 1, 0, 1);
        public static CommandAction streamASMRFive = new CommandAction("Stream", "ASMR 5", 40, 24, -12, 8, 0, 0, 1, 0, 0, 0, 1, 0, 1);

        public static CommandAction streamSexyOne = new CommandAction("Stream", "Sexy Stream 1", 30, 28, -15, 8, 0, 0, 1, 0, 0, 0, 1, 0, 1);
        public static CommandAction streamSexyTwo = new CommandAction("Stream", "Sexy Stream 2", 32, 28, -15, 8, 0, 0, 1, 0, 0, 0, 1, 0, 1);
        public static CommandAction streamSexyThree = new CommandAction("Stream", "Sexy Stream 3", 35, 28, -15, 8, 0, 0, 1, 0, 0, 0, 1, 0, 1);
        public static CommandAction streamSexyFour = new CommandAction("Stream", "Sexy Stream 4", 40, 28, -15, 8, 0, 0, 1, 0, 0, 0, 1, 0, 1);
        public static CommandAction streamSexyFive = new CommandAction("Stream", "Sexy Stream 5", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1);

        public static CommandAction streamPodcastOne = new CommandAction("Stream", "Angel Explain 1", 2, 14, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1);
        public static CommandAction streamPodcastTwo = new CommandAction("Stream", "Angel Explain 2", 4, 14, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1);
        public static CommandAction streamPodcastThree = new CommandAction("Stream", "Angel Explain 3", 8, 14, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1);
        public static CommandAction streamPodcastFour = new CommandAction("Stream", "Angel Explain 4", 12, 14, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1);
        public static CommandAction streamPodcastFive = new CommandAction("Stream", "Angel Explain 5", 30, 14, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1);

        public static CommandAction streamStuffOne = new CommandAction("Stream", "KAngel Tries Stuff 1", 4, 18, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1);
        public static CommandAction streamStuffTwo = new CommandAction("Stream", "KAngel Tries Stuff 2", 8, 18, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1);
        public static CommandAction streamStuffThree = new CommandAction("Stream", "KAngel Tries Stuff 3", 12, 18, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1);
        public static CommandAction streamStuffFour = new CommandAction("Stream", "KAngel Tries Stuff 4", 16, 18, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1);
        public static CommandAction streamStuffFive = new CommandAction("Stream", "KAngel Tries Stuff 5", 40, 18, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1);

        public static CommandAction streamBreakOne = new CommandAction("Stream", "Breakdown Stream 1", 20, 22, 0, 8, 1, 0, 1, 0, 0, 0, 1, 0, 1);
        public static CommandAction streamBreakTwo = new CommandAction("Stream", "Breakdown Stream 2", 25, 22, 0, 8, 1, 0, 1, 0, 0, 0, 1, 0, 1);
        public static CommandAction streamBreakThree = new CommandAction("Stream", "Breakdown Stream 3", 40, 22, 0, 8, 1, 0, 1, 0, 0, 0, 1, 0, 1);
        public static CommandAction streamBreakFour = new CommandAction("Stream", "Breakdown Stream 4", 30, 22, 0, 8, 1, 0, 1, 0, 0, 0, 1, 0, 1);
        public static CommandAction streamBreakFive = new CommandAction("Stream", "Breakdown Stream 5", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1);

        public static CommandAction streamAngelOne = new CommandAction("Stream", "Internet Angel 1", 25, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1);
        public static CommandAction streamAngelTwo = new CommandAction("Stream", "Internet Angel 2", 25, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1);
        public static CommandAction streamAngelThree = new CommandAction("Stream", "Internet Angel 3", 25, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1);
        public static CommandAction streamAngelFour = new CommandAction("Stream", "Internet Angel 4", 25, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1);
        public static CommandAction streamAngelFive = new CommandAction("Stream", "Internet Angel 5", 50, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1);
        public static CommandAction streamAngelSix = new CommandAction("Stream", "Internet Angel 5?", 50, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1);

        public static CommandAction streamDarkOne = new CommandAction("Stream", "Darkness 1", 10, 0, 0, 10, 0, 0, 1, 0, 0, 0, 0, 0, 1);
        public static CommandAction streamDarkTwo = new CommandAction("Stream", "Darkness 2", 10, 0, 0, 10, 0, 0, 1, 0, 0, 0, 0, 0, 1);

        public static CommandAction CmdTypeToCommand(CmdType cmd)
        {
            switch (cmd)
            {
                case CmdType.Error:
                    return new CommandAction("Stream", "", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1);
                case CmdType.Zatudan_1:
                    return streamChatOne;
                case CmdType.Zatudan_2:
                    return streamChatTwo;
                case CmdType.Zatudan_3:
                    return streamChatThree;
                case CmdType.Zatudan_4:
                    return streamChatFour;
                case CmdType.Zatudan_5:
                    return streamChatFive;
                case CmdType.Gamejikkyou_1:
                    return streamGameOne;
                case CmdType.Gamejikkyou_2:
                    return streamGameTwo;
                case CmdType.Gamejikkyou_3:
                    return streamGameThree;
                case CmdType.Gamejikkyou_4:
                    return streamGameFour;
                case CmdType.Gamejikkyou_5:
                    return streamGameFive;
                case CmdType.Otakutalk_1:
                    return streamNerdOne;
                case CmdType.Otakutalk_2:
                    return streamNerdTwo;
                case CmdType.Otakutalk_3:
                    return streamNerdThree;
                case CmdType.Otakutalk_4:
                    return streamNerdFour;
                case CmdType.Otakutalk_5:
                    return streamNerdFive;
                case CmdType.PR_1:
                    return streamAdOne;
                case CmdType.PR_2:
                    return streamAdTwo;
                case CmdType.PR_3:
                    return streamAdThree;
                case CmdType.PR_4:
                    return streamAdFour;
                case CmdType.PR_5:
                    return streamAdFive;
                case CmdType.Imbouron_1:
                    return streamConspireOne;
                case CmdType.Imbouron_2:
                    return streamConspireTwo;
                case CmdType.Imbouron_3:
                    return streamConspireThree;
                case CmdType.Imbouron_4:
                    return streamConspireFour;
                case CmdType.Imbouron_5:
                    return streamConspireFive;
                case CmdType.Kaidan_1:
                    return streamNetloreOne;
                case CmdType.Kaidan_2:
                    return streamNetloreTwo;
                case CmdType.Kaidan_3:
                    return streamNetloreThree;
                case CmdType.Kaidan_4:
                    return streamNetloreFour;
                case CmdType.Kaidan_5:
                    return streamNetloreFive;
                case CmdType.ASMR_1:
                    return streamASMROne;
                case CmdType.ASMR_2:
                    return streamASMRTwo;
                case CmdType.ASMR_3:
                    return streamASMRThree;
                case CmdType.ASMR_4:
                    return streamASMRFour;
                case CmdType.ASMR_5:
                    return streamASMRFive;
                case CmdType.Hnahaisin_1:
                    return streamSexyOne;
                case CmdType.Hnahaisin_2:
                    return streamSexyTwo;
                case CmdType.Hnahaisin_3:
                    return streamSexyThree;
                case CmdType.Hnahaisin_4:
                    return streamSexyFour;
                case CmdType.Hnahaisin_5:
                    return streamSexyFive;
                case CmdType.Kaisetu_1:
                    return streamPodcastOne;
                case CmdType.Kaisetu_2:
                    return streamPodcastTwo;
                case CmdType.Kaisetu_3:
                    return streamPodcastThree;
                case CmdType.Kaisetu_4:
                    return streamPodcastFour;
                case CmdType.Kaisetu_5:
                    return streamPodcastFive;
                case CmdType.Taiken_1:
                    return streamStuffOne;
                case CmdType.Taiken_2:
                    return streamStuffTwo;
                case CmdType.Taiken_3:
                    return streamStuffThree;
                case CmdType.Taiken_4:
                    return streamStuffFour;
                case CmdType.Taiken_5:
                    return streamStuffFive;
                case CmdType.Yamihaishin_1:
                    return streamBreakOne;
                case CmdType.Yamihaishin_2:
                    return streamBreakTwo;
                case CmdType.Yamihaishin_3:
                    return streamBreakThree;
                case CmdType.Yamihaishin_4:
                    return streamBreakFour;
                case CmdType.Yamihaishin_5:
                    return streamBreakFive;
                case CmdType.Angel_1:
                    return streamAngelOne;
                case CmdType.Angel_2:
                    return streamAngelTwo;
                case CmdType.Angel_3:
                    return streamAngelThree;
                case CmdType.Angel_4:
                    return streamAngelFour;
                case CmdType.Angel_5:
                    return streamAngelFive;
                case CmdType.Angel_6:
                    return streamAngelSix;
                case CmdType.Darkness_1:
                    return streamDarkOne;
                case CmdType.Darkness_2:
                    return streamDarkTwo;
                case CmdType.EntameGame:
                    return hangoutPlay;
                case CmdType.PlayIchatukuTalk:
                    return hangoutTalk;
                case CmdType.PlayIchatukuIchatuku:
                    return hangoutCuddle;
                case CmdType.PlayIchatukuKizu:
                    return hangoutPity;
                case CmdType.PlayMakeLove:
                    return hangoutLove;
                case CmdType.PlayKimeLove:
                    return hangoutHighLove;
                case CmdType.SleepToTwilight1:
                    return sleepDusk;
                case CmdType.SleepToNight1:
                    return sleepNightTwo;
                case CmdType.SleepToNight2:
                    return sleepNightOne;
                case CmdType.SleepToTomorrow3:
                    return sleepTomOne;
                case CmdType.SleepToTomorrow2:
                    return sleepTomTwo;
                case CmdType.SleepToTomorrow1:
                    return sleepTomThree;
                case CmdType.OkusuriDaypassModerate:
                    return depazNormal;
                case CmdType.OkusuriDaypassOverdoseY1:
                    return depazODOne;
                case CmdType.OkusuriDaypassOverdoseY2:
                    return depazODTwo;
                case CmdType.OkusuriDaypassOverdoseY3:
                    return depazODThree;
                case CmdType.OkusuriDaypassOverdoseY4:
                case CmdType.OkusuriDaypassOverdoseY5:
                    return depazODFour;
                case CmdType.OkusuriPuronModerate:
                    return dylsemNormal;
                case CmdType.OkusuriPuronOverdoseY2:
                    return dylsemODOne;
                case CmdType.OkusuriPuronOverdoseY3:
                    return dylsemODTwo;
                case CmdType.OkusuriPuronOverdoseY4:
                case CmdType.OkusuriPuronOverdoseY5:
                    return dylsemODThree;
                case CmdType.OkusuriHipuronModerate:
                    return embianNormal;
                case CmdType.OkusuriHiPuronOverdoseY3:
                    return embianODOne;
                case CmdType.OkusuriHiPuronOverdoseY4:
                case CmdType.OkusuriHiPuronOverdoseY5:
                    return embianODTwo;
                case CmdType.OkusuriHappaY4:
                    return weedOne;
                case CmdType.OkusuriHappaY5:
                    return weedTwo;
                case CmdType.OkusuriPsyche:
                    return paperOne;
                case CmdType.InternetPoketterF0Y12:
                    return tweetOne;
                case CmdType.InternetPoketterF1Y12:
                    return tweetTwo;
                case CmdType.InternetPoketterPoem:
                    return tweetThree;
                case CmdType.InternetPoketterF0Y45:
                    return venttweetOne;
                case CmdType.InternetPoketterF1Y3:
                    return venttweetTwo;
                case CmdType.InternetPoketterF1Y45:
                    return venttweetThree;
                case CmdType.InternetPoketterF0Y3:
                    return vanitySearch;
                case CmdType.InternetYoutube:
                    return videoWatch;
                case CmdType.Internet2chY12:
                    return anonOne;
                case CmdType.Internet2chY3:
                    return anonTwo;
                case CmdType.Internet2chY45:
                    return anonThree;
                case CmdType.InternetDeai2:
                    return dinderDate;
                case CmdType.OdekakeHospital:
                    return goOutHospital;
                case CmdType.OdekakeGinga:
                    return goOutGalaxy;
                case CmdType.OdekakeOdaiba:
                    return new CommandAction("Music Video", "", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2);
                case CmdType.DarknessS1:
                    return darknessOne;
                case CmdType.DarknessS2:
                    return darknessTwo;
                default: break;

            }
            if (cmd.ToString().Contains("Odekake"))
            {
                return goOutBase;
            }
            return null;
        }

        public static void CalculateStats(TargetActionData pastAction, TargetActionData presentAction)
        {
            pastAction.CommandResult ??= new CommandAction();
            presentAction.CommandResult ??= new CommandAction();
            presentAction.Followers = pastAction.Followers + CalculateFollowers(pastAction, presentAction);
            presentAction.Stress = (pastAction.Stress + presentAction.CommandResult.stress) < 0 ? 0 : pastAction.Stress + presentAction.CommandResult.stress;
            presentAction.Affection = (pastAction.Affection + presentAction.CommandResult.affection) < 0 ? 0 : pastAction.Affection + presentAction.CommandResult.affection;
            presentAction.Darkness = (pastAction.Darkness + presentAction.CommandResult.darkness) < 0 ? 0 : pastAction.Darkness + presentAction.CommandResult.darkness;
            if (pastAction.TargetAction.DayIndex != presentAction.TargetAction.DayIndex && presentAction.TargetAction.Action != ActionType.InternetPoketter)
            {
                presentAction.PreAlertBonus = false;
            }
            else if (presentAction.TargetAction.Action == ActionType.InternetPoketter) { presentAction.PreAlertBonus = true; }
            else { presentAction.PreAlertBonus = pastAction.PreAlertBonus; }
            presentAction.Communication = pastAction.Communication + presentAction.CommandResult.communication;
            presentAction.Experience = pastAction.Experience + presentAction.CommandResult.experience;
            presentAction.Impact = pastAction.Impact + presentAction.CommandResult.impact;
            presentAction.GamerGirl = pastAction.GamerGirl + presentAction.CommandResult.gamer;
            presentAction.Cinephile = pastAction.Cinephile + presentAction.CommandResult.movie;
            presentAction.RabbitHole = pastAction.RabbitHole + presentAction.CommandResult.tinfoil;

        }

        public static int CalculateFollowers(TargetActionData pastAction, TargetActionData presentAction)
        {
            presentAction.StreamStreak = pastAction.StreamStreak + presentAction.CommandResult.streamstreak;
            return (int)Math.Ceiling((float)((float)(presentAction.CommandResult.followers * 1f) * Math.Min((float)(Math.Log10(pastAction.Followers) * 25f), Math.Floor(pastAction.Followers / 100.0)) * calculateStreamStreak() * calculatePreBonus() * calculateGame() * calculateMovie() * calculateImpact() * calculateExper() * calculateComm() * calculateTinfoil()));

            double calculateStreamStreak()
            {
                if (presentAction.TargetAction.Action == ActionType.Haishin)
                {
                    return (float)(presentAction.StreamStreak * 1f) + 1f;
                }
                return 1f;
            }

            double calculatePreBonus()
            {
                if (pastAction.PreAlertBonus)
                {
                    return 1.2f;
                }
                return 1f;
            }

            double calculateGame()
            {
                if (presentAction.TargetAction.Stream == AlphaType.Gamejikkyou)
                {
                    return ((float)(pastAction.GamerGirl * 1f) / 2f) + 1f;
                }
                return 1f;

            }
            double calculateMovie()
            {
                if (presentAction.TargetAction.Stream == AlphaType.Otakutalk)
                {
                    return ((float)(pastAction.Cinephile * 1f) / 2f) + 1f;
                }
                return 1f;
            }
            double calculateImpact()
            {
                if (presentAction.TargetAction.Stream == AlphaType.Yamihaishin || presentAction.TargetAction.Stream == AlphaType.Darkness)
                {
                    return ((float)(pastAction.Impact * 1f) / 2f) + 1f;
                }
                return 1f;
            }
            double calculateExper()
            {
                if (presentAction.TargetAction.Stream == AlphaType.ASMR || presentAction.TargetAction.Stream == AlphaType.Hnahaisin)
                {
                    return ((float)(pastAction.Experience * 1f) / 2f) + 1f;
                }
                return 1f;
            }
            double calculateComm()
            {
                if (presentAction.TargetAction.Action == ActionType.Haishin)
                {
                    return ((float)(pastAction.Communication * 1f) / 10f) + 1f;
                }
                return 1f;

            }
            double calculateTinfoil()
            {
                if (pastAction.RabbitHole > 0 && presentAction.TargetAction.Action == ActionType.Haishin)
                {
                    return 1f - ((float)(pastAction.RabbitHole * 1f) / 100f);
                }
                return 1f;
            }
        }


    }
}
