using NGO;
using ngov3;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NSOEndingTreeMaker
{
    internal class NSODataManager
    {
        public static List<EndingType> EndingsList = new List<EndingType>()
        {
            EndingType.Ending_None,
            EndingType.Ending_Grand,
            EndingType.Ending_Happy,
            EndingType.Ending_Meta,
            EndingType.Ending_Normal,
            EndingType.Ending_Bad,
            EndingType.Ending_Work,
            EndingType.Ending_Needy,
            EndingType.Ending_Yami,
            EndingType.Ending_Av,
            EndingType.Ending_Healthy ,
            EndingType.Ending_Lust ,
            EndingType.Ending_Ntr ,
            EndingType.Ending_Sukisuki ,
            EndingType.Ending_Stressful ,
            EndingType.Ending_Sucide ,
            EndingType.Ending_Jine ,
            EndingType.Ending_KowaiInternet ,
            EndingType.Ending_Yarisute ,
            EndingType.Ending_Kyouso ,
            EndingType.Ending_Jikka ,
            EndingType.Ending_Ginga ,
            EndingType.Ending_DarkAngel ,
            EndingType.Ending_Ideon
        };
        public static Dictionary<EndingType, string> EndingNames = new Dictionary<EndingType, string>()
         {
             {EndingType.Ending_None, "None" },
             {EndingType.Ending_Grand, "Do You Love Me?" },
             {EndingType.Ending_Happy,"Unhappy End World" },
             {EndingType.Ending_Meta, "Rainbow Girl" },
             {EndingType.Ending_Normal, "Utopian Parody" },
             {EndingType.Ending_Bad, "Catastrophe" },
             {EndingType.Ending_Work, "Labor Is Evil" },
             {EndingType.Ending_Needy, "Needy Girl Overdose" },
             {EndingType.Ending_Yami, "Painful Future" },
             {EndingType.Ending_Av, "Fallen Angel" },
             {EndingType.Ending_Healthy, "Normie Life" },
             {EndingType.Ending_Lust, "Nymphomania" },
             {EndingType.Ending_Ntr, "Cucked" },
             {EndingType.Ending_Sukisuki, "Ground Control To Psychoelectric Angel" },
             {EndingType.Ending_Stressful, "Bomber Girl" },
             {EndingType.Ending_Sucide, "There Are No Angels" },
             {EndingType.Ending_Jine, "Flatline" },
             {EndingType.Ending_KowaiInternet, "Internet Overdose" },
             {EndingType.Ending_Yarisute, "Nerdy Girl Overload" },
             {EndingType.Ending_Kyouso, "Welcome To My Religion" },
             {EndingType.Ending_Jisatu, "Blazing Hell" },
             {EndingType.Ending_Jikka, "So Close Yet So Far" },
             {EndingType.Ending_Ginga, "Galactic Express" },
             {EndingType.Ending_DarkAngel, "Dark Angel" },
             {EndingType.Ending_Ideon, "Internet Runaway Angel: Be Invoked" }
         };

        public static bool IsNightEnding(EndingType ending)
        {
            return ending == EndingType.Ending_Stressful ||
            ending == EndingType.Ending_Healthy ||
            ending == EndingType.Ending_Sukisuki ||
            ending == EndingType.Ending_Ntr ||
            ending == EndingType.Ending_Meta;
        }

        public static bool IsLastDayEnding(EndingType ending)
        {
            return ending == EndingType.Ending_Grand ||
            ending == EndingType.Ending_Happy ||
            ending == EndingType.Ending_Normal ||
            ending == EndingType.Ending_Yarisute ||
            ending == EndingType.Ending_Needy ||
            ending == EndingType.Ending_Sucide ||
            ending == EndingType.Ending_Work ||
            ending == EndingType.Ending_Bad;
        }

        public static Dictionary<int, string> DayPartNames = new()
        {
            {-1, "" },
            {0, "Noon" },
            {1, "Dusk" },
            {2, "Night" },
            {3, "Way too late" }
        };

        public static List<AlphaType> StreamTopicList = new()
        {
            AlphaType.Zatudan,
            AlphaType.Gamejikkyou,
            AlphaType.Otakutalk,
            AlphaType.Imbouron,
            AlphaType.Kaidan,
            AlphaType.ASMR,
            AlphaType.Hnahaisin,
            AlphaType.Kaisetu,
            AlphaType.Taiken,
            AlphaType.Yamihaishin,
            AlphaType.PR ,
            AlphaType.Angel,
            AlphaType.Darkness ,
        };

        public static List<CmdType> HangOutList = new()
        {
            CmdType.EntameGame,
            CmdType.PlayIchatukuTalk,
            CmdType.PlayIchatukuIchatuku,
            CmdType.PlayIchatukuKizu,
            CmdType.PlayMakeLove,
            CmdType.PlayKimeLove
        };

        public static List<ActionType> HangOutActionList = new()
        {
            ActionType.EntameGame,
            ActionType.PlayIchatuku,
            ActionType.PlayMakeLove,
        };

        public static List<CmdType> SleepList = new()
        {
            CmdType.SleepToTwilight1,
            CmdType.SleepToNight2,
            CmdType.SleepToNight1,
            CmdType.SleepToTomorrow3,
            CmdType.SleepToTomorrow2,
            CmdType.SleepToTomorrow1
        };

        public static List<ActionType> SleepActionList = new()
        {
            ActionType.SleepToTwilight,
            ActionType.SleepToNight,
            ActionType.SleepToTomorrow,
        };

        public static List<CmdType> DrugList = new()
        {
            CmdType.OkusuriDaypassModerate,
            CmdType.OkusuriDaypassOverdoseY1,
            CmdType.OkusuriDaypassOverdoseY2,
            CmdType.OkusuriDaypassOverdoseY3,
            CmdType.OkusuriDaypassOverdoseY4,
            CmdType.OkusuriPuronModerate,
            CmdType.OkusuriPuronOverdoseY2,
            CmdType.OkusuriPuronOverdoseY3,
            CmdType.OkusuriPuronOverdoseY4,
            CmdType.OkusuriHiPuronOverdoseY3,
            CmdType.OkusuriHiPuronOverdoseY4,
            CmdType.OkusuriHappaY4,
            CmdType.OkusuriHappaY5,
            CmdType.OkusuriPsyche
        };

        public static List<ActionType> DrugActionList = new()
        {
            ActionType.OkusuriDaypassModerate,
            ActionType.OkusuriDaypassOverdose,
            ActionType.OkusuriPuronModerate,
            ActionType.OkusuriPuronOverdose,
            ActionType.OkusuriHiPuronOverdose,
            ActionType.OkusuriHappa,
            ActionType.OkusuriPsyche
        };

        public static List<CmdType> InternetList = new()
        {
            CmdType.InternetPoketterF0Y12,
            CmdType.InternetPoketterF1Y12,
            CmdType.InternetPoketterPoem,
            CmdType.InternetPoketterF0Y45,
            CmdType.InternetPoketterF1Y45,
            CmdType.InternetPoketterF0Y3,
            CmdType.InternetYoutube,
            CmdType.Internet2chY12,
            CmdType.Internet2chY3,
            CmdType.Internet2chY45,
            CmdType.InternetDeai2
        };

        public static List<ActionType> InternetActionList = new()
        {
            ActionType.InternetPoketter,
            ActionType.InternetPoketterEgosa,
            ActionType.InternetYoutube,
            ActionType.Internet2ch,
            ActionType.InternetDeai
        };

        public static List<CmdType> OutsideList = new()
        {
            CmdType.OdekakeNakano,
            CmdType.OdekakeHarajuku,
            CmdType.OdekakeAkihabara,
            CmdType.OdekakeShibuya,
            CmdType.OdekakeIkebukuro,
            CmdType.OdekakeUeno,
            CmdType.OdekakeJinbocho,
            CmdType.OdekakeShimokitazawa,
            CmdType.OdekakeKichijoji,
            CmdType.OdekakeGisneyland,
            CmdType.OdekakeHikarigaokaPark,
            CmdType.OdekakeAsakusa,
            CmdType.OdekakeShinjuku,
            CmdType.OdekakeToyosu,
            CmdType.OdekakeIchigaya,
            CmdType.OdekakeHospital,
            CmdType.OdekakeZikka,
            CmdType.OdekakeOdaiba,
            CmdType.OdekakeGinga,
        };

        public static List<CmdType> DarknessList = new()
        {
            CmdType.DarknessS1,
            CmdType.DarknessS2,
        };

        public static List<CmdType> StreamListSortedByTopic = new()
        {
            CmdType.Zatudan_1,
            CmdType.Zatudan_2,
            CmdType.Zatudan_3,
            CmdType.Zatudan_4,
            CmdType.Zatudan_5,
            CmdType.Gamejikkyou_1,
            CmdType.Gamejikkyou_2,
            CmdType.Gamejikkyou_3,
            CmdType.Gamejikkyou_4,
            CmdType.Gamejikkyou_5,
            CmdType.Otakutalk_1,
            CmdType.Otakutalk_2,
            CmdType.Otakutalk_3,
            CmdType.Otakutalk_4,
            CmdType.Otakutalk_5,
            CmdType.Imbouron_1,
            CmdType.Imbouron_2,
            CmdType.Imbouron_3,
            CmdType.Imbouron_4,
            CmdType.Imbouron_5,
            CmdType.Error,
            CmdType.Kaidan_1,
            CmdType.Kaidan_2,
            CmdType.Kaidan_3,
            CmdType.Kaidan_4,
            CmdType.Kaidan_5,
            CmdType.ASMR_1,
            CmdType.ASMR_2,
            CmdType.ASMR_3,
            CmdType.ASMR_4,
            CmdType.ASMR_5,
            CmdType.Hnahaisin_1,
            CmdType.Hnahaisin_2,
            CmdType.Hnahaisin_3,
            CmdType.Hnahaisin_4,
            CmdType.Hnahaisin_5,
            CmdType.Kaisetu_1,
            CmdType.Kaisetu_2,
            CmdType.Kaisetu_3,
            CmdType.Kaisetu_4,
            CmdType.Kaisetu_5,
            CmdType.Taiken_1,
            CmdType.Taiken_2,
            CmdType.Taiken_3,
            CmdType.Taiken_4,
            CmdType.Taiken_5,
            CmdType.Yamihaishin_1,
            CmdType.Yamihaishin_2,
            CmdType.Yamihaishin_3,
            CmdType.Yamihaishin_4,
            CmdType.Yamihaishin_5,
            CmdType.PR_1,
            CmdType.PR_2,
            CmdType.PR_3,
            CmdType.PR_4,
            CmdType.PR_5,
            CmdType.Angel_1,
            CmdType.Angel_2,
            CmdType.Angel_3,
            CmdType.Angel_4,
            CmdType.Angel_5,
            CmdType.Angel_6,
        };
        public static List<CmdType> StreamListSortedByIdea = new()
        {
            CmdType.Zatudan_1,
            CmdType.Gamejikkyou_1,
            CmdType.Gamejikkyou_5,
            CmdType.Imbouron_2,
            CmdType.Kaisetu_3,
            CmdType.PR_3,
            CmdType.ASMR_2,
            CmdType.Hnahaisin_1,
            CmdType.Kaisetu_2,
            CmdType.Taiken_5,
            CmdType.Yamihaishin_3,
            CmdType.PR_4,
            CmdType.ASMR_3,
            CmdType.Hnahaisin_2,
            CmdType.Hnahaisin_5,
            CmdType.Otakutalk_3,
            CmdType.ASMR_1,
            CmdType.Yamihaishin_1,
            CmdType.Yamihaishin_4,
            CmdType.Otakutalk_4,
            CmdType.Yamihaishin_5,
            CmdType.Imbouron_4,
            CmdType.Imbouron_5,
            CmdType.Error,
            CmdType.Kaisetu_5,
            CmdType.Zatudan_4,
            CmdType.Zatudan_5,
            CmdType.Gamejikkyou_3,
            CmdType.Imbouron_1,
            CmdType.Kaidan_4,
            CmdType.Kaisetu_4,
            CmdType.Otakutalk_2,
            CmdType.Taiken_2,
            CmdType.Yamihaishin_2,
            CmdType.PR_5,
            CmdType.Otakutalk_1,
            CmdType.Otakutalk_5,
            CmdType.Imbouron_3,
            CmdType.ASMR_4,
            CmdType.Hnahaisin_3,
            CmdType.Kaisetu_1,
            CmdType.Taiken_3,
            CmdType.PR_2,
            CmdType.Zatudan_2,
            CmdType.Kaidan_1,
            CmdType.Kaidan_2,
            CmdType.Kaidan_5,
            CmdType.ASMR_5,
            CmdType.Taiken_4,
            CmdType.Gamejikkyou_2,
            CmdType.Taiken_1,
            CmdType.Zatudan_3,
            CmdType.Kaidan_3,
            CmdType.Gamejikkyou_3,
            CmdType.Hnahaisin_4,
            CmdType.Angel_1,
            CmdType.PR_1,
            CmdType.Angel_2,
            CmdType.Angel_3,
            CmdType.Angel_4,
            CmdType.Angel_5,
            CmdType.Angel_6
        };

        public static string ParentActionName(ActionType action)
        {
            if (action == ActionType.Haishin) return "Stream";
            if (action.ToString().Contains("Play") || action == ActionType.EntameGame) return "Hang Out";
            if (action.ToString().Contains("Sleep")) return "Sleep";
            if (action.ToString().Contains("Okusuri")) return "Medication";
            if (action.ToString().Contains("Internet")) return "Internet";
            if (action.ToString().Contains("Odekake")) return "Go Out";
            if (action.ToString().Contains("Darkness")) return "Darkness";
            return "";
        }

        public static int ParentActionIndex(ActionType action)
        {
            if (action == ActionType.Haishin) return 0;
            if (action.ToString().Contains("Play") || action == ActionType.EntameGame) return 1;
            if (action.ToString().Contains("Sleep")) return 2;
            if (action.ToString().Contains("Okusuri")) return 3;
            if (action.ToString().Contains("Internet")) return 4;
            if (action.ToString().Contains("Odekake")) return 5;
            if (action.ToString().Contains("Darkness")) return 6;
            return -1;
        }

        public static ActionType CmdToActionConverter(CmdType cmd)
        {
            string cmdName = cmd.ToString();
            if (cmdName.Contains('_'))
            {
                return ActionType.Haishin;
            }
            if (cmd == CmdType.Error)
            {
                return ActionType.Haishin;
            }
            if (cmd == CmdType.EntameGame)
            {
                return ActionType.EntameGame;
            }
            if (cmdName.Contains("PlayIchatuku"))
            {
                return ActionType.PlayIchatuku;
            }
            if (cmd == CmdType.PlayMakeLove || cmd == CmdType.PlayKimeLove)
            {
                return ActionType.PlayMakeLove;
            }
            if (cmd == CmdType.SleepToTwilight1)
            {
                return ActionType.SleepToTwilight;
            }
            if (cmdName.Contains("SleepToTomorrow"))
            {
                return ActionType.SleepToTomorrow;
            }
            if (cmdName.Contains("SleepToNight"))
            {
                return ActionType.SleepToNight;
            }
            if (cmd == CmdType.OkusuriDaypassModerate)
            {
                return ActionType.OkusuriDaypassModerate;
            }
            if (cmd == CmdType.OkusuriPuronModerate)
            {
                return ActionType.OkusuriPuronModerate;
            }
            if (cmd == CmdType.OkusuriHipuronModerate)
            {
                return ActionType.OkusuriHipuronModerate;
            }
            if (cmdName.Contains("Daypass"))
            {
                return ActionType.OkusuriDaypassOverdose;
            }
            if (cmdName.Contains("HiPuron"))
            {
                return ActionType.OkusuriHiPuronOverdose;
            }
            if (cmdName.Contains("Happa"))
            {
                return ActionType.OkusuriHappa;
            }
            if (cmdName.Contains("Puron"))
            {
                return ActionType.OkusuriPuronOverdose;
            }
            if (cmd == CmdType.OkusuriPsyche)
            {
                return ActionType.OkusuriPsyche;
            }
            if (cmd == CmdType.InternetPoketterF0Y3)
            {
                return ActionType.InternetPoketterEgosa;
            }
            if (cmdName.Contains("InternetPoketter"))
            {
                return ActionType.InternetPoketter;
            }
            if (cmd == CmdType.InternetYoutube)
            {
                return ActionType.InternetYoutube;
            }
            if (cmdName.Contains("2ch"))
            {
                return ActionType.Internet2ch;
            }
            if (cmd == CmdType.InternetDeai2)
            {
                return ActionType.InternetDeai;
            }
            if (cmdName.Contains("Odekake"))
            {
                return Enum.TryParse(cmdName, out ActionType action) ? action : ActionType.None;
            }
            if (cmd == CmdType.DarknessS1 || cmd == CmdType.DarknessS2)
            {
                return ActionType.Darkness;
            }
            return ActionType.None;
        }

        public static string CmdName(CmdType cmd)
        {
            string cmdName = cmd.ToString();
            if (cmdName.Contains('_'))
            {
                AlphaType streamTopic = (AlphaType)Enum.Parse(typeof(AlphaType), cmdName.ToString().Split('_')[0]);
                int streamLevel = int.Parse(cmdName.ToString().Split('_')[1]);
                switch (streamTopic)
                {
                    case AlphaType.Zatudan:
                        return "Chat & Chill " + streamLevel.ToString();
                    case AlphaType.Gamejikkyou:
                        return "Letsplay " + streamLevel.ToString();
                    case AlphaType.Otakutalk:
                        return "Nerd Talk " + streamLevel.ToString();
                    case AlphaType.Imbouron:
                        return "Conspiracy Theories " + streamLevel.ToString();
                    case AlphaType.Kaidan:
                        return "Netlore " + streamLevel.ToString();
                    case AlphaType.ASMR:
                        return "ASMR " + streamLevel.ToString();
                    case AlphaType.Hnahaisin:
                        return "Sexy Stream " + streamLevel.ToString();
                    case AlphaType.Kaisetu:
                        return "Angel Explains " + streamLevel.ToString();
                    case AlphaType.Taiken:
                        return "KAngel Tries Stuff " + streamLevel.ToString();
                    case AlphaType.Yamihaishin:
                        return "Breakdown Stream " + streamLevel.ToString();
                    case AlphaType.PR:
                        return "Sponsorships " + streamLevel.ToString();
                    case AlphaType.Angel:
                        return "Internet Angel " + streamLevel.ToString();
                    case AlphaType.Darkness:
                        return "Darkness " + streamLevel.ToString();
                    default:
                        return "";
                }
            }
            switch (cmd)
            {
                case CmdType.EntameGame:
                    return "Play Game";
                case CmdType.PlayIchatukuTalk:
                    return "Spend Time Together";
                case CmdType.PlayIchatukuIchatuku:
                    return "Cuddle";
                case CmdType.PlayIchatukuKizu:
                    return "Pity Party";
                case CmdType.PlayMakeLove:
                    return "***";
                case CmdType.PlayKimeLove:
                    return "Have Chem***";
                case CmdType.SleepToTwilight1:
                    return "Sleep To Dusk";
                case CmdType.SleepToNight2:
                    return "Sleep To Night (Noon)";
                case CmdType.SleepToNight1:
                    return "Sleep To Night (Dusk)";
                case CmdType.SleepToTomorrow3:
                    return "Sleep To Tomorrow (Noon)";
                case CmdType.SleepToTomorrow2:
                    return "Sleep To Tomorrow (Dusk)";
                case CmdType.SleepToTomorrow1:
                    return "Sleep To Tomorrow (Night)";
                case CmdType.OkusuriDaypassModerate:
                    return "Prescription (normal dose)";
                case CmdType.OkusuriDaypassOverdoseY1:
                    return "Prescription GO! (strongest)";
                case CmdType.OkusuriDaypassOverdoseY2:
                    return "Prescription GO! (Dylsem)";
                case CmdType.OkusuriDaypassOverdoseY3:
                    return "Prescription GO! (Embian)";
                case CmdType.OkusuriDaypassOverdoseY4:
                case CmdType.OkusuriDaypassOverdoseY5:
                    return "Prescription GO! (weakest)";
                case CmdType.OkusuriPuronModerate:
                    return "OTC (normal dose)";
                case CmdType.OkusuriPuronOverdoseY2:
                    return "OTC GO! (strongest)";
                case CmdType.OkusuriPuronOverdoseY3:
                    return "OTC GO! (Embian)";
                case CmdType.OkusuriPuronOverdoseY4:
                    return "OTC GO! (weakest)";
                case CmdType.OkusuriHipuronModerate:
                    return "Sleeping Pills (normal dose)";
                case CmdType.OkusuriHiPuronOverdoseY3:
                    return "Sleeping Pills GO! (strongest)";
                case CmdType.OkusuriHiPuronOverdoseY4:
                    return "Sleeping Pills GO! (weakest)";
                case CmdType.OkusuriHappaY4:
                    return "Magic Grass GO! (strongest)";
                case CmdType.OkusuriHappaY5:
                    return "Magic Grass GO! (weakest)";
                case CmdType.OkusuriPsyche:
                    return "Magic Paper GO!";
                case CmdType.InternetPoketterF0Y12:
                    return "Daily Tweet";
                case CmdType.InternetPoketterF0Y3:
                    return "Vanity Search";
                case CmdType.InternetPoketterF0Y45:
                    return "Vent On Main";
                case CmdType.InternetPoketterF1Y12:
                    return "Send Business Tweet";
                case CmdType.InternetPoketterF1Y3:
                    return "Vent On Priv";
                case CmdType.InternetPoketterF1Y45:
                    return "Bash Others";
                case CmdType.InternetPoketterPoem:
                    return "Muse";
                case CmdType.InternetYoutube:
                    return "Video Streaming";
                case CmdType.Internet2chY12:
                    return "Search Opinions";
                case CmdType.Internet2chY3:
                    return "Go Undercover";
                case CmdType.Internet2chY45:
                    return "Stir Shit";
                case CmdType.InternetDeai2:
                    return "Dinder";
                case CmdType.OdekakeGinga:
                    return "Galactic Rail";
                case CmdType.OdekakeZikka:
                    return "Ame's Parents";
                case CmdType.OdekakeOdaiba:
                    return "Music Video";
                case CmdType.DarknessS1:
                    return "Cut Wrists";
                case CmdType.DarknessS2:
                    return "Go Berserk";
                case CmdType.Error:
                    return "Conspiracy Theories 6";
                default: break;
            }
            if (cmdName.Contains("Odekake"))
            {
                return cmdName.Remove(0, 7);
            }
            return "";
        }

        public static bool IsOverdoseAction(TargetActionData action)
        {
            var a = action.TargetAction.Action;
            return a.ToString().Contains("Okusuri") &&
                a != ActionType.OkusuriPuronModerate &&
                a != ActionType.OkusuriHipuronModerate &&
                a != ActionType.OkusuriDaypassModerate;
        }

        public static (int DayIndex, int DayPart, CmdType Idea) ActionToStreamIdea(TargetActionData pastAction, TargetActionData presentAction, EndingBranchData branch)
        {
            var noMeds = branch.NoMeds;
            int day = presentAction.TargetAction.DayIndex;
            int dayPart = presentAction.TargetAction.DayPart;
            switch (presentAction.TargetAction.Action)
            {

                case ActionType.EntameGame:
                    if (CanGetStreamIdea(1,AlphaType.Gamejikkyou)) 
                        return (day, dayPart, CmdType.Gamejikkyou_1);
                    if (CanGetStreamIdea(5, AlphaType.Gamejikkyou)) 
                        return (day, dayPart, CmdType.Gamejikkyou_5);
                    if (CanGetStreamIdea(2, AlphaType.Imbouron)) 
                        return (day, dayPart, CmdType.Imbouron_2);
                    if (CanGetStreamIdea(3, AlphaType.Kaisetu)) 
                        return (day, dayPart, CmdType.Kaisetu_3);
                    if (CanGetStreamIdea(3, AlphaType.PR)) 
                        return (day, dayPart, CmdType.PR_3);
                    break;
                case ActionType.PlayIchatuku:
                    if (CanGetStreamIdea(2, AlphaType.ASMR)) 
                        return (day, dayPart, CmdType.ASMR_2);
                    if (CanGetStreamIdea(1, AlphaType.Hnahaisin)) 
                        return (day, dayPart, CmdType.Hnahaisin_1);
                    if (CanGetStreamIdea(2, AlphaType.Kaisetu)) 
                        return (day, dayPart, CmdType.Kaisetu_2);
                    if (CanGetStreamIdea(5,AlphaType.Taiken)) 
                        return (day, dayPart, CmdType.Taiken_5);
                    if (CanGetStreamIdea(3,AlphaType.Yamihaishin)) 
                        return (day, dayPart, CmdType.Yamihaishin_3);
                    if (CanGetStreamIdea(4, AlphaType.PR))
                        return (day, dayPart, CmdType.PR_4);
                    break;
                case ActionType.PlayMakeLove:
                    if (CanGetStreamIdea(3,AlphaType.ASMR)) 
                        return (day, dayPart, CmdType.ASMR_3);
                    if (CanGetStreamIdea(2,AlphaType.Hnahaisin)) 
                        return (day, dayPart, CmdType.Hnahaisin_2);
                    if (CanGetStreamIdea(5,AlphaType.Hnahaisin)) 
                        return (day, dayPart, CmdType.Hnahaisin_5);
                    break;
                case ActionType.SleepToTwilight:
                    if (CanGetStreamIdea(3,AlphaType.Otakutalk)) 
                        return (day, dayPart, CmdType.Otakutalk_3);
                    break;
                case ActionType.SleepToTomorrow:
                    if (CanGetStreamIdea(1,AlphaType.ASMR)) 
                        return (day, dayPart, CmdType.ASMR_1);
                    break;
                case ActionType.OkusuriDaypassOverdose:
                    if (noMeds.isEventing) break;
                    if (CanGetStreamIdea(1,AlphaType.Yamihaishin)) 
                        return (day, dayPart, CmdType.Yamihaishin_1);
                    break;
                case ActionType.OkusuriHiPuronOverdose:
                    if (noMeds.isEventing) break;
                    if (CanGetStreamIdea(4,AlphaType.Yamihaishin)) 
                        return (day, dayPart, CmdType.Yamihaishin_4);
                    break;
                case ActionType.OkusuriHappa:
                    if (noMeds.isEventing) break;
                    if (CanGetStreamIdea(4,AlphaType.Otakutalk)) 
                        return (day, dayPart, CmdType.Otakutalk_4);
                    if (CanGetStreamIdea(5, AlphaType.Yamihaishin)) 
                        return (day, dayPart, CmdType.Yamihaishin_5);
                    break;
                case ActionType.OkusuriPsyche:
                    if (noMeds.isEventing) break;
                    if (CanGetStreamIdea(4, AlphaType.Imbouron)) 
                        return (day, dayPart, CmdType.Imbouron_4);
                    if (CanGetStreamIdea(5, AlphaType.Imbouron)) 
                        return (day, dayPart, CmdType.Imbouron_5);
                    if (CanGetStreamIdea(6, AlphaType.Imbouron)) 
                        return (day, dayPart, CmdType.Error);
                    if (CanGetStreamIdea(5, AlphaType.Kaisetu))
                        return (day, dayPart, CmdType.Kaisetu_5);
                    break;
                case ActionType.InternetPoketter:
                    if (CanGetStreamIdea(4, AlphaType.Zatudan))
                        return (day, dayPart, CmdType.Zatudan_4);
                    if (CanGetStreamIdea(5, AlphaType.Zatudan))
                        return (day, dayPart, CmdType.Zatudan_5);
                    if (CanGetStreamIdea(3, AlphaType.Gamejikkyou))
                        return (day, dayPart, CmdType.Gamejikkyou_3);
                    if (CanGetStreamIdea(1, AlphaType.Imbouron))
                        return (day, dayPart, CmdType.Imbouron_1);
                    if (CanGetStreamIdea(4, AlphaType.Kaidan))
                        return (day, dayPart, CmdType.Kaidan_4);
                    if (CanGetStreamIdea(4, AlphaType.Kaisetu))
                        return (day, dayPart, CmdType.Kaisetu_4);
                    break;
                case ActionType.InternetPoketterEgosa:
                    if (CanGetStreamIdea(2, AlphaType.Otakutalk))
                        return (day, dayPart, CmdType.Otakutalk_2);
                    if (CanGetStreamIdea(2, AlphaType.Taiken))
                        return (day, dayPart, CmdType.Taiken_2);
                    if (CanGetStreamIdea(2, AlphaType.Yamihaishin))
                        return (day, dayPart, CmdType.Yamihaishin_2);
                    if (CanGetStreamIdea(5, AlphaType.PR))
                        return (day, dayPart, CmdType.PR_5);
                    break;
                case ActionType.InternetYoutube:
                    if (CanGetStreamIdea(1, AlphaType.Otakutalk))
                        return (day, dayPart, CmdType.Otakutalk_1);
                    if (CanGetStreamIdea(5, AlphaType.Otakutalk))
                        return (day, dayPart, CmdType.Otakutalk_5);
                    if (CanGetStreamIdea(3, AlphaType.Imbouron))
                        return (day, dayPart, CmdType.Imbouron_3);
                    if (CanGetStreamIdea(4, AlphaType.ASMR))
                        return (day, dayPart, CmdType.ASMR_4);
                    if (CanGetStreamIdea(3, AlphaType.Hnahaisin))
                        return (day, dayPart, CmdType.Hnahaisin_3);
                    if (CanGetStreamIdea(1, AlphaType.Kaisetu))
                        return (day, dayPart, CmdType.Kaisetu_1);
                    if (CanGetStreamIdea(3, AlphaType.Taiken))
                        return (day, dayPart, CmdType.Taiken_3);
                    if (CanGetStreamIdea(2, AlphaType.PR))
                        return (day, dayPart, CmdType.PR_2);
                    break;
                case ActionType.Internet2ch:
                    if (CanGetStreamIdea(2, AlphaType.Zatudan))
                        return (day, dayPart, CmdType.Zatudan_2);
                    if (CanGetStreamIdea(1, AlphaType.Kaidan))
                        return (day, dayPart, CmdType.Kaidan_1);
                    if (CanGetStreamIdea(2, AlphaType.Kaidan))
                        return (day, dayPart, CmdType.Kaidan_2);
                    if (CanGetStreamIdea(5, AlphaType.Kaidan))
                        return (day, dayPart, CmdType.Kaidan_5);
                    break;
                case ActionType.InternetDeai:
                    if (CanGetStreamIdea(5, AlphaType.ASMR))
                        return (day, dayPart, CmdType.ASMR_5);
                    if (CanGetStreamIdea(4, AlphaType.Taiken))
                        return (day, dayPart, CmdType.Taiken_4);
                    break;
                case ActionType.OdekakeHikarigaokaPark:
                    if (CanGetStreamIdea(3, AlphaType.Zatudan))
                        return (day, dayPart, CmdType.Zatudan_3);
                    break;
                case ActionType.OdekakeAkihabara:
                    if (CanGetStreamIdea(2, AlphaType.Gamejikkyou))
                        return (day, dayPart, CmdType.Gamejikkyou_2);
                    break;
                case ActionType.OdekakeNakano:
                    if (CanGetStreamIdea(4, AlphaType.Gamejikkyou))
                        return (day, dayPart, CmdType.Gamejikkyou_4);
                    break;
                case ActionType.OdekakeIchigaya:
                    if (CanGetStreamIdea(3, AlphaType.Kaidan))
                        return (day, dayPart, CmdType.Kaidan_3);
                    break;
                case ActionType.OdekakeShibuya:
                    if (CanGetStreamIdea(4, AlphaType.Hnahaisin))
                        return (day, dayPart, CmdType.Hnahaisin_4);
                    break;
                case ActionType.OdekakeHarajuku:
                    if (CanGetStreamIdea(1, AlphaType.Taiken))
                        return (day, dayPart, CmdType.Taiken_1);
                    break;
                default: break;
            }
            return (0, 0, CmdType.None);

            bool CanGetStreamIdea(int streamLevelToGet, AlphaType streamTopic)
            {
                int followerReq;
                CmdType streamIdea;
                switch (streamLevelToGet)
                {
                    case 3:
                        followerReq = 10000;
                        break;
                    case 4:
                        followerReq = 100000;
                        break;
                    case 5:
                        followerReq = 250000;
                        break;
                    default:
                        followerReq = 0;
                        break;
                }
                if (streamLevelToGet < 1 || streamLevelToGet > 6)
                    throw new ArgumentOutOfRangeException("Only 1-6 is valid for this argument.");
                if (streamLevelToGet == 6 && streamTopic != AlphaType.Imbouron)
                    throw new ArgumentOutOfRangeException("Only 6 is valid if AlphaType is AlphaType.Imbouron.");
                if (streamLevelToGet == 6 && streamTopic == AlphaType.Imbouron)
                    return branch.UsedStreamExistsBeforeAction(presentAction, CmdType.Imbouron_5) && !branch.StreamIdeaExistsBeforeAction(presentAction, CmdType.Error);
                streamIdea = (CmdType)Enum.Parse(typeof(CmdType), $"{streamTopic}_{streamLevelToGet}");
                if (streamLevelToGet == 1)
                    return !branch.StreamIdeaExistsBeforeAction(presentAction, streamIdea);
                else
                {
                    var pastStream = (CmdType)Enum.Parse(typeof(CmdType), $"{streamTopic}_{streamLevelToGet-1}");
                    return pastAction.Followers >= followerReq && branch.UsedStreamExistsBeforeAction(presentAction, pastStream) && !branch.StreamIdeaExistsBeforeAction(presentAction, streamIdea);
                }
            }
        }

        public static void InitializeMilestoneIdea(TargetActionData action, EndingBranchData branch, bool isReallyStress)
        {

            var ideas = branch.StreamIdeaList;
            var isReallyStressed_EventCounter = branch.isReallyStressed;
            if (action.TargetAction.DayIndex < 4)
            {
                action.MilestoneIdea = CmdType.None;
                return;
            }
            if (action.TargetAction.DayPart != 0)
            {
                action.MilestoneIdea = CmdType.None;
                return;
            }
            if (action.Followers >= 10000 && !branch.StreamIdeaExistsBeforeAction(action,CmdType.Angel_1))
            {
                ideas.Add(new(action.TargetAction.DayIndex, action.TargetAction.DayPart, CmdType.Angel_1));
                action.MilestoneIdea = CmdType.Angel_1;
                return;
            }
            if (action.Followers >= 30000 && !branch.StreamIdeaExistsBeforeAction(action, CmdType.PR_1))
            {
                ideas.Add(new(action.TargetAction.DayIndex, action.TargetAction.DayPart, CmdType.PR_1));
                action.MilestoneIdea = CmdType.PR_1;
                return;
            }
            if (action.Followers >= 100000 && !branch.StreamIdeaExistsBeforeAction(action, CmdType.Angel_2))
            {
                ideas.Add(new(action.TargetAction.DayIndex, action.TargetAction.DayPart, CmdType.Angel_2));
                action.MilestoneIdea = CmdType.Angel_2;
                return;
            }
            if (action.Followers >= 250000 && !branch.StreamIdeaExistsBeforeAction(action, CmdType.Angel_3))
            {
                ideas.Add(new(action.TargetAction.DayIndex, action.TargetAction.DayPart, CmdType.Angel_3));
                action.MilestoneIdea = CmdType.Angel_3;
                return;
            }
            if (action.Followers >= 500000 && !branch.StreamIdeaExistsBeforeAction(action, CmdType.Angel_4))
            {
                ideas.Add(new(action.TargetAction.DayIndex, action.TargetAction.DayPart, CmdType.Angel_4));
                action.MilestoneIdea = CmdType.Angel_4;
                return;
            }
            if (((!isReallyStress && !isReallyStressed_EventCounter.isEventing) || (isReallyStress || isReallyStressed_EventCounter.isEventing && isReallyStressed_EventCounter.DayIndex > action.TargetAction.DayIndex)) && action.Followers >= 1000000 && !branch.StreamIdeaExistsBeforeAction(action, CmdType.Angel_5) && !branch.StreamIdeaExistsBeforeAction(action, CmdType.Angel_6))
            {
                ideas.Add(new(action.TargetAction.DayIndex, action.TargetAction.DayPart, CmdType.Angel_5));
                action.MilestoneIdea = CmdType.Angel_5;
                return;
            }
            if (isReallyStress || (isReallyStressed_EventCounter.isEventing && isReallyStressed_EventCounter.DayIndex < action.TargetAction.DayIndex))
            {
                if (branch.StreamIdeaExistsBeforeAction(action, CmdType.Angel_5) && branch.UsedStreamExistsBeforeAction(action, CmdType.Angel_5))
                {
                    action.MilestoneIdea = CmdType.None;
                    return;
                }
                if (action.Followers >= 1000000 && !branch.StreamIdeaExistsBeforeAction(action, CmdType.Angel_6))
                {
                    ideas.Add(new(action.TargetAction.DayIndex, action.TargetAction.DayPart, CmdType.Angel_6));
                    action.MilestoneIdea = CmdType.Angel_6;
                    return;
                }
                return;
            }
            action.MilestoneIdea = CmdType.None;
        }
    }
}
