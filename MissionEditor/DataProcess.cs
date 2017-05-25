using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Xml.Linq;

namespace MissionEditor
{
    internal class DataProcess
    {
        public DataTable TempletDataTable { get; set; } = new DataTable();

        public DataProcess()
        {
            CreateTempletDataTable();
        }

        private static readonly Dictionary<string, Type> FieldMap = new Dictionary<string, Type>
        {
            ["MissionID"] = typeof(int),
            ["MissionName"] = typeof(string),
            ["MissionTypeString"] = typeof(string),
            ["MinLevel"] = typeof(int),
            ["MaxLevel"] = typeof(int),
            ["TransMinLevel"] = typeof(int),
            ["TransMaxLevel"] = typeof(int),
            ["RequestMissionList"] = typeof(int[]),
            ["RequestRoleIDList"] = typeof(long[]),
            ["PostMissionList"] = typeof(int[]),
            ["TransformID"] = typeof(int),
            ["NoteInfo"] = typeof(string),
            ["ExpReward"] = typeof(long),
            ["MoneyReward"] = typeof(long),
            ["PetExpReward"] = typeof(long),
            ["ShengWang"] = typeof(int),
            ["SMoney"] = typeof(long),
            ["RewardMapJumpType"] = typeof(int),
            ["RewardMapID"] = typeof(int),
            ["RewardMapXPos"] = typeof(int),
            ["RewardMapYPos"] = typeof(int),
            ["ProcessBarTime"] = typeof(int),
            ["ProcessBarText"] = typeof(string),
            ["ProcessBarColor"] = typeof(string),
            ["DisPlayNPCID"] = typeof(int[]),
            ["RewardItemIDList"] = typeof(int[]),
            ["RewardItemNumList"] = typeof(int[]),
            ["RewardItemShapeIDList"] = typeof(int[]),
            ["RewardItemIsBindList"] = typeof(int[]),
            ["MissionType"] = typeof(int),
            ["ActiveInfoNpcID"] = typeof(int),
            ["ActiveInfoMapID"] = typeof(int),
            ["ActiveInfoLeftPos"] = typeof(int),
            ["ActiveInfoTopPos"] = typeof(int),
            ["ActiveInfoRightPos"] = typeof(int),
            ["ActiveInfoBottomPos"] = typeof(int),
            ["ActiveInfoTargetID"] = typeof(int),
            ["ActiveInfoTargetNum"] = typeof(int),
            ["ActiveInfoMiniStep"] = typeof(int),
            ["ActiveInfoStepProbability"] = typeof(int),
            ["ActiveInfoMaxStep"] = typeof(int),
            ["ActiveInfoTeamState"] = typeof(int),
            ["ActiveInfoTimeLimit"] = typeof(int),
            ["ActiveInfoIsRestartTimer"] = typeof(int),
            ["ActiveInfoGiveBackMoney"] = typeof(long),
            ["ActiveInfoGiveBackPetID"] = typeof(int),
            ["ActiveInfoUseItemID"] = typeof(int),
            ["ActiveInfoOtherType"] = typeof(int),
            ["QuestionInfoCorrectAnswer"] = typeof(string),
            ["QuestionInfoWrongAnswerList"] = typeof(string[]),
            ["QuestionInfoNpcID"] = typeof(int),
            ["QuestionInfoConversion"] = typeof(string),
            ["TaskInfoDescriptionListA"] = typeof(string),
            ["TaskInfoPurposeListA"] = typeof(string),
            ["TaskInfoTraceListA"] = typeof(string),
            ["AIInfoAIID"] = typeof(int),
            ["AIInfoBattleResult"] = typeof(int),
            ["AIInfoDeathPunish"] = typeof(int),
            ["AIInfoTeamSteate"] = typeof(int),
            ["AIInfoBattleLevel"] = typeof(string),
            ["BattleInfoBattleMapType"] = typeof(int),
            ["BattleInfoBattleZoneID"] = typeof(int),
            ["BattleInfoDrop"] = typeof(int),
            ["BattleInfoBattleTimes"] = typeof(int),
            ["BattleInfoMonsterList"] = typeof(int[]),
            ["BattleInfoMonsterNum"] = typeof(int),
            ["BattleInfoDropItemID"] = typeof(int),
            ["BattleInfoDropItemNum"] = typeof(int),
            ["ScenarioInfoAnimationID"] = typeof(int),
            ["ScenarioInfoBranchNpcID"] = typeof(int),
            ["ScenarioInfoBranchNote"] = typeof(string),
            ["ScenarioInfoBranchOptionList"] = typeof(string[]),
            ["ScenarioInfoNpcConversationList"] = typeof(string[]),
            ["ScenarioInfoNpcID"] = typeof(int[]),
            ["ScenarioInfoFinishConversationList"] = typeof(string[]),
            ["ScenarioInfoFinishNpcID"] = typeof(int[])
        };

        private static readonly Dictionary<int, int> _missionTypeMap = new Dictionary<int, int>
        {
            [0] = 10,//点击NPC
            [1] = 11,//给予金钱
            [2] = 12,//给予物品
            [3] = 13,//给予宠物
            [4] = 17,//答题
            [5] = 22,//使用物品
            [6] = 32,//练功区掉落物品
            [7] = 34,//练功区战斗计场次
            [8] = 35,//练功区战斗计数量
            [9] = 50,//无条件
            [10] = 54,//步数触发
            [11] = 56,//区域触发\护送
            [12] = 40,//NPC战斗
            [13] = 58,//等级限制任务
            [14] = 59//特殊类型
        };

        private static readonly Dictionary<int, int> ActiveInfoTeamStateMap = new Dictionary<int, int>
        {
            [0] = 0,//Yes
            [1] = 1,//No
            [2] = 2//Both
        };

        private static readonly Dictionary<int, int> BattleInfoBattleMapTypeMap = new Dictionary<int, int>
        {
            [0] = 0,//明雷区
            [1] = 1//暗雷区
        };

        private static readonly Dictionary<int, int> AIInfoTeamSteateMap = new Dictionary<int, int>
        {
            [0] = 0,//要求胜利
            [1] = 1//均可
        };

        private static readonly Dictionary<int, int> AIInfoDeathPunishMap = new Dictionary<int, int>
        {
            [0] = 0,//0：不接受
            [1] = 1//1：接受
        };

        public static readonly Dictionary<int, int> RewardMapJumpTypeMap = new Dictionary<int, int>
        {
            [0] = 0,//无
            [1] = 1,//传送
            [2] = 2,//飞行
            [3] = 3//副本
        };

        private static Type GeFieldType(string fieldName) => FieldMap[fieldName];

        public static int GetMissionType(int selectItemIndex) => _missionTypeMap[selectItemIndex];

        public static int GetMissionTypeSelectIndex(int missionType) => _missionTypeMap
            .FirstOrDefault(p => p.Value == missionType).Key;

        public static int GetBattleInfoBattleMapType(int selectItemIndex) => BattleInfoBattleMapTypeMap[selectItemIndex];

        public static int GetBattleInfoBattleMapTypeSelectIndex(int battleInfoBattleMapType) => BattleInfoBattleMapTypeMap
            .FirstOrDefault(p => p.Value == battleInfoBattleMapType).Key;

        public static int GetActiveInfoTeamState(int selectItemIndex) => ActiveInfoTeamStateMap[selectItemIndex];

        public static int GetActiveInfoTeamStateSelectIndex(int activeInfoTeamState) => ActiveInfoTeamStateMap
            .FirstOrDefault(p => p.Value == activeInfoTeamState).Key;

        public static int GetAIInfoTeamSteate(int selectItemIndex) => AIInfoTeamSteateMap[selectItemIndex];

        public static int GetAIInfoTeamSteateSelectIndex(int aiInfoTeamSteate) => AIInfoTeamSteateMap
            .FirstOrDefault(p => p.Value == aiInfoTeamSteate).Key;

        public static int GetAIInfoDeathPunish(int selectItemIndex) => AIInfoDeathPunishMap[selectItemIndex];

        public static int GetAIInfoDeathPunishSelectIndex(int aiInfoDeathPunish) => AIInfoDeathPunishMap
            .FirstOrDefault(p => p.Value == aiInfoDeathPunish).Key;

        public static int GetRewardMapJumpType(int selectItemIndex) => RewardMapJumpTypeMap[selectItemIndex];

        public static int GetRewardMapJumpTypeSelectIndex(int rewardMapJumpType) => RewardMapJumpTypeMap
            .FirstOrDefault(p => p.Value == rewardMapJumpType).Key;

        private void CreateTempletDataTable()
        {
            foreach (string fieldName in FieldMap.Keys)
            {
                if (GeFieldType(fieldName) == typeof(int) ||
                    GeFieldType(fieldName) == typeof(long) ||
                    GeFieldType(fieldName) == typeof(string))
                {
                    DataColumn column = new DataColumn(fieldName, GeFieldType(fieldName));
                    TempletDataTable.Columns.Add(column);
                }
                else
                {
                    for (int i = 0; i < 50; i++)
                    {
                        DataColumn column = new DataColumn(fieldName + i, GeFieldType(fieldName).GetElementType());
                        TempletDataTable.Columns.Add(column);
                    }
                }
            }
        }

        public void CreateXmlFile()
        {
            XDocument mainMissionInfo = new XDocument(
                new XElement("namespace", new XAttribute("name", "task")));

            XElement cMainMissionInfo = new XElement(
                new XElement("bean",
                    new XAttribute("name", "CMainMissionInfo"),
                    new XAttribute("from", "z主线任务.xlsx"),
                    new XAttribute("genxml", "client")));

            XElement sMainMissionInfo = new XElement(
                new XElement("bean",
                    new XAttribute("name", "SMainMissionInfo"),
                    new XAttribute("from", "z主线任务.xlsx"),
                    new XAttribute("genxml", "server")));

            foreach (string fieldName in FieldMap.Keys)
            {
                XElement newNode;
                if (fieldName == "MissionID")
                {
                    newNode = new XElement("variable",
                        new XAttribute("name", "id"));
                }
                else
                {
                    newNode = new XElement("variable",
                        new XAttribute("name", fieldName));
                }

                if (GeFieldType(fieldName) == typeof(int))
                {
                    newNode.SetAttributeValue("fromCol", fieldName);
                    newNode.SetAttributeValue("type", "int");
                }
                else if (GeFieldType(fieldName) == typeof(long))
                {
                    newNode.SetAttributeValue("fromCol", fieldName);
                    newNode.SetAttributeValue("type", "long");
                }
                else if (GeFieldType(fieldName) == typeof(string))
                {
                    newNode.SetAttributeValue("fromCol", fieldName);
                    newNode.SetAttributeValue("type", "string");
                }
                else if (GeFieldType(fieldName) == typeof(int[]))
                {
                    newNode.SetAttributeValue("fromCol", FromColSplice(fieldName));
                    newNode.SetAttributeValue("type", "vector");
                    newNode.SetAttributeValue("value", "int");
                }
                else if (GeFieldType(fieldName) == typeof(long[]))
                {
                    newNode.SetAttributeValue("fromCol", FromColSplice(fieldName));
                    newNode.SetAttributeValue("type", "vector");
                    newNode.SetAttributeValue("value", "long");
                }
                else if (GeFieldType(fieldName) == typeof(string[]))
                {
                    newNode.SetAttributeValue("fromCol", FromColSplice(fieldName));
                    newNode.SetAttributeValue("type", "vector");
                    newNode.SetAttributeValue("value", "string");
                }

                cMainMissionInfo.Add(newNode);
                sMainMissionInfo.Add(newNode);
            }

            mainMissionInfo.Root?.Add(cMainMissionInfo);
            mainMissionInfo.Root?.Add(sMainMissionInfo);

            mainMissionInfo.Save(@"D,//\ProjectResource\1.xml");
        }

        private static string FromColSplice(string fieldName)
        {
            string fromCol = null;
            for (int i = 0; i < 50; i++)
            {
                fromCol = (i == 49) ? (fromCol + fieldName + i) : (fromCol + fieldName + i + ",");
            }
            return fromCol;
        }
    }
}