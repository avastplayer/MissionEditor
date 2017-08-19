using System;
using System.Collections.Generic;
using System.Data;
using System.Xml.Linq;

namespace MissionEditor
{
    public class TempletManager
    {
        private static volatile TempletManager _instance;
        private static readonly object SyncRoot = new object();

        private TempletManager()
        {
        }

        public static TempletManager Instance
        {
            get
            {
                if (_instance != null) return _instance;
                lock (SyncRoot)
                {
                    if (_instance == null)
                    {
                        _instance = new TempletManager();
                    }
                }
                return _instance;
            }
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

        private static Type GeFieldType(string fieldName) => FieldMap[fieldName];

        public DataTable CreateTempletDataTable()
        {
            DataTable templetDataTable = new DataTable();
            foreach (string fieldName in FieldMap.Keys)
            {
                if (GeFieldType(fieldName) == typeof(int) ||
                    GeFieldType(fieldName) == typeof(long) ||
                    GeFieldType(fieldName) == typeof(string))
                {
                    DataColumn column = new DataColumn(fieldName, GeFieldType(fieldName));
                    templetDataTable.Columns.Add(column);
                }
                else
                {
                    for (int i = 0; i < 50; i++)
                    {
                        DataColumn column = new DataColumn(fieldName + i, GeFieldType(fieldName).GetElementType());
                        templetDataTable.Columns.Add(column);
                    }
                }
            }
            return templetDataTable;
        }

        public void CreateGBeansFile()
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

            mainMissionInfo.Save("mainmission.xml");
        }

        private static string FromColSplice(string fieldName)
        {
            string fromCol = null;
            for (int i = 0; i < 50; i++)
            {
                fromCol = i == 49 ? fromCol + fieldName + i : fromCol + fieldName + i + ",";
            }
            return fromCol;
        }
    }
}